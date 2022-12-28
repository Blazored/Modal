---
sidebar_position: 1
---

# Modal Style
If you want to change to default look of the modal you can pass in your own CSS classes which will replace the default style classes that are applied. This allows you complete control over the look of the modal. 

#### Configuring for all modals
```razor
<CascadingBlazoredModal Class="my-custom-modal-class" />
```


#### Configuring for a single modal
```csharp
var options = new ModalOptions() 
{ 
    Class = "my-custom-modal-class" 
};

Modal.Show<Confirm>("Are you sure?", options);
```

