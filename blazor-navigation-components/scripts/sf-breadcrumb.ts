import { BlazorDotnetObject, closest, EventHandler, createElement, isNullOrUndefined } from '@syncfusion/ej2-base';
import { getZindexPartial } from '@syncfusion/ej2-popups';

/**
 * Client side scripts for Blazor Breadcrumb
 */
class SfBreadcrumb {
    private dataId: string;
    private element: HTMLElement;
    public menu: HTMLElement;
    public popup: HTMLElement;
    public overflowMode: string;
    public maxItems: number;
    private dotnetRef: BlazorDotnetObject;
    private prevWidth: number;
    // eslint-disable-next-line max-len
    constructor(dataId: string, element: HTMLElement, dotnetRef: BlazorDotnetObject, overflowMode: string, maxItems: number, popup?: HTMLElement, menu?: HTMLElement) {
        this.dataId = dataId;
        this.element = element;
        this.menu = menu;
        this.popup = popup;
        this.overflowMode = overflowMode;
        this.maxItems = maxItems;
        this.dotnetRef = dotnetRef;
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        (window as any).sfBlazor.setCompInstance(this);
        this.calculateMaxItems();
        this.wireEvents();
    }

    public calculateMaxItems(): void {
        this.prevWidth = this.element.offsetWidth;
        if (this.overflowMode === 'Default' || this.overflowMode === 'Collapsed' || this.overflowMode === 'Menu') {
            let maxItems: number = -1;
            const width: number = this.element.offsetWidth;
            const liElems: HTMLElement[] = [].slice.call(this.element.children[0].children).reverse();
            let liWidth: number = this.overflowMode === 'Menu' ? 0 : liElems[liElems.length - 1].offsetWidth + (liElems[liElems.length - 2] ? liElems[liElems.length - 2].offsetWidth : 0);
            if (this.overflowMode === 'Menu') {
                const menuEle: HTMLElement = this.getMenuElement();
                this.element.append(menuEle);
                liWidth += menuEle.offsetWidth;
                menuEle.remove();
            }
            if (this.overflowMode === 'Menu' && liElems.length > 0 && liElems.length < 3) {
                for (let i: number = 0; i < liElems.length; i++) {
                    liWidth += liElems[i as number].offsetWidth;
                }
                if (liWidth > width) {
                    maxItems = 0;
                }
            }
            else {
                for (let i: number = 0; i < liElems.length - 2; i++) {
                    if (liWidth > width) {
                        maxItems = Math.ceil((i - 1) / 2) + ((this.overflowMode === 'Menu' && i <= 2) ? 0 : 1);
                        break;
                    } else {
                        if (this.overflowMode === 'Menu' && i === 2) {
                            liWidth += liElems[liElems.length - 1].offsetWidth + liElems[liElems.length - 2].offsetWidth;
                            if (liWidth > width) {
                                maxItems = 1;
                                break;
                            }
                        }
                        if (!(this.overflowMode === 'Menu' && liElems[parseInt(i.toString(), 10)].classList.contains('e-breadcrumb-menu'))) {
                            liWidth += liElems[parseInt(i.toString(), 10)].offsetWidth;
                            if (liWidth > width) {
                                maxItems = Math.ceil((i) / 2) + (this.overflowMode === 'Menu' && i <= 2 ? 0 : 1);
                                break;
                            }
                        }
                    }
                }
            }
            this.dotnetRef.invokeMethodAsync('ChangeMaxItems', maxItems);
        } else if ((this.overflowMode === 'Wrap' || this.overflowMode === 'Scroll') && this.maxItems > 0) {
            let width: number = 0;
            const liElems: NodeListOf<HTMLElement> = this.element.querySelectorAll('.e-breadcrumb-item,.e-breadcrumb-separator');
            if (liElems.length > this.maxItems + this.maxItems - 1) {
                for (let i: number = this.overflowMode === 'Wrap' ? 1 : 0; i < this.maxItems + this.maxItems - 1; i++) {
                    width += liElems[parseInt(i.toString(), 10)].offsetWidth;
                }
                width = width + 5 + (parseInt(getComputedStyle(this.element.children[0]).paddingLeft, 10) * 2);
                if (this.overflowMode === 'Wrap') {
                    (this.element.querySelector('.e-breadcrumb-wrapped-ol') as HTMLElement).style.width = width + 'px';
                } else {
                    this.element.style.width = width + 'px';
                }
            }
        }
    }

    private resize(): void {
        if (this.element && this.element.offsetWidth > 0 && this.prevWidth !== this.element.offsetWidth) {
            this.calculateMaxItems();
        }
    }

    private getMenuElement(): HTMLElement {
        return createElement('li', { className: 'e-icons e-breadcrumb-menu' });
    }

    public openPopup(menu: Element, popup: HTMLElement): void {
        let left: number; let top: number;
        document.body.appendChild(popup);
        const menuOffset: ClientRect = menu.getBoundingClientRect();
        const popupOffset: ClientRect = popup.getBoundingClientRect();
        left = menuOffset.left + scrollX;
        top = menuOffset.bottom + scrollY;
        if (menuOffset.bottom + popupOffset.height > document.documentElement.clientHeight) {
            if (top - menuOffset.height - popupOffset.height > document.documentElement.clientTop) {
                top = top - menuOffset.height - popupOffset.height;
            }
        }
        if (menuOffset.left + popupOffset.width > document.documentElement.clientWidth) {
            if (menuOffset.right - popupOffset.width > document.documentElement.clientLeft) {
                left = (left + menuOffset.width) - popupOffset.width;
            }
        }
        this.addEventListener();
        popup.style.left = Math.ceil(left) + 'px';
        popup.style.top = Math.ceil(top) + 'px';
        popup.style.zIndex = getZindexPartial(this.element) + '';
        popup.classList.remove('e-hidden-popup');
        (popup.firstElementChild as HTMLElement).focus();
    }

    private addEventListener(): void {
        EventHandler.add(document, 'mousedown', this.mousedownHandler, this);
        if (this.popup) {
            EventHandler.add(this.popup, 'keydown', this.popupKeyDownHandler, this);
        }
    }

    private popupKeyDownHandler(e: KeyboardEvent): void {
        if (e.key === 'Escape') {
            this.dotnetRef.invokeMethodAsync('ClosePopup', null);
        }
    }

    private mousedownHandler(e: MouseEvent): void {
        if (this.popup && this.popup.parentElement) {
            const target: Element = e.target as Element;
            if ((!closest(target, '#' + this.menu.id) && !closest(e.target as Element, '#' + this.popup.id))) {
                this.dotnetRef.invokeMethodAsync('ClosePopup', null);
                this.removeEventListener();
            }
        } else {
            this.removeEventListener();
        }
    }
    private removeEventListener(): void {
        EventHandler.remove(document, 'mousedown', this.mousedownHandler);
        if (this.popup) {
            EventHandler.remove(this.popup, 'keydown', this.popupKeyDownHandler);
        }
    }

    private wireEvents(): void {
        window.addEventListener('resize', this.resize.bind(this));
    }

    private unWireEvents(): void {
        window.removeEventListener('resize', this.resize.bind(this));
    }

    public destroy(): void {
        this.unWireEvents();
        this.element = null;
    }
}

const Breadcrumb: object = {
    initialize(dataId: string, element: HTMLElement, dotnetRef: BlazorDotnetObject, overflowMode: string, maxItems: number): void {
        if (element) {
            new SfBreadcrumb(dataId, element, dotnetRef, overflowMode, maxItems);
        }
    },
    // eslint-disable-next-line @typescript-eslint/tslint/config
    calculateMaxItems(dataId: string, overflowMode: string) {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNullOrUndefined(instance)) {
            instance.overflowMode = overflowMode;
            instance.calculateMaxItems();
        }
    },
    // eslint-disable-next-line @typescript-eslint/tslint/config
    openPopup(dataId: string, menu: HTMLElement, popup: HTMLElement) {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNullOrUndefined(instance)) {
            instance.popup = popup;
            instance.menu = menu;
            instance.openPopup(menu, popup);
        }
    },
    // eslint-disable-next-line @typescript-eslint/tslint/config
    destroy(dataId: string) {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const instance: any = (window as any).sfBlazor.getCompInstance(dataId);
        if (!isNullOrUndefined(instance)) {
            instance.destroy();
        }
    }
};

export default Breadcrumb;

