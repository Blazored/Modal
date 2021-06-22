import focusTrap, { Options, FocusTrap } from 'focus-trap';

interface FocusTrapInstance {
    id: string;
    focusTrap: FocusTrap;
}

interface ModalInstance {
    id: string;
}

export class BlazoredModal {
    
    readonly _options: Options = { escapeDeactivates: false, allowOutsideClick: () => true };
    private _traps: Array<FocusTrapInstance> = [];
    private _modals: Array<ModalInstance> = [];

    public activateScrollLock(id: string): void {
        const scrollY = window.scrollY;
        const body = document.body;
        body.style.width = `${body.offsetWidth}px`;
        body.style.position = 'fixed';
        body.style.top = `-${scrollY}px`;
        this._modals.push({ id });
    }

    public activateFocusTrap(element: any, id: string): void {
        const trap = focusTrap(element, this._options);

        try {
            trap.activate();
            this._traps.push({ id, focusTrap: trap });
        } catch (e) {
            if (e instanceof Error && e.message === 'Your focus-trap needs to have at least one focusable element') {
                console.log('Focus trap not activated - No focusable elements found.');
            }
        }
    }

    public deactivateFocusTrap(id: string): void {
        const trap = this._traps.find(i => i.id === id);
        const scrollY = document.body.style.top;
        if (this._modals.length == 1) {
            document.body.style.position = '';
            document.body.style.top = '';
            document.body.style.width = '';
        }
        window.scrollTo(0, parseInt(scrollY || '0') * -1);
        const currentModal = this._modals.find(i => i.id === id);
        if (currentModal) {
            const index = this._modals.indexOf(currentModal);
            if (index > -1) {
                this._modals.splice(index, 1);
            }
        }
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