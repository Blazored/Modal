---
sidebar_position: 4
---

# Hide Header

When displaying a modal, a header is rendered showing the title of the modal and the close button. However, if you're planning to render your own header as part of the component being displayed, or you just don't want a header, you can turn it off.

### Configuring for all modals
```razor
<CascadingBlazoredModal HideHeader="true" />
```

### Configuring for a single modal

```csharp
var options = new ModalOptions() 
{ 
    HideHeader = true 
};

Modal.Show<Confirm>("Are you sure?", options);
```