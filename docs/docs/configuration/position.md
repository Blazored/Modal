---
sidebar_position: 2
---

# Position

Modals are shown in the center of the screen near the top of the viewport. This can be customised via the `Position` option using the `ModalPosition` enum. The options available are:

- Top Left
- Top Right
- Top Center
- Middle
- Bottom Left
- Bottom Right
- Custom

### Using a predefined position
To configure Blazored Modal to use a predefined position, see the code examples below.


#### Configuring for all modals
```razor
<CascadingBlazoredModal Position="ModalPosition.TopLeft" />
```

#### Configuring for a single modal

```csharp
var options = new ModalOptions() 
{ 
    Position = ModalPosition.TopLeft 
};

Modal.Show<Confirm>("Are you sure?", options);
```

### Using a custom position
When using the `Custom` position setting, you will need to provide a CSS class, via the `PositionCustomClass` option, that sets the modal position. 

#### Configuring for all modals

```razor
<CascadingBlazoredModal Position="ModalPosition.Custom" PositionCustomClass="custom-modal-position" />
```

#### Configuring for a single modal

```csharp
var options = new ModalOptions() 
{ 
    Position = ModalPosition.Custom,
    PositionCustomClass = "custom-modal-position"
};

Modal.Show<Confirm>("Are you sure?", options);
```