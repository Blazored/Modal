# Returning Data From Modals
How to return data from the component being displayed in the modal.

---

Data can be returned from a modal by using the `ModalResult.Data` property. You can return simple strings as well as complex objects. In the example below, you can add a message in the modal that will be show here when you close the modal.

```html
<button @onclick="ShowModal">Show Modal</button>

@if (!string.IsNullOrWhiteSpace(_message))
{
    <p><strong>Your message was:</strong></p>
    <p>@_message</p>
}

@code {

    string _message;

    async Task ShowModal()
    {
        var messageForm = Modal.Show<MessageForm>();
        var result = await messageForm.Result;

        if (!result.Cancelled)
            _message = result.Data.ToString();
    }

}
```