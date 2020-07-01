import FocusTrap from './FocusTrap';

export class BlazoredModal {

    /**
     * setFocusTrap
     */
    public setFocusTrap(element:any):void {
        const trap = new FocusTrap(element, {});
        trap.activate({});
    }

}

declare global {
    interface Window { BlazoredModal: BlazoredModal; }
}

window.BlazoredModal = new BlazoredModal();