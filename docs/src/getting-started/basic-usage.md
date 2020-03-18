# Basic Usage
Simple example of creating a modal

---

```html
@page "/"
@inject IModalService Modal

<h1>Blazored Modal Sample</h1>

<hr class="mb-5" />

<p>
    This is an example of using Blazored Modal in its most simplistic form.
</p>

<button @onclick="ShowModal" class="btn btn-primary">Show Modal</button>

@code {

    void ShowModal() => Modal.Show<Confirm>("Welcome to Blazored Modal");

}
```