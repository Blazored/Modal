---
sidebar_position: 4
---

# Custom Overlay
The overlay can be customised by providing one or more custom CSS classes to augment or overwrite the default style.

If you wanted the overlay to be a transparent red you could define the following CSS class.

```css title="my-app.css"
.custom-modal-overlay {
    background-color: rgba(255, 0, 0, 0.5);
}
```

Then apply that to your modal with either of the following options.

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