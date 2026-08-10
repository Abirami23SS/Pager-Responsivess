window.sfBlazor = window.sfBlazor || {};
window.sfBlazor.Sidebar = (function () {
    'use strict';

    var LEFT = 'Left';
    var RIGHT = 'Right';
    var PUSH = 'Push';
    var OVER = 'Over';
    var SLIDE = 'Slide';
    var AUTO = 'Auto';
    var CLOSE = 'e-close';
    var ROOT = 'e-sidebar';
    var CONTROL = 'e-control';
    var CONTEXT = 'e-sidebar-context';
    var DEFAULTBACKDROP = 'e-sidebar-overlay';
    var SIDEBARABSOLUTE = 'e-sidebar-absolute';
    var MAINCONTENTANIMATION = 'e-content-animation';
    var SfSidebar = /** @class */ (function () {
        function SfSidebar(dataId, element, dotnetRef, property) {
            this.enableGestures = true;
            this.isOpen = false;
            this.closeOnDocumentClick = false;
            this.isPositionChange = false;
            this.dockSize = 'auto';
            this.width = 'auto';
            this.isSwipChange = false;
            this.element = element;
            this.dataId = dataId;
            window.sfBlazor.setCompInstance(this);
            this.dotnetRef = dotnetRef;
            this.resetProperty(property);
        }
        SfSidebar.prototype.initialize = function () {
            this.setTarget();
            this.addClass();
            this.setType();
            this.setCloseOnDocumentClick();
            this.setMediaQuery();
            if (sf.base.Browser.isDevice) {
                this.windowWidth = window.innerWidth;
            }
            this.wireEvents();
        };
        SfSidebar.prototype.addClass = function () {
            var mainElement = document.querySelector('.e-main-content');
            if (!sf.base.isNullOrUndefined(mainElement || this.targetElement)) {
                sf.base.addClass([mainElement || this.targetElement], [MAINCONTENTANIMATION]);
            }
        };
        SfSidebar.prototype.setTarget = function () {
            this.targetElement = this.element.nextElementSibling;
            if (typeof (this.target) === 'string') {
                this.target = document.querySelector(this.target);
            }
            if (this.target) {
                this.target.insertBefore(this.element, this.target.children[0]);
                sf.base.addClass([this.element], SIDEBARABSOLUTE);
                sf.base.addClass([this.target], CONTEXT);
                this.targetElement = this.getTargetElement();
            }
        };
        SfSidebar.prototype.getTargetElement = function () {
            var siblingElement = this.element.nextElementSibling;
            while (!sf.base.isNullOrUndefined(siblingElement)) {
                if (!siblingElement.classList.contains(ROOT)) {
                    break;
                }
                siblingElement = siblingElement.nextElementSibling;
            }
            return siblingElement;
        };
        SfSidebar.prototype.hide = function () {
            var _this = this;
            var sibling = document.querySelector('.e-main-content') || this.targetElement;
            if (!this.enableDock && sibling) {
                sibling.style.transform = sibling.classList.contains('e-sidebar') ? '' : 'translateX(' + 0 + 'px)';
                sibling.style[this.position === LEFT ? 'marginLeft' : 'marginRight'] = '0px';
            }
            this.destroyBackDrop();
            this.isOpen = false;
            this.sidebarOpened = false;
            if (this.enableDock) {
                setTimeout(function () { return _this.sidebarTimeout(); }, 50);
            }
            sf.base.EventHandler.add(this.element, 'transitionend', this.transitionEnd, this);
        };
        SfSidebar.prototype.show = function (isServercall) {
            var _this = this;
            if (isServercall) {
                setTimeout(function () { return _this.setType(); }, 50);
            }
            this.isOpen = true;
            this.sidebarOpened = true;
            sf.base.EventHandler.add(this.element, 'transitionend', this.transitionEnd, this);
        };
        SfSidebar.prototype.transitionEnd = function (value) {
            if (sf.base.isNullOrUndefined(this.dotnetRef)) {
                return;
            }
            if (this.enableDock && !this.isOpen) {
                var dimension = this.position === LEFT ? '-100' : '100';
                var transform = this.position === LEFT ? this.setDimension(this.dockSize) : '-' + this.setDimension(this.dockSize);
                this.element.style.transform = "translateX(".concat(dimension, "%) translateX(").concat(transform, ")");
            }
            this.dotnetRef.invokeMethodAsync('SetDock');
            if (!sf.base.isNullOrUndefined(value)) {
                this.dotnetRef.invokeMethodAsync('TriggerChange', this.isOpen, value);
            }
            sf.base.EventHandler.remove(this.element, 'transitionend', this.transitionEnd);
        };
        SfSidebar.prototype.createBackDrop = function (property) {
            var sidebarTarget = this.target;
            this.backDropApplied = this.showBackdrop;
            this.resetProperty(property);
            if (this.showBackdrop && this.sidebarOpened) {
                if (this.backDropApplied) {
                    this.destroyBackDrop();
                }
                this.modal = sf.base.createElement('div');
                this.modal.className = DEFAULTBACKDROP;
                this.modal.style.display = 'block';
                if (this.target || sidebarTarget || !this.backDropApplied) {
                    var sibling = document.querySelector('.e-main-content') || this.targetElement;
                    sibling.appendChild(this.modal);
                }
                else {
                    document.body.appendChild(this.modal);
                }
            }
            else {
                this.destroyBackDrop();
            }
        };
        SfSidebar.prototype.destroyBackDrop = function () {
            if (!sf.base.isNullOrUndefined(this.modal)) {
                this.modal.style.display = 'none';
                this.modal.outerHTML = '';
                this.modal = null;
            }
        };
        SfSidebar.prototype.enableGestureHandler = function (args) {
            var originalEvent = args.originalEvent;
            if (!originalEvent) {
                return; // Exit early if originalEvent is not available
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
            }
            else if (this.isOpen && (this.position === LEFT && args.swipeDirection === LEFT) || (this.position === RIGHT &&
                args.swipeDirection === RIGHT)) {
                this.eventArgs = {
                    left: this.getXYValue(originalEvent, 'X'),
                    top: this.getXYValue(originalEvent, 'Y')
                };
                this.dotnetRef.invokeMethodAsync('TriggerHide', this.eventArgs);
                this.hide();
                this.isSwipChange = false;
            }
        };
        SfSidebar.prototype.resize = function () {
            this.setMediaQuery();
            if (sf.base.Browser.isDevice) {
                this.windowWidth = window.innerWidth;
            }
        };
        SfSidebar.prototype.setEnableGestures = function (property) {
            this.resetProperty(property);
            if (this.enableGestures) {
                this.mainContentElement = new sf.base.Touch(document.body, { swipe: this.enableGestureHandler.bind(this) });
                this.sidebarElement = new sf.base.Touch(this.element, { swipe: this.enableGestureHandler.bind(this) });
            }
            else if (this.mainContentElement && this.sidebarElement) {
                this.mainContentElement.destroy();
                this.sidebarElement.destroy();
            }
        };
        SfSidebar.prototype.wireEvents = function () {
            this.setEnableGestures();
            window.addEventListener('resize', this.resize.bind(this));
        };
        SfSidebar.prototype.unWireEvents = function () {
            window.removeEventListener('resize', this.resize.bind(this));
            sf.base.EventHandler.remove(document, 'mousedown touchstart', this.documentclickHandler);
            if (this.mainContentElement) {
                this.mainContentElement.destroy();
            }
            if (this.sidebarElement) {
                this.sidebarElement.destroy();
            }
        };
        SfSidebar.prototype.documentclickHandler = function (e) {
            if (sf.base.isNullOrUndefined(this.dotnetRef)) {
                return;
            }
            if (!(sf.base.closest(e.target, '.' + CONTROL + '' + '.' + ROOT))) {
                this.eventArgs = {
                    left: this.getXYValue(e, 'X'),
                    top: this.getXYValue(e, 'Y')
                };
                if (this.closeOnDocumentClick) {
                    this.dotnetRef.invokeMethodAsync('TriggerHide', this.eventArgs);
                }
            }
        };
        SfSidebar.prototype.setCloseOnDocumentClick = function (property) {
            this.resetProperty(property);
            if (this.closeOnDocumentClick) {
                sf.base.EventHandler.add(document, 'mousedown touchstart', this.documentclickHandler, this);
            }
            else if (property) {
                sf.base.EventHandler.remove(document, 'mousedown touchstart', this.documentclickHandler);
            }
        };
        SfSidebar.prototype.setMediaQuery = function () {
            if (this.mediaQuery && this.windowWidth !== window.innerWidth) {
                if (window.matchMedia(this.mediaQuery).matches) {
                    this.dotnetRef.invokeMethodAsync('TriggerShow', null);
                }
                else if (this.isOpen) {
                    this.dotnetRef.invokeMethodAsync('TriggerHide', null);
                }
            }
        };
        SfSidebar.prototype.setDimension = function (width) {
            if (typeof width === 'number') {
                width = sf.base.formatUnit(width);
            }
            else if (typeof width === 'string') {
                width = (width.match(/px|%|em/)) ? width : sf.base.formatUnit(width);
            }
            else {
                width = '100%';
            }
            return width;
        };
        SfSidebar.prototype.sidebarTimeout = function () {
            var sibling = document.querySelector('.e-main-content') || this.targetElement;
            var leftMargin = this.isOpen ? this.setDimension(this.width) : this.setDimension(this.dockSize);
            var rightMargin = this.setDimension(this.element.getBoundingClientRect().width);
            if (sibling) {
                if (this.isOpen) {
                    this.positionStyles(this.width, sibling, rightMargin, leftMargin);
                }
                else if (this.element.classList.contains(CLOSE)) {
                    this.positionStyles(this.dockSize, sibling, rightMargin, leftMargin);
                }
            }
        };
        SfSidebar.prototype.positionStyles = function (size, sibling, rightMargin, leftMargin) {
            if (this.position === LEFT) {
                sibling.style.marginLeft = size === 'auto' ? rightMargin : leftMargin;
            }
            else {
                sibling.style.marginRight = size === 'auto' ? rightMargin : leftMargin;
            }
        };
        SfSidebar.prototype.siblingStyle = function (sibling, margin) {
            sibling.style[this.position === LEFT ? 'marginLeft' : 'marginRight'] = margin;
        };
        SfSidebar.prototype.resetProperty = function (property) {
            if (!sf.base.isNullOrUndefined(property)) {
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
        };
        SfSidebar.prototype.getXYValue = function (e, direction) {
            if (sf.base.isNullOrUndefined(e)) {
                return 0;
            }
            var touchList = e.changedTouches;
            var value;
            if (direction === 'X') {
                value = touchList ? touchList[0].clientX : e.clientX;
            }
            else {
                value = touchList ? touchList[0].clientY : e.clientY;
            }
            if (!value && e.type === 'focus' && e.target) {
                var rect = e.target.getBoundingClientRect();
                value = rect ? (direction === 'X' ? rect.left : rect.top) : null;
            }
            return Math.ceil(value);
        };
        SfSidebar.prototype.setType = function (property) {
            if (sf.base.closest(this.element, '.e-sidebarcontainer')) {
                return;
            }
            this.resetProperty(property);
            var elementWidth = this.width !== 'auto' && this.enableDock ? this.setDimension(this.width) : this.element.getBoundingClientRect().width;
            elementWidth = this.enableDock && !this.isOpen ? this.dockSize : !this.enableDock && !this.isOpen ? 0 : elementWidth;
            var sibling = document.querySelector('.e-main-content') || this.targetElement;
            if (sibling) {
                if (this.isPositionChange) {
                    sibling.style[this.position === LEFT ? 'marginRight' : 'marginLeft'] = '0px';
                }
                sibling.style.transform = sibling.classList.contains('e-sidebar') ? '' : 'translateX(' + 0 + 'px)';
                if ((!sf.base.Browser.isDevice && this.type !== AUTO) && this.type !== OVER) {
                    sibling.style[this.position === LEFT ? 'marginLeft' : 'marginRight'] = '0px';
                }
                this.isPositionChange = false;
                var margin = typeof (elementWidth) === 'string' ? elementWidth : elementWidth + 'px';
                var translate = this.position === LEFT ? elementWidth : -(elementWidth);
                var value = sibling && (this.enableDock || this.isOpen || this.isSwipChange);
                switch (this.type) {
                    case PUSH:
                        if (value) {
                            this.siblingStyle(sibling, margin);
                        }
                        break;
                    case SLIDE:
                        if (value) {
                            sibling.style.transform = 'translateX(' + translate + 'px)';
                            this.siblingStyle(sibling, margin);
                        }
                        break;
                    case OVER:
                        if (this.element.classList.contains(CLOSE)) {
                            if (this.enableDock) {
                                this.siblingStyle(sibling, this.dockSize);
                            }
                            else {
                                this.siblingStyle(sibling, '0px');
                            }
                        }
                        break;
                    case AUTO:
                        if (sf.base.Browser.isDevice) {
                            if ((this.enableDock) && !this.isOpen) {
                                this.siblingStyle(sibling, margin);
                            }
                        }
                        else if ((this.enableDock || this.isOpen || this.isSwipChange)) {
                            this.siblingStyle(sibling, margin);
                        }
                        else if (!this.enableDock && !this.isOpen) {
                            this.siblingStyle(sibling, margin);
                        }
                        this.isSwipChange = false;
                }
            }
        };
        SfSidebar.prototype.destroy = function () {
            this.destroyBackDrop();
            this.element.style.width = this.element.style.zIndex = this.element.style.transform = '';
            this.windowWidth = null;
            this.mediaQuery = null;
            var sibling = document.querySelector('.e-main-content') || this.targetElement;
            if (!sf.base.isNullOrUndefined(sibling)) {
                sibling.style.margin = sibling.style.transform = '';
            }
            this.unWireEvents();
            this.targetElement = null;
            this.mainContentElement = null;
            this.sidebarElement = null;
            this.element = null;
            this.dotnetRef = null;
        };
        return SfSidebar;
    }());
    var Sidebar = {
        initialize: function (dataId, element, dotnetRef, property) {
            new SfSidebar(dataId, element, dotnetRef, property);
            var instance = window.sfBlazor.getCompInstance(dataId);
            if (element && !sf.base.isNullOrUndefined(instance)) {
                instance.initialize();
                sf.base.removeClass([element], 'e-hidden');
            }
            return !sf.base.Browser.isDevice && (sf.base.isNullOrUndefined(property.MediaQuery) || window.matchMedia(property.MediaQuery).matches) ?
                true : (sf.base.Browser.isDevice && property.IsOpen) ? true : false;
        },
        setType: function (dataId, property) {
            var instance = window.sfBlazor.getCompInstance(dataId);
            if (!sf.base.isNullOrUndefined(instance)) {
                instance.setType(property);
            }
        },
        hide: function (dataId, property) {
            var instance = window.sfBlazor.getCompInstance(dataId);
            if (!sf.base.isNullOrUndefined(instance)) {
                instance.setType(property);
                instance.hide();
            }
        },
        show: function (dataId, property, isServerCall) {
            var instance = window.sfBlazor.getCompInstance(dataId);
            if (!sf.base.isNullOrUndefined(instance)) {
                instance.setType(property);
                instance.show(isServerCall);
                instance.createBackDrop(property);
            }
        },
        onPropertyChange: function (dataId, property) {
            var instance = window.sfBlazor.getCompInstance(dataId);
            if (!sf.base.isNullOrUndefined(instance)) {
                if (property.CloseOnDocumentClick !== undefined) {
                    instance.setCloseOnDocumentClick(property);
                }
                if (property.ShowBackdrop !== undefined) {
                    instance.createBackDrop(property);
                }
                if (property.Width !== undefined) {
                    instance.setType(property);
                }
            }
        },
        destroy: function (dataId) {
            var instance = window.sfBlazor.getCompInstance(dataId);
            if (!sf.base.isNullOrUndefined(instance)) {
                instance.destroy();
            }
        }
    };

    return Sidebar;

})();
