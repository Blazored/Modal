import tabbable from 'tabbable';

let activeFocusDelay;

const activeFocusTraps = (function () {
    var trapQueue = [];
    return {
        activateTrap: function (trap) {
            if (trapQueue.length > 0) {
                var activeTrap = trapQueue[trapQueue.length - 1];
                if (activeTrap !== trap) {
                    activeTrap.pause();
                }
            }

            var trapIndex = trapQueue.indexOf(trap);
            if (trapIndex === -1) {
                trapQueue.push(trap);
            } else {
                // move this existing trap to the front of the queue
                trapQueue.splice(trapIndex, 1);
                trapQueue.push(trap);
            }
        },

        deactivateTrap: function (trap) {
            var trapIndex = trapQueue.indexOf(trap);
            if (trapIndex !== -1) {
                trapQueue.splice(trapIndex, 1);
            }

            if (trapQueue.length > 0) {
                trapQueue[trapQueue.length - 1].unpause();
            }
        }
    };
})();

export default class FocusTrap {
    doc: Document;
    container: any;
    config: any;
    state: any;
    trap: any;

    constructor(element: any, userOptions: any) {
        this.doc = document;
        this.container = typeof element === "string" ? this.doc.querySelector(element) : element;
        this.config = {
            returnFocusOnDeactivate: true,
            escapeDeactivates: true,
            ...userOptions
        };
        this.state = {
            firstTabbableNode: null,
            lastTabbableNode: null,
            nodeFocusedBeforeActivation: null,
            mostRecentlyFocusedNode: null,
            active: false,
            paused: false
        };
        this.trap = {
            activate: this.activate,
            deactivate: this.deactivate,
            pause: this.pause,
            unpause: this.unpause
        };
    }

    activate(activateOptions): any {
        if (this.state.active) return;

        this.updateTabbableNodes();

        this.state.active = true;
        this.state.paused = false;
        this.state.nodeFocusedBeforeActivation = this.doc.activeElement;

        const onActivate =
            activateOptions && activateOptions.onActivate
                ? activateOptions.onActivate
                : this.config.onActivate;

        if (onActivate) {
            onActivate();
        }

        this.addListeners();

        return this.trap;
    }

    deactivate(deactivateOptions?: any): any {
        if (!this.state.active) return;

        clearTimeout(activeFocusDelay);

        this.removeListeners();
        this.state.active = false;
        this.state.paused = false;

        activeFocusTraps.deactivateTrap(this.trap);

        var onDeactivate = deactivateOptions && deactivateOptions.onDeactivate !== undefined
            ? deactivateOptions.onDeactivate
            : this.config.onDeactivate;

        if (onDeactivate) {
            onDeactivate();
        }

        var returnFocus = deactivateOptions && deactivateOptions.returnFocus !== undefined
            ? deactivateOptions.returnFocus
            : this.config.returnFocusOnDeactivate;

        if (returnFocus) {
            this.delay(function () {
                this.tryFocus(this.getReturnFocusNode(this.state.nodeFocusedBeforeActivation));
            });
        }

        return this.trap;
    }

    pause() {
        if (this.state.paused || !this.state.active) return;
        this.state.paused = true;
        this.removeListeners();
    }

    unpause() {
        if (!this.state.paused || !this.state.active) return;
        this.state.paused = false;
        this.updateTabbableNodes();
        this.addListeners();
    }

    private addListeners(): any {
        if (!this.state.active) return;

        // There can be only one listening focus trap at a time
        activeFocusTraps.activateTrap(this.trap);

        // Delay ensures that the focused element doesn't capture the event
        // that caused the focus trap activation.
        activeFocusDelay = this.delay(function () {
            this.tryFocus(this.getInitialFocusNode());
        });

        this.doc.addEventListener('focusin', this.checkFocusIn, true);
        this.doc.addEventListener('mousedown', this.checkPointerDown, {
            capture: true,
            passive: false
        });
        this.doc.addEventListener('touchstart', this.checkPointerDown, {
            capture: true,
            passive: false
        });
        this.doc.addEventListener('click', this.checkClick, {
            capture: true,
            passive: false
        });
        this.doc.addEventListener('keydown', this.checkKey, {
            capture: true,
            passive: false
        });

        return this.trap;
    }

    private removeListeners() {
        if (!this.state.active) return;

        this.doc.removeEventListener('focusin', this.checkFocusIn, true);
        this.doc.removeEventListener('mousedown', this.checkPointerDown, true);
        this.doc.removeEventListener('touchstart', this.checkPointerDown, true);
        this.doc.removeEventListener('click', this.checkClick, true);
        this.doc.removeEventListener('keydown', this.checkKey, true);

        return this.trap;
    }

    private getNodeForOption(optionName): any {
        const optionValue = this.config[optionName];
        let node = optionValue;

        if (!optionValue) {
            return null;
        }
        if (typeof optionValue === 'string') {
            node = this.doc.querySelector(optionValue);
            if (!node) {
                throw new Error('`' + optionName + '` refers to no known node');
            }
        }
        if (typeof optionValue === 'function') {
            node = optionValue();
            if (!node) {
                throw new Error('`' + optionName + '` did not return a node');
            }
        }
        return node;
    }

    private getInitialFocusNode() {
        let node;

        if (this.getNodeForOption('initialFocus') !== null) {
            node = this.getNodeForOption('initialFocus');
        } else if (this.container.contains(this.doc.activeElement)) {
            node = this.doc.activeElement;
        } else {
            node = this.state.firstTabbableNode || this.getNodeForOption('fallbackFocus');
        }

        if (!node) {
            throw new Error('Your focus-trap needs to have at least one focusable element');
        }

        return node;
    }

    private getReturnFocusNode(previousActiveElement): any {
        const node = this.getNodeForOption('setReturnFocus');

        return node ? node : previousActiveElement;
    }

    // This needs to be done on mousedown and touchstart instead of click
    // so that it precedes the focus event.
    private checkPointerDown(e): void {
        if (this.container.contains(e.target)) return;

        if (this.config.clickOutsideDeactivates) {
            this.deactivate({
                returnFocus: !tabbable.isFocusable(e.target)
            });

            return;
        }

        // This is needed for mobile devices.
        // (If we'll only let `click` events through,
        // then on mobile they will be blocked anyways if `touchstart` is blocked.)
        if (this.config.allowOutsideClick && this.config.allowOutsideClick(e)) {
            return;
        }

        e.preventDefault();
    }

    // In case focus escapes the trap for some strange reason, pull it back in.
    private checkFocusIn(e): void {
        // In Firefox when you Tab out of an iframe the Document is briefly focused.
        if (this.container.contains(e.target) || e.target instanceof Document) {
            return;
        }

        e.stopImmediatePropagation();
        this.tryFocus(this.state.mostRecentlyFocusedNode || this.getInitialFocusNode());
    }

    private checkKey(e): void {
        if (this.config.escapeDeactivates !== false && this.isEscapeEvent(e)) {
            e.preventDefault();
            this.deactivate();

            return;
        }
        if (this.isTabEvent(e)) {
            this.checkTab(e);

            return;
        }
    }

    // Hijack Tab events on the first and last focusable nodes of the trap,
    // in order to prevent focus from escaping. If it escapes for even a
    // moment it can end up scrolling the page and causing confusion so we
    // kind of need to capture the action at the keydown phase.
    private checkTab(e): void {
        this.updateTabbableNodes();
        if (e.shiftKey && e.target === this.state.firstTabbableNode) {
            e.preventDefault();
            this.tryFocus(this.state.lastTabbableNode);

            return;
        }
        if (!e.shiftKey && e.target === this.state.lastTabbableNode) {
            e.preventDefault();
            this.tryFocus(this.state.firstTabbableNode);

            return;
        }
    }

    private checkClick(e): void {
        if (this.config.clickOutsideDeactivates) return;
        if (this.container.contains(e.target)) return;
        if (this.config.allowOutsideClick && this.config.allowOutsideClick(e)) {
            return;
        }

        e.preventDefault();
        e.stopImmediatePropagation();
    }

    private updateTabbableNodes(): void {
        const tabbableNodes = tabbable(this.container);

        this.state.firstTabbableNode = tabbableNodes[0] || this.getInitialFocusNode();
        this.state.lastTabbableNode = tabbableNodes[tabbableNodes.length - 1] || this.getInitialFocusNode();
    }

    private tryFocus(node): void {
        if (node === this.doc.activeElement) return;

        if (!node || !node.focus) {
            this.tryFocus(this.getInitialFocusNode());
            return;
        }

        node.focus({ preventScroll: this.config.userOptions.preventScroll });

        this.state.mostRecentlyFocusedNode = node;

        if (this.isSelectableInput(node)) {
            node.select();
        }
    }

    private isSelectableInput(node): boolean {
        return (
            node.tagName &&
            node.tagName.toLowerCase() === 'input' &&
            typeof node.select === 'function'
        );
    }

    private isEscapeEvent(e): boolean {
        return e.key === 'Escape' || e.key === 'Esc' || e.keyCode === 27;
    }

    private isTabEvent(e): boolean {
        return e.key === 'Tab' || e.keyCode === 9;
    }

    private delay(fn: Function): number {
        return setTimeout(fn, 0);
    }
}
