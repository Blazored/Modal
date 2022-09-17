---
sidebar_position: 3
---

# Custom Layout
Blazored Modal provides a default look, this includes an overlay that covers the viewport and the default modal styling. However, you can remove all of this using the custom layout option. When set to `true` all of the default UI is removed and you become responsible for providing an alternative one. This option allows complete control over the look and feel of the modal.

An example usecase would be an application that used the bootstrap UI library and wanted the modal and overlay to match it's design. This could be achieved in the following way. 

Define a component to be displayed by Blazored Modal that uses Bootstraps HTML structure and CSS classes.

```razor title="BootstrapModal.razor"
<div class="modal fade show d-block" tabindex="-1" role="dialog">
    <div class="modal-backdrop fade show" @onclick="Cancel"></div>
    <div class="modal-dialog" style="z-index: 1050">
        <!-- Pop it above the backdrop -->
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Modal title</h5>
                <button type="button" class="close" aria-label="Close" @onclick="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <p>@Message</p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" @onclick="Close">Close</button>
            </div>
        </div>
    </div>
</div>

@code {
    [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

    [Parameter] public string? Message { get; set; }

    private async Task Close() => await BlazoredModal.CloseAsync(ModalResult.Ok(true));
    private async Task Cancel() => await BlazoredModal.CancelAsync();
}
```

Show the `BootstrapModal` component using Blazored Modal, setting `CustomLayout` to `true`.

```csharp
var options = new ModalOptions
{ 
    UseCustomLayout = true 
};

var parameters = new ModalParameters()
    .Add(nameof(BootstrapModal.Message), "Hello Bootstrap modal!!");

Modal.Show<BootstrapModal>("Bootstrap Modal", parameters, options);
```