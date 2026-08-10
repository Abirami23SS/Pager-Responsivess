/* eslint-disable @typescript-eslint/no-explicit-any */
import { BlazorDotnetObject, KeyboardEvents, isNullOrUndefined as isNOU, BaseEventArgs, EmitType } from '@syncfusion/ej2-base';
import { createElement, EventHandler, formatUnit, append, addClass, removeClass } from '@syncfusion/ej2-base';
import { selectAll, setStyleAttribute as setStyle, Browser, isVisible } from '@syncfusion/ej2-base';
import { closest, detach, classList, KeyboardEventArgs, attributes } from '@syncfusion/ej2-base';
import { Popup, calculatePosition } from '@syncfusion/ej2-popups';
import { HScroll } from './common/h-scroll';
import { VScroll } from './common/v-scroll';

type OverflowMode = 'Scrollable' | 'Popup' | 'MultiRow' | 'Extended';
type HTEle = HTMLElement;
type ItmAlign = 'lefts' | 'centers' | 'rights';
type ItemAlign = 'Left' | 'Center' | 'Right';
type OverflowOption = 'None' | 'Show' | 'Hide';
type ItemType = 'Button' | 'Separator' | 'Input';
type DisplayMode = 'Both' | 'Overflow' | 'Toolbar';
/**
 * Interface for a class Item
 */
interface ItemModel {
    id?: string;
    text?: string;
    width?: number | string;
    cssClass?: string;
    showAlwaysInPopup?: boolean;
    disabled?: boolean;
    prefixIcon?: string;
    suffixIcon?: string;
    visible?: boolean;
    overflow?: OverflowOption;
    template?: string | Object | Function;
    type?: ItemType;
    showTextOn?: DisplayMode;
    htmlAttributes?: { [key: string]: string };
    tooltipText?: string;
    align?: ItemAlign;
    click?: EmitType<ClickEventArgs>;
    tabIndex?: number;

}

/** An interface that holds options to control the toolbar clicked action. */
interface ClickEventArgs extends BaseEventArgs {
    /** Defines the current Toolbar Item Object. */
    item: ItemModel
    /**
     * Defines the current Event arguments.
     */
    originalEvent: Event
    /** Defines the prevent action. */
    cancel?: boolean
}
const CLS_TOOLBAR: string = 'e-toolbar';
const CLS_VERTICAL: string = 'e-vertical';
const CLS_ITEMS: string = 'e-toolbar-items';
const BZ_ITEMS: string = 'e-blazor-toolbar-items';
const CLS_ITEM: string = 'e-toolbar-item';
const CLS_RTL: string = 'e-rtl';
const CLS_SEPARATOR: string = 'e-separator';
const CLS_SPACER: string = 'e-spacer';
const CLS_POPUPICON: string = 'e-popup-up-icon';
const CLS_POPUPDOWN: string = 'e-popup-down-icon';
const CLS_POPUPOPEN: string = 'e-popup-open';
const CLS_TEMPLATE: string = 'e-template';
const CLS_DISABLE: string = 'e-overlay';
const CLS_POPUPTEXT: string = 'e-toolbar-text';
const CLS_TBARTEXT: string = 'e-popup-text';
const CLS_TBAROVERFLOW: string = 'e-overflow-show';
const CLS_POPOVERFLOW: string = 'e-overflow-hide';
const CLS_TBARNAV: string = 'e-hor-nav';
const CLS_TBARSCRLNAV: string = 'e-scroll-nav';
const CLS_TBARRIGHT: string = 'e-toolbar-right';
const CLS_TBARLEFT: string = 'e-toolbar-left';
const CLS_TBARCENTER: string = 'e-toolbar-center';
const CLS_TBARPOS: string = 'e-tbar-pos';
const CLS_HSCROLLCNT: string = 'e-hscroll-content';
const CLS_VSCROLLCNT: string = 'e-vscroll-content';
const CLS_POPUPNAV: string = 'e-hor-nav';
const CLS_POPUPCLASS: string = 'e-toolbar-pop';
const CLS_POPUP: string = 'e-toolbar-popup';
const CLS_TBARBTNTEXT: string = 'e-tbar-btn-text';
const CLS_TBARNAVACT: string = 'e-nav-active';
const CLS_TBARIGNORE: string = 'e-ignore';
const CLS_POPPRI: string = 'e-popup-alone';
const CLS_HIDDEN: string = 'e-hidden';
const CLS_MULTIROW: string = 'e-toolbar-multirow';
const CLS_MULTIROWPOS: string = 'e-multirow-pos';
const CLS_MULTIROW_SEPARATOR: string = 'e-multirow-separator';
const CLS_EXTENDABLE_SEPARATOR: string = 'e-extended-separator';
const CLS_EXTEANDABLE_TOOLBAR: string = 'e-extended-toolbar';
const CLS_EXTENDABLECLASS: string = 'e-toolbar-extended';
const CLS_EXTENDPOPUP: string = 'e-expended-nav';
const CLS_EXTENDEDPOPOPEN: string = 'e-tbar-extended';
const TAB: number = 9;
const DOWNARROW: number = 40;
const UPARROW: number = 38;
const END: number = 35;
const HOME: number = 36;
const NonFocusableSelectorClass: string = '.' + CLS_ITEM + ':not(.' + CLS_DISABLE + ' ):not(.' + CLS_SEPARATOR + ' ):not(.' + CLS_SPACER + ' ):not(.' + CLS_HIDDEN + ' )';

interface ToolbarItemAlignIn {
    lefts: HTMLElement[];
    centers: HTMLElement[];
    rights: HTMLElement[];
}

class SfToolbar {
    public popObj: Popup;
    private trgtEle: HTEle;
    private tbarEle: HTMLElement[];
    private tbarAlgEle: ToolbarItemAlignIn;
    public tbarAlign: boolean;
    private tbarEleMrgn: number;
    private tbResize: boolean;
    private offsetWid: number;
    private keyModule: KeyboardEvents;
    public scrollModule: HScroll | VScroll;
    private activeEle: HTEle;
    private popupPriCount: number;
    private isExtendedOpen: boolean;
    private resizeContext: EventListenerObject = this.resize.bind(this);
    private orientationChangeContext: EventListenerObject = this.orientationChange.bind(this);
    private sfBlazor: any = (window as any).sfBlazor;
    private keyConfigs: { [key: string]: string } = {
        moveLeft: 'leftarrow',
        moveRight: 'rightarrow',
        moveUp: 'uparrow',
        moveDown: 'downarrow',
        popupOpen: 'enter',
        popupClose: 'escape',
        tab: 'tab',
        home: 'home',
        end: 'end'
    };
    private scrollPosition: number;
    public element: HTMLElement;
    public dotNetRef: BlazorDotnetObject;
    public options: IToolbarOptions;
    public dataId: string;
    constructor(dataId: string, element: HTMLElement, options: IToolbarOptions, dotnetRef: BlazorDotnetObject) {
        this.element = element;
        this.dotNetRef = dotnetRef;
        this.options = options;
        this.dataId = dataId;
        this.sfBlazor.setCompInstance(this);
    }
    public destroy(): void {
        this.unwireEvents();
        this.clearProperty();
        this.popObj = null;
        this.tbarAlign = null;
    }
    private wireEvents(): void {
        EventHandler.add(this.element, 'click', this.clickHandler, this);
        window.addEventListener('resize', this.resizeContext);
        window.addEventListener('orientationchange', this.orientationChangeContext);
        if (this.options.allowKeyboard) {
            this.wireKeyboardEvent();
        }
    }
    public wireKeyboardEvent(): void {
        this.keyModule = new KeyboardEvents(this.element, {
            keyAction: this.keyActionHandler.bind(this),
            keyConfigs: this.keyConfigs
        });
        EventHandler.add(this.element, 'keydown', this.docKeyDown, this);
        this.updateTabIndex('0');
    }

    private updateTabIndex(tabIndex: string): void {
        const ele: HTEle = <HTEle>this.element.querySelector(NonFocusableSelectorClass);
        if (!isNOU(ele) && !isNOU(ele.firstElementChild)) {
            const dataTabIndex: string = ele.firstElementChild.getAttribute('data-tabindex');
            if (dataTabIndex && dataTabIndex === '-1' && ele.firstElementChild.tagName !== 'INPUT') {
                ele.firstElementChild.setAttribute('tabindex', tabIndex);
            }
            else if (ele.classList.contains(CLS_TEMPLATE)) {
                const firstChild: HTMLElement = ele.firstElementChild as HTMLElement;
                if (!isNOU(firstChild)) {
                    firstChild.setAttribute('tabindex', isNOU(firstChild.getAttribute('tabIndex')) ? '-1' : this.getDataTabindex(firstChild));
                    firstChild.setAttribute('data-tabindex', isNOU(firstChild.getAttribute('tabIndex')) ? '-1' : this.getDataTabindex(firstChild));
                }
            }
        }
    }

    public unwireKeyboardEvent(): void {
        if (this.keyModule) {
            EventHandler.remove(this.element, 'keydown', this.docKeyDown);
            this.keyModule.destroy();
            this.keyModule = null;
        }
    }
    private docKeyDown(e: KeyboardEvent): void {
        if ((<HTEle>e.target).tagName === 'INPUT') { return; }
        const popCheck: boolean = !isNOU(this.popObj) && isVisible(this.popObj.element) && this.options.overflowMode !== 'Extended';
        if (e.keyCode === TAB && (<HTEle>e.target).classList.contains('e-hor-nav') === true && popCheck) {
            this.popObj.hide({ name: 'FadeOut', duration: 100 });
        }
        const keyCheck: boolean = (e.keyCode === DOWNARROW || e.keyCode === UPARROW || e.keyCode === END || e.keyCode === HOME);
        if (keyCheck) {
            e.preventDefault();
        }
    }
    private unwireEvents(): void {
        EventHandler.remove(this.element, 'click', this.clickHandler);
        this.destroyScroll();
        this.unwireKeyboardEvent();
        window.removeEventListener('resize', this.resizeContext);
        window.removeEventListener('orientationchange', this.orientationChangeContext);
        EventHandler.remove(document, 'scroll', this.docEvent);
        EventHandler.remove(document, 'click', this.docEvent);
    }
    private clearProperty(): void {
        this.tbarEle = [];
        this.tbarAlgEle = { lefts: [], centers: [], rights: [] };
    }
    private docEvent(e: Event): void {
        const popEle: Element = closest(<Element>e.target, '.e-popup');
        if (this.popObj && isVisible(this.popObj.element) && !popEle && this.options.overflowMode === 'Popup') {
            this.popObj.hide({ name: 'FadeOut', duration: 100 });
        }
    }
    private destroyScroll(): void {
        if (this.scrollModule) {
            if (this.tbarAlign) { addClass([this.scrollModule.element], CLS_TBARPOS); }
            this.scrollModule.destroy(); this.scrollModule = null;
        }
    }
    public destroyMode(): void {
        if (this.scrollModule) {
            this.scrollPosition = (this.scrollModule as Record<string, any>)
                .scrollEle[this.scrollModule.element.classList.contains('e-hscroll') ? 'scrollLeft' : 'scrollTop'];
            removeClass([this.scrollModule.element], CLS_RTL);
            this.destroyScroll();
        }
        removeClass([this.element], CLS_EXTENDEDPOPOPEN);
        removeClass([this.element], CLS_EXTEANDABLE_TOOLBAR);
        const tempEle: HTMLElement = this.element.querySelector('.e-toolbar-multirow');
        if (tempEle) { removeClass([tempEle], CLS_MULTIROW); }
        if (this.popObj) {
            this.popupRefresh(this.popObj.element, true);
        }
    }
    private elementFocus(ele: HTEle): void {
        const fChild: HTEle = <HTEle>ele.firstElementChild;
        if (fChild) {
            fChild.focus();
            this.activeEleSwitch(ele);
        } else {
            ele.focus();
        }
    }
    private clstElement(tbrNavChk: boolean, trgt: HTEle): HTEle {
        let clst: HTEle;
        if (tbrNavChk && this.popObj && isVisible(this.popObj.element)) {
            clst = <HTEle>this.popObj.element.querySelector('.' + CLS_ITEM);
        } else if (this.element === trgt || tbrNavChk) {
            clst = <HTEle>this.element.querySelector(NonFocusableSelectorClass);
        } else {
            clst = <HTEle>closest(trgt, '.' + CLS_ITEM);
        }
        return clst;
    }
    private keyHandling(clst: HTEle, e: KeyboardEventArgs, trgt: HTEle, navChk: boolean, scrollChk: boolean): void {
        const popObj: Popup = this.popObj;
        const rootEle: HTEle = this.element;
        const popAnimate: Object = { name: 'FadeOut', duration: 100 };
        let ele: HTEle;
        let nodes: NodeList;
        let value: string;
        switch (e.action) {
        case 'moveRight':
            if (this.options.isVertical) { return; }
            if (rootEle === trgt) {
                this.elementFocus(clst);
            } else if (!navChk) {
                this.eleFocus(clst, 'next');
            }
            break;
        case 'moveLeft':
            if (this.options.isVertical) { return; }
            if (!navChk) {
                this.eleFocus(clst, 'previous');
            }
            break;
        case 'home':
        case 'end':
            if (clst) {
                let popupCheck: HTEle = <HTEle>closest(clst, '.e-popup');
                const extendedPopup: HTEle = this.element.querySelector('.' + CLS_EXTENDABLECLASS);
                if (this.options.overflowMode === 'Extended' && extendedPopup && extendedPopup.classList.contains('e-popup-open')) {
                    popupCheck = e.action === 'end' ? extendedPopup : null;
                }
                if (popupCheck) {
                    if (isVisible(this.popObj.element)) {
                        nodes = [].slice.call(popupCheck.children);
                        if (e.action === 'home') {
                            ele = <HTEle>nodes[0];
                        } else {
                            ele = <HTEle>nodes[nodes.length - 1];
                        }
                    }
                } else {
                    nodes = this.element.querySelectorAll('.' + CLS_ITEMS + ' .' + CLS_ITEM + ':not(.' + CLS_SEPARATOR + '):not(.' + CLS_SPACER + ' )');
                    if (e.action === 'home') {
                        ele = <HTEle>nodes[0];
                    } else {
                        ele = <HTEle>nodes[nodes.length - 1];
                    }
                }
                if (ele) {
                    this.elementFocus(ele);
                }
            }
            break;
        case 'moveUp':
        case 'moveDown':
            value = e.action === 'moveUp' ? 'previous' : 'next';
            if (!this.options.isVertical) {
                if (popObj && closest(trgt, '.e-popup')) {
                    const popEle: HTEle = popObj.element;
                    const popFrstEle: HTEle = popEle.firstElementChild as HTEle;
                    if ((value === 'previous' && popFrstEle === clst) || (value === 'next' && popEle.lastElementChild === clst)) {
                        return;
                    } else {
                        this.eleFocus(clst, value);
                    }
                } else if (e.action === 'moveDown' && popObj && isVisible(popObj.element)) {
                    const skipEle: string | boolean = this.eleContains(clst);
                    if (skipEle) {
                        this.eleFocus(clst, value);
                    } else {
                        this.elementFocus(clst);
                    }
                }
            } else {
                if (e.action === 'moveUp') {
                    this.eleFocus(clst, 'previous');
                } else {
                    this.eleFocus(clst, 'next');
                }
            }
            break;
        case 'tab':
            if (!scrollChk && !navChk) {
                const ele: HTEle = (<HTEle>clst.firstElementChild);
                if (rootEle === trgt) {
                    if (this.activeEle) {
                        this.activeEle.focus();
                    } else {
                        this.activeEleRemove(ele);
                        ele.focus();
                    }
                }
            }
            break;
        case 'popupClose':
            if (popObj && this.options.overflowMode !== 'Extended') {
                popObj.hide(popAnimate);
            }
            break;
        case 'popupOpen':
            if (!navChk) { return; }
            if (popObj && !isVisible(popObj.element)) {
                popObj.element.style.top = rootEle.offsetHeight + 'px';
                popObj.show({ name: 'FadeIn', duration: 100 });
            } else {
                popObj.hide(popAnimate);
            }
            break;
        }
    }
    private keyActionHandler(e: KeyboardEventArgs): void {
        const trgt: HTEle = <HTEle>e.target;
        if (trgt.tagName === 'INPUT' || trgt.tagName === 'TEXTAREA' || this.element.classList.contains(CLS_DISABLE)) {
            return;
        }
        e.preventDefault();
        const tbrNavChk: boolean = trgt.classList.contains(CLS_TBARNAV);
        const tbarScrollChk: boolean = trgt.classList.contains(CLS_TBARSCRLNAV);
        const clst: HTMLElement = this.clstElement(tbrNavChk, trgt);
        if (clst || tbarScrollChk) {
            this.keyHandling(clst, e, trgt, tbrNavChk, tbarScrollChk);
        }
    }
    private eleContains(el: HTEle): string | boolean {
        const isInputEle: boolean = el.classList.contains('e-template') ? !isNOU(el.querySelector('.e-input')) ? true : false : false ;
        return el.classList.contains(CLS_SEPARATOR) || this.nonFocusableElements(el) || el.classList.contains(CLS_DISABLE) || el.getAttribute('disabled') || el.classList.contains(CLS_HIDDEN) || !isVisible(el) || isInputEle;
    }
    private eleFocus(closest: HTEle, pos: string): void {
        const sib: HTEle = Object(closest)[pos + 'ElementSibling'];
        if (sib) {
            const skipEle: string | boolean = this.eleContains(sib);
            if (skipEle) {
                this.eleFocus(sib, pos); return;
            }
            this.elementFocus(sib);
        } else if (this.tbarAlign) {
            let elem: HTEle = Object(closest.parentElement)[pos + 'ElementSibling'] as HTEle;
            if (!isNOU(elem) && elem.children.length === 0) {
                elem = Object(elem)[pos + 'ElementSibling'] as HTEle;
            }
            if (!isNOU(elem) && elem.children.length > 0) {
                if (pos === 'next') {
                    const el: HTEle = <HTEle>elem.querySelector('.' + CLS_ITEM);
                    if (this.eleContains(el)) {
                        this.eleFocus(el, pos);
                    } else {
                        (<HTEle>el.firstElementChild).focus();
                        this.activeEleSwitch(el);
                    }
                } else {
                    const el: HTEle = <HTEle>elem.lastElementChild;
                    if (this.eleContains(el)) {
                        this.eleFocus(el, pos);
                    } else {
                        this.elementFocus(el);
                    }
                }
            }
        }
    }
    private clickHandler(e: Event & MouseEvent): void {
        if (this.element.classList.contains('e-drag-action')) {
            return;
        }
        const clst: HTEle = <HTEle>closest(<Node>e.target, '.' + CLS_ITEM);
        if (!isNOU(clst) && !clst.classList.contains(CLS_DISABLE) && !isNOU(clst.firstElementChild)
            && clst.firstElementChild.getAttribute('aria-disabled') !== 'true') {
            return;
        }
        const trgt: HTEle = <HTEle>e.target;
        let clsList: DOMTokenList = trgt.classList;
        const ele: HTEle = this.element;
        let popupNav: HTEle = <HTEle>closest(trgt, ('.' + CLS_TBARNAV));
        let trgParentDataIndex: number;
        let item: ItemModel;
        if (!popupNav) {
            popupNav = trgt;
        }
        if (!ele.children[0].classList.contains('e-hscroll') && !ele.children[0].classList.contains('e-vscroll')
            && (clsList.contains(CLS_TBARNAV))) {
            clsList = trgt.querySelector('.e-icons').classList;
        }
        if (clsList.contains(CLS_POPUPICON) || clsList.contains(CLS_POPUPDOWN)) {
            this.popupClickHandler(ele, popupNav, CLS_RTL);
        }
        if (isNOU(clst) && !popupNav.classList.contains(CLS_TBARNAV)) {
            return;
        }
        if (!isNOU(clst)) {
            trgParentDataIndex = parseInt(clst.getAttribute('data-index'), 10);
            item = { id: clst.id };
        }
        const eventArgs: MouseArgs = {
            altKey: e.altKey,
            button: e.button,
            buttons: e.buttons,
            clientX: e.clientX,
            clientY: e.clientY,
            ctrlKey: e.ctrlKey,
            detail: e.detail,
            metaKey: e.metaKey,
            offsetX: e.offsetX,
            offsetY: e.offsetY,
            screenX: e.screenX,
            screenY: e.screenY,
            shiftKey: e.shiftKey,
            type: e.type
        };
        this.dotNetRef.invokeMethodAsync('TriggerClickEvent', eventArgs, trgParentDataIndex, item);
    }
    private popupClickHandler(ele: HTMLElement, popupNav: HTMLElement, CLS_RTL: string): void {
        const popObj: Popup = this.popObj;
        if (isVisible(popObj.element)) {
            popupNav.classList.remove(CLS_TBARNAVACT);
            popObj.hide({ name: 'FadeOut', duration: 100 });
        } else {
            if (ele.classList.contains(CLS_RTL) || this.options.isVerticalLeft) {
                if (ele.classList.contains(CLS_RTL)) {
                    popObj.enableRtl = true;
                }
                popObj.position = { X: 'left', Y: 'top' };
            }
            if (popObj.offsetX === 0 && (!ele.classList.contains(CLS_RTL) && !this.options.isVerticalLeft)) {
                popObj.enableRtl = false;
                popObj.position = { X: 'right', Y: 'top' };
            }
            popObj.dataBind();
            popObj.refreshPosition();
            popObj.element.style.top = this.getElementOffsetY() + 'px';
            if (this.options.overflowMode === 'Extended') {
                popObj.element.style.left = '0px';
                popObj.element.style.minHeight = '0px';
            }
            popupNav.classList.add(CLS_TBARNAVACT);
            popObj.show({ name: 'FadeIn', duration: 100 });
        }
    }
    public render(): void {
        this.scrollModule = null;
        this.popObj = null;
        this.isExtendedOpen = false;
        this.popupPriCount = 0;
        const width: string = formatUnit(this.options.width);
        const height: string = formatUnit(this.options.height);
        if (this.element) {
            if (Browser.info.name !== 'msie' || this.options.height !== 'auto') {
                setStyle(this.element, { 'height': height });
            }
            setStyle(this.element, { 'width': width });
            this.renderControl();
            this.wireEvents();
        }
    }
    private renderControl(): void {
        this.tbarAlgEle = { lefts: [], centers: [], rights: [] };
        this.renderItems();
        this.renderLayout();
    }
    private renderLayout(): void {
        this.renderOverflowMode();
        if (this.tbarAlign) { this.itemPositioning(); }
        if (this.popObj && this.popObj.element.childElementCount > 1 && this.checkPopupRefresh(this.element, this.popObj.element)) {
            this.popupRefresh(this.popObj.element, false);
        }
        this.separator();
    }
    private itemsAlign(items: ItemModel[], itemEleDom: HTEle, firstRender: boolean): void {
        let innerItem: HTEle;
        let innerPos: HTEle;
        if (!this.tbarEle) {
            this.tbarEle = [];
        }
        for (let i: number = 0; i < items.length; i++) {
            const itemEleBlaDom: HTEle = this.element.querySelector('.' + BZ_ITEMS);
            if (firstRender) {
                innerItem = itemEleDom.querySelector('.' + CLS_ITEM + '[id="' + items[parseInt(i.toString(), 10)].id + '"]');
            } else {
                innerItem = itemEleBlaDom.querySelector('.' + CLS_ITEM + '[id="' + items[parseInt(i.toString(), 10)].id + '"]');
            }
            if (!innerItem) {
                continue;
            }
            if (items[parseInt(i.toString(), 10)].overflow !== 'Show' && items[parseInt(i.toString(), 10)].showAlwaysInPopup && !innerItem.classList.contains(CLS_SEPARATOR)) {
                this.popupPriCount++;
            }
            if (items[parseInt(i.toString(), 10)].htmlAttributes) {
                this.setAttr(items[parseInt(i.toString(), 10)].htmlAttributes, innerItem);
            }
            if (items[parseInt(i.toString(), 10)].type === 'Button') {
                EventHandler.remove(innerItem, 'click', this.itemClick);
                EventHandler.add(innerItem, 'click', this.itemClick, this);
            }
            if (this.tbarEle.indexOf(innerItem) === -1) {
                this.tbarEle.push(innerItem);
            }
            if (this.options.overflowMode === 'MultiRow' && firstRender) {
                continue;
            }
            if (!this.tbarAlign) {
                this.tbarItemAlign(items[parseInt(i.toString(), 10)], itemEleDom, i);
            }
            innerPos = <HTEle>itemEleDom.querySelector('.e-toolbar-' + items[parseInt(i.toString(), 10)].align.toLowerCase());
            if (innerPos) {
                if (!(items[parseInt(i.toString(), 10)].showAlwaysInPopup && items[parseInt(i.toString(), 10)].overflow !== 'Show')) {
                    this.tbarAlgEle[(items[parseInt(i.toString(), 10)].align + 's').toLowerCase() as ItmAlign].push(innerItem);
                }
                innerPos.appendChild(innerItem);
            } else if (!firstRender) {
                itemEleDom.appendChild(innerItem);
            }
        }
    }
    public serverItemsRefresh(firstRender: boolean): void {
        const ele: HTEle = this.element;
        const wrapBlaEleDom: HTEle = <HTEle>ele.querySelector('.' + BZ_ITEMS);
        let itemEleDom: HTEle = <HTEle>ele.querySelector('.' + CLS_ITEMS);
        if ((itemEleDom && itemEleDom.children.length > 0) || wrapBlaEleDom.children.length > 0) {
            if (!itemEleDom && ele && ele.classList.contains(CLS_TOOLBAR) && ele.firstElementChild) {
                itemEleDom = createElement('div', { className: CLS_ITEMS });
                ele.insertBefore(itemEleDom, ele.firstElementChild);
            }
            this.itemsAlign(this.options.items, itemEleDom, firstRender);
            this.renderLayout();
            this.refreshOverflow();
        }
    }
    public resetServerItems(firstRender: boolean): void {
        if (firstRender) {
            return;
        }
        const wrapBlaEleDom: HTEle = <HTEle>this.element.querySelector('.' + BZ_ITEMS);
        const itemEles: HTEle[] = [].slice.call(selectAll('.' + CLS_ITEMS + ' .' + CLS_ITEM, this.element));
        append(itemEles, wrapBlaEleDom);
        this.clearProperty();
    }
    public changeOrientation(): void {
        if (!this.options.isVertical) {
            this.element.classList.remove(CLS_VERTICAL);
            this.element.setAttribute('aria-orientation', 'horizontal');
            if (this.options.height === 'auto' || this.options.height === '100%') {
                this.element.style.height = this.options.height;
            }
        } else {
            this.element.classList.add(CLS_VERTICAL);
            this.element.setAttribute('aria-orientation', 'vertical');
            setStyle(this.element, { 'height': formatUnit(this.options.height), 'width': formatUnit(this.options.width) });
        }
        this.destroyMode();
        this.refreshOverflow();
    }
    private initScroll(element: HTEle, innerItems: NodeList): void {
        if (!this.scrollModule && this.checkOverflow(element, <HTEle>innerItems[0])) {
            if (this.tbarAlign) {
                this.element.querySelector('.' + CLS_ITEMS + ' .' + CLS_TBARCENTER).removeAttribute('style');
            }
            if (this.options.isVertical) {
                // eslint-disable-next-line max-len
                this.scrollModule = new VScroll({ scrollStep: this.options.scrollStep, enableRtl: this.options.enableRtl }, <HTEle>innerItems[0]);
            } else {
                // eslint-disable-next-line max-len
                this.scrollModule = new HScroll({ scrollStep: this.options.scrollStep, enableRtl: this.options.enableRtl }, <HTEle>innerItems[0]);
            }
            if (this.scrollPosition) {
                (this.scrollModule as Record<string, any>)
                    .scrollEle[this.scrollModule.element.classList.contains('e-hscroll') ? 'scrollLeft' : 'scrollTop'] = this.scrollPosition;
                this.scrollPosition = null;
            }
            const scrollEle: Element = this.scrollModule.element.querySelector('.' + 'e-hscroll-bar' + ', .' + 'e-vscroll-bar');
            if (scrollEle) {
                scrollEle.removeAttribute('tabindex');
            }
            removeClass([this.scrollModule.element], CLS_TBARPOS);
            setStyle(this.element, { overflow: 'hidden' });
        }
    }
    private itemWidthCal(items: HTEle): number {
        let width: number = 0;
        let style: CSSStyleDeclaration;
        [].slice.call(selectAll('.' + CLS_ITEM, items)).forEach((el: HTEle) => {
            if (isVisible(el) && !el.classList.contains(CLS_SPACER)) {
                style = window.getComputedStyle(el);
                width += this.options.isVertical ? el.offsetHeight : el.offsetWidth;
                width += parseFloat(this.options.isVertical ? style.marginTop : style.marginRight);
                width += parseFloat(this.options.isVertical ? style.marginBottom : style.marginLeft);
            }
        });
        return width;
    }
    private getScrollCntEle(innerItem: HTEle): HTEle {
        const trgClass: string = (this.options.isVertical) ? '.e-vscroll-content' : '.e-hscroll-content';
        return <HTEle>innerItem.querySelector(trgClass);
    }
    private checkOverflow(element: HTEle, innerItem: HTEle): boolean {
        if (isNOU(element) || isNOU(innerItem) || !isVisible(element)) {
            return false;
        }
        const eleWidth: number = this.options.isVertical ? element.offsetHeight : element.offsetWidth;
        let itemWidth: number = this.options.isVertical ? innerItem.offsetHeight : innerItem.offsetWidth;
        if (this.tbarAlign || this.scrollModule || (eleWidth === itemWidth)  || element.querySelector('.' + CLS_SPACER)) {
            itemWidth = this.itemWidthCal(this.scrollModule ? this.getScrollCntEle(innerItem) : innerItem);
        }
        const popNav: HTEle = <HTEle>element.querySelector('.' + CLS_TBARNAV);
        const scrollNav: HTEle = <HTEle>element.querySelector('.' + CLS_TBARSCRLNAV);
        let navEleWidth: number = 0;
        if (popNav) {
            navEleWidth = this.options.isVertical ? popNav.offsetHeight : popNav.offsetWidth;
        } else if (scrollNav) {
            navEleWidth = this.options.isVertical ? (scrollNav.offsetHeight * (2)) : (scrollNav.offsetWidth * 2);
        }
        if (itemWidth > eleWidth - navEleWidth) {
            return true;
        } else { return false; }
    }
    public refreshOverflow(): void {
        this.resize();
    }
    private toolbarAlign(innerItems: HTEle): void {
        if (this.tbarAlign) {
            addClass([innerItems], CLS_TBARPOS);
            this.itemPositioning();
        }
    }
    public renderOverflowMode(): void {
        const ele: HTEle = this.element;
        const innerItems: HTEle = <HTEle>ele.querySelector('.' + CLS_ITEMS);
        const priorityCheck: boolean = this.popupPriCount > 0;
        if (ele && ele.children.length > 0) {
            this.offsetWid = ele.offsetWidth;
            removeClass([this.element], 'e-toolpop');
            if (Browser.info.name === 'msie' && this.options.height === 'auto') {
                ele.style.height = '';
            }
            switch (this.options.overflowMode) {
            case 'Scrollable':
                if (isNOU(this.scrollModule)) {
                    this.initScroll(ele, [].slice.call(ele.getElementsByClassName(CLS_ITEMS)));
                }
                break;
            case 'Popup':
                addClass([this.element], 'e-toolpop');
                if (this.tbarAlign) { this.removePositioning(); }
                if (this.checkOverflow(ele, innerItems) || priorityCheck) {
                    this.setOverflowAttributes(ele);
                }
                this.toolbarAlign(innerItems);
                break;
            case 'MultiRow':
                addClass([innerItems], CLS_MULTIROW);
                if (this.checkOverflow(ele, innerItems) && this.tbarAlign) {
                    this.removePositioning();
                    addClass([innerItems], CLS_MULTIROWPOS);
                }
                if (ele.style.overflow === 'hidden') {
                    ele.style.overflow = '';
                }
                if (Browser.info.name === 'msie' || ele.style.height !== 'auto') {
                    ele.style.height = 'auto';
                }
                break;
            case 'Extended':
                addClass([this.element], CLS_EXTEANDABLE_TOOLBAR);
                if (this.checkOverflow(ele, innerItems) || priorityCheck) {
                    if (this.tbarAlign) {
                        this.removePositioning();
                    }
                    this.setOverflowAttributes(ele);
                }
                this.toolbarAlign(innerItems);
            }
        }
    }
    private setOverflowAttributes(ele: HTMLElement): void {
        this.createPopupEle(ele, [].slice.call(selectAll('.' + CLS_ITEMS + ' .' + CLS_ITEM, ele)));
        const ariaAttr: { [key: string]: string } = {
            'tabindex': '0', 'role': 'button', 'aria-haspopup' : 'true',
            'aria-label': 'overflow'
        };
        attributes(this.element.querySelector('.' + CLS_TBARNAV), ariaAttr);
    }
    private separator(): void {
        const element: HTEle = this.element;
        const eleItem: HTEle[] = [].slice.call(element.querySelectorAll('.' + CLS_SEPARATOR));
        const multiVar: HTEle = element.querySelector('.' + CLS_MULTIROW_SEPARATOR) as HTEle;
        const extendVar: HTEle = element.querySelector('.' + CLS_EXTENDABLE_SEPARATOR) as HTEle;
        const eleInlineItem: HTEle = this.options.overflowMode === 'MultiRow' ? multiVar : extendVar;
        if (eleInlineItem !== null) {
            if (this.options.overflowMode === 'MultiRow') {
                eleInlineItem.classList.remove(CLS_MULTIROW_SEPARATOR);
            } else if (this.options.overflowMode === 'Extended') {
                eleInlineItem.classList.remove(CLS_EXTENDABLE_SEPARATOR);
            }
        }
        for (let i: number = 0; i <= eleItem.length - 1; i++) {
            if (eleItem[parseInt(i.toString(), 10)].offsetLeft < 30 && eleItem[parseInt(i.toString(), 10)].offsetLeft !== 0) {
                if (this.options.overflowMode === 'MultiRow') {
                    eleItem[parseInt(i.toString(), 10)].classList.add(CLS_MULTIROW_SEPARATOR);
                } else if (this.options.overflowMode === 'Extended') {
                    eleItem[parseInt(i.toString(), 10)].classList.add(CLS_EXTENDABLE_SEPARATOR);
                }
            }
        }
    }
    private createPopupEle(ele: HTMLElement, innerEle: HTMLElement[]): void {
        let innerNav: HTEle = <HTEle>ele.querySelector('.' + CLS_TBARNAV);
        const vertical: boolean = this.options.isVertical;
        if (!innerNav) {
            this.createPopupIcon(ele);
        }
        innerNav = <HTEle>ele.querySelector('.' + CLS_TBARNAV);
        const innerNavDom: number = (vertical ? innerNav.offsetHeight : innerNav.offsetWidth);
        const eleWidth: number = ((vertical ? ele.offsetHeight : ele.offsetWidth) - (innerNavDom));
        this.element.classList.remove('e-rtl');
        setStyle(this.element, { direction: 'initial', textAlign: 'left' });
        this.checkPriority(ele, innerEle, eleWidth, true);
        if (this.options.enableRtl) {
            this.element.classList.add('e-rtl');
        }
        this.element.style.removeProperty('direction');
        this.element.style.removeProperty('text-align');
        this.createPopup();
    }
    private pushingPoppedEle(tbarObj: SfToolbar, popupPri: Element[], ele: HTEle, eleHeight: number, sepHeight: number): void {
        const element: HTEle = this.element;
        let nodes: HTEle[] = selectAll('.' + CLS_TBAROVERFLOW, ele);
        let nodeIndex: number = 0;
        const poppedEle: HTEle[] = [].slice.call(selectAll('.' + CLS_POPUP, element.querySelector('.' + CLS_ITEMS)));
        let nodePri: number = 0;
        poppedEle.forEach((el: HTEle, index: number) => {
            nodes = selectAll('.' + CLS_TBAROVERFLOW, ele);
            if (el.classList.contains(CLS_TBAROVERFLOW) && nodes.length > 0) {
                if (tbarObj.tbResize && nodes.length > index) {
                    ele.insertBefore(el, nodes[parseInt(index.toString(), 10)]); ++nodePri;
                } else { ele.insertBefore(el, ele.children[nodes.length]); ++nodePri; }
            } else if (el.classList.contains(CLS_TBAROVERFLOW)) {
                ele.insertBefore(el, ele.firstChild); ++nodePri;
            } else if (tbarObj.tbResize && el.classList.contains(CLS_POPOVERFLOW) && ele.children.length > 0 && nodes.length === 0) {
                ele.insertBefore(el, ele.firstChild); ++nodePri;
            } else if (el.classList.contains(CLS_POPOVERFLOW)) {
                popupPri.push(el);
            } else if (tbarObj.tbResize) {
                ele.insertBefore(el, ele.children[nodeIndex + nodePri]);
                ++nodeIndex;
            } else {
                const children: HTMLElement[] = Array.prototype.slice.call(ele.children) as HTMLElement[];
                const insertionNode: HTMLElement = children.slice(index)
                    .find((child: HTMLElement) => !child.classList.contains(CLS_POPPRI));
                ele.insertBefore(el, insertionNode || null);
            }
            if (tbarObj.nonFocusableElements(el)) {
                setStyle(el, { display: '', height: sepHeight + 'px' });
            } else {
                setStyle(el, { display: '', height: eleHeight + 'px' });
            }
        });
        popupPri.forEach((el: Element) => {
            ele.appendChild(el);
        });
        const tbarEle: HTEle[] = selectAll('.' + CLS_ITEM, element.querySelector('.' + CLS_ITEMS));
        for (let i: number = tbarEle.length - 1; i >= 0; i--) {
            const tbarElement: HTEle = tbarEle[parseInt(i.toString(), 10)];
            if (this.nonFocusableElements(tbarElement) && this.options.overflowMode !== 'Extended') {
                setStyle(tbarElement, { display: 'none' });
            } else {
                break;
            }
        }
    }
    private createPopup(): void {
        const element: HTEle = this.element;
        let sepHeight: number;
        let sepItem: Element;
        if (this.options.overflowMode === 'Extended') {
            sepItem = element.querySelector('.' + CLS_SEPARATOR + ':not(.' + CLS_POPUP + ')');
            sepHeight = (element.style.height === 'auto' || element.style.height === '') ? null : (sepItem && (sepItem as HTEle).offsetHeight);
        }

        const eleItem: Element = element.querySelector('.' + CLS_ITEM + ':not(.' + CLS_SEPARATOR + '):not(.' + CLS_SPACER + '):not(.' + CLS_POPUP + '):not(.' + CLS_HIDDEN + ')');
        const eleHeight: number = (element.style.height === 'auto' || element.style.height === '') ? null : (eleItem && (eleItem as HTEle).offsetHeight);
        let ele: HTEle;
        const popupPri: Element[] = [];
        if (element.querySelector('#' + element.id + '_popup.' + CLS_POPUPCLASS)) {
            ele = <HTEle>element.querySelector('#' + element.id + '_popup.' + CLS_POPUPCLASS);
        } else {
            const extendEle: HTEle = createElement('div', {
                id: element.id + '_popup', className: CLS_POPUPCLASS + ' ' + CLS_EXTENDABLECLASS
            });
            const popupEle: HTEle = createElement('div', { id: element.id + '_popup', className: CLS_POPUPCLASS });
            ele = this.options.overflowMode === 'Extended' ? extendEle : popupEle;
        }
        this.pushingPoppedEle(this, popupPri, ele, eleHeight, sepHeight);
        this.popupInit(element, ele);
    }
    private getElementOffsetY(): number {
        return (this.options.overflowMode === 'Extended' && window.getComputedStyle(this.element).getPropertyValue('box-sizing') === 'border-box' ?
            this.element.clientHeight : this.element.offsetHeight);
    }
    private popupInit(element: HTEle, ele: HTEle): void {
        if (!this.popObj) {
            element.appendChild(ele);
            setStyle(this.element, { overflow: '' });
            const eleStyles: CSSStyleDeclaration = window.getComputedStyle(this.element);
            const popup: Popup = new Popup(null, {
                relateTo: this.element,
                offsetY: (this.options.isVertical) ? 0 : this.getElementOffsetY(),
                enableRtl: this.options.enableRtl,
                open: this.popupOpen.bind(this),
                close: this.popupClose.bind(this),
                collision: { Y: this.options.enableCollision ? 'flip' : 'none' },
                position: this.options.enableRtl ? { X: 'left', Y: 'top' } : { X: 'right', Y: 'top' }
            });
            if (this.options.overflowMode === 'Extended') {
                popup.width = parseFloat(eleStyles.width) + ((parseFloat(eleStyles.borderRightWidth)) * 2);
                popup.offsetX = 0;
            }
            popup.appendTo(ele);
            EventHandler.add(document, 'scroll', this.docEvent.bind(this));
            EventHandler.add(document, 'click ', this.docEvent.bind(this));
            popup.element.style.maxHeight = popup.element.offsetHeight + 'px';
            if (this.options.isVertical) { popup.element.style.visibility = 'hidden'; }
            if (this.isExtendedOpen) {
                const popupNav: HTEle = this.element.querySelector('.' + CLS_TBARNAV);
                popupNav.classList.add(CLS_TBARNAVACT);
                classList(popupNav.firstElementChild, [CLS_POPUPICON], [CLS_POPUPDOWN]);
                this.element.querySelector('.' + CLS_EXTENDABLECLASS).classList.add(CLS_POPUPOPEN);
            } else {
                popup.hide();
            }
            this.popObj = popup;
        } else {
            const popupEle: HTEle = this.popObj.element;
            if (this.options.overflowMode === 'Extended') {
                const eleStyle: CSSStyleDeclaration = window.getComputedStyle(this.element);
                this.popObj.width = parseFloat(eleStyle.width) + ((parseFloat(eleStyle.borderRightWidth)) * 2);
                this.popObj.offsetX = 0;
                this.popObj.dataBind();
            }
            setStyle(popupEle, { maxHeight: '', display: 'block' });
            setStyle(popupEle, { maxHeight: popupEle.offsetHeight + 'px', display: '' });
        }
    }
    private tbarPopupHandler(isOpen: boolean): void {
        if (this.options.overflowMode === 'Extended') {
            if (isOpen) {
                addClass([this.element], CLS_EXTENDEDPOPOPEN);
            } else {
                removeClass([this.element], CLS_EXTENDEDPOPOPEN);
            }
        }
    }
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    private popupOpen(e: Event): void {
        const popObj: Popup = this.popObj;
        if (!this.options.isVertical) {
            popObj.offsetY = this.getElementOffsetY();
            popObj.dataBind();
        }
        const popupEle: HTEle = this.popObj.element;
        const toolEle: HTEle = this.popObj.element.parentElement;
        const popupNav: HTEle = <HTEle>toolEle.querySelector('.' + CLS_TBARNAV);
        popupNav.setAttribute('aria-expanded', 'true');
        setStyle(popObj.element, { height: 'auto', maxHeight: '' });
        popObj.element.style.maxHeight = popObj.element.offsetHeight + 'px';
        if (this.options.overflowMode === 'Extended') {
            popObj.element.style.left = '';
            popObj.element.style.minHeight = '';
        }
        const popupElePos: number = popupEle.offsetTop + popupEle.offsetHeight + calculatePosition(toolEle).top;
        const popIcon: Element = (popupNav.firstElementChild as Element);
        popupNav.classList.add(CLS_TBARNAVACT);
        classList(popIcon, [CLS_POPUPICON], [CLS_POPUPDOWN]);
        this.tbarPopupHandler(true);
        const scrollVal: number = isNOU(window.scrollY) ? 0 : window.scrollY;
        if ((this.options.overflowMode !== 'Extended' || this.options.enableCollision) && !this.options.isVertical &&
            ((window.innerHeight + scrollVal) < popupElePos) && (this.element.offsetTop < popupEle.offsetHeight)) {
            let overflowHeight: number = (popupEle.offsetHeight - ((popupElePos - window.innerHeight - scrollVal) + 5));
            popObj.height = overflowHeight + 'px';
            for (let i: number = 0; i <= popupEle.childElementCount; i++) {
                const ele: HTEle = <HTEle>popupEle.children[parseInt(i.toString(), 10)];
                if (ele.offsetTop + ele.offsetHeight > overflowHeight) {
                    overflowHeight = ele.offsetTop;
                    break;
                }
            }
            setStyle(popObj.element, { maxHeight: overflowHeight + 'px' });
        } else if (this.options.isVertical) {
            const tbEleData: ClientRect = this.element.getBoundingClientRect();
            setStyle(popObj.element, { maxHeight: (tbEleData.top + this.element.offsetHeight) + 'px', bottom: 0, visibility: '' });
        }
        if (popObj) {
            const popupOffset: ClientRect = popupEle.getBoundingClientRect();
            if ( popupOffset.right > document.documentElement.clientWidth && popupOffset.width > toolEle.getBoundingClientRect().width) {
                popObj.collision = { Y: 'none'};
                popObj.dataBind();
            }
            popObj.refreshPosition();
        }
    }
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    private popupClose(e: Event): void {
        const element: HTEle = this.element;
        const popupNav: HTEle = <HTEle>element.querySelector('.' + CLS_TBARNAV);
        popupNav.setAttribute('aria-expanded', 'false');
        const popIcon: Element = (popupNav.firstElementChild as Element);
        popupNav.classList.remove(CLS_TBARNAVACT);
        classList(popIcon, [CLS_POPUPDOWN], [CLS_POPUPICON]);
        this.tbarPopupHandler(false);
    }
    private checkPriority(ele: HTEle, inEle: HTEle[], eleWidth: number, pre: boolean): void {
        const popPriority: boolean = this.popupPriCount > 0;
        const len: number = inEle.length;
        const eleWid: number = eleWidth;
        let eleOffset: number;
        let checkoffset: boolean;
        let sepCheck: number = 0; let itemCount: number = 0; let itemPopCount: number = 0;
        const checkClass: Function = (ele: HTEle, val: string[]) => {
            let rVal: boolean = false;
            val.forEach((cls: string) => {
                if (ele.classList.contains(cls)) {
                    rVal = true;
                }
            });
            return rVal;
        };
        for (let i: number = len - 1; i >= 0; i--) {
            let mrgn: number;
            const compuStyle: CSSStyleDeclaration = window.getComputedStyle(inEle[parseInt(i.toString(), 10)]);
            if (this.options.isVertical) {
                mrgn = parseFloat((compuStyle).marginTop);
                mrgn += parseFloat((compuStyle).marginBottom);
            } else {
                mrgn = parseFloat((compuStyle).marginRight);
                mrgn += parseFloat((compuStyle).marginLeft);
            }
            const fstEleCheck: boolean = inEle[parseInt(i.toString(), 10)] === this.tbarEle[0];
            if (fstEleCheck) { this.tbarEleMrgn = mrgn; }
            eleOffset = this.options.isVertical ? inEle[parseInt(i.toString(), 10)].offsetHeight :
                inEle[parseInt(i.toString(), 10)].offsetWidth;
            const eleWid: number = fstEleCheck ? (eleOffset + mrgn) : eleOffset;
            if (checkClass(inEle[parseInt(i.toString(), 10)], [CLS_POPPRI]) && popPriority) {
                inEle[parseInt(i.toString(), 10)].classList.add(CLS_POPUP);
                if (this.options.isVertical) {
                    setStyle(inEle[parseInt(i.toString(), 10)], { display: 'none', minHeight: eleWid + 'px' });
                } else {
                    setStyle(inEle[parseInt(i.toString(), 10)], { display: 'none', minWidth: eleWid + 'px' });
                }
                itemPopCount++;
            }
            if (this.options.isVertical) {
                checkoffset = (inEle[parseInt(i.toString(), 10)].offsetTop +
                    inEle[parseInt(i.toString(), 10)].offsetHeight + mrgn) > eleWidth;
            } else {
                checkoffset = (inEle[parseInt(i.toString(), 10)].offsetLeft +
                    inEle[parseInt(i.toString(), 10)].offsetWidth + mrgn) > eleWidth;
            }
            const toolbarItems: HTMLElement = ele.querySelector('.' + CLS_ITEMS) as HTMLElement ;
            const checkWidth: boolean = ele.querySelector('.' + CLS_SPACER) ?
                toolbarItems.offsetWidth < this.getItemsWidth(toolbarItems) : checkoffset;
            if (checkWidth) {
                if (this.nonFocusableElements(inEle[parseInt(i.toString(), 10)])) {
                    if (this.options.overflowMode === 'Extended') {
                        if (itemCount === itemPopCount) {
                            const sepEle: HTEle = (inEle[parseInt(i.toString(), 10)] as HTEle);
                            if (checkClass(sepEle, [CLS_SEPARATOR, CLS_TBARIGNORE]) || checkClass(sepEle, [CLS_SPACER, CLS_TBARIGNORE]) ) {
                                inEle[parseInt(i.toString(), 10)].classList.add(CLS_POPUP);
                                itemPopCount++;
                            }
                        }
                        itemCount++;
                    } else if (this.options.overflowMode === 'Popup') {
                        if (sepCheck > 0 && itemCount === itemPopCount) {
                            const sepEle: HTEle = (inEle[i + itemCount + (sepCheck - 1)] as HTEle);
                            if (checkClass(sepEle, [CLS_SEPARATOR, CLS_TBARIGNORE]) ||
                                checkClass(sepEle, [CLS_SPACER, CLS_TBARIGNORE]) ||
                                checkClass(sepEle, [CLS_SPACER, CLS_TBARIGNORE])) {
                                setStyle(sepEle, { display: 'none' });
                            }
                        }
                        sepCheck++; itemCount = 0; itemPopCount = 0;
                    }
                } else {
                    itemCount++;
                }
                if (inEle[parseInt(i.toString(), 10)].classList.contains(CLS_TBAROVERFLOW) && pre) {
                    eleWidth -= ((this.options.isVertical ? inEle[parseInt(i.toString(), 10)].offsetHeight :
                        inEle[parseInt(i.toString(), 10)].offsetWidth) + (mrgn));
                } else if (!checkClass(inEle[parseInt(i.toString(), 10)], [CLS_SEPARATOR, CLS_TBARIGNORE]) &&
                    !checkClass(inEle[parseInt(i.toString(), 10)], [CLS_SPACER, CLS_TBARIGNORE])) {
                    inEle[parseInt(i.toString(), 10)].classList.add(CLS_POPUP);
                    if (this.options.isVertical) {
                        setStyle(inEle[parseInt(i.toString(), 10)], { display: 'none', minHeight: eleWid + 'px' });
                    } else {
                        setStyle(inEle[parseInt(i.toString(), 10)], { display: 'none', minWidth: eleWid + 'px' });
                    }
                    itemPopCount++;
                } else {
                    eleWidth -= ((this.options.isVertical ? inEle[parseInt(i.toString(), 10)].offsetHeight :
                        inEle[parseInt(i.toString(), 10)].offsetWidth) + (mrgn));
                }
            }
        }
        if (pre) {
            const popedEle: HTEle[] = selectAll('.' + CLS_ITEM + ':not(.' + CLS_POPUP + ')', this.element);
            this.checkPriority(ele, popedEle, eleWid, false);
        }
    }
    private createPopupIcon(element: HTEle): void {
        const id: string = element.id.concat('_nav');
        let className: string = 'e-' + element.id.concat('_nav ' + CLS_POPUPNAV);
        className = this.options.overflowMode === 'Extended' ? className + ' ' + CLS_EXTENDPOPUP : className;
        const nav: HTEle = createElement('div', { id: id, className: className });
        if (Browser.info.name === 'msie' || Browser.info.name === 'edge') {
            nav.classList.add('e-ie-align');
        }
        const navItem: HTEle = createElement('div', { className: CLS_POPUPDOWN + ' e-icons' });
        nav.appendChild(navItem);
        nav.setAttribute('tabindex', '0');
        nav.setAttribute('role', 'button');
        element.appendChild(nav);
        element.classList.add('e-pop-mode');
    }
    private tbarPriRef(inEle: HTEle, indx: number, sepPri: number, el: HTEle, des: boolean, elWid: number, wid: number, ig: number): void {
        const ignoreCount: number = ig;
        const popEle: HTEle = this.popObj.element;
        const query: string = '.' + CLS_ITEM + ':not(.' + CLS_SEPARATOR + '):not(.' + CLS_SPACER + '):not(.' + CLS_TBAROVERFLOW + ')';
        const priEleCnt: number = selectAll('.' + CLS_POPUP + ':not(.' + CLS_TBAROVERFLOW + ')', popEle).length;
        const checkClass: Function = (ele: HTEle, val: string) => {
            return ele.classList.contains(val);
        };
        if (selectAll(query, inEle).length === 0) {
            const eleSep: HTEle = inEle.children[indx - (indx - sepPri) - 1] as HTEle;
            const ignoreCheck: boolean = (!isNOU(eleSep) && checkClass(eleSep, CLS_TBARIGNORE));
            if ((!isNOU(eleSep) && checkClass(eleSep, CLS_SEPARATOR) &&
                checkClass(eleSep, CLS_SPACER) && !isVisible(eleSep)) || ignoreCheck) {
                const sepDisplay: string = 'none';
                eleSep.style.display = 'inherit';
                const eleSepWidth: number = eleSep.offsetWidth + (parseFloat(window.getComputedStyle(eleSep).marginRight) * 2);
                const prevSep: HTEle = eleSep.previousElementSibling as HTEle;
                if ((elWid + eleSepWidth) < wid || des) {
                    inEle.insertBefore(el, inEle.children[(indx + ignoreCount) - (indx - sepPri)]);
                    if (!isNOU(prevSep)) {
                        prevSep.style.display = '';
                    }
                } else {
                    if (this.nonFocusableElements(prevSep)) {
                        prevSep.style.display = sepDisplay;
                    }
                }
                eleSep.style.display = '';
            } else {
                inEle.insertBefore(el, inEle.children[(indx + ignoreCount) - (indx - sepPri)]);
            }
        } else {
            inEle.insertBefore(el, inEle.children[(indx + ignoreCount) - priEleCnt]);
        }
    }
    public popupRefresh(popupEle: HTMLElement, destroy: boolean): void {
        const ele: HTEle = this.element;
        const isVer: boolean = this.options.isVertical;
        let popNav: HTEle = <HTEle>ele.querySelector('.' + CLS_TBARNAV);
        const innerEle: HTEle = <HTEle>ele.querySelector('.' + CLS_ITEMS);
        if (isNOU(popNav) || isNOU(innerEle)) {
            return;
        }
        innerEle.removeAttribute('style');
        popupEle.style.display = 'block';
        let dimension: number;
        if (isVer) {
            dimension = ele.offsetHeight - (popNav.offsetHeight + (ele.querySelector('.' + CLS_SPACER) ? this.itemWidthCal(innerEle) : innerEle.offsetHeight));
        } else {
            dimension = ele.offsetWidth - (popNav.offsetWidth + (ele.querySelector('.' + CLS_SPACER) ? this.itemWidthCal(innerEle) : innerEle.offsetWidth));
        }
        let popupEleWidth: number = 0;
        [].slice.call(popupEle.children).forEach((el: HTMLElement): void => {
            popupEleWidth += this.popupEleWidth(el);
            setStyle(el, { 'position': '' });
        });
        if ((dimension + (isVer ? popNav.offsetHeight : popNav.offsetWidth)) > (popupEleWidth) && this.popupPriCount === 0) {
            destroy = true;
        }
        this.popupEleRefresh(dimension, popupEle, destroy);
        popupEle.style.display = '';
        if (popupEle.children.length === 0 && popNav && this.popObj) {
            detach(popNav);
            popNav = null;
            this.popObj.destroy();
            detach(this.popObj.element);
            this.popObj = null;
            this.element.classList.remove('e-pop-mode');
        }
    }
    private ignoreEleFetch(index: number, innerEle: HTEle): number {
        const ignoreEle: HTEle[] = [].slice.call(innerEle.querySelectorAll('.' + CLS_TBARIGNORE));
        const ignoreInx: number[] = [];
        let count: number = 0;
        if (ignoreEle.length > 0) {
            ignoreEle.forEach((ele: HTEle): void => {
                ignoreInx.push([].slice.call(innerEle.children).indexOf(ele));
            });
        } else {
            return 0;
        }
        ignoreInx.forEach((val: number): void => {
            if (val <= index) { count++; }
        });
        return count;
    }
    private checkPopupRefresh(root: HTEle, popEle: HTEle): boolean {
        popEle.style.display = 'block';
        const elWid: number = this.popupEleWidth(<HTEle>popEle.firstElementChild);
        (<HTEle>popEle.firstElementChild).style.removeProperty('Position');
        const tbarWidth: number = root.offsetWidth - (<HTEle>root.querySelector('.' + CLS_TBARNAV)).offsetWidth;
        const tbarItems: HTEle = (<HTEle>root.querySelector('.' + CLS_ITEMS));
        const tbarItemsWid : number = root.querySelector('.' + CLS_SPACER) ? this.itemWidthCal(tbarItems) : tbarItems.offsetWidth;
        popEle.style.removeProperty('display');
        if (tbarWidth > (elWid + tbarItemsWid)) {
            return true;
        }
        return false;
    }
    private popupEleWidth(el: HTEle): number {
        el.style.position = 'absolute';
        let elWidth: number = this.options.isVertical ? el.offsetHeight : el.offsetWidth;
        const btnText: HTEle = <HTEle>el.querySelector('.' + CLS_TBARBTNTEXT);
        if (el.classList.contains('e-tbtn-align') || el.classList.contains(CLS_TBARTEXT)) {
            const btn: HTEle = <HTEle>el.children[0];
            if (!isNOU(btnText) && el.classList.contains(CLS_TBARTEXT)) {
                btnText.style.display = 'none';
            } else if (!isNOU(btnText) && el.classList.contains(CLS_POPUPTEXT)) {
                btnText.style.display = 'block';
            }
            btn.style.minWidth = '0%';
            elWidth = parseFloat(!this.options.isVertical ? el.style.minWidth : el.style.minHeight);
            btn.style.minWidth = '';
            btn.style.minHeight = '';
            if (!isNOU(btnText)) {
                btnText.style.display = '';
            }
        }
        return elWidth;
    }
    private popupEleRefresh(width: number, popupEle: HTEle, destroy: boolean): void {
        const popPriority: boolean = this.popupPriCount > 0;
        let eleSplice: HTEle[] = this.tbarEle;
        let priEleCnt: number;
        let index: number;
        let innerEle: HTEle = <HTEle>this.element.querySelector('.' + CLS_ITEMS);
        let ignoreCount: number = 0;
        for (const el of [].slice.call(popupEle.children)) {
            if (el.classList.contains(CLS_POPPRI) && popPriority && !destroy) {
                continue;
            }
            let elWidth: number = this.popupEleWidth(el);
            if (el === this.tbarEle[0]) { elWidth += this.tbarEleMrgn; }
            el.style.position = '';
            if (elWidth < width || destroy ) {
                setStyle(el, { minWidth: '', height: '', minHeight: '' });
                if (!el.classList.contains(CLS_POPOVERFLOW)) {
                    el.classList.remove(CLS_POPUP);
                }
                index = this.tbarEle.indexOf(el);
                if (this.tbarAlign) {
                    const pos: ItemAlign = this.options.items[parseInt(index.toString(), 10)].align;
                    index = this.tbarAlgEle[(pos + 's').toLowerCase() as ItmAlign].indexOf(el);
                    eleSplice = this.tbarAlgEle[(pos + 's').toLowerCase() as ItmAlign];
                    innerEle = <HTEle>this.element.querySelector('.' + CLS_ITEMS + ' .' + 'e-toolbar-' + pos.toLowerCase());
                }
                let sepBeforePri: number = 0;
                if (this.options.overflowMode !== 'Extended') {
                    eleSplice.slice(0, index).forEach((el: HTEle) => {
                        if (!isNOU(el.classList) && (el.classList.contains(CLS_TBAROVERFLOW) || this.nonFocusableElements(el))) {
                            if (this.nonFocusableElements(el)) {
                                el.style.display = '';
                                width -= el.offsetWidth;
                            }
                            sepBeforePri++;
                        }
                    });
                }
                ignoreCount = this.ignoreEleFetch(index, innerEle);
                if (el.classList.contains(CLS_TBAROVERFLOW)) {
                    this.tbarPriRef(innerEle, index, sepBeforePri, el, destroy, elWidth, width, ignoreCount);
                    width -= el.offsetWidth;
                } else if (index === 0) {
                    innerEle.insertBefore(el, innerEle.firstChild);
                    width -= el.offsetWidth;
                } else {
                    priEleCnt = selectAll('.' + CLS_TBAROVERFLOW, this.popObj.element).length;
                    innerEle.insertBefore(el, innerEle.children[(index + ignoreCount) - priEleCnt]);
                    width -= el.offsetWidth;
                }
                el.style.height = '';
            } else {
                break;
            }
        }
        const checkOverflow: boolean = this.checkOverflow(this.element, this.element.getElementsByClassName(CLS_ITEMS)[0] as HTEle);
        if (checkOverflow && !destroy) {
            this.renderOverflowMode();
        }
    }
    private removePositioning(): void {
        const item: HTEle = this.element.querySelector('.' + CLS_ITEMS) as HTEle;
        if (isNOU(item) || !item.classList.contains(CLS_TBARPOS)) { return; }
        removeClass([item], CLS_TBARPOS);
        const innerItem: HTEle[] = [].slice.call(item.children);
        innerItem[1].removeAttribute('style');
        innerItem[2].removeAttribute('style');
    }
    private refreshPositioning(): void {
        const item: HTEle = this.element.querySelector('.' + CLS_ITEMS) as HTEle;
        addClass([item], CLS_TBARPOS);
        this.itemPositioning();
    }
    public itemPositioning(): void {
        const item: HTEle = this.element.querySelector('.' + CLS_ITEMS) as HTEle;
        let margin: number;
        if (isNOU(item) || !item.classList.contains(CLS_TBARPOS)) { return; }
        const popupNav: HTEle = <HTEle>this.element.querySelector('.' + CLS_TBARNAV);
        let innerItem: HTEle[];
        if (this.scrollModule) {
            const trgClass: string = (this.options.isVertical) ? CLS_VSCROLLCNT : CLS_HSCROLLCNT;
            innerItem = [].slice.call(item.querySelector('.' + trgClass).children);
        } else {
            innerItem = [].slice.call(item.children);
        }
        if (this.options.isVertical) {
            margin = innerItem[0].offsetHeight + innerItem[2].offsetHeight;
        } else {
            margin = innerItem[0].offsetWidth + innerItem[2].offsetWidth;
        }
        let tbarWid: number = this.options.isVertical ? this.element.offsetHeight : this.element.offsetWidth;
        if (popupNav) {
            tbarWid -= (this.options.isVertical ? popupNav.offsetHeight : popupNav.offsetWidth);
            const popWid: string = (this.options.isVertical ? popupNav.offsetHeight : popupNav.offsetWidth) + 'px';
            innerItem[2].removeAttribute('style');
            if (this.options.isVertical) {
                if (this.options.enableRtl) {
                    innerItem[2].style.top = popWid;
                } else {
                    innerItem[2].style.bottom = popWid;
                }
            } else {
                if (this.options.enableRtl) {
                    innerItem[2].style.left = popWid;
                } else {
                    innerItem[2].style.right = popWid;
                }
            }
        }
        if (tbarWid <= margin) { return; }
        // eslint-disable-next-line max-len
        const value: number = (((tbarWid - margin)) - (!this.options.isVertical ? innerItem[1].offsetWidth : innerItem[1].offsetHeight)) / 2;
        innerItem[1].removeAttribute('style');
        const mrgn: string = ((!this.options.isVertical ? innerItem[0].offsetWidth : innerItem[0].offsetHeight) + value) + 'px';
        if (this.options.isVertical) {
            if (this.options.enableRtl) {
                innerItem[1].style.marginBottom = mrgn;
            } else {
                innerItem[1].style.marginTop = mrgn;
            }
        } else {
            if (this.options.enableRtl) {
                innerItem[1].style.marginRight = mrgn;
            } else {
                innerItem[1].style.marginLeft = mrgn;
            }
        }
    }
    private tbarItemAlign(item: ItemModel, itemEle: HTEle, pos: number): void {
        if (item.showAlwaysInPopup && item.overflow !== 'Show') { return; }
        const alignDiv: HTMLElement[] = [];
        alignDiv.push(createElement('div', { className: CLS_TBARLEFT, attrs: { role: 'group' } }));
        alignDiv.push(createElement('div', { className: CLS_TBARCENTER, attrs: { role: 'group' } }));
        alignDiv.push(createElement('div', { className: CLS_TBARRIGHT, attrs: { role: 'group' } }));
        if (pos === 0 && item.align !== 'Left') {
            alignDiv.forEach((ele: HTEle) => {
                itemEle.appendChild(ele);
            });
            this.tbarAlign = true;
            addClass([itemEle], CLS_TBARPOS);
        } else if (item.align !== 'Left') {
            const alignEle: HTMLElement[] = [].slice.call(itemEle.children);
            const leftAlign: HTEle = alignDiv[0];
            [].slice.call(alignEle).forEach((el: HTEle) => {
                this.tbarAlgEle.lefts.push(el);
                leftAlign.appendChild(el);
            });
            itemEle.appendChild(leftAlign);
            itemEle.appendChild(alignDiv[1]);
            itemEle.appendChild(alignDiv[2]);
            this.tbarAlign = true;
            addClass([itemEle], CLS_TBARPOS);
        }
    }
    private renderItems(): void {
        const ele: HTEle = this.element;
        const items: ItemModel[] = <ItemModel[]>this.options.items;
        if (ele && !isNOU(items) && items.length > 0) {
            const itemEleDom: HTEle = <HTEle>ele.querySelector('.' + CLS_ITEMS);
            this.itemsAlign(items, itemEleDom, true);
        }
    }
    private setAttr(attr: { [key: string]: string; }, element: HTEle): void {
        const key: Object[] = Object.keys(attr);
        let keyVal: string;
        for (let i: number = 0; i < key.length; i++) {
            keyVal = key[parseInt(i.toString(), 10)] as string;
            if (keyVal === 'class') {
                addClass([element], attr[`${keyVal}`]);
            } else {
                element.setAttribute(keyVal, attr[`${keyVal}`]);
            }
        }
    }
    private getDataTabindex(ele: HTEle): string {
        return isNOU(ele.getAttribute('data-tabindex')) ? '-1' : ele.getAttribute('data-tabindex');
    }
    private itemClick(e: Event): void {
        this.activeEleSwitch(<HTEle>e.currentTarget);
    }
    private activeEleSwitch(ele: HTEle): void {
        this.activeEleRemove(<HTEle>ele.firstElementChild);
        this.activeEle.focus();
    }
    private activeEleRemove(curEle: HTEle): void {
        if (!isNOU(this.activeEle)) {
            this.activeEle.setAttribute('tabindex', this.getDataTabindex(this.activeEle));
        }
        this.activeEle = curEle;
        if (this.getDataTabindex(this.activeEle) === '-1') {
            if (isNOU(this.trgtEle) && !(<HTEle>curEle.parentElement).classList.contains(CLS_TEMPLATE)) {
                this.updateTabIndex('-1');
                curEle.removeAttribute('tabindex');
            } else {
                const tabIndex: number = parseInt(this.getDataTabindex(this.activeEle), 10) + 1;
                this.activeEle.setAttribute('tabindex', tabIndex.toString());
            }
        }
    }
    private resize(): void {
        const ele: HTEle = this.element;
        this.tbResize = true;
        if (this.tbarAlign) { this.itemPositioning(); }
        if (this.popObj && this.options.overflowMode === 'Popup') {
            this.popObj.hide();
        }
        const checkOverflow: boolean = this.checkOverflow(ele, ele.getElementsByClassName(CLS_ITEMS)[0] as HTEle);
        if (!checkOverflow) {
            this.destroyScroll();
            const multirowele: HTEle = ele.querySelector('.' + CLS_ITEMS);
            if (!isNOU(multirowele)) {
                removeClass([multirowele], CLS_MULTIROWPOS);
                if (this.tbarAlign) { addClass([multirowele], CLS_TBARPOS); }
            }
        }
        if (checkOverflow && this.scrollModule && (this.offsetWid === ele.offsetWidth)) { return; }
        if (this.offsetWid > ele.offsetWidth || checkOverflow) {
            this.renderOverflowMode();
        }
        if (this.popObj) {
            if (this.options.overflowMode === 'Extended') {
                const eleStyles: CSSStyleDeclaration = window.getComputedStyle(this.element);
                this.popObj.width = parseFloat(eleStyles.width) + ((parseFloat(eleStyles.borderRightWidth)) * 2);
            }
            if (this.tbarAlign) { this.removePositioning(); }
            this.popupRefresh(this.popObj.element, false);
            if (this.tbarAlign) { this.refreshPositioning(); }
        }
        this.offsetWid = ele.offsetWidth;
        this.tbResize = false;
        this.separator();
    }

    private orientationChange(): void {
        setTimeout(() => {
            this.resize();
        }, 500);
    }

    public extendedOpen(): void {
        const sib: HTEle = this.element.querySelector('.' + CLS_EXTENDABLECLASS) as HTEle;
        if (this.options.overflowMode === 'Extended' && sib) {
            this.isExtendedOpen = sib.classList.contains(CLS_POPUPOPEN);
        }
    }
    public updateHideEleTabIndex(ele: HTMLElement, isHidden: boolean, eleIndex: number, innerItems: HTEle[]): void {
        let nextEle: HTEle = innerItems[++eleIndex];
        while (nextEle) {
            const skipEle: string | boolean = this.eleContains(nextEle);
            if (!skipEle) {
                const dataTabIndex: string = nextEle.firstElementChild.getAttribute('data-tabindex');
                if (!isHidden && dataTabIndex === '-1') {
                    nextEle.firstElementChild.setAttribute('tabindex', '0');
                } else if (dataTabIndex !== nextEle.firstElementChild.getAttribute('tabindex')) {
                    nextEle.firstElementChild.setAttribute('tabindex', dataTabIndex);
                }
                break;
            }
            nextEle = innerItems[++eleIndex];
        }
    }
    public disable(value: boolean): void {
        const rootEle: HTMLElement = this.element;
        if (value) {
            rootEle.classList.add(CLS_DISABLE);
        } else {
            rootEle.classList.remove(CLS_DISABLE);
        }
        if (this.activeEle) {
            this.activeEle.setAttribute('tabindex', this.activeEle.getAttribute('data-tabindex'));
        }
        if (this.scrollModule) {
            this.scrollModule.disable(value);
        }
        if (this.popObj) {
            if (isVisible(this.popObj.element) && this.options.overflowMode !== 'Extended') {
                this.popObj.hide();
            }
            rootEle.querySelector('#' + rootEle.id + '_nav').setAttribute('tabindex', !value ? '0' : '-1');
        }
    }
    public setCssClass(cssClass: string): void {
        this.extendedOpen();
        if (this.options.cssClass) { removeClass([this.element], this.options.cssClass.split(' ')); }
        if (cssClass) { addClass([this.element], cssClass.split(' ')); }
        this.options.cssClass = cssClass;
    }

    private nonFocusableElements(element: HTMLElement): boolean {
        return element.classList.contains(CLS_SEPARATOR) || element.classList.contains(CLS_SPACER);
    }
    private getItemsWidth(element: HTMLElement): number {
        const width: number = Array.from(element.children).reduce((total: number, child: Element) => total +
            (child as HTMLElement).offsetWidth, 0);
        return width;
    }
}

interface IToolbarOptions {
    items: ItemModel[];
    width: string;
    height: string;
    cssClass: string;
    overflowMode: OverflowMode;
    scrollStep: number;
    enableCollision: boolean;
    allowKeyboard: boolean;
    enableRtl: boolean;
    isVertical: boolean;
    isVerticalLeft: boolean;
}

interface MouseArgs {
    altKey: boolean;
    button: number;
    buttons: number;
    clientX: number;
    clientY: number;
    ctrlKey: boolean;
    detail: number;
    metaKey: boolean;
    offsetX: number;
    offsetY: number;
    screenX: number;
    screenY: number;
    shiftKey: boolean;
    type: string;
}

// tslint:disable
const Toolbar: object = {
    initialize(dataId: string, element: HTMLElement, options: IToolbarOptions, dotnetRef: BlazorDotnetObject): void {
        if (options.scrollStep === 0) {
            options.scrollStep = null;
        }
        const instance: SfToolbar = new SfToolbar(dataId, element, options, dotnetRef);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            const isSpacer: HTMLElement = (instance.element.querySelector('.' + CLS_ITEMS) as HTMLElement).querySelector('.' + CLS_SPACER) as HTMLElement;
            if (isSpacer == null) {
                instance.element.classList.remove('e-spacer-toolbar');
            }
            instance.render();
        }
    },
    hidePopup(dataId: string, targetIndex: number): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element) && !isNOU(instance.popObj)) {
            const element: HTEle = instance.element.querySelector('.' + CLS_ITEM + '[data-index="' + targetIndex + '"]');
            if (!isNOU(element) && !isNOU(closest(element, '.' + CLS_POPUPCLASS))) {
                instance.popObj.hide({ name: 'FadeOut', duration: 100 });
            }
        }
    },
    setCssClass(dataId: string, cssClass: string): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.setCssClass(cssClass);
        }
    },
    setWidth(dataId: string, width: string): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.options.width = width;
            instance.extendedOpen();
            const wid: number = instance.element.offsetWidth;
            setStyle(instance.element, { 'width': formatUnit(width) });
            instance.renderOverflowMode();
            if (instance.popObj && wid < instance.element.offsetWidth) {
                instance.popupRefresh(instance.popObj.element, false);
            }
        }
    },
    setHeight(dataId: string, height: string): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.options.height = height;
            instance.extendedOpen();
            setStyle(instance.element, { 'height': formatUnit(height) });
        }
    },
    setOverflowMode(dataId: string, overflowMode: OverflowMode) {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.options.overflowMode = overflowMode;
            instance.extendedOpen();
            instance.destroyMode();
            instance.renderOverflowMode();
            if (instance.options.enableRtl) {
                addClass([instance.element], CLS_RTL);
            }
            instance.refreshOverflow();
        }
    },
    setEnableRTL(dataId: string, enableRtl: boolean): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.options.enableRtl = enableRtl;
            instance.extendedOpen();
            if (enableRtl) {
                addClass([instance.element], CLS_RTL);
            } else {
                removeClass([instance.element], CLS_RTL);
            }
            if (!isNOU(instance.scrollModule)) {
                if (enableRtl) {
                    addClass([instance.scrollModule.element], CLS_RTL);
                } else {
                    removeClass([instance.scrollModule.element], CLS_RTL);
                }
            }
            if (!isNOU(instance.popObj)) {
                if (enableRtl) {
                    addClass([instance.popObj.element], CLS_RTL);
                } else {
                    removeClass([instance.popObj.element], CLS_RTL);
                }
            }
            if (instance.tbarAlign) { instance.itemPositioning(); }
        }
    },
    setScrollStep(dataId: string, scrollStep: number): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.options.scrollStep = scrollStep;
            instance.extendedOpen();
            if (instance.scrollModule) {
                instance.scrollModule.scrollStep = scrollStep;
            }
        }
    },
    setEnableCollision(dataId: string, enableCollision: boolean): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.options.enableCollision = enableCollision;
            instance.extendedOpen();
            if (instance.popObj) {
                instance.popObj.collision = { Y: enableCollision ? 'flip' : 'none' };
            }
        }
    },
    setAllowKeyboard(dataId: string, allowKeyboard: boolean): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.options.allowKeyboard = allowKeyboard;
            instance.extendedOpen();
            instance.unwireKeyboardEvent();
            if (allowKeyboard) {
                instance.wireKeyboardEvent();
            }
        }
    },
    serverItemsRerender(dataId: string, items: ItemModel[], firstRender: boolean): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            const isSpacer = instance.element.querySelector('.' + CLS_ITEMS + ' ' + '.' + CLS_SPACER);
            if (!isNOU(isSpacer) && !instance.element.classList.contains('e-spacer-toolbar')) {
                instance.element.classList.add('e-spacer-toolbar');
            }
            const docActive: HTMLElement = document.activeElement as HTMLElement;
            instance.options.items = items;
            instance.extendedOpen();
            instance.destroyMode();
            instance.resetServerItems(firstRender);
            instance.serverItemsRefresh(firstRender);
            if (document.activeElement !== docActive) {
                docActive.focus();
            }
        }
    },
    hideItem(dataId: string, items: ItemModel[], eleIndex: number): void {
        if (isNOU(eleIndex)) {
            return;
        }
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.options.items = items;
            const innerItems: HTEle[] = [].slice.call(selectAll('.' + CLS_ITEM, this.element));
            const ele: HTMLElement = innerItems[parseInt(eleIndex.toString(), 10)];
            if(ele){
                if (!items[parseInt(eleIndex.toString(), 10)].visible) {
                    if (!instance.nonFocusableElements(ele)) {
                        if (isNOU(ele.firstElementChild.getAttribute('tabindex')) ||
                            ele.firstElementChild.getAttribute('tabindex') !== '-1') {
                            instance.updateHideEleTabIndex(ele, items[parseInt(eleIndex.toString(), 10)].visible, eleIndex, innerItems);
                        }
                    }
                } else {
                    if (!instance.nonFocusableElements(ele)) {
                        instance.updateHideEleTabIndex(ele, items[parseInt(eleIndex.toString(), 10)].visible, eleIndex, innerItems);
                    }
                }
            }
            instance.refreshOverflow();
        }
    },
    disable(dataId: string, value: boolean): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.disable(value);
        }
    },
    refreshOverflow(dataId: string): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.refreshOverflow();
        }
    },
    destroy(dataId: string): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.destroy();
        }
    },
    refresh(dataId: string, options: IToolbarOptions): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (options.scrollStep === 0) {
            options.scrollStep = null;
        }
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.options = options;
            instance.destroyMode();
            instance.resetServerItems(false);
            instance.serverItemsRefresh(false);
        }
    }
};
export default Toolbar;
