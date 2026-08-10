import { BlazorDotnetObject, EventHandler, KeyboardEventArgs, MouseEventArgs, enableBlazorMode, isNullOrUndefined } from '@syncfusion/ej2-base';


/**
 * @hidden
 * Traverses up the DOM tree from a given element until it finds a parent element that matches the selector.
 *
 * @param {Element} elem The starting element for traversal.
 * @param {string} selector The selector to match against parent elements.
 * @param {boolean} [isID=false] Optional flag indicating if the selector is an ID.
 * @returns {Element | null} The matching parent element, or null if none found.
 */
export function parentsUntil(elem: Element, selector: string, isID?: boolean): Element | null {
    let parent: Element | null = elem;
    while (parent) {
        if (isID ? parent.id === selector : parent.classList.contains(selector)) {
            break;
        }
        parent = parent.parentElement;
    }
    return parent;
}

// If Blazor's .NET interop manages classes, this would be called differently.
const classList = (function () {
    return function (element: Element | null, addClasses?: string[], removeClasses?: string[]): void {
        if (!element) {
            return;
        }
        if (addClasses) {
            addClasses.forEach((cls: string) => element.classList.add(cls));
        }
        if (removeClasses) {
            removeClasses.forEach((cls: string) => element.classList.remove(cls));
        }
    };
})();

/**
 * Client side script for Blazor Pager
 */
class SfPager {
    private dataId: string;
    private element: HTMLElement;
    private dotnetRef: BlazorDotnetObject;
    private previousBrowserWidth: number = 0;
    private resizeFinalizeTimer: number | null;
    private totalPages: number = 0;

    constructor(dataId: string, element: HTMLElement, dotnetRef: BlazorDotnetObject) {
        this.element = element;
        this.dataId = dataId;
        this.dotnetRef = dotnetRef;
        this.resizeFinalizeTimer = null;
        this.windowResized();
        this.wireEvents();
        /* eslint-disable @typescript-eslint/no-explicit-any */
        (window as any).sfBlazor.setCompInstance(this);
    }

    private wireEvents(): void {
        EventHandler.add(this.element, 'keydown', this.documentKeyHandler, this);
        EventHandler.add(this.element, 'click', this.pagerClickHandler, this);
        /* eslint-disable @typescript-eslint/no-explicit-any */
        EventHandler.add(window as any, 'resize', this.windowResized, this);
    }

    private unWireEvents(): void {
        EventHandler.remove(this.element, 'keydown', this.documentKeyHandler);
        EventHandler.remove(this.element, 'click', this.pagerClickHandler);
        /* eslint-disable @typescript-eslint/no-explicit-any */
        EventHandler.remove(window as any, 'resize', this.windowResized);
        // Clear any pending resize timer and force final server sync
        if (this.resizeFinalizeTimer) {
            clearTimeout(this.resizeFinalizeTimer);
            this.resizeFinalizeTimer = null;
        }
    }

    private windowResized(): void {
        /* eslint-disable @typescript-eslint/no-this-alias */
        const _this: SfPager = this;
        if (_this.resizeFinalizeTimer) {
            clearTimeout(_this.resizeFinalizeTimer);
        }
        // Set debounce timer to trigger server sync after resize ends
        _this.resizeFinalizeTimer = window.setTimeout(function () {
            _this.handleResizeEnd();
        }, 100);
        setTimeout((): void => {
            const pagerMessageDiv: HTMLElement = _this.element.querySelector('.e-parentmsgbar') as HTMLElement;
            const mfirst: HTMLElement = _this.element.querySelector('.e-mfirst') as HTMLElement;
            if (!isNullOrUndefined(mfirst) && mfirst.offsetWidth === 0) {
                const pagerWidth: number = _this.element.offsetWidth;
                const pagerWithoutMargin: number = _this.element.offsetWidth - _this.getMargin(_this.element, 'Left') - _this.getMargin(_this.element, 'Right') - 16;
                const numericContainerWidth: number = (_this.element.querySelector('.e-pagercontainer') as HTMLElement).offsetWidth + _this.getMargin(_this.element.querySelector('.e-pagercontainer') as HTMLElement, 'Left') + 4;
                const pageSizesDiv: HTMLElement = _this.element.querySelector('.e-pagesizes') as HTMLElement;
                let pagerDropdown: HTMLElement;
                let pagerConstant: HTMLElement;
                let pageSizesOffsetWidth: number = 0;
                if (_this.element.classList.contains('e-adaptive')) {
                    setTimeout((): void => {
                        _this.element.classList.remove('e-adaptive');
                    }, 0);
                    // When transitioning from mobile to desktop, initialize e-parentmsgbar with e-hide
                    if (!isNullOrUndefined(pagerMessageDiv)) {
                        pagerMessageDiv.classList.add('e-hide');
                        // Restore numeric items hidden while in mobile mode so desktop shows initial set
                        setTimeout(function () {
                            try {
                                const pagerContainerRestore: Element = _this.element.querySelector('.e-pagercontainer');
                                if (pagerContainerRestore) {
                                    const hiddenItems: NodeListOf<Element> = pagerContainerRestore.querySelectorAll('.e-numericitem.e-hide');
                                    for (let i = 0; i < hiddenItems.length; i++) {
                                        hiddenItems[i].classList.remove('e-hide');
                                    }
                                }
                            }
                            catch (e) { }
                        }, 0);
                    }
                    _this.dotnetRef.invokeMethodAsync('IsMobileDevice', false);
                }
                if (!isNullOrUndefined(pageSizesDiv)) {
                    pagerDropdown = (pageSizesDiv.querySelector('.e-pagerdropdown') as HTMLElement);
                    pagerConstant = (pageSizesDiv.querySelector('.e-pagerconstant') as HTMLElement);
                    pageSizesOffsetWidth = pagerDropdown.offsetWidth + _this.getMargin(pagerDropdown, 'Left') + _this.getMargin(pagerDropdown, 'Right') +
                                        pagerConstant.offsetWidth + _this.getMargin(pagerConstant, 'Left') + _this.getMargin(pagerConstant, 'Right') + 5;
                }
                if (!isNullOrUndefined(pagerMessageDiv) && numericContainerWidth + pageSizesOffsetWidth + pagerMessageDiv.offsetWidth >
                    pagerWithoutMargin
                    && _this.previousBrowserWidth > pagerWidth) {
                    //Executed when decreasing the browser width
                    pagerMessageDiv.style.display = 'none';
                    if (numericContainerWidth + pageSizesOffsetWidth > pagerWithoutMargin) {
                        if (!isNullOrUndefined(pageSizesDiv)) {
                            pagerConstant.style.display = 'none';
                            if (numericContainerWidth + pagerDropdown.offsetWidth + _this.getMargin(pagerDropdown, 'Left') + _this.getMargin(pagerDropdown, 'Right') + 3 > pagerWithoutMargin) {
                                pagerDropdown.style.display = 'none';
                            }
                        }
                    }
                } else if (!isNullOrUndefined(pagerMessageDiv) && _this.previousBrowserWidth < pagerWidth) {
                    //Executed when increasing the browser width
                    if (numericContainerWidth + pageSizesOffsetWidth < pagerWithoutMargin) {
                        if (!isNullOrUndefined(pageSizesDiv)) {
                            pagerDropdown.style.display = '';
                            if (numericContainerWidth + pagerDropdown.offsetWidth + _this.getMargin(pagerDropdown, 'Left') + _this.getMargin(pagerDropdown, 'Right') + 105 < pagerWithoutMargin) {
                                //105 is pagerconstant width including padding/margin
                                pagerConstant.style.display = '';
                            }
                        }
                        if (numericContainerWidth + pageSizesOffsetWidth < pagerWithoutMargin) {
                            pagerMessageDiv.style.display = '';
                            if (numericContainerWidth + pageSizesOffsetWidth + pagerMessageDiv.offsetWidth > pagerWithoutMargin) {
                                pagerMessageDiv.style.display = 'none';
                            }
                            if (!isNullOrUndefined(pagerMessageDiv) && pagerWidth > 900) {
                                    pagerMessageDiv.style.visibility = 'visible';
                                    pagerMessageDiv.classList.remove('e-hide');
                                }
                        }
                    } else if (!isNullOrUndefined(pageSizesDiv)) {
                        //Executed when browser is resized from smaller size(less than 769px) to little larger but not much space to render the pagerMessageDiv
                        pagerMessageDiv.style.display = 'none';
                    }
                }
            } else if (!isNullOrUndefined(pagerMessageDiv)){
                //To render the parentmessagebar div when browser width is less than 769px(small devices)
                pagerMessageDiv.style.display = '';
                _this.refresh();
            }
            _this.previousBrowserWidth = _this.element.offsetWidth;
        }, 50);
        const isStyleApplied: boolean = this.element.classList.contains('e-pager')
            ? getComputedStyle(this.element).getPropertyValue('padding') !== '0px'
            : false;

        if (isStyleApplied) {
            this.resizePager();
        }
    }

    private resizePager(): void {
        const pagerElements = Array.from(this.element.querySelectorAll(
            '.e-mfirst, .e-mprev, .e-icon-first, .e-icon-prev, .e-pp:not(.e-disable), .e-numericitem, .e-numericitem.e-active.e-hide, .e-np:not(.e-disable), .e-icon-next, .e-icon-last, .e-parentmsgbar, .e-mnext, .e-mlast, .e-pagerdropdown, .e-pagerconstant'
        )) as HTMLElement[];

        let actualWidth = 0;
        for (let i = 0; i < pagerElements.length; i++) {
            const item = pagerElements[i];
            if (getComputedStyle(item).display !== 'none') {
                actualWidth += item.offsetWidth
                    + parseFloat(getComputedStyle(item).marginLeft)
                    + parseFloat(getComputedStyle(item).marginRight);
            }
        }

        const pagerContainer = this.element.querySelector('.e-pagercontainer');
        if (!pagerContainer) {
            return;
        }

        actualWidth += parseFloat(getComputedStyle(pagerContainer).marginLeft)
            + parseFloat(getComputedStyle(pagerContainer).marginRight);

        const pagerWidth = this.element.clientWidth
            - parseFloat(getComputedStyle(this.element).paddingLeft)
            - parseFloat(getComputedStyle(this.element).paddingRight)
            - parseFloat(getComputedStyle(this.element).marginLeft)
            - parseFloat(getComputedStyle(this.element).marginRight);

        const numItems = Array.from(pagerContainer.querySelectorAll('.e-numericitem:not(.e-hide):not([style*="display: none"]):not(.e-np):not(.e-pp)')) as HTMLElement[];
        const hiddenNumItems = pagerContainer.querySelectorAll('.e-numericitem.e-hide:not([style*="display: none"])');
        const hideFrom = numItems.length;
        const showFrom = 1;
        const bufferWidth = parentsUntil(this.element, 'e-bigger') ? 10 : 5;
        const nextPageElement = pagerContainer.querySelector('.e-np');
        const previousPageElement = pagerContainer.querySelector('.e-pp');
        const numItemsArray = Array.from(pagerContainer.querySelectorAll('.e-numericitem:not(.e-np):not(.e-pp)')) as HTMLElement[];
        const focusedIndex = numItemsArray.findIndex(item => item.classList.contains('e-currentitem')) + 1;

        if (!numItems.length) {
            return;
        }

        let totalWidth = 0;
        for (let i = 0; i < numItems.length; i++) {
            const item = numItems[i];
            totalWidth += item.offsetWidth
                + parseFloat(getComputedStyle(item).marginLeft)
                + parseFloat(getComputedStyle(item).marginRight);
        }

        const numericItemWidth = totalWidth / numItems.length;
        if (actualWidth >= (pagerWidth - numericItemWidth) && numItems.length > 1) {
            const diff = Math.abs(actualWidth - pagerWidth);
            let numToHide = Math.floor(diff / numericItemWidth);
            numToHide = (numToHide === 0) ? 1 : (numToHide > numItems.length) ? (numItems.length - 1) : numToHide;
            this.hideNumericItems(pagerContainer, numItems, hideFrom, focusedIndex, nextPageElement, previousPageElement, numToHide);
        } else if (actualWidth < pagerWidth && hiddenNumItems.length && window.innerWidth >= 768 && this.previousBrowserWidth < this.element.offsetWidth) {
            this.showNumericItems(hiddenNumItems, pagerWidth, actualWidth, numericItemWidth, bufferWidth, focusedIndex, showFrom);
        }
    }

     private hideNumericItems(
        pagerContainer: Element,
        numItems: HTMLElement[],
        hideFrom: number,
        focusedIndex: number,
        nextPageElement: Element | null,
        previousPageElement: Element | null,
        numToHide: number
    ): void {
        if (focusedIndex !== this.totalPages) {
            classList(nextPageElement, ['e-numericitem', 'e-pager-default'], ['e-nextprevitemdisabled', 'e-disable']);
        }

        let tempfocusElement = -1;
        numItems.forEach((item, index) => {
            if (item.classList.contains('e-currentitem')) {
                tempfocusElement = index + 1;
            }
        });

        for (let i = 1; i <= numToHide; i++) {
            let hideIndex = hideFrom - parseInt(i.toString(), 10);
            numItems = Array.from(pagerContainer.querySelectorAll('.e-numericitem:not(.e-hide):not([style*="display: none"]):not(.e-np):not(.e-pp)')) as HTMLElement[];
            if (focusedIndex !== 1 && (
                tempfocusElement === hideIndex + 1 ||
                parseInt(numItems[Math.abs(hideIndex)]?.getAttribute('index') ?? '0', 10) === focusedIndex ||
                parseInt(numItems[numItems.length - 1]?.getAttribute('index') ?? '0', 10) === focusedIndex
            )) {
                hideIndex = 0;
                classList(previousPageElement, ['e-numericitem', 'e-pager-default'], ['e-nextprevitemdisabled', 'e-disable']);
            }
            numItems[Math.abs(hideIndex)]?.classList.add('e-hide');
        }
    }

    private showNumericItems(
        hiddenNumItems: NodeListOf<Element>,
        pagerWidth: number,
        actualWidth: number,
        numericItemWidth: number,
        bufferWidth: number,
        focusedIndex: number,
        showFrom: number
    ): void {
        const diff = Math.abs(pagerWidth - actualWidth);
        if (diff <= (numericItemWidth * 2)) {
            return;
        }

        let numToShow = Math.floor(diff / (numericItemWidth + bufferWidth));
        numToShow = (numToShow > hiddenNumItems.length) ? hiddenNumItems.length : (numToShow - 1);

        const lesserIndexItems = Array.from(hiddenNumItems)
            .filter((item) => parseInt(item.getAttribute('index') ?? '0', 10) < focusedIndex)
            .sort((a, b) => parseInt(b.getAttribute('index') ?? '0', 10) - parseInt(a.getAttribute('index') ?? '0', 10)) as HTMLElement[];
        const greaterIndexItems = Array.from(hiddenNumItems)
            .filter((item) => parseInt(item.getAttribute('index') ?? '0', 10) > focusedIndex) as HTMLElement[];

        let showItems: HTMLElement[] | null = lesserIndexItems.length ? lesserIndexItems : (greaterIndexItems.length ? greaterIndexItems : null);
        for (let i = 1; i <= numToShow; i++) {
            const showItem = showItems && showItems[Math.abs(showFrom - i)];
            if (showItem) {
                showItem.classList.remove('e-hide');
                if (showItems && showItem === showItems[showItems.length - 1]) {
                    showItems = null;
                }
            }
        }
    }

    private handleResizeEnd(): void {
        if (this.resizeFinalizeTimer) {
            clearTimeout(this.resizeFinalizeTimer);
        }

        this.resizeFinalizeTimer = window.setTimeout(() => {
            this.syncVisibleNumericRange();
            this.resizeFinalizeTimer = null;
        }, 100);
    }

    private syncVisibleNumericRange(): void {
        const pagerContainer = this.element.querySelector('.e-pagercontainer');
        if (!pagerContainer) {
            return;
        }

        const visibleItems = Array.from(pagerContainer.querySelectorAll('.e-numericitem:not(.e-hide):not([style*="display: none"]):not(.e-np):not(.e-pp)'));
        if (!visibleItems.length) {
            return;
        }

        const startIndex = parseInt(visibleItems[0].getAttribute('index') ?? '', 10);
        const endIndex = parseInt(visibleItems[visibleItems.length - 1].getAttribute('index') ?? '', 10);
        if (Number.isNaN(startIndex) || Number.isNaN(endIndex)) {
            return;
        }

        this.dotnetRef.invokeMethodAsync('UpdateVisibleNumericRange', startIndex, endIndex);
    }

    private setPageSizeState(): void {
        const pagerMessageDiv = this.element.querySelector('.e-parentmsgbar') as HTMLElement | null;
        const pageSizesDiv = this.element.querySelector('.e-pagesizes') as HTMLElement | null;

        const toggleElement = (element: HTMLElement | null, isVisible: boolean): void => {
            if (isNullOrUndefined(element)) {
                return;
            }
            element.style.display = isVisible ? '' : 'none';
            element.classList.toggle('e-hide', !isVisible);
        };

        if (pagerMessageDiv) {
            toggleElement(pagerMessageDiv, true);
        }

        if (isNullOrUndefined(pageSizesDiv)) {
            const pagerWidth = this.element.offsetWidth;
            const pagerContainer = this.element.querySelector('.e-pagercontainer') as HTMLElement | null;
            const pagerContainerWidth = pagerContainer ? pagerContainer.offsetWidth : 0;
            const availablePagerSpace = pagerWidth - pagerContainerWidth;

            if (pagerMessageDiv && (pagerMessageDiv.offsetWidth + 20) > availablePagerSpace) {
                pagerMessageDiv.classList.add('e-hide');
            } else if (pagerMessageDiv) {
                pagerMessageDiv.classList.remove('e-hide');
            }
            return;
        }

        const pagerDropdown = pageSizesDiv.querySelector('.e-pagerdropdown') as HTMLElement | null;
        const pagerConstant = pageSizesDiv.querySelector('.e-pagerconstant') as HTMLElement | null;

        toggleElement(pagerDropdown, true);
        toggleElement(pagerConstant, true);
        toggleElement(pageSizesDiv, true);

        const parentWidth = this.element.offsetWidth;
        const pagerContainer = this.element.querySelector('.e-pagercontainer') as HTMLElement | null;
        const pagerContainerWidth = pagerContainer ? pagerContainer.offsetWidth : 0;
        const availableSpace = parentWidth - pagerContainerWidth - (pagerMessageDiv ? pagerMessageDiv.offsetWidth : 0);
        const pageSizesWidth = pageSizesDiv.offsetWidth;
        let otherElementsWidth = 0;
        const mfirst = this.element.querySelector('.e-mfirst') as HTMLElement | null;
        const mprev = this.element.querySelector('.e-mprev') as HTMLElement | null;
        const mnext = this.element.querySelector('.e-mnext') as HTMLElement | null;
        const mlast = this.element.querySelector('.e-mlast') as HTMLElement | null;

        if (!isNullOrUndefined(mfirst)) {
            otherElementsWidth += mfirst.offsetWidth;
        }
        if (!isNullOrUndefined(mprev)) {
            otherElementsWidth += mprev.offsetWidth;
        }
        if (!isNullOrUndefined(mnext)) {
            otherElementsWidth += mnext.offsetWidth;
        }
        if (!isNullOrUndefined(mlast)) {
            otherElementsWidth += mlast.offsetWidth;
        }

        const messageDivWidth = pagerMessageDiv ? pagerMessageDiv.offsetWidth : 0;

        if (availableSpace > pageSizesWidth + 20) {
            toggleElement(pagerDropdown, true);
            toggleElement(pagerConstant, true);
            toggleElement(pageSizesDiv, true);
        } else {
            toggleElement(pagerDropdown, false);
            toggleElement(pagerConstant, false);
            toggleElement(pageSizesDiv, false);
        }

        if (pagerContainer) {
            this.handleResizeEnd();
        }
    }

    public refresh(): void {
        /* eslint-disable @typescript-eslint/no-this-alias */
        const _this: SfPager = this;
        const mfirst: HTMLElement = _this.element.querySelector('.e-mfirst') as HTMLElement;
        const pageSize: HTMLElement = _this.element.querySelector('.e-pagesizes') as HTMLElement;
        const parentMsgBar: HTMLElement = _this.element.querySelector('.e-parentmsgbar') as HTMLElement;
        const pagerConstant: HTMLElement = _this.element.querySelector('.e-pagerconstant') as HTMLElement;
        const pagerDropDown: HTMLElement = _this.element.querySelector('.e-pagerdropdown') as HTMLElement;
        if (!isNullOrUndefined(mfirst) && mfirst.offsetWidth !== 0 && this.element.offsetWidth < 769) {
            if (this.element.offsetWidth < 481) {
                this.dotnetRef.invokeMethodAsync('IsMobileDevice', true);
            }
            else{
                this.dotnetRef.invokeMethodAsync('IsMobileDevice', false);
            }
            setTimeout((): void => {
                if (!isNullOrUndefined(pageSize)) {
                    pageSize.classList.remove('e-hide');
                }
                if (!isNullOrUndefined(parentMsgBar)) {
                    parentMsgBar.classList.remove('e-hide');
                }
                if (!isNullOrUndefined(pagerConstant)) {
                    pagerConstant.style.display = '';
                }
                if (!isNullOrUndefined(pagerDropDown)) {
                    pagerDropDown.style.display = '';
                }
                _this.element.classList.add('e-adaptive');
            }, 0);
        }
        else if (this.element.classList.contains('e-adaptive')) {
            setTimeout((): void => {
                _this.element.classList.remove('e-adaptive');
            }, 0);
            this.dotnetRef.invokeMethodAsync('IsMobileDevice', false);
        }
    }

    public focusOutElement(key: string): void {
        /* eslint-disable @typescript-eslint/no-explicit-any */
        const pageElement: any = this.element;
        const focusableSelectors: string =
            'a[href], area[href], input:not([disabled]), select:not([disabled]), ' +
            'textarea:not([disabled]), button:not([disabled]), iframe, object, embed, ' +
            '[tabindex], [contenteditable]';

        const allFocusableElementsInDom: any = Array.prototype.slice
            .call(document.querySelectorAll(focusableSelectors))
            .filter(function (el: any): boolean {
                const isVisible: boolean = el.offsetParent !== null;
                return isVisible && !pageElement.contains(el);
            });
        if (key === 'Tab') {
            // Find the first focusable element after the pager in DOM order
            const afterPagerFirstFocusableElement: any =
                allFocusableElementsInDom.filter(function (el: any): boolean {
                    return !!(
                        pageElement.compareDocumentPosition(el) &
                        Node.DOCUMENT_POSITION_FOLLOWING
                    );
                });
            if (!isNullOrUndefined(afterPagerFirstFocusableElement) && afterPagerFirstFocusableElement.length > 0) {
                afterPagerFirstFocusableElement[0].focus();
            } else {
                pageElement.blur();
            }
        }
        else if (key === 'ShiftTab') {
            const closestGrid: HTMLElement | null  = this.element.closest('.e-grid') as HTMLElement;
            const pagerDropdown: HTMLElement | null = this.element.querySelector('.e-ddl') as HTMLElement;
            if (!isNullOrUndefined(pagerDropdown) && !isNullOrUndefined(closestGrid)) {
                const emptyCell: HTMLElement | null = closestGrid.querySelector('.e-emptyrow td') as HTMLElement;
                if (!isNullOrUndefined(emptyCell)) {
                    pagerDropdown.blur();
                    const innerFocusable: NodeListOf<HTMLElement> = emptyCell.querySelectorAll(focusableSelectors);
                    if (innerFocusable.length > 0) {
                        innerFocusable[innerFocusable.length - 1].focus();
                    }
                    else {
                        emptyCell.focus();
                    }
                }
            }
        }
    }

    private getMargin(element: HTMLElement, direction: string): number {
        const margin: string = direction === 'Left' ? getComputedStyle(element).marginLeft : getComputedStyle(element).marginRight;
        return Number(margin.replace('px', ''));
    }

    private documentKeyHandler(e: KeyboardEventArgs): void {
        if (e.altKey && e.keyCode === 74 && !isNullOrUndefined(this.element))
        {
            this.element.focus();
        }
        const activeElement: Element | null = document.activeElement;
        if (e.shiftKey && e.keyCode === 9 && activeElement !== null && activeElement.classList.contains('e-pager'))
        {
            e.stopImmediatePropagation();
            if (!isNullOrUndefined(activeElement.closest('.e-grid'))) {
                /* eslint-disable @typescript-eslint/no-explicit-any */
                if ((activeElement.closest('.e-grid') as any).__eventList.events.filter((e : any) => e.name === 'shiftTabNavigation').length > 0) {
                    const customEvent: CustomEvent = new CustomEvent('shiftTabNavigation', {
                        detail: { currentTarget: activeElement }
                    });
                    activeElement.closest('.e-grid').dispatchEvent(customEvent);
                }
            }
        }

    }
    
    private pagerClickHandler(e: MouseEventArgs): void {
        const target: HTMLElement = e.target as HTMLElement;
        if (!isNullOrUndefined(target) && target.classList.contains('e-numericitem'))
        {
            e.preventDefault();
        }
    }
}

const Pager: object = {
    initialize(dataId: string, element: HTMLElement, dotnetRef: BlazorDotnetObject): void {
        enableBlazorMode();
        new SfPager(dataId, element, dotnetRef);
    },

    destroy(dataId: string): void {
        /* eslint-disable @typescript-eslint/no-explicit-any */
        const pagerInstance: any = this.sfBlazor.getCompInstance(dataId);
        pagerInstance.previousBrowserWidth = 0;
        pagerInstance.unWireEvents();
        /* eslint-disable @typescript-eslint/no-explicit-any */
        (window as any).sfBlazor.disposeWindowsInstance(dataId);
    },

    refresh(dataId: string): void {
        /* eslint-disable @typescript-eslint/no-explicit-any */
        const pagerInstance: any = this.sfBlazor.getCompInstance(dataId);
        pagerInstance.refresh();
    },

    setPageSizeState(dataId: string): void {
        /* eslint-disable @typescript-eslint/no-explicit-any */
        const pagerInstance: any = this.sfBlazor.getCompInstance(dataId);
        pagerInstance.setPageSizeState();
    },

    resizeEllipsis(dataId: string): void {
        const pagerInstance: any = this.sfBlazor.getCompInstance(dataId);
        pagerInstance.windowResized();
    },

    currentPageFocus(dataId: string, key: string, currentPage: string): void {
        /* eslint-disable @typescript-eslint/no-explicit-any */
        const pagerInstance: any = this.sfBlazor.getCompInstance(dataId);
        const numericContainer: HTMLElement | null = pagerInstance.element.querySelector('.e-numericcontainer');
        const numericElement: HTMLElement = numericContainer.querySelectorAll('.e-link:last-child')[0] as HTMLElement;
        if ((key === 'PreviousPage' || numericElement.innerText !== currentPage) && key !== numericElement.innerText && (key === 'LastPage' && numericElement.nextElementSibling !== null)) {
            (numericContainer.querySelector('.e-link') as HTMLElement).focus();
        } else if (key !== 'FirstPage' && !document.activeElement.classList.contains('e-first') && currentPage !== '1') {
            numericElement.focus({preventScroll: true});
        }
    },

    pagerFocus(dataId: string, key: string): string {
        /* eslint-disable @typescript-eslint/no-explicit-any */
        const pagerInstance: any = this.sfBlazor.getCompInstance(dataId);
        const pagerContainer: HTMLElement = pagerInstance.element.querySelector('.e-pagercontainer');
        const numericContainer: HTMLElement | null = pagerContainer.querySelector('.e-numericcontainer');
        const firstPage: HTMLElement | null = pagerContainer.querySelector('.e-firstpage.e-pager-default');
        const mFirst: HTMLElement = pagerInstance.element.querySelector('.e-mfirst');
        const mNext: HTMLElement = pagerInstance.element.querySelector('.e-mnext');
        const mPreviousPage: HTMLElement = pagerInstance.element.querySelector('.e-mprev');
        const mLastPage: HTMLElement = pagerInstance.element.querySelector('.e-mlast');
        const previousPage: HTMLElement | null = pagerContainer.querySelector('.e-prevpage.e-pager-default');
        const pagerElement: Element = pagerContainer.querySelector('.e-pp');
        const numericLink: HTMLElement = numericContainer.querySelectorAll('.e-link')[0] as HTMLElement;
        const numericAllFocusedLink: NodeListOf<HTMLElement> = (numericContainer as HTMLElement).querySelectorAll('.e-link.e-focused');
        const numericFocuedLink : HTMLElement | null = numericContainer.querySelector('.e-link.e-focused');
        const nextPager: Element = pagerContainer.querySelector('.e-nextpage');
        const lastPager: Element = pagerContainer.querySelector('.e-lastpage');
        const lastPage: Element = pagerContainer.querySelector('.e-last');
        const numericAllLink: NodeListOf<HTMLElement> = numericContainer.querySelectorAll('.e-link');
        const lastNumericLink: HTMLElement | null = numericAllLink.length > 0 ? numericAllLink[numericAllLink.length - 1] : null;
        const pagerDropdown: HTMLElement | null = pagerInstance.element.querySelector('.e-ddl');
        let previousFocus: boolean;
        const activeElement : Element | null = document.activeElement;
        if (!isNullOrUndefined(previousPage)) {
            previousFocus = (previousPage as HTMLElement).classList.contains('e-focused');
        }
        if (key === 'ArrowDown') {
            if (firstPage) {
                (firstPage as HTMLElement).focus();
                return 'FirstPage';
            } else if (previousPage) {
                (firstPage as HTMLElement).focus();
                return 'PreviousPage';
            } else {
                (numericAllLink[1] as HTMLElement).focus();
                return '1';
            }
        } else if (key === 'Tab') {
            if (window.getComputedStyle(mFirst).display === 'none') {
                if (activeElement !== null && activeElement.classList.contains('e-pager')) {
                    if (pagerContainer && pagerContainer.firstElementChild && pagerContainer.firstElementChild.classList.contains('e-firstpagedisabled')) {
                        (numericLink as HTMLElement).focus();
                        return '1';
                    } else {
                        (firstPage as HTMLElement).focus();
                        return 'FirstPage';
                    }
                }
                if (firstPage !== null && firstPage.classList.contains('e-focused')) {
                    (previousPage as HTMLElement).focus();
                    return 'PreviousPage';
                } else if (previousPage !== null && previousFocus || pagerContainer.querySelector('.e-pp.e-focused') !== null) {
                    if (pagerElement !== null && !pagerElement.classList.contains('e-focused')) {
                        (pagerElement as HTMLElement).focus();
                        return 'PreviousPagerCount';
                    } else {
                        numericLink.focus();
                        return numericLink.innerText;
                    }
                } else if (!isNullOrUndefined(numericFocuedLink) && numericAllFocusedLink.length > 0 && pagerContainer.querySelector('.e-link.e-focused') !== null && pagerContainer.querySelector('.e-link.e-focused')!.nextElementSibling !== null) {
                    const nextSibling: HTMLElement | null = numericFocuedLink.nextElementSibling as HTMLElement;
                    if (!isNullOrUndefined(nextSibling) && nextSibling.classList.contains('e-numericitem') && !nextSibling.classList.contains('e-hide')) {
                        nextSibling.focus();
                        return nextSibling.innerText;
                    } else if (pagerContainer.querySelector('.e-np') !== null) {
                        (pagerContainer.querySelector('.e-np') as HTMLElement).focus();
                        return 'NextPagerCount';
                    }
                } else if (numericAllFocusedLink.length > 0 && pagerContainer.querySelector('.e-np') !== null && pagerContainer.querySelector('.e-np.e-focused') === null) {
                    (pagerContainer.querySelector('.e-np') as HTMLElement).focus();
                    return 'NextPagerCount';
                } else if ((!isNullOrUndefined(numericFocuedLink) && numericFocuedLink.classList.contains('e-focused') && numericAllFocusedLink.length > 0) || pagerContainer.querySelectorAll('.e-np.e-focused').length > 0) {
                    if (nextPager != null) {
                        (nextPager as HTMLElement).focus();
                        return 'NextPage';
                    } else if (!isNullOrUndefined(pagerDropdown) && !pagerDropdown.classList.contains('e-input-focus')) {
                        (pagerDropdown as HTMLElement).focus();
                        return 'DropDown';
                    } else if (!isNullOrUndefined(pagerDropdown) || (activeElement === lastNumericLink && lastPage !== null && lastPage.classList.contains('e-disable'))) {
                        numericFocuedLink.blur();
                        pagerInstance.element.blur();
                        if (activeElement != null && activeElement.classList.contains('e-numericitem')) {
                            (activeElement as HTMLElement).blur();
                        }
                        pagerInstance.focusOutElement(key);
                        return 'FocusOut';
                    } else {
                        return '';
                    }
                } else if (pagerContainer.querySelector('.e-nextpage.e-focused') != null) {
                    (lastPager as HTMLElement).focus();
                    return 'LastPage';
                } else if (!isNullOrUndefined(lastPager) && !lastPager.classList.contains('e-focused') && !isNullOrUndefined(pagerDropdown) && !pagerDropdown.classList.contains('e-input-focus')) {
                    (lastPager as HTMLElement).focus();
                    return 'LastPage';
                } else if (!isNullOrUndefined(pagerDropdown) && !pagerDropdown.classList.contains('e-input-focus')) {
                    (pagerDropdown as HTMLElement).focus();
                    return 'DropDown';
                } else if (!isNullOrUndefined(lastPager) && lastPager.classList.contains('e-focused') && isNullOrUndefined(pagerDropdown)) {
                    (lastPager as HTMLElement).blur();
                    pagerInstance.focusOutElement(key);
                    return 'FocusOut';
                }
                else {
                    if (!isNullOrUndefined(pagerDropdown))
                    {
                        (pagerDropdown as HTMLElement).blur();
                    }
                    pagerInstance.element.blur();
                    pagerInstance.focusOutElement(key);
                    return 'FocusOut';
                }
            }
            else {
                if (activeElement != null && activeElement.classList.contains('e-pager')) {
                    if (mFirst.classList.contains('e-firstpagedisabled') && !mNext.classList.contains('e-nextpagedisabled')) {
                        mNext.focus();
                        return 'mNextPage';
                    } else if (!mFirst.classList.contains('e-firstpagedisabled')) {
                        mFirst.focus();
                        return 'mFirstPage';
                    } else {
                        pagerInstance.element.blur();
                        pagerInstance.focusOutElement(key);
                        return 'FocusOut';
                    }
                } else if (mFirst.classList.contains('e-focused')) {
                    mPreviousPage.focus();
                    return 'mPreviousPage';
                } else if (mPreviousPage.classList.contains('e-focused') || (activeElement !== null && activeElement === mPreviousPage)) {
                    if (!mNext.classList.contains('e-nextpagedisabled')) {
                        mNext.focus();
                        return 'mNextPage';
                    } else {
                        mPreviousPage.blur();
                        pagerInstance.focusOutElement(key);
                        return 'FocusOut';
                    }
                } else if (mNext.classList.contains('e-focused')) {
                    mLastPage.focus();
                    return 'mLastPage';
                } else if (mLastPage.classList.contains('e-focused') || (activeElement !== null && activeElement === mLastPage)) {
                    mLastPage.blur();
                    pagerInstance.focusOutElement(key);
                    return 'FocusOut';
                }
                return '0';
            }
        }
        else if (key === 'ShiftTab') {
            if (window.getComputedStyle(mFirst).display === 'none') {
                if (!isNullOrUndefined(pagerDropdown) && pagerDropdown.classList.contains('e-input-focus')) {
                    if (!isNullOrUndefined(lastPager)) {
                        (lastPager as HTMLElement).focus();
                        return 'LastPage';
                    } else {
                        if (isNullOrUndefined(numericContainer) || isNullOrUndefined(numericContainer.lastElementChild))
                        {
                            pagerInstance.focusOutElement(key);
                            return '0';
                        }
                        else {
                            (numericContainer.lastElementChild as HTMLElement).focus();
                            return (numericContainer.lastElementChild as HTMLElement).innerText;
                        }
                    }
                }
                if (previousPage != null && previousFocus) {
                    (firstPage as HTMLElement).focus();
                    return 'FirstPage';
                } else if (previousPage && pagerContainer.querySelector('.e-pp.e-focused')) {
                    (previousPage as HTMLElement).focus();
                    return 'PreviousPage';
                } else if (numericAllLink[0].classList.contains('e-focused') ||  document.activeElement === numericAllLink[0]) {
                    if (pagerElement != null) {
                        (pagerElement as HTMLElement).focus();
                        return 'PreviousPagerCount';
                    } else if (previousPage) {
                        (previousPage as HTMLElement).focus();
                        return 'PreviousPage';
                    } else {
                        numericLink.blur();
                        pagerInstance.element.focus();
                        return 'FocusOut';
                    }
                } else if (numericAllFocusedLink.length > 0) {
                    (numericFocuedLink.previousElementSibling as HTMLElement).focus();
                    return (numericFocuedLink.previousElementSibling as HTMLElement).innerText;
                } else if (pagerContainer.querySelectorAll('.e-nextpage.e-focused').length > 0 && pagerContainer.querySelector('.e-np') != null) {
                    (pagerContainer.querySelector('.e-np') as HTMLElement).focus();
                    return 'NextPagerCount';
                } else if (pagerContainer.querySelectorAll('.e-nextpage.e-focused').length > 0 || pagerContainer.querySelectorAll('.e-np.e-focused').length > 0) {
                    const visibleNumericLinks: NodeListOf<HTMLElement> = numericContainer.querySelectorAll('.e-numericitem:not(.e-hide):not(.e-np):not(.e-pp)');
                    if (visibleNumericLinks.length > 0) {
                        const lastVisibleLink: HTMLElement = visibleNumericLinks[visibleNumericLinks.length - 1];
                        lastVisibleLink.focus();
                        return lastVisibleLink.innerText;
                    }
                    const page: number = numericAllLink.length;
                    (numericAllLink[page - 1] as HTMLElement).focus();
                    return (numericContainer.querySelectorAll('.e-link:last-child')[0] as HTMLElement).innerText;
                } else if (activeElement != null && activeElement.classList.contains('e-numericitem')) {
                    const innerText: string = (activeElement.previousElementSibling as HTMLElement).innerText;
                    (activeElement.previousElementSibling as HTMLElement).focus();
                    return innerText;
                }
                else if (pagerContainer.querySelector('.e-lastpage.e-focused') != null || activeElement.classList.contains('e-lastpage')) {
                    (nextPager as HTMLElement).focus();
                    return 'NextPage';
                } else {
                    if (firstPage.classList.contains('e-focused')) {
                        firstPage.blur();
                        pagerInstance.element.focus();
                        return 'FocusOut';
                    } else if (!firstPage.classList.contains('.e-disabled')) {
                        (firstPage as HTMLElement).focus();
                        return 'FirstPage';
                    }
                    return '0';
                }
            } else {
                if (mLastPage.classList.contains('e-focused') || activeElement.classList.contains('e-lastpage')) {
                    mNext.focus();
                    return 'mNextPage';
                } else if (mNext.classList.contains('e-focused') && !mPreviousPage.classList.contains('e-prevpagedisabled')) {
                    mPreviousPage.focus();
                    return 'mPreviousPage';
                } else if (mPreviousPage.classList.contains('e-focused') || activeElement.classList.contains('e-mprev')) {
                    mFirst.focus();
                    return 'mFirstPage';
                } else if (mFirst.classList.contains('e-focused')) {
                    mFirst.blur();
                    pagerInstance.element.focus();
                    return 'FocusOut';
                } else if (mNext.classList.contains('e-focused')) {
                    mNext.blur();
                    pagerInstance.element.focus();
                    return 'FocusOut';
                }
                return '0';
            }
        }
        else { return '0'; }
    }
};

export default Pager;
