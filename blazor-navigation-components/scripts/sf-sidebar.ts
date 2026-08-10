import { BlazorDotnetObject, EventHandler, isNullOrUndefined as isNOU, Browser } from '@syncfusion/ej2-base';
import { addClass, closest, formatUnit, createElement, Touch, SwipeEventArgs, removeClass } from '@syncfusion/ej2-base';

const LEFT: string = 'Left';
const RIGHT: string = 'Right';
const PUSH: string = 'Push';
const OVER: string = 'Over';
const SLIDE: string = 'Slide';
const AUTO: string = 'Auto';
const CLOSE: string = 'e-close';
const ROOT: string = 'e-sidebar';
const CONTROL: string = 'e-control';
const CONTEXT: string = 'e-sidebar-context';
const DEFAULTBACKDROP: string = 'e-sidebar-overlay';
const SIDEBARABSOLUTE: string = 'e-sidebar-absolute';
const MAINCONTENTANIMATION: string = 'e-content-animation';
type SidebarPosition = 'Left' | 'Right';
type SidebarType = 'Slide' | 'Over' | 'Push' | 'Auto';

class SfSidebar {
    private element: HTMLElement;
    private dataId: string;
    private targetElement: HTMLElement;
    private position: SidebarPosition;
    private target: HTMLElement | string;
    private showBackdrop: boolean;
    private type: SidebarType;
    private windowWidth: string | number;
    private dotnetRef: BlazorDotnetObject;
    private enableDock: boolean;
    private modal: HTMLElement;
    private mediaQuery: string;
    private enableGestures: boolean = true;
    private isOpen: boolean = false;
    private mainContentElement: Touch;
    private sidebarElement: Touch;
    private closeOnDocumentClick: boolean = false;
    private isPositionChange: boolean = false;
    private dockSize: string = 'auto';
    private width: string = 'auto';
    private isSwipChange: boolean = false;
    public eventArgs: EventArgs;

    constructor(dataId: string, element: HTMLElement, dotnetRef: BlazorDotnetObject, property: ISidebar) {
        this.element = element;
        this.dataId = dataId;
        (window as any).sfBlazor.setCompInstance(this);
        this.dotnetRef = dotnetRef;
        this.resetProperty(property);
    }

    public initialize(): void {
        this.setTarget();
        this.addClass();
        this.setType();
        this.setCloseOnDocumentClick();
        this.setMediaQuery();
        if (Browser.isDevice) {
            this.windowWidth = window.innerWidth;
        }
        this.wireEvents();
    }

    private addClass(): void {
        const mainElement: HTMLElement = <HTMLElement>document.querySelector('.e-main-content');
        if (!isNOU(mainElement || this.targetElement)) {
            addClass([mainElement || this.targetElement], [MAINCONTENTANIMATION]);
        }
    }

    private setTarget(): void {
        this.targetElement = <HTMLElement>this.element.nextElementSibling;
        if (typeof (this.target) === 'string') {
            this.target = <HTMLElement>document.querySelector(this.target);
        }
        if (this.target) {
            (<HTMLElement>this.target).insertBefore(this.element, (<HTMLElement>this.target).children[0]);
            addClass([this.element], SIDEBARABSOLUTE);
            addClass([(<HTMLElement>this.target)], CONTEXT);
            this.targetElement = this.getTargetElement();
        }
    }

    private getTargetElement(): HTMLElement {
        let siblingElement: HTMLElement = <HTMLElement>this.element.nextElementSibling;
        while (!isNOU(siblingElement)) {
            if (!siblingElement.classList.contains(ROOT)) {
                break;
            }
            siblingElement = <HTMLElement>siblingElement.nextElementSibling;
        }
        return siblingElement;
    }

    public hide(): void {
        const sibling: HTMLElement = <HTMLElement>document.querySelector('.e-main-content') || this.targetElement;
        if (!this.enableDock && sibling) {
            sibling.style.transform = sibling.classList.contains('e-sidebar') ? '' : 'translateX(' + 0 + 'px)';
            sibling.style[this.position === LEFT ? 'marginLeft' : 'marginRight'] = '0px';
        }
        this.destroyBackDrop();
        this.isOpen = false;
        this.sidebarOpened = false;
        if (this.enableDock) {
            setTimeout((): void => this.sidebarTimeout(), 50);
        }
        EventHandler.add(this.element, 'transitionend', this.transitionEnd, this);
    }

    sidebarOpened: boolean;
    public show(isServercall?: boolean): void {
        if (isServercall) {
            setTimeout((): void => this.setType(), 50);
        }
        this.isOpen = true;
        this.sidebarOpened = true;
        EventHandler.add(this.element, 'transitionend', this.transitionEnd, this);
    }

    private transitionEnd(value: Event): void {
        if (isNOU(this.dotnetRef)) {
            return;
        }
        if (this.enableDock && !this.isOpen) {
            const dimension : string = this.position === LEFT ? '-100' : '100';
            const transform : string = this.position === LEFT ? this.setDimension(this.dockSize) :  '-' + this.setDimension(this.dockSize);
            this.element.style.transform = `translateX(${dimension}%) translateX(${transform})`;
        }
        this.dotnetRef.invokeMethodAsync('SetDock');
        if (!isNOU(value)) {
            this.dotnetRef.invokeMethodAsync('TriggerChange', this.isOpen, value);
        }
        EventHandler.remove(this.element, 'transitionend', this.transitionEnd);
    }

    backDropApplied: boolean;
    public createBackDrop(property: ISidebar): void {
        const sidebarTarget: string | HTMLElement = this.target;
        this.backDropApplied = this.showBackdrop;
        this.resetProperty(property);
        if (this.showBackdrop && this.sidebarOpened) {
            if (this.backDropApplied) {
                this.destroyBackDrop();
            }
            this.modal = createElement('div');
            this.modal.className = DEFAULTBACKDROP;
            this.modal.style.display = 'block';
            if (this.target || sidebarTarget || !this.backDropApplied) {
                const sibling: HTMLElement = <HTMLElement>document.querySelector('.e-main-content') || this.targetElement;
                sibling.appendChild(this.modal);
            } else {
                document.body.appendChild(this.modal);
            }
        } else {
            this.destroyBackDrop();
        }
    }

    private destroyBackDrop(): void {
        if (!isNOU(this.modal)) {
            this.modal.style.display = 'none';
            this.modal.outerHTML = '';
            this.modal = null;
        }
    }

    private enableGestureHandler(args: SwipeEventArgs): void {
        const originalEvent: TouchEvent | MouseEvent = args.originalEvent as TouchEvent | MouseEvent;
        if (!originalEvent) {
            return;  // Exit early if originalEvent is not available
        }
        if (!this.isOpen && ((this.position === LEFT && args.swipeDirection === RIGHT &&
            (args.startX <= 20 && args.distanceX >= 50 && args.velocity >= 0.5)) || (this.position === RIGHT && args.swipeDirection === LEFT
                && (window.innerWidth - args.startX <= 20 && args.distanceX >= 50 && args.velocity >= 0.5)))) {
            this.eventArgs = {
                left: this.getXYValue(originalEvent, 'X'),
                top: this.getXYValue(originalEvent, 'Y')
            };
            this.dotnetRef.invokeMethodAsync('TriggerShow', this.eventArgs);
            this.show();
            this.isSwipChange = true;
        } else if (this.isOpen && (this.position === LEFT && args.swipeDirection === LEFT) || (this.position === RIGHT &&
            args.swipeDirection === RIGHT)) {
            this.eventArgs = {
                left: this.getXYValue(originalEvent, 'X'),
                top: this.getXYValue(originalEvent, 'Y')
            };
            this.dotnetRef.invokeMethodAsync('TriggerHide', this.eventArgs);
            this.hide();
            this.isSwipChange = false;
        }
    }

    private resize(): void {
        this.setMediaQuery();
        if (Browser.isDevice) {
            this.windowWidth = window.innerWidth;
        }
    }

    public setEnableGestures(property?: ISidebar): void {
        this.resetProperty(property);
        if (this.enableGestures) {
            this.mainContentElement = new Touch(document.body, { swipe: this.enableGestureHandler.bind(this) });
            this.sidebarElement = new Touch(<HTMLElement>this.element, { swipe: this.enableGestureHandler.bind(this) });
        } else if (this.mainContentElement && this.sidebarElement) {
            this.mainContentElement.destroy();
            this.sidebarElement.destroy();
        }
    }

    private wireEvents(): void {
        this.setEnableGestures();
        window.addEventListener('resize', this.resize.bind(this));
    }
    private unWireEvents(): void {
        window.removeEventListener('resize', this.resize.bind(this));
        EventHandler.remove(document, 'mousedown touchstart', this.documentclickHandler);
        if (this.mainContentElement) { this.mainContentElement.destroy(); }
        if (this.sidebarElement) { this.sidebarElement.destroy(); }
    }

    private documentclickHandler(e: TouchEvent | MouseEvent): void {
        if (isNOU(this.dotnetRef)) {
            return;
        }
        if (!(closest((<HTMLElement>e.target), '.' + CONTROL + '' + '.' + ROOT))) {
            this.eventArgs = {
                left: this.getXYValue(e, 'X'),
                top: this.getXYValue(e, 'Y')
            };
            if (this.closeOnDocumentClick) {
                this.dotnetRef.invokeMethodAsync('TriggerHide', this.eventArgs);
            }
        }
    }

    public setCloseOnDocumentClick(property?: ISidebar): void {
        this.resetProperty(property);
        if (this.closeOnDocumentClick) {
            EventHandler.add(document, 'mousedown touchstart', this.documentclickHandler, this);
        } else if (property) {
            EventHandler.remove(document, 'mousedown touchstart', this.documentclickHandler);
        }
    }

    public setMediaQuery(): void {
        if (this.mediaQuery && this.windowWidth !== window.innerWidth) {
            if (window.matchMedia(this.mediaQuery).matches) {
                this.dotnetRef.invokeMethodAsync('TriggerShow', null);
            } else if (this.isOpen) {
                this.dotnetRef.invokeMethodAsync('TriggerHide', null);
            }
        }
    }

    private setDimension(width: number | string): string {
        if (typeof width === 'number') {
            width = formatUnit(width);
        } else if (typeof width === 'string') {
            width = (width.match(/px|%|em/)) ? width : formatUnit(width);
        } else {
            width = '100%';
        }
        return width;
    }

    private sidebarTimeout(): void {
        const sibling: HTMLElement = <HTMLElement>document.querySelector('.e-main-content') || this.targetElement;
        const leftMargin: string = this.isOpen ? this.setDimension(this.width) : this.setDimension(this.dockSize);
        const rightMargin: string = this.setDimension(this.element.getBoundingClientRect().width);
        if (sibling) {
            if (this.isOpen) {
                this.positionStyles(this.width, sibling, rightMargin, leftMargin);
            } else if (this.element.classList.contains(CLOSE)) {
                this.positionStyles(this.dockSize, sibling, rightMargin, leftMargin);
            }
        }
    }

    private positionStyles(size: string, sibling: HTMLElement, rightMargin: string, leftMargin: string): void {
        if (this.position === LEFT) {
            sibling.style.marginLeft = size === 'auto' ? rightMargin : leftMargin;
        } else {
            sibling.style.marginRight = size === 'auto' ? rightMargin : leftMargin;
        }
    }

    private siblingStyle(sibling: HTMLElement, margin: string): void {
        sibling.style[this.position === LEFT ? 'marginLeft' : 'marginRight'] = margin;
    }

    private resetProperty(property: ISidebar): void {
        if (!isNOU(property)) {
            this.type = property.Type;
            this.isOpen = property.IsOpen;
            this.isPositionChange = this.position !== property.Position;
            this.position = property.Position;
            this.enableDock = property.EnableDock;
            this.showBackdrop = property.ShowBackdrop;
            this.target = property.Target;
            this.enableGestures = property.EnableGestures;
            this.closeOnDocumentClick = property.CloseOnDocumentClick;
            this.mediaQuery = property.MediaQuery;
            this.dockSize = property.DockSize;
            this.width = property.Width;
        }
    }

    private getXYValue(e: MouseEvent | TouchEvent, direction: string): number {
        if (isNOU(e)) { return 0; }
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

    public setType(property?: ISidebar): void {
        if (closest((<HTMLElement>this.element), '.e-sidebarcontainer')) {
            return;
        }
        this.resetProperty(property);
        let elementWidth: number | string =  this.width !== 'auto' && this.enableDock ? this.setDimension(this.width) : this.element.getBoundingClientRect().width;
        elementWidth = this.enableDock && !this.isOpen ?  this.dockSize : !this.enableDock && !this.isOpen ? 0 : elementWidth;
        const sibling: HTMLElement = <HTMLElement>document.querySelector('.e-main-content') || this.targetElement;
        if (sibling) {
            if (this.isPositionChange) {
                sibling.style[this.position === LEFT ? 'marginRight' : 'marginLeft'] = '0px';
            }
            sibling.style.transform = sibling.classList.contains('e-sidebar') ? '' : 'translateX(' + 0 + 'px)';
            if ((!Browser.isDevice && this.type !== AUTO) && this.type !== OVER) {
                sibling.style[this.position === LEFT ? 'marginLeft' : 'marginRight'] = '0px';
            }
            this.isPositionChange = false;
            const margin: string = typeof (elementWidth) === 'string' ? elementWidth : elementWidth + 'px';
            const translate: string | number = this.position === LEFT ? elementWidth : - (elementWidth);
            const value: boolean = sibling && (this.enableDock || this.isOpen || this.isSwipChange);
            switch (this.type) {
            case PUSH:
                if (value) {
                    this.siblingStyle(sibling, margin);
                } break;
            case SLIDE:
                if (value) {
                    sibling.style.transform = 'translateX(' + translate + 'px)';
                    this.siblingStyle(sibling, margin);
                } break;
            case OVER:
                if (this.element.classList.contains(CLOSE)) {
                    if (this.enableDock) {
                        this.siblingStyle(sibling, this.dockSize);
                    } else {
                        this.siblingStyle(sibling, '0px');
                    }
                } break;
            case AUTO:
                if (Browser.isDevice) {
                    if ((this.enableDock) && !this.isOpen) {
                        this.siblingStyle(sibling, margin);
                    }
                } else if ((this.enableDock || this.isOpen || this.isSwipChange)) {
                    this.siblingStyle(sibling, margin);
                }
                else if (!this.enableDock && !this.isOpen) {
                    this.siblingStyle(sibling, margin);
                }
                this.isSwipChange = false;
            }
        }
    }

    public destroy(): void {
        this.destroyBackDrop();
        this.element.style.width = this.element.style.zIndex = this.element.style.transform = '';
        this.windowWidth = null;
        this.mediaQuery = null;
        const sibling: HTMLElement = <HTMLElement>document.querySelector('.e-main-content') || this.targetElement;
        if (!isNOU(sibling)) {
            sibling.style.margin = sibling.style.transform = '';
        }
        this.unWireEvents();
        this.targetElement = null;
        this.mainContentElement = null;
        this.sidebarElement = null;
        this.element = null;
        this.dotnetRef = null;
    }
}
const Sidebar: object = {
    initialize(dataId: string, element: HTMLElement, dotnetRef: BlazorDotnetObject, property: ISidebar): boolean {
        new SfSidebar(dataId, element, dotnetRef, property);
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (element && !isNOU(instance)) {instance.initialize(); removeClass([element], 'e-hidden'); }
        return !Browser.isDevice && (isNOU(property.MediaQuery) || window.matchMedia(property.MediaQuery).matches) ?
            true : (Browser.isDevice && property.IsOpen) ? true : false;
    },
    setType(dataId: string, property: ISidebar): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            instance.setType(property);
        }
    },
    hide(dataId: string, property: ISidebar): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            instance.setType(property);
            instance.hide();
        }
    },
    show(dataId: string, property: ISidebar, isServerCall: boolean): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            instance.setType(property);
            instance.show(isServerCall);
            instance.createBackDrop(property);
        }
    },
    onPropertyChange(dataId: string, property: ISidebar): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            if (property.CloseOnDocumentClick !== undefined) { instance.setCloseOnDocumentClick(property); }
            if (property.ShowBackdrop !== undefined) { instance.createBackDrop(property); }
            if (property.Width !== undefined) {
                instance.setType(property);
            }
        }
    },
    destroy(dataId: string): void {
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNOU(instance)) {
            instance.destroy();
        }
    }
};

interface EventArgs {
    left?: number;
    top?: number;
}

interface ISidebar {
    Target: string;
    Width: string;
    MediaQuery: string;
    DockSize: string;
    IsOpen: boolean;
    EnableGestures: boolean;
    EnableDock: boolean;
    ShowBackdrop: boolean;
    CloseOnDocumentClick: boolean;
    Position: SidebarPosition;
    Type: SidebarType;
}
export default Sidebar;
