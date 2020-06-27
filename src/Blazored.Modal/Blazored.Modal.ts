import 'focus-trap';
import focusTrap from './FocusTrap.js';

export class BlazoredModal {

    /**
     * setFocusTrap
     */
    public setFocusTrap(element:any):void {
        const trap = focusTrap(element, {});
        trap.activate({});
    }

}

declare global {
    interface Window { BlazoredModal: BlazoredModal; }
}

window.BlazoredModal = new BlazoredModal();