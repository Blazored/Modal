---
sidebar_position: 2
---

# Passing Data
Data can be passed to a modal by using the `ModalParameters` object. The items you add to this collection must match the parameters defined on the component being displayed in the modal. Let's look at an example.

The following component is going to be displayed using Blazored Modal. It defines a `Message` parameter, which is then displayed in a `p`.

```razor title="DisplayMessage.razor"
<div>
    <p>@Message</p>

    <button @onclick="Close">Close</button>
</div>

@code {
    [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

    [Parameter] public string? Message { get; set; }

    private async Task Close() => await BlazoredModal.CloseAsync();
}
```

In order to pass a value to the `Message` parameter of the `DisplayMessage` component, we do the following.

```csharp
var parameters = new ModalParameters()
    .Add(nameof(DisplayMessage.Message), "Hello, World!");

Modal.Show<DisplayMessage>("Passing Data", parameters);
```

Note the use of `nameof`. Although you can define the parameter name as a string, using `nameof` gives strong typing.