# Passing Data To Modals
How to pass data into the component being displayed in the modal.

---

Data can be passed to a component displayed in a modal by using the `ModalParameters` class. Once you create an instance, you can add values to it via the `Add` method which takes two arguments, `parameterName` and `value`. The `parameterName` must match the name of a parameter on the component being displayed in the modal. 

Lets look at an example. 

If we wanted to display a modal containing the following `DisplayMessage` component.

```html
<div>

    <p>@Message</p>

    <button @onclick="SubmitForm" class="btn btn-primary">Submit</button>
    <button @onclick="Cancel" class="btn btn-secondary">Cancel</button>
</div>

@code {

    [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; }
    
    [Parameter] public string Message { get; set; }

    void SubmitForm() => BlazoredModal.Close();
    void Cancel() => BlazoredModal.Close();

}
```

We can pass a value for the `Message` parameter like this.

```html
<div>
    <label>Enter a message</label>
    <input type="text" @bind="_message" />
</div>

<button @onclick="ShowModal">Show Modal</button>

@code {

    string _message;

    async Task ShowModal()
    {
        var parameters = new ModalParameters();
        parameters.Add(nameof(DisplayMessage.Message), _message);

        _ = Modal.Show<DisplayMessage>("Passing Data", parameters);
    }

}
```

**Note:** The use of `nameof` is recommended, where possible, to give a bit of compile time help when referencing parameters on components.