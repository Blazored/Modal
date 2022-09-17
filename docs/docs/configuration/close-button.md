---
sidebar_position: 3
---

# Close Button

By default, modals will display a close button in the top right corner. However, if you prefer, you can remove the close button using the `HideCloseButton` option.

:::info

If you're hiding the header using the [`HideHeader`](./hide-header) option, the `HideCloseButton` option will have no effect.

:::

### Configuring for all modals
```razor
<CascadingBlazoredModal HideCloseButton="true" />
```

### Configuring for a single modal

```csharp
var options = new ModalOptions() 
{ 
    HideCloseButton = true 
};

Modal.Show<Confirm>("Are you sure?", options);
```