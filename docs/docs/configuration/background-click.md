---
sidebar_position: 5
---

# Background Click
By default, a modal is cancelled if the user clicks anywhere outside the modal. This behavior can be disabled by setting `DisableBackgroundCancel` option to `true`.

#### Configuring for all modals
```razor
<CascadingBlazoredModal DisableBackgroundCancel="true" />
```

#### Configuring for a single modal

```csharp
var options = new ModalOptions() 
{ 
    DisableBackgroundCancel = true 
};

Modal.Show<Confirm>("Are you sure?", options);
```