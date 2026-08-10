import { BlazorDotnetObject, EventHandler, MouseEventArgs, isNullOrUndefined as isNOU, selectAll, closest, Touch, Browser, TouchEventArgs } from '@syncfusion/ej2-base';
import { select, KeyboardEventArgs, Effect, isVisible, animationMode, createElement, detach, matches, getElement, remove } from '@syncfusion/ej2-base';
import { Draggable, DragEventArgs, Droppable, DropEventArgs, KeyboardEvents, BlazorDragEventArgs, Animation } from '@syncfusion/ej2-base';
import { TapEventArgs, removeClass, addClass, AnimationOptions } from '@syncfusion/ej2-base';

const LISTITEM: string = 'e-list-item';
const LISTWRAP: string = 'e-text-wrap';
const PARENTITEM: string = 'e-list-parent';
const HOVER: string = 'e-hover';
const COLLAPSIBLE: string = 'e-icon-collapsible';
const EXPANDABLE: string = 'e-icon-expandable';
const MOUSEOVER: string = 'mouseover';
const CLICK: string = 'Click';
const DBLCLICK: string = 'DoubleClick';
const FOCUSING: string = 'focusin';
const BLUR: string = 'focusout';
const MOUSEDOWN: string = 'mousedown';
const MOUSEUP: string = 'mouseup';
const MOUSEOUT: string = 'mouseout';
const EXPANDONNONE: string = 'None';
const EXPANDONAUTO: string = 'DoubleClick';
const ICON: string = 'e-icons';
const CHECK: string = 'e-check';
const BLOCK: string = 'block';
const HIDDEN: string = 'hidden';
const NONE: string = 'none';
const EMPTY: string = '';
const DISPLAYNONE: string = 'e-display-none';
const ACTIVE: string = 'e-active';
const CONTROL: string = 'e-control';
const ROOT: string = 'e-treeview';
const FOCUS: string = 'e-node-focus';
const PROCESS: string = 'e-process';
const CHECKBOXFRAME: string = 'e-frame';
const CHECKBOXWRAP: string = 'e-checkbox-wrapper';
const CHECKBOXRIPPLE: string = 'e-ripple-container';
const EDITING: string = 'e-editing';
const INPUT: string = 'e-input';
const INPUTGROUP: string = 'e-input-group';
const DISABLED: string = 'e-disabled';
const TEXTWRAP: string = 'e-text-content';
const FULLROW: string = 'e-fullrow';
const DRAGITEM: string = 'e-drag-item';
const DROPPABLE: string = 'e-droppable';
const DRAGGING: string = 'e-dragging';
const SIBLING: string = 'e-sibling';
const DROPIN: string = 'e-drop-in';
const DROPNEXT: string = 'e-drop-next';
const DROPOUT: string = 'e-drop-out';
const NODROP: string = 'e-no-drop';
const RTL: string = 'e-rtl';
const DROPCOUNT: string = 'e-drop-count';
const ITEM_ANIMATION_ACTIVE: string = 'e-animation-active';
const ALLOWDRAGANDDROP: string = 'allowDragAndDrop';
const ALLOWEDITING: string = 'allowEditing';
const SHOWCHECKBOX: string = 'showCheckBox';
const ALLOWTEXTWRAP: string = 'allowTextWrap';
const SETDISABLED: string = 'disabled';
const DRAGAREA: string = 'dragArea';
const CSSCLASS: string = 'cssClass';
const ANIMATION: string = 'animation';
const EXPANDONTYPE: string = 'expandOnType';
const ENABLERTL: string = 'enableRtl';
const DISABLE: string = 'e-disable';
const RIPPLE: string = 'e-ripple';
const RIPPLEELMENT: string = 'e-ripple-element';
const FULLROWSELECT: string = 'fullRowSelect';
const FULLROWWRAP: string = 'e-fullrow-wrap';
const INTERACTION: string = 'e-interaction';
const UL: string = 'e-ul';

class SfTreeView {
    public element: HTMLElement;
    public dotNetRef: BlazorDotnetObject;
    public options: ITreeViewOptions;
    private allowMultiSelection: boolean;
    private dragObj: Draggable;
    private dropObj: Droppable;
    private keyboardModule: KeyboardEvents;
    private dragLi: Element;
    private virtualEle: HTMLElement;
    private dragTarget: Element;
    private dragData: { [key: string]: Object };
    private oldText: string;
    private isHelperElement: boolean = true;
    private iconElement: Element;
    private dragStartAction: boolean;
    private touchClickObj: Touch;
    private touchExpandObj: Touch;
    private touchEditObj: Touch;
    private mouseDownStatus: boolean = false;
    private mouseUpStatus: boolean = false;
    private listBaseOption: { [key: string]: Object };
    private dragParent: Element;
    private expandArgs: NodeExpandEventArgs;
    private editEventArgs: NodeEditEventArgs;
    private dragStartEventArgs: DragEventArgs & BlazorDragEventArgs;
    private draggingEventArgs: DragAndDropEventArgs;
    private dragStopEventArgs: { event: MouseEvent & TouchEvent, element: HTMLElement, target: Element, helper: HTMLElement };
    private startNode: Element;
    private animationObj: Animation;
    private liList: HTMLElement[];
    private keyAction: KeyboardEventArgs;
    private keyConfigs: { [key: string]: string };
    private preventExpand: boolean = false;
    private keyBoardAction: boolean = false;
    private isNodeRendered: boolean = false;
    private isEdited: boolean = false;
    private firstTap: Element;
    public focussedElement: Element;
    private dropRoot: Element;
    private dropTargetElement: Element;
    private dataId: string;
    private isUp: boolean;
    private startTime: number = 0;
    private endTime: number = 0;
    private sibEle: HTMLElement;
    private isKeyUp: boolean = false;
    private timer: number = 0;
    private isParentMouseDown: boolean = false;
    private isAnimationCompleted: boolean = true;
    private prevScrollValue: number = 0;
    private isClickSuppressedAfterDrop: boolean = false;
    private animatingUls: HTMLElement[] = [];
    //Used for store the TapEventArgs while node collapsing
    private tapEvent: TapEventArgs | KeyboardEventArgs | MouseEventArgs;
    constructor(dataId: string, element: HTMLElement, options: ITreeViewOptions, dotnetRef: BlazorDotnetObject) {
        this.element = element;
        this.dataId = dataId;
        this.dotNetRef = dotnetRef;
        this.options = options;
        (window as any).sfBlazor.setCompInstance(this);
    }
    public render(): void {
        this.dragStartAction = false;
        this.listBaseOption = {
            expandCollapse: true,
            showIcon: true,
            expandIconClass: EXPANDABLE,
            expandIconPosition: 'Left'
        };
        this.keyConfigs = {
            escape: 'escape',
            end: 'end',
            enter: 'enter',
            f2: 'f2',
            home: 'home',
            moveDown: 'downarrow',
            moveLeft: 'leftarrow',
            moveRight: 'rightarrow',
            moveUp: 'uparrow',
            ctrlDown: 'ctrl+downarrow',
            ctrlUp: 'ctrl+uparrow',
            ctrlEnter: 'ctrl+enter',
            ctrlHome: 'ctrl+home',
            ctrlEnd: 'ctrl+end',
            ctrlA: 'ctrl+A',
            shiftDown: 'shift+downarrow',
            shiftUp: 'shift+uparrow',
            shiftEnter: 'shift+enter',
            shiftHome: 'shift+home',
            shiftEnd: 'shift+end',
            csDown: 'ctrl+shift+downarrow',
            csUp: 'ctrl+shift+uparrow',
            csEnter: 'ctrl+shift+enter',
            csHome: 'ctrl+shift+home',
            csEnd: 'ctrl+shift+end',
            space: 'space',
            shiftSpace: 'shift+space',
            ctrlSpace: 'ctrl+space'
        };
        this.animationObj = new Animation({});
        const liElements: Element[] = <NodeListOf<HTMLLIElement> & Element[]>this.element.querySelectorAll('.' + LISTITEM);
        if (liElements.length > 0 && !Browser.isDevice) {
            liElements[0].setAttribute('tabindex', '0');
        }
        this.setDisabledMode(this.options.disabled);
        this.setMultiSelect(this.options.allowMultiSelection);
        if (this.options.hasTemplate) { this.element.classList.add(INTERACTION); }
    }

    public setDisabledMode(isEnabled: boolean): void {
        this.setDragAndDrop(this.options.allowDragAndDrop);
        this.wireEditingEvents(this.options.allowEditing);
        this.checkAllDisabled(isEnabled);
        if (isEnabled) {
            this.unWireEvents();
        } else {
            this.wireEvents();
        }
    }

    public checkAllDisabled(isDisabled: boolean): void{
        if (isDisabled) {
            this.element.classList.add(DISABLED);
        } else {
            this.element.classList.remove(DISABLED);
        }
    }

    private isUlAnimating(ul: HTMLElement): boolean {
        return this.animatingUls.indexOf(ul) !== -1;
    }

    private addAnimatingUl(ul: HTMLElement): void {
        if (!isNOU(ul) && this.animatingUls.indexOf(ul) === -1) {
            this.animatingUls.push(ul);
        }
    }

    private removeAnimatingUl(ul: HTMLElement): void {
        const index: number = this.animatingUls.indexOf(ul);
        if (index !== -1) {
            this.animatingUls.splice(index, 1);
        }
    }

    public updateWrap(ulEle?: HTMLElement): void {
        if (!this.options.fullRowSelect) { return; }
        const liEle: Element[] = selectAll('.' + LISTITEM, ulEle ? ulEle : this.element);
        liEle.forEach((li: Element) => {
            const element: HTMLElement = select('.' + FULLROW, li);
            if (element && element.nextElementSibling) {
                element.style.height = this.options.allowTextWrap ? (element.nextElementSibling as HTMLElement).offsetHeight + 'px' : '';
            }
        });

    }

    private setTextWrap(): void {
        if (this.options.allowTextWrap && !this.element.classList.contains(LISTWRAP)) {
            addClass([this.element], LISTWRAP);
        } else if (!this.options.allowTextWrap && this.element.classList.contains(LISTWRAP)) {
            removeClass([this.element], LISTWRAP);
        }
        this.updateWrap();
    }

    private mouseDownHandler(e: MouseEvent): void {
        this.mouseDownStatus = true;
        if (e.shiftKey || e.ctrlKey) {
            e.preventDefault();
        }
        if (e.ctrlKey && this.options.allowMultiSelection) {
            EventHandler.add(this.element, 'contextmenu', this.preventContextMenu, this);
        }
    }

    private mouseupHandler(): void {
        this.mouseUpStatus = true;
    }

    private onMouseLeave(e: MouseEvent): void {
        this.removeHover();
    }

    public unWireEvents(): void {
        this.wireExpandOnEvent(false);
        if (this.options.allowTextWrap) {
            const parentElement: HTMLElement = this.getParentElement(this.element);
            if (parentElement.nodeName !== 'BODY') {
                EventHandler.remove(parentElement, 'mousedown mouseup mousemove', this.resizeHandler);
            }
        }
        EventHandler.remove(this.element, MOUSEDOWN, this.mouseDownHandler);
        EventHandler.remove(this.element, 'click', this.clickHandler);
        EventHandler.remove(this.element, FOCUSING, this.focusIn);
        EventHandler.remove(this.element, BLUR, this.focusOut);
        EventHandler.remove(this.element, MOUSEOVER, this.onMouseOver);
        EventHandler.remove(this.element, MOUSEOUT, this.onMouseLeave);
        EventHandler.remove(this.element, 'contextmenu', this.contextLongPress);
        if (Browser.isDevice && this.options.allowMultiSelection) {
            EventHandler.remove(this.element, 'touchstart', this.touchStart);
            EventHandler.remove(this.element, 'touchend', this.touchEnd);
        }
        if (!this.options.disabled && this.keyboardModule) {
            this.keyboardModule.destroy();
        }
        if (this.element.classList.contains('e-virtualization')) {
            EventHandler.remove(this.element, 'scroll wheel', this.virtualScrollHandler);
        }
    }

    private keyboardActionHandler(e: KeyboardEventArgs): void {
        if (!this.isAnimationCompleted) {
            return;
        }
        this.isKeyUp = true;
        this.keyAction = e;
        const target: Element = <Element>e.target;
        const focusedNode: Element = this.getFocusedNode();
        if (target && (target.classList.contains(INPUT) || target.nodeName === 'INPUT' || target.nodeName === 'TEXTAREA')) {
            const inpEle: HTMLInputElement = <HTMLInputElement>target;
            if (e.action === 'enter') {
                inpEle.blur();
                (<HTMLElement>focusedNode).focus();
                addClass([focusedNode], FOCUS);
            } else if (e.action === 'escape') {
                inpEle.value = this.oldText;
                inpEle.blur();
                (<HTMLElement>focusedNode).focus();
                addClass([focusedNode], FOCUS);
            }
            return;
        }
        e.preventDefault();
        const eventArgs: NodeKeyPressEventArgs = {
            cancel: false,
            event: e
        };
        const id: string = focusedNode.getAttribute('data-uid');
        (this as any).dotNetRef.invokeMethodAsync('TriggerKeyboardEvent', eventArgs, id, e.action, e.key).then((args: NodeKeyPressEventArgs) => {
            if (!isNOU(args)) {
                setTimeout(() => {
                    this.KeyActionHandler(args as any, id);
                }, this.isAnimationCompleted ? 0 : this.animationObj.duration);
            }
        });
    }
    public setMultiSelect(isEnabled: boolean): void {
        this.options.allowMultiSelection = isEnabled;
        if (isEnabled) {
            this.element.setAttribute('aria-multiselectable', 'true');
        } else {
            this.element.setAttribute('aria-multiselectable', 'false');
        }
    }

    public setCssClass(cssClass: string): void {
        if (this.options.cssClass) { removeClass([this.element], this.options.cssClass.split(' ')); }
        if (cssClass) { addClass([this.element], cssClass.split(' ')); }
        this.options.cssClass = cssClass;
    }
    public wireEditingEvents(toBind: boolean): void {
        if (toBind && !this.options.disabled) {
            // eslint-disable-next-line
            const proxy: SfTreeView = this;
            this.touchEditObj = new Touch(this.element, {
                tap: (e: TapEventArgs) => {
                    if ( this.isDoubleTapped(e) && e.tapCount === 2) {
                        e.originalEvent.preventDefault();
                        proxy.editingHandler(e.originalEvent);
                    }
                }
            });
        } else  if (this.touchEditObj) {
            this.touchEditObj.destroy();
        }
    }

    public setDragAndDrop(toBind: boolean): void {
        if (toBind && !this.options.disabled) {
            this.initializeDrag();
        } else {
            this.destroyDrag();
        }
    }

    public setDragArea(dragArea: string): void {
        if (this.options.allowDragAndDrop) {
            this.dragObj.dragArea = dragArea;
        }
    }

    private destroyDrag(): void {
        if (this.dragObj && this.dropObj) {
            this.dragObj.destroy();
            this.dropObj.destroy();
        }
    }

    private scrollUp(scrollParent: Element): void {
        const node: HTMLElement = select(this.options.fullRowSelect ? '.e-fullrow ' : '.e-list-item', this.element);
        if (node) {
            scrollParent.scrollBy(0, -(node.offsetHeight / 2));
        }
    }

    private scrollDown(scrollParent: Element): void {
        const node: HTMLElement = select(this.options.fullRowSelect ? '.e-fullrow ' : '.e-list-item', this.element);
        if (node) {
            scrollParent.scrollBy(0, (node.offsetHeight / 2));
        }
    }

    private clearTimer(): void {
        window.clearInterval(this.timer);
        if (this.virtualEle) {
            this.virtualEle.style.position = 'absolute';
        }
    }

    private initializeDrag(): void {
        let virtualEle: HTMLElement;
        this.dragObj = new Draggable(this.element, {
            enableTailMode: true,
            dragArea: this.options.dropArea,
            dragTarget: '.' + TEXTWRAP,
            helper: (e: { sender: MouseEvent & TouchEvent, element: HTMLElement }) => {
                this.dragTarget = <Element>e.sender.target;
                const dragRoot: Element = closest(this.dragTarget, '.' + ROOT);
                let dragWrap: Element = closest(this.dragTarget, '.' + TEXTWRAP);
                this.dragLi = closest(this.dragTarget, '.' + LISTITEM);
                if (this.options.fullRowSelect && !dragWrap && this.dragTarget.classList.contains(FULLROW)) {
                    dragWrap = this.dragTarget.nextElementSibling;
                }
                if (!this.dragTarget || !e.element.isSameNode(dragRoot) || !dragWrap ||
                    this.dragTarget.classList.contains(ROOT) || this.dragTarget.classList.contains(PARENTITEM) ||
                    this.dragTarget.classList.contains(LISTITEM) || this.dragLi.classList.contains(DISABLE)) {
                    return false;
                }
                const cloneEle: Element = <Element>(dragWrap.cloneNode(true));
                if (isNOU(select('div.' + ICON, cloneEle))) {
                    const icon: HTMLElement = createElement('div', { className: ICON + ' ' + EXPANDABLE });
                    cloneEle.insertBefore(icon, cloneEle.children[0]);
                }
                const cssClass: string = DRAGITEM + ' ' + ROOT + ' ' + this.options.cssClass + ' ' + (this.options.enableRtl ? RTL : EMPTY);
                virtualEle = createElement('div', { className: cssClass });
                virtualEle.appendChild(cloneEle);
                const selectedLI: Element[] = <NodeListOf<Element> & Element[]>this.element.querySelectorAll('.' + ACTIVE);
                const length: number = selectedLI.length;
                if (length > 1 && this.options.allowMultiSelection && this.dragLi.classList.contains(ACTIVE)) {
                    const cNode: HTMLElement = createElement('span', { className: DROPCOUNT, innerHTML: EMPTY + length });
                    virtualEle.appendChild(cNode);
                }
                document.body.appendChild(virtualEle);
                document.body.style.cursor = EMPTY;
                this.dragData = this.getNodeData(this.dragLi);
                this.virtualEle = virtualEle;
                return virtualEle;
            },
            drag: (e: DragEventArgs) => {
                this.clearTimer();
                if (this.mouseUpStatus) {
                    detach(virtualEle);
                    removeClass([this.element], DRAGGING);
                    this.removeVirtualEle();
                    document.body.style.cursor = EMPTY;
                    return;
                }
                this.dragObj.setProperties({ cursorAt: { top: (!isNOU(e.event.targetTouches) || Browser.isDevice) ? 60 : -20 } });
                this.dragAction(e, virtualEle);
                const scrollParent: Element = this.getScrollParent(e.target);
                let elementData: DOMRect | ClientRect;
                if (!isNOU(scrollParent)) {
                    elementData = scrollParent.getBoundingClientRect();
                    if ((e.event.y <= (elementData.top + 30)) || (elementData.top < 0 && e.event.y <= 30)) {
                        this.virtualEle.style.position = 'fixed';
                        // eslint-disable-next-line
                        const _this: SfTreeView = this;
                        this.timer = window.setInterval(function (): void {
                            _this.scrollUp(scrollParent);
                        }, 200);
                    }
                    if (e.event.y >= (elementData.top < 0 ? (scrollParent.clientHeight - 60) :
                        (elementData.top + scrollParent.clientHeight - 60))) {
                        this.virtualEle.style.position = 'fixed';
                        // eslint-disable-next-line
                        const _this: SfTreeView = this;
                        this.timer = window.setInterval(function (): void {
                            _this.scrollDown(scrollParent);
                        }, 200);
                    }
                }
            },
            dragStart: (e: DragEventArgs & BlazorDragEventArgs) => {
                if (isNOU(e.target)) { return; }
                EventHandler.add(document, 'scroll', this.scrollHandler, this);
                addClass([this.element], DRAGGING);
                const listItem: Element = closest(e.target, '.' + LISTITEM); let level: number;
                if (listItem) {
                    level = parseInt(listItem.getAttribute('aria-level'), 10);
                    EventHandler.add(listItem, MOUSEUP, this.mouseupHandler, this);
                }
                if (this.element.classList.contains('e-virtualization')) {
                    this.dragParent = this.dragLi.parentElement;
                }
                const eventArgs: DragAndDropEventArgs = this.getDragEvent(e.event, this, null, e.target, null, virtualEle, level);
                if (eventArgs.draggedNode.classList.contains(EDITING)) {
                    this.dragObj.intDestroy(e.event);
                    this.dragCancelAction(virtualEle);
                } else {
                    this.dragStartEventArgs = e;
                    const left: number = this.getXYValue(e.event, 'X');
                    const top: number = this.getXYValue(e.event, 'Y');
                    virtualEle.style.display = NONE;
                    this.dotNetRef.invokeMethodAsync('TriggerDragStartEvent', this.updateObjectValues(eventArgs), left, top);
                }
            },
            dragStop: (e: { event: MouseEvent & TouchEvent, element: HTMLElement, target: Element, helper: HTMLElement }) => {
                EventHandler.remove(document, 'scroll', this.scrollHandler);
                if (isNOU(e.target)) { return; }
                this.clearTimer();
                removeClass([this.element], DRAGGING);
                const hoveredNode: HTMLElement = select('.' + HOVER, this.element);
                let sibEleOffsetTop: number;
                //If the target is SIBLING it will change the target
                if (e.target.classList.contains(SIBLING)) { e.target = !isNOU(hoveredNode) ? hoveredNode : closest(e.target, '.' + LISTITEM); }
                this.sibEle = select('.' + SIBLING);
                if (this.sibEle) { sibEleOffsetTop = this.sibEle.offsetTop; }
                this.removeVirtualEle();
                const dropTarget: Element = e.target;
                const preventTargetExpand: boolean = false;
                this.dropRoot = (closest(dropTarget, '.' + DROPPABLE));
                this.isHelperElement = true;
                if (!dropTarget || !this.dropRoot) {
                    if (e.helper && e.helper.parentNode) { remove(e.helper); }
                    document.body.style.cursor = EMPTY;
                    this.isHelperElement = false;
                }
                const listItem: Element = closest(dropTarget, '.' + LISTITEM); let level: number;
                const liItem: HTMLElement = <HTMLElement>listItem;
                if (liItem && e && e.event) {
                    const rect: ClientRect = liItem.getBoundingClientRect();
                    const pointerY: number = e.event.changedTouches ? e.event.changedTouches[0].clientY : e.event.clientY;
                    this.isUp = pointerY <= (rect.top + rect.height / 2);
                }
                if (isNOU(this.sibEle)) { this.isUp = true; }
                if (listItem) {
                    level = parseInt(listItem.getAttribute('aria-level'), 10);
                }
                this.mouseUpStatus = false;
                if (this.dragLi) {
                    EventHandler.remove(this.dragLi, MOUSEUP, this.mouseupHandler);
                }
                const dropEle: HTMLElement = <HTMLElement>dropTarget;
                const eventArgs: DragAndDropEventArgs = this.getDragEvent(e.event, this, dropTarget, dropEle, null, e.helper, level);
                this.dragStopEventArgs = e;
                eventArgs.preventTargetExpand = preventTargetExpand;
                const left: number = this.getXYValue(e.event, 'X');
                const top: number = this.getXYValue(e.event, 'Y');
                if (isNOU(eventArgs.dropIndicator)){
                    eventArgs.dropIndicator = NODROP;
                    document.body.style.cursor = 'not-allowed';
                }
                this.isClickSuppressedAfterDrop = true;
                const externalDrag: boolean = this.isExternalDrop(eventArgs.draggedNode);
                const dropInstance: SfTreeView = this.dropRoot && externalDrag ? (window as any).sfBlazor.getCompInstance(this.dropRoot.getAttribute('data-id')) : null;
                this.dotNetRef.invokeMethodAsync('TriggerDragStopEvent', this.updateObjectValues(eventArgs), left, top, dropInstance ? dropInstance.dotNetRef : null);
            }
        });
        this.dropObj = new Droppable(this.element, {
            out: (e: { evt: MouseEvent & TouchEvent, target: Element }) => {
                if (!isNOU(e && e.target) && !e.target.classList.contains(SIBLING)) {
                    document.body.style.cursor = 'not-allowed';
                }
            },
            over: (e: { evt: MouseEvent & TouchEvent, target: Element }) => {
                document.body.style.cursor = EMPTY;
            }
        });
    }

    private scrollHandler(): void {
        if (this.virtualEle) {
            const currentTop: number = parseFloat(window.getComputedStyle(this.virtualEle).getPropertyValue('top'));
            const scrollingElement: Element = this.getScrollParent(this.element);
            if (!isNOU(currentTop) && !isNOU(scrollingElement) && currentTop > scrollingElement.clientHeight) {
                const newTop: number = currentTop - scrollingElement.scrollTop;
                this.virtualEle.style.top = newTop + 'px';
            }
            this.virtualEle.style.position = 'fixed';
        }
    }

    private updateObjectValues(evtArgs: DragAndDropEventArgs): DragAndDropEventArgs {
        evtArgs['clonedNode'] = null;
        evtArgs['draggedNode'] = null;
        evtArgs['draggedParentNode'] = null;
        evtArgs['dropTarget'] = null;
        evtArgs['droppedNode'] = null;
        evtArgs['target'] = null;
        return evtArgs;
    }

    public dragNodeStop(eventArgs: DragAndDropEventArgs): void {
        this.dragParent = eventArgs.draggedParentNode;
        this.preventExpand = eventArgs.preventTargetExpand;
        if (eventArgs.cancel || eventArgs.dropIndicator === NODROP) {
            if (this.dragStopEventArgs.helper.parentNode) {
                remove(this.dragStopEventArgs.helper);
            }
            document.body.style.cursor = '';
            this.isHelperElement = false;
        }
        this.dragStartAction = false;
        if (this.isHelperElement) {
            this.dropAction(this.dragStopEventArgs );
        }
    }

    public dragStartActionContinue(cancel: boolean): void {
        if  (cancel) {
            this.dragObj.intDestroy(this.dragStartEventArgs.event);
            this.dragCancelAction(this.virtualEle);
        } else {
            this.virtualEle.style.display = BLOCK;
            this.dragStartAction = true;
            this.dragStartEventArgs.bindEvents(getElement(this.dragStartEventArgs.dragElement));
        }
    }

    private getId(ele: string | Element): string {
        if (isNOU(ele)) {
            return null;
        } else if (typeof ele === 'string') {
            return ele;
        } else if (typeof ele === 'object') {
            return (getElement(ele)).getAttribute('data-uid');
        } else {
            return null;
        }
    }

    private getOffsetValue(e: any, direction: string): number {
        let value: number;
        const classList: DOMTokenList = e.target.classList;
        if (Browser.info.name === 'mozilla' && !isNOU(classList)) {
            const rect: ClientRect = e.target.getBoundingClientRect();
            value =  Math.ceil((direction === 'Y') ? (e.event.clientY - rect.top) : (e.event.clientX - rect.left));
        } else {
            value = (direction === 'Y') ? e.event.offsetY : e.event.offsetX;
        }
        return value;
    }

    private dropAction(e: any): void {
        const offsetY: number = this.getOffsetValue(e, 'Y');
        const dropTarget: Element = <Element>e.target;
        let level: number;
        let drop: boolean = false;
        const dragObj: SfTreeView = (window as any).sfBlazor.getCompInstance(this.dataId);
        if (dragObj && dragObj.dragTarget) {
            const dragTarget: Element = dragObj.dragTarget;
            const dragLi: Element = (closest(dragTarget, '.' + LISTITEM));
            let dropLi: Element = (closest(dropTarget, '.' + LISTITEM));
            if (dropLi == null && dropTarget.classList.contains(ROOT)) {
                dropLi = dropTarget.firstElementChild;
            }
            remove(e.helper);
            if (dropTarget && !dropTarget.closest('.' + ROOT + '.' + DROPPABLE)) {
                return;
            }
            document.body.style.cursor = EMPTY;
            if (!dropLi || dropLi.isSameNode(dragLi) || this.isDescendant(dragLi, dropLi)) {
                return;
            }
            if (dragObj.options.allowMultiSelection && dragLi.classList.contains(ACTIVE)) {
                const sNodes: HTMLElement[] = selectAll('.' + ACTIVE, dragObj.element);
                if (e.target.offsetHeight <= 33 && offsetY > e.target.offsetHeight - 10 && offsetY > 6) {
                    for (let i: number = sNodes.length - 1; i >= 0; i--) {
                        if (dropLi.isSameNode(sNodes[i as number]) || this.isDescendant(sNodes[i as number], dropLi)) {
                            continue;
                        }
                        this.appendNode(dropTarget, sNodes[i as number], dropLi, e, dragObj, offsetY);
                    }
                } else {
                    for (let i: number = 0; i < sNodes.length; i++) {
                        if (dropLi.isSameNode(sNodes[i as number]) || this.isDescendant(sNodes[i as number], dropLi)) {
                            continue;
                        }
                        this.appendNode(dropTarget, sNodes[i as number], dropLi, e, dragObj, offsetY);
                    }
                }
            } else {
                this.appendNode(dropTarget, dragLi, dropLi, e, dragObj, offsetY);
            }
            level = parseInt(dragLi.getAttribute('aria-level'), 10);
            drop = true;
        }
        const element: HTMLLIElement = <HTMLLIElement>e.element;
        const eventArgs: DragAndDropEventArgs = this.getDragEvent(e.event, dragObj, dropTarget, e.target, element, null, level, drop);
        const left: number = this.getXYValue(e.event, 'X');
        const top: number = this.getXYValue(e.event, 'Y');
        this.dotNetRef.invokeMethodAsync('TriggerNodeDropped', this.updateObjectValues(eventArgs), left, top);
    }

    private isDoubleTapped(e: TapEventArgs): boolean {
        const target: Element = <Element>e.originalEvent.target;
        let secondTap: Element;
        if (target && e.tapCount) {
            if (e.tapCount === 1) {
                this.firstTap = closest(target, '.' + LISTITEM);
            } else if (e.tapCount === 2) {
                secondTap = closest(target, '.' + LISTITEM);
            }
        }
        return (this.firstTap === secondTap);
    }

    private isDescendant(parent: Element, child: Element): boolean {
        let node: Element = <Element>child.parentNode;
        while (!isNOU(node)) {
            if (node === parent) {
                return true;
            }
            node = <Element>node.parentNode;
        }
        return false;
    }

    private appendNode(dropTarget: Element, dragLi: Element, dropLi: Element, e: DropEventArgs,
                       dragObj: SfTreeView, offsetY: number): void {
        const checkContainer: HTMLElement = closest(dropTarget, '.' + CHECKBOXWRAP) as HTMLElement;
        const collapse: Element = closest(e.target, '.' + COLLAPSIBLE);
        const expand: Element = closest(e.target, '.' + EXPANDABLE);
        const offsetX: number = this.getOffsetValue (e, 'X');
        if (!dragLi.classList.contains(DISABLE) && !checkContainer && ((expand && offsetY < 5) || (collapse && offsetX < 3)
            || (expand && offsetY > 19) || (collapse && offsetX > 19) || (!expand && !collapse))) {
            if (dropTarget.classList.contains(LISTITEM)) {
                this.dropAsSiblingNode(dragLi, dropLi, e, dragObj);
            } else if (dropTarget.firstElementChild && dropTarget.classList.contains(ROOT)) {
                if (dropTarget.firstElementChild.classList.contains(UL)) {
                    this.dropAsSiblingNode(dragLi, dropLi, e, dragObj);
                }
            } else if ((dropTarget.classList.contains(COLLAPSIBLE)) || (dropTarget.classList.contains(EXPANDABLE))) {
                this.dropAsSiblingNode(dragLi, dropLi, e, dragObj);
            } else {
                this.dropAsChildNode(dragLi, dropLi, dragObj, null, e, offsetY);
            }
        } else {
            this.dropAsChildNode(dragLi, dropLi, dragObj, null, e, offsetY, true);
        }
    }

    private dropAsSiblingNode(dragLi: Element, dropLi: Element, e: DropEventArgs, dragObj: SfTreeView): void {
        const dropUl: Element = closest(dropLi, '.' + PARENTITEM);
        const dragParentUl: Element = closest(dragLi, '.' + PARENTITEM);
        const dragParentLi: Element = closest(dragParentUl, '.' + LISTITEM);
        const dropParentLi: Element = closest(dropUl, '.' + LISTITEM);
        let dropParentLiId: string = null; let dragParentLiId: string = null;
        let pre: boolean;
        const offsetX: number = this.getOffsetValue(e, 'X');
        const offsetY: number = this.getOffsetValue(e, 'Y');
        if (e.target.offsetHeight > 0 && offsetY > e.target.offsetHeight - 2) {
            pre = false;
        } else if (offsetY < 2) {
            pre = true;
        } else if (e.target.classList.contains(EXPANDABLE) || (e.target.classList.contains(COLLAPSIBLE))) {
            if ((offsetY < 5) || (offsetX < 3)) {
                pre = true;
            } else if ((offsetY > 15) || (offsetX > 17)) {
                pre = false;
            }
        }
        const originalTarget: HTMLElement = e.event.target as HTMLElement;
        if (originalTarget.classList.contains(SIBLING) && this.isUp) { pre = true; }
        if (dropParentLi) {
            dropParentLiId = dropParentLi.getAttribute('data-uid');
        }
        if (dragParentLi) {
            dragParentLiId = dragParentLi.getAttribute('data-uid');
        }
        const outerDrag: boolean = this.isExternalDrop(dragLi);
        const targetControl: SfTreeView = this.dropRoot ? (window as any).sfBlazor.getCompInstance(this.dropRoot.getAttribute('data-id')) : null;
        const droppedInstance: SfTreeView = outerDrag ? targetControl : this;
        const eventArgs: DropTreeArgs = this.getDropArgs(dragLi, dropLi, dragParentLiId, dragObj, dropParentLiId, pre);
        droppedInstance.dotNetRef.invokeMethodAsync('DropNodeAsSibling', eventArgs);
        this.updateAriaLevel(dragLi);
    }

    private updateAriaLevel(dragLi: Element): void {
        const level: number = this.parents(dragLi, '.' + PARENTITEM).length;
        dragLi.setAttribute('aria-level', EMPTY + level);
        this.updateChildAriaLevel(select('.' + PARENTITEM, dragLi), level + 1);
    }

    private updateChildAriaLevel(element: Element, level: number): void {
        if (!isNOU(element)) {
            const cNodes: Element[] = <NodeListOf<HTMLLIElement> & Element[]>element.querySelectorAll('.' + LISTITEM);
            for (const liEle of cNodes) {
                liEle.setAttribute('aria-level', String(level));
                this.updateChildAriaLevel(select('.' + PARENTITEM, liEle), level + 1);
            }
        }
    }

    private dropAsChildNode(dragLi: Element, dropLi: Element, dragObj: SfTreeView, index?: number,
                            e?: DropEventArgs, pos?: number, isCheck?: boolean): void {
        const dragParentUl: Element = closest(dragLi, '.' + PARENTITEM);
        const dragParentLi: Element = dragParentUl ? closest(dragParentUl, '.' + LISTITEM) : null;
        const dropParentUl: Element = closest(dropLi, '.' + PARENTITEM);
        const dropParentLi: Element = closest(dropParentUl, '.' + LISTITEM);
        let dropParentLiId: string = null; let dragParentLiId: string = null;
        if (dropParentLi) {
            dropParentLiId = dropParentLi.getAttribute('data-uid');
        }
        if (dragParentLi) {
            dragParentLiId = dragParentLi.getAttribute('data-uid');
        }
        const outerDrag: boolean = this.isExternalDrop(dragLi);
        const srcControl : SfTreeView = closest(dragLi, '.' + ROOT) ? (window as any).sfBlazor.getCompInstance(this.dataId) :
            (this.element ? (window as any).sfBlazor.getCompInstance(this.dataId) : null);
        const targetControl: SfTreeView = this.dropRoot ? (window as any).sfBlazor.getCompInstance(this.dropRoot.getAttribute('data-id')) : null;
        const droppedInstance: SfTreeView = outerDrag ? targetControl : this;
        let eventArgs: DropTreeArgs;
        if (e && (pos < 7) && !isCheck) {
            eventArgs = this.getDropArgs(dragLi, dropLi, dragParentLiId, srcControl, dropParentLiId, true);
            droppedInstance.dotNetRef.invokeMethodAsync('DropNodeAsSibling', eventArgs);
        } else if (e && (e.target.offsetHeight > 0 && pos > (e.target.offsetHeight - 10)) && !isCheck) {
            eventArgs = this.getDropArgs(dragLi, dropLi, dragParentLiId, srcControl, dropParentLiId, false);
            droppedInstance.dotNetRef.invokeMethodAsync('DropNodeAsSibling', eventArgs);
        } else {
            eventArgs = this.getDropArgs(dragLi, dropLi, dragParentLiId, srcControl);
            droppedInstance.dotNetRef.invokeMethodAsync('DropNodeAsChild', eventArgs);
        }
        this.updateAriaLevel(dragLi);
    }

    private isExternalDrop(dragLi: Element ): boolean {
        let isExternalDrop: boolean = false;
        const srcElement: Element = closest(dragLi, '.' + ROOT) ? closest(dragLi, '.' + ROOT) : this.element;
        const targetElement: Element = this.dropRoot;
        if ((srcElement != null && targetElement != null && !srcElement.isSameNode(targetElement))) {
            isExternalDrop = true;
        }
        return isExternalDrop;
    }

    private getDropArgs(dragLi: Element, dropLi: Element, dragParentLiId: string,
                        treeObj: SfTreeView, dropParentLi?: string , pre?: boolean): DropTreeArgs {
        return {
            dragLi: dragLi.getAttribute('data-uid'),
            dropLi: dropLi.getAttribute('data-uid'),
            dragParentLi: dragParentLiId,
            dropParentLi: dropParentLi,
            pre: pre,
            srcTree: treeObj.dotNetRef,
            isExternalDrag: this.isExternalDrop(dragLi)};
    }

    private dragCancelAction(virtualEle: HTMLElement): void {
        detach(virtualEle);
        removeClass([this.element], DRAGGING);
        this.dragStartAction = false;
    }

    private removeVirtualEle(): void {
        const sibEle: Element = select('.' + SIBLING);
        if (sibEle) {
            detach(sibEle);
        }
    }

    private dragAction(e: DropEventArgs, virtualEle: HTMLElement): void {
        if (isNOU(e.target)) { return; }
        const dropRoot: Element = closest(e.target, '.' + DROPPABLE);
        let dropWrap: Element = closest(e.target, '.' + TEXTWRAP);
        const icon: Element = select('div.' + ICON, virtualEle);
        const offsetX: number = this.getOffsetValue(e, 'X');
        const offsetY: number = this.getOffsetValue(e, 'Y');
        removeClass([icon], [DROPIN, DROPNEXT, DROPOUT, NODROP]);
        this.removeVirtualEle();
        document.body.style.cursor = EMPTY;
        const classList: DOMTokenList = e.target.classList;
        if (this.options.fullRowSelect && !dropWrap && !isNOU(classList) && classList.contains(FULLROW)) {
            dropWrap = e.target.nextElementSibling;
        }
        if (dropRoot) {
            const dropLi: Element = closest(e.target, '.' + LISTITEM);
            const checkContainer: HTMLElement = closest(e.target, '.' + CHECKBOXWRAP) as HTMLElement;
            const collapse: Element = closest(e.target, '.' + COLLAPSIBLE);
            const expand: Element = closest(e.target, '.' + EXPANDABLE);
            if (!dropRoot.classList.contains(ROOT) || (dropWrap &&
                (!dropLi.isSameNode(this.dragLi) && !this.isDescendant(this.dragLi, dropLi)))) {
                if ((dropLi && e && (!expand && !collapse) && (offsetY < 7) && !checkContainer) ||
                    (((expand && offsetY < 5) || (collapse && offsetX < 3)))) {
                    addClass([icon], DROPNEXT);
                    const element: Element = createElement('div', { className: SIBLING });
                    const index: number = this.options.fullRowSelect ? (1) : (0);
                    dropLi.insertBefore(element, dropLi.children[index as number]);
                } else if ((dropLi && e && (!expand && !collapse) && (e.target.offsetHeight > 0 && offsetY >
                    (e.target.offsetHeight - 10)) && !checkContainer) || (((expand && offsetY > 19) ||
                    (collapse && offsetX > 19)))) {
                    addClass([icon], DROPNEXT);
                    const element: Element = createElement('div', { className: SIBLING });
                    const index: number = this.options.fullRowSelect ? (2) : (1);
                    dropLi.insertBefore(element, dropLi.children[index as number]);
                } else {
                    addClass([icon], DROPIN);
                }
            } else if (
                e.target.classList.contains(LISTITEM) &&
                (!dropLi.isSameNode(this.dragLi) &&
                !this.isDescendant(this.dragLi, dropLi))
            ) {
                addClass([icon], DROPNEXT);
                this.renderVirtualEle(e);
            } else if (e.target.classList.contains(SIBLING)) {
                addClass([icon], DROPNEXT);
            } else if (e.target.classList.contains(DROPPABLE)) {
                addClass([icon], DROPIN);
            } else {
                addClass([icon], DROPOUT);
            }
        } else {
            addClass([icon], NODROP);
            document.body.style.cursor = 'not-allowed';
        }
        const listItem: Element = closest(e.target, LISTITEM);
        let level: number;
        if (listItem) {
            level = parseInt(listItem.getAttribute('aria-level'), 10);
        }
        const eventArgs: DragAndDropEventArgs = this.getDragEvent(e.event, this, e.target, e.target, null, virtualEle, level);
        if (eventArgs.dropIndicator) {
            removeClass([icon], eventArgs.dropIndicator);
        }
        this.iconElement = icon;
        this.draggingEventArgs = eventArgs;
        const left: number = this.getXYValue(e.event, 'X');
        const top: number = this.getXYValue(e.event, 'Y');
        if (this.options.draggedEvent) {
            this.dotNetRef.invokeMethodAsync('TriggerNodeDraggingEvent', this.updateObjectValues(eventArgs), left, top);
        }
        else {
            this.nodeDragging();
        }
    }

    public nodeDragging(): void {
        if (this.draggingEventArgs.dropIndicator) {
            addClass([this.iconElement], this.draggingEventArgs.dropIndicator);
        }
    }

    private renderVirtualEle(e: DragEventArgs): void {
        const offsetY: number = this.getOffsetValue(e, 'Y');
        let previous: boolean;
        if (offsetY > e.target.offsetHeight - 2) {
            previous = false;
        } else if (offsetY < 2) {
            previous = true;
        }
        const element: Element = createElement('div', { className: SIBLING });
        const index: number = this.options.fullRowSelect ? (previous ? 1 : 2) : (previous ? 0 : 1);
        this.dropTargetElement = e.target.children[index as number];
        e.target.insertBefore(element, e.target.children[index as number]);
    }

    private parents(element: Element | Node, selector: string): Element[] {
        const matched: Element[] = [];
        let node: Element = <Element>element.parentNode;
        while (!isNOU(node)) {
            if (matches(node, selector)) {
                matched.push(node);
            }
            node = <Element>node.parentNode;
        }
        return matched;
    }

    private getDragEvent(event: MouseEvent & TouchEvent, obj: SfTreeView, dropTarget: Element, target: HTMLElement,
                         dragNode?: HTMLLIElement, cloneEle?: HTMLElement, level?: number, drop?: boolean): DragAndDropEventArgs {
        const dropLi: Element = dropTarget ? closest(dropTarget, '.' + LISTITEM) : null;
        const dropData: { [key: string]: Object } = dropLi ? this.getNodeData(dropLi) : null;
        const draggedNode: HTMLLIElement = obj ? obj.dragLi as HTMLLIElement : dragNode;
        const draggedNodeData: { [key: string]: Object } = obj ? obj.dragData : null;
        const newParent: Element[] = dropTarget ? this.parents(dropTarget, '.' + LISTITEM) : null;
        const dragLiParent: Element = obj.dragLi.parentElement;
        let dragParent: Element = obj.dragLi && dragLiParent ? closest(dragLiParent, '.' + LISTITEM) : null;
        let targetParent: Element = null;
        let indexValue: number = null;
        const iconCss: string[] = [DROPNEXT, DROPIN, DROPOUT, NODROP];
        let iconClass: string = null;
        const node: Element = drop ? draggedNode : dropLi;
        const index: Element = node ? closest(node, '.e-list-parent') : null;
        let i: number = 0;
        dragParent = (obj.dragLi && dragParent === null && dragLiParent) ? closest(dragLiParent, '.' + ROOT) : dragParent;
        dragParent = drop ? this.dragParent : dragParent;
        if (cloneEle) {
            while (i < 4) {
                if (select('.' + ICON, cloneEle).classList.contains(iconCss[i as number])) {
                    iconClass = iconCss[i as number];
                    break;
                }
                i++;
            }
        }
        if (index) {
            let dragIndex: number;
            let treeNodes: Element[] = [];
            const hasvalidDropLi: boolean = (!isNOU(dropLi) && dropLi.classList.length > 1);
            if (hasvalidDropLi) {
                treeNodes = <NodeListOf<Element> & Element[]>obj.element.querySelectorAll('.' + dropLi.classList[1]);
            }
            for (let i: number = 0; i < treeNodes.length; i++) {
                if (treeNodes[i as number] === dropLi) { indexValue = i; }
                if (treeNodes[i as number] === draggedNode) { dragIndex = i; }
                if (!isNOU(dragIndex) && !isNOU(indexValue)) { break; }
            }
            if (!isNOU(this.sibEle)) {
                if (hasvalidDropLi && draggedNode.classList[1] === dropLi.classList[1]) {
                    indexValue = this.isUp ? indexValue - 1 : indexValue;
                }
                else {
                    if (indexValue === (treeNodes.length - 1)) { indexValue = !this.isUp ? treeNodes.length : indexValue; }
                    else { indexValue = this.isUp ? indexValue : indexValue + 1; }
                }
                if (dragIndex > indexValue) { indexValue += 1; }
            } else {
                if (dragIndex > indexValue) { indexValue += 1; }
                //For drop the node to different level
                if (hasvalidDropLi && draggedNode.classList[1] !== dropLi.classList[1]) {
                    indexValue = indexValue !== 0 ? (indexValue === treeNodes.length - 1 ? treeNodes.length : indexValue + 1) : 0;
                }
                else if (event.offsetY <= 5 && indexValue !== 0) { indexValue -= 1; }
                if (iconClass === DROPIN) {
                    indexValue = 0;
                    if (hasvalidDropLi && dropLi.classList.contains('e-has-child')) {
                        const level: number = parseInt(dropLi.getAttribute('aria-level'), 10) + 1;
                        const nodeArray: Element[] = <NodeListOf<HTMLLIElement> & Element[]>dropLi.querySelectorAll('[aria-level="' + level + '"]');
                        const tempArray: Element[] = Array.from(nodeArray);
                        indexValue = tempArray.findIndex((data: Element) => data === draggedNode) === -1 ?
                            nodeArray.length : nodeArray.length - 1;
                    }
                }
            }
            if (iconClass == null || iconClass === NODROP) { indexValue = null; }
        }
        if (dropTarget) {
            if (newParent.length === 0) {
                targetParent = null;
            } else if (dropTarget.classList.contains(LISTITEM)) {
                targetParent = newParent[0];
            } else {
                targetParent = newParent[1];
            }
        }
        if (dropLi === draggedNode) { targetParent = dropLi; }
        if (dropTarget && target.offsetHeight <= 33 && event.offsetY < target.offsetHeight - 10 && event.offsetY > 6) {
            targetParent = dropLi;
            if (!drop) {
                level = ++level;
                const parent: Element = targetParent ? select('.e-list-parent', targetParent) : null;
                indexValue = (parent) ? parent.children.length : 0;
            }
        }
        return {
            cancel: false,
            clonedNode: cloneEle,
            event: event,
            draggedNode: draggedNode,
            draggedNodeData: draggedNodeData,
            droppedNode: dropLi as HTMLLIElement,
            droppedNodeData: dropData,
            dropIndex: indexValue,
            dropLevel: level,
            draggedParentNode: dragParent,
            dropTarget: targetParent,
            dropIndicator: iconClass,
            target: target
        };
    }

    private editingHandler(e: MouseEvent): void {
        const target: Element = <Element>e.target;
        if (!target || target.classList.contains(ROOT) || target.classList.contains(PARENTITEM) ||
            target.classList.contains(LISTITEM) || target.classList.contains(ICON) ||
            target.classList.contains(INPUT) || target.classList.contains(INPUTGROUP)) {
            return;
        } else {
            this.createTextbox(closest(target, '.' + LISTITEM), e);
        }
    }

    private createTextbox(liEle: Element, e: MouseEvent | KeyboardEventArgs): void {
        this.editEventArgs = this.getEditEvent(liEle, null, null);
        addClass([liEle], EDITING);
        this.isEdited = true;
        if (this.options.allowDragAndDrop) {
            this.dragObj.intDestroy(null);
            this.destroyDrag();
        }
        this.dotNetRef.invokeMethodAsync('TriggerNodeEditingEvent', this.editEventArgs);
    }

    private getEditEvent(liEle: Element, newText: string, inputEle: string): NodeEditEventArgs {
        const data: { [key: string]: Object } = this.getNodeData(liEle);
        return { newText: newText, nodeData: data, oldText: this.oldText, innerHtml: inputEle };
    }

    private focusIn(): void {
        if (document.activeElement.nodeName === 'INPUT' || document.activeElement.nodeName === 'TEXTAREA') {
            const inputElement: HTMLInputElement = <HTMLInputElement>document.activeElement;
            this.updateOldText(inputElement.value);
        }
        if (!this.mouseDownStatus) {
            addClass([this.getFocusedNode()], FOCUS);
        }
        this.mouseDownStatus = false;
    }

    private focusOut(): void {
        this.removeHover();
        removeClass([this.getFocusedNode()], FOCUS);
    }

    private touchStart(e: TouchEvent): void {
        this.startTime = e.timeStamp;
    }

    private touchEnd(e: TouchEvent): void {
        this.endTime = e.timeStamp;
        const touchDifference: number = this.endTime - this.startTime;
        if (touchDifference > 500 && !this.dragStartAction) {
            const target: Element = <Element>e.target;
            const li: Element = closest(target, '.' + LISTITEM);
            e.preventDefault();
            this.toggleSelect(li, e, true);
        }
    }

    private getParentElement(element: HTMLElement): HTMLElement {
        let parentEle: HTMLElement = element.parentElement;
        if (window.getComputedStyle(parentEle).resize !== 'none') {
            return parentEle;
        }
        else if (parentEle.nodeName !== 'BODY') {
            parentEle = this.getParentElement(parentEle);
        }
        return parentEle;
    }

    private resizeHandler(e: MouseEvent): void {
        switch (e.type) {
        case 'mousedown':
            this.isParentMouseDown = true;
            break;
        case 'mouseup':
            this.isParentMouseDown = false;
            break;
        case 'mousemove':
            if (this.isParentMouseDown) {
                this.updateWrap();
            }
            break;
        }
    }

    public wireEvents(): void {
        if (!this.options.disabled) {
            this.setExpandOnType();
            if (this.options.allowTextWrap) {
                const parentElement: HTMLElement = this.getParentElement(this.element);
                if (parentElement.nodeName !== 'BODY') {
                    EventHandler.add(parentElement, 'mousedown mouseup mousemove', this.resizeHandler, this);
                }
            }
            EventHandler.add(this.element, MOUSEDOWN, this.mouseDownHandler, this);
            EventHandler.add(this.element, 'click', this.clickHandler, this);
            EventHandler.add(this.element, MOUSEOVER, this.onMouseOver, this);
            EventHandler.add(this.element, FOCUSING, this.focusIn, this);
            EventHandler.add(this.element, BLUR, this.focusOut, this);
            EventHandler.add(this.element, MOUSEOUT, this.onMouseLeave, this);
            EventHandler.add(this.element, 'contextmenu', this.contextLongPress, this);
            if (Browser.isDevice && this.options.allowMultiSelection) {
                EventHandler.add(this.element, 'touchstart', this.touchStart, this);
                EventHandler.add(this.element, 'touchend', this.touchEnd, this);
            }
            if (this.options.showCheckBox) {
                const frame: Element = select('.' + CHECKBOXFRAME, this.element);
                if (!isNOU(frame)) {
                    EventHandler.add(frame, 'mousedown', this.frameMouseHandler, this);
                    EventHandler.add(frame, 'mouseup', this.frameMouseHandler, this);
                }
            }
            if (this.options.expandOnType !== EXPANDONNONE) {
                this.wireExpandOnEvent(true);
            }
            this.keyboardModule = new KeyboardEvents(
                this.element,
                {
                    keyAction: this.keyboardActionHandler.bind(this),
                    keyConfigs: this.keyConfigs,
                    eventName: 'keydown'
                }
            );
            if (this.element.classList.contains('e-virtualization')) {
                EventHandler.add(this.element, 'scroll wheel', this.virtualScrollHandler, this);
            }
        }
    }

    private virtualScrollHandler(): void {
        const maskParent: HTMLElement = select('.e-mask-parent', this.element);
        const ulElement: HTMLElement = select('.e-list-parent', this.element);
        if (Math.abs(this.prevScrollValue - this.element.scrollTop) > 150) {
            maskParent.style.display = 'block';
            ulElement.style.visibility = 'hidden';
            let height: number = this.element.scrollTop;
            if (this.prevScrollValue > this.element.scrollTop) {
                height -= 300;
            }
            maskParent.style.transform = 'translate(0px,' + height + 'px)';
            setTimeout(function (): void {
                maskParent.style.display = 'none';
                maskParent.style.transform = '';
                ulElement.style.visibility = '';

            }, 200);
        }
        this.prevScrollValue = this.element.scrollTop;
    }

    private frameMouseHandler(e: MouseEvent): void {
        const rippleSpan: Element = select('.' + CHECKBOXRIPPLE, (e.target as Element).parentElement);
        this.rippleMouseHandler(e, rippleSpan);
    }
    private rippleMouseHandler(e: MouseEvent, rippleSpan: Element): void {
        if (rippleSpan) {
            const event: MouseEvent = new MouseEvent(e.type, {
                bubbles: false,
                cancelable: true
            });
            rippleSpan.dispatchEvent(event);
        }
    }
    private setExpandOnType(): void {
        const expandOnType: string = this.options.expandOnType;
        this.options.expandOnType = (expandOnType === EXPANDONAUTO) ? (Browser.isDevice ? CLICK : DBLCLICK) : expandOnType;
    }
    private expandHandler(e: TapEventArgs): void {
        const target: Element = <Element>e.originalEvent.target;
        if (!target || target.classList.contains(INPUT) || target.classList.contains(ROOT) ||
            target.classList.contains(PARENTITEM) || target.classList.contains(LISTITEM) ||
            target.classList.contains(ICON) || this.options.showCheckBox && closest(target, '.' + CHECKBOXWRAP)) {
            return;
        } else {
            this.expandCollapseAction(closest(target, '.' + LISTITEM), e);
        }
    }
    public handleCollapseCallback(expandArgs: NodeExpandEventArgs, fromClickHandler: boolean): void {
        (this as any).dotNetRef.invokeMethodAsync('NodeCollapsingEventCallback', expandArgs).then((args: NodeExpandEventArgs) => {
            if (!isNOU(args)) {
                const currentLi: Element = this.element.querySelector('[data-uid="' + args.nodeData.id + '"]');
                this.collapseAction(currentLi, null, fromClickHandler, args.cancel);
            }
        });
    }
    private expandCollapseAction(currLi: Element, e: TapEventArgs): void {
        const icon: Element = select('div.' + ICON, currLi);
        if (!icon || icon.classList.contains(PROCESS)) {
            return;
        } else {
            if (icon.classList.contains(EXPANDABLE)) {
                this.expandAction(currLi, e);
            } else if (icon.classList.contains(COLLAPSIBLE)) {
                this.tapEvent = e;
                this.expandArgs = this.getExpandEvent(currLi, e);
                this.handleCollapseCallback(this.expandArgs, false);
            }
        }
    }

    private animateHeight(args: AnimationOptions, start: number, end: number): void {
        const remaining: number = (args.duration - args.timeStamp) / args.duration;
        const currentHeight: number = (end - start) * remaining + start;
        args.element.parentElement.style.height = currentHeight + 'px';
    }

    public expandAction(currLi: Element, e: TapEventArgs | KeyboardEventArgs | MouseEventArgs): void {
        this.expandArgs = this.getExpandEvent(currLi, e);
        if (this.options.allowTextWrap) {
            const ul: HTMLElement = <HTMLElement>select('.' + PARENTITEM, currLi);
            this.isNodeRendered = ul ? true : false;
        }
        if (currLi && currLi.classList.contains(PROCESS)) { removeClass([currLi], PROCESS); }
        this.dotNetRef.invokeMethodAsync('TriggerNodeExpandingEvent', this.expandArgs);
    }

    public collapseAction(currLi: Element, e: TapEventArgs | KeyboardEventArgs | MouseEventArgs,
                          fromClickHandler: boolean, cancel?: boolean): void {
        if (isNOU(e)) { e = this.tapEvent; }
        this.expandArgs = this.getExpandEvent(currLi, e);
        if (!cancel) {
            let start: number = 0;
            let end: number = 0;
            // eslint-disable-next-line
            const proxy: SfTreeView = this;
            const ul: HTMLElement = <HTMLElement>select('.' + PARENTITEM, currLi);
            const liEle: HTMLElement = <HTMLElement>currLi;
            const activeElement: HTMLElement = <HTMLElement>select('.' + LISTITEM + '.' + ACTIVE, currLi);
            if (ul) {
                const icon: Element = select('div.' + ICON, liEle);
                if (!isNOU(icon)) {
                    removeClass([icon], COLLAPSIBLE);
                    addClass([icon], EXPANDABLE);
                }
            }
            if (!isNOU(currLi.getAttribute('aria-expanded'))) {
                currLi.setAttribute('aria-expanded', 'false');
            }
            if (this.options.animation.collapse.duration === 0) {
                ul.style.display = NONE;
                proxy.dotNetRef.invokeMethodAsync('TriggerNodeCollapsingEvent', proxy.expandArgs);
                if (fromClickHandler){
                    proxy.triggerClickEvent(e as any, currLi);
                }
            } else {
                this.addAnimatingUl(ul);
                this.animationObj.animate(ul, {
                    name: (<Effect>this.options.animation.collapse.effect === <Effect>'None' && animationMode === 'Enable') ? <Effect>'SlideUp' : this.options.animation.collapse.effect,
                    duration: this.options.animation.collapse.duration,
                    timingFunction: this.options.animation.collapse.easing,
                    begin: (args: AnimationOptions): void => {
                        proxy.isAnimationCompleted = false;
                        if (!this.element.classList.contains('e-virtualization')) {
                            liEle.style.overflow = HIDDEN;
                        }
                        if (!isNOU(activeElement) && activeElement instanceof HTMLElement) {
                            activeElement.classList.add(ITEM_ANIMATION_ACTIVE);
                        }
                        start = (<HTMLElement>select('.' + TEXTWRAP, currLi)).offsetHeight;
                        end = liEle.offsetHeight;
                    },
                    progress: (args: AnimationOptions): void => {
                        proxy.animateHeight(args, start, end);
                    },
                    end: (args: AnimationOptions): void => {
                        proxy.removeAnimatingUl(ul);
                        args.element.style.display = NONE;
                        if (!isNOU(activeElement) && activeElement instanceof HTMLElement) {
                            activeElement.classList.remove(ITEM_ANIMATION_ACTIVE);
                        }
                        proxy.dotNetRef.invokeMethodAsync('TriggerNodeCollapsingEvent', proxy.expandArgs);
                        if (fromClickHandler){
                            proxy.triggerClickEvent(e as any, currLi);
                        }
                        proxy.isAnimationCompleted = true;
                    }
                });
            }
        }
    }

    private wireExpandOnEvent(toBind: boolean): void {
        if (toBind) {
            // eslint-disable-next-line
            const proxy: SfTreeView = this;
            this.touchExpandObj = new Touch(this.element, {
                tap: (e: TapEventArgs) => {
                    if ((this.options.expandOnType === CLICK || (this.options.expandOnType === DBLCLICK
                        &&  this.isDoubleTapped(e) && e.tapCount === 2 ))
                        && e.originalEvent.which !== 3) {
                        proxy.expandHandler(e);
                    }
                }
            });
        } else {
            if (this.touchExpandObj) {
                this.touchExpandObj.destroy();
            }
        }
    }

    private getNodeData(currLi: Element, fromDS?: boolean): { [key: string]: Object } {
        if (!isNOU(currLi) && currLi.classList.contains(LISTITEM) &&
            !isNOU(closest(currLi, '.' + CONTROL)) && closest(currLi, '.' + CONTROL).classList.contains(ROOT)) {
            const id: string = currLi.getAttribute('data-uid');
            const pNode: Element = closest(currLi.parentNode, '.' + LISTITEM);
            const pid: string = pNode ? pNode.getAttribute('data-uid') : null;
            const selected: boolean = currLi.classList.contains(ACTIVE);
            const expanded: boolean = (currLi.getAttribute('aria-expanded') === 'true');
            const hasChildren: boolean = (currLi.getAttribute('aria-expanded') === null);
            let checked: string = null;
            if (this.options.showCheckBox) {
                checked = select('.' + CHECKBOXWRAP, currLi).getAttribute('aria-checked');
            }
            return {
                id: id, text: null, parentID: pid, selected: selected, expanded: expanded,
                isChecked: checked, hasChildren: hasChildren
            };
        }
        return { id: EMPTY, text: EMPTY, parentID: EMPTY, selected: false, expanded: false, isChecked: EMPTY, hasChildren: false };
    }

    private getExpandEvent(currLi: Element, e: MouseEvent | KeyboardEventArgs | TapEventArgs | MouseEventArgs): NodeExpandEventArgs {
        const nodedata: { [key: string]: Object } = this.getNodeData(currLi);
        return { isInteracted: !isNOU(e), nodeData: nodedata, event: e, isLoaded: currLi.querySelector('.' + UL) != null ? true : false, nodeLevel: parseInt(currLi.getAttribute('aria-level'), 10) };
    }

    public updateSpinnerClass(): void {
        const spinnerEle: Element = this.element.querySelector('.e-icons-spinner');
        if (spinnerEle){
            removeClass([spinnerEle], 'e-icons-spinner');
        }
    }

    public expandedNode(expandArgs: NodeExpandEventArgs): void {
        const li: Element = this.element.querySelector('[data-uid="' + expandArgs.nodeData.id + '"]');
        if (isNOU(li)) {
            return;
        }
        this.focussedElement = li;
        const ulele: HTMLElement = <HTMLElement>select('.' + PARENTITEM, li);
        if (ulele) {
            ulele.classList.remove(DISPLAYNONE);
            const icon: Element = select('div.' + ICON, li);
            this.expandArgs = this.getExpandEvent(li, expandArgs.event);
            const liEle: HTMLElement = <HTMLElement>li;
            const activeElement: HTMLElement = <HTMLElement>select('.' + LISTITEM + '.' + ACTIVE, li);
            let start: number = 0;
            let end: number = 0;
            // eslint-disable-next-line
            const proxy: SfTreeView = this;
            this.setHeight(liEle, ulele);
            if (this.options.animation.expand.duration === 0) {
                if (!isNOU(icon)) {
                    removeClass([icon], EXPANDABLE);
                    addClass([icon], COLLAPSIBLE);
                }
                proxy.updateSpinnerClass();
                proxy.dotNetRef.invokeMethodAsync('TriggerNodeExpandedEvent', proxy.expandArgs);
                ulele.style.display = BLOCK;
                liEle.style.display = BLOCK;
                liEle.style.overflow = EMPTY;
                liEle.style.height = EMPTY;
                removeClass([icon], 'e-icons-spinner');
                if (this.options.allowTextWrap) {
                    if (!this.isNodeRendered || this.isEdited) {
                        this.isEdited = false;
                        this.updateWrap(ulele);
                    } else if (this.isNodeRendered) {
                        this.updateWrap();
                    }
                }
            } else {
                this.addAnimatingUl(ulele);
                this.animationObj.animate(ulele, {
                    name: (<Effect>this.options.animation.expand.effect === <Effect>'None' && animationMode === 'Enable') ? <Effect>'SlideDown' : this.options.animation.expand.effect,
                    duration: this.options.animation.expand.duration,
                    timingFunction: this.options.animation.expand.easing,
                    begin: (args: AnimationOptions): void => {
                        proxy.isAnimationCompleted = false;
                        if (!this.element.classList.contains('e-virtualization')) {
                            liEle.style.overflow = HIDDEN;
                        }
                        if (!isNOU(activeElement) && activeElement instanceof HTMLElement) {
                            activeElement.classList.add(ITEM_ANIMATION_ACTIVE);
                        }
                        start = liEle.offsetHeight;
                        end = (<HTMLElement>select('.' + TEXTWRAP, li)).offsetHeight;
                    },
                    progress: (args: AnimationOptions): void => {
                        removeClass([icon], EXPANDABLE);
                        addClass([icon], COLLAPSIBLE);
                        args.element.style.display = BLOCK;
                        proxy.animateHeight(args, start, end);
                    },
                    end: (args: AnimationOptions): void => {
                        proxy.removeAnimatingUl(ulele);
                        if (proxy.element && proxy.element.closest('.e-ddt') && !proxy.element.closest('.e-popup-open')) {
                            return;
                        }
                        args.element.style.display = BLOCK;
                        if (!isNOU(activeElement) && activeElement instanceof HTMLElement) {
                            activeElement.classList.remove(ITEM_ANIMATION_ACTIVE);
                        }
                        proxy.updateSpinnerClass();
                        proxy.dotNetRef.invokeMethodAsync('TriggerNodeExpandedEvent', proxy.expandArgs);
                        ulele.style.display = BLOCK;
                        liEle.style.display = BLOCK;
                        liEle.style.overflow = EMPTY;
                        liEle.style.height = EMPTY;
                        removeClass([icon], 'e-icons-spinner');
                        if (this.options.allowTextWrap) {
                            if (!this.isNodeRendered || this.isEdited) {
                                this.isEdited = false;
                                this.updateWrap(ulele);
                            } else if (this.isNodeRendered) {
                                this.updateWrap();
                            }
                        }
                        proxy.isAnimationCompleted = true;
                    }
                });
            }
        }
        if (!ulele)
        {
            this.expandArgs = this.getExpandEvent(li, expandArgs.event);
            this.updateSpinnerClass();
            this.dotNetRef.invokeMethodAsync('TriggerNodeExpandedEvent', this.expandArgs);
        }
        this.setHover(this.getFocusedNode());
    }

    private setHeight(currli: HTMLElement, ul: HTMLElement): void {
        ul.style.display = BLOCK;
        ul.style.visibility = HIDDEN;
        currli.style.height = currli.offsetHeight + 'px';
        ul.style.display = NONE;
        ul.style.visibility = EMPTY;
    }

    public collapsedNode(collapseArgs: NodeExpandEventArgs): void {
        const li: HTMLElement = this.element.querySelector('[data-uid="' + collapseArgs.nodeData.id + '"]');
        if (isNOU(li)) {
            return;
        }
        this.focussedElement = li;
        const ulelement: HTMLElement = li.querySelector('.' + UL);
        if (ulelement) {
            ulelement.style.display = NONE;
            ulelement.classList.add(DISPLAYNONE);
        }
        li.style.overflow = EMPTY;
        li.style.height = EMPTY;
        this.expandArgs = this.getExpandEvent(li, null);
        const icon: Element = select('div.' + ICON, li);
        if (!isNOU(icon)) {
            removeClass([icon], COLLAPSIBLE);
            addClass([icon], EXPANDABLE);
            if (this.options.nodeCollapsedEvent) {
                this.dotNetRef.invokeMethodAsync('TriggerNodeCollapsedEvent', this.expandArgs);
            }
        }
    }

    private preventContextMenu(e: MouseEvent): void {
        e.preventDefault();
    }

    private contextLongPress(event: TouchEventArgs): void {
        const target: Element = <Element>event.target;
        const eventArgs: NodeClickEventArgs = {
            event: event,
            node: null
        };
        const li: Element = closest(target, '.' + LISTITEM);
        const mouseEventArgs: SerializableMouseEvent = this.getSerializableMouseEvent(event);
        this.dotNetRef.invokeMethodAsync('TriggerNodeClickingEvent', eventArgs, mouseEventArgs, li.getAttribute('data-uid'), this.getXYValue(eventArgs.event, 'X'), this.getXYValue(eventArgs.event, 'Y'));
    }

    private clickHandler(event: MouseEventArgs): void {
        if (this.isClickSuppressedAfterDrop) {
            this.isClickSuppressedAfterDrop = false;
            return;
        }
        this.tapEvent = event;
        const target: Element = <Element>event.target;
        const currLi: Element = closest(target, '.' + LISTITEM) as Element;
        if (isNOU(currLi)) { return; }
        const iconEle: Element = closest(target, '.' + EXPANDABLE + ',.' + COLLAPSIBLE) as Element;
        if (!isNOU(iconEle)) {
            const childUl: HTMLElement = select('.' + PARENTITEM, currLi) as HTMLElement;
            if (!isNOU(childUl) && this.isUlAnimating(childUl)) {
                return;
            }
        }
        let isCollapsAction: boolean = false;
        EventHandler.remove(this.element, 'contextmenu', this.preventContextMenu);
        if (!target) {
            return;
        } else {
            if (target.nodeName === 'INPUT' || target.nodeName === 'TEXTAREA') {
                const inputElement: HTMLInputElement = <HTMLInputElement>target;
                this.updateOldText(inputElement.value);
            }
            const classList: DOMTokenList = target.classList;
            const li: Element = closest(target, '.' + LISTITEM);
            if (!li) {
                return;
            } else if (event.which !== 3) {
                const rippleElement: Element = select('.' + RIPPLEELMENT, li);
                const rippleIcons: Element = select('.' + ICON, li);
                this.removeHover();
                this.focussedElement = li;
                this.setFocusElement(li);
                if (this.options.showCheckBox && !li.classList.contains(DISABLE)) {
                    const checkContainer: HTMLElement = closest(target, '.' + CHECKBOXWRAP) as HTMLElement;
                    if (!isNOU(checkContainer)) {
                        const checkElement: Element = select('.' + CHECKBOXFRAME, checkContainer);
                        this.validateCheckNode(checkContainer, checkElement.classList.contains(CHECK), li, event);
                        this.triggerClickEvent(event, li);
                        return;
                    }
                }
                if (classList.contains(EXPANDABLE)) {
                    this.expandAction(li, event);
                } else if (classList.contains(COLLAPSIBLE)) {
                    this.expandArgs = this.getExpandEvent(li, event);
                    this.handleCollapseCallback(this.expandArgs, true);
                    isCollapsAction = true;
                } else if (rippleElement && rippleIcons) {
                    if (rippleIcons.classList.contains(RIPPLE) && rippleIcons.classList.contains(EXPANDABLE)) {
                        this.expandAction(li, event);
                    } else if (rippleIcons.classList.contains(RIPPLE) && rippleIcons.classList.contains(COLLAPSIBLE)) {
                        this.collapseAction(li, event, true);
                        isCollapsAction = true;
                    } else if (!classList.contains(PARENTITEM) && !classList.contains(LISTITEM)) {
                        this.toggleSelect(li, event, false);
                    }
                } else {
                    if (!classList.contains(PARENTITEM) && !classList.contains(LISTITEM)) {
                        this.toggleSelect(li, event, false);
                    }
                }
            }
            if (!isCollapsAction) {
                this.triggerClickEvent(event, li);
            }

        }
    }

    private getXYValue(e: MouseEvent | TouchEvent, direction: string): number {
        const touchList: TouchList = (e as TouchEvent).changedTouches;
        let value: number;
        if (direction === 'X') {
            value = touchList ? touchList[0].clientX : (e as MouseEvent).clientX;
        } else {
            value = touchList ? touchList[0].clientY : (e as MouseEvent).clientY;
        }
        if (!value && e.type === 'focus' && e.target) {
            const rect: ClientRect = (e.target as HTMLElement).getBoundingClientRect();
            value = rect ? (direction === 'X' ? rect.left : rect.top) : null;
        }
        return Math.ceil(value);
    }

    private triggerClickEvent(e: MouseEvent | MouseEventArgs, li: Element): void {
        const eventArgs: NodeClickEventArgs = {
            event: e,
            node: null
        };
        const mouseEventArgs: SerializableMouseEvent = this.getSerializableMouseEvent(e);
        this.dotNetRef.invokeMethodAsync('TriggerNodeClickingEvent', eventArgs, mouseEventArgs, li.getAttribute('data-uid'), this.getXYValue(e, 'X'), this.getXYValue(e, 'Y'));
    }

    private getSerializableMouseEvent(e: MouseEvent | TouchEventArgs): SerializableMouseEvent {
        return {
            detail: e.detail || 0,
            screenX: e.screenX || 0,
            screenY: e.screenY || 0,
            clientX: e.clientX || 0,
            clientY: e.clientY || 0,
            offsetX: e.offsetX || 0,
            offsetY: e.offsetY || 0,
            pageX: e.pageX || 0,
            pageY: e.pageY || 0,
            movementX: e.movementX || 0,
            movementY: e.movementY || 0,
            button: e.button,
            buttons: e.buttons || 0,
            ctrlKey: e.ctrlKey || false,
            shiftKey: e.shiftKey || false,
            altKey: e.altKey || false,
            metaKey: e.metaKey || false,
            type: e.type || 'click'
        };
    }

    private getCheckEvent(currLi: Element, action: string, e: MouseEvent | KeyboardEventArgs): NodeCheckEventArgs {
        return { action: action, isInteracted: !isNOU(e), nodeData: this.getNodeData(currLi) };
    }

    private validateCheckNode(checkWrap: HTMLElement | Element, isCheck: boolean,
                              li: HTMLElement | Element, e: KeyboardEventArgs | MouseEvent): void {
        const currLi: Element = closest(checkWrap, '.' + LISTITEM);
        const ariaState: string = !isCheck ? 'true' : 'false';
        if (!isNOU(ariaState)) {
            checkWrap.setAttribute('role', 'checkbox');
            checkWrap.setAttribute('aria-checked', ariaState);
            const textElement: HTMLElement = currLi.querySelector('.e-list-text');
            if (textElement) {
                const textId: string = currLi.id + '_text';
                textElement.id = textId;
                checkWrap.setAttribute('aria-labelledby', textId);
            }
        }
        const eventArgs: NodeCheckEventArgs = this.getCheckEvent(currLi, isCheck ? 'uncheck' : 'check', e);
        this.dotNetRef.invokeMethodAsync('TriggerNodeCheckingEvent', eventArgs, null);
    }

    private toggleSelect(li: Element, e: MouseEvent | KeyboardEventArgs | TouchEvent | MouseEventArgs, multiSelect?: boolean): void {
        if (!li.classList.contains(DISABLE)) {
            if (this.options.allowMultiSelection && ((e && e.ctrlKey) || multiSelect) && li.classList.contains(ACTIVE)) {
                this.unselectNode(li, e, multiSelect);
            } else {
                this.selectNode(li, e, multiSelect);
                if (this.options.allowMultiSelection && e && (e.ctrlKey || e.shiftKey)) {
                    this.setFocusElement(li);
                    this.focussedElement = li;
                }
            }
        }
    }

    private unselectNode(li: Element, e: MouseEvent | KeyboardEventArgs | TouchEvent, multiSelect: boolean): void {
        const eventArgs: NodeSelectEventArgs = this.getSelectEvent(li, 'un-select', e, multiSelect, []);
        this.dotNetRef.invokeMethodAsync('TriggerNodeSelectingEvent', eventArgs);
    }

    private getSelectEvent(currLi: Element, action: string, e: MouseEvent | KeyboardEventArgs | TouchEvent,
                           multiSelect: boolean, nodes: string[]): NodeSelectEventArgs {
        const detail: { [key: string]: Object } = this.getNodeData(currLi);
        return { action: action, isInteracted: !isNOU(e), nodeData: detail,
            isMultiSelect: multiSelect, isCtrKey: !isNOU(e) && e.ctrlKey ?
                true : false, isShiftKey: !isNOU(e) && e.shiftKey ? true : false, nodes: nodes };
    }

    private selectNode(li: Element, e: MouseEvent | KeyboardEventArgs | TouchEvent, multiSelect?: boolean): void {
        if (isNOU(li) || (!this.options.allowMultiSelection && li.classList.contains(ACTIVE) && !isNOU(e))) {
            this.setFocusElement(li);
            this.focussedElement = li;
            return;
        }
        const array: string[] = [];
        if (this.options.allowMultiSelection && e && e.shiftKey) {
            const activeElements: HTMLElement[] = selectAll('.' + LISTITEM + '.' + ACTIVE, this.element);
            const activeLen: number = activeElements ? activeElements.length : 0;
            if (!this.startNode) {
                this.startNode = activeLen > 0 ? activeElements[activeLen - 1] : li;
            }
            const liList: HTMLElement[] = Array.prototype.slice.call(selectAll('.' + LISTITEM, this.element));
            let startIndex: number = liList.indexOf(<HTMLElement>this.startNode);
            let endIndex: number = liList.indexOf(<HTMLElement>li);
            if (startIndex > endIndex) {
                [startIndex, endIndex] = [endIndex, startIndex];
            }
            for (let i: number = startIndex; i <= endIndex; i++) {
                const currNode: Element = liList[i as number];
                if (isVisible(currNode) && !currNode.classList.contains(DISABLE)) {
                    array.push(currNode.getAttribute('data-uid'));
                }
            }
        } else {
            this.startNode = li;
        }
        const eventArgs: NodeSelectEventArgs = this.getSelectEvent(li, 'select', e, multiSelect, array);
        this.dotNetRef.invokeMethodAsync('TriggerNodeSelectingEvent', eventArgs);
    }

    private setFocusElement(li: Element): void {
        if (!isNOU(li)) {
            const focusedNode: Element = this.getFocusedNode();
            if (focusedNode) {
                removeClass([focusedNode], FOCUS);
                if (!Browser.isDevice) {
                    focusedNode.setAttribute('tabindex', '-1');
                }
            }
            addClass([li], FOCUS);
            if (!Browser.isDevice) {
                li.setAttribute('tabindex', '0');
            }
            this.focussedElement = li;
            this.updateIdAttr(focusedNode, li);
        }
    }

    private updateIdAttr(preNode: Element, nextNode: Element): void {
        this.element.removeAttribute('aria-activedescendant');
        const idArray: Element[] = <NodeListOf<HTMLLIElement> & Element[]>this.element.querySelectorAll('[id=_active]');
        if (idArray[0] !== preNode) {
            idArray.forEach((element: Element) => {
                element.removeAttribute('id');
            });
        }
        if (preNode) {
            preNode.removeAttribute('id');
        }
        nextNode.setAttribute('id', this.element.id + '_active');
        this.element.setAttribute('aria-activedescendant', this.element.id + '_active');
    }

    private getFocusedNode(): Element {
        let selectedItem: Element;
        let fNode: Element;
        fNode = select('.' + LISTITEM + '[tabindex="0"]', this.element);
        if (!this.isKeyUp && isNOU(fNode)) { fNode = select('.' + LISTITEM + '.' + ACTIVE, this.element); }
        if (isNOU(fNode)){
            fNode = this.focussedElement ? this.focussedElement :
                select('.' + LISTITEM + '.' + FOCUS, this.element);
        }
        if (isNOU(fNode)) { selectedItem = select('.' + LISTITEM, this.element); }
        return isNOU(fNode) ? (isNOU(selectedItem) ? this.element.firstElementChild : selectedItem) : fNode;
    }

    public setFullRow(isEnabled: boolean): void {
        (isEnabled ? addClass : removeClass)([this.element], FULLROWWRAP);
        this.options.fullRowSelect = isEnabled;
    }

    private onMouseOver(e: MouseEvent): void {
        const target: Element = <Element>e.target;
        const classList: DOMTokenList = target.classList;
        const currentLi: Element = closest(target, '.' + LISTITEM);
        if (!currentLi || classList.contains(PARENTITEM) || classList.contains(LISTITEM)) {
            this.removeHover();
            return;
        } else {
            if (currentLi && !currentLi.classList.contains(DISABLE)) {
                this.setHover(currentLi);
            }
        }
    }

    private setHover(li: Element): void {
        if (!li.classList.contains(HOVER)) {
            this.removeHover();
            addClass([li], HOVER);
        }
    }

    private removeHover(): void {
        const hoveredNode: Element[] = selectAll('.' + HOVER, this.element);
        if (hoveredNode && hoveredNode.length) {
            removeClass(hoveredNode, HOVER);
        }
    }

    private checkNode(e: KeyboardEventArgs): void {
        const focusedNode: Element = this.getFocusedNode();
        const checkWrap: Element = select('.' + CHECKBOXWRAP, focusedNode);
        const isChecked: boolean = select(' .' + CHECKBOXFRAME, checkWrap).classList.contains(CHECK);
        if (!focusedNode.classList.contains(DISABLE) && focusedNode.getElementsByClassName('e-checkbox-disabled').length === 0) {
            this.validateCheckNode(checkWrap, isChecked, focusedNode, e);
        }
    }
    private openNode(toBeOpened: boolean, e: KeyboardEventArgs): void {
        const focusedNode: Element = this.getFocusedNode();
        const icon: Element = select('div.' + ICON, focusedNode);
        if (toBeOpened) {
            if (!icon) {
                return;
            } else if (icon.classList.contains(EXPANDABLE)) {
                this.expandAction(focusedNode, e);
            } else {
                this.focusNextNode(focusedNode, true);
            }
        } else {
            if (icon && icon.classList.contains(COLLAPSIBLE)) {
                this.tapEvent = e;
                this.expandArgs = this.getExpandEvent(focusedNode, e);
                this.handleCollapseCallback(this.expandArgs, false);
            } else {
                const parentLi: Element = closest(closest(focusedNode, '.' + PARENTITEM), '.' + LISTITEM);
                if (!parentLi) {
                    return;
                } else {
                    if (!parentLi.classList.contains(DISABLE)) {
                        this.setNodeFocus(focusedNode, parentLi);
                        this.navigateToFocus(true);
                        (<HTMLElement>parentLi).focus();
                    }
                }
            }
        }
    }
    private getScrollParent(node: Element): Element {
        if (isNOU(node)) {
            return null;
        }
        return (node.scrollHeight > node.clientHeight) ? node : this.getScrollParent(node.parentElement);
    }
    private navigateToFocus(isUp: boolean): void {
        const focusNode: Element = this.getFocusedNode().querySelector('.' + TEXTWRAP);
        const pos: ClientRect = focusNode.getBoundingClientRect();
        const parent: Element = this.getScrollParent(this.element);
        if (!isNOU(parent)) {
            const parentPos: ClientRect = parent.getBoundingClientRect();
            if (pos.bottom > parentPos.bottom) {
                parent.scrollTop += pos.bottom - parentPos.bottom;
            } else if (pos.top < parentPos.top) {
                parent.scrollTop -= parentPos.top - pos.top;
            }
        }
        const isVisible: boolean = this.isVisibleInViewport(focusNode);
        if (!isVisible) {
            focusNode.scrollIntoView(isUp);
        }
    }
    private isVisibleInViewport(txtWrap: Element): boolean {
        const pos: ClientRect = txtWrap.getBoundingClientRect();
        return (pos.top >= 0 && pos.left >= 0 && pos.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
        pos.right <= (window.innerWidth || document.documentElement.clientWidth));
    }
    private setNodeFocus(preNode: Element, nextNode: Element): void {
        removeClass([preNode], FOCUS);
        if (!Browser.isDevice) {
            preNode.setAttribute('tabindex', '-1');
        }
        if (!nextNode.classList.contains(DISABLE)) {
            this.focussedElement = nextNode;
            addClass([nextNode],  FOCUS);
            if (!Browser.isDevice) {
                nextNode.setAttribute('tabindex', '0');
            }
            this.updateIdAttr(preNode, nextNode);
        }
    }
    private focusNextNode(li: Element, isTowards: boolean): void {
        const nextNode: Element = isTowards ? this.getNextNode(li) : this.getPrevNode(li);
        this.setNodeFocus(li, nextNode);
        this.navigateToFocus(!isTowards);
        if (nextNode.classList.contains(DISABLE)) {
            const lastChild: HTMLElement  = nextNode.lastChild as HTMLElement;
            if (nextNode.previousSibling == null && nextNode.classList.contains('e-level-1')) {
                this.focusNextNode(nextNode, true);
            } else if (nextNode.nextSibling == null && nextNode.classList.contains('e-node-collapsed')) {
                this.focusNextNode(nextNode, false);
            } else if (nextNode.nextSibling == null && lastChild.classList.contains(TEXTWRAP)) {
                this.focusNextNode(nextNode, false);
            } else {
                this.focusNextNode(nextNode, isTowards);
            }
        }
        (<HTMLElement>nextNode).focus();
    }
    private shiftKeySelect(isTowards: boolean, e: KeyboardEventArgs): void {
        if (this.options.allowMultiSelection) {
            const focusedNode: Element = this.getFocusedNode();
            const nextNode: Element = isTowards ? this.getNextNode(focusedNode) : this.getPrevNode(focusedNode);
            this.removeHover();
            this.setFocusElement(nextNode);
            this.focussedElement = nextNode;
            this.toggleSelect(nextNode, e, false);
            this.navigateToFocus(!isTowards);
        } else {
            this.navigateNode(isTowards);
        }
    }

    private getNextNode(li: Element): Element {
        let index: number = this.liList.indexOf(<HTMLElement>li);
        let nextNode: Element;
        do {
            index++;
            nextNode = this.liList[index as number];
            if (isNOU(nextNode)) {
                return li;
            }
        }
        while (!isVisible(nextNode));
        return nextNode;
    }

    private getPrevNode(li: Element): Element {
        let index: number = this.liList.indexOf(<HTMLElement>li);
        let prevNode: Element;
        do {
            index--;
            prevNode = this.liList[index as number];
            if (isNOU(prevNode)) {
                return li;
            }
        }
        while (!isVisible(prevNode));
        return prevNode;
    }

    private getRootNode(): Element {
        let index: number = 0;
        let rootNode: Element;
        do {
            rootNode = this.liList[index as number];
            index++;
        }
        while (!isVisible(rootNode));
        return rootNode;
    }

    private getEndNode(): Element {
        let index: number = this.liList.length - 1;
        let endNode: Element;
        do {
            endNode = this.liList[index as number];
            index--;
        }
        while (!isVisible(endNode));
        return endNode;
    }
    private navigateNode(isTowards: boolean): void {
        this.focusNextNode(this.getFocusedNode(), isTowards);
    }
    public updateOldText(oldText: string): void {
        this.oldText = oldText;
    }
    public onPropertyChanged(newProp: ITreeViewOptions): void {
        for (const prop of Object.keys(newProp)) {
            switch (prop) {
            case SHOWCHECKBOX:
                this.options.showCheckBox = newProp.showCheckBox;
                break;
            case ALLOWDRAGANDDROP:
                this.setDragAndDrop(newProp.allowDragAndDrop);
                break;
            case ALLOWTEXTWRAP:
                this.options.allowTextWrap = newProp.allowTextWrap;
                this.setTextWrap();
                break;
            case ALLOWEDITING:
                this.wireEditingEvents(newProp.allowEditing);
                break;
            case SETDISABLED:
                if (this.options.disabled !== newProp.disabled) {
                    this.options.disabled = newProp.disabled;
                    this.setDisabledMode(newProp.disabled);
                }
                break;
            case DRAGAREA:
                this.setDragArea(newProp.dropArea);
                break;
            case CSSCLASS:
                this.setCssClass(newProp.cssClass);
                break;
            case FULLROWSELECT:
                this.setFullRow(newProp.fullRowSelect);
                break;
            case EXPANDONTYPE:
                this.options.expandOnType = newProp.expandOnType;
                this.wireExpandOnEvent(false);
                this.setExpandOnType();
                if (this.options.expandOnType !== 'None' && !this.options.disabled) {
                    this.wireExpandOnEvent(true);
                }
                break;
            case ENABLERTL:
                this.options.enableRtl = newProp.enableRtl;
                (this.options.enableRtl ? addClass : removeClass)([this.element], RTL);
                break;
            case ANIMATION:
                this.options.animation = newProp.animation;
            }
        }
    }
    private navigateRootNode(isBackwards: boolean): void {
        const focusedNode: Element = this.getFocusedNode();
        const rootNode: Element = isBackwards ? this.getRootNode() : this.getEndNode();
        if (!rootNode.classList.contains(DISABLE)) {
            this.setNodeFocus(focusedNode, rootNode);
            this.navigateToFocus(isBackwards);
        }
    }

    private selectGivenNodes(sNodes: HTMLElement[]): void {
        for (const node of sNodes) {
            if (!node.classList.contains(DISABLE)) {
                this.selectNode(node, null, true);
            }
        }
    }

    public beginEdit(node: string): void {
        const nodeElement: Element = this.element.querySelector('[data-uid="' + node + '"]');
        if (isNOU(nodeElement) || this.options.disabled) {
            return;
        }
        this.createTextbox(nodeElement, null);
    }

    public ensureVisible(node: string): void {
        const liEle: Element = this.element.querySelector('[data-uid="' + node + '"]');
        if (isNOU(liEle)) {
            return;
        }
        setTimeout(() => { liEle.scrollIntoView(true); }, 450);
    }

    public nodeCollapse(id: string): void {
        const liElement: Element = this.element.querySelector('[data-uid="' + id + '"]');
        this.collapseAction(liElement, null, false);
    }

    public nodeExpand(id: string): void {
        const liElement: Element = this.element.querySelector('[data-uid="' + id + '"]');
        this.expandAction(liElement, null);
    }

    public nodeSelection(idArray: string[]): void {
        const selectedNodes: HTMLElement[] = [];
        if (idArray) {
            const liCollections: HTMLElement[] = selectAll('.' + LISTITEM, this.element);
            const idSet: Set<string> = new Set(idArray);
            for (const li of liCollections) {
                const uid: string = li.closest('.e-list-item').getAttribute('data-uid');
                if (idSet.has(uid)) {
                    selectedNodes.push(li);
                }
            }
        }
        const activeElements: HTMLElement[] = selectAll('.' + LISTITEM + '.' + ACTIVE, this.element);
        removeClass(activeElements, ACTIVE);
        addClass(selectedNodes, ACTIVE);
    }

    public nodeCheck(checkedId: string[], intermediateId: string[]): void {
        const checkedNodes: HTMLElement[] = [];
        const intermediateNodes: HTMLElement[] = [];
        if (checkedId) {
            const checkboxElements: HTMLElement[] = selectAll('.' + CHECKBOXFRAME, this.element);
            const checkedIdSet: Set<string> = new Set(checkedId);
            const intermediateIdSet: Set<string> = new Set(intermediateId);
            for (const checkbox of checkboxElements) {
                const uid: string = checkbox.closest('.' + LISTITEM).getAttribute('data-uid');
                if (checkedIdSet.has(uid)) {
                    checkedNodes.push(checkbox);
                }
                if (intermediateIdSet.size && intermediateIdSet.has(uid)) {
                    intermediateNodes.push(checkbox);
                }
            }
        }
        const activeElements: HTMLElement[] = selectAll('.' + CHECK, this.element);
        removeClass(activeElements, CHECK);
        const inElement: HTMLElement[] = selectAll('.e-stop', this.element);
        removeClass(inElement, 'e-stop');
        addClass(checkedNodes, CHECK);
        addClass(intermediateNodes, 'e-stop');
    }

    public KeyActionHandler(e: KeyboardEventArgs, nodeId: string): void {
        this.liList = Array.prototype.slice.call(selectAll('.' + LISTITEM, this.element));
        const nodeElement : Element = this.element.querySelector('[data-uid="' + nodeId + '"]');
        const focusedNode: Element = isNOU(nodeElement) ? this.getFocusedNode() : nodeElement;
        switch (e.action) {
        case 'space':
            if (this.options.showCheckBox) {
                this.checkNode(this.keyAction);
            }
            else {
                this.toggleSelect(focusedNode, this.keyAction, false);
            }
            break;
        case 'moveRight':
            this.keyBoardAction = true;
            this.openNode(!this.options.enableRtl, this.keyAction);
            break;
        case 'moveLeft':
            this.keyBoardAction = true;
            this.openNode(this.options.enableRtl, this.keyAction);
            break;
        case 'shiftDown':
            this.shiftKeySelect(true, this.keyAction);
            break;
        case 'moveDown':
        case 'ctrlDown':
        case 'csDown':
            this.navigateNode(true);
            break;
        case 'shiftUp':
            this.shiftKeySelect(false, this.keyAction);
            break;
        case 'moveUp':
        case 'ctrlUp':
        case 'csUp':
            this.navigateNode(false);
            break;
        case 'home':
        case 'shiftHome':
        case 'ctrlHome':
        case 'csHome':
            this.navigateRootNode(true);
            break;
        case 'end':
        case 'shiftEnd':
        case 'ctrlEnd':
        case 'csEnd':
            this.navigateRootNode(false);
            break;
        case 'enter':
        case 'ctrlEnter':
        case 'shiftEnter':
        case 'csEnter':
        case 'shiftSpace':
        case 'ctrlSpace':
            this.toggleSelect(focusedNode, this.keyAction, false);
            break;
        case 'f2':
            if (this.options.allowEditing && !focusedNode.classList.contains(DISABLE)) {
                this.createTextbox(focusedNode, this.keyAction);
            }
            break;
        case 'ctrlA':
            if (this.options.allowMultiSelection) {
                const sNodes: HTMLElement[] = selectAll('.' + LISTITEM + ':not(.' + ACTIVE + ')', this.element);
                this.selectGivenNodes(sNodes);
            }
            break;
        }
        this.isKeyUp = false;
        // eslint-disable-next-line
        const _this: any = this;
        // eslint-disable-next-line
        setTimeout(function() {
            if (_this.keyBoardAction) {
                _this.setHover(_this.getFocusedNode());
                _this.keyBoardAction = false;
            }
        }, 100);
    }
}

export type createElementParams = (
    tag: string,
    prop?: { id?: string, className?: string, innerHTML?: string, styles?: string, attrs?: { [key: string]: string } }
) => HTMLElement;

export interface NodeEditEventArgs {
    newText: string;
    nodeData: { [key: string]: Object };
    oldText: string;
    innerHtml: string;
}
export interface NodeClickEventArgs {
    event: MouseEvent;
    node: HTMLElement;
}
export interface SerializableMouseEvent {
    detail: number;
    screenX: number;
    screenY: number;
    clientX: number;
    clientY: number;
    offsetX: number;
    offsetY: number;
    pageX: number;
    pageY: number;
    movementX: number;
    movementY: number;
    button: number;
    buttons: number;
    ctrlKey: boolean;
    shiftKey: boolean;
    altKey: boolean;
    metaKey: boolean;
    type: string;
}
export interface NodeKeyPressEventArgs {
    cancel: boolean;
    event: KeyboardEventArgs;
}
export interface NodeSelectEventArgs {
    action: string;
    isInteracted: boolean;
    nodeData: { [key: string]: Object };
    isMultiSelect?: boolean;
    isCtrKey?: boolean;
    isShiftKey?: boolean;
    nodes?: string[];
}

export interface NodeExpandEventArgs {
    isInteracted: boolean;
    nodeData: { [key: string]: Object };
    event: MouseEvent | KeyboardEventArgs | TapEventArgs;
    isLoaded?: boolean;
    nodeLevel: number;
    cancel?: boolean;
}
export interface NodeCheckEventArgs {
    action: string;
    isInteracted: boolean;
    nodeData: { [key: string]: Object };
}

export interface DragAndDropEventArgs {
    cancel: boolean;
    event: MouseEvent & TouchEvent;
    clonedNode: HTMLElement;
    draggedNode: HTMLLIElement;
    draggedNodeData: { [key: string]: Object };
    draggedParentNode: Element;
    dropTarget: Element;
    dropIndex: number;
    dropLevel: number;
    droppedNode: HTMLLIElement;
    dropIndicator: string;
    droppedNodeData: { [key: string]: Object };
    target: HTMLElement;
    preventTargetExpand?: boolean;
}
export interface DropTreeArgs {
    dragLi: string;
    dropLi: string;
    dragParentLi: string;
    srcTree: BlazorDotnetObject;
    isExternalDrag: boolean;
    dropParentLi?: string;
    pre?: boolean;
}
class ActionSettings {
    public effect: Effect;
    public duration: number;
    public easing: string;
}
class NodeAnimationSettings {
    public collapse: ActionSettings;
    public expand: ActionSettings;
}

class FieldsSettingsModel {
    public child: string | FieldsSettingsModel;
    public children: string | FieldsSettingsModel;
    public dataSource: { [key: string]: Object }[];
    public expanded: string;
    public hasChildren: string;
    public htmlAttributes: string;
    public iconCss: string;
    public id: string;
    public imageUrl: string;
    public isChecked: string;
    public parentID: string;
    public selected: string;
    public tableName: string;
    public text: string;
    public tooltip: string;
    public navigateUrl: string;
}

interface ITreeViewOptions {
    enableRtl: boolean;
    expandOnType: string;
    animation: NodeAnimationSettings;
    fields: FieldsSettingsModel;
    allowMultiSelection: boolean;
    allowTextWrap: boolean;
    showCheckBox: boolean;
    allowEditing: boolean;
    disabled: boolean;
    dropArea: string;
    allowDragAndDrop: boolean;
    fullRowSelect: boolean;
    cssClass: string;
    hasTemplate: boolean;
    draggedEvent: boolean;
    nodeCollapsedEvent: boolean;
    createdEvent: boolean;
}

const TreeView: object = {
    initialize(dataId: string, element: HTMLElement, options: ITreeViewOptions, dotnetRef: BlazorDotnetObject): void {
        new SfTreeView(dataId, element, options, dotnetRef);
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        instance.render();
        if (element && !isNOU(instance) && instance.options.allowTextWrap) {
            instance.updateWrap();
        }
        if (element && options.createdEvent) {
            instance.dotNetRef.invokeMethodAsync('CreatedEvent', null);
        }
    },
    updateTextWrap(dataId: string): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && instance.options.allowTextWrap) {
            instance.updateWrap();
        }
    },
    dataSourceChanged(dataId: string): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            instance.unWireEvents();
            instance.wireEvents();
        }
    },
    collapseAction(dataId: string, nodeId: string): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            // eslint-disable-next-line
            const currentLi = instance.element.querySelector('[data-uid="' + nodeId + '"]');
            instance.collapseAction(currentLi, null, false);
        }
    },
    NodeCollapseAction(dataId: string, nodeId: string, cancel: boolean, fromClickHandler: boolean): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            // eslint-disable-next-line
            const currentLi = instance.element.querySelector('[data-uid="' + nodeId + '"]');
            instance.collapseAction(currentLi, null, fromClickHandler, cancel);
        }
    },
    expandAction(dataId: string, nodeId: string): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            // eslint-disable-next-line
            const currentLi = instance.element.querySelector('[data-uid="' + nodeId + '"]');
            instance.expandAction(currentLi, null);
        }
    },
    expandedNode(dataId: string, args: NodeExpandEventArgs): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            setTimeout(function (): void {
                instance.expandedNode(args);
            }, 10);
        }
    },
    collapsedNode(dataId: string, args: NodeExpandEventArgs): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            instance.collapsedNode(args);
        }
    },
    setMultiSelect(dataId: string, args: boolean): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            instance.setMultiSelect(args);
        }
    },
    dragStartActionContinue: function dragStartActionContinue(dataId: string, cancel: boolean): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            instance.dragStartActionContinue(cancel);
        }
    },
    dragNodeStop: function dragNodeStop(dataId: string, args: DragAndDropEventArgs): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            instance.dragNodeStop(args);
        }
    },
    nodeDragging: function nodeDragging(dataId: string): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            instance.nodeDragging();
        }
    },
    setFocus: function setFocus(dataId: string, element: HTMLElement, liElement: HTMLElement, text: string): void {
        if (!isNOU(element) && !isNOU(liElement)) {
            const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
            if (!isNOU(instance)) {
                instance.updateOldText(text);
            }
            const inputEle: HTMLInputElement = <HTMLInputElement>(document.getElementById(element.id));
            inputEle.focus();
            inputEle.setSelectionRange(0, inputEle.value.length);
        }
    },
    nodeEdited: function nodeEdited(dataId: string, liElement: HTMLElement): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance) && !isNOU(liElement)) {
            if (instance.options.allowTextWrap) {
                instance.updateWrap();
            }
            instance.unWireEvents();
            instance.wireEvents();
            if (instance.options.allowDragAndDrop) {
                instance.destroyDrag();
                instance.initializeDrag();
            }
            removeClass([liElement], EDITING);
        }
    },
    updateSpinnerClass: function updateSpinnerClass(dataId: string): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            instance.updateSpinnerClass();
        }
    },
    onPropertyChanged: function onPropertyChanged(dataId: string, properties: ITreeViewOptions): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            instance.onPropertyChanged(properties);
        }
    },
    beginEdit: function beginEdit(dataId: string, node: string): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            instance.beginEdit(node);
        }
    },
    ensureVisible: function ensureVisible(dataId: string, node: string): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            instance.ensureVisible(node);
        }
    },
    nodeCollapse: function nodeCollapse(dataId: string, id: string): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            instance.nodeCollapse(id);
        }
    },
    nodeExpand: function nodeCollapse(dataId: string, id: string): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            instance.nodeExpand(id);
        }
    },
    nodeSelection: function nodeSelection(dataId: string, idArray: string[]): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            instance.nodeSelection(idArray);
        }
    },
    nodeCheck: function nodeCheck(dataId: string,  idArray: string[], intermediateNodes: string[]): void{
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            instance.nodeCheck(idArray, intermediateNodes);
        }
    },
    getAriaLevel: function getAriaLevel(dataId: string, args: NodeExpandEventArgs): number {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        let level: number = 0;
        if (!isNOU(instance)) {
            const li: Element = instance.element.querySelector('[data-uid="' + args.nodeData.id + '"]');
            if (!isNOU(li)) {
                level = parseInt(li.getAttribute('aria-level'), 10);
            }
        }
        return level;
    }
};
export default TreeView;
