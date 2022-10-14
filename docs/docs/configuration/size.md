---
sidebar_position: 1
---

# Size

The width of a modal can be configured using the `Size` option. Blazored Modal has a range of built-in sizes as well as the ability to define a custom size.

- Automatic (Size is determined by the content)
- Small (300px)
- Medium (500px)
- Large (800px)
- Extra Large (1140px)
- Custom

If no `Size` option is specified the default size is `Medium`.

### Using a predefined size
To configure Blazored Modal to use a predefined size, see the code examples below.

#### Configuring for all modals
To set the `Size` for all modals in your application, you can use the `Size` parameter on the `CascadingBlazoredModal` component.

```razor
<CascadingBlazoredModal Size="ModalSize.Large" />
```

#### Configuring for a single modal
To set the `Size` for a single modal, use the `ModalOptions` type and pass it into the `Show` method.

```csharp
var options = new ModalOptions() 
{ 
    Size = ModalSize.Large 
};

Modal.Show<Confirm>("Are you sure?", options);
```

### Using a custom size
If you don't want to use one of the default sizes provided out-of-the-box, you can set the `Size` to `ModalSize.Custom`. When using the `Custom` option, you will need to provide a CSS class which sets the size of the modal. Do this using the `SizeCustomClass` option. 

#### Configuring for all modals
To set a custom size for all modals in your application, use the `CascadingBlazoredModal` component.

```razor
<CascadingBlazoredModal Size="ModalSize.Custom" SizeCustomClass="custom-modal" />
```

#### Configuring for a single modal
To set a custom size for a single modal, use the `ModalOptions` type and pass it into the `Show` method.

```csharp
var options = new ModalOptions() 
{ 
    Size = ModalSize.Custom,
    SizeCustomClass = "custom-modal"
};

Modal.Show<Confirm>("Are you sure?", options);
```