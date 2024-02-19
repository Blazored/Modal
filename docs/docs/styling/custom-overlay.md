---
sidebar_position: 4
---

# Custom Overlay
The overlay can be customised by providing one or more custom CSS classes to augment or overwrite the default style. Note that you may need to add the `!important` keyword to some properties due the the use of scoped CSS, which can create a higher specificity for the default styles.

The following example shows how to define a CSS class that overrides the default overlay background colour.

```css title="my-app.css"
.custom-modal-overlay {
    background-color: rgba(255, 0, 0, 0.5) !important;
}
```

This can then be applied to your modal with either of the following options.

#### Configuring for all modals
```razor
<CascadingBlazoredModal OverlayCustomClass="custom-modal-overlay" />
```

#### Configuring for a single modal

```csharp
var options = new ModalOptions() 
{ 
    OverlayCustomClass = "custom-modal-overlay" 
};

Modal.Show<Confirm>("Are you sure?", options);
```