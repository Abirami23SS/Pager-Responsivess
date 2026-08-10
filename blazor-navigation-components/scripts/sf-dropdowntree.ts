import { BlazorDotnetObject, EventHandler, isNullOrUndefined, closest, addClass, Browser, removeClass, attributes, formatUnit } from '@syncfusion/ej2-base';
import { matches, KeyboardEvents, KeyboardEventArgs, select, MouseEventArgs, detach, selectAll, createElement, remove } from '@syncfusion/ej2-base';
import { Popup, PopupModel } from '@syncfusion/ej2-popups';

const PARENTITEM: string = 'e-list-parent';
const INPUTFOCUS: string = 'e-input-focus';
const ICONANIMATION: string = 'e-icon-anim';
const DDTHIDEICON: string = 'e-ddt-icon-hide';
const DROPDOWN: string = 'e-dropdown';
const NODATA: string = 'e-no-data';
const HIDEICON: string = 'e-icon-hide';
const SHOW_CLEAR: string = 'e-show-clear';
const OVERFLOW_VIEW: string = '.e-overflow';
const SHOW_TEXT: string = 'e-show-text';
const CHIP_WRAPPER: string = '.e-chips-wrapper';
const SHOW_CHIP: string = 'e-show-chip';
const CHIP_INPUT: string = 'e-chip-input';
const FOOTER: string = 'e-ddt-footer';
const HEADER: string = 'e-ddt-header';
const CHIP_CLOSE: string = 'e-chips-close';
const CHECKALLPARENT: string = 'e-selectall-parent';
const CHECKBOXWRAP: string = 'e-checkbox-wrapper';
const CHECKBOXFRAME: string = 'e-frame';
const CHECKBOXLABEL: string = 'e-label';
const ALLTEXT: string = 'e-all-text';
const FILTERWRAP: string = 'e-filter-wrap';

class SfDropDownTree {
    private element: HTMLElement;
    private inputWrapper: HTMLElement;
    private overFlowWrapper: HTMLElement;
    private chipWrapper: HTMLElement;
    private checkBoxElement: HTMLElement;
    private checkAllParent: HTMLElement;
    private filterContainer: HTMLElement;
    private dotNetRef: BlazorDotnetObject;
    private options: IDropDownTreeOptions;
    private dataId: string;
    private sfBlazor: any = (window as any).sfBlazor;
    private keyConfigs: { [key: string]: string };
    private popupObj: Popup;
    private popupEle: HTMLElement;
    private overAllClear: HTMLElement;
    private inputFocus: boolean;
    private isPopupOpen: boolean;
    private popupDiv: HTMLElement;
    private keyboardModule: KeyboardEvents;
    private filterInputEle: HTMLElement;
    private isClearIconClick: boolean;
    private uniqueID: string;
    private valueTemplateContainer: HTMLElement;

    constructor(dataId: string, containerElement: HTMLElement, dotnetRef: BlazorDotnetObject, options: IDropDownTreeOptions,
                uniqueID: string) {
        this.dataId = dataId;
        this.inputWrapper = containerElement;
        this.element = select('.e-dropdowntree', containerElement);
        this.options = options;
        this.sfBlazor.setCompInstance(this);
        this.dotNetRef = dotnetRef;
        this.uniqueID = uniqueID;
    }

    public initialize(): void {
        this.keyConfigs = {
            escape: 'escape',
            altUp: 'alt+uparrow',
            altDown: 'alt+downarrow',
            tab: 'tab',
            shiftTab: 'shift+tab',
            space: 'space',
            moveDown: 'downarrow'
        };
        this.checkInputValueAvailable();
        if (this.options.showClearButton) {
            this.overAllClear = select('.e-clear-icon', this.inputWrapper);
        }
        this.createOverFlowWrapper();
        this.setDisable();
        this.wireEvents();
    }

    private checkInputValueAvailable(): void {
        if (!isNullOrUndefined(this.inputWrapper.querySelector('.e-input-value'))) {
            this.valueTemplateContainer = this.inputWrapper.querySelector('.e-input-value');
        }
    }

    private createOverFlowWrapper(): void {
        if (this.options.allowMultiSelection || this.options.showCheckBox) {
            if (this.options.mode !== 'Delimiter') {
                this.createChip();
            }
            if (!this.options.textWrap && this.options.mode !== 'Custom') {
                this.overFlowWrapper = select(OVERFLOW_VIEW + ':not(.e-input-value)', this.inputWrapper);
                this.inputWrapper.insertBefore(this.overFlowWrapper, this.element);
                if (this.options.mode !== 'Box') {
                    addClass([this.overFlowWrapper], SHOW_TEXT);
                }
            }
        }
    }

    private renderPopup(): void {
        addClass([this.inputWrapper], [ICONANIMATION]);
        this.popupEle = select('#' + this.element.id + '_options_' + this.uniqueID);
        this.keyboardModule = new KeyboardEvents(
            this.popupEle,
            {
                keyAction: this.popupKeyActionHandler.bind(this),
                keyConfigs: this.keyConfigs,
                eventName: 'keydown'
            }
        );
        document.body.appendChild(this.popupEle);
        this.createPopup(this.popupEle);
        if ((this.options.allowMultiSelection || this.options.showCheckBox) && this.options.mode !== 'Delimiter') {
            this.createChip();
        }
        removeClass([this.popupEle], DDTHIDEICON);
        if (this.options.allowFiltering) { this.filterContainer = select('.' + FILTERWRAP, this.popupEle); }
        if (this.options.showCheckBox && this.options.showSelectAll && (!this.popupDiv.classList.contains(NODATA))) {
            this.checkAllParent = select('.' + CHECKALLPARENT);
            this.checkBoxElement = select('.' + CHECKBOXWRAP, this.checkAllParent);
        }
        attributes(this.element, { 'aria-expanded': 'true' });
        this.popupObj.show(null, (this.options.zIndex === 1000) ? this.element : null);
        removeClass([this.popupEle], DDTHIDEICON);
        this.updatePopupHeight();
        this.popupObj.refreshPosition();
        const treeItems: NodeList = this.popupDiv.querySelectorAll('li');
        if (!(this.options.showCheckBox && this.options.showSelectAll) && (!this.popupDiv.classList.contains(NODATA)
            && treeItems.length > 0)) {
            const focusedElement: HTMLElement = this.popupDiv.querySelector('li');
            focusedElement.focus();
        }
        if (this.options.allowFiltering) {
            removeClass([this.inputWrapper], [INPUTFOCUS]);
            this.filterInputEle = select('#' + this.element.id + '_filter_' + this.uniqueID, this.filterContainer);
            this.filterInputEle.focus();
        }
    }

    private removeChip(e: MouseEvent): void {
        const value: string = (e.target as HTMLElement).parentElement ? (e.target as HTMLElement).parentElement.getAttribute('data-value') : '';
        this.dotNetRef.invokeMethodAsync('RemoveChip', value);
    }

    private updateView(): void {
        if (this.options.mode === 'Custom' || this.inputFocus) {
            return;
        }
        if (this.options.mode !== 'Box') {
            addClass([this.inputWrapper, this.overFlowWrapper], SHOW_TEXT);
        } else {
            addClass([this.inputWrapper], SHOW_CHIP);
        }
        if (this.options.value && this.options.value.length !== 0) {
            if (this.inputWrapper.contains(this.chipWrapper)) {
                this.chipWrapper.style.display = '';
                addClass([this.chipWrapper], HIDEICON);
            }
            addClass([this.element], CHIP_INPUT);
            this.updateOverFlowView();
            this.ensurePlaceHolder();
        }
        else {
            this.overFlowWrapper.innerHTML = '';
            addClass([this.overFlowWrapper], HIDEICON);
            removeClass([this.element], CHIP_INPUT);
        }
    }

    private updateOverFlowView(): void {
        this.overFlowWrapper.classList.remove('e-total-count');
        removeClass([this.overFlowWrapper], HIDEICON);
        let clearIconWidth: number;
        if (this.options.showClearButton) {
            clearIconWidth = (<HTMLElement>select('.e-clear-icon', this.inputWrapper)).offsetWidth;
            if (clearIconWidth === 0) {
                removeClass([this.overAllClear], ['e-icon-hide', 'e-clear-icon-hide']);
                clearIconWidth = this.overAllClear.offsetWidth;
                addClass([this.overAllClear], ['e-icon-hide', 'e-clear-icon-hide']);
            }
        }
        if (this.options.value && this.options.value.length) {
            let data: string = ''; let overAllContainer: number;
            let temp: string; let tempData: string; let templateTempData: any[] = [];
            let tempIndex: number = 1; let wrapperleng: number; let templateWrapperLength: any;
            let remaining: number; let downIconWidth: number = 0;
            this.overFlowWrapper.innerHTML = '';
            const remainElement: HTMLElement = createElement('span', { className: 'e-remain' });
            const remainContent: string = '+${count} more..';
            const totalContent: string = '${count} selected';
            this.overFlowWrapper.appendChild(remainElement);
            remainElement.innerText = remainContent.replace('${count}', this.options.value.length.toString());
            const remainSize: number = remainElement.offsetWidth;
            remove(remainElement);
            downIconWidth = (<HTMLElement>select('.' + 'e-ddt-icon', this.inputWrapper)).offsetWidth;
            if (!isNullOrUndefined(this.options.value)) {
                if (this.options.mode !== 'Box') {
                    if (!isNullOrUndefined(this.valueTemplateContainer)) {
                        addClass([this.valueTemplateContainer], HIDEICON);
                        const clonedElement: HTMLElement = <HTMLElement>(this.valueTemplateContainer as Node).cloneNode(true);
                        const chips: HTMLElement[] = Array.prototype.slice.call(clonedElement.children);
                        for (let i: number = 0; i < chips.length; i++) {
                            this.overFlowWrapper.appendChild(chips[i as number]);
                            templateWrapperLength = this.overFlowWrapper.offsetWidth;
                            overAllContainer = this.inputWrapper.offsetWidth;
                            if (templateWrapperLength + downIconWidth + clearIconWidth > overAllContainer) {
                                if (templateTempData !== undefined && templateTempData[i as number] !== '') {
                                    i = tempIndex + 1;
                                }
                                while (this.overFlowWrapper.firstChild) {
                                    this.overFlowWrapper.removeChild(this.overFlowWrapper.firstChild);
                                }
                                for (let j: number = 0; j < templateTempData.length; j++) {
                                    this.overFlowWrapper.appendChild(templateTempData[j as number]);
                                }
                                remaining = this.options.value.length - i;
                                templateWrapperLength = this.overFlowWrapper.offsetWidth;
                                while (
                                    templateWrapperLength + remainSize + downIconWidth + clearIconWidth >= overAllContainer &&
                                    wrapperleng !== 0 &&
                                    this.overFlowWrapper.firstChild
                                ) {
                                    this.overFlowWrapper.removeChild(this.overFlowWrapper.lastChild);
                                    remaining++;
                                    templateWrapperLength = this.overFlowWrapper.offsetWidth;
                                }
                                break;
                            } else if (templateWrapperLength + remainSize + downIconWidth + clearIconWidth <= overAllContainer) {
                                templateTempData.push(chips[i as number]); tempIndex = i;
                            } else if (i === 0) {
                                templateTempData = null; tempIndex = -1;
                            }
                        }
                        if (remaining > 0) {
                            this.overFlowWrapper.appendChild(
                                this.updateRemainTemplate(remainElement, remaining, remainContent, totalContent)
                            );
                        }
                        if (this.options.mode === 'Box' && !this.overFlowWrapper.classList.contains('e-total-count')) {
                            addClass([remainElement], 'e-wrap-count');
                        }
                        return;
                    }
                    const textArray: string[] = (this.element as HTMLInputElement).value.split(this.options.delimiterChar + ' ');
                    for (let index: number = 0; !isNullOrUndefined(textArray[index as number]); index++) {
                        data += (index === 0) ? '' : this.options.delimiterChar + ' ';
                        temp = textArray[index as number];
                        data += temp;
                        temp = this.overFlowWrapper.innerHTML;
                        this.overFlowWrapper.innerHTML = data;
                        wrapperleng = this.overFlowWrapper.offsetWidth;
                        overAllContainer = this.inputWrapper.offsetWidth;
                        if ((wrapperleng + downIconWidth + clearIconWidth) > overAllContainer) {
                            if (tempData !== undefined && tempData !== '') {
                                temp = tempData;
                                index = tempIndex + 1;
                            }
                            this.overFlowWrapper.innerHTML = temp;
                            remaining = this.options.value.length - index;
                            wrapperleng = this.overFlowWrapper.offsetWidth;
                            while (((wrapperleng + remainSize + downIconWidth + clearIconWidth) >= overAllContainer)
                                && wrapperleng !== 0 && this.overFlowWrapper.innerHTML !== '') {
                                const textArr: string[] = this.overFlowWrapper.innerHTML.split(this.options.delimiterChar);
                                textArr.pop();
                                this.overFlowWrapper.innerHTML = textArr.join(this.options.delimiterChar);
                                remaining++;
                                wrapperleng = this.overFlowWrapper.offsetWidth;
                            }
                            break;
                        } else if ((wrapperleng + remainSize + downIconWidth + clearIconWidth) <= overAllContainer) {
                            tempData = data; tempIndex = index;
                        } else if (index === 0) { tempData = ''; tempIndex = -1; }
                    }
                }
                else {
                    addClass([this.chipWrapper], HIDEICON);
                    addClass([this.overFlowWrapper], 'e-chip-list');
                    const ele: HTMLElement = <HTMLElement>(this.chipWrapper as Node).cloneNode(true);
                    const chips: HTMLElement[] = selectAll('.' + 'e-chips', ele);
                    for (let i: number = 0; i < chips.length; i++) {
                        temp = this.overFlowWrapper.innerHTML;
                        this.overFlowWrapper.appendChild(chips[i as number]);
                        data = this.overFlowWrapper.innerHTML;
                        wrapperleng = this.overFlowWrapper.offsetWidth;
                        overAllContainer = this.inputWrapper.offsetWidth;
                        if ((wrapperleng + downIconWidth + clearIconWidth) > overAllContainer) {
                            if (tempData !== undefined && tempData !== '') {
                                temp = tempData; i = tempIndex + 1;
                            }
                            this.overFlowWrapper.innerHTML = temp;
                            remaining = this.options.value.length - i;
                            wrapperleng = this.overFlowWrapper.offsetWidth;
                            while (((wrapperleng + remainSize + downIconWidth + clearIconWidth) >= overAllContainer)
                                && wrapperleng !== 0 && this.overFlowWrapper.innerHTML !== '') {
                                this.overFlowWrapper.removeChild(this.overFlowWrapper.lastChild);
                                remaining++;
                                wrapperleng = this.overFlowWrapper.offsetWidth;
                            }
                            break;
                        } else if ((wrapperleng + remainSize + downIconWidth + clearIconWidth) <= overAllContainer) {
                            tempData = data; tempIndex = i;
                        } else if (i === 0) {
                            tempData = ''; tempIndex = -1;
                        }
                    }
                    const finalChips: HTMLElement[] = selectAll('.e-chips', this.overFlowWrapper);
                    for (let i: number = 0; i < finalChips.length; i++) {
                        const deleteIcon: HTMLElement = select('.e-chips-close', finalChips[i as number]);
                        EventHandler.add(deleteIcon, 'mousedown', this.removeChip, this);
                    }
                }
            }
            if (remaining > 0) {
                this.overFlowWrapper.appendChild(
                    this.updateRemainTemplate(remainElement, remaining, remainContent, totalContent)
                );
            }
            if (this.options.mode === 'Box' && !this.overFlowWrapper.classList.contains('e-total-count')) {
                addClass([remainElement], 'e-wrap-count');
            }

        } else {
            this.overFlowWrapper.innerHTML = '';
            addClass([this.overFlowWrapper], HIDEICON);
        }
    }

    private updateRemainTemplate(remainElement: HTMLElement, remaining: number, remainContent: string, totalContent: string): HTMLElement {
        if (this.overFlowWrapper.firstChild && this.overFlowWrapper.firstChild.nodeType === 3 &&
            this.overFlowWrapper.firstChild.nodeValue === '') {
            this.overFlowWrapper.removeChild(this.overFlowWrapper.firstChild);
        }
        remainElement.innerHTML = '';
        remainElement.innerText = (this.overFlowWrapper.firstChild && (this.overFlowWrapper.firstChild.nodeType === 3 || this.options.mode === 'Box') || !isNullOrUndefined(this.valueTemplateContainer)) ?
            remainContent.replace('${count}', remaining.toString()) : totalContent.replace('${count}', remaining.toString());
        if (this.overFlowWrapper.firstChild && (this.overFlowWrapper.firstChild.nodeType === 3 || this.options.mode === 'Box')) {
            removeClass([this.overFlowWrapper], 'e-total-count');
        } else {
            addClass([this.overFlowWrapper], 'e-total-count');
            removeClass([this.overFlowWrapper], 'e-wrap-count');
        }
        return remainElement;
    }

    private setDisable(): void {
        if (!this.options.disabled) {
            this.element.setAttribute('aria-disabled', 'false');
        } else {
            if (this.isPopupOpen) {
                this.invokePopupEvent();
            }
            if (this.inputWrapper && this.inputWrapper.classList.contains(INPUTFOCUS)) {
                removeClass([this.inputWrapper], [INPUTFOCUS]);
            }
            this.element.setAttribute('aria-disabled', 'true');
        }
    }

    private createChip(): void {
        if (!this.inputWrapper.contains(this.chipWrapper)) {
            this.chipWrapper = select(CHIP_WRAPPER, this.inputWrapper);
            this.inputWrapper.insertBefore(this.chipWrapper, this.element);
            addClass([this.inputWrapper], SHOW_CHIP);
            const isValid: boolean = this.getValidMode();
            if (isValid && this.options.value !== null && (this.options.value && this.options.value.length !== 0)) {
                addClass([this.element], CHIP_INPUT);
            } else if (this.options.value === null || (this.options.value && this.options.value.length === 0) || this.checkBoxElement) {
                addClass([this.chipWrapper], HIDEICON);
            }
        }
    }

    private getValidMode(): boolean {
        if (this.options.allowMultiSelection || this.options.showCheckBox) {
            return this.options.mode === 'Box' ? true : (this.options.mode === 'Default' && this.inputFocus) ? true : false;
        } else {
            return false;
        }
    }

    private createPopup(element: HTMLElement): void {
        this.popupObj = new Popup(element, {
            width: this.setWidth(),
            targetType: 'relative',
            collision: { X: 'flip', Y: 'flip' },
            relateTo: this.inputWrapper,
            zIndex: this.options.zIndex,
            enableRtl: !isNullOrUndefined(select('.e-rtl')),
            position: { X: 'left', Y: 'bottom' },
            close: () => {
                this.isPopupOpen = false;
                this.dotNetRef.invokeMethodAsync('UpdatePopupState', this.isPopupOpen);
            },
            open: () => {
                this.isPopupOpen = true;
                this.dotNetRef.invokeMethodAsync('UpdatePopupState', this.isPopupOpen);

            },
            targetExitViewport: () => {
                if (!Browser.isDevice) { this.invokePopupEvent(); }
            }
        });
    }

    private getHeight(): string {
        let height: string = formatUnit(this.options.popupHeight);
        if (height.indexOf('%') > -1) {
            // Will set the height of the popup according to the view port height
            height = (document.documentElement.clientHeight * parseFloat(height) / 100).toString() + 'px';
        }
        return height;
    }

    private updatePopupHeight(): void {
        let popupHeight: string = this.getHeight();
        this.popupEle.style.maxHeight = popupHeight;
        const header: HTMLElement = select('.e-ddt-header', this.popupEle);
        const footer: HTMLElement = select('.e-ddt-footer', this.popupEle);
        if (this.options.allowFiltering) {
            const height: number = Math.round(this.filterContainer.getBoundingClientRect().height);
            popupHeight = formatUnit(parseInt(popupHeight, 10) - height + 'px');
        }
        if (header) {
            const height: number = Math.round(header.getBoundingClientRect().height);
            popupHeight = formatUnit(parseInt(popupHeight, 10) - height + 'px');
        }
        if (this.options.showCheckBox && this.options.showSelectAll && (!this.popupDiv.classList.contains(NODATA))) {
            const height: number = Math.round(this.checkAllParent.getBoundingClientRect().height);
            popupHeight = formatUnit(parseInt(popupHeight, 10) - height + 'px');
        }
        if (footer) {
            const height: number = Math.round(footer.getBoundingClientRect().height);
            popupHeight = formatUnit(parseInt(popupHeight, 10) - height + 'px');
        }
        let border: number = parseInt(window.getComputedStyle(this.popupEle).borderTopWidth, 10);
        border = border + parseInt(window.getComputedStyle(this.popupEle).borderBottomWidth, 10);
        popupHeight = formatUnit(parseInt(popupHeight, 10) - border + 'px');
        this.popupDiv.style.maxHeight = popupHeight;
    }

    private setWidth(): string {
        let width: string = formatUnit(this.options.popupWidth);
        if (width.indexOf('%') > -1) {
            width = (this.inputWrapper.offsetWidth * parseFloat(width) / 100).toString() + 'px';
        }
        return width;
    }

    private onDocumentClick(e: MouseEvent): void {
        const target: HTMLElement = <HTMLElement>e.target;
        const isTree: Element = closest(target, '.' + PARENTITEM);
        const isFilter: Element = closest(target, '.' + FILTERWRAP);
        const isHeader: Element = closest(target, '.' + HEADER);
        const isFooter: Element = closest(target, '.' + FOOTER);
        const isScroller: boolean = target.classList.contains(DROPDOWN) ? true :
            (matches(target, '.e-ddt .e-popup') || matches(target, '.e-ddt .e-treeview'));
        if (this.overAllClear && target === this.overAllClear || target.classList.contains('e-chips-close')) {
            this.isClearIconClick = true;
        }
        if ((this.isPopupOpen && ((!isNullOrUndefined(this.inputWrapper) && this.inputWrapper.contains(target)) || isTree
        || isScroller || isHeader || isFooter)) || ((this.options.allowMultiSelection || this.options.showCheckBox) &&
        (this.isPopupOpen && target.classList.contains(CHIP_CLOSE) || (this.isPopupOpen && (target.classList.contains(CHECKALLPARENT) ||
        target.classList.contains(ALLTEXT) || target.classList.contains(CHECKBOXFRAME) || target.classList.contains(CHECKBOXLABEL)))))) {
            e.preventDefault();
        } else if (!isNullOrUndefined(this.inputWrapper) && !this.inputWrapper.contains(target) && this.inputFocus) {
            this.focusOut(e, !isNullOrUndefined(isFilter));
        }
    }

    private wireEvents(): void {
        EventHandler.add(this.inputWrapper, 'focus', this.focusIn, this);
        EventHandler.add(this.inputWrapper, 'blur', this.focusOut, this);
        document.addEventListener('mousedown', this.onDocumentClick.bind(this));
        this.keyboardModule = new KeyboardEvents(
            this.inputWrapper,
            {
                keyAction: this.inputKeyActionHandler.bind(this),
                keyConfigs: this.keyConfigs,
                eventName: 'keydown'
            }
        );
        window.addEventListener('resize', this.onWindowResize.bind(this));
    }

    private unWireEvents(): void {
        EventHandler.remove(this.inputWrapper, 'focus', this.focusIn);
        EventHandler.remove(this.inputWrapper, 'blur', this.focusOut);
        document.removeEventListener('mousedown', this.onDocumentClick.bind(this));
        window.removeEventListener('resize', this.onWindowResize.bind(this));
        if (this.keyboardModule) {
            this.keyboardModule.destroy();
        }
    }

    private onWindowResize(): void {
        if (this.isPopupOpen) {
            this.popupObj.setProperties({ width: this.setWidth() });
            this.popupObj.refreshPosition();
        }
    }

    private inputKeyActionHandler(e: KeyboardEventArgs): void {
        switch (e.action) {
        case 'escape':
        case 'altUp':
            if (this.isPopupOpen) {
                this.invokePopupEvent();
            }
            break;
        case 'shiftTab':
        case 'tab':
            if (this.isPopupOpen) {
                this.invokePopupEvent();
            }
            if (this.inputFocus) {
                this.focusOut(e);
            }
            break;
        case 'altDown':
            if (!this.isPopupOpen) {
                this.dotNetRef.invokeMethodAsync('InvokePopupEvent', null);
                e.preventDefault();
            }
            break;
        case 'moveDown':
            if (this.options.showSelectAll && this.options.showCheckBox) {
                this.checkAllParent.focus();
            }
            break;
        }
    }

    private popupKeyActionHandler(e: KeyboardEventArgs): void {
        switch (e.target) {
        case this.filterInputEle:
            this.filterAction(e);
            break;
        case this.checkAllParent:
            this.checkAllAction(e);
            break;
        default:
            if (this.popupDiv.contains(e.target as Node)) {
                this.treeAction(e);
            }
            break;
        }
    }

    private checkAllAction(e: KeyboardEventArgs): void {
        switch (e.action) {
        case 'space':
            this.dotNetRef.invokeMethodAsync('OnSelectAllClick');
            break;
        case 'moveDown': {
            const focusedElement: HTMLElement = this.popupDiv.querySelector('li');
            focusedElement.focus();
            break;
        }
        case 'shiftTab':
            e.preventDefault();
            if (this.options.allowFiltering) {
                this.filterInputEle.focus();
            } else {
                this.inputWrapper.focus();
            }
        }
    }

    private treeAction(e: KeyboardEventArgs): void {
        switch (e.action) {
        case 'escape':
        case 'altUp':
            this.inputWrapper.focus();
            e.preventDefault();
            if (this.isPopupOpen) {
                this.invokePopupEvent();
            }
            break;
        case 'tab':
            if (this.isPopupOpen) {
                this.invokePopupEvent();
            }
            break;
        case 'shiftTab':
            e.preventDefault();
            if (this.options.showSelectAll && this.options.showCheckBox) {
                this.checkAllParent.focus();
            }
            else if (this.options.allowFiltering) {
                this.filterInputEle.focus();
            } else {
                this.inputWrapper.focus();
            }
            break;
        }
    }

    private filterAction(e: KeyboardEventArgs): void {
        switch (e.action) {
        case 'escape':
        case 'altUp':
            this.inputWrapper.focus();
            e.preventDefault();
            if (this.isPopupOpen) {
                this.invokePopupEvent();
            }
            break;
        case 'shiftTab':
            this.inputFocus = false;
            e.preventDefault();
            this.inputWrapper.focus();
            break;
        case 'tab':
            if (this.options.showSelectAll && this.options.showCheckBox) {
                this.checkAllParent.focus();
            }
            else {
                const focusedElement: HTMLElement = this.popupDiv.querySelector('li');
                focusedElement.focus();
                e.preventDefault();
            }
            break;
        }
    }

    private showPopup(): void {
        if (this.options.disabled || this.isPopupOpen) {
            return;
        }
        this.focusIn();
        this.renderPopup();
    }

    private updateInputElement(): void {
        addClass([this.inputWrapper], SHOW_CHIP);
        addClass([this.element], CHIP_INPUT);
    }

    private focusIn(e?: FocusEvent | MouseEvent | KeyboardEvent | TouchEvent): void {
        if (this.options.disabled || this.inputFocus || this.isClearIconClick) {
            this.isClearIconClick = false;
            return;
        }
        this.inputFocus = true;
        addClass([this.inputWrapper], [INPUTFOCUS]);
        if (this.options.allowMultiSelection || this.options.showCheckBox) {
            if (this.options.mode !== 'Delimiter' && this.inputFocus) {
                if (this.chipWrapper && (this.options.value && this.options.value.length !== 0)) {
                    removeClass([this.chipWrapper], HIDEICON);
                    addClass([this.element], CHIP_INPUT);
                }
                addClass([this.inputWrapper], SHOW_CHIP);
            }
            if (!this.options.textWrap && this.options.mode !== 'Custom') {
                if (this.inputWrapper.contains(this.overFlowWrapper)) {
                    addClass([this.overFlowWrapper], HIDEICON);
                }
                if (this.options.mode === 'Delimiter') {
                    if (!isNullOrUndefined(this.valueTemplateContainer)) {
                        this.updateInputElement();
                        this.showOrHideValueTemplate(true);
                    } else {
                        removeClass([this.inputWrapper], SHOW_CHIP);
                        removeClass([this.element], CHIP_INPUT);
                    }
                } else {
                    addClass([this.inputWrapper], SHOW_CHIP);
                }
                removeClass([this.inputWrapper], SHOW_TEXT);
                this.ensurePlaceHolder();
            } else if (this.options.textWrap && !isNullOrUndefined(this.valueTemplateContainer) && this.options.mode !== 'Delimiter') {
                addClass([this.valueTemplateContainer], HIDEICON);
            }
            if (this.popupObj) {
                this.popupObj.refreshPosition();
            }
        } else {
            if (!isNullOrUndefined(this.valueTemplateContainer)) {
                addClass([this.inputWrapper], SHOW_CHIP);
                if (this.options.mode !== 'Box' && this.options.value && this.options.value.length !== 0) {
                    addClass([this.element], CHIP_INPUT);
                }
            } else {
                removeClass([this.inputWrapper], SHOW_CHIP);
                removeClass([this.element], CHIP_INPUT);
            }
        }
    }

    private focusOut(e: MouseEvent | KeyboardEventArgs, isFilter?: boolean): void {
        if (this.options.disabled || !this.inputFocus) {
            return;
        }
        if ((Browser.isIE || Browser.info.name === 'edge') && (e.target === this.inputWrapper)) {
            return;
        }
        if (e.target !== this.inputWrapper || !this.isPopupOpen) {
            this.onFocusOut(isFilter);

        }
    }

    private onFocusOut(isFilter: boolean = false): void {
        this.inputFocus = isFilter;
        if (this.isPopupOpen && !isFilter) {
            this.invokePopupEvent();
        }
        if (this.overAllClear && !this.overAllClear.classList.contains(HIDEICON)) {
            addClass([this.overAllClear], HIDEICON);
            removeClass([this.inputWrapper], SHOW_CLEAR);
        }
        removeClass([this.inputWrapper], [INPUTFOCUS]);
        if ((this.options.allowMultiSelection || this.options.showCheckBox)) {
            if (this.options.mode !== 'Delimiter' && this.options.mode !== 'Custom') {
                if (this.chipWrapper && (this.options.mode === 'Default')) {
                    this.chipWrapper.style.display = '';
                    addClass([this.chipWrapper], HIDEICON);
                    removeClass([this.inputWrapper], SHOW_CHIP);
                    removeClass([this.element], CHIP_INPUT);
                }
            }
            if (!this.options.textWrap && this.options.value && this.options.value.length) {
                this.updateView();
            } else if (this.options.textWrap && !isNullOrUndefined(this.valueTemplateContainer) && this.options.mode !== 'Box') {
                addClass([this.element], CHIP_INPUT);
                this.ensurePlaceHolder();
                removeClass([this.valueTemplateContainer], HIDEICON);
            }
        }
    }

    private invokePopupEvent(): void {
        const popupArgs: PopupModel = {
            offsetX: this.popupObj.offsetX, offsetY: this.popupObj.offsetY, targetType: this.popupObj.targetType,
            collision: { X: this.popupObj.collision.X, Y: this.popupObj.collision.Y },
            position: { X: this.popupObj.position.X, Y: this.popupObj.position.Y }
        };
        this.dotNetRef.invokeMethodAsync('InvokePopupEvent', popupArgs);
    }

    public closePopup(): void {
        this.inputWrapper.classList.remove(ICONANIMATION);
        if (this.popupEle) {
            addClass([this.popupEle], DDTHIDEICON);
        }
        attributes(this.element, { 'aria-expanded': 'false' });
        if (this.popupObj && this.isPopupOpen) {
            this.popupObj.hide();
            this.popupObj.destroy();
            this.popupObj = null;
            if (this.inputFocus) {
                this.inputWrapper.focus();
                if (this.options.allowFiltering) {
                    addClass([this.inputWrapper], [INPUTFOCUS]);
                }
            }
        }
    }

    private showOverAllClear(): void {
        if (this.options.disabled) {
            return;
        }
        if (this.options.showClearButton) {
            this.overAllClear = select('.e-clear-icon', this.inputWrapper);
        }
        if (this.overAllClear) {
            const isValue: boolean = this.options.value ? (this.options.value.length ? true : false) : false;
            if (isValue && this.options.showClearButton) {
                removeClass([this.overAllClear], [HIDEICON, 'e-clear-icon-hide']);
                addClass([this.inputWrapper], SHOW_CLEAR);
            } else {
                addClass([this.overAllClear], HIDEICON);
                removeClass([this.inputWrapper], SHOW_CLEAR);
            }
        }
    }

    public onNodeSelected(value: string[]): void {
        this.options.value = value;
        this.showOverAllClear();
        if (!isNullOrUndefined(this.popupObj)) {
            this.invokePopupEvent();
        }
    }

    public clearIconClick(value: string[]): void {
        this.options.value = value;
        this.showOverAllClear();
        this.showOrHideValueTemplate(false);
        if (isNullOrUndefined(this.options.value) || (this.options.value && this.options.value.length === 0)) {
            removeClass([this.element], CHIP_INPUT);
            if (!this.options.textWrap && !isNullOrUndefined(this.overFlowWrapper)) {
                addClass([this.overFlowWrapper], HIDEICON);
            }
            if (this.options.mode !== 'Delimiter' && !isNullOrUndefined(this.chipWrapper)) {
                addClass([this.chipWrapper], HIDEICON);
                this.chipWrapper.style.display = '';
            }
        }
        this.ensurePlaceHolder();
        if ((this.options.allowMultiSelection || this.options.showCheckBox)) {
            if (this.popupObj) {
                this.popupObj.refreshPosition();
            }
        }
    }
    public updateSelectedValue(setChipWrapper: boolean): void {
        this.checkInputValueAvailable();
        const isValue: boolean = this.options.value ? (this.options.value.length ? true : false) : false;
        if ((this.options.mode !== 'Delimiter') && (this.options.allowMultiSelection || this.options.showCheckBox) && isValue) {
            addClass([this.inputWrapper], SHOW_CHIP);
            this.chipWrapper.style.display = 'block';
        }
        const isValid: boolean = this.getValidMode();
        if (this.options.mode !== 'Custom' && this.options.mode !== 'Box' && !isValid) {
            if ((this.options.allowMultiSelection || this.options.showCheckBox)) {
                if (!isNullOrUndefined(this.valueTemplateContainer)) {
                    this.updateInputElement();
                    if (!this.options.textWrap) {
                        addClass([this.overFlowWrapper], HIDEICON);
                    }
                    removeClass([this.valueTemplateContainer], HIDEICON);
                }
                if (this.chipWrapper) {
                    addClass([this.chipWrapper], HIDEICON);
                    removeClass([this.inputWrapper], SHOW_CHIP);
                    this.chipWrapper.style.display = '';
                }
                this.showOrHideValueTemplate(true);
            } else {
                if (!isNullOrUndefined(this.valueTemplateContainer)) {
                    this.updateInputElement();
                    removeClass([this.valueTemplateContainer], HIDEICON);
                }
            }
        }
        if (this.options.mode === 'Custom' && (this.options.allowMultiSelection || this.options.showCheckBox)) {
            this.setCustomModeClass();
        }
        if (this.options.showClearButton && this.inputFocus) {
            this.showOverAllClear();
        }
        if (setChipWrapper) {
            this.setChipWrapperClass();
        }
        if ((this.options.allowMultiSelection || this.options.showCheckBox) && this.popupObj) {
            this.popupObj.refreshPosition();
        }
        this.ensurePlaceHolder();
    }

    private updateValue(value: string[]): void {
        this.checkInputValueAvailable();
        const isValid: boolean = this.getValidMode();
        if (this.options.mode !== 'Custom' && this.options.mode !== 'Box' && !isValid) {
            if (isNullOrUndefined(this.valueTemplateContainer)) {
                this.updateInputElement();
                removeClass([this.valueTemplateContainer], HIDEICON);
            }
            this.showOrHideValueTemplate(true);
        }
        if (this.options.showClearButton && this.inputFocus) {
            this.showOverAllClear();
        }
        this.closePopup();
    }

    private setCustomModeClass(): void {
        if (isNullOrUndefined(this.options.value)) {
            return;
        }
        if (!this.inputWrapper.contains(this.chipWrapper)) {
            this.createChip();
        }
        if (!this.inputWrapper.classList.contains(SHOW_CHIP)) {
            addClass([this.inputWrapper], SHOW_CHIP);
        }
        if (!this.element.classList.contains(CHIP_INPUT)) {
            addClass([this.element], CHIP_INPUT);
        }
        if (this.chipWrapper.classList.contains(HIDEICON)) {
            removeClass([this.chipWrapper], HIDEICON);
        }
    }

    private ensurePlaceHolder(): void {
        if (isNullOrUndefined(this.options.value) || (this.options.value && this.options.value.length === 0)) {
            removeClass([this.element], CHIP_INPUT);
            if (this.chipWrapper) {
                addClass([this.chipWrapper], HIDEICON);
                this.chipWrapper.style.display = '';
            }
        }
    }

    private setChipWrapperClass(): void {
        const checkSelection: boolean = this.options.allowMultiSelection ? true : (this.options.showCheckBox ? true : false);
        if (this.inputWrapper.contains(this.chipWrapper) && !checkSelection) {
            removeClass([this.element], CHIP_INPUT);
            detach(this.chipWrapper);
        }
        const isValid: boolean = this.getValidMode();
        if (isValid && this.options.value !== null) {
            addClass([this.element], CHIP_INPUT);
            if (this.chipWrapper) {
                removeClass([this.chipWrapper], HIDEICON);
            }
        }
        const isValue: boolean = this.options.value ? (this.options.value.length ? true : false) : false;
        if (this.chipWrapper && (this.options.mode === 'Box' && !isValue)) {
            addClass([this.chipWrapper], HIDEICON);
            if (!this.options.textWrap) {
                addClass([this.overFlowWrapper], HIDEICON);
            }
            removeClass([this.element], CHIP_INPUT);
            this.chipWrapper.style.display = '';
        }
        if (!this.options.textWrap && this.inputWrapper.offsetWidth !== 0 &&
            (this.options.allowMultiSelection || this.options.showCheckBox)) {
            this.updateView();
        } else if (this.options.textWrap && !isNullOrUndefined(this.valueTemplateContainer)) {
            addClass([this.inputWrapper], SHOW_TEXT);
        }
    }

    private updateOverflowWrapper(state: boolean): void {
        if (!state) {
            if (!this.inputWrapper.contains(this.overFlowWrapper)) {
                this.overFlowWrapper = select(OVERFLOW_VIEW + ':not(.e-input-value)', this.inputWrapper);
                this.inputWrapper.insertBefore(this.overFlowWrapper, this.element);
            }
        } else if (this.inputWrapper.contains(this.overFlowWrapper) && state) {
            while (this.overFlowWrapper.firstChild) {
                this.overFlowWrapper.removeChild(this.overFlowWrapper.firstChild);
            }
        }
    }

    private updateMode(): void {
        if (!this.options.textWrap) {
            const overFlow: Element = select(OVERFLOW_VIEW + ':not(.e-input-value)', this.inputWrapper);
            if (overFlow) {
                while (overFlow.firstChild) {
                    overFlow.removeChild(overFlow.firstChild);
                }
            }
        }
        if (this.options.mode === 'Custom') { return; }
        if (this.options.mode !== 'Delimiter') {
            if (!this.inputWrapper.contains(this.chipWrapper)) {
                this.createChip();
            }
            const isValid: boolean = this.getValidMode();
            if (this.chipWrapper.classList.contains(HIDEICON) && isValid) {
                removeClass([this.chipWrapper], HIDEICON);
                this.showOrHideValueTemplate(false, true);
                addClass([this.inputWrapper], SHOW_CHIP);
            } else if (!isValid) {
                addClass([this.chipWrapper], HIDEICON);
                removeClass([this.inputWrapper], SHOW_CHIP);
                this.showOrHideValueTemplate(true);
                this.chipWrapper.style.display = '';
            }
            const isValue: boolean = this.options.value !== null ? (this.options.value.length !== 0 ? true : false) : false;
            if ((isValid && isValue) || !isNullOrUndefined(this.valueTemplateContainer)) {
                addClass([this.element], CHIP_INPUT);
            } else {
                removeClass([this.element], CHIP_INPUT);
            }
        } else if (this.element.classList.contains(CHIP_INPUT)) {
            removeClass([this.element], CHIP_INPUT);
            if (this.chipWrapper) {
                addClass([this.chipWrapper], HIDEICON);
                if (!this.options.textWrap) {
                    addClass([this.overFlowWrapper], HIDEICON);
                }
                removeClass([this.inputWrapper], SHOW_CHIP);
                this.chipWrapper.style.display = '';
                this.showOrHideValueTemplate(true);
            }
        }
        if (!this.options.textWrap && (this.options.value && this.options.value.length !== 0)) {
            this.updateOverFlowView();
            addClass([this.element], CHIP_INPUT);
            if (this.options.mode === 'Box') {
                removeClass([this.overFlowWrapper, this.inputWrapper], SHOW_TEXT);
            } else {
                addClass([this.overFlowWrapper, this.inputWrapper], SHOW_TEXT);
            }
        }
    }

    private showOrHideValueTemplate(show: boolean, showChip: boolean = false): void {
        if (!isNullOrUndefined(this.valueTemplateContainer)) {
            if (show) {
                removeClass([this.valueTemplateContainer], HIDEICON);
                this.updateInputElement();
            } else {
                addClass([this.valueTemplateContainer], HIDEICON);
                if (!showChip) {
                    removeClass([this.inputWrapper], SHOW_CHIP);
                    removeClass([this.element], CHIP_INPUT);
                }
            }
        }
    }

    public updateProperties(options: IDropDownTreeOptions): void {
        for (const prop of Object.keys(options)) {
            switch (prop) {
            case 'showSelectAll':
                this.options.showSelectAll = options.showSelectAll;
                break;
            case 'showCheckBox':
                this.options.showCheckBox = options.showCheckBox;
                this.createOverFlowWrapper();
                break;
            case 'popupHeight':
                this.options.popupHeight = options.popupHeight;
                if (this.popupObj) {
                    this.popupObj.height = this.options.popupHeight;
                    this.updatePopupHeight();
                }
                break;
            case 'popupWidth':
                this.options.popupWidth = options.popupWidth;
                if (this.popupObj) {
                    this.popupObj.element.style.width = this.setWidth();
                }
                break;
            case 'zIndex':
                this.options.zIndex = options.zIndex;
                if (this.popupObj) {
                    this.popupObj.zIndex = this.options.zIndex;
                }
                break;
            case 'allowFiltering':
                this.options.allowFiltering = options.allowFiltering;
                break;
            case 'allowMultiSelection':
                this.options.allowMultiSelection = options.allowMultiSelection;
                this.createOverFlowWrapper();
                break;
            case 'disabled':
                this.options.disabled = options.disabled;
                this.setDisable();
                break;
            case 'mode': {
                if (!this.options.showCheckBox && !this.options.allowMultiSelection) { return; }
                const oldMode: string = this.options.mode;
                this.options.mode = options.mode;

                if (this.options.mode === 'Custom') {
                    if (this.overFlowWrapper) {
                        detach(this.overFlowWrapper);
                    }
                    if (this.chipWrapper) {
                        detach(this.chipWrapper);
                    }
                    this.setCustomModeClass();
                } else {
                    if (oldMode === 'Custom') { this.updateOverflowWrapper(this.options.textWrap); }
                    this.updateMode();
                }
                break;
            }
            case 'showClearButton':
                this.options.showClearButton = options.showClearButton;
                this.overAllClear = select('.e-clear-icon', this.inputWrapper);
                break;
            case 'textWrap':
                this.options.textWrap = options.textWrap;
                this.updateOverflowWrapper(this.options.textWrap);
                if ((this.options.allowMultiSelection || this.options.showCheckBox) && !this.options.textWrap) {
                    this.updateView();
                }
                else {
                    addClass([this.overFlowWrapper], HIDEICON);
                    removeClass([this.inputWrapper], SHOW_TEXT);
                    if (this.chipWrapper && this.options.mode === 'Box') {
                        removeClass([this.chipWrapper], HIDEICON);
                    }
                    else {
                        removeClass([this.inputWrapper], SHOW_CHIP);
                        removeClass([this.element], CHIP_INPUT);
                    }
                }
                break;
            }
        }
    }

    public destroy(): void {
        if (this.popupObj) {
            this.popupObj.destroy();
            this.popupObj = null;
        }
        this.popupDiv = null;
        this.popupEle = null;
        this.isPopupOpen = false;
        this.unWireEvents();
        this.overAllClear = null;
        this.inputWrapper = null;
        this.keyboardModule = null;
        this.element = null;
    }
}

const DropDownTree: object = {
    initialize(dataId: string, containerElement: HTMLElement, dotnetRef: BlazorDotnetObject,
               options: IDropDownTreeOptions, uniqueID: string): void {
        const instance: any = new SfDropDownTree(dataId, containerElement, dotnetRef, options, uniqueID);
        if (!isNullOrUndefined(instance)) {
            instance.initialize();
        }
    },
    showPopup(dataId: string, value: string[], args: MouseEventArgs, popupContentElement: HTMLElement): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNullOrUndefined(instance)) {
            instance.popupDiv = popupContentElement;
            instance.options.value = value;
            if ((instance.chipWrapper && instance.options.mode !== 'Delimiter') && instance.options.value && instance.options.value.length !== 0) {
                instance.chipWrapper.style.display = 'block';
                if (!isNullOrUndefined(instance.overFlowWrapper) && !isNullOrUndefined(instance.valueTemplateContainer) &&
                        !instance.options.textWrap) {
                    addClass([instance.overFlowWrapper], HIDEICON);
                }
            }
            if (!isNullOrUndefined(args)) {
                const target: HTMLElement = <HTMLElement>document.elementFromPoint(args.clientX, args.clientY);
                if (target && target.classList.contains('e-chips-close')) {
                    return;
                }
            }
            if (!instance.isPopupOpen && !(!isNullOrUndefined(args) && args.button === 2)) {
                instance.showOverAllClear();
                instance.showPopup();
                instance.inputFocus = true;
            }
        }
    },
    invokePopupEvent(dataId: string, value: string[], args: MouseEventArgs): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNullOrUndefined(instance)) {
            instance.options.value = value;
            if (!isNullOrUndefined(args)) {
                const target: HTMLElement = <HTMLElement>document.elementFromPoint(args.clientX, args.clientY);
                if (target && target.classList.contains('e-chips-close')) {
                    return;
                }
            }
            if (instance.isPopupOpen) {
                instance.invokePopupEvent();
                instance.showOverAllClear();
            }
        }
    },
    closePopup(dataId: string): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNullOrUndefined(instance)) {
            instance.closePopup();
        }
    },
    onNodeSelected(dataId: string, value: string[]): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNullOrUndefined(instance)) {
            instance.onNodeSelected(value);
        }
    },
    updateValue(dataId: string, value: string[]): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNullOrUndefined(instance)) {
            instance.updateValue(value);
        }
    },
    clearIconClick(dataId: string, value: string[], removeFocus: boolean): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNullOrUndefined(instance)) {
            instance.clearIconClick(value);
            if (removeFocus && instance.inputFocus) {
                instance.onFocusOut();
            }
        }
    },
    updateSelectedValue(dataId: string, value: string[], setChipWrapper: boolean): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNullOrUndefined(instance)) {
            instance.options.value = value;
            instance.updateSelectedValue(setChipWrapper);
        }
    },
    getTreeItemsId(dataId: string): string[] {
        const items: string[] = [];
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNullOrUndefined(instance)) {
            const li: HTMLElement[] = selectAll('li', instance.popupDiv);
            let id: string;
            for (let i: number = 0; i < li.length; i++) {
                id = li[i as number].getAttribute('data-uid').toString();
                items.push(id);
            }
        }
        return items;
    },
    updateProperties(dataId: string, options: IDropDownTreeOptions): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNullOrUndefined(instance)) {
            instance.updateProperties(options);
        }
    },
    refreshPosition(dataId: string): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNullOrUndefined(instance && instance.popupObj)) {
            instance.popupObj.refreshPosition();
        }
    },
    destroy(dataId: string): void {
        const instance: any = this.sfBlazor.getCompInstance(dataId);
        if (!isNullOrUndefined(instance)) {
            instance.destroy();
        }
    }
};

interface IDropDownTreeOptions {
    disabled: boolean;
    allowMultiSelection: boolean;
    showCheckBox: boolean;
    allowFiltering: boolean;
    popupWidth: string | number;
    popupHeight: string | number;
    zIndex: number;
    showSelectAll: boolean;
    showClearButton: boolean;
    value: string[];
    mode: string;
    delimiterChar: string;
    textWrap: boolean;
}

export interface nodeCheckEventArgs {
    isInteracted: boolean;
    action: string;
}

export default DropDownTree;
