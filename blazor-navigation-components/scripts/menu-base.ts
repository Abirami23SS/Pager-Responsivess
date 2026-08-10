import { select, closest } from '@syncfusion/ej2-base';

const MENUITEM: string = 'e-menu-item';
const FOCUSED: string = 'e-focused';
const SELECTED: string = 'e-selected';
const CONTAINER: string = 'e-menu-container';
const MENU: string = 'e-contextmenu';
const SUBMENU: string = 'e-ul';
const SEPARATOR: string = 'e-separator';
const DISABLED: string = 'e-disabled';
const HIDE: string = 'e-menu-hide';
const MENUPARENT: string = 'e-menu-parent';
const RTL: string = 'e-rtl';
const HAMBURGER: string = '.e-hamburger';
const SCROLLMENU: string = '.e-menu-vscroll';
const NONE: string = 'none';
const DOT: string = '.';
const ESC: number = 27;
const ENTER: number = 13;
const UP: number = 38;
const DOWN: number = 40;
const LEFT: number = 37;
const RIGHT: number = 39;

export type MenuEffect = 'None' | 'SlideDown' | 'ZoomIn' | 'FadeIn';
/**
 * Interface for a class MenuAnimationSettings
 */
export interface MenuAnimationSettingsModel {

    /**
     * Specifies the effect that shown in the sub menu transform.
     * The possible effects are:
     * * None: Specifies the sub menu transform with no animation effect.
     * * SlideDown: Specifies the sub menu transform with slide down effect.
     * * ZoomIn: Specifies the sub menu transform with zoom in effect.
     * * FadeIn: Specifies the sub menu transform with fade in effect.
     *
     * @default 'SlideDown'
     * @aspType Syncfusion.EJ2.Navigations.MenuEffect
     * @blazorType Syncfusion.EJ2.Navigations.MenuEffect
     * @isEnumeration true
     */
    effect?: MenuEffect;

    /**
     * Specifies the time duration to transform object.
     *
     * @default 400
     */
    duration?: number;

    /**
     * Specifies the easing effect applied while transform.
     *
     * @default 'ease'
     */
    easing?: string;

}

// eslint-disable-next-line jsdoc/require-param, valid-jsdoc
/**
 * Keyboard action handler common for menu and context menu.
 *
 * @hidden
 */
export function keyActionHandler(container: HTMLElement, target: Element, keyCode: number, menuId?: string): void {
    if (keyCode === 9 && menuId) { keyCode = RIGHT; }
    if (keyCode === DOWN || keyCode === UP) {
        let index: number; let ul: Element; let focusedLi: Element;
        if (target.classList.contains(MENUPARENT)) {
            ul = target;
            focusedLi = ul.querySelector(`${DOT}${MENUITEM}${DOT}${FOCUSED}`);
            if (focusedLi) {
                index = Array.prototype.indexOf.call(ul.children, focusedLi);
                index = keyCode === DOWN ? (index === ul.childElementCount - 1 ? 0 : index + 1) :
                    (index === 0 ? ul.childElementCount - 1 : index - 1);
            } else {
                index = 0;
            }
            index = isValidLI(ul, index, keyCode === DOWN);
        } else if (target.classList.contains(MENUITEM)) {
            ul = target.parentElement;
            focusedLi = ul.querySelector(`${DOT}${MENUITEM}${DOT}${FOCUSED}`);
            index = Array.prototype.indexOf.call(ul.children, focusedLi ? focusedLi : target);
            index = keyCode === DOWN ? (index === ul.childElementCount - 1 ? 0 : index + 1) : (index === 0 ?
                ul.childElementCount - 1 : index - 1);
            index = isValidLI(ul, index, keyCode === DOWN);
        }
        if (ul && index !== -1) {
            (ul.children[parseInt(index.toString(), 10)] as HTMLElement).focus();
        }
    } else if (((container.classList.contains(RTL) ? keyCode === RIGHT : keyCode === LEFT) || keyCode === ESC || (keyCode === RIGHT && !target.classList.contains('e-menu-caret-icon') && menuId) ||
        (keyCode === ENTER && closest(target, DOT + CONTAINER))) && (target.classList.contains(SUBMENU) ||
        (target.classList.contains(MENUITEM) && !(target.parentElement.classList.contains(MENU))))) {
        let menuContainer: Element;
        if (menuId) { menuContainer = select(menuId); }
        const ul: Element = target.classList.contains(SUBMENU) ? target : target.parentElement;
        let menu: Element = closest(ul, SCROLLMENU);
        let selectedLi: HTMLElement; const previousUl: Element = menu ? menu.previousElementSibling : ul.previousElementSibling;
        if (menuContainer && (!previousUl || keyCode === ENTER)) {
            selectedLi = select(`${DOT}${MENUITEM}${DOT}${SELECTED}`, menuContainer);
            menu = select(SCROLLMENU, container);
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            if (menu) { (menuContainer as any).blazor__instance.destroyScroll(NONE); }
        } else {
            const hamburgerMenu: Element = closest(ul, HAMBURGER);
            if (hamburgerMenu) {
                selectedLi = select(`${DOT}${MENUITEM}${DOT}${SELECTED}`, hamburgerMenu);
            } else {
                selectedLi = select(`${DOT}${MENUITEM}${DOT}${SELECTED}`, previousUl);
            }
        }
        if (selectedLi) { selectedLi.focus(); }
    }
}
//eslint-disable-next-line jsdoc/require-jsdoc
function isValidLI(ul: Element, index: number, isKeyDown: boolean, count: number = 0): number {
    let cli: Element = ul.children[parseInt(index.toString(), 10)];
    if (count === ul.childElementCount) { return -1; }
    if (cli.classList.contains(SEPARATOR) || cli.classList.contains(DISABLED) || cli.classList.contains(HIDE)) {
        index = isKeyDown ? (index === ul.childElementCount - 1 ? 0 : index + 1) : (index === 0 ? ul.childElementCount - 1 : index - 1);
        count++;
    }
    cli = ul.children[parseInt(index.toString(), 10)];
    if (cli.classList.contains(SEPARATOR) || cli.classList.contains(DISABLED) || cli.classList.contains(HIDE)) {
        index = isValidLI(ul, index, isKeyDown);
    }
    return index;
}
