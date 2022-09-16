---
slug: /size
title: Size
sidebar_position: 1
---

# Size

The width of a modal can be configured using the `Size` option. Blazored Modal has a range of built-in sizes as well as the ability to define a custom size.

- Small (300px)
- Medium (500px)
- Large (800px)
- Extra Large (1140px)
- Custom

If no `Size` option is specified the default size is `Medium`.

### Configuring Size for all modals
To set the `Size` for all modals in your application, you can use the `Size` parameter on the `CascadingBlazoredModal` component. 

```html
<CascadingBlazoredModal Size="ModalSize.Large" />
```

### Configuring Size for a single modal
To set the `Size` for a single modal, use the `ModalOptions` type and pass it into the `Show` method.

```csharp
var options = new ModalOptions() { Size = ModalSize.Large };
Modal.Show<Confirm>("My Title", options);
```