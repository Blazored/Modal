# Disable Background Cancel
How to control the background cancel feature.

---

By default, a modal is cancelled if the user clicks anywhere outside the modal. This behaviour can be disabled by setting `DisableBackgroundCancel` to `true`.

## Setting DisableBackgroundCancel Globally

To control background cancel globally for all modals in your application, you can set the `DisableBackgroundCancel` parameter on the `BlazoredModal` component in your `MainLayout`.

```html
@inherits LayoutComponentBase

<BlazoredModal DisableBackgroundCancel="true" />

<!-- Other code removed for brevity -->
```

## Setting DisableBackgroundCancel Per Modal

To set the background cancel of a specific modal instance you can pass in a `ModalOptions` object when calling `Show`.

```csharp
var options = new ModalOptions { DisableBackgroundCancel = true };

_ = Modal.Show<Confirm>("My Modal", options);
```
