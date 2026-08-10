/* eslint-disable @typescript-eslint/no-explicit-any */
import { BlazorDotnetObject, closest, attributes, detach, Instance, formatUnit, isNullOrUndefined, DragEventArgs, BaseEventArgs } from '@syncfusion/ej2-base';
import { KeyboardEvents, KeyboardEventArgs, Effect, Browser, DomElements, select, isVisible, remove } from '@syncfusion/ej2-base';
import { setStyleAttribute as setStyle, isNullOrUndefined as isNOU, selectAll, addClass, removeClass } from '@syncfusion/ej2-base';
import { EventHandler, rippleEffect, animationMode, Touch, SwipeEventArgs, Animation, AnimationModel, getRandomId } from '@syncfusion/ej2-base';
import { getElement, BlazorDragEventArgs, Draggable, DragEventArgs as DragArgs, DropEventArgs } from '@syncfusion/ej2-base';
import { Popup, PopupModel } from '@syncfusion/ej2-popups';

type HTEle = HTMLElement;
type HeaderPosition = 'Top' | 'Bottom' | 'Left' | 'Right';
type ContentLoad = 'Dynamic' | 'Init' | 'Demand';
const CLS_TAB: string = 'e-tab';
const CLS_HEADER: string = 'e-tab-header';
const CLS_BLA_TEM: string = 'blazor-template';
const CLS_CONTENT: string = 'e-content';
const CLS_NEST: string = 'e-nested';
const CLS_ITEM: string = 'e-item';
const CLS_RTL: string = 'e-rtl';
const CLS_ACTIVE: string = 'e-active';
const CLS_DISABLE: string = 'e-disable';
const CLS_HIDDEN: string = 'e-hidden';
const CLS_FOCUS: string = 'e-focused';
const CLS_INDICATOR: string = 'e-indicator';
const CLS_WRAP: string = 'e-tab-wrap';
const CLS_TB_ITEMS: string = 'e-toolbar-items';
const CLS_TB_ITEM: string = 'e-toolbar-item';
const CLS_TB_POP: string = 'e-toolbar-pop';
const CLS_TB_POPUP: string = 'e-toolbar-popup';
const CLS_POPUP_OPEN: string = 'e-popup-open';
const CLS_POPUP_CLOSE: string = 'e-popup-close';
const CLS_PROGRESS: string = 'e-progress';
const CLS_IGNORE: string = 'e-ignore';
const CLS_OVERLAY: string = 'e-overlay';
const CLS_HSCRCNT: string = 'e-hscroll-content';
const CLS_VSCRCNT: string = 'e-vscroll-content';
const CLS_VTAB: string = 'e-vertical-tab';
const CLS_HBOTTOM: string = 'e-horizontal-bottom';
const CLS_REORDER_ACTIVE_ITEM: string = 'e-reorder-active-item';
const CLS_VERTICAL_ICON: string = 'e-vertical-icon';
const CLS_VLEFT: string = 'e-vertical-left';
const CLS_VRIGHT: string = 'e-vertical-right';
const SPACEBAR: number = 32;
const END: number = 35;

type OverflowMode = 'Scrollable' | 'Popup' | 'MultiRow' | 'Extended';

/**
 * Interface for a class TabAnimationSettings
 */
interface TabAnimationSettingsModel {

    /**
     * Specifies the animation to appear while moving to previous Tab content.
     *
     * @default { effect: 'SlideLeftIn', duration: 600, easing: 'ease' }
     */
    previous?: TabActionSettingsModel;

    /**
     * Specifies the animation to appear while moving to next Tab content.
     *
     * @default { effect: 'SlideRightIn', duration: 600, easing: 'ease' }
     */
    next?: TabActionSettingsModel;

}

/**
 * Interface for a class TabActionSettings
 */
interface TabActionSettingsModel {

    /**
     * Specifies the animation effect for displaying Tab content.
     *
     * @default 'SlideLeftIn'
     * @aspType string
     */
    effect?: 'None' | Effect;

    /**
     * Specifies the time duration to transform content.
     *
     * @default 600
     */
    duration?: number;

    /**
     * Specifies easing effect applied while transforming content.
     *
     * @default 'ease'
     */
    easing?: string;

}

/** An interface that holds options to control the selected item action. */
interface SelectEventArgs extends BaseEventArgs {
    /** Defines the previous Tab item element. */
    previousItem: HTMLElement
    /** Defines the previous Tab item index. */
    previousIndex: number
    /** Defines the selected Tab item element. */
    selectedItem: HTMLElement
    /** Defines the selected Tab item index. */
    selectedIndex: number
    /** Defines the content selection done through swiping. */
    isSwiped: boolean
    /** Defines the prevent action. */
    cancel?: boolean
    /** Defines the selected content. */
    selectedContent: HTMLElement
    /** Determines whether the event is triggered via user interaction or programmatic way. True, if the event is triggered by user interaction. */
    isInteracted?: boolean
    /** Determines whether the Tab item needs to focus or not after it is selected */
    preventFocus?: boolean
}
/** An interface that holds options to control the selecting item action. */
interface SelectingEventArgs extends SelectEventArgs {
    /** Defines the selecting Tab item element. */
    selectingItem: HTMLElement
    /** Defines the selecting Tab item index. */
    selectingIndex: number
    /** Defines the selecting Tab item content. */
    selectingContent: HTMLElement
    /** Defines the type of the event. */
    event?: Event
}

class SfTab {
    private hdrEle: HTEle;
    private cntEle: HTEle;
    public tabId: string;
    private tbItems: HTEle;
    private tbItem: HTEle[];
    private tbPop: HTEle;
    public isPopup: boolean;
    private prevIndex: number;
    private prevItem: HTEle;
    private popEle: DomElements;
    private bdrLine: HTEle;
    private popObj: Popup;
    private show: object = {};
    private hide: object = {};
    private enableAnimation: boolean;
    private keyModule: KeyboardEvents;
    private tabKeyModule: KeyboardEvents;
    private touchModule: Touch;
    private initRender: boolean;
    private prevActiveEle: string;
    private isSwiped: boolean;
    private isNested: boolean;
    private scrCntClass: string;
    private draggableItems: object[] = [{}];
    private dragItem: HTMLElement;
    private cloneElement: HTMLElement;
    private droppedIndex: number;
    private dragStartIndex: number;
    public isDestroyed: boolean;
    private resizeContext: EventListenerObject = this.refreshActElePosition.bind(this);
    private sfBlazor: any = (window as any).sfBlazor;
    private keyConfigs: { [key: string]: string } = {
        tab: 'tab',
        home: 'home',
        end: 'end',
        enter: 'enter',
        space: 'space',
        delete: 'delete',
        moveLeft: 'leftarrow',
        moveRight: 'rightarrow',
        moveUp: 'uparrow',
        moveDown: 'downarrow'
    };
    public element: HTMLElement;
    public dotNetRef: BlazorDotnetObject;
    public options: ITabOptions;
    public dataId: string;
    constructor(dataId: string, element: HTMLElement, options: ITabOptions, dotnetRef: BlazorDotnetObject) {
        this.element = element;
        this.dotNetRef = dotnetRef;
        this.options = options;
        this.dataId = dataId;
        this.sfBlazor.setCompInstance(this);
    }
    public render(): void {
        const nested: Element = closest(this.element, '.' + CLS_CONTENT);
        this.prevIndex = 0;
        this.isNested = false;
        this.isPopup = false;
        this.initRender = true;
        this.isSwiped = false;
        if (!isNOU(nested)) {
            nested.parentElement.classList.add(CLS_NEST);
            this.isNested = true;
        }
        const name: string = Browser.info.name;
        const css: string = (name === 'msie') ? 'e-ie' : (name === 'edge') ? 'e-edge' : (name === 'safari') ? 'e-safari' : '';
        setStyle(this.element, { 'width': formatUnit(this.options.width), 'height': formatUnit(this.options.height) });
        attributes(this.element, { 'aria-disabled': 'false'});
        this.setCssClass(this.element, css, true);
        this.updatePopAnimationConfig();
        this.tabId = this.element.id.length > 0 ? ('-' + this.element.id) : getRandomId();
        this.wireEvents();
        this.initRender = false;
    }
    public serverItemsChanged(preventFocus: boolean): void {
        this.enableAnimation = false;
        if (this.tbItem && this.tbItem.length !== 0 && this.element && this.element.classList.contains(CLS_HIDDEN)) {
            this.element.classList.remove(CLS_HIDDEN);
        }
        this.setActive(this.options.selectedItem, preventFocus);
        if (this.options.loadOn !== 'Dynamic' && !isNOU(this.cntEle)) {
            const itemCollection: HTMLElement[] = [].slice.call(this.cntEle.children);
            const content: string = CLS_CONTENT + this.tabId + '_' + this.options.selectedItem;
            itemCollection.forEach((item: HTEle) => {
                if (item.classList.contains(CLS_ACTIVE) && item.id !== content) {
                    item.classList.remove(CLS_ACTIVE);
                }
                if (item.id === content) {
                    item.classList.add(CLS_ACTIVE);
                }
            });
            this.prevIndex = this.options.selectedItem;
            this.triggerAnimation(CLS_ITEM + this.tabId + '_' + this.options.selectedItem, false);
        }
        if (animationMode !== 'Disable'){
            this.enableAnimation = true;
        }
    }
    public headerReady(): void {
        this.initRender = true;
        this.hdrEle = this.getTabHeader();
        this.setOrientation(this.options.headerPlacement, this.hdrEle);
        this.tbItems = <HTEle>select('.' + CLS_HEADER + ' .' + CLS_TB_ITEMS, this.element);
        if (!isNOU(this.tbItems)) {
            rippleEffect(this.tbItems, { selector: '.e-tab-wrap' });
        }
        if (selectAll('.' + CLS_TB_ITEM, this.element).length > 0 && !isNullOrUndefined(this.tbItems)) {
            this.bdrLine = <HTEle>select('.' + CLS_INDICATOR + '.' + CLS_IGNORE, this.element);
            const scrollCnt: HTEle = <HTEle>select('.' + this.scrCntClass, this.tbItems);
            if (!isNOU(scrollCnt)) {
                scrollCnt.insertBefore(this.bdrLine, scrollCnt.firstElementChild);
            } else {
                this.tbItems.insertBefore(this.bdrLine, this.tbItems.firstElementChild);
            }
            this.select(this.options.selectedItem);
        }
        this.cntEle = <HTEle>select('.' + CLS_TAB + ' > .' + CLS_CONTENT, this.element);
        if (!isNOU(this.cntEle)) {
            if (isNOU(this.touchModule)) {
                this.bindSwipeEvents();
            }
            if (this.options.height !== 'auto' && !this.isVertical()) {
                this.cntEle.style.height = 'calc(100% - ' + this.hdrEle.offsetHeight + 'px)';
            }
        }
        if (this.options.loadOn === 'Demand' && this.options.selectedItem >= 0) {
            const id: string = this.setActiveContent();
            this.triggerAnimation(id, false);
        }
        this.applyTablistRole();
        this.initRender = false;
    }
    private bindSwipeEvents(): void {
        if (this.options.swipeMode !== -4 && this.options.swipeMode !== -1) {
            this.touchModule = new Touch(this.cntEle, { swipe: this.swipeHandler.bind(this) });
        }
    }

    private setActiveContent(): string {
        const id: string = CLS_ITEM + this.tabId + '_' + this.options.selectedItem;
        const item: HTEle = this.getTrgContent(this.cntEle, this.extIndex(id));
        if (!isNOU(item)) {
            item.classList.add(CLS_ACTIVE);
        }
        return id;
    }
    private removeActiveClass(): void {
        const tabHeader: HTMLElement = this.getTabHeader();
        if (tabHeader) {
            const tabItems: HTMLElement[] = selectAll('.' + CLS_TB_ITEM + '.' + CLS_ACTIVE, tabHeader);
            removeClass(tabItems, CLS_ACTIVE);
            [].slice.call(tabItems).forEach((node: HTMLElement) => node.firstElementChild.setAttribute('aria-selected', 'false'));
        }
    }
    private checkPopupOverflow(ele: HTEle): boolean {
        let isOverflow: boolean = false;
        this.tbPop = <HTEle>select('.' + CLS_TB_POP, this.element);
        const popIcon: HTEle = (<HTEle>select('.e-hor-nav', this.element));
        const tbrItems: HTEle = (<HTEle>select('.' + CLS_TB_ITEMS, this.element));
        const lastChild: HTEle = <HTMLElement>tbrItems.lastChild;
        if ((!this.isVertical() && ((this.options.enableRtl && ((popIcon.offsetLeft + popIcon.offsetWidth) > tbrItems.offsetLeft))
            || (!this.options.enableRtl && popIcon.offsetLeft < tbrItems.offsetWidth))) ||
            (this.isVertical() && (popIcon.offsetTop < lastChild.offsetTop + lastChild.offsetHeight))) {
            isOverflow = true;
            ele.classList.add(CLS_TB_POPUP);
            this.tbPop.insertBefore(<Node>ele, selectAll('.' + CLS_TB_POPUP, this.tbPop)[0]);
        }
        return isOverflow;
    }
    private popupHandler(target: HTEle): number {
        const ripEle: HTEle = <HTEle>target.querySelector('.e-ripple-element');
        if (!isNOU(ripEle)) {
            ripEle.outerHTML = '';
            target.querySelector('.' + CLS_WRAP).classList.remove('e-ripple');
        }
        this.tbItem = selectAll('.' + CLS_TB_ITEMS + ' .' + CLS_TB_ITEM, this.hdrEle);
        const lastChild: HTEle = <HTEle>this.tbItem[this.tbItem.length - 1];
        if (this.tbItem.length !== 0) {
            target.classList.remove(CLS_TB_POPUP);
            target.removeAttribute('style');
            this.tbItems.appendChild(target);
            if (this.checkPopupOverflow(lastChild)) {
                for (let i: number = 0; i < this.tbItems.children.length; i++) {
                    let prevEle: HTEle = <HTEle>(<HTEle>this.tbItems.lastChild).previousElementSibling;
                    prevEle = <HTEle>(prevEle && prevEle.classList.contains(CLS_INDICATOR) ? prevEle.previousElementSibling : prevEle);
                    if (!this.checkPopupOverflow(prevEle ? prevEle : target)) {
                        break;
                    }
                }
            }
            this.isPopup = true;
        }
        return selectAll('.' + CLS_TB_ITEM, this.tbItems).length - 1;
    }
    private previousContentAnimation(prev: number, current: number): AnimationModel {
        let animation: AnimationModel;
        if (this.isPopup || prev <= current) {
            if (this.options.animation.previous.effect === 'SlideLeftIn') {
                animation = {
                    name: 'SlideLeftOut',
                    duration: this.options.animation.previous.duration, timingFunction: this.options.animation.previous.easing
                };
            } else {
                animation = null;
            }
        } else {
            if (this.options.animation.next.effect === 'SlideRightIn') {
                animation = {
                    name: 'SlideRightOut',
                    duration: this.options.animation.next.duration, timingFunction: this.options.animation.next.easing
                };
            } else { animation = null; }
        }
        return animation;
    }
    private triggerPreviousAnimation(oldCnt: HTEle, prevIndex: number): void {
        const animateObj: AnimationModel = this.previousContentAnimation(prevIndex, this.options.selectedItem);
        if (!isNOU(animateObj)) {
            animateObj.begin = () => {
                setStyle(oldCnt, { 'position': 'absolute' });
                addClass([oldCnt], [CLS_PROGRESS, 'e-view']);
            };
            animateObj.end = () => {
                oldCnt.style.display = 'none';
                removeClass([oldCnt], [CLS_ACTIVE, CLS_PROGRESS, 'e-view']);
                setStyle(oldCnt, { 'display': '', 'position': '' });
                if (oldCnt.childNodes.length === 0) {
                    detach(oldCnt);
                }
            };
            new Animation(animateObj).animate(oldCnt);
        } else {
            oldCnt.classList.remove(CLS_ACTIVE);
        }
    }
    private triggerAnimation(id: string, value: boolean): void {
        const prevIndex: number = this.prevIndex;
        let oldCnt: HTEle;
        let newCnt: HTEle;
        let prevEle: HTEle;
        if (this.options.loadOn !== 'Dynamic') {
            const itemCollection: HTMLElement[] = [].slice.call(this.element.querySelector('.' + CLS_CONTENT).children);
            itemCollection.forEach((item: HTEle) => {
                if (item.id === this.prevActiveEle) {
                    oldCnt = item;
                }
            });
            if (!isNOU(this.tbItem)) {
                prevEle = this.tbItem[parseInt(prevIndex.toString(), 10)];
            }
            newCnt = this.getTrgContent(this.cntEle, this.extIndex(id));
            if (isNOU(oldCnt) && !isNOU(prevEle)) {
                const idNo: string = this.extIndex(prevEle.id);
                oldCnt = this.getTrgContent(this.cntEle, idNo);
            }
        } else {
            newCnt = this.cntEle.firstElementChild as HTMLElement;
        }
        if (!isNOU(newCnt)) {
            this.prevActiveEle = newCnt.id;
        }
        if (this.initRender || value === false || isNOU(this.options.animation) || (this.options.animation && Object.keys(this.options.animation).length === 0)) {
            if (oldCnt && oldCnt !== newCnt) { oldCnt.classList.remove(CLS_ACTIVE); }
            return;
        }
        const cnt: HTEle = <HTEle>select('.' + CLS_CONTENT, this.element);
        let animateObj: AnimationModel;
        if (this.prevIndex > this.options.selectedItem && !this.isPopup) {
            const openEff: Effect = (<Effect>this.options.animation.previous.effect === <Effect>'None' && animationMode === 'Enable') ? <Effect>'SlideLeftIn' : <Effect>this.options.animation.previous.effect;
            animateObj = {
                name: <Effect>((openEff === <Effect>'None') ? '' : ((openEff !== <Effect>'SlideLeftIn') ? openEff : 'SlideLeftIn')),
                duration: this.options.animation.previous.duration,
                timingFunction: this.options.animation.previous.easing
            };
        } else if (this.isPopup || this.prevIndex < this.options.selectedItem || this.prevIndex === this.options.selectedItem) {
            const clsEff: Effect = (<Effect>this.options.animation.next.effect === <Effect>'None' && animationMode === 'Enable') ? <Effect>'SlideRightIn' : <Effect>this.options.animation.next.effect;
            animateObj = {
                name: <Effect>((clsEff === <Effect>'None') ? '' : ((clsEff !== <Effect>'SlideRightIn') ? clsEff : 'SlideRightIn')),
                duration: this.options.animation.next.duration,
                timingFunction: this.options.animation.next.easing
            };
        }
        animateObj.progress = () => {
            cnt.classList.add(CLS_PROGRESS); this.setActiveBorder();
        };
        animateObj.end = () => {
            cnt.classList.remove(CLS_PROGRESS);
            newCnt.classList.add(CLS_ACTIVE);
        };
        if (!this.initRender && !isNOU(oldCnt)) {
            this.triggerPreviousAnimation(oldCnt, prevIndex);
        }
        this.isPopup = false;
        if (animateObj.name === <Effect>'') {
            newCnt.classList.add(CLS_ACTIVE);
        } else {
            new Animation(animateObj).animate(newCnt);
        }
    }
    private keyPressed(trg: HTEle): void {
        const trgParent: HTEle = <HTEle>closest(trg, '.' + CLS_HEADER + ' .' + CLS_TB_ITEM);
        const trgIndex: number = this.getEleIndex(trgParent);
        if (!isNOU(this.popEle) && trg.classList.contains('e-hor-nav')) {
            if (this.popEle.classList.contains(CLS_POPUP_OPEN)) {
                this.popObj.hide(this.hide);
            } else {
                this.popObj.show(this.show);
            }
        } else if (trg.classList.contains('e-scroll-nav')) {
            trg.click();
        } else {
            if (!isNOU(trgParent) && trgParent.classList.contains(CLS_ACTIVE) === false) {
                this.select(trgIndex, true);
                if (!isNOU(this.popEle)) {
                    this.popObj.hide(this.hide);
                }
            }
        }
    }
    private getTabHeader(): HTMLElement {
        const headers: HTMLElement[] = [].slice.call(this.element.children).filter((e: HTMLElement) => e.classList.contains(CLS_HEADER));
        if (headers.length > 0) {
            return headers[0];
        } else {
            // eslint-disable-next-line max-len
            const wrap: HTMLElement = [].slice.call(this.element.children).filter((e: HTMLElement) => !e.classList.contains(CLS_BLA_TEM))[0];
            if (!wrap) {
                return undefined;
            }
            return [].slice.call(wrap.children).filter((e: HTMLElement) => e.classList.contains(CLS_HEADER))[0];
        }
    }
    private getEleIndex(item: HTEle): number {
        return Array.prototype.indexOf.call(selectAll('.' + CLS_TB_ITEM, this.getTabHeader()), item);
    }
    private extIndex(id: string): string {
        return id.replace(CLS_ITEM + this.tabId + '_', '');
    }
    private getTrgContent(cntEle: HTEle, no: string): HTEle {
        let ele: HTEle;
        if (this.element.classList.contains(CLS_NEST)) {
            ele = <HTEle>select('.' + CLS_NEST + '> .' + CLS_CONTENT + ' > #' + CLS_CONTENT + this.tabId + '_' + no, this.element);
        } else { ele = this.findEle(cntEle.children, CLS_CONTENT + this.tabId + '_' + no); }
        return ele;
    }
    private findEle(items: HTMLCollection, key: string): HTEle {
        let ele: HTEle;
        for (let i: number = 0; i < items.length; i++) {
            if (items[parseInt(i.toString(), 10)].id === key) { ele = <HTEle>items[parseInt(i.toString(), 10)]; break; }
        }
        return ele;
    }
    private isVertical(): boolean {
        const isVertical: boolean = (this.options.headerPlacement === 'Left' || this.options.headerPlacement === 'Right') ? true : false;
        this.scrCntClass = (isVertical) ? CLS_VSCRCNT : CLS_HSCRCNT;
        return isVertical;
    }
    private updatePopAnimationConfig(): void {
        this.show = { name: (this.isVertical() ? 'FadeIn' : 'SlideDown'), duration: 100 };
        this.hide = { name: (this.isVertical() ? 'FadeOut' : 'SlideUp'), duration: 100 };
    }
    public focusItem(preventScroll: boolean, preventFocus: boolean = false): void {
        const curActItem: HTEle = <HTEle>select(' #' + CLS_ITEM + this.tabId + '_' + this.options.selectedItem, this.hdrEle);
        if (!isNOU(curActItem)) {
            if (!preventFocus) {
                (<HTEle>curActItem.firstElementChild).focus({ preventScroll: preventScroll });
            }
        }
    }
    public serverChangeOrientation(newProp: HeaderPosition, toolbarDataId: string, isVertical: boolean, isChange: boolean): void {
        this.setOrientation(newProp, this.hdrEle);
        removeClass([this.element], [CLS_VTAB, CLS_VLEFT, CLS_VRIGHT, CLS_HBOTTOM]);
        if (isChange) {
            this.changeToolbarOrientation(toolbarDataId, isVertical);
        }
        if (this.options.headerPlacement === 'Bottom') {
            addClass([this.element], CLS_HBOTTOM);
        }
        if (this.isVertical()) {
            const tbPos: string = (this.options.headerPlacement === 'Left') ? CLS_VLEFT : CLS_VRIGHT;
            if (!this.element.classList.contains(CLS_NEST)) {
                addClass([this.element], [CLS_VTAB, tbPos]);
            } else {
                addClass([this.hdrEle], [CLS_VTAB, tbPos]);
            }
        }
        this.setActiveBorder();
        this.focusItem(true, true);
    }
    private changeToolbarOrientation(toolbarDataId: string, isVertical: boolean): void {
        const instance: any = this.sfBlazor.getCompInstance(toolbarDataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            (instance as any).options.width = (isVertical ? 'auto' : '100%');
            (instance as any).options.height = (isVertical ? '100%' : 'auto');
            (instance as any).options.isVertical = isVertical;
            (instance as any).changeOrientation();
        }
        this.updatePopAnimationConfig();
    }
    private setOrientation(place: string, ele: HTEle): void {
        if (isNullOrUndefined(ele)) {
            return;
        }
        const headerPos: number = Array.prototype.indexOf.call(this.element.children, ele);
        const contentPos: number = Array.prototype.indexOf.call(this.element.children, this.element.querySelector('.' + CLS_CONTENT));
        if (place === 'Bottom' && (contentPos > headerPos)) {
            this.element.appendChild(ele);
        } else if (place !== 'Bottom') {
            removeClass([ele], [CLS_HBOTTOM]);
            this.element.insertBefore(ele, select('.' + CLS_CONTENT, this.element));
        }
    }
    public setCssClass(ele: HTEle, cls: string, val: boolean): void {
        if (cls === '') { return; }
        if (val) {
            addClass([ele], cls.split(' '));
        } else {
            removeClass([ele], cls.split(' '));
        }
    }
    private setActiveBorder(): void {
        const trgHdrEle: Element = this.getTabHeader();
        const trg: HTEle = <HTEle>select('.' + CLS_TB_ITEM + '.' + CLS_ACTIVE, trgHdrEle);
        if (isNOU(trg) || isNOU(trgHdrEle)) {
            return;
        }
        if (!this.options.reorderActiveTab) {
            if (trg.classList.contains(CLS_TB_POPUP) && !this.bdrLine.classList.contains(CLS_HIDDEN)) {
                this.bdrLine.classList.add(CLS_HIDDEN);
            }
            if (!trgHdrEle.classList.contains(CLS_REORDER_ACTIVE_ITEM)) {
                trgHdrEle.classList.add(CLS_REORDER_ACTIVE_ITEM);
            }
        } else {
            trgHdrEle.classList.remove(CLS_REORDER_ACTIVE_ITEM);
        }
        if (trg.classList.contains(CLS_TB_POPUP) && this.options.reorderActiveTab) {
            this.popupHandler(trg);
        }
        const root: HTEle = <HTEle>closest(trg, '.' + CLS_TAB);
        if (this.element !== root) { return; }
        this.tbItems = <HTEle>select('.' + CLS_TB_ITEMS, trgHdrEle);
        const bar: HTEle = <HTEle>select('.' + CLS_INDICATOR, trgHdrEle);
        const scrollCnt: HTEle = <HTEle>select('.' + CLS_TB_ITEMS + ' .' + this.scrCntClass, trgHdrEle);
        if (this.isVertical()) {
            setStyle(bar, { 'left': '', 'right': '' });
            const tbHeight: number = (isNOU(scrollCnt)) ? this.tbItems.offsetHeight : scrollCnt.offsetHeight;
            if (tbHeight !== 0) {
                setStyle(bar, { 'top': trg.offsetTop + 'px', 'height': trg.offsetHeight + 'px' });
            } else {
                setStyle(bar, { 'top': 0, 'height': 0 });
            }
        } else {
            if (this.options.overflowMode === 'MultiRow') {
                const top: number = this.options.headerPlacement === 'Bottom' ? trg.offsetTop : trg.offsetHeight + trg.offsetTop;
                setStyle(bar, { 'top': top + 'px', 'height': '' });
            } else {
                setStyle(bar, { 'top': '', 'height': '' });
            }
            const tbWidth: number = (isNOU(scrollCnt)) ? this.tbItems.offsetWidth : scrollCnt.offsetWidth;
            if (tbWidth !== 0) {
                setStyle(bar, { 'left': trg.offsetLeft + 'px', 'right': tbWidth - (trg.offsetLeft + trg.offsetWidth) + 'px' });
            } else {
                setStyle(bar, { 'left': 'auto', 'right': 'auto' });
            }
        }
        if (!isNOU(this.bdrLine) && !trg.classList.contains(CLS_TB_POPUP)) { this.bdrLine.classList.remove(CLS_HIDDEN); }
    }
    private setActive(value: number, preventFocus: boolean = false): void {
        this.tbItem = selectAll('.' + CLS_TB_ITEM, this.getTabHeader());
        if (isNOU(this.hdrEle)) {
            this.hdrEle = this.getTabHeader();
        }
        const trg: HTMLElement = this.hdrEle.querySelector('.' + CLS_TB_ITEM + '[data-index="' + value + '"]');
        if (!trg || value < 0 || isNaN(value) || this.tbItem.length === 0) { return; }
        if (!isNOU(trg) && trg.classList.contains(CLS_DISABLE)) {
            return;
        }
        this.options.selectedItem = value;
        if (!isNOU(trg) && trg.classList.contains(CLS_HIDDEN)) {
            trg.classList.remove(CLS_HIDDEN);
            if (!isNOU(trg.nextElementSibling)) {
                trg.nextElementSibling.classList.add(CLS_HIDDEN);
            }
        }
        if (trg.classList.contains(CLS_ACTIVE)) {
            this.setActiveBorder();
            return;
        }
        const prev: HTEle = this.tbItem[this.prevIndex];
        if (!isNOU(prev)) { prev.firstElementChild.removeAttribute('aria-controls'); }
        attributes(trg.firstElementChild, { 'aria-controls': CLS_CONTENT + this.tabId + '_' + value });
        const id: string = CLS_ITEM + this.tabId + '_' + this.options.selectedItem;
        this.removeActiveClass();
        trg.classList.add(CLS_ACTIVE);
        trg.firstElementChild.setAttribute('aria-selected', 'true');
        const no: number = Number(this.extIndex(id));
        if (isNOU(this.prevActiveEle)) {
            this.prevActiveEle = CLS_CONTENT + this.tabId + '_' + no;
        }
        if (this.options.loadOn === 'Init') {
            this.cntEle = <HTEle>select('.' + CLS_TAB + ' > .' + CLS_CONTENT, this.element);
            const item: HTEle = this.getTrgContent(this.cntEle, this.extIndex(id));
            if (!isNOU(item)) {
                item.classList.add(CLS_ACTIVE);
            }
            this.triggerAnimation(id, this.enableAnimation);
        }
        this.setActiveBorder();
        this.refreshItemVisibility(trg);
        if (!this.initRender && !preventFocus) {
            (<HTEle>trg.firstElementChild).focus();
        }
    }
    public contentReady(): void {
        const id: string = this.setActiveContent();
        this.triggerAnimation(id, this.enableAnimation);
    }
    public setRTL(value: boolean): void {
        this.setCssClass(this.element, CLS_RTL, value);
        this.refreshActiveBorder();
    }
    public refreshActiveBorder(): void {
        if (!isNOU(this.bdrLine)) { this.bdrLine.classList.add(CLS_HIDDEN); }
        this.setActiveBorder();
    }
    public setDragAndDrop(isDragAndDrop: boolean): void {
        if (isDragAndDrop) {
            this.bindDraggable();
        } else {
            if (this.draggableItems) {
                this.draggableItems.forEach((item: Draggable) => {
                    if (item && Object.keys(item).length !== 0) {
                        item.destroy();
                    }
                });
            }
        }
    }
    private showPopup(config: object): void {
        const tbPop: HTEle = <HTEle>select('.e-popup.e-toolbar-pop', this.hdrEle);
        if (tbPop && tbPop.classList.contains('e-popup-close')) {
            const tbPopObj: Popup = (<PopupModel>(tbPop && (<Instance>tbPop).ej2_instances[0])) as Popup;
            tbPopObj.position.X = (this.options.headerPlacement === 'Left') ? 'left' : 'right';
            tbPopObj.dataBind();
            tbPopObj.show(config);
        }
    }
    private initializeDrag(target: HTEle): void {
        this.options.dragArea = !isNOU(this.options.dragArea) ? this.options.dragArea : '#' + this.element.id + ' ' + ('.' + CLS_HEADER);
        const dragObj: Draggable = new Draggable(target, {
            dragArea: this.options.dragArea,
            dragTarget: '.' + CLS_TB_ITEM,
            clone: true,
            enableTapHold: (this.options.overflowMode === 'Scrollable') ? true : false,
            distance: 5,
            helper: this.helper.bind(this),
            dragStart: this.itemDragStart.bind(this),
            drag: (e: DragArgs) => {
                const dragIndex: number = this.getEleIndex(this.dragItem);
                let dropIndex: number;
                if (!isNOU(e.target.closest('.' + CLS_TAB)) && !e.target.closest('.' + CLS_TAB).isEqualNode(this.element) && this.options.dragArea !== '.' + CLS_HEADER) {
                    return;
                }
                if (!(e.target.closest(this.options.dragArea)) && this.options.overflowMode !== 'Popup') {
                    document.body.style.cursor = 'not-allowed';
                    addClass([this.cloneElement], CLS_HIDDEN);
                    if (this.dragItem.classList.contains(CLS_HIDDEN)) {
                        removeClass([this.dragItem], CLS_HIDDEN);
                    }
                    (<HTEle>this.dragItem.querySelector('.' + CLS_WRAP)).style.visibility = 'visible';
                } else {
                    document.body.style.cursor = '';
                    (<HTEle>this.dragItem.querySelector('.' + CLS_WRAP)).style.visibility = 'hidden';
                    if (this.cloneElement.classList.contains(CLS_HIDDEN)) {
                        removeClass([this.cloneElement], CLS_HIDDEN);
                    }
                }
                if (this.options.overflowMode === 'Scrollable' && !isNOU(this.element.querySelector('.e-hscroll'))) {
                    const scrollRightNavEle: HTMLElement = this.element.querySelector('.e-scroll-right-nav');
                    const scrollLeftNavEle: HTMLElement = this.element.querySelector('.e-scroll-left-nav');
                    const hscrollBar: HTMLElement = this.element.querySelector('.e-hscroll-bar');
                    if (!isNOU(scrollRightNavEle) && Math.abs((scrollRightNavEle.offsetWidth / 2) +
                        scrollRightNavEle.offsetLeft) > this.cloneElement.offsetLeft + this.cloneElement.offsetWidth) {
                        hscrollBar.scrollLeft -= 10;
                    }
                    if (!isNOU(scrollLeftNavEle) && Math.abs((scrollLeftNavEle.offsetLeft + scrollLeftNavEle.offsetWidth) -
                        this.cloneElement.offsetLeft) > (scrollLeftNavEle.offsetWidth / 2)) {
                        hscrollBar.scrollLeft += 10;
                    }
                }
                this.cloneElement.style.pointerEvents = 'none';
                const x: number = this.cloneElement.getBoundingClientRect().left;
                const y: number = this.cloneElement.getBoundingClientRect().top;
                const ele: HTMLElement = <HTMLElement>document.elementFromPoint(x, y);
                const dropItem: HTMLElement = <HTMLElement>closest(ele, '.' + CLS_HEADER + ' ' + '.' + CLS_TB_ITEM);
                let scrollContentWidth: number = 0;
                if (this.options.overflowMode === 'Scrollable' && !isNOU(this.element.querySelector('.e-hscroll'))) {
                    scrollContentWidth = (<HTMLElement>this.element.querySelector('.e-hscroll-content')).offsetWidth;
                }
                if (dropItem != null && !dropItem.isSameNode(this.dragItem) &&
                    dropItem.closest('.' + CLS_TAB).isSameNode(this.dragItem.closest('.' + CLS_TAB))) {
                    dropIndex = this.getEleIndex(dropItem);
                    if (dropIndex < dragIndex &&
                        (Math.abs((dropItem.offsetLeft + dropItem.offsetWidth) -
                            this.cloneElement.offsetLeft) > (dropItem.offsetWidth / 2))) {
                        this.dragAction(dropItem, dragIndex, dropIndex);
                    }
                    if (dropIndex > dragIndex &&
                        (Math.abs(dropItem.offsetWidth / 2) + dropItem.offsetLeft -
                            scrollContentWidth) < this.cloneElement.offsetLeft + this.cloneElement.offsetWidth) {
                        this.dragAction(dropItem, dragIndex, dropIndex);
                    }
                }
                this.droppedIndex = this.getEleIndex(this.dragItem);
            },
            dragStop: this.itemDragStop.bind(this)
        });
        this.draggableItems.push(dragObj);
    }

    private helper(e: { sender: MouseEvent & TouchEvent, element: HTMLElement }): HTMLElement {
        if (e.element) {
            this.cloneElement = <HTMLElement>(e.element.cloneNode(true));
            addClass([this.cloneElement], ['e-tab-clone-element', CLS_TAB]);
            removeClass([this.cloneElement.querySelector('.' + CLS_WRAP)], 'e-ripple');
            if (!isNOU(this.cloneElement.querySelector('.e-ripple-element'))) {
                remove(this.cloneElement.querySelector('.e-ripple-element'));
            }
            document.body.appendChild(this.cloneElement);
        }
        return this.cloneElement;
    }

    private itemDragStart(e: DragArgs & BlazorDragEventArgs): void {
        this.dragItem = e.element;
        this.dragStartIndex = this.getEleIndex(this.dragItem);
        // eslint-disable-next-line @typescript-eslint/no-unused-vars
        const toolbarEle: HTEle = this.element.querySelector('.e-toolbar');
        (this as any).dotNetRef.invokeMethodAsync('OnDragStart', this.dragStartIndex).then((isCancel: boolean) => {
            if (isCancel) {
                detach(this.cloneElement);
                const dragObj: Draggable = (e.element as Record<string, any>).ej2_instances[0] as Draggable;
                if (!isNullOrUndefined(dragObj)) {
                    dragObj.intDestroy((e as DragEventArgs).event as MouseEvent & TouchEvent);
                }
            } else {
                this.removeActiveClass();
                addClass([this.tbItems.querySelector('.' + CLS_INDICATOR)], CLS_HIDDEN);
                (<HTEle>this.dragItem.querySelector('.' + CLS_WRAP)).style.visibility = 'hidden';
                e.bindEvents(getElement(e.dragElement));
            }
        });
    }

    private dragAction(dropItem: HTMLElement, dragsIndex: number, dropIndex: number): void {
        if (this.options.overflowMode === 'MultiRow') {
            dropItem.parentNode.insertBefore(this.dragItem, dropItem.nextElementSibling);
        }
        if (dragsIndex > dropIndex) {
            if (!(this.dragItem.parentElement).isSameNode(dropItem.parentElement)) {
                if (this.options.overflowMode === 'Extended') {
                    if (dropItem.isSameNode(dropItem.parentElement.lastChild)) {
                        const popupContainer: Node = this.dragItem.parentNode;
                        dropItem.parentNode.insertBefore(this.dragItem, dropItem);
                        popupContainer.insertBefore(dropItem.parentElement.lastChild, popupContainer.childNodes[0]);
                    } else {
                        this.dragItem.parentNode.insertBefore(
                            (dropItem.parentElement.lastChild), this.dragItem.parentElement.childNodes[0]);
                        dropItem.parentNode.insertBefore(this.dragItem, dropItem);
                    }
                } else {
                    const lastEle: HTMLElement = <HTEle>(dropItem.parentElement).lastChild;
                    if (dropItem.isSameNode(lastEle)) {
                        const popupContainer: Node = <HTEle>this.dragItem.parentNode;
                        dropItem.parentNode.insertBefore(this.dragItem, dropItem);
                        popupContainer.insertBefore(lastEle, popupContainer.childNodes[0]);
                    } else {
                        this.dragItem.parentNode.insertBefore(
                            (dropItem.parentElement).lastChild, this.dragItem.parentElement.childNodes[0]);
                        dropItem.parentNode.insertBefore(this.dragItem, dropItem);
                    }
                }
            } else {
                this.dragItem.parentNode.insertBefore(this.dragItem, dropItem);
            }
        }
        if (dragsIndex < dropIndex) {
            if (!(this.dragItem.parentElement).isSameNode(dropItem.parentElement)) {
                if (this.options.overflowMode === 'Extended') {
                    this.dragItem.parentElement.appendChild(dropItem.parentElement.firstElementChild);
                    dropItem.parentNode.insertBefore(this.dragItem, dropItem.nextSibling);
                } else {
                    this.dragItem.parentNode.insertBefore(
                        (dropItem.parentElement).lastChild, this.dragItem.parentElement.childNodes[0]);
                    dropItem.parentNode.insertBefore(this.dragItem, dropItem);
                }
            } else {
                this.dragItem.parentNode.insertBefore(dropItem, this.dragItem);
            }
        }
    }

    private itemDragStop(e: DropEventArgs): void {
        detach(this.cloneElement);
        const toolbarEle: HTMLElement = this.element.querySelector('.e-toolbar');
        (<HTEle>this.dragItem.querySelector('.' + CLS_WRAP)).style.visibility = 'visible';
        addClass([toolbarEle], 'e-drag-action');
        document.body.style.cursor = '';
        // tslint:disable-next-line:no-any
        const left: number = this.getXYValue(e.event, 'X');
        const top: number = this.getXYValue(e.event, 'Y');
        this.dotNetRef.invokeMethodAsync('Dragged', this.droppedIndex, this.dragStartIndex, left, top);
    }

    private getXYValue(e: MouseEvent | TouchEvent, direction: string): number {
        const touchList: TouchList = (e as TouchEvent).changedTouches;
        let value: number;
        if (direction === 'X') {
            value = touchList ? touchList[0].clientX : (e as MouseEvent).clientX;
        } else {
            value = touchList ? touchList[0].clientY : (e as MouseEvent).clientY;
        }
        return Math.ceil(value);
    }

    public bindDraggable(): void {
        if (this.options.allowDragAndDrop) {
            const items: NodeList = this.element.querySelectorAll('.' + CLS_HEADER + ' ' + '.' + CLS_TB_ITEM);
            items.forEach((element: HTMLElement) => {
                if (isNOU(this.options.dragArea)) {
                    this.options.dragArea = '#' + this.element.id + ' ' + ('.' + CLS_HEADER);
                }
                if (!element.classList.contains('e-draggable')) {
                    this.initializeDrag(element as HTMLElement);
                }
            });
        }
    }
    private wireEvents(): void {
        this.bindDraggable();
        window.addEventListener('resize', this.resizeContext);
        EventHandler.add(this.element, 'keydown', this.spaceKeyDown, this);
        if (!isNOU(this.cntEle)) { this.bindSwipeEvents(); }
        this.keyModule = new KeyboardEvents(this.element, { keyAction: this.keyHandler.bind(this), keyConfigs: this.keyConfigs });
        this.tabKeyModule = new KeyboardEvents(this.element, {
            keyAction: this.keyHandler.bind(this),
            keyConfigs: { openPopup: 'shift+f10', tab: 'tab', shiftTab: 'shift+tab' },
            eventName: 'keydown'
        });
    }
    private unWireEvents(): void {
        this.keyModule.destroy();
        this.tabKeyModule.destroy();
        if (!isNOU(this.cntEle) && !isNOU(this.touchModule)) {
            this.touchModule.destroy();
        }
        window.removeEventListener('resize', this.resizeContext);
        EventHandler.remove(this.element, 'keydown', this.spaceKeyDown);
        removeClass([this.element], [CLS_RTL, CLS_FOCUS]);
    }
    private swipeHandler(e: SwipeEventArgs): void {
        if ((e.velocity < 3 && isNOU(e.originalEvent.changedTouches)) || ((this.options.swipeMode === 1 && (e.originalEvent.type === 'mouseup' || e.originalEvent.type === 'mouseleave')) || (this.options.swipeMode === 2 && e.originalEvent.type === 'touchend'))) {
            return;
        }
        if (this.isNested) {
            this.element.setAttribute('data-swipe', 'true');
        }
        const nestedTab: HTMLElement = this.element.querySelector('[data-swipe="true"]');
        if (nestedTab) {
            nestedTab.removeAttribute('data-swipe');
            return;
        }
        this.isSwiped = true;
        if (e.swipeDirection === 'Right' && this.options.selectedItem !== 0) {
            for (let k: number = this.options.selectedItem - 1; k >= 0; k--) {
                if (!this.tbItem[parseInt(k.toString(), 10)].classList.contains(CLS_HIDDEN) &&
                    !this.tbItem[parseInt(k.toString(), 10)].classList.contains(CLS_DISABLE)) {
                    this.select(k, true);
                    break;
                }
            }
        } else if (e.swipeDirection === 'Left' && (this.options.selectedItem !== selectAll('.' + CLS_TB_ITEM, this.element).length - 1)) {
            for (let i: number = this.options.selectedItem + 1; i < this.tbItem.length; i++) {
                if (!this.tbItem[parseInt(i.toString(), 10)].classList.contains(CLS_HIDDEN) &&
                    !this.tbItem[parseInt(i.toString(), 10)].classList.contains(CLS_DISABLE)) {
                    this.select(i, true);
                    break;
                }
            }
        }
        this.isSwiped = false;
    }
    private spaceKeyDown(e: KeyboardEvent): void {
        if ((e.keyCode === SPACEBAR && e.which === SPACEBAR) || (e.keyCode === END && e.which === END)) {
            const clstHead: HTEle = <HTEle>closest(<Element>e.target, '.' + CLS_HEADER);
            if (!isNOU(clstHead)) {
                e.preventDefault();
            }
        }
    }
    private keyHandler(e: KeyboardEventArgs): void {
        if (this.element.classList.contains(CLS_DISABLE)) { return; }
        this.element.classList.add(CLS_FOCUS);
        const trg: HTEle = <HTEle>e.target;
        const tabHeader: HTMLElement = this.getTabHeader();
        const actEle: HTEle = <HTEle>select('.' + CLS_ACTIVE, tabHeader);
        this.popEle = <DomElements>select('.' + CLS_TB_POP, tabHeader);
        if (!isNOU(this.popEle)) { this.popObj = <Popup>this.popEle.ej2_instances[0]; }
        let item: HTEle;
        let trgParent: HTEle;
        switch (e.action) {
        case 'space':
        case 'enter':
            if (trg.parentElement.classList.contains(CLS_DISABLE)) { return; }
            if (e.action === 'enter' && trg.classList.contains('e-hor-nav')) {
                this.showPopup(this.show);
                break;
            }
            this.keyPressed(trg);
            break;
        case 'tab':
        case 'shiftTab':
            if (trg.classList.contains(CLS_WRAP)
                && (<HTEle>closest(trg, '.' + CLS_TB_ITEM)).classList.contains(CLS_ACTIVE) === false) {
                trg.setAttribute('tabindex', trg.getAttribute('data-tabindex'));
            }
            if (this.popObj && isVisible(this.popObj.element)) {
                this.popObj.hide(this.hide);
            }
            if (!isNOU(actEle) && actEle.children.item(0).getAttribute('tabindex') === '-1') {
                actEle.children.item(0).setAttribute('tabindex', '0');
            }
            break;
        case 'moveLeft':
        case 'moveRight':
            item = <HTEle>closest(document.activeElement, '.' + CLS_TB_ITEM);
            if (!isNOU(item)) { this.refreshItemVisibility(item); }
            break;
        case 'openPopup':
            e.preventDefault();
            if (!isNOU(this.popEle) && this.popEle.classList.contains(CLS_POPUP_CLOSE)) { this.popObj.show(this.show); }
            break;
        case 'delete':
            trgParent = <HTEle>closest(trg, '.' + CLS_TB_ITEM);
            if (this.options.showCloseButton === true && !isNOU(trgParent)) {
                if (this.getEleIndex(trgParent) === -1) { return; }
                const nxtSib: HTEle = <HTEle>trgParent.nextElementSibling;
                if (!isNOU(nxtSib) && nxtSib.classList.contains(CLS_TB_ITEM)) { (<HTEle>nxtSib.firstElementChild).focus(); }
                this.dotNetRef.invokeMethodAsync('RemoveTab', parseInt(trgParent.getAttribute('data-index'), 10));
            }
            this.setActiveBorder();
            break;
        }
    }
    public refreshActElePosition(): void {
        const activeEle: Element = select('.' + CLS_TB_ITEM + '.' + CLS_TB_POPUP + '.' + CLS_ACTIVE, this.element);
        if (!isNOU(activeEle) && this.options.reorderActiveTab) {
            this.select(this.getEleIndex(<HTEle>activeEle));
        }
        this.refreshActiveBorder();
        this.applyTablistRole();
    }
    private refreshItemVisibility(target: HTEle): void {
        const scrCnt: HTEle = <HTEle>select('.' + this.scrCntClass, this.tbItems);
        if (!this.isVertical() && !isNOU(scrCnt)) {
            const scrBar: HTEle = <HTEle>select('.e-hscroll-bar', this.tbItems);
            const scrStart: number = scrBar.scrollLeft;
            const scrEnd: number = scrStart + (this.options.enableRtl ? -scrBar.offsetWidth : scrBar.offsetWidth);
            const eleWidth: number = target.offsetWidth;
            const eleEnd: number = this.options.enableRtl ? -(scrCnt.scrollWidth - target.offsetLeft) :
                target.offsetLeft + target.offsetWidth;
            const eleStart: number = this.options.enableRtl ? eleEnd + eleWidth : target.offsetLeft;
            if (scrStart < eleStart && scrEnd < eleEnd) {
                const eleViewRange: number = this.options.enableRtl ? -(eleEnd - scrStart) : scrEnd - eleStart;
                scrBar.scrollLeft = scrStart + (eleWidth - eleViewRange);
            } else if ((scrStart > eleStart) && (scrEnd > eleEnd)) {
                const eleViewRange: number = this.options.enableRtl ? -(scrEnd - eleStart) : eleEnd - scrStart;
                scrBar.scrollLeft = scrStart - (eleWidth - eleViewRange);
            }
        } else { return; }
    }
    private applyTablistRole(): void {
        const header: HTEle = this.getTabHeader() as HTEle;
        if (!header) { return; }
        const scrollContainer: HTEle = header.querySelector('.' + this.scrCntClass);
        const itemsContainer: HTEle = header.querySelector('.' + CLS_TB_ITEMS);
        if (scrollContainer) {
            scrollContainer.setAttribute('role', 'tablist');
            if (itemsContainer && itemsContainer !== scrollContainer && itemsContainer.getAttribute('role') === 'tablist') {
                itemsContainer.removeAttribute('role');
            }
        } else if (itemsContainer) {
            itemsContainer.setAttribute('role', 'tablist');
        }
    }
    public enableTab(index: number, value: boolean): void {
        const tbItems: HTEle = selectAll('.' + CLS_TB_ITEM, this.element)[parseInt(index.toString(), 10)];
        if (isNOU(tbItems)) { return; }
        if (value === true) {
            tbItems.classList.remove(CLS_DISABLE, CLS_OVERLAY);
            (<HTEle>tbItems.firstElementChild).setAttribute('tabindex', (<HTEle>tbItems.firstElementChild).getAttribute('data-tabindex'));
        } else {
            tbItems.classList.add(CLS_DISABLE, CLS_OVERLAY);
            (<HTEle>tbItems.firstElementChild).removeAttribute('tabindex');
            if (tbItems.classList.contains(CLS_ACTIVE)) { this.select(index + 1); }
        }
        if (!isNOU(tbItems.firstElementChild)) {
            tbItems.firstElementChild.setAttribute('aria-disabled', (value === true) ? 'false' : 'true');
        }
    }
    public hideTab(index: number, value: boolean = true): void {
        let items: HTMLElement[];
        const item: HTEle = select('.' + CLS_TB_ITEM + '[data-index="' + index + '"]', this.element);
        if (isNOU(item)) { return; }
        this.bdrLine.classList.add(CLS_HIDDEN);
        if (value) {
            item.classList.add(CLS_HIDDEN);
            items = selectAll('.' + CLS_TB_ITEM + ':not(.' + CLS_HIDDEN + ')', this.tbItems);
            if (items.length !== 0 && item.classList.contains(CLS_ACTIVE)) {
                this.tbItem = selectAll('.' + CLS_TB_ITEM, this.getTabHeader());
                if (index !== 0) {
                    for (let i: number = index - 1; i >= 0; i--) {
                        if (!this.tbItem[parseInt(i.toString(), 10)].classList.contains(CLS_HIDDEN)) {
                            const activeIndex: number = Array.from(items).indexOf(this.tbItem[i as number]);
                            this.select(activeIndex);
                            break;
                        } else if (i === 0) {
                            for (let k: number = index + 1; k < this.tbItem.length; k++) {
                                if (!this.tbItem[parseInt(k.toString(), 10)].classList.contains(CLS_HIDDEN)) {
                                    const activeIndex: number = Array.from(items).indexOf(this.tbItem[k as number]);
                                    this.select(activeIndex);
                                    break;
                                }
                            }
                        }
                    }
                } else {
                    for (let k: number = index + 1; k < this.tbItem.length; k++) {
                        if (!this.tbItem[parseInt(k.toString(), 10)].classList.contains(CLS_HIDDEN)) {
                            const activeIndex: number = Array.from(items).indexOf(this.tbItem[k as number]);
                            this.select(activeIndex);
                            break;
                        }
                    }
                }
            } else if (items.length === 0) {
                this.element.classList.add(CLS_HIDDEN);
            }
        } else {
            this.element.classList.remove(CLS_HIDDEN);
            items = selectAll('.' + CLS_TB_ITEM + ':not(.' + CLS_HIDDEN + ')', this.tbItems);
            item.classList.remove(CLS_HIDDEN);
            if (items.length === 0) { this.select(index); }
        }
        this.setActiveBorder();
        if (!isNOU(item.firstElementChild)) {
            item.firstElementChild.setAttribute('aria-hidden', '' + value);
        }
    }
    public select(args: number, isInteracted: boolean = false): void {
        const tabHeader: HTMLElement = this.getTabHeader();
        this.tbItems = <HTEle>select('.' + CLS_TB_ITEMS, tabHeader);
        this.tbItem = selectAll('.' + CLS_TB_ITEM, tabHeader);
        this.prevItem = this.tbItem[this.prevIndex];
        let value: number;
        const selectedItem: number = this.options.selectedItem;
        if (isNOU(selectedItem) || (selectedItem < 0) || (this.tbItem.length <= selectedItem) || isNaN(selectedItem)) {
            this.options.selectedItem = 0;
        }
        const visibleTabs: HTMLElement[] = [].slice.call(this.tbItem).filter((item: HTMLElement) => !item.classList.contains('e-hidden'));
        const trg: HTEle = visibleTabs[args as number];
        if (!isNOU(trg) && trg.classList.contains(CLS_DISABLE)) {
            return;
        }
        if (!isNOU(this.prevItem) && !this.prevItem.classList.contains(CLS_DISABLE)) {
            this.prevItem.children.item(0).setAttribute('tabindex', this.prevItem.firstElementChild.getAttribute('tabindex'));
        }
        if (!this.initRender) {
            if (trg) {
                value = parseInt(trg.getAttribute('data-index'), 10);
            }
            const eventArg: SelectingEventArgs = {
                previousItem: null,
                previousIndex: this.prevIndex,
                selectedItem: null,
                selectedIndex: this.options.selectedItem,
                selectedContent: null,
                selectingItem: null,
                selectingIndex: value,
                selectingContent: null,
                isSwiped: this.isSwiped,
                isInteracted: isInteracted,
                cancel: false
            };
            this.dotNetRef.invokeMethodAsync('SelectingEvent', eventArg, value);
        } else {
            this.selectingContent(args);
        }
    }
    public setPersistence(elementId: string, selectedItem: string): void {
        if (this.options.enablePersistence) {
            window.localStorage.setItem(elementId, selectedItem);
        }
    }
    public selectingContent(args: number, preventFocus: boolean = false): void {
        this.tbItem = selectAll('.' + CLS_TB_ITEM, this.hdrEle);
        if (this.tbItem.length > args && args >= 0 && !isNaN(args)) {
            this.prevIndex = this.options.selectedItem;
            const item: HTMLElement = this.hdrEle.querySelector('.' + CLS_TB_ITEM + '[data-index="' + args + '"]');
            if (item && item.classList.contains(CLS_TB_POPUP) && this.options.reorderActiveTab) {
                this.popupHandler(item);
            }
            this.setActive(args, preventFocus);
        } else {
            this.setActive(0, preventFocus);
        }
    }
    public disable(value: boolean): void {
        this.setCssClass(this.element, CLS_DISABLE, value);
        this.element.setAttribute('aria-disabled', '' + value);
    }
    public headerItemsUpdate(args: number): void {
        const tabHeader: HTMLElement = this.getTabHeader();
        this.tbItems = <HTEle>select('.' + CLS_TB_ITEMS, tabHeader);
        this.tbItem = selectAll('.' + CLS_TB_ITEM, tabHeader);
        this.prevItem = this.tbItem[this.prevIndex];
        if (!isNOU(this.prevItem) && !this.prevItem.classList.contains(CLS_DISABLE)) {
            this.prevItem.children.item(0).setAttribute('tabindex', this.prevItem.firstElementChild.getAttribute('tabindex'));
        }
        this.selectingContent(args);
    }
    public destroy(): void {
        this.unWireEvents();
        this.element.removeAttribute('aria-disabled');
    }
    public getContentElement(index: number): HTMLElement {
        return <HTEle>select('.' + CLS_CONTENT + ' #' + CLS_CONTENT + this.tabId + '_' + index, this.element);
    }
}

interface ITabOptions {
    swipeMode: number;
    width: string;
    height: string;
    cssClass: string;
    selectedItem: number;
    headerPlacement: HeaderPosition;
    overflowMode: OverflowMode;
    loadOn: ContentLoad;
    showCloseButton: boolean;
    scrollStep: number;
    enableRtl: boolean;
    animation: TabAnimationSettingsModel;
    enablePersistence: boolean;
    allowDragAndDrop: boolean;
    dragArea: string;
    reorderActiveTab: boolean;
}

interface ToolbarEventArgs {
    toolbarItemIndex?: number;
    isPopupElement: boolean;
}

// tslint:disable
const Tab: object = {
    initialize(dataId: string, element: HTMLElement, options: ITabOptions, dotnetRef: BlazorDotnetObject): void {
        if (element && dataId) {
            const instance: SfTab = new SfTab(dataId, element, options, dotnetRef);
            instance.render();
            instance.headerReady();
        }
    },
    headerReady(dataId: string, isCreatedEvent: boolean): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.headerReady();
        }
    },
    contentReady(dataId: string, selectingIndex: number): string {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            const args: ToolbarEventArgs = { isPopupElement: false };
            instance.element.classList.remove(CLS_FOCUS);
            instance.isPopup = false;
            const headerEle: HTEle = instance.element.querySelector('.' + CLS_HEADER + ' .' + CLS_TB_ITEM + '[data-index="' + selectingIndex + '"]');
            if (!isNOU(headerEle)) {
                args.isPopupElement = !isNOU(closest(headerEle, '.' + CLS_TB_POP));
            }
            const tbItem: HTEle[] = selectAll('.' + CLS_HEADER + ' .' + CLS_TB_ITEMS + ' .' + CLS_TB_ITEM, instance.element);
            args.toolbarItemIndex = tbItem.length - 1;
            instance.headerItemsUpdate(selectingIndex);
            instance.setPersistence('tab' + instance.element.id, selectingIndex.toString());
            if (instance.options.loadOn !== 'Init') {
                instance.contentReady();
            }
            return JSON.stringify(args);
        }
        return null;
    },
    selectingContent(dataId: string, selectingIndex: number): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.selectingContent(selectingIndex, true);
            instance.setPersistence('tab' + instance.element.id, selectingIndex.toString());
            if (instance.options.loadOn !== 'Init') {
                instance.contentReady();
            }
        }
    },
    // eslint-disable-next-line max-len
    serverItemsChanged(dataId: string, selectedItem: number, animation: TabAnimationSettingsModel, isVerticalIcon: boolean, preventFocus: boolean): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.options.selectedItem = selectedItem;
            instance.options.animation = animation;
            if (!isNOU(instance.element.querySelector('.e-toolbar'))) {
                removeClass([instance.element.querySelector('.e-toolbar')], 'e-drag-action');
                removeClass([instance.element.querySelector('.' + CLS_INDICATOR)], CLS_HIDDEN);
            }
            if (isVerticalIcon) {
                addClass([instance.element], CLS_VERTICAL_ICON);
            } else {
                removeClass([instance.element], CLS_VERTICAL_ICON);
            }
            instance.serverItemsChanged(preventFocus);
            if (instance.options.allowDragAndDrop) {
                instance.bindDraggable();
            }
        }
    },
    enableTab(dataId: string, index: number, value: boolean): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.enableTab(index, value);
        }
    },
    hideTab(dataId: string, index: number, value: boolean): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.hideTab(index, value);
        }
    },
    select(dataId: string, index: number): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.select(index);
        }
    },
    disable(dataId: string, value: boolean): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.disable(value);
        }
    },
    setCssClass(dataId: string, cssClass: string): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            if (instance.options.cssClass !== '') {
                instance.setCssClass(instance.element, instance.options.cssClass, false);
            }
            instance.setCssClass(instance.element, cssClass, true);
            instance.options.cssClass = cssClass;
        }
    },
    showCloseButton(dataId: string, showCloseButton: boolean): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.options.showCloseButton = showCloseButton;
            instance.refreshActElePosition();
        }
    },
    // eslint-disable-next-line max-len
    headerPlacement(dataId: string, headerPlacement: HeaderPosition, selectedItem: number, toolbarDataId: string, toolbarCssClass: string, isVertical: boolean, isOrientationChange: boolean): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.options.headerPlacement = headerPlacement;
            instance.options.selectedItem = selectedItem;
            const toolbarInstance: any = this.sfBlazor.getCompInstance(toolbarDataId);
            if (!isNOU(toolbarInstance.element)) {
                // tslint:disable-next-line:no-any
                (toolbarInstance as any).setCssClass(toolbarCssClass);
            }
            instance.serverChangeOrientation(headerPlacement, toolbarDataId, isVertical, isOrientationChange);
        }
    },
    enableRtl(dataId: string, enableRtl: boolean): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.options.enableRtl = enableRtl;
            instance.setRTL(enableRtl);
        }
    },
    overflowMode(dataId: string, overflowMode: OverflowMode): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.options.overflowMode = overflowMode;
            instance.refreshActElePosition();
        }
    },
    allowDragAndDrop(dataId: string, allowDragAndDrop: boolean): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.options.allowDragAndDrop = allowDragAndDrop;
            instance.setDragAndDrop(allowDragAndDrop);
        }
    },
    refresh(dataId: string): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.refreshActiveBorder();
        }
    },
    destroy(dataId: string, elementId: string, selectedItem: string): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.isDestroyed = true;
            instance.setPersistence(elementId, selectedItem);
            instance.destroy();
        }
    },
    getTabItem(element: HTMLElement, index: number): string {
        if (!isNOU(element)) {
            const dom: HTMLElement = element.querySelector('.' + CLS_TB_ITEM + '[data-index="' + index + '"]');
            if (dom) {
                // tslint:disable-next-line:no-any
                return JSON.stringify((window as any).sfBlazor.getDomObject('tabitem', dom));
            }
        }
        return null;
    },
    getTabContent(dataId: string, index: number): string {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            const dom: HTMLElement = instance.getContentElement(index);
            if (dom) {
                // tslint:disable-next-line:no-any
                return JSON.stringify((window as any).sfBlazor.getDomObject('tabcontent', dom));
            }
        }
        return null;
    },
    focusSelectedTab(dataId: string, preventFocus: boolean): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(instance.element)) {
            instance.focusItem(false, preventFocus);
        }
    }
};
export default Tab;
