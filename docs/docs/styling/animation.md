---
sidebar_position: 2
---

# Animation
By default, the modal is shown with a subtle fade in out animation. However, this can be disabled so the modal shows and hides immediately.

#### Configuring for all modals
```razor
<BlazoredModal AnimationType="@ModalAnimationType.None"/>
```


#### Configuring for a single modal
```csharp
var options = new ModalOptions() 
{ 
    AnimationType = ModalAnimationType.None 
};

Modal.Show<Confirm>("Are you sure?", options);
```
