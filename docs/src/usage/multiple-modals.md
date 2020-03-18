# Multiple Modals
How to show multiple modals at once.

---

It is possible to show multiple modals at the same time. Each new modal needs to be shown from the currently active modal. In this example an inital confirmation modal is shown, if you select *Yes* then a second confirmation modal is shown. When you `Close` the second modal both modals will be closed. If you `Cancel` the second modal, only the second modal will be closed.

```html
<button @onclick="ShowModal" class="btn btn-primary">Show Modal</button>

@code {

    void ShowModal() => Modal.Show<YesNoPrompt>("First Modal");

}
```

```html
<div>
    <p>Are you sure you want to delete the record?</p>

    <button @onclick="Yes" class="btn btn-outline-danger">Yes</button>
    <button @onclick="No" class="btn btn-primary">No</button>
</div>

@code {

    [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; }

    async Task Yes()
    {
        var confirmationModal = ModalService.Show<Confirm>("Second Modal");
        var result = await confirmationModal.Result;

        if (result.Cancelled)
            return;

        BlazoredModal.Close();
    }

    void No() => BlazoredModal.Cancel();

}
```

```html
<div>
    <p>Please click one of the buttons below to close or cancel the modal.</p>

    <button @onclick="Close" class="btn btn-primary">Close</button>
    <button @onclick="Cancel" class="btn btn-secondary">Cancel</button>
</div>

@code {

    [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; }

    void Close() => BlazoredModal.Close(ModalResult.Ok(true));
    void Cancel() => BlazoredModal.Close(ModalResult.Cancel());

}
```