---
sidebar_position: 1
---

# Modal Reference
When you open a modal you can capture a reference to it and await the result of that modal. This is useful when you want to perform an action when a modal is closed or cancelled.

```razor
@page "/movies"

<h1>Movies</h1>

<button @onclick="ShowModal">View Movies</button>

@code {

    [CascadingParameter] IModalService Modal { get; set; } = default!;

    private async Task ShowModal()
    {
        var moviesModal = Modal.Show<Movies>("My Movies");
        var result = await moviesModal.Result;

        if (result.Cancelled)
        {
            Console.WriteLine("Modal was cancelled");
        }
        else if (result.Confirmed)
        {
            Console.WriteLine("Modal was closed");
        }
    }
}
```