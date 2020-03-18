# Class
How to customise the look of modals.

---

Blazored Modal comes with a default look and feel which is deliberately plain. If you want to customise how the modal looks to match the rest of your application you can set a top level class and then use CSS selectors to add custom styles. 

## Setting Class Globally

To set a global class for all modals in your application, you can set the `Class` parameter on the `BlazoredModal` component in your `MainLayout`.

```html
@inherits LayoutComponentBase

<BlazoredModal Class="my-custom-class" />

<!-- Other code removed for brevity -->
```

## Setting Class Per Modal

To set the class of a specific modal instance you can pass in a `ModalOptions` object when calling `Show`.

```csharp
var options = new ModalOptions { Class = "my-custom-class" };

_ = Modal.Show<Confirm>("My Modal", options);
```

