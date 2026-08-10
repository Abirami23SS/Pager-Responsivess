/* eslint-disable @typescript-eslint/no-explicit-any */
import { BlazorDotnetObject, closest, KeyboardEvents, EventHandler, rippleEffect, attributes, BaseEventArgs } from '@syncfusion/ej2-base';
import { KeyboardEventArgs, select, isVisible, Effect, selectAll } from '@syncfusion/ej2-base';
import { isNullOrUndefined as isNOU, isRippleEnabled, animationMode, addClass, removeClass } from '@syncfusion/ej2-base';
import { setStyleAttribute as setStyle, Animation, AnimationModel } from '@syncfusion/ej2-base';

type ExpandMode = 'Single' | 'Multiple';
type HTEle = HTMLElement;

/**
 * Interface for a class AccordionAnimationSettings
 */
interface AccordionAnimationSettingsModel {

    /**
     * Specifies the animation to appear while collapsing the Accordion item.
     *
     * @default { effect: 'SlideDown', duration: 400, easing: 'linear' }
     */
    collapse?: AccordionActionSettingsModel;

    /**
     * Specifies the animation to appear while expanding the Accordion item.
     *
     * @default { effect: 'SlideDown', duration: 400, easing: 'linear' }
     */
    expand?: AccordionActionSettingsModel;

}

/**
 * Interface for a class AccordionActionSettings
 */
interface AccordionActionSettingsModel {

    /**
     * Specifies the type of animation.
     *
     * @default 'SlideDown'
     * @aspType string
     */
    effect?: 'None' | Effect;

    /**
     * Specifies the duration to animate.
     *
     * @default 400
     */
    duration?: number;

    /**
     * Specifies the animation timing function.
     *
     * @default 'linear'
     */
    easing?: string;

}
/** An interface that holds options to control the expanding item action. */
interface ExpandEventArgs extends BaseEventArgs {
    /** Defines the current Accordion Item Object. */
    item?: AccordionItemModel
    /** Defines the current Accordion Item Element. */
    element?: HTMLElement
    /** Defines the expand/collapse state. */
    isExpanded?: boolean
    /** Defines the prevent action. */
    cancel?: boolean
    /** Defines the Accordion Item Index */
    index?: number
    /** Defines the Accordion Item Content */
    content?: HTMLElement
}
/**
 * Interface for a class AccordionItem
 */
interface AccordionItemModel {

    /**
     * Sets the text content to be displayed for the Accordion item.
     * You can set the content of the Accordion item using `content` property.
     * It also supports to include the title as `HTML element`, `string`, or `query selector`.
     * ```typescript
     *   let accordionObj: Accordion = new Accordion( {
     *        items: [
     *          { header: 'Accordion Header', content: 'Accordion Content' },
     *          { header: '<div>Accordion Header</div>', content: '<div>Accordion Content</div>' },
     *          { header: '#headerContent', content: '#panelContent' }]
     *        });
     *   accordionObj.appendTo('#accordion');
     * ```
     *
     * @default null
     */
    content?: string;

    /**
     * Sets the header text to be displayed for the Accordion item.
     * You can set the title of the Accordion item using `header` property.
     * It also supports to include the title as `HTML element`, `string`, or `query selector`.
     * ```typescript
     *   let accordionObj: Accordion = new Accordion( {
     *        items: [
     *          { header: 'Accordion Header', content: 'Accordion Content' },
     *          { header: '<div>Accordion Header</div>', content: '<div>Accordion Content</div>' },
     *          { header: '#headerContent', content: '#panelContent' }]
     *        });
     *   accordionObj.appendTo('#accordion');
     * ```
     *
     * @default null
     */
    header?: string;

    /**
     * Defines single/multiple classes (separated by a space) are to be used for Accordion item customization.
     *
     * @default null
     */
    cssClass?: string;

    /**
     * Defines an icon with the given custom CSS class that is to be rendered before the header text.
     * Add the css classes to the `iconCss` property and write the css styles to the defined class to set images/icons.
     * Adding icon is applicable only to the header.
     * ```typescript
     *   let accordionObj: Accordion = new Accordion( {
     *        items: [
     *          { header: 'Accordion Header', iconCss: 'e-app-icon' }]
     *        });
     *   accordionObj.appendTo('#accordion');
     * ```
     * ```css
     * .e-app-icon::before {
     *   content: "\e710";
     * }
     * ```
     *
     * @default null
     */
    iconCss?: string;

    /**
     * Sets the expand (true) or collapse (false) state of the Accordion item. By default, all the items are in a collapsed state.
     *
     * @default false
     */
    expanded?: boolean;

    /**
     * Sets false to hide an accordion item.
     *
     * @default true
     */
    visible?: boolean;

    /**
     * Sets true to disable an accordion item.
     *
     * @default false
     */
    disabled?: boolean;

    /**
     * Sets unique ID to accordion item.
     *
     * @default null
     */
    id?: string;

}

const CLS_ACRDN_ROOT: string = 'e-acrdn-root';
const CLS_ROOT: string = 'e-accordion';
const CLS_ITEM: string = 'e-acrdn-item';
const CLS_ITEMFOCUS: string = 'e-item-focus';
const CLS_HEADER: string = 'e-acrdn-header';
const CLS_CONTENT: string = 'e-acrdn-panel';
const CLS_TOOGLEICN: string = 'e-toggle-icon';
const CLS_EXPANDICN: string = 'e-expand-icon';
const CLS_CTNHIDE: string = 'e-content-hide';
const CLS_SLCT: string = 'e-select';
const CLS_SLCTED: string = 'e-selected';
const CLS_ACTIVE: string = 'e-active';
const CLS_ANIMATE: string = 'e-animate';
const CLS_DISABLE: string = 'e-overlay';
const CLS_TOGANIMATE: string = 'e-toggle-animation';
const CLS_NEST: string = 'e-nested';
const CLS_EXPANDSTATE: string = 'e-expand-state';
const CLS_SCOPE: string = 'scope';
const CLS_RTL: string = 'e-rtl';

class SfAccordion {
    private lastActiveItemId: string;
    private keyModule: KeyboardEvents;
    private isNested: boolean;
    private isDestroy: boolean;
    private accItem: HTEle[];
    private removeRippleEffect: Function;
    private sfBlazor: any = (window as any).sfBlazor;
    private keyConfigs: { [key: string]: string } = {
        moveUp: 'uparrow',
        moveDown: 'downarrow',
        enter: 'enter',
        space: 'space',
        home: 'home',
        end: 'end'
    };
    public element: HTMLElement;
    public dotNetRef: BlazorDotnetObject;
    public options: IAccordionOptions;
    public dataId: string;
    constructor(dataId: string, element: HTMLElement, options: IAccordionOptions, dotnetRef: BlazorDotnetObject) {
        this.element = element;
        this.dotNetRef = dotnetRef;
        this.options = options;
        this.dataId = dataId;
        this.sfBlazor.setCompInstance(this);
    }
    public destroy(): void {
        const ele: HTEle = this.element;
        this.unwireEvents();
        this.isDestroy = true;
        ele.classList.remove(CLS_ACRDN_ROOT);
        if (!this.isNested && isRippleEnabled) {
            this.removeRippleEffect();
        }
    }
    public render(): void {
        const nested: Element = closest(this.element, '.' + CLS_CONTENT);
        this.isNested = false;
        if (!this.isDestroy) {
            this.isDestroy = false;
        }
        if (nested && nested.firstElementChild && nested.firstElementChild.firstElementChild) {
            if (nested.firstElementChild.firstElementChild.classList.contains(CLS_ROOT)) {
                nested.classList.add(CLS_NEST);
                this.isNested = true;
            }
        } else {
            this.element.classList.add(CLS_ACRDN_ROOT);
        }
        this.wireFocusEvents();
        this.wireEvents();
    }
    public wireFocusEvents(): void {
        const acrdItem: HTEle[] = [].slice.call(this.element.querySelectorAll('.' + CLS_ITEM));
        for (const item of acrdItem) {
            const headerEle: Element = item.querySelector('.' + CLS_HEADER);
            if (item.childElementCount > 0 && headerEle) {
                EventHandler.clearEvents(headerEle);
                EventHandler.add(headerEle, 'focus', this.focusIn, this);
                EventHandler.add(headerEle, 'blur', this.focusOut, this);
            }
        }
    }
    private unwireEvents(): void {
        if (!isNOU(this.keyModule)) {
            this.keyModule.destroy();
        }
    }
    private wireEvents(): void {
        if (!this.isNested && !this.isDestroy) {
            this.removeRippleEffect = rippleEffect(this.element, { selector: '.' + CLS_HEADER });
        }
        if (!this.isNested) {
            this.keyModule = new KeyboardEvents(
                this.element,
                {
                    keyAction: this.keyActionHandler.bind(this),
                    keyConfigs: this.keyConfigs,
                    eventName: 'keydown'
                });
        }
    }
    private focusIn(e: FocusEvent): void {
        (<HTEle>e.target).parentElement.classList.add(CLS_ITEMFOCUS);
    }
    private focusOut(e: FocusEvent): void {
        (<HTEle>e.target).parentElement.classList.remove(CLS_ITEMFOCUS);
    }
    /**
     * To perform expand and collapse action while clicking the item
     *
     * @param {HTEle} targetEle Accepts the DOM element
     * @returns {void}
     */
    public afterContentRender(targetEle: HTEle): void {
        const acrdActive: HTEle[] = [];
        const acrdnItem: HTEle = targetEle;
        const acrdnHdr: HTEle = <HTEle>acrdnItem.children[0];
        const acrdnCtn: HTEle = <HTEle>acrdnItem.children[1];
        let acrdnCtnItem: HTEle;
        if (acrdnHdr) {
            acrdnCtnItem = <HTEle>closest(acrdnHdr, '.' + CLS_ITEM);
        } else if (acrdnCtn) {
            acrdnCtnItem = <HTEle>closest(acrdnCtn, '.' + CLS_ITEM);
        }
        const acrdnchild: HTMLCollection = this.element.children;
        [].slice.call(acrdnchild).forEach((el: HTEle) => {
            if (el.classList.contains(CLS_ACTIVE)) { acrdActive.push(el); }
        });
        const acrdAniEle: HTEle[] = [].slice.call(this.element.querySelectorAll('.' + CLS_ITEM + ' [' + CLS_ANIMATE + ']'));
        if (acrdAniEle.length > 0) {
            for (const el of acrdAniEle) {
                acrdActive.push(el.parentElement);
            }
        }
        const sameContentCheck: boolean = acrdActive.indexOf(acrdnCtnItem) !== -1 && acrdnCtn.getAttribute('e-animate') === 'true';
        let sameHeader: boolean = false;
        if (!isNOU(acrdnItem) && !isNOU(acrdnHdr)) {
            const acrdnCtn: HTEle = <HTEle>select('.' + CLS_CONTENT, acrdnItem);
            const acrdnRoot: HTEle = <HTEle>closest(acrdnItem, '.' + CLS_ACRDN_ROOT);
            const expandState: HTEle = <HTEle>acrdnRoot.querySelector('.' + CLS_EXPANDSTATE);
            if (isNOU(acrdnCtn)) {
                return;
            }
            sameHeader = (expandState === acrdnItem);
            if (isVisible(acrdnCtn) && (!sameContentCheck || acrdnCtnItem.classList.contains(CLS_SLCTED))) {
                this.collapse(acrdnCtn);
            } else {
                if ((acrdActive.length > 0) && this.options.expandMode === 'Single' && !sameContentCheck) {
                    acrdActive.forEach((el: HTEle) => {
                        this.collapse(<HTEle>select('.' + CLS_CONTENT, el));
                        el.classList.remove(CLS_EXPANDSTATE);
                    });
                }
                this.expand(acrdnCtn);
            }
            if (!isNOU(expandState) && !sameHeader) {
                expandState.classList.remove(CLS_EXPANDSTATE);
            }
        }
    }
    private eleMoveFocus(action: string, root: HTEle, trgt: HTEle): void {
        let clst: HTEle;
        let clstItem: HTEle = <HTEle>closest(trgt, '.' + CLS_ITEM);
        if (trgt === root) {
            clst = <HTEle>((action === 'moveUp' ? trgt.lastElementChild : trgt).querySelector('.' + CLS_HEADER));
        } else if (trgt.classList.contains(CLS_HEADER)) {
            clstItem = <HTEle>(action === 'moveUp' ? clstItem.previousElementSibling : clstItem.nextElementSibling);
            if (clstItem) {
                clst = <HTEle>select('.' + CLS_HEADER, clstItem);
            }
        }
        if (clst) {
            clst.focus();
        }
    }
    private keyActionHandler(e: KeyboardEventArgs): void {
        const trgt: HTEle = <HTEle>e.target;
        const header: HTEle = <HTEle>closest(e.target as HTEle, CLS_HEADER);
        if (isNOU(header) && !trgt.classList.contains(CLS_ROOT) && !trgt.classList.contains(CLS_HEADER)) {
            return;
        }
        let clst: HTEle;
        const root: HTEle = this.element;
        let content: HTEle;
        switch (e.action) {
        case 'moveUp':
        case 'moveDown':
            this.eleMoveFocus(e.action, root, trgt);
            break;
        case 'space':
        case 'enter':
            content = trgt.nextElementSibling as HTEle;
            if (!isNOU(content) && content.classList.contains(CLS_CONTENT)) {
                if (content.getAttribute('e-animate') !== 'true') {
                    trgt.click();
                }
            } else {
                trgt.click();
            }
            e.preventDefault();
            break;
        case 'home':
        case 'end':
            clst = e.action === 'home' ? <HTEle>root.firstElementChild.children[0] : <HTEle>root.lastElementChild.children[0];
            clst.focus();
            e.preventDefault();
            break;
        }
    }
    private expand(trgt: HTEle): void {
        const trgtItemEle: HTEle = <HTEle>closest(trgt, '.' + CLS_ITEM);
        if (isNOU(trgt) || (isVisible(trgt) && trgt.getAttribute('e-animate') !== 'true') || trgtItemEle.classList.contains(CLS_DISABLE)) {
            return;
        }
        this.dotNetRef.invokeMethodAsync('TriggerExpandingEvent', this.getIndexByItem(trgtItemEle));
    }
    private expandAnimation(ef: string, icn: HTEle, trgt: HTEle, trgtItemEle: HTEle, animate: AnimationModel, args: ExpandEventArgs): void {
        let height: number;
        this.lastActiveItemId = trgtItemEle.id;
        if (ef === 'None' && animationMode === 'Enable') {
            ef = 'SlideDown';
            animate.name = 'SlideDown';
        }
        if (ef === 'SlideDown') {
            animate.begin = () => {
                this.expandProgress('begin', icn, trgt, trgtItemEle, args);
                trgt.style.position = 'absolute';
                height = trgtItemEle.offsetHeight;
                trgt.style.maxHeight = (trgt.offsetHeight) + 'px';
                trgtItemEle.style.maxHeight = '';
            };
            animate.progress = () => {
                trgtItemEle.style.minHeight = (height + trgt.offsetHeight) + 'px';
            };
            animate.end = () => {
                setStyle(trgt, { 'position': '', 'maxHeight': '' });
                trgtItemEle.style.minHeight = '';
                this.expandProgress('end', icn, trgt, trgtItemEle, args);
            };
        } else {
            animate.begin = () => {
                this.expandProgress('begin', icn, trgt, trgtItemEle, args);
            };
            animate.end = () => {
                this.expandProgress('end', icn, trgt, trgtItemEle, args);
            };
        }
        new Animation(animate).animate(trgt);
    }
    private expandProgress(progress: string, icon: HTEle, trgt: HTEle, trgtItemEle: HTEle, eventArgs: ExpandEventArgs): void {
        removeClass([trgt], CLS_CTNHIDE);
        addClass([trgtItemEle], CLS_SLCTED);
        addClass([icon], CLS_EXPANDICN);
        if (progress === 'end') {
            addClass([trgtItemEle], CLS_ACTIVE);
            trgt.setAttribute('aria-hidden', 'false');
            attributes(trgtItemEle.firstElementChild, { 'aria-expanded': 'true' });
            icon.classList.remove(CLS_TOGANIMATE);
            this.dotNetRef.invokeMethodAsync('TriggerExpandedEvent', eventArgs);
            this.setPersistence('accordion' + this.element.id);
        }
    }
    private expandedItemsPush(item: HTEle): void {
        const index: number = this.getIndexByItem(item);
        if (this.options.expandedIndices.indexOf(index) === -1) {
            const temp: number[] = [].slice.call(this.options.expandedIndices);
            temp.push(index);
            this.options.expandedIndices = temp;
        }
    }
    private getIndexByItem(item: HTEle): number {
        const itemEle: HTEle[] = this.getItemElements();
        return [].slice.call(itemEle).indexOf(item);
    }
    private getItemElements(): HTEle[] {
        const itemEle: HTEle[] = [];
        const itemCollection: HTMLCollection = this.element.children;
        [].slice.call(itemCollection).forEach((el: HTEle) => {
            if (el.classList.contains(CLS_ITEM)) { itemEle.push(el); }
        });
        return itemEle;
    }
    private expandedItemsPop(item: HTEle): void {
        const index: number = this.getIndexByItem(item);
        const temp: number[] = [].slice.call(this.options.expandedIndices);
        const tempIndex: number = temp.indexOf(index);
        if (tempIndex > -1) {
            temp.splice(tempIndex, 1);
        }
        this.options.expandedIndices = temp;
    }
    private collapse(trgt: HTEle): void {
        const trgtItemEle: HTEle = <HTEle>closest(trgt, '.' + CLS_ITEM);
        if (isNOU(trgt) || !isVisible(trgt) || trgtItemEle.classList.contains(CLS_DISABLE)) { return; }
        this.dotNetRef.invokeMethodAsync('TriggerCollapsingEvent', this.getIndexByItem(trgtItemEle));
    }
    private collapseAnimation(ef: string, trgt: HTEle, trgtItEl: HTEle, icn: HTEle, animate: AnimationModel, args: ExpandEventArgs): void {
        let height: number;
        let trgtHeight: number;
        let itemHeight: number;
        let remain: number;
        this.lastActiveItemId = trgtItEl.id;
        if (ef === 'None' && animationMode === 'Enable') {
            ef = 'SlideUp';
            animate.name = 'SlideUp';
        }
        if (ef === 'SlideUp') {
            animate.begin = () => {
                itemHeight = trgtItEl.offsetHeight;
                trgtItEl.style.minHeight = itemHeight + 'px';
                trgt.style.position = 'absolute';
                height = trgtItEl.offsetHeight;
                trgtHeight = trgt.offsetHeight;
                trgt.style.maxHeight = trgtHeight + 'px';
                this.collapseProgress('begin', icn, trgt, trgtItEl, args);
            };
            animate.progress = () => {
                remain = ((height - (trgtHeight - trgt.offsetHeight)));
                if (remain < itemHeight) {
                    trgtItEl.style.minHeight = remain + 'px';
                }
            };
            animate.end = () => {
                trgt.style.display = 'none';
                this.collapseProgress('end', icn, trgt, trgtItEl, args);
                trgtItEl.style.minHeight = '';
                setStyle(trgt, { 'position': '', 'maxHeight': '', 'display': '' });
            };
        } else {
            animate.begin = () => {
                this.collapseProgress('begin', icn, trgt, trgtItEl, args);
            };
            animate.end = () => {
                this.collapseProgress('end', icn, trgt, trgtItEl, args);
            };
        }
        new Animation(animate).animate(trgt);
    }
    private collapseProgress(progress: string, icon: HTEle, trgt: HTEle, trgtItemEle: HTEle, eventArgs: ExpandEventArgs): void {
        removeClass([icon], CLS_EXPANDICN);
        removeClass([trgtItemEle], CLS_SLCTED);
        if (progress === 'end') {
            addClass([trgt], CLS_CTNHIDE);
            icon.classList.remove(CLS_TOGANIMATE);
            removeClass([trgtItemEle], CLS_ACTIVE);
            trgt.setAttribute('aria-hidden', 'true');
            attributes(trgtItemEle.firstElementChild, { 'aria-expanded': 'false' });
            this.dotNetRef.invokeMethodAsync('TriggerCollapsedEvent', eventArgs);
            this.setPersistence('accordion' + this.element.id);
        }
    }
    public expandingItem(expandArgs: ExpandEventArgs): void {
        this.accItem = selectAll(':' + CLS_SCOPE + ' > .' + CLS_ITEM, this.element);
        const trgtItemEle: HTEle = this.getElementByIndex(expandArgs.index);
        const trgt: HTEle = <HTEle>select('.' + CLS_CONTENT, trgtItemEle);
        const acrdnRoot: HTEle = <HTEle>closest(trgtItemEle, '.' + CLS_ACRDN_ROOT);
        const icon: HTEle = <HTEle>select('.' + CLS_TOOGLEICN, trgtItemEle).firstElementChild;
        const expandState: HTEle = <HTEle>acrdnRoot.querySelector('.' + CLS_EXPANDSTATE);
        const animation: AnimationModel = {
            name: <Effect>this.options.animation.expand.effect,
            duration: this.options.animation.expand.duration,
            timingFunction: this.options.animation.expand.easing
        };
        icon.classList.add(CLS_TOGANIMATE);
        this.expandedItemsPush(trgtItemEle);
        if (!isNOU(expandState)) {
            expandState.classList.remove(CLS_EXPANDSTATE);
        }
        trgtItemEle.classList.add(CLS_EXPANDSTATE);
        if ((animation.name === <Effect>'None' && animationMode !== 'Enable') || animationMode === 'Disable' ) {
            this.expandProgress('begin', icon, trgt, trgtItemEle, expandArgs);
            this.expandProgress('end', icon, trgt, trgtItemEle, expandArgs);
        } else {
            this.expandAnimation(animation.name, icon, trgt, trgtItemEle, animation, expandArgs);
        }
    }
    private getElementByIndex(index: number): HTEle {
        if (this.accItem[parseInt(index.toString(), 10)]) {
            return this.accItem[parseInt(index.toString(), 10)];
        }
        return null;
    }
    public collapsingItem(expandArgs: ExpandEventArgs): void {
        this.accItem = selectAll(':' + CLS_SCOPE + ' > .' + CLS_ITEM, this.element);
        const trgtItemEle: HTEle = this.getElementByIndex(expandArgs.index);
        const trgt: HTEle = <HTEle>select('.' + CLS_CONTENT, trgtItemEle);
        const icon: HTEle = <HTEle>select('.' + CLS_TOOGLEICN, trgtItemEle).firstElementChild;
        const animation: AnimationModel = {
            name: <Effect>this.options.animation.collapse.effect,
            duration: this.options.animation.collapse.duration,
            timingFunction: this.options.animation.collapse.easing
        };
        this.expandedItemsPop(trgtItemEle);
        trgtItemEle.classList.remove(CLS_EXPANDSTATE);
        icon.classList.add(CLS_TOGANIMATE);
        if ((animation.name === <Effect>'None' && animationMode !== 'Enable') || animationMode === 'Disable') {
            this.collapseProgress('begin', icon, trgt, trgtItemEle, expandArgs);
            this.collapseProgress('end', icon, trgt, trgtItemEle, expandArgs);
        } else {
            this.collapseAnimation(animation.name, trgt, trgtItemEle, icon, animation, expandArgs);
        }
    }
    public select(index: number): void {
        const itemEle: HTEle[] = this.getItemElements();
        const ele: HTEle = <HTEle>itemEle[parseInt(index.toString(), 10)];
        if (isNOU(ele) || isNOU(select('.' + CLS_HEADER, ele))) {
            return;
        }
        (<HTEle>ele.children[0]).focus();
    }
    public expandItem(isExpand: boolean, index?: number): void {
        const itemEle: HTEle[] = this.getItemElements();
        if (isNOU(index)) {
            if (this.options.expandMode === 'Single' && isExpand) {
                const ele: HTEle = <HTEle>itemEle[itemEle.length - 1];
                this.itemExpand(isExpand, ele, this.getIndexByItem(ele));
            } else {
                const item: HTMLElement = <HTMLElement>select('#' + this.lastActiveItemId, this.element);
                [].slice.call(itemEle).forEach((el: HTEle) => {
                    this.itemExpand(isExpand, el, this.getIndexByItem(el));
                    el.classList.remove(CLS_EXPANDSTATE);
                });
                const expandedItem: Element = select('.' + CLS_EXPANDSTATE, this.element);
                if (expandedItem) { expandedItem.classList.remove(CLS_EXPANDSTATE); }
                if (item) { item.classList.add(CLS_EXPANDSTATE); }
            }
        } else {
            const ele: HTEle = <HTEle>itemEle[parseInt(index.toString(), 10)];
            if (isNOU(ele) || !ele.classList.contains(CLS_SLCT) || (ele.classList.contains(CLS_ACTIVE) && isExpand)) {
                return;
            } else {
                if (this.options.expandMode === 'Single') {
                    this.expandItem(false);
                }
                this.itemExpand(isExpand, ele, index);
            }
        }
    }
    public setPersistence(elementId: string): void {
        if (this.options.enablePersistence) {
            window.localStorage.setItem(elementId, this.options.expandedIndices.toString());
        }
    }
    private itemExpand(isExpand: boolean, ele: HTEle, index: number): void {
        let ctn: HTEle = <HTEle>ele.children[1];
        if (ele.classList.contains(CLS_DISABLE)) {
            return;
        }
        if (isNOU(ctn) && isExpand) {
            (this as any).dotNetRef.invokeMethodAsync('OnAccordionClick', index).then(() => {
                ctn = <HTEle>ele.children[1];
                if (!isNOU(ctn)) {
                    this.expand(ctn);
                }
            });
        } else if (!isNOU(ctn)) {
            if (isExpand) {
                this.expand(ctn);
            } else {
                this.collapse(ctn);
            }
        }
    }
}

interface IAccordionOptions {
    animation: AccordionAnimationSettingsModel;
    expandMode: ExpandMode;
    expandedIndices: number[];
    enablePersistence: boolean;
    createdEnabled: boolean;
}

// tslint:disable
const Accordion: object = {
    initialize(dataId: string, element: HTMLElement, options: IAccordionOptions, dotnetRef: BlazorDotnetObject): void {
        if (element && dataId) {
            if (options.expandedIndices === null) {
                options.expandedIndices = [];
            }
            const instance: SfAccordion = new SfAccordion(dataId, element, options, dotnetRef);
            instance.render();
            if (document.body.contains(element) && options.createdEnabled) {
                instance.dotNetRef.invokeMethodAsync('CreatedEvent', null);
            }
        }
    },
    expandingItem(dataId: string, args: ExpandEventArgs): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (instance.element) {
            instance.expandingItem(args);
        }
    },
    collapsingItem(dataId: string, args: ExpandEventArgs): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (instance.element) {
            instance.collapsingItem(args);
        }
    },
    select(dataId: string, index: number): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (instance.element) {
            instance.select(index);
        }
    },
    destroy(dataId: string): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (instance.element) {
            instance.setPersistence('accordion' + instance.element.id);
            instance.destroy();
        }
    },
    // eslint-disable-next-line max-len
    setExpandModeAndRTL(dataId: string, enableRtl: boolean, expandMode: ExpandMode, isRtlChanged: boolean, isExpandModeChanged: boolean): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (instance.element) {
            if (isRtlChanged) {
                if (enableRtl) {
                    addClass([instance.element], CLS_RTL);
                } else {
                    removeClass([instance.element], CLS_RTL);
                }
            }
            if (isExpandModeChanged) {
                instance.options.expandMode = expandMode;
                if (expandMode === 'Single') {
                    if (instance.options.expandedIndices.length > 1) {
                        instance.expandItem(false);
                    }
                }
            }
        }
    },
    itemChanged(dataId: string): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (instance.element) {
            instance.wireFocusEvents();
        }
    },
    afterContentRender(dataId: string, targetEle: HTEle, animation: AccordionAnimationSettingsModel): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (instance.element) {
            instance.options.animation = animation;
            instance.afterContentRender(targetEle);
        }
    },
    itemExpandedOrCollapsed(dataId: string, args: ExpandEventArgs) {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (instance.element) {
            const content: HTMLElement = instance.accItem[args.index].querySelector('.' + CLS_CONTENT);
            if (content && content.firstElementChild && content.firstElementChild.firstElementChild) {
                if (content.firstElementChild.firstElementChild.classList.contains(CLS_ROOT)) {
                    content.classList.add(CLS_NEST);
                }
            }
        }
    }
};

export default Accordion;
