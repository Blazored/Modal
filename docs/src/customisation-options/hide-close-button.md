# Hide Close Button
How to control visibility of the close button.

---

By default, a close button is shown in the top right of the modal. If you prefer, the button can be hidden and you can handle closing the modal yourself by setting `HideCloseButton` to `true`.

## Setting HideCloseButton Globally

To control the visibility of the close button globally for all modals in your application, you can set the `HideCloseButton` parameter on the `BlazoredModal` component in your `MainLayout`.

```html
@inherits LayoutComponentBase

<BlazoredModal HideCloseButton="true" />

<!-- Other code removed for brevity -->
```

## Setting HideCloseButton Per Modal

To set the visibility of the close button for a specific modal instance you can pass in a `ModalOptions` object when calling `Show`.

```csharp
var options = new ModalOptions { HideCloseButton = true };

_ = Modal.Show<Confirm>("My Modal", options);
```
