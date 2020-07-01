/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, { enumerable: true, get: getter });
/******/ 		}
/******/ 	};
/******/
/******/ 	// define __esModule on exports
/******/ 	__webpack_require__.r = function(exports) {
/******/ 		if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 			Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 		}
/******/ 		Object.defineProperty(exports, '__esModule', { value: true });
/******/ 	};
/******/
/******/ 	// create a fake namespace object
/******/ 	// mode & 1: value is a module id, require it
/******/ 	// mode & 2: merge all properties of value into the ns
/******/ 	// mode & 4: return value when already ns object
/******/ 	// mode & 8|1: behave like require
/******/ 	__webpack_require__.t = function(value, mode) {
/******/ 		if(mode & 1) value = __webpack_require__(value);
/******/ 		if(mode & 8) return value;
/******/ 		if((mode & 4) && typeof value === 'object' && value && value.__esModule) return value;
/******/ 		var ns = Object.create(null);
/******/ 		__webpack_require__.r(ns);
/******/ 		Object.defineProperty(ns, 'default', { enumerable: true, value: value });
/******/ 		if(mode & 2 && typeof value != 'string') for(var key in value) __webpack_require__.d(ns, key, function(key) { return value[key]; }.bind(null, key));
/******/ 		return ns;
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = "./Blazored.Modal.ts");
/******/ })
/************************************************************************/
/******/ ({

/***/ "./Blazored.Modal.ts":
/*!***************************!*\
  !*** ./Blazored.Modal.ts ***!
  \***************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

exports.__esModule = true;
exports.BlazoredModal = void 0;
var FocusTrap_1 = __webpack_require__(/*! ./FocusTrap */ "./FocusTrap.ts");
var BlazoredModal = /** @class */ (function () {
    function BlazoredModal() {
    }
    /**
     * setFocusTrap
     */
    BlazoredModal.prototype.setFocusTrap = function (element) {
        var trap = new FocusTrap_1["default"](element, {});
        trap.activate({});
    };
    return BlazoredModal;
}());
exports.BlazoredModal = BlazoredModal;
window.BlazoredModal = new BlazoredModal();


/***/ }),

/***/ "./FocusTrap.ts":
/*!**********************!*\
  !*** ./FocusTrap.ts ***!
  \**********************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
exports.__esModule = true;
var tabbable_1 = __webpack_require__(/*! tabbable */ "./node_modules/tabbable/index.js");
var activeFocusDelay;
var activeFocusTraps = (function () {
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
            }
            else {
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
var FocusTrap = /** @class */ (function () {
    function FocusTrap(element, userOptions) {
        this.doc = document;
        this.container = typeof element === "string" ? this.doc.querySelector(element) : element;
        this.config = __assign({ returnFocusOnDeactivate: true, escapeDeactivates: true }, userOptions);
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
    FocusTrap.prototype.activate = function (activateOptions) {
        if (this.state.active)
            return;
        this.updateTabbableNodes();
        this.state.active = true;
        this.state.paused = false;
        this.state.nodeFocusedBeforeActivation = this.doc.activeElement;
        var onActivate = activateOptions && activateOptions.onActivate
            ? activateOptions.onActivate
            : this.config.onActivate;
        if (onActivate) {
            onActivate();
        }
        this.addListeners();
        return this.trap;
    };
    FocusTrap.prototype.deactivate = function (deactivateOptions) {
        if (!this.state.active)
            return;
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
    };
    FocusTrap.prototype.pause = function () {
        if (this.state.paused || !this.state.active)
            return;
        this.state.paused = true;
        this.removeListeners();
    };
    FocusTrap.prototype.unpause = function () {
        if (!this.state.paused || !this.state.active)
            return;
        this.state.paused = false;
        this.updateTabbableNodes();
        this.addListeners();
    };
    FocusTrap.prototype.addListeners = function () {
        if (!this.state.active)
            return;
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
    };
    FocusTrap.prototype.removeListeners = function () {
        if (!this.state.active)
            return;
        this.doc.removeEventListener('focusin', this.checkFocusIn, true);
        this.doc.removeEventListener('mousedown', this.checkPointerDown, true);
        this.doc.removeEventListener('touchstart', this.checkPointerDown, true);
        this.doc.removeEventListener('click', this.checkClick, true);
        this.doc.removeEventListener('keydown', this.checkKey, true);
        return this.trap;
    };
    FocusTrap.prototype.getNodeForOption = function (optionName) {
        var optionValue = this.config[optionName];
        var node = optionValue;
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
    };
    FocusTrap.prototype.getInitialFocusNode = function () {
        var node;
        if (this.getNodeForOption('initialFocus') !== null) {
            node = this.getNodeForOption('initialFocus');
        }
        else if (this.container.contains(this.doc.activeElement)) {
            node = this.doc.activeElement;
        }
        else {
            node = this.state.firstTabbableNode || this.getNodeForOption('fallbackFocus');
        }
        if (!node) {
            throw new Error('Your focus-trap needs to have at least one focusable element');
        }
        return node;
    };
    FocusTrap.prototype.getReturnFocusNode = function (previousActiveElement) {
        var node = this.getNodeForOption('setReturnFocus');
        return node ? node : previousActiveElement;
    };
    // This needs to be done on mousedown and touchstart instead of click
    // so that it precedes the focus event.
    FocusTrap.prototype.checkPointerDown = function (e) {
        if (this.container.contains(e.target))
            return;
        if (this.config.clickOutsideDeactivates) {
            this.deactivate({
                returnFocus: !tabbable_1["default"].isFocusable(e.target)
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
    };
    // In case focus escapes the trap for some strange reason, pull it back in.
    FocusTrap.prototype.checkFocusIn = function (e) {
        // In Firefox when you Tab out of an iframe the Document is briefly focused.
        if (this.container.contains(e.target) || e.target instanceof Document) {
            return;
        }
        e.stopImmediatePropagation();
        this.tryFocus(this.state.mostRecentlyFocusedNode || this.getInitialFocusNode());
    };
    FocusTrap.prototype.checkKey = function (e) {
        if (this.config.escapeDeactivates !== false && this.isEscapeEvent(e)) {
            e.preventDefault();
            this.deactivate();
            return;
        }
        if (this.isTabEvent(e)) {
            this.checkTab(e);
            return;
        }
    };
    // Hijack Tab events on the first and last focusable nodes of the trap,
    // in order to prevent focus from escaping. If it escapes for even a
    // moment it can end up scrolling the page and causing confusion so we
    // kind of need to capture the action at the keydown phase.
    FocusTrap.prototype.checkTab = function (e) {
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
    };
    FocusTrap.prototype.checkClick = function (e) {
        if (this.config.clickOutsideDeactivates)
            return;
        if (this.container.contains(e.target))
            return;
        if (this.config.allowOutsideClick && this.config.allowOutsideClick(e)) {
            return;
        }
        e.preventDefault();
        e.stopImmediatePropagation();
    };
    FocusTrap.prototype.updateTabbableNodes = function () {
        var tabbableNodes = tabbable_1["default"](this.container);
        this.state.firstTabbableNode = tabbableNodes[0] || this.getInitialFocusNode();
        this.state.lastTabbableNode = tabbableNodes[tabbableNodes.length - 1] || this.getInitialFocusNode();
    };
    FocusTrap.prototype.tryFocus = function (node) {
        if (node === this.doc.activeElement)
            return;
        if (!node || !node.focus) {
            this.tryFocus(this.getInitialFocusNode());
            return;
        }
        node.focus({ preventScroll: this.config.userOptions.preventScroll });
        this.state.mostRecentlyFocusedNode = node;
        if (this.isSelectableInput(node)) {
            node.select();
        }
    };
    FocusTrap.prototype.isSelectableInput = function (node) {
        return (node.tagName &&
            node.tagName.toLowerCase() === 'input' &&
            typeof node.select === 'function');
    };
    FocusTrap.prototype.isEscapeEvent = function (e) {
        return e.key === 'Escape' || e.key === 'Esc' || e.keyCode === 27;
    };
    FocusTrap.prototype.isTabEvent = function (e) {
        return e.key === 'Tab' || e.keyCode === 9;
    };
    FocusTrap.prototype.delay = function (fn) {
        return setTimeout(fn, 0);
    };
    return FocusTrap;
}());
exports["default"] = FocusTrap;


/***/ }),

/***/ "./node_modules/tabbable/index.js":
/*!****************************************!*\
  !*** ./node_modules/tabbable/index.js ***!
  \****************************************/
/*! no static exports found */
/***/ (function(module, exports) {

var candidateSelectors = [
  'input',
  'select',
  'textarea',
  'a[href]',
  'button',
  '[tabindex]',
  'audio[controls]',
  'video[controls]',
  '[contenteditable]:not([contenteditable="false"])',
];
var candidateSelector = candidateSelectors.join(',');

var matches = typeof Element === 'undefined'
  ? function () {}
  : Element.prototype.matches || Element.prototype.msMatchesSelector || Element.prototype.webkitMatchesSelector;

function tabbable(el, options) {
  options = options || {};

  var regularTabbables = [];
  var orderedTabbables = [];

  var candidates = el.querySelectorAll(candidateSelector);

  if (options.includeContainer) {
    if (matches.call(el, candidateSelector)) {
      candidates = Array.prototype.slice.apply(candidates);
      candidates.unshift(el);
    }
  }

  var i, candidate, candidateTabindex;
  for (i = 0; i < candidates.length; i++) {
    candidate = candidates[i];

    if (!isNodeMatchingSelectorTabbable(candidate)) continue;

    candidateTabindex = getTabindex(candidate);
    if (candidateTabindex === 0) {
      regularTabbables.push(candidate);
    } else {
      orderedTabbables.push({
        documentOrder: i,
        tabIndex: candidateTabindex,
        node: candidate,
      });
    }
  }

  var tabbableNodes = orderedTabbables
    .sort(sortOrderedTabbables)
    .map(function(a) { return a.node })
    .concat(regularTabbables);

  return tabbableNodes;
}

tabbable.isTabbable = isTabbable;
tabbable.isFocusable = isFocusable;

function isNodeMatchingSelectorTabbable(node) {
  if (
    !isNodeMatchingSelectorFocusable(node)
    || isNonTabbableRadio(node)
    || getTabindex(node) < 0
  ) {
    return false;
  }
  return true;
}

function isTabbable(node) {
  if (!node) throw new Error('No node provided');
  if (matches.call(node, candidateSelector) === false) return false;
  return isNodeMatchingSelectorTabbable(node);
}

function isNodeMatchingSelectorFocusable(node) {
  if (
    node.disabled
    || isHiddenInput(node)
    || isHidden(node)
  ) {
    return false;
  }
  return true;
}

var focusableCandidateSelector = candidateSelectors.concat('iframe').join(',');
function isFocusable(node) {
  if (!node) throw new Error('No node provided');
  if (matches.call(node, focusableCandidateSelector) === false) return false;
  return isNodeMatchingSelectorFocusable(node);
}

function getTabindex(node) {
  var tabindexAttr = parseInt(node.getAttribute('tabindex'), 10);
  if (!isNaN(tabindexAttr)) return tabindexAttr;
  // Browsers do not return `tabIndex` correctly for contentEditable nodes;
  // so if they don't have a tabindex attribute specifically set, assume it's 0.
  if (isContentEditable(node)) return 0;
  return node.tabIndex;
}

function sortOrderedTabbables(a, b) {
  return a.tabIndex === b.tabIndex ? a.documentOrder - b.documentOrder : a.tabIndex - b.tabIndex;
}

function isContentEditable(node) {
  return node.contentEditable === 'true';
}

function isInput(node) {
  return node.tagName === 'INPUT';
}

function isHiddenInput(node) {
  return isInput(node) && node.type === 'hidden';
}

function isRadio(node) {
  return isInput(node) && node.type === 'radio';
}

function isNonTabbableRadio(node) {
  return isRadio(node) && !isTabbableRadio(node);
}

function getCheckedRadio(nodes) {
  for (var i = 0; i < nodes.length; i++) {
    if (nodes[i].checked) {
      return nodes[i];
    }
  }
}

function isTabbableRadio(node) {
  if (!node.name) return true;
  // This won't account for the edge case where you have radio groups with the same
  // in separate forms on the same page.
  var radioSet = node.ownerDocument.querySelectorAll('input[type="radio"][name="' + node.name + '"]');
  var checked = getCheckedRadio(radioSet);
  return !checked || checked === node;
}

function isHidden(node) {
  // offsetParent being null will allow detecting cases where an element is invisible or inside an invisible element,
  // as long as the element does not use position: fixed. For them, their visibility has to be checked directly as well.
  return node.offsetParent === null || getComputedStyle(node).visibility === 'hidden';
}

module.exports = tabbable;


/***/ })

/******/ });
//# sourceMappingURL=blazored.modal.js.map