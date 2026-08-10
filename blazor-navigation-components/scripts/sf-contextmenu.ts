/* eslint-disable max-len */
/* eslint-disable @typescript-eslint/tslint/config */
import { BlazorDotnetObject, closest, MouseEventArgs, EventHandler, Browser, Touch, TapEventArgs, Animation, AnimationOptions } from '@syncfusion/ej2-base';
import { isNullOrUndefined, getInstance, select, selectAll, createElement } from '@syncfusion/ej2-base';
import { getZindexPartial, getScrollableParent } from '@syncfusion/ej2-popups';
import { keyActionHandler, MenuAnimationSettingsModel } from './menu-base';
import { addScrolling, destroyScroll } from './common/menu-scroll';
import { VScroll } from './common/v-scroll';

const TRANSPARENT: string = 'e-transparent';
const MENU: string = 'e-menu-parent';
const MENUITEM: string = 'e-menu-item';
const FOCUSED: string = 'e-focused';
const SELECTED: string = 'e-selected';
const CLOSE: string = 'CloseMenuAsync';
const KEYDOWN: string = 'keydown';
const SCROLLMENU: string = '.e-menu-vscroll';
const SCROLLNAV: string = '.e-scroll-nav';
const SPACE: string = ' ';
const HIDDEN: string = 'hidden';
const OPENMENU: string = 'OpenContextMenuAsync';
const PIXEL: string = 'px';
const MOUSEDOWN: string = 'mousedown touchstart';
const MOUSEOVER: string = 'mouseover';
const SCROLL: string = 'scroll';
const NONE: string = 'none';
const HASH: string = '#';
const EMPTY: string = '';
const DOT: string = '.';
const TARGET: string = 'Target';
const FILTER: string = 'Filter';
const SHOWON: string = 'ShowOn';
const CARET: string = 'e-caret';

/**
 * Client side scripts for Blazor context menu
 */
class SfContextMenu {
    private element: HTMLElement;
    private target: string;
    private filter: string;
    private showOn: string;
    private closeOn: string;
    private subMenuOpen: boolean;
    private menuId: string;
    private targetElement: HTMLElement;
    private openAsMenu: boolean;
    private dotnetRef: BlazorDotnetObject;
    private delegateMouseDownHandler: Function;
    private delegateMouseOverHandler: Function;
    private animation: Animation = new Animation({});
    private animationSettings: MenuAnimationSettingsModel;
    private animationElement: HTMLElement;
    //eslint-disable-next-line @typescript-eslint/no-explicit-any
    private cmTarget: any;
    //eslint-disable-next-line @typescript-eslint/no-explicit-any
    private menuTarget: any;
    private dataId: string;
    private enableScrolling: boolean;
    private isShowItemOnClick: boolean;

    constructor(dataId: string, element: HTMLElement, target: string, filter: string, showOn: string, closeOn: string, enableScrolling: boolean, dotnetRef: BlazorDotnetObject, animationSettings: MenuAnimationSettingsModel) {
        this.dataId = dataId;
        this.element = element;
        this.target = target;
        this.filter = filter;
        this.showOn = showOn;
        this.closeOn = closeOn;
        this.dotnetRef = dotnetRef;
        this.enableScrolling = enableScrolling;
        this.animationSettings = animationSettings;
        //eslint-disable-next-line @typescript-eslint/no-explicit-any
        (window as any).sfBlazor.setCompInstance(this);
        this.addContextMenuEvent();
        this.addEventListener();
    }
    private addContextMenuEvent(add: boolean = true): void {
        let target: HTMLElement;
        if (this.target) {
            const targetElems: HTMLElement[] = selectAll(this.target);
            if (targetElems.length) {
                for (let i: number = 0, len: number = targetElems.length; i < len; i++) {
                    target = targetElems[parseInt(i.toString(), 10)];
                    if (add) {
                        if (Browser.isIos) {
                            new Touch(target, { tapHold: this.touchHandler.bind(this) });
                        } else {
                            EventHandler.add(target, this.showOn, this.cmenuHandler, this);
                        }
                    } else {
                        if (Browser.isIos) {
                            const touchModule: Touch = getInstance(target, Touch) as Touch;
                            if (touchModule) { touchModule.destroy(); }
                        } else {
                            EventHandler.remove(target, this.showOn, this.cmenuHandler);
                        }
                    }
                }
                if (isNullOrUndefined(this.targetElement)) { this.targetElement = target; }
                if (add) {
                    EventHandler.add(this.targetElement, SCROLL, this.scrollHandler, this);
                    for (const parent of getScrollableParent(this.targetElement)) {
                        EventHandler.add(parent, SCROLL, this.scrollHandler, this);
                    }
                } else {
                    let scrollableParents: HTMLElement[];
                    if (this.targetElement.parentElement) {
                        EventHandler.remove(this.targetElement, SCROLL, this.scrollHandler);
                        scrollableParents = getScrollableParent(this.targetElement);
                    } else {
                        scrollableParents = getScrollableParent(target);
                    }
                    for (const parent of scrollableParents) {
                        EventHandler.remove(parent, SCROLL, this.scrollHandler);
                    }
                    this.targetElement = null;
                }
            }
        }
    }
    private scrollHandler(): void {
        if (select(DOT + MENU, this.element)) {
            this.dotnetRef.invokeMethodAsync(CLOSE, 0, false, true, false);
            if (this.enableScrolling) {
                //eslint-disable-next-line @typescript-eslint/no-explicit-any, @typescript-eslint/no-this-alias
                const proxy: any = this;
                setTimeout(function () { proxy.destroyScroll(); }, 100);
            }
        }
    }
    private vscrollHandler(): void {
        const scrollEle: HTMLElement = this.element.querySelector('.e-menu-parent');
        if (scrollEle != null && scrollEle.classList.contains('e-transparent')) {
            scrollEle.classList.remove('e-transparent');
        }
    }
    private touchHandler(e: TapEventArgs): void {
        this.cmenuHandler(e.originalEvent);
    }
    private keyDownHandler(e: KeyboardEvent): void {
        const classList: DOMTokenList = (e.target as HTMLElement).classList;
        if (classList.contains(MENUITEM) || classList.contains(MENU)) {
            e.preventDefault();
        }
        keyActionHandler(this.element, e.target as Element, e.keyCode, this.menuId);
    }
    private cmenuHandler(e: MouseEventArgs): void {
        this.cmTarget = e.target;
        if (this.filter) {
            let canOpen: boolean = false;
            const filter: string[] = this.filter.split(SPACE);
            for (let i: number = 0, len: number = filter.length; i < len; i++) {
                if (closest(e.target as Element, filter[parseInt(i.toString(), 10)])) { canOpen = true; break; }
            }
            if (!canOpen) { return; }
        }
        e.preventDefault();
        e.stopPropagation();
        let left: number = e.changedTouches ? e.changedTouches[0].clientX : e.clientX;
        let top: number = e.changedTouches ? e.changedTouches[0].clientY : e.clientY;
        if (this.showOn === 'mouseover') {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const rect: any = this.cmTarget.getBoundingClientRect();
            if (rect.right > left && left > rect.right - 5) {
                left = left - 10;
            }
            if (rect.bottom > top && top > rect.bottom - 5) {
                top = top - 10;
            }
        }
        if (this.closeOn !== MOUSEDOWN) {
            EventHandler.remove(e.target as HTMLElement, this.showOn, this.cmenuHandler);
            if (this.closeOn === 'mouseleave') {
                EventHandler.add(e.target as HTMLElement, this.closeOn as string, this.mouseLeaveHandler, this);
            } else {
                EventHandler.add(document, this.closeOn as string, this.delegateMouseDownHandler, this);
            }
        }
        this.dotnetRef.invokeMethodAsync(OPENMENU, Math.ceil(left), Math.ceil(top), this.cmTarget.id);
    }
    private mouseLeaveHandler(e: MouseEvent, isMouseOver?: boolean): void {
        const rect: DOMRect = this.cmTarget.getBoundingClientRect() as DOMRect;
        const top: boolean = rect.top < e.clientY && rect.bottom - 3 > e.clientY;
        const left: boolean = rect.left < e.clientX && rect.right > e.clientX;
        if (!left || !top || isMouseOver) {
            this.dotnetRef.invokeMethodAsync(CLOSE, 0, false, true, false);
            if (this.enableScrolling) {
                // eslint-disable-next-line @typescript-eslint/no-explicit-any, @typescript-eslint/no-this-alias
                const proxy: any = this;
                setTimeout(() => { proxy.destroyScroll(); }, 100);
            }
            EventHandler.remove(this.cmTarget, this.closeOn as string, this.mouseLeaveHandler);
            EventHandler.add(this.cmTarget as HTMLElement, this.showOn, this.cmenuHandler, this);
        }
    }

    public clickHandler(isUnwire: boolean) {
        if (isUnwire) {
            EventHandler.add(this.cmTarget as HTMLElement, this.showOn, this.cmenuHandler, this);
            if (this.closeOn === 'mouseleave') {
                EventHandler.remove(this.cmTarget, this.closeOn as string, this.mouseLeaveHandler);
            } else {
                EventHandler.remove(document, this.closeOn as string, this.delegateMouseDownHandler);
            }
        }
        if (this.enableScrolling) {
            this.destroyScroll(null, true);
        }
    }

    public contextMenuPosition(left: number, top: number, rtl: boolean, subMenu: boolean, isCollision: boolean, scrollHeight: number, isDevice?: boolean): void {
        let cmenu: HTMLElement = this.hideMenu(true);
        if (!cmenu) { return; }
        this.subMenuOpen =  false;
        this.setBlankIconStyle(cmenu, rtl);
        const cmenuOffset: ClientRect = cmenu.getBoundingClientRect();
        const cmenuWidth: number = this.getMenuWidth(cmenu, cmenuOffset.width, rtl);
        if (subMenu && isDevice) {
            cmenu.style.width = Math.ceil(cmenuWidth) + PIXEL;
            cmenu.style.visibility = EMPTY;
            return;
        }
        if (isCollision) {
            if (top + cmenuOffset.height > document.documentElement.clientHeight) {
                const newTop: number = document.documentElement.clientHeight - cmenuOffset.height - 20;
                if (newTop > document.documentElement.clientTop) { top = newTop; }
            }
            if (rtl) {
                if (left < cmenuWidth) {
                    left += cmenuWidth;
                }
            }
            else if (left + cmenuWidth > document.documentElement.clientWidth) {
                const newLeft: number = document.documentElement.clientWidth - cmenuWidth - 20;
                if (newLeft > document.documentElement.clientLeft) { left = newLeft; }
            }
        }
        cmenu = this.updateScroll(scrollHeight, cmenu);
        const ul: HTMLElement = cmenu.classList.contains('e-menu-parent') ? cmenu : select('.e-menu-parent', cmenu);
        this.element.style.top = Math.ceil(top + 1) + scrollY + PIXEL;
        this.element.style.left = Math.ceil(left + 1) + scrollX + PIXEL;
        cmenu.style.width = Math.ceil(cmenuWidth) + PIXEL;
        this.element.style.zIndex = getZindexPartial(this.element).toString();
        cmenu.style.visibility = EMPTY;
        ul.style.visibility = EMPTY;
        cmenu.focus();
    }
    private setBlankIconStyle(menu: HTMLElement, isRtl: boolean): void {
        const blankIconList: HTMLElement[] = [].slice.call(menu.getElementsByClassName('e-blankicon'));
        const cssProp: { padding: string, cssSelector: string, margin: string } =  isRtl ? { padding: 'paddingRight', cssSelector:
            'padding-right', margin: 'marginLeft' } : { padding: 'paddingLeft', cssSelector: 'padding-left', margin: 'marginRight' };
        [].slice.call(menu.querySelectorAll(
            '.e-menu-item[style*="' + cssProp.cssSelector + '"]:not(.e-blankicon)')).forEach((li: HTMLElement): void => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            (li.style as any)[cssProp.padding] = EMPTY;
        });
        if (!blankIconList.length) { return; }
        const iconLi: HTMLElement = menu.querySelector('.e-menu-item:not(.e-blankicon):not(.e-separator)') as HTMLElement;
        const icon: HTMLElement = iconLi && iconLi.querySelector('.e-menu-icon') as HTMLElement;
        if (!icon) { return; }
        const iconCssProps: CSSStyleDeclaration = getComputedStyle(icon);
        let iconSize: number = parseInt(iconCssProps.fontSize, 10);
        if (!!parseInt(iconCssProps.width, 10) && parseInt(iconCssProps.width, 10) > iconSize) {
            iconSize = parseInt(iconCssProps.width, 10);
        }
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const size: string = `${iconSize + parseInt((iconCssProps as any)[cssProp.margin], 10) + parseInt((getComputedStyle(iconLi) as any)[cssProp.padding], 10)}px`;
        blankIconList.forEach((li: HTMLElement): void => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            (li.style as any)[cssProp.padding] = size;
        });
    }
    public getMenuWidth(cmenu: Element, width: number, isRtl: boolean): number {
        const caretIcon: HTMLElement = cmenu.getElementsByClassName(CARET)[0] as HTMLElement;
        if (caretIcon) { width += parseInt(getComputedStyle(caretIcon)[isRtl ? 'marginRight' : 'marginLeft'], 10); }
        return width < 120 ? 120 : width;
    }
    private addEventListener(): void {
        this.delegateMouseDownHandler = this.mouseDownHandler.bind(this); this.delegateMouseOverHandler = this.mouseOverHandler.bind(this);
        if (this.closeOn === MOUSEDOWN) {
            EventHandler.add(document, MOUSEDOWN, this.delegateMouseDownHandler, this);
        }
        EventHandler.add(document, MOUSEOVER, this.delegateMouseOverHandler, this);
        EventHandler.add(this.element, KEYDOWN, this.keyDownHandler, this);
    }
    private removeEventListener(): void {
        EventHandler.remove(document, MOUSEDOWN, this.delegateMouseDownHandler);
        EventHandler.remove(document, MOUSEOVER, this.delegateMouseOverHandler);
        EventHandler.remove(this.element, KEYDOWN, this.keyDownHandler);
    }
    private mouseDownHandler(e: MouseEvent & TouchEvent): void {
        if (this.isShowItemOnClick && this.animationElement && closest(e.target as Element, HASH + this.element.id)) {
            Animation.stop(this.animationElement);
        }
        const target: HTMLElement = e.target as HTMLElement;
        if (target.tagName === 'DIV' || target.tagName === 'SPAN') {
            if (target.classList.contains('scroll') || target.classList.contains('arrow')) {
                return;
            }
        }
        if (!document.getElementById(this.element.id)) { this.removeEventListener(); return; }
        let closestElem: HTMLElement;
        if (/^\d+$/.test(this.element.id) || !this.element.id.includes('sfcontextmenu')) {
            closestElem = this.getClosest(e.target, this.element.id) as HTMLElement;
        } else {
            closestElem = closest(e.target as Element, HASH + this.element.id) as HTMLElement;
        }
        if (!closestElem && (isNullOrUndefined(this.menuId) ||
            !closest(e.target as Element, this.menuId)) && select(DOT + MENU, this.element)) {
            if (!closest(select(DOT + MENU, this.element), '.e-dropdown-popup')) {
                this.dotnetRef.invokeMethodAsync(CLOSE, 0, false, true, false);
            }
            if (this.enableScrolling) {
                // eslint-disable-next-line @typescript-eslint/no-explicit-any, @typescript-eslint/no-this-alias
                const proxy: any = this;
                setTimeout(() => { proxy.destroyScroll(); }, 100);
            }
            if (this.closeOn !== MOUSEDOWN) {
                EventHandler.remove(document, this.closeOn as string, this.delegateMouseDownHandler);
                EventHandler.add(this.cmTarget as HTMLElement, this.showOn, this.cmenuHandler, this);
            }
        }
    }
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    private getClosest(target: any, id: string): HTMLElement {
        let closestElement: HTMLElement = null;
        let currentElement: HTMLElement = target;
        while (currentElement) {
            if (currentElement.id === id) {
                closestElement = currentElement;
                break;
            }
            currentElement = currentElement.parentElement as HTMLElement;
        }
        return closestElement;
    }
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    private isHavingChild(target: any): boolean {
        if (target && target.tagName === 'LI' && target.className.indexOf('e-menu-item') < 0) {
            target = target.closest('.e-menu-item');
        }
        if (target &&  target.tagName === 'LI' && target.className.indexOf('e-menu-item') > -1 && target.className.indexOf('e-menu-caret-icon') > -1) {
            return true;
        }
        return false;
    }
    private mouseOverHandler(e: MouseEvent): void {
        let target: HTMLElement = e.target as HTMLElement;
        if (target.tagName === 'DIV' || target.tagName === 'SPAN') {
            if (target.className.indexOf('scroll') > -1 || target.className.indexOf('arrow') > -1) {
                return;
            }
        }
        if (!document.getElementById(this.element.id)) { this.removeEventListener(); return; }
        const menus: HTMLElement[] = [].slice.call(selectAll(DOT + MENU, this.element));
        if (!menus.length) { return; }
        const scrollNav: Element = closest(target, SCROLLNAV);
        if (this.enableScrolling && !this.isHavingChild(target)) {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any, @typescript-eslint/no-this-alias
            const proxy: any = this;
            setTimeout(function () {
                const subMenus: HTMLElement[] = [].slice.call(selectAll(SCROLLMENU, this.element));
                for (let i: number = 0; i < subMenus.length; i++) {
                    if (target && target.tagName === 'LI' && target.className.indexOf('e-menu-item') < 0) {
                        target = target.closest('.e-menu-item') as HTMLElement;
                    }
                    if (target && target.parentElement !== subMenus[parseInt(i.toString(), 10)].querySelectorAll('.e-menu-parent')[0] && i === subMenus.length - 1) {
                        proxy.destroyScroll(subMenus[parseInt(i.toString(), 10)]);
                    }
                }
            }, 100);
        }
        if (this.subMenuOpen && (menus.length > 1 || (!isNullOrUndefined(this.menuId) && !scrollNav))) {
            let closestEle: HTMLElement;
            if (/^\d+$/.test(this.element.id) || !this.element.id.includes('sfcontextmenu')) {
                closestEle = this.getClosest(target, this.element.id) as HTMLElement;
            } else {
                closestEle = closest(target, HASH + this.element.id) as HTMLElement;
            }
            if ((!closestEle && (isNullOrUndefined(this.menuId) || !closest(target, this.menuId))) ||
                scrollNav) {
                let index: number = 1;
                if (!isNullOrUndefined(this.menuId)) {
                    index = 0;
                    if (scrollNav) {
                        index = menus.indexOf(select(DOT + MENU, scrollNav.parentElement)) + 1;
                        if (index === menus.length) { return; }
                    }
                }
                this.dotnetRef.invokeMethodAsync(CLOSE, index, false, true, false);
                if (this.enableScrolling) {
                    // eslint-disable-next-line @typescript-eslint/no-explicit-any, @typescript-eslint/no-this-alias
                    const proxy: any = this;
                    setTimeout(() => {
                        for (let i: number = index; i < menus.length; i++) {
                            proxy.destroyScroll(menus[parseInt(i.toString(), 10)]);
                        }
                    }, 100);
                }
                if (this.closeOn === 'mouseleave') {
                    this.mouseLeaveHandler(e);
                    EventHandler.remove(this.menuTarget, this.closeOn as string, this.menuMouseLeave);
                }
                if (!isNullOrUndefined(this.menuId) && !closest(target, SCROLLNAV)) { this.destroyMenuScroll(null); }
            }
            let closestElem: HTMLElement;
            if (/^\d+$/.test(this.element.id) || !this.element.id.includes('sfcontextmenu')) {
                closestElem = this.getClosest(target, this.element.id) as HTMLElement;
            } else {
                closestElem = closest(target, HASH + this.element.id) as HTMLElement;
            }
            if (!isNullOrUndefined(this.menuId) && (closestElem || closest(target, this.menuId)) &&
                closest(target, DOT + MENUITEM) && !closest(target, DOT + SELECTED)) {
                this.destroyMenuScroll(closest(target, DOT + MENU));
            }
        } else if (this.closeOn === 'mouseleave' && target.classList.contains('e-menu-item')) {
            this.menuTarget = target.parentElement;
            EventHandler.add(this.menuTarget, this.closeOn, this.menuMouseLeave, this);
        }
        if (!this.openAsMenu) {
            const activeEle: Element = document.activeElement;
            let closestElem: HTMLElement;
            if (/^\d+$/.test(this.element.id) || !this.element.id.includes('sfcontextmenu')) {
                closestElem = this.getClosest(activeEle, this.element.id) as HTMLElement;
            } else {
                closestElem = closest(activeEle, `${HASH}${this.element.id}`) as HTMLElement;
            }
            if (!closestElem && menus.length && activeEle.tagName === 'BODY') {
                const lastChild: HTMLElement = this.getLastMenu();
                if (lastChild) { lastChild.focus(); }
            }
        }
    }
    private menuMouseLeave(e: MouseEvent): void {
        const rect: DOMRect = this.menuTarget.getBoundingClientRect() as DOMRect;
        const top: boolean = rect.top < e.clientY && rect.bottom > e.clientY;
        const left: boolean = rect.left < e.clientX && rect.right > e.clientX;
        if (!left || !top) {
            this.mouseLeaveHandler(e, true);
            EventHandler.remove(this.menuTarget, this.closeOn as string, this.menuMouseLeave);
        }
    }
    private destroyMenuScroll(menu: Element): void {
        if (!select(SCROLLMENU, this.element)) { return; }
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const instance: any = (window as any).sfBlazor.getCompInstance(this.dataId);
        if (!isNullOrUndefined(instance)) {
            instance.destroyScroll(NONE, menu);
        }
    }
    public hideMenu(first?: boolean): HTMLElement {
        let cMenu: HTMLElement;
        if (first) {
            cMenu = select(DOT + MENU, this.element);
            if (!cMenu || isNullOrUndefined(this.element.parentElement)) { return null; }
            if ((this.element.parentElement !== document.body && !this.element.parentElement.classList.contains('e-dropDown-button'))) { document.body.appendChild(this.element); }
        } else {
            const menus: HTMLElement[] = selectAll(DOT + MENU, this.element);
            if (menus.length < 2) { return null; }
            cMenu = menus[menus.length - 1];
        }
        cMenu.style.width = EMPTY;
        cMenu.style.visibility = HIDDEN;
        cMenu.classList.remove(TRANSPARENT);
        return cMenu;
    }

    public subMenuPosition(cmenu: HTMLElement, isRtl: boolean, showOnClick: boolean, isNull: boolean, scrollHeight?: number, isContextMenu?: boolean, enterKey?: boolean): void {
        if (!cmenu) { return; }
        const menus: HTMLElement[] = selectAll(DOT + MENU, this.element);
        const parentLi: Element = menus[menus.length - 2].querySelector(`.${MENUITEM}.${SELECTED}`);
        const parentOffset: ClientRect = parentLi.getBoundingClientRect();
        const containerOffset: ClientRect = this.element.getBoundingClientRect();
        const menu: HTMLElement = cmenu.classList.contains(MENU) ? cmenu : select(DOT + MENU, cmenu);
        this.setBlankIconStyle(menu, isRtl);
        const curUlOffset: ClientRect = menu.getBoundingClientRect();
        const cmenuWidth: number = this.getMenuWidth(menu, curUlOffset.width, isRtl);
        let left: number; let borderLeft: number;
        if (isRtl) {
            borderLeft = parseInt(getComputedStyle(menu).borderWidth, 10);
            left = parentOffset.left - cmenuWidth - containerOffset.left;
        } else if (this.closeOn === 'mouseleave') {
            left = parentOffset.right - containerOffset.left - 10;
        }
        else {
            left = parentOffset.right - containerOffset.left;
        }
        let top: number = parentOffset.top - containerOffset.top;
        if (isRtl) {
            if (parentOffset.left - borderLeft - cmenuWidth < document.documentElement.clientLeft) {
                if (parentOffset.right + cmenuWidth < document.documentElement.clientWidth) {
                    left = parentOffset.right - containerOffset.left;
                }
            }
        } else if (parentOffset.right + cmenuWidth > document.documentElement.clientWidth) {
            const newLeft: number = parentOffset.left - cmenuWidth;
            if (newLeft > document.documentElement.clientLeft) {
                left = newLeft - containerOffset.left;
                if (this.closeOn === 'mouseleave') {
                    left = newLeft - containerOffset.left + 10;
                }
            }
        }
        const height: number = scrollHeight || curUlOffset.height;
        if (parentOffset.top + height > document.documentElement.clientHeight) {
            const newTop: number = document.documentElement.clientHeight - height - 20;
            if (newTop > document.documentElement.clientTop) {
                top = newTop - containerOffset.top;
            }
        }
        if (isContextMenu) {
            cmenu = this.updateScroll(scrollHeight, cmenu);
            if (cmenu.className.indexOf('scroll') > -1) {
                menu.style.left = 0 + PIXEL;
                menu.style.top = 0 + PIXEL;
            }
        }
        this.subMenuOpen = !showOnClick;
        cmenu.style.left = Math.ceil(left) + PIXEL;
        cmenu.style.top = Math.ceil(top) + PIXEL;
        cmenu.style.width = Math.ceil(cmenuWidth) + PIXEL;
        menu.style.visibility = EMPTY;
        const focusedLi: HTMLElement = menu.querySelector(`${DOT}${MENUITEM}${DOT}${FOCUSED}`) as HTMLElement;
        if (focusedLi) {
            focusedLi.focus();
        } else if (enterKey) {
            menu.focus();
        }
        if (isNull) { this.openAsMenu = true; }
    }
    private updateScroll(scrollHeight: number, menu: HTMLElement): HTMLElement {
        if (this.enableScrolling) {
            this.destroyScroll(menu);
            if (scrollHeight > 0 && menu) {
                menu = addScrolling(createElement, this.element, menu, 'vscroll', false, scrollHeight);
            }
            const scrollElement: HTMLElement = this.element.querySelector('.e-vscroll-bar');
            if (scrollElement) {
                EventHandler.add(scrollElement, 'scroll', this.vscrollHandler, this);
            }
        }
        return menu;
    }

    private destroyScroll(curMenu?: HTMLElement, isParent?: boolean): void {
        const scrollElements: HTMLElement[] = selectAll('.e-menu-vscroll', this.element);
        scrollElements.forEach(function (element: HTMLElement) {
            if ((isParent && !element.querySelector('ul')) || !isParent) {
                destroyScroll(getInstance(element, VScroll) as VScroll, element, curMenu);
            }
        });
    }

    public getLastMenu(): HTMLElement {
        const menus: HTMLElement[] = selectAll(DOT + MENU, this.element);
        return menus.length ? menus[menus.length - 1] : null;
    }
    public onPropertyChanged(key: string, result: string): void {
        switch (key) {
        case TARGET:
            this.addContextMenuEvent(false);
            this.target = result;
            this.addContextMenuEvent();
            break;
        case FILTER:
            this.filter = result;
            break;
        case SHOWON:
            this.addContextMenuEvent(false);
            this.showOn = result;
            this.addContextMenuEvent();
            break;
        }
    }
    public destroy(refElement: HTMLElement): void {
        this.removeEventListener();
        this.addContextMenuEvent(false);
        if (refElement && refElement.parentElement && refElement.previousElementSibling !== this.element) {
            refElement.parentElement.insertBefore(this.element, refElement);
        }
        if ((!refElement || !refElement.parentElement) && this.element.parentElement && this.element.parentElement === document.body) {
            document.body.removeChild(this.element);
        }
    }
    public updateProperty(showItemOnClick: boolean, menu?: HTMLElement): void {
        if (menu) { this.menuId = HASH + menu.id; }
        this.subMenuOpen = !showItemOnClick;
    }
    private toggleContextMenuAnimation(ul: HTMLElement, animationSettings: MenuAnimationSettingsModel): void {
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

const ContextMenu: object = { initialize(dataId: string, element: HTMLElement, target: string, filter: string, showOn: string, closeOn: string, enableScrolling: boolean, dotnetRef: BlazorDotnetObject, animationSettings: MenuAnimationSettingsModel): void {
    if (!isNullOrUndefined(element)) { new SfContextMenu(dataId, element, target, filter, showOn, closeOn, enableScrolling, dotnetRef, animationSettings); }
},
contextMenuPosition(dataId: string, left: number, top: number, isRtl: boolean, subMenu: boolean, isCollision: boolean, scrollHeight: number, isDevice?: boolean): void {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
    if (!isNullOrUndefined(instance)) {
        instance.contextMenuPosition(left, top, isRtl, subMenu, isCollision, scrollHeight, isDevice);
        let menu: HTMLElement =  select(DOT + 'e-contextmenu' + DOT + 'e-menu-parent', instance.element);
        if (instance.animationSettings != null && !isNullOrUndefined(menu)) {
            // eslint-disable-next-line no-self-assign
            instance.animationSettings = instance.animationSettings;
            if (instance.enableScrolling && menu && closest(menu, '.e-menu-vscroll')) {
                menu = closest(menu, '.e-menu-vscroll') as HTMLElement;
            }
            instance.toggleContextMenuAnimation(menu, instance.animationSettings);
        }
    }
},
subMenuPosition(dataId: string, isRtl: boolean, showOnClick: boolean, isNull?: boolean, scrollHeight?: boolean): void {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
    if (!isNullOrUndefined(instance)) {
        if (instance.animationElement && instance.animationSettings) {
            Animation.stop(instance.animationElement);
        }
        instance.isShowItemOnClick = showOnClick;
        const cmenu: HTMLElement = instance.hideMenu();
        instance.subMenuPosition(cmenu, isRtl, showOnClick, isNull, scrollHeight, true);
        if (instance.animationSettings != null) {
            let menu: HTMLElement = cmenu;
            if (instance.enableScrolling && cmenu && closest(cmenu, '.e-menu-vscroll')) {
                menu = closest(cmenu, '.e-menu-vscroll') as HTMLElement;
            }
            instance.toggleContextMenuAnimation(menu, instance.animationSettings);
        }
    }
},
toggleAnimation: function (dataId: string) {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
    if (instance && instance.animationSettings != null && instance.animationElement && !isNullOrUndefined(instance.toggleContextMenuAnimation)) {
        instance.toggleContextMenuAnimation(instance.animationElement, instance.animationSettings);
    }
},
onPropertyChanged(dataId: string, key: string, result: string): void {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
    if (!isNullOrUndefined(instance)) {
        instance.onPropertyChanged(key, result);
    }
},
click(dataId: string, isUnwire: boolean): void {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
    if (!isNullOrUndefined(instance)) {
        instance.clickHandler(isUnwire);
    }
},
destroy(dataId: string, refElement: HTMLElement): void {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
    if (!isNullOrUndefined(instance)) {
        instance.destroy(refElement);
    }
},
destroyScrollElement(dataId: string): void {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
    if (!isNullOrUndefined(instance)) {
        instance.destroyScroll();
    }
}
};

export default ContextMenu;

