---
sidebar_position: 4
---

# Closing Programmatically

While most modals will be dismissed by a user via buttons on the UI, sometimes you may want to dismiss a modal programmatically. An example of such a usecase is opening a modal with a loading spinner while data is being loaded from a UI or other long running process.

Closing a modal programmatically is done using the `IModalReference.Close()` method. In the following example, a `Spinner` component is shown in the modal while a long running task is being awaited. Once the long running task completes, the modal is closed programmatically.

```csharp
[CascadingParameter] IModalService Modal { get; set; } = default!;

private async Task ShowSpinner()
{
    var spinnerModal = Modal.Show<Spinner>();

    await MyLongRunningTask();

    spinnerModal.Close();
}
```