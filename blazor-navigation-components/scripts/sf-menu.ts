/* eslint-disable @typescript-eslint/tslint/config */
import { BlazorDotnetObject, closest, EventHandler, select, selectAll, createElement, getInstance, Animation, AnimationOptions } from '@syncfusion/ej2-base';
import { isNullOrUndefined, Browser } from '@syncfusion/ej2-base';
import { getZindexPartial } from '@syncfusion/ej2-popups';
import { keyActionHandler, MenuAnimationSettingsModel } from './menu-base';
import { addScrolling, destroyScroll } from './common/menu-scroll';
import { VScroll } from './common/v-scroll';
import { HScroll } from './common/h-scroll';

const CONTAINER: string = 'e-menu-container';
const MENUCLASS: string = '.e-menu';
const MENUITEM: string = 'e-menu-item';
const FOCUSED: string = '.e-focused';
const SELECTED: string = '.e-selected';
const MENU: string = '.e-ul';
const MOUSEDOWNHANDLER: string = 'DocumentMouseDownAsync';
const PIXEL: string = 'px';
const MOUSEDOWN: string = 'mousedown touchstart';
const MOUSEOVER: string = 'mouseover';
const CLICK: string = 'click';
const RESIZE: string = 'resize';
const HASH: string = '#';
const EMPTY: string = '';
const DOT: string = '.';
const MENUPARENT: string = 'e-menu-parent';
const MENUCARET: string = 'e-menu-caret-icon';
const KEYDOWN: string = 'keydown';
const HAMBURGER: string = 'e-hamburger';
const VERTICAL: string = 'e-vertical';
const VSCROLL: string = 'vscroll';
const HSCROLL: string = 'hscroll';
const SCROLLMENU: string = 'e-menu-';
const SCROLLNAV: string = '.e-scroll-nav';
const NONE: string = 'none';
const SCROLL: string = 'scroll';
const LEFT: number = 37;
const RIGHT: number = 39;
const UP: number = 38;
const DOWN: number = 40;
let SUBMENUITEM: MenuOptions;

/**
 * Client side scripts for SfMenu
 */
class SfMenu {
    private element: HTMLElement;
    private popup: HTMLElement;
    private dotnetRef: BlazorDotnetObject;
    private enableScroller: boolean;
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    private popupInstance: any;
    private dataId: string;
    private isTabFocused: boolean;
    private eventData: EventOptions[];
    private animation: Animation = new Animation({});
    private animationSettings: MenuAnimationSettingsModel;
    private animationElement: HTMLElement;
    private isShowItemOnClick: boolean;

    constructor(options: MenuOptions, dotnetRef: BlazorDotnetObject) {
        this.dataId = options.dataId;
        this.element = options.element;
        this.dotnetRef = dotnetRef;
        this.enableScroller = options.enableScrolling;
        this.animationSettings = options.animationSettings;
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        (window as any).sfBlazor.setCompInstance(this);
        this.addEventListener();
        this.updateScroll(options.enableScrolling, options.isRtl, false);
    }

    public calculatePosition(args: MenuOptions, enterKey?: boolean, customLeft?: number, customTop?: number): void {
        this.isShowItemOnClick = args.showItemOnClick;
        SUBMENUITEM = args;
        let left: number; let top: number;
        const parent: HTMLElement = <HTMLElement>args.element.getElementsByClassName(MENUITEM)[args.itemIndex];
        const offset: ClientRect = parent.getBoundingClientRect();
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        this.popupInstance = (window as any).sfBlazor.getCompInstance(args.popupDataId);
        let menu: HTMLElement = this.popupInstance.hideMenu(true);
        if (!menu) { return; }
        this.popup = args.popup;
        if (args.enableScrolling) {
            menu = this.positionHelper(args, menu);
            const scrollElement: Element = this.popup.querySelector('.e-vscroll-bar');
            if (scrollElement) {
                EventHandler.add(scrollElement, SCROLL, this.scrollHandler, this);
            }
        }
        const ul: HTMLElement = menu.classList.contains(MENUPARENT) ? menu : select(DOT + MENUPARENT, menu);
        this.popupInstance.setBlankIconStyle(ul, args.isRtl);
        const menuOffset: ClientRect = ul.getBoundingClientRect();
        const width: number = this.popupInstance.getMenuWidth(ul, menuOffset.width, args.isRtl);
        if (args.isVertical) {
            top = offset.top;
            if (customTop) { top = customTop; }
            if (args.isRtl) {
                left = offset.left;
                if (left - width < document.documentElement.clientLeft) {
                    const newLeft: number = offset.right + width;
                    if (newLeft < document.documentElement.clientWidth) { left = newLeft; }
                }
            } else {
                left = offset.right;
                if (customLeft) { left = customLeft; }
                if (left + width > document.documentElement.clientWidth) {
                    const newLeft: number = offset.left - width;
                    if (newLeft > document.documentElement.clientLeft) { left = newLeft; }
                }
            }
        } else {
            top = offset.bottom;
            if (customTop) { top = customTop; }
            if (args.isRtl) {
                left = offset.right;
                if (offset.right - width < document.documentElement.clientLeft) {
                    const newLeft: number = offset.left + width;
                    if (newLeft < document.documentElement.clientWidth) { left = newLeft; }
                }
            } else {
                left = offset.left;
                if (customLeft) { left = customLeft; }
                if (left + width > document.documentElement.clientWidth) {
                    const newLeft: number = offset.right - width;
                    if (newLeft > document.documentElement.clientLeft) { left = newLeft; }
                }
            }
        }
        const height: number = args.scrollHeight || menuOffset.height;
        if (top + height > document.documentElement.clientHeight) {
            const targetHeight: number = parent ? parent.getBoundingClientRect().top : document.documentElement.clientHeight;
            const newTop: number = targetHeight - height - 1;
            if (newTop > document.documentElement.clientTop) { 
                const scrollTop: number = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0;
                top = newTop + scrollTop;
            }
        }
        this.popupInstance.updateProperty(args.showItemOnClick, args.element);
        this.popup.style.zIndex = getZindexPartial(this.popup).toString();
        menu.style.width = Math.ceil(width) + PIXEL;
        this.popup.style.left = Math.ceil(left) + PIXEL;
        this.popup.style.top = Math.ceil(top) + PIXEL;
        ul.style.visibility = EMPTY;
        if (enterKey) {
            ul.focus();
        }
    }

    public subMenuPosition(args: MenuOptions, enterKey?: boolean): void {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const popupInstance: any = (window as any).sfBlazor.getCompInstance(args.popupDataId);
        let menu: HTMLElement = popupInstance.hideMenu();
        if (args.enableScrolling && menu) {
            menu = this.positionHelper(args, menu);
        }
        popupInstance.subMenuPosition(menu, args.isRtl, args.showItemOnClick, false, args.scrollHeight, false, enterKey);
    }
    private positionHelper(args: MenuOptions, menu: HTMLElement): HTMLElement {
        this.destroyScroll(null, menu);
        return args.scrollHeight ? addScrolling(createElement, args.popup, menu, VSCROLL, args.isRtl, args.scrollHeight) : menu;
    }
    private clickHandler(e: MouseEvent & TouchEvent): void {
        const target: Element = e.target as Element;
        if (!isNullOrUndefined(this.popup) && closest(target, HASH + this.popup.id)) {
            const li: Element = closest(target, DOT + MENUITEM);
            if (li && !li.classList.contains(MENUCARET)) {
                if (!closest(target, DOT + MENUITEM + SELECTED)) { this.destroyScroll(NONE); }
                const selectedLi: HTMLElement = select(SELECTED, this.element);
                if (selectedLi && (!closest(document.activeElement, HASH + this.popup.id) || document.activeElement.tagName === 'LI')) { selectedLi.focus(); }
            }
        } else if (closest(target, HASH + this.element.id + DOT + HAMBURGER) && !target.classList.contains(MENUPARENT) &&
            !target.classList.contains(CONTAINER)) {
            const li: Element = target.classList.contains(MENUITEM) ? target : closest(target, DOT + MENUITEM + FOCUSED);
            if (li && !li.classList.contains(MENUCARET)) { this.focusMenu(true); }
        }
    }

    private resizeMenu(): void {
        if (this.enableScroller) {
            const menuElement: Element = this.element.parentElement.querySelectorAll('.e-menu-container')[0];
            const ulElement: Element = menuElement.getElementsByClassName('e-menu-parent')[0];
            const scrollElement: HTMLElement = select('.e-menu-hscroll', menuElement);
            const menuWidth: number = ulElement && (ulElement as HTMLElement).offsetWidth;
            const leftWidth: number = window.innerWidth - ulElement.getBoundingClientRect().left;
            if (menuWidth > leftWidth) {
                (menuElement as HTMLElement).style.width = Math.ceil(leftWidth) + 'px';
                if (!scrollElement) {
                    const menu: HTMLElement = select('.e-menu', menuElement);
                    if (menu) {
                        addScrolling(createElement, menuElement as HTMLElement, menu, 'hscroll', false);
                    }
                }
            }
            else {
                if (scrollElement) {
                    // eslint-disable-next-line max-len
                    const scrollInstance: VScroll | HScroll = (this.element.classList.contains(VERTICAL) ? getInstance(scrollElement, VScroll) :
                        getInstance(scrollElement, HScroll)) as VScroll | HScroll;
                    destroyScroll(scrollInstance, scrollElement);
                    (menuElement as HTMLElement).style.width = '';
                }
            }
        }
    }

    private scrollHandler(): void {
        const scrollEle: Element = this.popup.querySelector('.e-menu-parent');
        if (scrollEle != null && scrollEle.classList.contains('e-transparent')) {
            scrollEle.classList.remove('e-transparent');
        }
    }

    private keyDownHandler(e: KeyboardEvent): void {
        (this.element.children[0] as HTMLElement).tabIndex = -1;
        if (e.keyCode !== 9 && !e.shiftKey) { e.preventDefault(); }
        if (this.element.classList.contains(HAMBURGER)) {
            keyActionHandler(this.element, e.target as Element, e.keyCode);
        } else {
            const isVertical: boolean = select(MENUCLASS, this.element).classList.contains(VERTICAL);
            if (isVertical) {
                if (e.keyCode === UP || e.keyCode === DOWN) {
                    keyActionHandler(this.element, e.target as Element, e.keyCode);
                }
            } else {
                if (e.keyCode === LEFT || e.keyCode === RIGHT) {
                    keyActionHandler(this.element, e.target as Element, e.keyCode === LEFT ? UP : DOWN);
                }
            }
        }
        if (this.isTabFocused) { (e.target as Element).classList.remove('e-focused'); this.isTabFocused = false; }
        if (e.keyCode === 9 || e.keyCode === 9 && e.shiftKey) {
            (e.target as HTMLElement).blur();
            // eslint-disable-next-line @typescript-eslint/no-explicit-any, @typescript-eslint/no-this-alias
            const proxy: any = this;
            setTimeout( function () { (proxy.element.children[0] as HTMLElement).tabIndex = 0; }, 5);
        }
    }

    private focusHandler(e: FocusEvent): void {
        if (e.relatedTarget) {
            const focusedLi: Element = (e.target as Element).querySelector(`${DOT}${MENUITEM}${FOCUSED}`);
            if (!focusedLi) {
                (this.element.children[0] as HTMLElement).tabIndex = -1;
                ((e.target as Element).children[0] as HTMLElement).focus();
                ((e.target as Element).children[0] as HTMLElement).classList.add('e-focused');
                this.isTabFocused = true;
            }
        }
    }

    private docKeyDownHandler(e: KeyboardEvent): void {
        const isDown: boolean =  e.key ? (e.key === 'ArrowDown') : e.keyCode === DOWN;
        if (!isDown) { return; }
        const hostPopup: Element = closest(this.element, '.e-popup-open');
        if (!hostPopup) { return; }
        if (closest(document.activeElement as Element, `#${this.element.id}`)) { return; }
        const active = document.activeElement as HTMLElement;
        if (!active) { return; }
        const expanded = active.getAttribute('aria-expanded') === 'true';
        if (!expanded) { return; }
        const ul: HTMLElement = select(DOT + MENUPARENT, this.element);
        if (!ul) { return; }
        ul.focus();
    }

    public focusMenu(first: boolean): void {
        if (select(DOT + SCROLLMENU + VSCROLL, this.popup)) {
            this.destroyScroll(EMPTY);
            const menu: HTMLElement = this.popupInstance.getLastMenu();
            if (menu) { menu.focus(); }
            return;
        }
        const menuCollections: HTMLElement[] = selectAll(DOT + MENUPARENT, this.element);
        if (menuCollections.length) {
            if (first) {
                menuCollections[0].focus();
            } else {
                const focusedEle: HTMLElement = select(DOT + MENUITEM + FOCUSED, menuCollections[menuCollections.length - 1]);
                let menu: HTMLElement;
                if (focusedEle) {
                    focusedEle.focus();
                    menu = focusedEle;
                } else {
                    menuCollections[menuCollections.length - 1].focus();
                    menu = menuCollections[menuCollections.length - 1];
                }
                if (this.animationSettings != null && menu.classList.contains('e-ul')) {
                    this.toggleMenuAnimation(menu, this.animationSettings);
                }

            }
        }
    }

    public destroyScroll(display: string, curMenu?: HTMLElement, isVisible?: boolean): void {
        const scrollElements: HTMLElement[] = selectAll(DOT + SCROLLMENU + VSCROLL, this.popup);
        const menus: HTMLElement[] = [].slice.call(selectAll(DOT + MENUPARENT, this.popup));
        let menu: HTMLElement; let index: number = -1;
        if (!isNullOrUndefined(display) && curMenu) { index = menus.indexOf(curMenu); }
        scrollElements.forEach((element: HTMLElement): void => {
            menu = null; menu = select(MENU, element);
            if (isVisible || (menu && !isNullOrUndefined(display))) {
                if (curMenu) {
                    if (menus.indexOf(menu) > index) { element.style.display = display; }
                } else {
                    element.style.display = display;
                }
            } else {
                destroyScroll(getInstance(element, VScroll) as VScroll, element, curMenu);
            }
        });
    }

    private mouseDownHandler(e: MouseEvent): void {
        if (this.isShowItemOnClick && this.animationElement && closest(e.target as Element, HASH + this.element.id)) {
            Animation.stop(this.animationElement);
        }
        const target: Element = e.target as Element;
        if (this.isTabFocused) {
            const focusli: Element = this.element.querySelector(`${DOT}${MENUITEM}${FOCUSED}`);
            if (focusli) { focusli.classList.remove('e-focused'); this.isTabFocused = false; }
        }
        (this.element.children[0] as HTMLElement).tabIndex = 0;
        const isEleAvailable: boolean = !document.body.contains(this.element);
        if (isNullOrUndefined(this.element) || isEleAvailable) { this.removeEventListener(false); }
        const scrollNav: Element = closest(target, SCROLLNAV);
        if (isNullOrUndefined(this.popup) || (!closest(target, HASH + this.popup.id) || scrollNav)) {
            const menuLength: number = selectAll(MENU, this.element).length;
            if (!isEleAvailable && (select(FOCUSED, this.element) || select(SELECTED, this.element)) &&
                !closest(e.target as Element, HASH + this.element.id) && (!scrollNav || menuLength > 1)) {
                this.dotnetRef.invokeMethodAsync(MOUSEDOWNHANDLER, true, false, !isNullOrUndefined(scrollNav), false, false);
            }
            if (!isNullOrUndefined(this.popup) && !isNullOrUndefined(this.popupInstance) &&
                (!closest(e.target as Element, DOT + MENUITEM + SELECTED) || !this.popupInstance.subMenuOpen) && !scrollNav) {
                if (!this.popupInstance.subMenuOpen) {
                    const menu: HTMLElement = closest(e.target as Element, MENU) as HTMLElement;
                    if (select(DOT + SCROLLMENU + VSCROLL, this.popup)) { this.destroyScroll(NONE, menu); }
                } else {
                    this.destroyScroll(NONE);
                }
            }
        }
    }

    private mouseOverHandler(e: MouseEvent): void {
        const isEleAvailable: boolean = document.body.contains(this.element);
        if (isNullOrUndefined(this.element) || !isEleAvailable) { this.removeEventListener(false); }
        const li: Element = closest(e.target as Element, DOT + MENUITEM);
        if (this.popup && this.popupInstance.subMenuOpen && closest(e.target as Element, HASH + this.element.id) && li) {
            // eslint-disable-next-line radix
            if (!li.querySelector('.e-caret') || (parseInt(this.popup.style.top) !== Math.ceil((e.target as Element).getBoundingClientRect().top))) {
                this.destroyScroll(NONE, closest(e.target as Element, MENU) as HTMLElement, true);
                if (this.enableScroller && SUBMENUITEM) {
                    this.calculatePosition(SUBMENUITEM);
                }
            }
        }
        if (isEleAvailable && select(FOCUSED, this.element) && !closest(e.target as Element, HASH + this.element.id)) {
            this.dotnetRef.invokeMethodAsync(MOUSEDOWNHANDLER, false, false, false, true, true);
        }
    }

    public updateScroll(enableScrolling: boolean, isRtl: boolean, destroy: boolean): void {
        if (enableScrolling) {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            EventHandler.add(window as any, RESIZE, this.resizeMenu, this);
            const menu: HTMLElement = select(MENUCLASS, this.element);
            if (menu && isNullOrUndefined(this.element.querySelector('.e-menu-hscroll'))) {
                addScrolling(createElement, this.element, menu, menu.classList.contains(VERTICAL) ? VSCROLL : HSCROLL, isRtl);
            }
        } else if (destroy) {
            const scrollElement: HTMLElement = select(
                DOT + SCROLLMENU + (this.element.classList.contains(VERTICAL) ? VSCROLL : HSCROLL), this.element);
            if (scrollElement) {
                // eslint-disable-next-line @typescript-eslint/no-explicit-any
                EventHandler.remove(window as any, RESIZE, this.resizeMenu);
                const scrollInstance: VScroll | HScroll = (this.element.classList.contains(VERTICAL) ? getInstance(scrollElement, VScroll) :
                    getInstance(scrollElement, HScroll)) as VScroll | HScroll;
                destroyScroll(scrollInstance, scrollElement);
            }
        }
    }

    public orientationScroll(enableScrolling: boolean, isRtl: boolean): void {
        EventHandler.remove(window as any, RESIZE, this.resizeMenu);
        const vScrollElement: HTMLElement = select(DOT + SCROLLMENU + VSCROLL, this.element);
        if (vScrollElement) {
            const vInstance: VScroll = getInstance(vScrollElement, VScroll) as VScroll;
            destroyScroll(vInstance, vScrollElement);
        }
        const hScrollElement: HTMLElement = select(DOT + SCROLLMENU + HSCROLL, this.element);
        if (hScrollElement) {
            const hInstance: HScroll = getInstance(hScrollElement, HScroll) as HScroll;
            destroyScroll(hInstance, hScrollElement);
        }
        if (!enableScrolling) { return; }
        EventHandler.add(window as any, RESIZE, this.resizeMenu, this);
        const menu: HTMLElement = select(MENUCLASS, this.element);
        if (!menu) { return; }
        addScrolling(createElement, this.element, menu, menu.classList.contains(VERTICAL) ? VSCROLL : HSCROLL, isRtl);
    }

    private removeDocEvent(element: Element | HTMLElement | Document, eventName: string, listener: Function, id: string): void {
        let index: number = -1; let debounceListener: Function;
        const events: string[] = eventName.split(' ');
        for (let i: number = 0; i < events.length; i++) {
            for (let j: number = this.eventData.length - 1; j >= 0; j--) {
                // eslint-disable-next-line max-len
                if (this.eventData[parseInt(j.toString(), 10)].name === events[parseInt(j.toString(), 10)] && this.eventData[parseInt(j.toString(), 10)].listener === listener && this.eventData[parseInt(j.toString(), 10)].id === id) {
                    index = j;
                    debounceListener = this.eventData[parseInt(j.toString(), 10)].debounce;
                }
            }
            if (index !== -1) {
                this.eventData.splice(index, 1);
            }
            if (debounceListener) {
                element.removeEventListener(events[parseInt(i.toString(), 10)], <EventListener>debounceListener);
            }
        }
    }

    private addEventListener(): void {
        EventHandler.add(this.element, KEYDOWN, this.keyDownHandler, this);
        EventHandler.add(this.element.children[0], 'focus', this.focusHandler, this);
        this.eventData = [];
        EventHandler.add(document, MOUSEDOWN, this.mouseDownHandler, this);
        EventHandler.add(document, MOUSEOVER, this.mouseOverHandler, this);
        EventHandler.add(document, CLICK, this.clickHandler, this);
        EventHandler.add(document, KEYDOWN, this.docKeyDownHandler, this);
    }

    public removeEventListener(isEleAvailable: boolean): void {
        if (isEleAvailable) {
            EventHandler.remove(this.element, KEYDOWN, this.keyDownHandler);
            EventHandler.remove(this.element.children[0], 'focus', this.focusHandler);
        }
        this.removeDocEvent(document, MOUSEDOWN, this.mouseDownHandler, this.element.id);
        this.removeDocEvent(document, MOUSEOVER, this.mouseOverHandler, this.element.id);
        this.removeDocEvent(document, CLICK, this.clickHandler, this.element.id);
        this.removeDocEvent(document, KEYDOWN, this.docKeyDownHandler, this.element.id);
    }

    private toggleMenuAnimation(ul: HTMLElement, animationSettings: MenuAnimationSettingsModel): void {
        if (animationSettings.effect !== 'None') {
            this.animationElement = ul;
            this.animation.animate(ul, {
                name: animationSettings.effect,
                duration: animationSettings.duration,
                timingFunction: animationSettings.easing,
                begin: (options: AnimationOptions) => {
                    if (options.element && options.element.parentElement) {
                        options.element.parentElement.style.height = options.element.offsetHeight + 'px';
                    }
                },
                end: (options: AnimationOptions) => {
                    if (options.element && options.element.parentElement) {
                        options.element.parentElement.style.height = '';
                    }
                }
            });
        }
    }
}

interface EventData extends Element {
    __eventList: EventList;
}

interface EventList {
    events?: EventOptions[];
}

interface EventOptions {
    name: string;
    listener: Function;
    debounce?: Function;
    id: string
}

interface MenuOptions {
    dataId: string;
    popupDataId: string;
    element: HTMLElement;
    popup: HTMLElement;
    itemIndex: number;
    isRtl: boolean;
    isVertical: boolean;
    showItemOnClick: boolean;
    enableScrolling: boolean;
    scrollHeight: number;
    animationSettings: MenuAnimationSettingsModel;
}

// tslint:disable-next-line:variable-name
const Menu: object = {
    initialize(args: MenuOptions, dotnetRef: BlazorDotnetObject): void {
        if (!isNullOrUndefined(args.element)) { new SfMenu(args, dotnetRef); }
    },
    calculatePosition(args: MenuOptions, enterKey?: boolean, customLeft?: number, customTop?: number): void {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const instance: any = (window as any).sfBlazor.getCompInstance(args.dataId);
        if (!isNullOrUndefined(instance)) {
            instance.calculatePosition(args, enterKey, customLeft, customTop);
            if (instance.animationSettings != null) {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
                const popupInstance = (window as any).sfBlazor.getCompInstance(args.popupDataId);
                let menu: HTMLElement = select(DOT + 'e-menu-parent', popupInstance.element);
                if (instance.enableScroller && menu && closest(menu, '.e-menu-vscroll')) {
                    menu = closest(menu, '.e-menu-vscroll') as HTMLElement;
                }
                instance.toggleMenuAnimation(menu, instance.animationSettings);
            }
        }
    },
    subMenuPosition(args: MenuOptions, enterKey?: boolean): void {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const instance: any = (window as any).sfBlazor.getCompInstance(args.dataId);
        if (!isNullOrUndefined(instance)) {
            if (instance.animationElement && instance.animationSettings) {
                Animation.stop(instance.animationElement);
            }
            instance.subMenuPosition(args, enterKey);
            if (instance.animationSettings != null) {
                // eslint-disable-next-line @typescript-eslint/no-explicit-any
                const popupInstance = (window as any).sfBlazor.getCompInstance(args.popupDataId);
                let cmenu: HTMLElement = popupInstance.hideMenu();
                cmenu.style.visibility = '';
                if (instance.enableScroller && cmenu && closest(cmenu, '.e-menu-vscroll')) {
                    cmenu = closest(cmenu, '.e-menu-vscroll') as HTMLElement;
                }
                instance.toggleMenuAnimation(cmenu, instance.animationSettings);
            }
        }
    },
    focusMenu(dataId: string): void {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNullOrUndefined(instance) && document.activeElement.tagName !== 'INPUT' && document.activeElement.className.indexOf('e-input') < 0) {
            instance.focusMenu(false);
        }
    },
    updateScroll(dataId: string, enableScrolling: boolean, isRtl: boolean): void {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNullOrUndefined(instance)) {
            instance.updateScroll(enableScrolling, isRtl, true);
        }
    },
    toggleAnimation: function (dataId: string) {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (instance && instance.animationSettings != null && instance.animationElement) {
            instance.toggleMenuAnimation(instance.animationElement, instance.animationSettings);
        }
    },
    orientationScroll(dataId: string, enableScrolling: boolean, isRtl: boolean): void {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNullOrUndefined(instance)) {
            instance.orientationScroll(enableScrolling, isRtl);
        }
    },
    destroy(dataId: string): void {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNullOrUndefined(instance)) {
            instance.removeEventListener(true);
        }
    }
};

export default Menu;
