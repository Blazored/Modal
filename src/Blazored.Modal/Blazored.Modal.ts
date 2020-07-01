import focusTrap, { Options } from 'focus-trap';

export class BlazoredModal {

    public setFocusTrap(element: any): void {
        const options: Options = {
            escapeDeactivates: false
        };
        const trap = focusTrap(element, options);
        trap.activate();
    }
}

declare global {
    interface Window { BlazoredModal: BlazoredModal; }
}

window.BlazoredModal = new BlazoredModal();