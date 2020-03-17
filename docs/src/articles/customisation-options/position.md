# Position
How to customise where modals are displayed.

---

By default, modals are shown in the center of the viewport. However, you can customise where modals are shown using the `ModalOptions.Position` setting. The available options are `Center`, `TopLeft`, `TopRight`, `BottomLeft` and `BottomRight`. You can define this setting both at a global level, to effect all modals, or on a modal by modal basis. 

## Setting Position Globally

To set a global default position for all modals in your application, you can set the `Position` parameter on the `BlazoredModal` component in your `MainLayout`.

```html
@inherits LayoutComponentBase

<BlazoredModal Position="ModalPosition.BottomRight" />

<!-- Other code removed for brevity -->
```

## Setting Position Per Modal

To set the position of a specfic modal instance you can pass in a `ModalOptions` object when calling `Show`.

```csharp
var options = new ModalOptions { Position = ModalPosition.TopRight };

_ = Modal.Show<Confirm>("My Modal", options);
```

