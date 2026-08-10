import { addClass, removeClass, Touch, EventHandler, Browser, BlazorDotnetObject } from '@syncfusion/ej2-base';
import { isNullOrUndefined, ScrollEventArgs, SwipeEventArgs } from '@syncfusion/ej2-base';

const CLS_ITEMS: string = 'e-carousel-items';
const CLS_ITEM: string = 'e-carousel-item';
const TRANSLATE_CLASS: string = 'e-translate';

class SfCarousel {
    private element: HTMLElement;
    private swipeMode: string;
    private timeStampStart: number;
    private isScrollTriggered: boolean;
    private itemsContainer: HTMLElement;
    private dotnetRef: BlazorDotnetObject;
    private touchInstance: Touch;
    private dataId: string;
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    private sfBlazor: any = (window as any).sfBlazor;
    constructor(dataId: string, element: HTMLElement, swipeMode: string, dotnetRef: BlazorDotnetObject) {
        this.element = element;
        this.dotnetRef = dotnetRef;
        this.swipeMode = swipeMode;
        this.dataId = dataId;
        this.sfBlazor.setCompInstance(this);
        this.itemsContainer = this.element.querySelector('.' + CLS_ITEMS) as HTMLElement;
        this.initializeTouch();
    }

    private initializeTouch(): void {
        if ((!(this.swipeMode.toString() === '-4' || this.swipeMode.toString() === '-2'))) {
            this.touchInstance = new Touch(this.itemsContainer, {
                scroll: this.scrollHandler.bind(this),
                swipe: this.swipeHandler.bind(this),
                swipeSettings: { swipeThresholdDistance: 1 }
            });
            EventHandler.add(this.element, 'transitionend', this.onTransitionEnd, this);
        }
    }

    private removeTouch(): void {
        if (this.touchInstance) {
            this.touchInstance.destroy();
            this.touchInstance = null;
            EventHandler.remove(this.element, 'transitionend', this.onTransitionEnd);
        }
    }

    public updateTouch(enableTouchSwipe: boolean, swipeMode: string): void {
        this.swipeMode = swipeMode;
        if (!(swipeMode.toString() === '-4' || swipeMode.toString() === '-2')) {
            this.initializeTouch();
        } else {
            this.removeTouch();
        }
    }

    private scrollHandler(e: ScrollEventArgs): void {
        if ((this.element.classList.contains('e-carousel-custom-animation')) || (this.swipeMode.toString() === '-4')) {
            return;
        }
        if (!this.timeStampStart) {
            this.timeStampStart = Date.now();
        }
        if (this.element.classList.contains(TRANSLATE_CLASS)) {
            this.onTransitionEnd();
        }
        if (this.swipeMode === 'Touch' || this.swipeMode.toString() === '-3') {
            if (e.originalEvent.type === 'mousemove') {
                return;
            }
        }
        else if (this.swipeMode === 'Mouse') {
            if (e.originalEvent.type === 'touchmove') {
                return;
            }
        }
        if (e.scrollDirection === 'Left' || e.scrollDirection === 'Right') {
            const scrollDiv: HTMLElement = this.element.querySelector('.' + CLS_ITEMS) as HTMLElement;
            if (scrollDiv && scrollDiv.scrollWidth > scrollDiv.clientWidth) {
                return;
            } else {
                this.isScrollTriggered = true;
                e.originalEvent.preventDefault();
                e.originalEvent.stopPropagation();
            }
        }
        if (e.scrollDirection === 'Left') {
            this.itemsContainer.style.transform = 'translatex(' + (this.getTranslateX(this.itemsContainer) + (-e.distanceX)) + 'px)';
        }
        else if (e.scrollDirection === 'Right') {
            this.itemsContainer.style.transform = 'translatex(' + (this.getTranslateX(this.itemsContainer) + (e.distanceX)) + 'px)';
        }
    }

    private swipeHandler(e: SwipeEventArgs): void {
        if (this.swipeMode.toString() === '-4') {
            this.cancelSwipe();
            return;
        }
        const itemsCount: number = this.itemsContainer.children.length;
        const eventName: String = e.startEvents ? e.startEvents.toString() : null;
        if (((e.swipeDirection) === 'Left' || (e.swipeDirection) === 'Right')) {
            const time: number = Date.now() - this.timeStampStart;
            const offsetDist: number = (e.distanceX * (Browser.isDevice ? 6 : 1.66));
            if (offsetDist > time || (e.distanceX > (this.element.offsetWidth / 2))) {
                if ((e.distanceX > (this.element.offsetWidth / 2))) {
                    this.applySwipeAnimation(e, offsetDist, time);
                }
                const selectedIndex: string = getComputedStyle(this.itemsContainer).getPropertyValue('--carousel-items-current');
                if (((!this.element.classList.contains('e-loop')) && e.swipeDirection === ('Right')) && (selectedIndex === '0')) {
                    this.cancelSwipe();
                }
                else if (((!this.element.classList.contains('e-loop')) && e.swipeDirection === ('Left')) && (selectedIndex === (itemsCount - 1).toString())) {
                    this.cancelSwipe();
                }
                else if (eventName && ((this.swipeMode === 'Touch' && eventName.includes('Mouse')) ||
                    (this.swipeMode === 'Mouse' && eventName.includes('Touch')))) {
                    this.cancelSwipe();
                }
                else {
                    if (this.element.classList.contains('e-rtl')) {
                        if (e.swipeDirection === ('Right')) {
                            e.swipeDirection = 'Left';
                        }
                        else {
                            e.swipeDirection = 'Right';
                        }
                    }
                    this.confirmSwipe(e.swipeDirection);
                }
            } else {
                this.cancelSwipe();
            }
        }
        else {
            this.cancelSwipe();
        }
        this.timeStampStart = null;
    }

    private inverseDirection(direction: string) : string {
        switch (direction) {
        case 'Left':
            direction = 'Next';
            break;
        case 'Right':
            direction = 'Previous';
            break;
        case 'Next':
            direction = 'Left';
            break;
        case 'Previous':
            direction = 'Right';
            break;
        }
        return direction;
    }

    private applySwipeAnimation(e?: SwipeEventArgs, offsetDist?: number, time?: number): void {
        if ((this.element.classList.contains('e-carousel-slide-animation'))) {
            if (isNullOrUndefined(e) || isNullOrUndefined(offsetDist) || isNullOrUndefined(time)) {
                this.itemsContainer.classList.add('e-slide');
            }
            else {
                this.itemsContainer.style.transitionDuration = (((Browser.isDevice ? e.distanceX : offsetDist) / time) / 10) + 's';
            }
        }
        else if ((this.element.classList.contains('e-carousel-fade-animation'))) {
            this.itemsContainer.classList.add('e-fade-in-out');
        }
    }

    private confirmSwipe(direction: string, index?: number): void {
        addClass([this.element], TRANSLATE_CLASS);
        if (isNullOrUndefined(index)) {
            this.changeActiveClass(direction);
            this.changeSlide(direction);
        }
        else {
            this.changeActiveClass(direction, index);
        }
    }

    private cancelSwipe(): void {
        this.element.classList.add(TRANSLATE_CLASS);
        this.onTransitionEnd();
    }

    private changeActiveClass(direction: string, index?: number): void {
        const previousPanel: HTMLElement = this.element.querySelector('.' + CLS_ITEM + '.' + 'e-active' + ':not(.e-clone)').previousElementSibling as HTMLElement;
        const nextPanel: HTMLElement = this.element.querySelector('.' + CLS_ITEM + '.' + 'e-active' + ':not(.e-clone)').nextElementSibling as HTMLElement;
        const currentPanel: NodeListOf<Element> = this.element.querySelectorAll('.' + CLS_ITEM + '.' + 'e-active');
        currentPanel.forEach((element: Element) => {
            element.classList.remove('e-active');
        });
        if (!isNullOrUndefined(index)) {
            const currentPanel: HTMLElement = this.element.querySelectorAll('.' + CLS_ITEM + ':not(.' + 'e-clone' + ')')[parseInt(index.toString(), 10)] as HTMLElement;
            currentPanel.classList.add('e-active');
            if (direction === 'Left') {
                currentPanel.classList.add('e-next');
            }
            else if (direction === 'Right') {
                currentPanel.classList.add('e-prev');
            }
            return;
        }
        if (direction === 'Left') {
            if (nextPanel.classList.contains('e-clone')) {
                this.element.querySelector('.' + CLS_ITEM + ':not(.e-clone)').classList.add('e-active');
            }
            else {
                nextPanel.classList.add('e-active', 'e-next');
            }
        }
        else {
            if (previousPanel.classList.contains('e-clone')) {
                this.element.querySelectorAll('.' + CLS_ITEM)[this.itemsContainer.children.length - 2].classList.add('e-active');
            }
            else {
                previousPanel.classList.add('e-active');
            }
        }
    }

    private getTranslateX(element: HTMLElement): number {
        const style: CSSStyleDeclaration = window.getComputedStyle(element);
        return new WebKitCSSMatrix(style.transform).m41;
    }

    private onTransitionEnd(): void {
        this.itemsContainer.style.transitionDuration = '';
        this.itemsContainer.style.transitionTimingFunction = '';
        this.itemsContainer.style.transform = '';
        this.element.classList.remove(TRANSLATE_CLASS);
        removeClass([this.itemsContainer], ['e-fade-in-out', 'e-slide']);
        this.timeStampStart = null;
        this.isScrollTriggered = false;
        const prevOrNext: HTMLElement = this.element.querySelector('.' + CLS_ITEM + '.e-prev, .' + CLS_ITEM + '.e-next') as HTMLElement;
        if (!isNullOrUndefined(prevOrNext)) {
            prevOrNext.classList.remove('e-prev');
            prevOrNext.classList.remove('e-next');
        }
    }

    private changeSlide(direction: string): void {
        direction = this.inverseDirection(direction);
        try {
            this.dotnetRef.invokeMethodAsync('ChangeSlide', direction);
        }
        catch (e) {
            // eslint-disable-next-line no-console
            console.log(e);
        }
    }

    private destroy(): void {
        this.removeTouch();
        this.itemsContainer = null;
        this.element = null;
    }
}

const BlazorCarousel: object = {
    initialize(dataId: string, element: HTMLElement, swipeMode: string, dotnetRef: BlazorDotnetObject): void {
        // eslint-disable-next-line @typescript-eslint/no-unused-vars
        const instance: SfCarousel = new SfCarousel(dataId, element, swipeMode, dotnetRef);
    },

    swipeHandler(dataId: string, direction: string, index: number): void {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (instance && instance.element) {
            instance.applySwipeAnimation();
            direction = instance.inverseDirection(direction);
            instance.confirmSwipe(direction, index);
        }
    },

    updateTouch(dataId: string, enableTouchSwipe: boolean, swipeMode: string): void {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (instance && instance.element) {
            instance.updateTouch(enableTouchSwipe, swipeMode);
        }
    },
    destroy(dataId: string): void {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (instance.element) {
            instance.destroy();
        }
    }
};

export default BlazorCarousel;
