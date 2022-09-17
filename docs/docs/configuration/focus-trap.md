---
sidebar_position: 6
---

# Focus Trap
Blazored Modal comes with a built-in focus trap. Focus traps are an important feature for accessability as they stop focus dropping behind the modal when pressing the *tab* key.

If you do wish to disable this feature, you can do so using the `UseFocusTrap` option and setting it to `false`.

#### Configuring for all modals
```razor
<CascadingBlazoredModal UseFocusTrap="false" />
```

#### Configuring for a single modal

```csharp
var options = new ModalOptions() 
{ 
    UseFocusTrap = false 
};

Modal.Show<Confirm>("Are you sure?", options);
```