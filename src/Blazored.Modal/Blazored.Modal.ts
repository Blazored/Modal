import focusTrap, { Options, FocusTrap } from 'focus-trap';

interface FocusTrapInstance {
    id: string;
    focusTrap: FocusTrap;
}

export class BlazoredModal {

    readonly _options: Options = { escapeDeactivates: false };
    private _traps: Array<FocusTrapInstance> = [];

    public activateFocusTrap(element: any, id: string): void {
        const trap = focusTrap(element, this._options);
        trap.activate();
        this._traps.push({ id, focusTrap: trap });
    }

    public deactivateFocusTrap(id: string): void {
        const trap = this._traps.find(i => i.id === id);

        if (trap) {
            trap.focusTrap.deactivate();

            const index = this._traps.indexOf(trap);
            if (index > -1) {
                this._traps.splice(index, 1);
            }
        }
    }
}

declare global {
    interface Window { BlazoredModal: BlazoredModal; }
}

window.BlazoredModal = new BlazoredModal();