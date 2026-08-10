window.sfBlazor = window.sfBlazor || {};
window.sfBlazor.Pager = (function () {
    'use strict';

    /**
     * @hidden
     * Traverses up the DOM tree from a given element until it finds a parent element that matches the selector.
     *
     * @param {Element} elem The starting element for traversal.
     * @param {string} selector The selector to match against parent elements.
     * @param {boolean} [isID=false] Optional flag indicating if the selector is an ID.
     * @returns {Element | null} The matching parent element, or null if none found.
     */
    function parentsUntil(elem, selector, isID) {
        var parent = elem;
        while (parent) {
            if (isID ? parent.id === selector : parent.classList.contains(selector)) {
                break;
            }
            parent = parent.parentElement;
        }
        return parent;
    }
    // If Blazor's .NET interop manages classes, this would be called differently.
    var classList = (function () {
        return function (element, addClasses, removeClasses) {
            if (!element) {
                return;
            }
            if (addClasses) {
                addClasses.forEach(function (cls) { return element.classList.add(cls); });
            }
            if (removeClasses) {
                removeClasses.forEach(function (cls) { return element.classList.remove(cls); });
            }
        };
    })();
    /**
     * Client side script for Blazor Pager
     */
    var SfPager = /** @class */ (function () {
        function SfPager(dataId, element, dotnetRef) {
            this.previousBrowserWidth = 0;
            this.totalPages = 0;
            this.element = element;
            this.dataId = dataId;
            this.dotnetRef = dotnetRef;
            this.resizeFinalizeTimer = null;
            this.windowResized();
            this.wireEvents();
            /* eslint-disable @typescript-eslint/no-explicit-any */
            window.sfBlazor.setCompInstance(this);
        }
        SfPager.prototype.wireEvents = function () {
            sf.base.EventHandler.add(this.element, 'keydown', this.documentKeyHandler, this);
            sf.base.EventHandler.add(this.element, 'click', this.pagerClickHandler, this);
            /* eslint-disable @typescript-eslint/no-explicit-any */
            sf.base.EventHandler.add(window, 'resize', this.windowResized, this);
        };
        SfPager.prototype.unWireEvents = function () {
            sf.base.EventHandler.remove(this.element, 'keydown', this.documentKeyHandler);
            sf.base.EventHandler.remove(this.element, 'click', this.pagerClickHandler);
            /* eslint-disable @typescript-eslint/no-explicit-any */
            sf.base.EventHandler.remove(window, 'resize', this.windowResized);
            // Clear any pending resize timer and force final server sync
            if (this.resizeFinalizeTimer) {
                clearTimeout(this.resizeFinalizeTimer);
                this.resizeFinalizeTimer = null;
            }
        };
        SfPager.prototype.windowResized = function () {
            /* eslint-disable @typescript-eslint/no-this-alias */
            var _this = this;
            if (_this.resizeFinalizeTimer) {
                clearTimeout(_this.resizeFinalizeTimer);
            }
            // Set debounce timer to trigger server sync after resize ends
            _this.resizeFinalizeTimer = window.setTimeout(function () {
                _this.handleResizeEnd();
            }, 100);
            setTimeout(function () {
                var pagerMessageDiv = _this.element.querySelector('.e-parentmsgbar');
                var mfirst = _this.element.querySelector('.e-mfirst');
                if (!sf.base.isNullOrUndefined(mfirst) && mfirst.offsetWidth === 0) {
                    var pagerWidth = _this.element.offsetWidth;
                    var pagerWithoutMargin = _this.element.offsetWidth - _this.getMargin(_this.element, 'Left') - _this.getMargin(_this.element, 'Right') - 16;
                    var numericContainerWidth = _this.element.querySelector('.e-pagercontainer').offsetWidth + _this.getMargin(_this.element.querySelector('.e-pagercontainer'), 'Left') + 4;
                    var pageSizesDiv = _this.element.querySelector('.e-pagesizes');
                    var pagerDropdown = void 0;
                    var pagerConstant = void 0;
                    var pageSizesOffsetWidth = 0;
                    if (_this.element.classList.contains('e-adaptive')) {
                        setTimeout(function () {
                            _this.element.classList.remove('e-adaptive');
                        }, 0);
                        // When transitioning from mobile to desktop, initialize e-parentmsgbar with e-hide
                        if (!sf.base.isNullOrUndefined(pagerMessageDiv)) {
                            pagerMessageDiv.classList.add('e-hide');
                            // Restore numeric items hidden while in mobile mode so desktop shows initial set
                            setTimeout(function () {
                                try {
                                    var pagerContainerRestore = _this.element.querySelector('.e-pagercontainer');
                                    if (pagerContainerRestore) {
                                        var hiddenItems = pagerContainerRestore.querySelectorAll('.e-numericitem.e-hide');
                                        for (var i = 0; i < hiddenItems.length; i++) {
                                            hiddenItems[i].classList.remove('e-hide');
                                        }
                                    }
                                }
                                catch (e) { }
                            }, 0);
                        }
                        _this.dotnetRef.invokeMethodAsync('IsMobileDevice', false);
                    }
                    if (!sf.base.isNullOrUndefined(pageSizesDiv)) {
                        pagerDropdown = pageSizesDiv.querySelector('.e-pagerdropdown');
                        pagerConstant = pageSizesDiv.querySelector('.e-pagerconstant');
                        pageSizesOffsetWidth = pagerDropdown.offsetWidth + _this.getMargin(pagerDropdown, 'Left') + _this.getMargin(pagerDropdown, 'Right') +
                            pagerConstant.offsetWidth + _this.getMargin(pagerConstant, 'Left') + _this.getMargin(pagerConstant, 'Right') + 5;
                    }
                    if (!sf.base.isNullOrUndefined(pagerMessageDiv) && numericContainerWidth + pageSizesOffsetWidth + pagerMessageDiv.offsetWidth >
                        pagerWithoutMargin
                        && _this.previousBrowserWidth > pagerWidth) {
                        //Executed when decreasing the browser width
                        pagerMessageDiv.style.display = 'none';
                        if (numericContainerWidth + pageSizesOffsetWidth > pagerWithoutMargin) {
                            if (!sf.base.isNullOrUndefined(pageSizesDiv)) {
                                pagerConstant.style.display = 'none';
                                if (numericContainerWidth + pagerDropdown.offsetWidth + _this.getMargin(pagerDropdown, 'Left') + _this.getMargin(pagerDropdown, 'Right') + 3 > pagerWithoutMargin) {
                                    pagerDropdown.style.display = 'none';
                                }
                            }
                        }
                    }
                    else if (!sf.base.isNullOrUndefined(pagerMessageDiv) && _this.previousBrowserWidth < pagerWidth) {
                        //Executed when increasing the browser width
                        if (numericContainerWidth + pageSizesOffsetWidth < pagerWithoutMargin) {
                            if (!sf.base.isNullOrUndefined(pageSizesDiv)) {
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
                                if (!sf.base.isNullOrUndefined(pagerMessageDiv) && pagerWidth > 900) {
                                    pagerMessageDiv.style.visibility = 'visible';
                                    pagerMessageDiv.classList.remove('e-hide');
                                }
                            }
                        }
                        else if (!sf.base.isNullOrUndefined(pageSizesDiv)) {
                            //Executed when browser is resized from smaller size(less than 769px) to little larger but not much space to render the pagerMessageDiv
                            pagerMessageDiv.style.display = 'none';
                        }
                    }
                }
                else if (!sf.base.isNullOrUndefined(pagerMessageDiv)) {
                    //To render the parentmessagebar div when browser width is less than 769px(small devices)
                    pagerMessageDiv.style.display = '';
                    _this.refresh();
                }
                _this.previousBrowserWidth = _this.element.offsetWidth;
            }, 50);
            var isStyleApplied = this.element.classList.contains('e-pager')
                ? getComputedStyle(this.element).getPropertyValue('padding') !== '0px'
                : false;
            if (isStyleApplied) {
                this.resizePager();
            }
        };
        SfPager.prototype.resizePager = function () {
            var pagerElements = Array.from(this.element.querySelectorAll('.e-mfirst, .e-mprev, .e-icon-first, .e-icon-prev, .e-pp:not(.e-disable), .e-numericitem, .e-numericitem.e-active.e-hide, .e-np:not(.e-disable), .e-icon-next, .e-icon-last, .e-parentmsgbar, .e-mnext, .e-mlast, .e-pagerdropdown, .e-pagerconstant'));
            var actualWidth = 0;
            for (var i = 0; i < pagerElements.length; i++) {
                var item = pagerElements[i];
                if (getComputedStyle(item).display !== 'none') {
                    actualWidth += item.offsetWidth
                        + parseFloat(getComputedStyle(item).marginLeft)
                        + parseFloat(getComputedStyle(item).marginRight);
                }
            }
            var pagerContainer = this.element.querySelector('.e-pagercontainer');
            if (!pagerContainer) {
                return;
            }
            actualWidth += parseFloat(getComputedStyle(pagerContainer).marginLeft)
                + parseFloat(getComputedStyle(pagerContainer).marginRight);
            var pagerWidth = this.element.clientWidth
                - parseFloat(getComputedStyle(this.element).paddingLeft)
                - parseFloat(getComputedStyle(this.element).paddingRight)
                - parseFloat(getComputedStyle(this.element).marginLeft)
                - parseFloat(getComputedStyle(this.element).marginRight);
            var numItems = Array.from(pagerContainer.querySelectorAll('.e-numericitem:not(.e-hide):not([style*="display: none"]):not(.e-np):not(.e-pp)'));
            var hiddenNumItems = pagerContainer.querySelectorAll('.e-numericitem.e-hide:not([style*="display: none"])');
            var hideFrom = numItems.length;
            var showFrom = 1;
            var bufferWidth = parentsUntil(this.element, 'e-bigger') ? 10 : 5;
            var nextPageElement = pagerContainer.querySelector('.e-np');
            var previousPageElement = pagerContainer.querySelector('.e-pp');
            var numItemsArray = Array.from(pagerContainer.querySelectorAll('.e-numericitem:not(.e-np):not(.e-pp)'));
            var focusedIndex = numItemsArray.findIndex(function (item) { return item.classList.contains('e-currentitem'); }) + 1;
            if (!numItems.length) {
                return;
            }
            var totalWidth = 0;
            for (var i = 0; i < numItems.length; i++) {
                var item = numItems[i];
                totalWidth += item.offsetWidth
                    + parseFloat(getComputedStyle(item).marginLeft)
                    + parseFloat(getComputedStyle(item).marginRight);
            }
            var numericItemWidth = totalWidth / numItems.length;
            if (actualWidth >= (pagerWidth - numericItemWidth) && numItems.length > 1) {
                var diff = Math.abs(actualWidth - pagerWidth);
                var numToHide = Math.floor(diff / numericItemWidth);
                numToHide = (numToHide === 0) ? 1 : (numToHide > numItems.length) ? (numItems.length - 1) : numToHide;
                this.hideNumericItems(pagerContainer, numItems, hideFrom, focusedIndex, nextPageElement, previousPageElement, numToHide);
            }
            else if (actualWidth < pagerWidth && hiddenNumItems.length && window.innerWidth >= 768 && this.previousBrowserWidth < this.element.offsetWidth) {
                this.showNumericItems(hiddenNumItems, pagerWidth, actualWidth, numericItemWidth, bufferWidth, focusedIndex, showFrom);
            }
        };
        SfPager.prototype.hideNumericItems = function (pagerContainer, numItems, hideFrom, focusedIndex, nextPageElement, previousPageElement, numToHide) {
            var _a, _b, _c, _d, _e;
            if (focusedIndex !== this.totalPages) {
                classList(nextPageElement, ['e-numericitem', 'e-pager-default'], ['e-nextprevitemdisabled', 'e-disable']);
            }
            var tempfocusElement = -1;
            numItems.forEach(function (item, index) {
                if (item.classList.contains('e-currentitem')) {
                    tempfocusElement = index + 1;
                }
            });
            for (var i = 1; i <= numToHide; i++) {
                var hideIndex = hideFrom - parseInt(i.toString(), 10);
                numItems = Array.from(pagerContainer.querySelectorAll('.e-numericitem:not(.e-hide):not([style*="display: none"]):not(.e-np):not(.e-pp)'));
                if (focusedIndex !== 1 && (tempfocusElement === hideIndex + 1 ||
                    parseInt((_b = (_a = numItems[Math.abs(hideIndex)]) === null || _a === void 0 ? void 0 : _a.getAttribute('index')) !== null && _b !== void 0 ? _b : '0', 10) === focusedIndex ||
                    parseInt((_d = (_c = numItems[numItems.length - 1]) === null || _c === void 0 ? void 0 : _c.getAttribute('index')) !== null && _d !== void 0 ? _d : '0', 10) === focusedIndex)) {
                    hideIndex = 0;
                    classList(previousPageElement, ['e-numericitem', 'e-pager-default'], ['e-nextprevitemdisabled', 'e-disable']);
                }
                (_e = numItems[Math.abs(hideIndex)]) === null || _e === void 0 ? void 0 : _e.classList.add('e-hide');
            }
        };
        SfPager.prototype.showNumericItems = function (hiddenNumItems, pagerWidth, actualWidth, numericItemWidth, bufferWidth, focusedIndex, showFrom) {
            var diff = Math.abs(pagerWidth - actualWidth);
            if (diff <= (numericItemWidth * 2)) {
                return;
            }
            var numToShow = Math.floor(diff / (numericItemWidth + bufferWidth));
            numToShow = (numToShow > hiddenNumItems.length) ? hiddenNumItems.length : (numToShow - 1);
            var lesserIndexItems = Array.from(hiddenNumItems)
                .filter(function (item) { var _a; return parseInt((_a = item.getAttribute('index')) !== null && _a !== void 0 ? _a : '0', 10) < focusedIndex; })
                .sort(function (a, b) { var _a, _b; return parseInt((_a = b.getAttribute('index')) !== null && _a !== void 0 ? _a : '0', 10) - parseInt((_b = a.getAttribute('index')) !== null && _b !== void 0 ? _b : '0', 10); });
            var greaterIndexItems = Array.from(hiddenNumItems)
                .filter(function (item) { var _a; return parseInt((_a = item.getAttribute('index')) !== null && _a !== void 0 ? _a : '0', 10) > focusedIndex; });
            var showItems = lesserIndexItems.length ? lesserIndexItems : (greaterIndexItems.length ? greaterIndexItems : null);
            for (var i = 1; i <= numToShow; i++) {
                var showItem = showItems && showItems[Math.abs(showFrom - i)];
                if (showItem) {
                    showItem.classList.remove('e-hide');
                    if (showItems && showItem === showItems[showItems.length - 1]) {
                        showItems = null;
                    }
                }
            }
        };
        SfPager.prototype.handleResizeEnd = function () {
            var _this_1 = this;
            if (this.resizeFinalizeTimer) {
                clearTimeout(this.resizeFinalizeTimer);
            }
            this.resizeFinalizeTimer = window.setTimeout(function () {
                _this_1.syncVisibleNumericRange();
                _this_1.resizeFinalizeTimer = null;
            }, 100);
        };
        SfPager.prototype.syncVisibleNumericRange = function () {
            var _a, _b;
            var pagerContainer = this.element.querySelector('.e-pagercontainer');
            if (!pagerContainer) {
                return;
            }
            var visibleItems = Array.from(pagerContainer.querySelectorAll('.e-numericitem:not(.e-hide):not([style*="display: none"]):not(.e-np):not(.e-pp)'));
            if (!visibleItems.length) {
                return;
            }
            var startIndex = parseInt((_a = visibleItems[0].getAttribute('index')) !== null && _a !== void 0 ? _a : '', 10);
            var endIndex = parseInt((_b = visibleItems[visibleItems.length - 1].getAttribute('index')) !== null && _b !== void 0 ? _b : '', 10);
            if (Number.isNaN(startIndex) || Number.isNaN(endIndex)) {
                return;
            }
            this.dotnetRef.invokeMethodAsync('UpdateVisibleNumericRange', startIndex, endIndex);
        };
        SfPager.prototype.setPageSizeState = function () {
            var pagerMessageDiv = this.element.querySelector('.e-parentmsgbar');
            var pageSizesDiv = this.element.querySelector('.e-pagesizes');
            var toggleElement = function (element, isVisible) {
                if (sf.base.isNullOrUndefined(element)) {
                    return;
                }
                element.style.display = isVisible ? '' : 'none';
                element.classList.toggle('e-hide', !isVisible);
            };
            if (pagerMessageDiv) {
                toggleElement(pagerMessageDiv, true);
            }
            if (sf.base.isNullOrUndefined(pageSizesDiv)) {
                var pagerWidth = this.element.offsetWidth;
                var pagerContainer_1 = this.element.querySelector('.e-pagercontainer');
                var pagerContainerWidth_1 = pagerContainer_1 ? pagerContainer_1.offsetWidth : 0;
                var availablePagerSpace = pagerWidth - pagerContainerWidth_1;
                if (pagerMessageDiv && (pagerMessageDiv.offsetWidth + 20) > availablePagerSpace) {
                    pagerMessageDiv.classList.add('e-hide');
                }
                else if (pagerMessageDiv) {
                    pagerMessageDiv.classList.remove('e-hide');
                }
                return;
            }
            var pagerDropdown = pageSizesDiv.querySelector('.e-pagerdropdown');
            var pagerConstant = pageSizesDiv.querySelector('.e-pagerconstant');
            toggleElement(pagerDropdown, true);
            toggleElement(pagerConstant, true);
            toggleElement(pageSizesDiv, true);
            var parentWidth = this.element.offsetWidth;
            var pagerContainer = this.element.querySelector('.e-pagercontainer');
            var pagerContainerWidth = pagerContainer ? pagerContainer.offsetWidth : 0;
            var availableSpace = parentWidth - pagerContainerWidth - (pagerMessageDiv ? pagerMessageDiv.offsetWidth : 0);
            var pageSizesWidth = pageSizesDiv.offsetWidth;
            var otherElementsWidth = 0;
            var mfirst = this.element.querySelector('.e-mfirst');
            var mprev = this.element.querySelector('.e-mprev');
            var mnext = this.element.querySelector('.e-mnext');
            var mlast = this.element.querySelector('.e-mlast');
            if (!sf.base.isNullOrUndefined(mfirst)) {
                otherElementsWidth += mfirst.offsetWidth;
            }
            if (!sf.base.isNullOrUndefined(mprev)) {
                otherElementsWidth += mprev.offsetWidth;
            }
            if (!sf.base.isNullOrUndefined(mnext)) {
                otherElementsWidth += mnext.offsetWidth;
            }
            if (!sf.base.isNullOrUndefined(mlast)) {
                otherElementsWidth += mlast.offsetWidth;
            }
            pagerMessageDiv ? pagerMessageDiv.offsetWidth : 0;
            if (availableSpace > pageSizesWidth + 20) {
                toggleElement(pagerDropdown, true);
                toggleElement(pagerConstant, true);
                toggleElement(pageSizesDiv, true);
            }
            else {
                toggleElement(pagerDropdown, false);
                toggleElement(pagerConstant, false);
                toggleElement(pageSizesDiv, false);
            }
            if (pagerContainer) {
                this.handleResizeEnd();
            }
        };
        SfPager.prototype.refresh = function () {
            /* eslint-disable @typescript-eslint/no-this-alias */
            var _this = this;
            var mfirst = _this.element.querySelector('.e-mfirst');
            var pageSize = _this.element.querySelector('.e-pagesizes');
            var parentMsgBar = _this.element.querySelector('.e-parentmsgbar');
            var pagerConstant = _this.element.querySelector('.e-pagerconstant');
            var pagerDropDown = _this.element.querySelector('.e-pagerdropdown');
            if (!sf.base.isNullOrUndefined(mfirst) && mfirst.offsetWidth !== 0 && this.element.offsetWidth < 769) {
                if (this.element.offsetWidth < 481) {
                    this.dotnetRef.invokeMethodAsync('IsMobileDevice', true);
                }
                else {
                    this.dotnetRef.invokeMethodAsync('IsMobileDevice', false);
                }
                setTimeout(function () {
                    if (!sf.base.isNullOrUndefined(pageSize)) {
                        pageSize.classList.remove('e-hide');
                    }
                    if (!sf.base.isNullOrUndefined(parentMsgBar)) {
                        parentMsgBar.classList.remove('e-hide');
                    }
                    if (!sf.base.isNullOrUndefined(pagerConstant)) {
                        pagerConstant.style.display = '';
                    }
                    if (!sf.base.isNullOrUndefined(pagerDropDown)) {
                        pagerDropDown.style.display = '';
                    }
                    _this.element.classList.add('e-adaptive');
                }, 0);
            }
            else if (this.element.classList.contains('e-adaptive')) {
                setTimeout(function () {
                    _this.element.classList.remove('e-adaptive');
                }, 0);
                this.dotnetRef.invokeMethodAsync('IsMobileDevice', false);
            }
        };
        SfPager.prototype.focusOutElement = function (key) {
            /* eslint-disable @typescript-eslint/no-explicit-any */
            var pageElement = this.element;
            var focusableSelectors = 'a[href], area[href], input:not([disabled]), select:not([disabled]), ' +
                'textarea:not([disabled]), button:not([disabled]), iframe, object, embed, ' +
                '[tabindex], [contenteditable]';
            var allFocusableElementsInDom = Array.prototype.slice
                .call(document.querySelectorAll(focusableSelectors))
                .filter(function (el) {
                var isVisible = el.offsetParent !== null;
                return isVisible && !pageElement.contains(el);
            });
            if (key === 'Tab') {
                // Find the first focusable element after the pager in DOM order
                var afterPagerFirstFocusableElement = allFocusableElementsInDom.filter(function (el) {
                    return !!(pageElement.compareDocumentPosition(el) &
                        Node.DOCUMENT_POSITION_FOLLOWING);
                });
                if (!sf.base.isNullOrUndefined(afterPagerFirstFocusableElement) && afterPagerFirstFocusableElement.length > 0) {
                    afterPagerFirstFocusableElement[0].focus();
                }
                else {
                    pageElement.blur();
                }
            }
            else if (key === 'ShiftTab') {
                var closestGrid = this.element.closest('.e-grid');
                var pagerDropdown = this.element.querySelector('.e-ddl');
                if (!sf.base.isNullOrUndefined(pagerDropdown) && !sf.base.isNullOrUndefined(closestGrid)) {
                    var emptyCell = closestGrid.querySelector('.e-emptyrow td');
                    if (!sf.base.isNullOrUndefined(emptyCell)) {
                        pagerDropdown.blur();
                        var innerFocusable = emptyCell.querySelectorAll(focusableSelectors);
                        if (innerFocusable.length > 0) {
                            innerFocusable[innerFocusable.length - 1].focus();
                        }
                        else {
                            emptyCell.focus();
                        }
                    }
                }
            }
        };
        SfPager.prototype.getMargin = function (element, direction) {
            var margin = direction === 'Left' ? getComputedStyle(element).marginLeft : getComputedStyle(element).marginRight;
            return Number(margin.replace('px', ''));
        };
        SfPager.prototype.documentKeyHandler = function (e) {
            if (e.altKey && e.keyCode === 74 && !sf.base.isNullOrUndefined(this.element)) {
                this.element.focus();
            }
            var activeElement = document.activeElement;
            if (e.shiftKey && e.keyCode === 9 && activeElement !== null && activeElement.classList.contains('e-pager')) {
                e.stopImmediatePropagation();
                if (!sf.base.isNullOrUndefined(activeElement.closest('.e-grid'))) {
                    /* eslint-disable @typescript-eslint/no-explicit-any */
                    if (activeElement.closest('.e-grid').__eventList.events.filter(function (e) { return e.name === 'shiftTabNavigation'; }).length > 0) {
                        var customEvent = new CustomEvent('shiftTabNavigation', {
                            detail: { currentTarget: activeElement }
                        });
                        activeElement.closest('.e-grid').dispatchEvent(customEvent);
                    }
                }
            }
        };
        SfPager.prototype.pagerClickHandler = function (e) {
            var target = e.target;
            if (!sf.base.isNullOrUndefined(target) && target.classList.contains('e-numericitem')) {
                e.preventDefault();
            }
        };
        return SfPager;
    }());
    var Pager = {
        initialize: function (dataId, element, dotnetRef) {
            sf.base.enableBlazorMode();
            new SfPager(dataId, element, dotnetRef);
        },
        destroy: function (dataId) {
            /* eslint-disable @typescript-eslint/no-explicit-any */
            var pagerInstance = window.sfBlazor.getCompInstance(dataId);
            pagerInstance.previousBrowserWidth = 0;
            pagerInstance.unWireEvents();
            /* eslint-disable @typescript-eslint/no-explicit-any */
            window.sfBlazor.disposeWindowsInstance(dataId);
        },
        refresh: function (dataId) {
            /* eslint-disable @typescript-eslint/no-explicit-any */
            var pagerInstance = window.sfBlazor.getCompInstance(dataId);
            pagerInstance.refresh();
        },
        setPageSizeState: function (dataId) {
            /* eslint-disable @typescript-eslint/no-explicit-any */
            var pagerInstance = window.sfBlazor.getCompInstance(dataId);
            pagerInstance.setPageSizeState();
        },
        resizeEllipsis: function (dataId) {
            var pagerInstance = window.sfBlazor.getCompInstance(dataId);
            pagerInstance.windowResized();
        },
        currentPageFocus: function (dataId, key, currentPage) {
            /* eslint-disable @typescript-eslint/no-explicit-any */
            var pagerInstance = window.sfBlazor.getCompInstance(dataId);
            var numericContainer = pagerInstance.element.querySelector('.e-numericcontainer');
            var numericElement = numericContainer.querySelectorAll('.e-link:last-child')[0];
            if ((key === 'PreviousPage' || numericElement.innerText !== currentPage) && key !== numericElement.innerText && (key === 'LastPage' && numericElement.nextElementSibling !== null)) {
                numericContainer.querySelector('.e-link').focus();
            }
            else if (key !== 'FirstPage' && !document.activeElement.classList.contains('e-first') && currentPage !== '1') {
                numericElement.focus({ preventScroll: true });
            }
        },
        pagerFocus: function (dataId, key) {
            /* eslint-disable @typescript-eslint/no-explicit-any */
            var pagerInstance = window.sfBlazor.getCompInstance(dataId);
            var pagerContainer = pagerInstance.element.querySelector('.e-pagercontainer');
            var numericContainer = pagerContainer.querySelector('.e-numericcontainer');
            var firstPage = pagerContainer.querySelector('.e-firstpage.e-pager-default');
            var mFirst = pagerInstance.element.querySelector('.e-mfirst');
            var mNext = pagerInstance.element.querySelector('.e-mnext');
            var mPreviousPage = pagerInstance.element.querySelector('.e-mprev');
            var mLastPage = pagerInstance.element.querySelector('.e-mlast');
            var previousPage = pagerContainer.querySelector('.e-prevpage.e-pager-default');
            var pagerElement = pagerContainer.querySelector('.e-pp');
            var numericLink = numericContainer.querySelectorAll('.e-link')[0];
            var numericAllFocusedLink = numericContainer.querySelectorAll('.e-link.e-focused');
            var numericFocuedLink = numericContainer.querySelector('.e-link.e-focused');
            var nextPager = pagerContainer.querySelector('.e-nextpage');
            var lastPager = pagerContainer.querySelector('.e-lastpage');
            var lastPage = pagerContainer.querySelector('.e-last');
            var numericAllLink = numericContainer.querySelectorAll('.e-link');
            var lastNumericLink = numericAllLink.length > 0 ? numericAllLink[numericAllLink.length - 1] : null;
            var pagerDropdown = pagerInstance.element.querySelector('.e-ddl');
            var previousFocus;
            var activeElement = document.activeElement;
            if (!sf.base.isNullOrUndefined(previousPage)) {
                previousFocus = previousPage.classList.contains('e-focused');
            }
            if (key === 'ArrowDown') {
                if (firstPage) {
                    firstPage.focus();
                    return 'FirstPage';
                }
                else if (previousPage) {
                    firstPage.focus();
                    return 'PreviousPage';
                }
                else {
                    numericAllLink[1].focus();
                    return '1';
                }
            }
            else if (key === 'Tab') {
                if (window.getComputedStyle(mFirst).display === 'none') {
                    if (activeElement !== null && activeElement.classList.contains('e-pager')) {
                        if (pagerContainer && pagerContainer.firstElementChild && pagerContainer.firstElementChild.classList.contains('e-firstpagedisabled')) {
                            numericLink.focus();
                            return '1';
                        }
                        else {
                            firstPage.focus();
                            return 'FirstPage';
                        }
                    }
                    if (firstPage !== null && firstPage.classList.contains('e-focused')) {
                        previousPage.focus();
                        return 'PreviousPage';
                    }
                    else if (previousPage !== null && previousFocus || pagerContainer.querySelector('.e-pp.e-focused') !== null) {
                        if (pagerElement !== null && !pagerElement.classList.contains('e-focused')) {
                            pagerElement.focus();
                            return 'PreviousPagerCount';
                        }
                        else {
                            numericLink.focus();
                            return numericLink.innerText;
                        }
                    }
                    else if (!sf.base.isNullOrUndefined(numericFocuedLink) && numericAllFocusedLink.length > 0 && pagerContainer.querySelector('.e-link.e-focused') !== null && pagerContainer.querySelector('.e-link.e-focused').nextElementSibling !== null) {
                        var nextSibling = numericFocuedLink.nextElementSibling;
                        if (!sf.base.isNullOrUndefined(nextSibling) && nextSibling.classList.contains('e-numericitem') && !nextSibling.classList.contains('e-hide')) {
                            nextSibling.focus();
                            return nextSibling.innerText;
                        }
                        else if (pagerContainer.querySelector('.e-np') !== null) {
                            pagerContainer.querySelector('.e-np').focus();
                            return 'NextPagerCount';
                        }
                    }
                    else if (numericAllFocusedLink.length > 0 && pagerContainer.querySelector('.e-np') !== null && pagerContainer.querySelector('.e-np.e-focused') === null) {
                        pagerContainer.querySelector('.e-np').focus();
                        return 'NextPagerCount';
                    }
                    else if ((!sf.base.isNullOrUndefined(numericFocuedLink) && numericFocuedLink.classList.contains('e-focused') && numericAllFocusedLink.length > 0) || pagerContainer.querySelectorAll('.e-np.e-focused').length > 0) {
                        if (nextPager != null) {
                            nextPager.focus();
                            return 'NextPage';
                        }
                        else if (!sf.base.isNullOrUndefined(pagerDropdown) && !pagerDropdown.classList.contains('e-input-focus')) {
                            pagerDropdown.focus();
                            return 'DropDown';
                        }
                        else if (!sf.base.isNullOrUndefined(pagerDropdown) || (activeElement === lastNumericLink && lastPage !== null && lastPage.classList.contains('e-disable'))) {
                            numericFocuedLink.blur();
                            pagerInstance.element.blur();
                            if (activeElement != null && activeElement.classList.contains('e-numericitem')) {
                                activeElement.blur();
                            }
                            pagerInstance.focusOutElement(key);
                            return 'FocusOut';
                        }
                        else {
                            return '';
                        }
                    }
                    else if (pagerContainer.querySelector('.e-nextpage.e-focused') != null) {
                        lastPager.focus();
                        return 'LastPage';
                    }
                    else if (!sf.base.isNullOrUndefined(lastPager) && !lastPager.classList.contains('e-focused') && !sf.base.isNullOrUndefined(pagerDropdown) && !pagerDropdown.classList.contains('e-input-focus')) {
                        lastPager.focus();
                        return 'LastPage';
                    }
                    else if (!sf.base.isNullOrUndefined(pagerDropdown) && !pagerDropdown.classList.contains('e-input-focus')) {
                        pagerDropdown.focus();
                        return 'DropDown';
                    }
                    else if (!sf.base.isNullOrUndefined(lastPager) && lastPager.classList.contains('e-focused') && sf.base.isNullOrUndefined(pagerDropdown)) {
                        lastPager.blur();
                        pagerInstance.focusOutElement(key);
                        return 'FocusOut';
                    }
                    else {
                        if (!sf.base.isNullOrUndefined(pagerDropdown)) {
                            pagerDropdown.blur();
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
                        }
                        else if (!mFirst.classList.contains('e-firstpagedisabled')) {
                            mFirst.focus();
                            return 'mFirstPage';
                        }
                        else {
                            pagerInstance.element.blur();
                            pagerInstance.focusOutElement(key);
                            return 'FocusOut';
                        }
                    }
                    else if (mFirst.classList.contains('e-focused')) {
                        mPreviousPage.focus();
                        return 'mPreviousPage';
                    }
                    else if (mPreviousPage.classList.contains('e-focused') || (activeElement !== null && activeElement === mPreviousPage)) {
                        if (!mNext.classList.contains('e-nextpagedisabled')) {
                            mNext.focus();
                            return 'mNextPage';
                        }
                        else {
                            mPreviousPage.blur();
                            pagerInstance.focusOutElement(key);
                            return 'FocusOut';
                        }
                    }
                    else if (mNext.classList.contains('e-focused')) {
                        mLastPage.focus();
                        return 'mLastPage';
                    }
                    else if (mLastPage.classList.contains('e-focused') || (activeElement !== null && activeElement === mLastPage)) {
                        mLastPage.blur();
                        pagerInstance.focusOutElement(key);
                        return 'FocusOut';
                    }
                    return '0';
                }
            }
            else if (key === 'ShiftTab') {
                if (window.getComputedStyle(mFirst).display === 'none') {
                    if (!sf.base.isNullOrUndefined(pagerDropdown) && pagerDropdown.classList.contains('e-input-focus')) {
                        if (!sf.base.isNullOrUndefined(lastPager)) {
                            lastPager.focus();
                            return 'LastPage';
                        }
                        else {
                            if (sf.base.isNullOrUndefined(numericContainer) || sf.base.isNullOrUndefined(numericContainer.lastElementChild)) {
                                pagerInstance.focusOutElement(key);
                                return '0';
                            }
                            else {
                                numericContainer.lastElementChild.focus();
                                return numericContainer.lastElementChild.innerText;
                            }
                        }
                    }
                    if (previousPage != null && previousFocus) {
                        firstPage.focus();
                        return 'FirstPage';
                    }
                    else if (previousPage && pagerContainer.querySelector('.e-pp.e-focused')) {
                        previousPage.focus();
                        return 'PreviousPage';
                    }
                    else if (numericAllLink[0].classList.contains('e-focused') || document.activeElement === numericAllLink[0]) {
                        if (pagerElement != null) {
                            pagerElement.focus();
                            return 'PreviousPagerCount';
                        }
                        else if (previousPage) {
                            previousPage.focus();
                            return 'PreviousPage';
                        }
                        else {
                            numericLink.blur();
                            pagerInstance.element.focus();
                            return 'FocusOut';
                        }
                    }
                    else if (numericAllFocusedLink.length > 0) {
                        numericFocuedLink.previousElementSibling.focus();
                        return numericFocuedLink.previousElementSibling.innerText;
                    }
                    else if (pagerContainer.querySelectorAll('.e-nextpage.e-focused').length > 0 && pagerContainer.querySelector('.e-np') != null) {
                        pagerContainer.querySelector('.e-np').focus();
                        return 'NextPagerCount';
                    }
                    else if (pagerContainer.querySelectorAll('.e-nextpage.e-focused').length > 0 || pagerContainer.querySelectorAll('.e-np.e-focused').length > 0) {
                        var visibleNumericLinks = numericContainer.querySelectorAll('.e-numericitem:not(.e-hide):not(.e-np):not(.e-pp)');
                        if (visibleNumericLinks.length > 0) {
                            var lastVisibleLink = visibleNumericLinks[visibleNumericLinks.length - 1];
                            lastVisibleLink.focus();
                            return lastVisibleLink.innerText;
                        }
                        var page = numericAllLink.length;
                        numericAllLink[page - 1].focus();
                        return numericContainer.querySelectorAll('.e-link:last-child')[0].innerText;
                    }
                    else if (activeElement != null && activeElement.classList.contains('e-numericitem')) {
                        var innerText = activeElement.previousElementSibling.innerText;
                        activeElement.previousElementSibling.focus();
                        return innerText;
                    }
                    else if (pagerContainer.querySelector('.e-lastpage.e-focused') != null || activeElement.classList.contains('e-lastpage')) {
                        nextPager.focus();
                        return 'NextPage';
                    }
                    else {
                        if (firstPage.classList.contains('e-focused')) {
                            firstPage.blur();
                            pagerInstance.element.focus();
                            return 'FocusOut';
                        }
                        else if (!firstPage.classList.contains('.e-disabled')) {
                            firstPage.focus();
                            return 'FirstPage';
                        }
                        return '0';
                    }
                }
                else {
                    if (mLastPage.classList.contains('e-focused') || activeElement.classList.contains('e-lastpage')) {
                        mNext.focus();
                        return 'mNextPage';
                    }
                    else if (mNext.classList.contains('e-focused') && !mPreviousPage.classList.contains('e-prevpagedisabled')) {
                        mPreviousPage.focus();
                        return 'mPreviousPage';
                    }
                    else if (mPreviousPage.classList.contains('e-focused') || activeElement.classList.contains('e-mprev')) {
                        mFirst.focus();
                        return 'mFirstPage';
                    }
                    else if (mFirst.classList.contains('e-focused')) {
                        mFirst.blur();
                        pagerInstance.element.focus();
                        return 'FocusOut';
                    }
                    else if (mNext.classList.contains('e-focused')) {
                        mNext.blur();
                        pagerInstance.element.focus();
                        return 'FocusOut';
                    }
                    return '0';
                }
            }
            else {
                return '0';
            }
        }
    };

    exports["default"] = Pager;
    exports.parentsUntil = parentsUntil;

    Object.defineProperty(exports, '__esModule', { value: true });

    return exports;

})();
