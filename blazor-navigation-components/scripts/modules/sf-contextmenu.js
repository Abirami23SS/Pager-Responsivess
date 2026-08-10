window.sfBlazor = window.sfBlazor || {};
window.sfBlazor.ContextMenu = (function () {
    'use strict';

    var MENUITEM$1 = 'e-menu-item';
    var FOCUSED$1 = 'e-focused';
    var SELECTED$1 = 'e-selected';
    var CONTAINER = 'e-menu-container';
    var MENU$1 = 'e-contextmenu';
    var SUBMENU = 'e-ul';
    var SEPARATOR = 'e-separator';
    var DISABLED = 'e-disabled';
    var HIDE = 'e-menu-hide';
    var MENUPARENT = 'e-menu-parent';
    var RTL = 'e-rtl';
    var HAMBURGER = '.e-hamburger';
    var SCROLLMENU$1 = '.e-menu-vscroll';
    var NONE$1 = 'none';
    var DOT$1 = '.';
    var ESC = 27;
    var ENTER = 13;
    var UP = 38;
    var DOWN = 40;
    var LEFT = 37;
    var RIGHT = 39;
    // eslint-disable-next-line jsdoc/require-param, valid-jsdoc
    /**
     * Keyboard action handler common for menu and context menu.
     *
     * @hidden
     */
    function keyActionHandler(container, target, keyCode, menuId) {
        if (keyCode === 9 && menuId) {
            keyCode = RIGHT;
        }
        if (keyCode === DOWN || keyCode === UP) {
            var index = void 0;
            var ul = void 0;
            var focusedLi = void 0;
            if (target.classList.contains(MENUPARENT)) {
                ul = target;
                focusedLi = ul.querySelector("".concat(DOT$1).concat(MENUITEM$1).concat(DOT$1).concat(FOCUSED$1));
                if (focusedLi) {
                    index = Array.prototype.indexOf.call(ul.children, focusedLi);
                    index = keyCode === DOWN ? (index === ul.childElementCount - 1 ? 0 : index + 1) :
                        (index === 0 ? ul.childElementCount - 1 : index - 1);
                }
                else {
                    index = 0;
                }
                index = isValidLI(ul, index, keyCode === DOWN);
            }
            else if (target.classList.contains(MENUITEM$1)) {
                ul = target.parentElement;
                focusedLi = ul.querySelector("".concat(DOT$1).concat(MENUITEM$1).concat(DOT$1).concat(FOCUSED$1));
                index = Array.prototype.indexOf.call(ul.children, focusedLi ? focusedLi : target);
                index = keyCode === DOWN ? (index === ul.childElementCount - 1 ? 0 : index + 1) : (index === 0 ?
                    ul.childElementCount - 1 : index - 1);
                index = isValidLI(ul, index, keyCode === DOWN);
            }
            if (ul && index !== -1) {
                ul.children[parseInt(index.toString(), 10)].focus();
            }
        }
        else if (((container.classList.contains(RTL) ? keyCode === RIGHT : keyCode === LEFT) || keyCode === ESC || (keyCode === RIGHT && !target.classList.contains('e-menu-caret-icon') && menuId) ||
            (keyCode === ENTER && sf.base.closest(target, DOT$1 + CONTAINER))) && (target.classList.contains(SUBMENU) ||
            (target.classList.contains(MENUITEM$1) && !(target.parentElement.classList.contains(MENU$1))))) {
            var menuContainer = void 0;
            if (menuId) {
                menuContainer = sf.base.select(menuId);
            }
            var ul = target.classList.contains(SUBMENU) ? target : target.parentElement;
            var menu = sf.base.closest(ul, SCROLLMENU$1);
            var selectedLi = void 0;
            var previousUl = menu ? menu.previousElementSibling : ul.previousElementSibling;
            if (menuContainer && (!previousUl || keyCode === ENTER)) {
                selectedLi = sf.base.select("".concat(DOT$1).concat(MENUITEM$1).concat(DOT$1).concat(SELECTED$1), menuContainer);
                menu = sf.base.select(SCROLLMENU$1, container);
                // eslint-disable-next-line @typescript-eslint/no-explicit-any
                if (menu) {
                    menuContainer.blazor__instance.destroyScroll(NONE$1);
                }
            }
            else {
                var hamburgerMenu = sf.base.closest(ul, HAMBURGER);
                if (hamburgerMenu) {
                    selectedLi = sf.base.select("".concat(DOT$1).concat(MENUITEM$1).concat(DOT$1).concat(SELECTED$1), hamburgerMenu);
                }
                else {
                    selectedLi = sf.base.select("".concat(DOT$1).concat(MENUITEM$1).concat(DOT$1).concat(SELECTED$1), previousUl);
                }
            }
            if (selectedLi) {
                selectedLi.focus();
            }
        }
    }
    //eslint-disable-next-line jsdoc/require-jsdoc
    function isValidLI(ul, index, isKeyDown, count) {
        if (count === void 0) { count = 0; }
        var cli = ul.children[parseInt(index.toString(), 10)];
        if (count === ul.childElementCount) {
            return -1;
        }
        if (cli.classList.contains(SEPARATOR) || cli.classList.contains(DISABLED) || cli.classList.contains(HIDE)) {
            index = isKeyDown ? (index === ul.childElementCount - 1 ? 0 : index + 1) : (index === 0 ? ul.childElementCount - 1 : index - 1);
            count++;
        }
        cli = ul.children[parseInt(index.toString(), 10)];
        if (cli.classList.contains(SEPARATOR) || cli.classList.contains(DISABLED) || cli.classList.contains(HIDE)) {
            index = isValidLI(ul, index, isKeyDown);
        }
        return index;
    }

    /* eslint-disable max-len */
    var TRANSPARENT = 'e-transparent';
    var MENU = 'e-menu-parent';
    var MENUITEM = 'e-menu-item';
    var FOCUSED = 'e-focused';
    var SELECTED = 'e-selected';
    var CLOSE = 'CloseMenuAsync';
    var KEYDOWN = 'keydown';
    var SCROLLMENU = '.e-menu-vscroll';
    var SCROLLNAV = '.e-scroll-nav';
    var SPACE = ' ';
    var HIDDEN = 'hidden';
    var OPENMENU = 'OpenContextMenuAsync';
    var PIXEL = 'px';
    var MOUSEDOWN = 'mousedown touchstart';
    var MOUSEOVER = 'mouseover';
    var SCROLL = 'scroll';
    var NONE = 'none';
    var HASH = '#';
    var EMPTY = '';
    var DOT = '.';
    var TARGET = 'Target';
    var FILTER = 'Filter';
    var SHOWON = 'ShowOn';
    var CARET = 'e-caret';
    /**
     * Client side scripts for Blazor context menu
     */
    var SfContextMenu = /** @class */ (function () {
        function SfContextMenu(dataId, element, target, filter, showOn, closeOn, enableScrolling, dotnetRef, animationSettings) {
            this.animation = new sf.base.Animation({});
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
            window.sfBlazor.setCompInstance(this);
            this.addContextMenuEvent();
            this.addEventListener();
        }
        SfContextMenu.prototype.addContextMenuEvent = function (add) {
            if (add === void 0) { add = true; }
            var target;
            if (this.target) {
                var targetElems = sf.base.selectAll(this.target);
                if (targetElems.length) {
                    for (var i = 0, len = targetElems.length; i < len; i++) {
                        target = targetElems[parseInt(i.toString(), 10)];
                        if (add) {
                            if (sf.base.Browser.isIos) {
                                new sf.base.Touch(target, { tapHold: this.touchHandler.bind(this) });
                            }
                            else {
                                sf.base.EventHandler.add(target, this.showOn, this.cmenuHandler, this);
                            }
                        }
                        else {
                            if (sf.base.Browser.isIos) {
                                var touchModule = sf.base.getInstance(target, sf.base.Touch);
                                if (touchModule) {
                                    touchModule.destroy();
                                }
                            }
                            else {
                                sf.base.EventHandler.remove(target, this.showOn, this.cmenuHandler);
                            }
                        }
                    }
                    if (sf.base.isNullOrUndefined(this.targetElement)) {
                        this.targetElement = target;
                    }
                    if (add) {
                        sf.base.EventHandler.add(this.targetElement, SCROLL, this.scrollHandler, this);
                        for (var _i = 0, _a = sf.popups.getScrollableParent(this.targetElement); _i < _a.length; _i++) {
                            var parent_1 = _a[_i];
                            sf.base.EventHandler.add(parent_1, SCROLL, this.scrollHandler, this);
                        }
                    }
                    else {
                        var scrollableParents = void 0;
                        if (this.targetElement.parentElement) {
                            sf.base.EventHandler.remove(this.targetElement, SCROLL, this.scrollHandler);
                            scrollableParents = sf.popups.getScrollableParent(this.targetElement);
                        }
                        else {
                            scrollableParents = sf.popups.getScrollableParent(target);
                        }
                        for (var _b = 0, scrollableParents_1 = scrollableParents; _b < scrollableParents_1.length; _b++) {
                            var parent_2 = scrollableParents_1[_b];
                            sf.base.EventHandler.remove(parent_2, SCROLL, this.scrollHandler);
                        }
                        this.targetElement = null;
                    }
                }
            }
        };
        SfContextMenu.prototype.scrollHandler = function () {
            if (sf.base.select(DOT + MENU, this.element)) {
                this.dotnetRef.invokeMethodAsync(CLOSE, 0, false, true, false);
                if (this.enableScrolling) {
                    //eslint-disable-next-line @typescript-eslint/no-explicit-any, @typescript-eslint/no-this-alias
                    var proxy_1 = this;
                    setTimeout(function () { proxy_1.destroyScroll(); }, 100);
                }
            }
        };
        SfContextMenu.prototype.vscrollHandler = function () {
            var scrollEle = this.element.querySelector('.e-menu-parent');
            if (scrollEle != null && scrollEle.classList.contains('e-transparent')) {
                scrollEle.classList.remove('e-transparent');
            }
        };
        SfContextMenu.prototype.touchHandler = function (e) {
            this.cmenuHandler(e.originalEvent);
        };
        SfContextMenu.prototype.keyDownHandler = function (e) {
            var classList = e.target.classList;
            if (classList.contains(MENUITEM) || classList.contains(MENU)) {
                e.preventDefault();
            }
            keyActionHandler(this.element, e.target, e.keyCode, this.menuId);
        };
        SfContextMenu.prototype.cmenuHandler = function (e) {
            this.cmTarget = e.target;
            if (this.filter) {
                var canOpen = false;
                var filter = this.filter.split(SPACE);
                for (var i = 0, len = filter.length; i < len; i++) {
                    if (sf.base.closest(e.target, filter[parseInt(i.toString(), 10)])) {
                        canOpen = true;
                        break;
                    }
                }
                if (!canOpen) {
                    return;
                }
            }
            e.preventDefault();
            e.stopPropagation();
            var left = e.changedTouches ? e.changedTouches[0].clientX : e.clientX;
            var top = e.changedTouches ? e.changedTouches[0].clientY : e.clientY;
            if (this.showOn === 'mouseover') {
                // eslint-disable-next-line @typescript-eslint/no-explicit-any
                var rect = this.cmTarget.getBoundingClientRect();
                if (rect.right > left && left > rect.right - 5) {
                    left = left - 10;
                }
                if (rect.bottom > top && top > rect.bottom - 5) {
                    top = top - 10;
                }
            }
            if (this.closeOn !== MOUSEDOWN) {
                sf.base.EventHandler.remove(e.target, this.showOn, this.cmenuHandler);
                if (this.closeOn === 'mouseleave') {
                    sf.base.EventHandler.add(e.target, this.closeOn, this.mouseLeaveHandler, this);
                }
                else {
                    sf.base.EventHandler.add(document, this.closeOn, this.delegateMouseDownHandler, this);
                }
            }
            this.dotnetRef.invokeMethodAsync(OPENMENU, Math.ceil(left), Math.ceil(top), this.cmTarget.id);
        };
        SfContextMenu.prototype.mouseLeaveHandler = function (e, isMouseOver) {
            var rect = this.cmTarget.getBoundingClientRect();
            var top = rect.top < e.clientY && rect.bottom - 3 > e.clientY;
            var left = rect.left < e.clientX && rect.right > e.clientX;
            if (!left || !top || isMouseOver) {
                this.dotnetRef.invokeMethodAsync(CLOSE, 0, false, true, false);
                if (this.enableScrolling) {
                    // eslint-disable-next-line @typescript-eslint/no-explicit-any, @typescript-eslint/no-this-alias
                    var proxy_2 = this;
                    setTimeout(function () { proxy_2.destroyScroll(); }, 100);
                }
                sf.base.EventHandler.remove(this.cmTarget, this.closeOn, this.mouseLeaveHandler);
                sf.base.EventHandler.add(this.cmTarget, this.showOn, this.cmenuHandler, this);
            }
        };
        SfContextMenu.prototype.clickHandler = function (isUnwire) {
            if (isUnwire) {
                sf.base.EventHandler.add(this.cmTarget, this.showOn, this.cmenuHandler, this);
                if (this.closeOn === 'mouseleave') {
                    sf.base.EventHandler.remove(this.cmTarget, this.closeOn, this.mouseLeaveHandler);
                }
                else {
                    sf.base.EventHandler.remove(document, this.closeOn, this.delegateMouseDownHandler);
                }
            }
            if (this.enableScrolling) {
                this.destroyScroll(null, true);
            }
        };
        SfContextMenu.prototype.contextMenuPosition = function (left, top, rtl, subMenu, isCollision, scrollHeight, isDevice) {
            var cmenu = this.hideMenu(true);
            if (!cmenu) {
                return;
            }
            this.subMenuOpen = false;
            this.setBlankIconStyle(cmenu, rtl);
            var cmenuOffset = cmenu.getBoundingClientRect();
            var cmenuWidth = this.getMenuWidth(cmenu, cmenuOffset.width, rtl);
            if (subMenu && isDevice) {
                cmenu.style.width = Math.ceil(cmenuWidth) + PIXEL;
                cmenu.style.visibility = EMPTY;
                return;
            }
            if (isCollision) {
                if (top + cmenuOffset.height > document.documentElement.clientHeight) {
                    var newTop = document.documentElement.clientHeight - cmenuOffset.height - 20;
                    if (newTop > document.documentElement.clientTop) {
                        top = newTop;
                    }
                }
                if (rtl) {
                    if (left < cmenuWidth) {
                        left += cmenuWidth;
                    }
                }
                else if (left + cmenuWidth > document.documentElement.clientWidth) {
                    var newLeft = document.documentElement.clientWidth - cmenuWidth - 20;
                    if (newLeft > document.documentElement.clientLeft) {
                        left = newLeft;
                    }
                }
            }
            cmenu = this.updateScroll(scrollHeight, cmenu);
            var ul = cmenu.classList.contains('e-menu-parent') ? cmenu : sf.base.select('.e-menu-parent', cmenu);
            this.element.style.top = Math.ceil(top + 1) + scrollY + PIXEL;
            this.element.style.left = Math.ceil(left + 1) + scrollX + PIXEL;
            cmenu.style.width = Math.ceil(cmenuWidth) + PIXEL;
            this.element.style.zIndex = sf.popups.getZindexPartial(this.element).toString();
            cmenu.style.visibility = EMPTY;
            ul.style.visibility = EMPTY;
            cmenu.focus();
        };
        SfContextMenu.prototype.setBlankIconStyle = function (menu, isRtl) {
            var blankIconList = [].slice.call(menu.getElementsByClassName('e-blankicon'));
            var cssProp = isRtl ? { padding: 'paddingRight', cssSelector: 'padding-right', margin: 'marginLeft' } : { padding: 'paddingLeft', cssSelector: 'padding-left', margin: 'marginRight' };
            [].slice.call(menu.querySelectorAll('.e-menu-item[style*="' + cssProp.cssSelector + '"]:not(.e-blankicon)')).forEach(function (li) {
                // eslint-disable-next-line @typescript-eslint/no-explicit-any
                li.style[cssProp.padding] = EMPTY;
            });
            if (!blankIconList.length) {
                return;
            }
            var iconLi = menu.querySelector('.e-menu-item:not(.e-blankicon):not(.e-separator)');
            var icon = iconLi && iconLi.querySelector('.e-menu-icon');
            if (!icon) {
                return;
            }
            var iconCssProps = getComputedStyle(icon);
            var iconSize = parseInt(iconCssProps.fontSize, 10);
            if (!!parseInt(iconCssProps.width, 10) && parseInt(iconCssProps.width, 10) > iconSize) {
                iconSize = parseInt(iconCssProps.width, 10);
            }
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            var size = "".concat(iconSize + parseInt(iconCssProps[cssProp.margin], 10) + parseInt(getComputedStyle(iconLi)[cssProp.padding], 10), "px");
            blankIconList.forEach(function (li) {
                // eslint-disable-next-line @typescript-eslint/no-explicit-any
                li.style[cssProp.padding] = size;
            });
        };
        SfContextMenu.prototype.getMenuWidth = function (cmenu, width, isRtl) {
            var caretIcon = cmenu.getElementsByClassName(CARET)[0];
            if (caretIcon) {
                width += parseInt(getComputedStyle(caretIcon)[isRtl ? 'marginRight' : 'marginLeft'], 10);
            }
            return width < 120 ? 120 : width;
        };
        SfContextMenu.prototype.addEventListener = function () {
            this.delegateMouseDownHandler = this.mouseDownHandler.bind(this);
            this.delegateMouseOverHandler = this.mouseOverHandler.bind(this);
            if (this.closeOn === MOUSEDOWN) {
                sf.base.EventHandler.add(document, MOUSEDOWN, this.delegateMouseDownHandler, this);
            }
            sf.base.EventHandler.add(document, MOUSEOVER, this.delegateMouseOverHandler, this);
            sf.base.EventHandler.add(this.element, KEYDOWN, this.keyDownHandler, this);
        };
        SfContextMenu.prototype.removeEventListener = function () {
            sf.base.EventHandler.remove(document, MOUSEDOWN, this.delegateMouseDownHandler);
            sf.base.EventHandler.remove(document, MOUSEOVER, this.delegateMouseOverHandler);
            sf.base.EventHandler.remove(this.element, KEYDOWN, this.keyDownHandler);
        };
        SfContextMenu.prototype.mouseDownHandler = function (e) {
            if (this.isShowItemOnClick && this.animationElement && sf.base.closest(e.target, HASH + this.element.id)) {
                sf.base.Animation.stop(this.animationElement);
            }
            var target = e.target;
            if (target.tagName === 'DIV' || target.tagName === 'SPAN') {
                if (target.classList.contains('scroll') || target.classList.contains('arrow')) {
                    return;
                }
            }
            if (!document.getElementById(this.element.id)) {
                this.removeEventListener();
                return;
            }
            var closestElem;
            if (/^\d+$/.test(this.element.id) || !this.element.id.includes('sfcontextmenu')) {
                closestElem = this.getClosest(e.target, this.element.id);
            }
            else {
                closestElem = sf.base.closest(e.target, HASH + this.element.id);
            }
            if (!closestElem && (sf.base.isNullOrUndefined(this.menuId) ||
                !sf.base.closest(e.target, this.menuId)) && sf.base.select(DOT + MENU, this.element)) {
                if (!sf.base.closest(sf.base.select(DOT + MENU, this.element), '.e-dropdown-popup')) {
                    this.dotnetRef.invokeMethodAsync(CLOSE, 0, false, true, false);
                }
                if (this.enableScrolling) {
                    // eslint-disable-next-line @typescript-eslint/no-explicit-any, @typescript-eslint/no-this-alias
                    var proxy_3 = this;
                    setTimeout(function () { proxy_3.destroyScroll(); }, 100);
                }
                if (this.closeOn !== MOUSEDOWN) {
                    sf.base.EventHandler.remove(document, this.closeOn, this.delegateMouseDownHandler);
                    sf.base.EventHandler.add(this.cmTarget, this.showOn, this.cmenuHandler, this);
                }
            }
        };
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        SfContextMenu.prototype.getClosest = function (target, id) {
            var closestElement = null;
            var currentElement = target;
            while (currentElement) {
                if (currentElement.id === id) {
                    closestElement = currentElement;
                    break;
                }
                currentElement = currentElement.parentElement;
            }
            return closestElement;
        };
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        SfContextMenu.prototype.isHavingChild = function (target) {
            if (target && target.tagName === 'LI' && target.className.indexOf('e-menu-item') < 0) {
                target = target.closest('.e-menu-item');
            }
            if (target && target.tagName === 'LI' && target.className.indexOf('e-menu-item') > -1 && target.className.indexOf('e-menu-caret-icon') > -1) {
                return true;
            }
            return false;
        };
        SfContextMenu.prototype.mouseOverHandler = function (e) {
            var target = e.target;
            if (target.tagName === 'DIV' || target.tagName === 'SPAN') {
                if (target.className.indexOf('scroll') > -1 || target.className.indexOf('arrow') > -1) {
                    return;
                }
            }
            if (!document.getElementById(this.element.id)) {
                this.removeEventListener();
                return;
            }
            var menus = [].slice.call(sf.base.selectAll(DOT + MENU, this.element));
            if (!menus.length) {
                return;
            }
            var scrollNav = sf.base.closest(target, SCROLLNAV);
            if (this.enableScrolling && !this.isHavingChild(target)) {
                // eslint-disable-next-line @typescript-eslint/no-explicit-any, @typescript-eslint/no-this-alias
                var proxy_4 = this;
                setTimeout(function () {
                    var subMenus = [].slice.call(sf.base.selectAll(SCROLLMENU, this.element));
                    for (var i = 0; i < subMenus.length; i++) {
                        if (target && target.tagName === 'LI' && target.className.indexOf('e-menu-item') < 0) {
                            target = target.closest('.e-menu-item');
                        }
                        if (target && target.parentElement !== subMenus[parseInt(i.toString(), 10)].querySelectorAll('.e-menu-parent')[0] && i === subMenus.length - 1) {
                            proxy_4.destroyScroll(subMenus[parseInt(i.toString(), 10)]);
                        }
                    }
                }, 100);
            }
            if (this.subMenuOpen && (menus.length > 1 || (!sf.base.isNullOrUndefined(this.menuId) && !scrollNav))) {
                var closestEle = void 0;
                if (/^\d+$/.test(this.element.id) || !this.element.id.includes('sfcontextmenu')) {
                    closestEle = this.getClosest(target, this.element.id);
                }
                else {
                    closestEle = sf.base.closest(target, HASH + this.element.id);
                }
                if ((!closestEle && (sf.base.isNullOrUndefined(this.menuId) || !sf.base.closest(target, this.menuId))) ||
                    scrollNav) {
                    var index_1 = 1;
                    if (!sf.base.isNullOrUndefined(this.menuId)) {
                        index_1 = 0;
                        if (scrollNav) {
                            index_1 = menus.indexOf(sf.base.select(DOT + MENU, scrollNav.parentElement)) + 1;
                            if (index_1 === menus.length) {
                                return;
                            }
                        }
                    }
                    this.dotnetRef.invokeMethodAsync(CLOSE, index_1, false, true, false);
                    if (this.enableScrolling) {
                        // eslint-disable-next-line @typescript-eslint/no-explicit-any, @typescript-eslint/no-this-alias
                        var proxy_5 = this;
                        setTimeout(function () {
                            for (var i = index_1; i < menus.length; i++) {
                                proxy_5.destroyScroll(menus[parseInt(i.toString(), 10)]);
                            }
                        }, 100);
                    }
                    if (this.closeOn === 'mouseleave') {
                        this.mouseLeaveHandler(e);
                        sf.base.EventHandler.remove(this.menuTarget, this.closeOn, this.menuMouseLeave);
                    }
                    if (!sf.base.isNullOrUndefined(this.menuId) && !sf.base.closest(target, SCROLLNAV)) {
                        this.destroyMenuScroll(null);
                    }
                }
                var closestElem = void 0;
                if (/^\d+$/.test(this.element.id) || !this.element.id.includes('sfcontextmenu')) {
                    closestElem = this.getClosest(target, this.element.id);
                }
                else {
                    closestElem = sf.base.closest(target, HASH + this.element.id);
                }
                if (!sf.base.isNullOrUndefined(this.menuId) && (closestElem || sf.base.closest(target, this.menuId)) &&
                    sf.base.closest(target, DOT + MENUITEM) && !sf.base.closest(target, DOT + SELECTED)) {
                    this.destroyMenuScroll(sf.base.closest(target, DOT + MENU));
                }
            }
            else if (this.closeOn === 'mouseleave' && target.classList.contains('e-menu-item')) {
                this.menuTarget = target.parentElement;
                sf.base.EventHandler.add(this.menuTarget, this.closeOn, this.menuMouseLeave, this);
            }
            if (!this.openAsMenu) {
                var activeEle = document.activeElement;
                var closestElem = void 0;
                if (/^\d+$/.test(this.element.id) || !this.element.id.includes('sfcontextmenu')) {
                    closestElem = this.getClosest(activeEle, this.element.id);
                }
                else {
                    closestElem = sf.base.closest(activeEle, "".concat(HASH).concat(this.element.id));
                }
                if (!closestElem && menus.length && activeEle.tagName === 'BODY') {
                    var lastChild = this.getLastMenu();
                    if (lastChild) {
                        lastChild.focus();
                    }
                }
            }
        };
        SfContextMenu.prototype.menuMouseLeave = function (e) {
            var rect = this.menuTarget.getBoundingClientRect();
            var top = rect.top < e.clientY && rect.bottom > e.clientY;
            var left = rect.left < e.clientX && rect.right > e.clientX;
            if (!left || !top) {
                this.mouseLeaveHandler(e, true);
                sf.base.EventHandler.remove(this.menuTarget, this.closeOn, this.menuMouseLeave);
            }
        };
        SfContextMenu.prototype.destroyMenuScroll = function (menu) {
            if (!sf.base.select(SCROLLMENU, this.element)) {
                return;
            }
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            var instance = window.sfBlazor.getCompInstance(this.dataId);
            if (!sf.base.isNullOrUndefined(instance)) {
                instance.destroyScroll(NONE, menu);
            }
        };
        SfContextMenu.prototype.hideMenu = function (first) {
            var cMenu;
            if (first) {
                cMenu = sf.base.select(DOT + MENU, this.element);
                if (!cMenu || sf.base.isNullOrUndefined(this.element.parentElement)) {
                    return null;
                }
                if ((this.element.parentElement !== document.body && !this.element.parentElement.classList.contains('e-dropDown-button'))) {
                    document.body.appendChild(this.element);
                }
            }
            else {
                var menus = sf.base.selectAll(DOT + MENU, this.element);
                if (menus.length < 2) {
                    return null;
                }
                cMenu = menus[menus.length - 1];
            }
            cMenu.style.width = EMPTY;
            cMenu.style.visibility = HIDDEN;
            cMenu.classList.remove(TRANSPARENT);
            return cMenu;
        };
        SfContextMenu.prototype.subMenuPosition = function (cmenu, isRtl, showOnClick, isNull, scrollHeight, isContextMenu, enterKey) {
            if (!cmenu) {
                return;
            }
            var menus = sf.base.selectAll(DOT + MENU, this.element);
            var parentLi = menus[menus.length - 2].querySelector(".".concat(MENUITEM, ".").concat(SELECTED));
            var parentOffset = parentLi.getBoundingClientRect();
            var containerOffset = this.element.getBoundingClientRect();
            var menu = cmenu.classList.contains(MENU) ? cmenu : sf.base.select(DOT + MENU, cmenu);
            this.setBlankIconStyle(menu, isRtl);
            var curUlOffset = menu.getBoundingClientRect();
            var cmenuWidth = this.getMenuWidth(menu, curUlOffset.width, isRtl);
            var left;
            var borderLeft;
            if (isRtl) {
                borderLeft = parseInt(getComputedStyle(menu).borderWidth, 10);
                left = parentOffset.left - cmenuWidth - containerOffset.left;
            }
            else if (this.closeOn === 'mouseleave') {
                left = parentOffset.right - containerOffset.left - 10;
            }
            else {
                left = parentOffset.right - containerOffset.left;
            }
            var top = parentOffset.top - containerOffset.top;
            if (isRtl) {
                if (parentOffset.left - borderLeft - cmenuWidth < document.documentElement.clientLeft) {
                    if (parentOffset.right + cmenuWidth < document.documentElement.clientWidth) {
                        left = parentOffset.right - containerOffset.left;
                    }
                }
            }
            else if (parentOffset.right + cmenuWidth > document.documentElement.clientWidth) {
                var newLeft = parentOffset.left - cmenuWidth;
                if (newLeft > document.documentElement.clientLeft) {
                    left = newLeft - containerOffset.left;
                    if (this.closeOn === 'mouseleave') {
                        left = newLeft - containerOffset.left + 10;
                    }
                }
            }
            var height = scrollHeight || curUlOffset.height;
            if (parentOffset.top + height > document.documentElement.clientHeight) {
                var newTop = document.documentElement.clientHeight - height - 20;
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
            var focusedLi = menu.querySelector("".concat(DOT).concat(MENUITEM).concat(DOT).concat(FOCUSED));
            if (focusedLi) {
                focusedLi.focus();
            }
            else if (enterKey) {
                menu.focus();
            }
            if (isNull) {
                this.openAsMenu = true;
            }
        };
        SfContextMenu.prototype.updateScroll = function (scrollHeight, menu) {
            if (this.enableScrolling) {
                this.destroyScroll(menu);
                if (scrollHeight > 0 && menu) {
                    menu = sf.navigations.addScrolling(sf.base.createElement, this.element, menu, 'vscroll', false, scrollHeight);
                }
                var scrollElement = this.element.querySelector('.e-vscroll-bar');
                if (scrollElement) {
                    sf.base.EventHandler.add(scrollElement, 'scroll', this.vscrollHandler, this);
                }
            }
            return menu;
        };
        SfContextMenu.prototype.destroyScroll = function (curMenu, isParent) {
            var scrollElements = sf.base.selectAll('.e-menu-vscroll', this.element);
            scrollElements.forEach(function (element) {
                if ((isParent && !element.querySelector('ul')) || !isParent) {
                    sf.navigations.destroyScroll(sf.base.getInstance(element, sf.navigations.VScroll), element, curMenu);
                }
            });
        };
        SfContextMenu.prototype.getLastMenu = function () {
            var menus = sf.base.selectAll(DOT + MENU, this.element);
            return menus.length ? menus[menus.length - 1] : null;
        };
        SfContextMenu.prototype.onPropertyChanged = function (key, result) {
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
        };
        SfContextMenu.prototype.destroy = function (refElement) {
            this.removeEventListener();
            this.addContextMenuEvent(false);
            if (refElement && refElement.parentElement && refElement.previousElementSibling !== this.element) {
                refElement.parentElement.insertBefore(this.element, refElement);
            }
            if ((!refElement || !refElement.parentElement) && this.element.parentElement && this.element.parentElement === document.body) {
                document.body.removeChild(this.element);
            }
        };
        SfContextMenu.prototype.updateProperty = function (showItemOnClick, menu) {
            if (menu) {
                this.menuId = HASH + menu.id;
            }
            this.subMenuOpen = !showItemOnClick;
        };
        SfContextMenu.prototype.toggleContextMenuAnimation = function (ul, animationSettings) {
            if (animationSettings.effect !== 'None') {
                this.animationElement = ul;
                this.animation.animate(ul, {
                    name: animationSettings.effect,
                    duration: animationSettings.duration,
                    timingFunction: animationSettings.easing,
                    begin: function (options) {
                        if (options.element && options.element.parentElement) {
                            options.element.parentElement.style.height = options.element.offsetHeight + 'px';
                        }
                    },
                    end: function (options) {
                        if (options.element && options.element.parentElement) {
                            options.element.parentElement.style.height = '';
                        }
                    }
                });
            }
        };
        return SfContextMenu;
    }());
    var ContextMenu = { initialize: function (dataId, element, target, filter, showOn, closeOn, enableScrolling, dotnetRef, animationSettings) {
            if (!sf.base.isNullOrUndefined(element)) {
                new SfContextMenu(dataId, element, target, filter, showOn, closeOn, enableScrolling, dotnetRef, animationSettings);
            }
        }, contextMenuPosition: function (dataId, left, top, isRtl, subMenu, isCollision, scrollHeight, isDevice) {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            var instance = window.sfBlazor.getCompInstance(dataId);
            if (!sf.base.isNullOrUndefined(instance)) {
                instance.contextMenuPosition(left, top, isRtl, subMenu, isCollision, scrollHeight, isDevice);
                var menu = sf.base.select(DOT + 'e-contextmenu' + DOT + 'e-menu-parent', instance.element);
                if (instance.animationSettings != null && !sf.base.isNullOrUndefined(menu)) {
                    // eslint-disable-next-line no-self-assign
                    instance.animationSettings = instance.animationSettings;
                    if (instance.enableScrolling && menu && sf.base.closest(menu, '.e-menu-vscroll')) {
                        menu = sf.base.closest(menu, '.e-menu-vscroll');
                    }
                    instance.toggleContextMenuAnimation(menu, instance.animationSettings);
                }
            }
        }, subMenuPosition: function (dataId, isRtl, showOnClick, isNull, scrollHeight) {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            var instance = window.sfBlazor.getCompInstance(dataId);
            if (!sf.base.isNullOrUndefined(instance)) {
                if (instance.animationElement && instance.animationSettings) {
                    sf.base.Animation.stop(instance.animationElement);
                }
                instance.isShowItemOnClick = showOnClick;
                var cmenu = instance.hideMenu();
                instance.subMenuPosition(cmenu, isRtl, showOnClick, isNull, scrollHeight, true);
                if (instance.animationSettings != null) {
                    var menu = cmenu;
                    if (instance.enableScrolling && cmenu && sf.base.closest(cmenu, '.e-menu-vscroll')) {
                        menu = sf.base.closest(cmenu, '.e-menu-vscroll');
                    }
                    instance.toggleContextMenuAnimation(menu, instance.animationSettings);
                }
            }
        }, toggleAnimation: function (dataId) {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            var instance = window.sfBlazor.getCompInstance(dataId);
            if (instance && instance.animationSettings != null && instance.animationElement && !sf.base.isNullOrUndefined(instance.toggleContextMenuAnimation)) {
                instance.toggleContextMenuAnimation(instance.animationElement, instance.animationSettings);
            }
        }, onPropertyChanged: function (dataId, key, result) {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            var instance = window.sfBlazor.getCompInstance(dataId);
            if (!sf.base.isNullOrUndefined(instance)) {
                instance.onPropertyChanged(key, result);
            }
        }, click: function (dataId, isUnwire) {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            var instance = window.sfBlazor.getCompInstance(dataId);
            if (!sf.base.isNullOrUndefined(instance)) {
                instance.clickHandler(isUnwire);
            }
        }, destroy: function (dataId, refElement) {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            var instance = window.sfBlazor.getCompInstance(dataId);
            if (!sf.base.isNullOrUndefined(instance)) {
                instance.destroy(refElement);
            }
        }, destroyScrollElement: function (dataId) {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            var instance = window.sfBlazor.getCompInstance(dataId);
            if (!sf.base.isNullOrUndefined(instance)) {
                instance.destroyScroll();
            }
        }
    };

    return ContextMenu;

})();
