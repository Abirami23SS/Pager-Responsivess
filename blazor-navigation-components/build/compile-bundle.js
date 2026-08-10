'use strict';

/**
 * compile-bundle.js
 * -----------------
 * Generic Gulp tasks for compiling TypeScript and bundling scripts for any
 * Syncfusion Blazor component. Component metadata is driven entirely from
 * config.json — no hardcoding of component names, paths, or externals here.
 *
 * Tasks registered:
 *   compile          – tsc only (all packages)
 *   bundle           – tsc + rollup bundle (all packages)
 *
 * Usage examples:
 *   gulp compile                        → compile all ts files into js files 
 *   gulp bundle                         → bundle all components

 *
 * Adding a new component:
 *   1. Add a new entry under "packages" in config.json with:
 *        "tsEntry"    : entry TS file name (without .ts extension)
 *        "scriptsDir" : folder containing the .ts source files
 *        "outputDir"  : folder where the bundled .js will be written
 *        "externals"  : map of package → global variable (for rollup UMD)
 *   2. That's it — no changes to this file needed.
 */

const path    = require('path');
const gulp    = require('gulp');
const config  = require(path.resolve(__dirname, '../config.json'));
var shelljs   = global.shelljs = global.shelljs || require('shelljs');
var fs        = global.fs      = global.fs      || require('fs');

// ─── Helpers ──────────────────────────────────────────────────────────────────

/**
 * Read the optional --component <Name> CLI argument.
 * e.g.  gulp bundle --component Grid
 * Returns null when the flag is absent (meaning "all components").
 */
function getTargetComponent() {
    const idx = process.argv.indexOf('--component');
    return idx !== -1 ? process.argv[idx + 1] : null;
}

/**
 * Resolve the list of packages to process.
 * When --component is supplied, only that package is returned.
 * When absent, all packages in config.json are returned.
 *
 * @returns {{ name: string, pkg: object }[]}
 */
function resolvePackages() {
    const target   = getTargetComponent();
    const packages = config.packages;

    if (!packages || Object.keys(packages).length === 0) {
        throw new Error('No packages defined in config.json → "packages".');
    }

    if (target) {
        const pkg = packages[target];
        if (!pkg) {
            throw new Error(
                `Component "${target}" not found in config.json. ` +
                `Available: ${Object.keys(packages).join(', ')}`
            );
        }
        validatePackage(target, pkg);
        return [{ name: target, pkg }];
    }

    return Object.keys(packages).map(name => {
        const pkg = packages[name];
        validatePackage(name, pkg);
        return { name, pkg };
    });
}

/**
 * Validate that a package entry in config.json has all required fields.
 *
 * @param {string} name
 * @param {object} pkg
 */
function validatePackage(name, pkg) {
    const required = ['tsEntry', 'scriptsDir', 'outputDir'];
    for (const field of required) {
        if (!pkg[field]) {
            throw new Error(
                `config.json → packages["${name}"] is missing required field: "${field}". ` +
                `Expected structure:\n` +
                `  "tsEntry"    : "<entryFileNameWithoutExtension>",\n` +
                `  "scriptsDir" : "<folderContainingTsFiles>",\n` +
                `  "outputDir"  : "<bundleOutputFolder>",\n` +
                `  "externals"  : { "@scope/pkg": "globalVar" }   // optional`
            );
        }
    }
}

// ─── Core: compile one component (tsc) ────────────────────────────────────────

/**
 * Run `tsc` for a single component package.
 * Writes intermediate .js files to <scriptsDir>/.tmp/
 *
 * @param {{ name: string, pkg: object }} param0
 * @returns {Promise<void>}
 */
async function compileComponent({ name, pkg }) {
    const repoRoot   = path.resolve(__dirname, '..');
    const scriptsDir = path.resolve(repoRoot, pkg.scriptsDir);
    const tmpDir     = path.join(scriptsDir, '.tmp');

    console.log(`\n🔷 [${name}] Step 1: Compiling TypeScript → ${pkg.scriptsDir}/.tmp/`);

    // Generate a component-specific tsconfig that overrides only the outDir,
    // extending the root tsconfig so compiler options stay in one place.
    const tempTsConfig = {
        extends: path.relative(scriptsDir, path.join(repoRoot, 'tsconfig.json')).replace(/\\/g, '/'),
        compilerOptions: {
            outDir: './.tmp'
        },
        include: ['./**/*.ts'],
        exclude: ['./modules', './.tmp', './bundles', '../node_modules']
    };

    const tempTsConfigPath = path.join(scriptsDir, `tsconfig.${name.toLowerCase()}.tmp.json`);
    fs.writeFileSync(tempTsConfigPath, JSON.stringify(tempTsConfig, null, 2));

    try {
        const tscBin  = path.join(repoRoot, 'node_modules', '.bin', 'tsc');
        const tscCmd  = `"${tscBin}" --project "${tempTsConfigPath}"`;
        const result  = shelljs.exec(tscCmd, { silent: false });

        // code 2 = fatal syntax/config error → abort
        if (result.code === 2) {
            throw new Error(
                `[${name}] TypeScript compilation failed with fatal errors (exit ${result.code}).`
            );
        }
        // code 1 = type errors only, JS still emitted (noEmitOnError:false)
        if (result.code === 1) {
            console.warn(`⚠️  [${name}] TypeScript type errors reported (exit 1), JS still emitted.`);
        } else {
            console.log(`✅ [${name}] TypeScript compilation succeeded.`);
        }

        // Verify entry-point was produced
        const entryJs = path.join(tmpDir, `${pkg.tsEntry}.js`);
        if (!fs.existsSync(entryJs)) {
            throw new Error(
                `[${name}] Expected entry file not found: ${entryJs}\n` +
                `Check "tsEntry" and "scriptsDir" in config.json.`
            );
        }

        // Post-compilation: update imports based on blazorDependencies
        if (config.blazorDependencies) {
            await updateCompiledImports({ name, pkg, tmpDir, repoRoot });
        }
    } finally {
        // Always remove the temporary tsconfig
        if (fs.existsSync(tempTsConfigPath)) {
            fs.unlinkSync(tempTsConfigPath);
        }
    }
}

/**
 * Update imports in compiled JS files to replace local paths with external npm packages.
 * Based on config.blazorDependencies mapping.
 *
 * @param {{ name: string, pkg: object, tmpDir: string, repoRoot: string }} param0
 * @returns {Promise<void>}
 */
async function updateCompiledImports({ name, pkg, tmpDir, repoRoot }) {
    console.log(`\n📝 [${name}] Step 1b: Updating imports for external dependencies...`);

    // Build a map of relative paths to external npm packages
    const importMap = {};
    
    for (const [depName, depPaths] of Object.entries(config.blazorDependencies || {})) {
        if (Array.isArray(depPaths)) {
            // Find which BlazorScripts package contains this dependency group
            let packageName = null;
            for (const [scriptPkg, scriptData] of Object.entries(config.BlazorScripts || {})) {
                // For now, assume the first BlazorScripts package name
                // e.g. "Popups" → "@syncfusion/ej2-popups"
                packageName = scriptPkg;
                break;
            }
            
            if (!packageName) {
                console.warn(`  ⚠️  No BlazorScripts package found for dependency "${depName}"`);
                continue;
            }

            // Convert package name to npm format: "Popups" → "@syncfusion/ej2-popups"
            const npmPackageName = `@syncfusion/ej2-${packageName.toLowerCase()}`;

            for (const depPath of depPaths) {
                // Map both relative and non-relative paths
                importMap[depPath] = npmPackageName;
                importMap[`../${depPath}`] = npmPackageName;
                importMap[`./${depPath}`] = npmPackageName;
            }
        }
    }

    // Find all .js files in tmpDir recursively
    const findJsFiles = (dir) => {
        let files = [];
        const items = fs.readdirSync(dir, { withFileTypes: true });
        for (const item of items) {
            const fullPath = path.join(dir, item.name);
            if (item.isDirectory()) {
                files = files.concat(findJsFiles(fullPath));
            } else if (item.name.endsWith('.js')) {
                files.push(fullPath);
            }
        }
        return files;
    };

    const jsFiles = findJsFiles(tmpDir);

    for (const filePath of jsFiles) {
        let content = fs.readFileSync(filePath, 'utf8');
        let modified = false;

        // Replace import statements
        for (const [from, to] of Object.entries(importMap)) {
            // Match: import { ... } from '../common/position';
            const importRegex = new RegExp(`from\\s+['"]${from.replace(/\//g, '\\/')}['"]`, 'g');
            if (importRegex.test(content)) {
                content = content.replace(importRegex, `from '${to}'`);
                modified = true;
            }
        }

        if (modified) {
            fs.writeFileSync(filePath, content, 'utf8');
        }
    }

    console.log(`✅ [${name}] Imports updated for external dependencies.`);
}

// ─── Core: bundle one component (rollup) ──────────────────────────────────────

/**
 * Run Rollup for a single component package.
 *
 * Reads  : <scriptsDir>/.tmp/<tsEntry>.js
 * Writes : <outputDir>/<outputFile ?? tsEntry.js>
 *
 * Supported config.json fields per package:
 *   tsEntry      - entry filename without extension (required)
 *   scriptsDir   - folder with .ts source files (required)
 *   outputDir    - destination folder for the bundle (required)
 *   outputFile   - override output filename, default "<tsEntry>.js"
 *   outputFormat - rollup format: "iife" | "umd" | "esm" | "cjs"
 *                  default "iife"  ← matches the original sf-<componentname>.js style
 *   outputName   - global variable name for iife/umd output
 *                  supports dotted paths e.g. "sfBlazor.<componentname>"
 *                  default: camelCase of tsEntry e.g. "sf<componentname>"
 *   banner       - string prepended to the output file
 *                  e.g. "window.sfBlazor = window.sfBlazor || {};"
 *   externals    - map of npm package → runtime global
 *                  e.g. { "@syncfusion/ej2-base": "sf.base" }
 *
 * @param {{ name: string, pkg: object }} param0
 * @returns {Promise<void>}
 */
async function bundleComponent({ name, pkg }) {
    const rollup          = require('rollup');
    const rollupResolve   = require('@rollup/plugin-node-resolve').default;
    const rollupCommonJs  = require('@rollup/plugin-commonjs');

    const repoRoot    = path.resolve(__dirname, '..');
    const scriptsDir  = path.resolve(repoRoot, pkg.scriptsDir);
    const tmpDir      = path.join(scriptsDir, '.tmp');
    const outputDir   = path.resolve(repoRoot, pkg.outputDir);

    // Resolve output filename: use explicit "outputFile" field or fall back to "<tsEntry>.js"
    const outputFileName = pkg.outputFile || `${pkg.tsEntry}.js`;
    const outputFile     = path.join(outputDir, outputFileName);

    // Resolve output format: default to "iife" to match original sf-<componentname>.js wrapper style
    const outputFormat = pkg.outputFormat || 'iife';

    // Resolve global name for iife/umd:
    //   - Use explicit "outputName" from config (supports dotted paths like "sfBlazor.<componentname>")
    //   - Fall back to camelCase of tsEntry: "sf-<componentname>" → "sf<componentname>"
    const outputName = pkg.outputName
        || pkg.tsEntry.replace(/-([a-z])/g, (_, c) => c.toUpperCase());

    // Banner prepended to the output (e.g. namespace guard)
    const banner = pkg.banner || '';

    // Footer appended to the output (e.g. namespace assignment)
    const footer = pkg.footer || '';

    // Externals: map of npm package id → runtime global variable
    const externals = pkg.externals || {};

    const entryFile = path.join(tmpDir, `${pkg.tsEntry}.js`);

    // ── Build a paramName → globalPath rename map ──────────────────────────
    // Rollup derives IIFE parameter names from the npm package's last path segment,
    // camelCased: "@syncfusion/ej2-base" → "ej2Base", "@syncfusion/ej2-popups" → "ej2Popups".
    // But the original sf-<componentname>.js accesses globals directly as "sf.base.X", "sf.popups.X".
    // We capture this mapping so we can rename usages in the final chunk.
    const paramToGlobal = {};   // e.g.  { ej2Base: 'sf.base', ej2Popups: 'sf.popups' }
    for (const [pkg, global] of Object.entries(externals)) {
        // "@syncfusion/ej2-base" → last segment "ej2-base" → camelCase "ej2Base"
        const lastSegment = pkg.split('/').pop();
        const paramName   = lastSegment.replace(/-([a-z])/g, (_, c) => c.toUpperCase());
        paramToGlobal[paramName] = global;
    }

    /**
     * Rollup plugin: produce output that exactly matches the original hand-crafted sf-<componentname>.js.
     *
     * Rollup IIFE wraps externals as function parameters:
     *   var sfBlazor.<componentname> = (function (ej2Base, ej2Popups) { ... }(sf.base, sf.popups));
     *
     * The original file accesses globals directly with NO wrapper arguments:
     *   window.sfBlazor.<componentname> = (function () { sf.base.isNullOrUndefined(...) }());
     *
     * This plugin fixes four things in order:
     *   1. param usages  : "ej2Base."    → "sf.base.",  "ej2Popups." → "sf.popups."  etc.
     *   2. this.sfBlazor : Rollup replaces "window." with "this." inside IIFE — revert it.
     *   3. IIFE signature: "(function (ej2Base, ej2Popups) {" → "(function () {"
     *   4. IIFE tail args: "}(sf.base, sf.popups));" → "}());"
     */
    const renameGlobalsPlugin = {
        name: 'rename-globals-to-direct-access',
        renderChunk(code) {
            let result = code;

            // ── 1. Rename param identifiers → direct global paths ─────────────
            // e.g.  ej2Base.isNullOrUndefined  →  sf.base.isNullOrUndefined
            for (const [param, globalPath] of Object.entries(paramToGlobal)) {
                result = result.replace(new RegExp(`\\b${param}\\.`, 'g'), `${globalPath}.`);
            }

            // ── 2. Revert Rollup's "this." → "window." substitution ───────────
            // Inside an IIFE Rollup replaces window.X with this.X.
            // The original code always uses window.sfBlazor explicitly.
            result = result.replace(/\bthis\.sfBlazor\b/g, 'window.sfBlazor');

            const paramList = Object.keys(paramToGlobal).join('|');
            if (paramList) {
                const anyArgsPattern = `\\([^)]*\\)`;

                // ── 3 & 4. Handle IIFE signature and invocation ────────────────
                // Behavior depends on pkg.withExportsParameter config:
                //   - If true:  "(function (ej2Base, ...) {" → "(function (exports) {"
                //               and "}(sf.base, ...))" → "}({}));"
                //   - If false: "(function (ej2Base, ...) {" → "(function () {"
                //               and "}(sf.base, ...))" → "}());"

                if (pkg.withExportsParameter) {
                    // Update signature to have exports parameter
                    const signatureRe = new RegExp(
                        `(\\(function\\s*)\\([^)]*\\b(?:${paramList})\\b[^)]*\\)(\\s*\\{)`,
                        'g'
                    );
                    result = result.replace(signatureRe, '$1(exports)$2');
                    result = result.replace(/\}\)\(\{\}\,(.*?)\)\;/, '});');
                } else {
                    // Strip params from function signature
                    // "(function (ej2Base, ej2Popups) {"  →  "(function () {"
                    const signatureRe = new RegExp(
                        `(\\(function\\s*)\\([^)]*\\b(?:${paramList})\\b[^)]*\\)(\\s*\\{)`,
                        'g'
                    );
                    result = result.replace(signatureRe, '$1()$2');

                    // Strip args from IIFE invocation tail
                    // Form A: }(...)); → }());
                    result = result.replace(
                        new RegExp(`\\}${anyArgsPattern}\\);`, 'g'),
                        '}());'
                    );
                    // Form B: })(...); → })();
                    result = result.replace(
                        new RegExp(`\\}\\)${anyArgsPattern};`, 'g'),
                        '})();'
                    );
                    // Form C: }({}, ...); → });  (Rollup sometimes generates }({}, args) format)
                    result = result.replace(/\}\(\{\}\,(.*?)\)\;/, '});');
                }
            }

            return result;
        }
    };

    console.log(`\n🔷 [${name}] Step 2: Bundling → ${pkg.outputDir}/${outputFileName}`);
    console.log(`         format   : ${outputFormat}`);
    console.log(`         name     : ${outputName}`);
    console.log(`         globals  : ${JSON.stringify(paramToGlobal)}`);
    if (banner) console.log(`         banner   : ${banner}`);
    if (footer) console.log(`         footer   : ${footer}`);

    shelljs.mkdir('-p', outputDir);

    let bundle;
    try {
        bundle = await rollup.rollup({
            input: entryFile,
            external: Object.keys(externals),
            plugins: [
                rollupResolve({ browser: true }),
                rollupCommonJs(),
                // renameGlobalsPlugin is applied at output stage (renderChunk), not here
            ],
            onwarn(warning, warn) {
                // Circular dependency warnings are common in large TS codebases – suppress
                if (warning.code === 'CIRCULAR_DEPENDENCY') return;
                warn(warning);
            },
        });

        await bundle.write({
            format: outputFormat,
            name: outputName,
            file: outputFile,
            globals: externals,
            // banner is written literally before the IIFE wrapper
            banner: banner,
            // footer is written literally after the IIFE wrapper
            footer: footer,
            // Output-stage plugin: renames ej2Base.X → sf.base.X and cleans up the IIFE signature
            plugins: [renameGlobalsPlugin],
        });
    } finally {
        if (bundle) await bundle.close();
    }

    console.log(`✅ [${name}] Bundle written → ${outputFile}`);
}

// ─── Core: cleanup .tmp for one component ─────────────────────────────────────

/**
 * Remove the intermediate .tmp directory for a component.
 *
 * @param {{ name: string, pkg: object }} param0
 */
function cleanupTmp({ name, pkg }) {
    const repoRoot   = path.resolve(__dirname, '..');
    const scriptsDir = path.resolve(repoRoot, pkg.scriptsDir);
    const tmpDir     = path.join(scriptsDir, '.tmp');
    if (fs.existsSync(tmpDir)) {
        shelljs.rm('-rf', tmpDir);
        console.log(`🧹 [${name}] Removed ${pkg.scriptsDir}/.tmp`);
    }
}

// ─── Gulp task: compile ────────────────────────────────────────────────────────

/**
 * gulp compile [--component <Name>]
 *
 * Compiles TypeScript source files to .js for each resolved component.
 * Does NOT bundle — useful for quick type-checking / IDE integration.
 */
gulp.task('compile', async () => {
    const targets = resolvePackages();
    console.log(`\n📋 Components to compile: ${targets.map(t => t.name).join(', ')}`);

    for (const target of targets) {
        await compileComponent(target);
    }

    console.log('\n✅ compile task completed successfully.');
});

// ─── Gulp task: bundle ────────────────────────────────────────────────────────

/**
 * gulp bundle [--component <Name>]
 *
 * Compiles TypeScript → intermediate JS, then bundles to a single UMD file,
 * then removes intermediate files. All driven by config.json.
 */
gulp.task('bundle', async () => {
    const targets = resolvePackages();
    console.log(`\n📋 Components to bundle: ${targets.map(t => t.name).join(', ')}`);

    for (const target of targets) {
        await compileComponent(target);
        await bundleComponent(target);
        cleanupTmp(target);
    }

    console.log('\n✅ bundle task completed successfully.');
});
