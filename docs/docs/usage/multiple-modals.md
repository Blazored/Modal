---
sidebar_position: 5
---

# Multiple Modals
It's possible to show multiple modals at the same time, however, each new modal needs to be shown from the currently active modal.

In the following example, `ModalOne` is displayed from the `Homepage` component.

```razor title="Homepage.razor"
<button @onclick="ShowModal">Show Modal</button>

@code {
    [CascadingParameter] IModalService Modal { get; set; } = default!;

    private void ShowModal() => Modal.Show<ModalOne>("First Modal");
}
```

`ModalOne` then has two buttons *Show Modal Two* and *Close*. Clicking *Show Modal Two* will then create a second modal, `ModalTwo`, which renders on top of `ModalOne`. 

```razor title="ModalOne.razor"
<h1>Modal One</h1>

<button @onclick="ShowModalTwo">Show Modal Two</button>
<button @onclick="Close">Close</button>

@code {
    [CascadingParameter] BlazoredModalInstance ModalOne { get; set; } = default!;
    [CascadingParameter] IModalService Modal { get; set; } = default!;

    private async Task ShowModalTwo()
    {
        var modalTwo = ModalService.Show<ModalTwo>("Second Modal");
        _ = await modalTwo.Result;

        await ModalOne.CloseAsync();
    }

    private async Task Close() => await ModalOne.CloseAsync();
}
```

```csharp title="ModalTwo.razor"
<h1>Modal Two</h1>

<button @onclick="Close">Close</button>

@code {
    [CascadingParameter] BlazoredModalInstance ModalTwo { get; set; } = default!;

    private async Task Close() => await ModalTwo.CloseAsync();
}
```