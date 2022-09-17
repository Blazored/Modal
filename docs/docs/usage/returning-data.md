---
sidebar_position: 3
---

# Returning Data

Data can be returned from a modal by using the `ModalResult.Data` property. You can return simple strings as well as complex objects. 

In the example below, a message can be entered into the `MessageForm` component and when the form is submitted, the text entered is returned to the calling component in the modal result object.

```razor title="MessageForm.razor"
<div>
    <EditForm Model="_form" OnValidSubmit="SubmitForm">
        <label for="message">Message</label>
        <InputText @bind-Value="_form.Message" />

        <button type="submit">Submit</button>
        <button @onclick="Cancel">Cancel</button>
    </EditForm>
</div>

@code {
    private readonly Form _form = new();

    [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

    protected override void OnInitialized() => BlazoredModal.SetTitle("Enter a Message");

    private async Task SubmitForm() => await BlazoredModal.CloseAsync(ModalResult.Ok(_form.Message));
    private async Task Cancel() => await BlazoredModal.CancelAsync();
}
```

The component that invoked the modal can access the message by awaiting the result of the modal and then accessing the `result.Data` property.

```csharp
var messageForm = Modal.Show<MessageForm>();
var result = await messageForm.Result;

if (result.Confirmed)
{
    _message = result.Data.ToString();
}
```