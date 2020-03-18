# Hide Header
How to control visibility of the modal header.

---

By default, the modal has a header which displays the title you specify when you call `Show`. However, you can choose to hide this header to give the modal a more compact look.  Just set the `HideHeader` option to `true`. 

## Setting HideHeader Globally

To control the visibility of the header globally for all modals in your application, you can set the `HideHeader` parameter on the `BlazoredModal` component in your `MainLayout`.

```html
@inherits LayoutComponentBase

<BlazoredModal HideHeader="true" />

<!-- Other code removed for brevity -->
```

## Setting HideHeader Per Modal

To set the visibility of the header for a specific modal instance you can pass in a `ModalOptions` object when calling `Show`.

```csharp
var options = new ModalOptions { HideHeader = true };

_ = Modal.Show<Confirm>("My Modal", options);
```
