import { EventHandler, isNullOrUndefined, BlazorDotnetObject, extend, attributes, removeClass, BaseEventArgs } from '@syncfusion/ej2-base';

const ITEMCONTAINER: string = 'e-step-container';
const ITEMLIST: string = 'e-stepper-steps';
const ICONCSS: string = 'e-indicator';
const TEXTCSS: string = 'e-step-text-container';
const STEPLABEL: string = 'e-step-label-container';
const SELECTED: string = 'e-step-selected';
const INPROGRESS: string = 'e-step-inprogress';
const NOTSTARTED: string = 'e-step-notstarted';
const FOCUS: string = 'e-step-focus';
const COMPLETED: string = 'e-step-completed';
const DISABLED: string = 'e-step-disabled';
const PROGRESSVALUE: string = '--progress-value';
const RTL: string = 'e-rtl';
const LABELAFTER: string = 'e-label-after';
const LABELBEFORE: string = 'e-label-before';
const HORIZSTEP: string = 'e-horizontal';
const STEPICON: string = 'e-step-item';
const STEPTEXT: string = 'e-step-text';
const LABEL: string = 'e-label';
const STEPINDICATOR: string = 'e-step-type-indicator';
const PREVSTEP: string = 'e-previous';
const NEXTSTEP: string = 'e-next';
/**
 * Provides information about stepChanging event callback.
 */
interface StepperChangingEventArgs extends StepperChangedEventArgs {
    /**
     * Provides whether the change has been prevented or not. Default value is false.
     */
    cancel: boolean;
}
/**
 * Provides information about stepChanged event callback.
 */
interface StepperChangedEventArgs extends BaseEventArgs {
    /**
     * Provides the original event.
     */
    event: Event;

    /**
     * Provides whether the change is triggered by user interaction.
     */
    isInteracted: boolean;

    /**
     * Provides the index of the previous step.
     */
    previousStep: number;

    /**
     * Provides the index of the current step.
     */
    activeStep: number;

    /**
     * Provides the stepper element.
     */
    element: HTMLElement;
}

class SfStepper {

    /* Property variables */
    private activeStep: number;
    private element: HTMLElement;
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    private sfBlazor: any = (window as any).sfBlazor;
    private dotNetRef: BlazorDotnetObject;
    private enableRtl: boolean;
    private readOnly: boolean;
    private linear: boolean;

    /*Local varibles */
    private progressbar: HTMLElement;
    private progressValue: HTMLElement;
    private stepperItemList: HTMLElement;
    private liElements: NodeListOf<HTMLElement>;
    protected progressBarPosition: number;
    private beforeLabelWidth: number;
    private textEleWidth: number;
    private isKeyNavFocus: boolean;
    private isDefaultStep: boolean;
    private stepperStatus: string;
    private statusIndex: number;
    private dataId: string;
    private duration: number;

    constructor(options: { [key: string]: Object }) {
        this.dataId = options.dataId as string;
        this.updateContext(options);
        this.sfBlazor.setCompInstance(this);
        this.stepperItemList = this.element.querySelector('.' + ITEMLIST);
        this.progressValue = this.element.querySelector('.e-progressbar-value');
        this.progressbar = this.element.querySelector('.e-stepper-progressbar');
        this.getElements();
        this.bindEvent();
        this.renderProgressBar();
        this.navigateToStep(this.activeStep, false, null);
    }
    private updateContext(stepperObj: { [key: string]: Object }): void {
        extend(this, this, stepperObj);
    }
    private getElements(): void {
        this.liElements = this.element.querySelectorAll('.' + ITEMCONTAINER);
    }
    private bindItemEvent(liElement: HTMLElement, index: number): void {
        EventHandler.add(liElement, 'keydown', this.keyActionHandler.bind(this), this);
        EventHandler.add(liElement, 'click', (e: Event) => { return this.linearModeHandler(e, index); }, this);
        EventHandler.add(<HTMLElement & Window><unknown>window, 'resize', () => { this.resizeHandler(); }, this);
    }
    private bindEvent(): void {
        EventHandler.add(<HTMLElement & Window><unknown>window, 'click', () => { this.updateStepFocus(); }, this);
        for (let i: number = 0; i < this.liElements.length; i++) {
            this.bindItemEvent(this.liElements[parseInt(i.toString(), 10)], i);
        }
    }
    private unBindItemEvent(liElement: HTMLElement, index: number): void {
        EventHandler.remove(liElement, 'keydown', this.keyActionHandler);
        EventHandler.remove(liElement, 'click', (e: Event) => { return this.linearModeHandler(e, index); });
        EventHandler.remove(<HTMLElement & Window><unknown>window, 'resize', () => { this.resizeHandler(); });
    }
    private unBindEvent(): void {
        EventHandler.remove(<HTMLElement & Window><unknown>window, 'click', () => { this.updateStepFocus(); });
        for (let i: number = 0; i < this.liElements.length; i++) {
            this.unBindItemEvent(this.liElements[parseInt(i.toString(), 10)], i);
        }
    }
    private linearModeHandler(e: Event, index: number): void {
        if (this.linear) {
            const linearModeValue: number = index - this.activeStep;
            if (Math.abs(linearModeValue) === 1) { this.stepClickHandler(index, e); }
        } else { this.stepClickHandler(index, e); }
    }
    private resizeHandler(): void {
        if (this.stepperItemList && this.progressbar && this.element.classList.contains(HORIZSTEP)) { return this.renderProgressBar(); }
    }
    private stepClickHandler(index: number, e: Event): void {
        if (!this.readOnly) {
            this.dotNetRef.invokeMethodAsync('StepClickHandler', this.activeStep, index);
            this.navigateToStep(index, true, e);
        }
    }
    private updateStepFocus(): void {
        if (this.isKeyNavFocus) {
            this.isKeyNavFocus = false;
            const isFocus: HTMLElement = this.element.querySelector('.' + FOCUS);
            if (isFocus) { isFocus.classList.remove(FOCUS); this.element.classList.remove('e-steps-focus'); }
        }
    }
    private renderProgressBar(): void {
        if (this.element.classList.contains(HORIZSTEP)) {
            const stepItemContainer: HTMLElement = (this.element.querySelector('.' + ITEMCONTAINER));
            const stepItemEle: HTMLElement = stepItemContainer.firstChild as HTMLElement;
            const lastEle: HTMLElement = this.stepperItemList.lastChild.firstChild as HTMLElement;
            const textEle: HTMLElement = stepItemContainer.querySelector('.' + TEXTCSS);
            const labelEle: HTMLElement = stepItemContainer.querySelector('.' + STEPLABEL);
            if ((!stepItemContainer.classList.contains(STEPICON)) && (textEle && !textEle.classList.contains('e-text') || labelEle && !labelEle.classList.contains('e-label'))) {
                const targetEle: HTMLElement = textEle ? textEle.querySelector('.e-text') as HTMLElement : labelEle.querySelector('.e-label') as HTMLElement;
                this.progressbar.style.setProperty('--progress-top-position', targetEle.offsetHeight / 2 + 5 + 'px');
            } else {
                const topPos: number = (this.element.classList.contains('e-label-before')) ?
                    ((this.stepperItemList as HTMLElement).offsetHeight - (stepItemEle.offsetHeight / 2) - 1) :
                    (stepItemEle.offsetHeight / 2);
                this.progressbar.style.setProperty('--progress-top-position', topPos + 'px');
            }
            if (this.element.classList.contains(RTL)) {
                const leftPost: number = ((stepItemEle.offsetLeft + stepItemEle.offsetWidth) - (this.stepperItemList).offsetWidth);
                this.progressbar.style.setProperty('--progress-left-position', Math.abs(leftPost) + 'px');
                this.progressbar.style.setProperty('--progress-bar-width', Math.abs(lastEle.offsetLeft - stepItemEle.offsetLeft) + 'px');
            } else {
                this.progressbar.style.setProperty('--progress-left-position', (stepItemEle.offsetLeft + 1) + 'px');
                this.progressbar.style.setProperty('--progress-bar-width', ((lastEle.offsetWidth + lastEle.offsetLeft - 2) - (stepItemEle.offsetLeft + 2)) + 'px');
            }
        }
        else {
            this.progressBarPosition = this.beforeLabelWidth = this.textEleWidth = 0;
            const isBeforeLabel: boolean = (this.element.classList.contains(LABELBEFORE)) ? true : false;
            for (let i: number = 0; i < this.liElements.length; i++) {
                const textEle: HTMLElement = (this.liElements[parseInt(i.toString(), 10)].querySelector('.' + TEXTCSS));
                const iconOnly: boolean = (this.liElements[parseInt(i.toString(), 10)].classList.contains(STEPICON) && !this.liElements[parseInt(i.toString(), 10)].classList.contains(STEPTEXT) && !this.liElements[parseInt(i.toString(), 10)].classList.contains('e-step-label')) ? true : false;
                if (textEle) { this.textEleWidth = this.textEleWidth < textEle.offsetWidth ? textEle.offsetWidth : this.textEleWidth; }
                if (isBeforeLabel) {
                    let itemWidth: number;
                    const labelWidth: number = (this.liElements[parseInt(i.toString(), 10)].querySelector('.' + LABEL) as HTMLElement).offsetWidth + 15;
                    this.beforeLabelWidth = Math.max(this.beforeLabelWidth, labelWidth);
                    if ((this.element.querySelector('ol').lastChild as HTMLElement).querySelector('.' + ICONCSS)) { itemWidth = (this.beforeLabelWidth + ((this.liElements[parseInt(i.toString(), 10)].querySelector('.' + ICONCSS) as HTMLElement).offsetWidth / 2)); }
                    else if ((this.liElements[parseInt(i.toString(), 10)].querySelector('.' + TEXTCSS))) { itemWidth = (this.beforeLabelWidth + ((this.liElements[parseInt(i.toString(), 10)].querySelector('.' + TEXTCSS) as HTMLElement).offsetWidth / 2)); }
                    this.progressBarPosition = Math.max(this.progressBarPosition, itemWidth);
                } else if (this.progressBarPosition < (iconOnly ? (this.liElements[parseInt(i.toString(), 10)] as HTMLElement).offsetWidth : (this.element.querySelector('ol').lastChild.firstChild as HTMLElement).offsetWidth)) {
                    this.progressBarPosition = iconOnly ? (this.liElements[parseInt(i.toString(), 10)] as HTMLElement).offsetWidth : (this.element.querySelector('ol').lastChild.firstChild as HTMLElement).offsetWidth;
                }
            }
            const labelContainer: HTMLElement = (this.element.querySelector('li').querySelector('.' + STEPLABEL));
            if (this.element.classList.contains('e-label-bottom') || this.element.classList.contains('e-label-top')) {
                this.progressbar.style.setProperty('--progress-position', (this.stepperItemList.offsetWidth / 2) + 'px');
            }
            else { this.progressbar.style.setProperty('--progress-position', ((this.progressBarPosition / 2) - 1) + 'px'); }
            if (labelContainer && (labelContainer.classList.contains(LABELBEFORE))) {
                const listItems: NodeListOf<Element> = this.stepperItemList.querySelectorAll('.' + LABEL);
                for (let i: number = 0; i < listItems.length; i++) {
                    const labelEle: HTMLElement = listItems[parseInt((i).toString(), 10)] as HTMLElement;
                    labelEle.style.setProperty('--label-width', (this.beforeLabelWidth) + 'px');
                }
                this.progressbar.style.setProperty('--progress-position', (((this.progressBarPosition) - 1)) + 'px');
            }
        }
    }
    private updateStepperStatus(): void {
        for (let i: number = 0; i < this.liElements.length; i++) {
            if (this.stepperStatus && this.statusIndex === this.activeStep) {
                const itemElement: HTMLElement = this.liElements[parseInt(i.toString(), 10)];
                itemElement.classList.remove(SELECTED, INPROGRESS, COMPLETED, NOTSTARTED);
                this.updateStatusClass(i, this.statusIndex, itemElement, this.stepperStatus.toLowerCase() === 'completed' ? null : this.stepperStatus.toLowerCase() === 'inprogress');
            }
        }
    }
    private updateStatusClass(currentStep: number, index: number, ele: HTMLElement, isInprogress?: boolean): void {
        if (currentStep < index) { ele.classList.add(COMPLETED); }
        else if (currentStep === index) {
            if (isInprogress == null) { ele.classList.add(COMPLETED, SELECTED); }
            else if (isInprogress) { ele.classList.add(INPROGRESS, SELECTED); }
            else { ele.classList.add(NOTSTARTED); }
        }
        else { ele.classList.add(NOTSTARTED); }
    }
    private navigateToStep(index: number, isInteraction: boolean, e?: Event): void {
        if (isInteraction !== false) {
            const previousStep: number = this.activeStep;
            const stepperArgs: StepperChangingEventArgs = {
                cancel: false,
                isInteracted: true,
                previousStep: this.activeStep,
                activeStep: index,
                element: this.element,
                event: e
            };
            // eslint-disable-next-line @typescript-eslint/ban-ts-comment
            // @ts-ignore-start
            this.dotNetRef.invokeMethodAsync('StepChangingHandler', stepperArgs).then((stepArgs: StepperChangingEventArgs) => {
                // eslint-disable-next-line @typescript-eslint/ban-ts-comment
                // @ts-ignore-end
                if (!stepArgs.cancel) {
                    this.navigationHandler(index);
                    this.updateStepperStatus();
                    this.dotNetRef.invokeMethodAsync('StepChangedHandler', isInteraction, previousStep, this.activeStep);
                } else {
                    this.navigationHandler(this.activeStep);
                    this.updateStepperStatus();
                }
            });
        } else {
            this.navigationHandler(index);
            this.updateStepperStatus();
        }
    }
    private navigationHandler(index: number): void {
        if (index !== this.activeStep) {
            this.progressValue.style.transitionDuration = this.duration + 'ms';
        }
        index = (index >= this.liElements.length - 1) ? this.liElements.length - 1 : index;
        const Itemslength: number = this.liElements.length;
        if (index >= 0 && index < Itemslength) { index = this.liElements[parseInt(index.toString(), 10)].classList.contains(DISABLED) ?
            this.activeStep : index; }
        this.activeStep = index;
        for (let i: number = 0; i < this.liElements.length; i++) {
            const itemElement: HTMLElement = this.liElements[parseInt(i.toString(), 10)];
            itemElement.classList.remove(SELECTED, INPROGRESS, COMPLETED, NOTSTARTED);
            if (i === this.activeStep) { itemElement.classList.add(SELECTED); }
            if (this.linear) {
                itemElement.classList.toggle(PREVSTEP, (i === this.activeStep - 1));
                itemElement.classList.toggle(NEXTSTEP, (i === this.activeStep + 1));
            }
            if (this.activeStep >= 0 && this.progressValue) {
                if (this.element.classList.contains(HORIZSTEP)) { this.calculateProgressbarPos(); }
                else { this.progressValue.style.setProperty(PROGRESSVALUE, ((100 / (this.liElements.length - 1)) * index) + '%'); }
            }
            else if (this.activeStep < 0 && this.progressValue) { this.progressValue.style.setProperty(PROGRESSVALUE, 0 + '%'); }
            if (i === this.activeStep) { itemElement.classList.add(INPROGRESS); }
            else if (this.activeStep > 0 && i < this.activeStep) { itemElement.classList.add(COMPLETED); }
            else { itemElement.classList.add(NOTSTARTED); }
            if (itemElement.classList.contains(INPROGRESS)) { attributes(itemElement, { 'tabindex': '0', 'aria-current': 'true' }); }
            else { attributes(itemElement, { 'tabindex': '-1', 'aria-current': 'false' }); }
            if (this.element.classList.contains(STEPINDICATOR) && this.isDefaultStep && !itemElement.classList.contains('e-step-valid') && !itemElement.classList.contains('e-step-error')) {
                if (itemElement.classList.contains(COMPLETED)) {
                    (itemElement.firstChild as HTMLElement).classList.remove('e-icons', 'e-step-indicator');
                    (itemElement.firstChild as HTMLElement).classList.add(ICONCSS, 'e-icons', 'e-check');
                }
                else if (itemElement.classList.contains(INPROGRESS) || itemElement.classList.contains(NOTSTARTED)) {
                    (itemElement.firstChild as HTMLElement).classList.remove(ICONCSS, 'e-icons', 'e-check');
                    (itemElement.firstChild as HTMLElement).classList.add('e-icons', 'e-step-indicator');
                }
            }
        }
        this.progressValue.style.transitionDuration = '0ms';
    }

    private calculateProgressbarPos(): void {
        if ((this.element.classList.contains(LABELBEFORE) || this.element.classList.contains(LABELAFTER)) && !this.element.classList.contains('e-step-type-indicator') &&
            this.liElements[this.activeStep].classList.contains(STEPICON)) {
            const selectedEle: HTMLElement = this.liElements[this.activeStep].firstChild as HTMLElement;
            let value: number = this.activeStep === 0 ? 0 : (selectedEle.offsetLeft - this.progressbar.offsetLeft +
                (selectedEle.offsetWidth / 2)) / this.progressbar.offsetWidth * 100;
            if (this.element.classList.contains(RTL)) {
                value = (this.progressbar.getBoundingClientRect().right - selectedEle.getBoundingClientRect().right +
                (selectedEle.offsetWidth / 2)) / this.progressbar.offsetWidth * 100;
            }
            this.progressValue.style.setProperty(PROGRESSVALUE, (value) + '%');
        }
        else {
            let totalLiWidth: number = 0;
            let activeLiWidth: number = 0;
            for (let j: number = 0; j < this.liElements.length; j++) {
                totalLiWidth += this.liElements[parseInt(j.toString(), 10)].offsetWidth;
                if (j <= this.activeStep) {
                    activeLiWidth += (j < this.activeStep) ? this.liElements[parseInt(j.toString(), 10)].offsetWidth :
                        (j === this.activeStep && j !== 0 ) ? (this.liElements[parseInt(j.toString(), 10)].offsetWidth / 2) : 0;
                }
            }
            const spaceWidth: number = (this.stepperItemList.offsetWidth - totalLiWidth) / (this.liElements.length - 1);
            const progressValue: number = ((activeLiWidth +
                (spaceWidth * this.activeStep)) / this.stepperItemList.offsetWidth) * 100;
            this.progressValue.style.setProperty(PROGRESSVALUE, (progressValue) + '%');
        }
    }

    private keyActionHandler(e: KeyboardEvent): void {
        if (this.readOnly) { return; }
        switch (e.key) {
        case 'ArrowUp':
        case 'ArrowDown':
        case 'ArrowLeft':
        case 'ArrowRight':
        case 'Tab':
            this.handleNavigation(this.enableRtl && this.element.classList.contains(HORIZSTEP) ? (e.key === 'ArrowLeft' || (e.shiftKey && e.key === 'Tab') || e.key === 'ArrowUp') : (e.key === 'ArrowRight' || (e.key === 'Tab' && !e.shiftKey) || e.key === 'ArrowDown'), e);
            break;
        case ' ':
        case 'Enter':
        case 'Escape':
            this.handleNavigation(null, e);
            break;
        case 'Home':
        case 'End':
            this.handleNavigation(null, e, this.enableRtl);
            break;
        }
    }

    private handleNavigation(isNextStep: boolean, e: KeyboardEvent, isRTL?: boolean): void {
        this.isKeyNavFocus = true;
        this.element.classList.add('e-steps-focus');
        let focusedEle: HTMLElement = this.element.querySelector('.' + FOCUS);
        if (!focusedEle) { focusedEle = this.element.querySelector('.' + SELECTED); }
        const stepItems: HTMLElement[] = Array.prototype.slice.call(this.stepperItemList.children);
        let index: number = stepItems.indexOf(focusedEle);
        if (e.key === 'Tab' || e.key === 'ArrowDown' || e.key === 'ArrowUp' || e.key === ' ' || e.key === 'Home' || e.key === 'End') {
            if (((e.key === 'Tab' && !e.shiftKey) && index === stepItems.length - 1) || ((e.key === 'Tab' && e.shiftKey) && index === 0)) {
                if ((focusedEle as HTMLElement).classList.contains(FOCUS)) {
                    this.updateStepFocus();
                    return;
                }
            }
            else { e.preventDefault(); }
        }
        if (e.key === 'Escape') {
            stepItems[parseInt(index.toString(), 10)].classList.remove(FOCUS);
            this.element.classList.remove('e-steps-focus');
        }
        if (!(e.key === ' ' || e.key === 'Enter')) {
            const prevIndex: number = index;
            index = isNextStep ? index + 1 : index - 1;
            while ((index >= 0 && index < stepItems.length) && stepItems[parseInt(index.toString(), 10)].classList.contains(DISABLED)) {
                index = isNextStep ? index + 1 : index - 1;
            }
            index = (index < 0) ? 0 : (index > stepItems.length - 1) ? stepItems.length - 1 : index;
            if (stepItems[parseInt(prevIndex.toString(), 10)].classList.contains(FOCUS)) {
                stepItems[parseInt(prevIndex.toString(), 10)].classList.remove(FOCUS);
            }
            if ((e.key === 'Home' || e.key === 'End')) {
                index = e.key === 'Home' ? (isRTL ? stepItems.length - 1 : 0) : (isRTL ? 0 : stepItems.length - 1);
            }
            if (index >= 0 && index < stepItems.length) { stepItems[parseInt(index.toString(), 10)].classList.add(FOCUS); }
        }
        else if ((e.key === ' ' || e.key === 'Enter')) {
            let isupdateFocus: boolean = false;
            if (this.linear) {
                const linearModeValue: number = this.activeStep - index;
                if (Math.abs(linearModeValue) === 1) { this.navigateToStep(index, true, null); isupdateFocus = true; }
            }
            else { this.navigateToStep(index, true, null);  isupdateFocus = true; }
            if (isupdateFocus) {
                this.updateStepFocus();
                (this.liElements[parseInt(index.toString(), 10)] as HTMLElement).focus();
            }
        }
    }
    private updateLabelClass(showLabelClass: string): void {
        const removeCss: string[] = (this.element as HTMLElement).classList.value.match(/(e-label-[after|before|start|end|top|bottom]+)/g);
        if (removeCss) { removeClass([this.element as HTMLElement], removeCss); }
        (this.element as HTMLElement).classList.add(showLabelClass as string);
    }
    private updateStepLength(isAdd: boolean, stepCountDiff: number): void {
        const prevStepCount: number = this.liElements.length;
        if (!isAdd) {
            for (let i: number = prevStepCount - stepCountDiff; i < prevStepCount; i++) {
                this.unBindItemEvent(this.liElements[parseInt(i.toString(), 10)], i);
            }
        }
        this.getElements();
        if (isAdd) {
            for (let i: number = prevStepCount; i < this.liElements.length; i++) {
                this.bindItemEvent(this.liElements[parseInt(i.toString(), 10)], i);
            }
        }
        this.navigationHandler(this.activeStep);
    }
    public stepperPropsUpdate(options: { [key: string]: Object }): void {
        this.updateContext(options);
        this.getElements();
        if (options.showLabelClass) { this.updateLabelClass(options.showLabelClass as string); }
        if (options.stepNavigation) { this.navigateToStep(options.activeStep as number, true, null); }
    }
    public stepperPropsDynamicUpdate(options: { [key: string]: Object }): void {
        this.updateContext(options);
        this.getElements();
        if (options.showLabelClass) { this.updateLabelClass(options.showLabelClass as string); }
        this.renderProgressBar();
        this.navigateToStep(options.activeStep as number, true, null);
    }
    public destroy(): void {
        this.unBindEvent();
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        (window as any).sfBlazor.disposeWindowsInstance(this.dataId);
        this.element = null;
        this.progressValue = null;
        this.stepperItemList = null;
        this.progressbar = null;
        this.liElements = null;
    }
}
const Stepper: object = {
    initialize(options: { [key: string]: Object }): void {
        if (options.dataId) {
            if (options.showLabelClass) {
                (options.element as HTMLElement).classList.add(options.showLabelClass as string);
            }
            new SfStepper(options);
        }
    },
    updateStepperProps(options: { [key: string]: Object }): void {
        if (options.dataId) {
            const stepper: SfStepper = this.sfBlazor.getCompInstance(options.dataId);
            stepper.stepperPropsUpdate(options);
        }
    },
    updateDynamicStepperProps(options: { [key: string]: Object }): void {
        if (options.dataId) {
            const stepper: SfStepper = this.sfBlazor.getCompInstance(options.dataId);
            stepper.stepperPropsDynamicUpdate(options);
        }
    },
    updateStepperValue(dataId: string, activeStep: number, isInteraction: boolean): void {
        if (dataId) { this.sfBlazor.getCompInstance(dataId).navigateToStep(activeStep, isInteraction, null); }
    },
    updateLinear(options: { [key: string]: Object }): void {
        if (options.dataId) {
            this.sfBlazor.getCompInstance(options.dataId).updateContext(options);
        }
    },
    updateStepLength(dataId: string, isAdd: boolean, stepCountDiff: number): void {
        if (dataId && !isNullOrUndefined(this.sfBlazor.getCompInstance(dataId))) {
            this.sfBlazor.getCompInstance(dataId).updateStepLength(isAdd, stepCountDiff);
        }
    },
    refreshProgressbar(dataId: string, activeStep: number): void {
        if (dataId) {
            /* eslint-disable-next-line  @typescript-eslint/no-explicit-any */
            const stepper: any = this.sfBlazor.getCompInstance(dataId);
            stepper.renderProgressBar();
            stepper.navigateToStep(activeStep, false, null);
        }
    },
    destroy(dataId: string): void {
        if (dataId) { this.sfBlazor.getCompInstance(dataId).destroy(); }
    }
};

export default Stepper;
