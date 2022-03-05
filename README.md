# Blazored Modal

A powerful and customizable modal implementation for [Blazor](https://blazor.net) applications.

[![Build Status](https://github.com/Blazored/Modal/workflows/Build%20&%20Test%20Main/badge.svg)](https://github.com/Blazored/Modal/actions?query=workflow%3A%22Build+%26+Test+Main%22)
[![Nuget](https://img.shields.io/nuget/v/blazored.modal.svg)](https://www.nuget.org/packages/Blazored.Modal/)

![Screenshot of the component in action](screenshot.png)

## Getting Setup

You can install the package via the nuget package manager just search for *Blazored.Modal*. You can also install via powershell using the following command.

```powershell
Install-Package Blazored.Modal
```

Or via the dotnet CLI.

```bash
dotnet add package Blazored.Modal
```

#### Internet Explorer 11
This package can be used with Internet Explorer 11, but some special care should to be taken.

- Only Blazor Server works with IE11. Blazor WebAssembly does not work with any IE version. See [this](https://docs.microsoft.com/en-us/aspnet/core/blazor/supported-platforms?view=aspnetcore-3.1)
- A [polyfill](https://github.com/Daddoon/Blazor.Polyfill) is necessary for this component to work. See [this](https://github.com/Daddoon/Blazor.Polyfill) page for an explanation on how to install and use it. The sample project for Blazor Server uses the polyfill and thus should work on IE11
- V6.0.1 or higher of `Blazored.Modal` should be used

Taking these things into account, `Blazored.Modal` should work on IE11.

### Please note: When upgrading from v4 to v5 (or higher) you must remove the `<BlazoredModal>` tag from your `MainLayout` component.

### 1. Register Services

**For Blazor Server**: You will need to add the following using statement and add a call to register the Blazored Modal services in your applications `Startup.ConfigureServices` method.

```csharp
using Blazored.Modal;

public void ConfigureServices(IServiceCollection services)
{
    services.AddBlazoredModal();
}
```

**For Blazor WebAssembly**: You will need to add the following using statement and add a call to register the Blazored Modal services in your applications `Program.Main` method.

```csharp
using Blazored.Modal;

public static async Task Main(string[] args)
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("app");

    builder.Services.AddBlazoredModal();

    await builder.Build().RunAsync();
}
```

### 2. Add Imports

Add the following to your *_Imports.razor*

```razor
@using Blazored.Modal
@using Blazored.Modal.Services
```

### 3. Add CascadingBlazoredModal Component around the existing Router component

Add the `<CascadingBlazoredModal />` component into your applications *App.razor*, wrapping the Router as per the example below.

```razor
<CascadingBlazoredModal>
    <Router AppAssembly="typeof(Program).Assembly">
        ...
    </Router>
</CascadingBlazoredModal>
```

### 4. Add reference to style sheet & javascript reference

Add the following line to the `head` tag of your `_Host.cshtml` (Blazor Server) or `index.html` (Blazor WebAssembly).

```html
<link href="_content/Blazored.Modal/blazored-modal.css" rel="stylesheet" />
```

Then add a reference to the Blazored Modal JavaScript file at the bottom of the respective page after the reference to the Blazor file.

```html
<script src="_content/Blazored.Modal/blazored.modal.js"></script>
```

## Usage
Please checkout the [sample projects](https://github.com/Blazored/Modal/tree/main/samples) in this repo to see working examples of the features in the modal. 

### Displaying the modal

In order to show a modal, you need to inject the `IModalService` into the component or service you want to invoke the modal. You can then call the `Show` method passing in the title for the modal and the type of the component you want the modal to display.

For example, if I have a component called `Movies` which I want to display in the modal and I want to call it from the `Index` component on a button click.

```razor
@page "/"

<h1>Hello, world!</h1>

Welcome to Blazored Modal.

<button @onclick="@(() => Modal.Show<Movies>("My Movies"))" class="btn btn-primary">View Movies</button>

@code {
    [CascadingParameter] public IModalService Modal { get; set; }
}
```

### Passing Parameters

If you want to pass values to the component you're displaying in the modal, then you can use the `ModalParameters` object. The name you provide must match the name of a parameter defined on the component being displayed.

#### Index Component

```razor
@page "/"

<h1>My Movies</h1>

<ul>
    @foreach (var movie in Movies)
    {
        <li>@movie.Name (@movie.Year) - <button @onclick="@(() => ShowEditMovie(movie.Id))" class="btn btn-primary">Edit Movie</button></li>
    }
</ul>

@code {

    [CascadingParameter] public IModalService Modal { get; set; }

    List<Movies> Movies { get; set; }

    void ShowEditMovie(int movieId)
    {
        var parameters = new ModalParameters();
        parameters.Add(nameof(EditMovie.MovieId), movieId);

        Modal.Show<EditMovie>("Edit Movie", parameters);
    }

}
```

#### EditMovie Component

```razor
@inject IMovieService MovieService

<div class="simple-form">

    <div class="form-group">
        <label for="movie-name">Movie Name</label>
        <input @bind="@Movie.Name" type="text" class="form-control" id="movie-name" />
    </div>

    <div class="form-group">
        <label for="year">Year</label>
        <input @bind="@Movie.Year" type="text" class="form-control" id="year" />
    </div>

    <button @onclick="SaveMovie" class="btn btn-primary">Submit</button>
    <button @onclick="ModalInstance.CancelAsync" class="btn btn-secondary">Cancel</button>
</div>

@code {

    [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }

    [Parameter] public int MovieId { get; set; }

    Movie Movie { get; set; }

    protected override void OnInitialized()
    {
        Movie = MovieService.Load(MovieId);
    }

    void SaveMovie()
    {
        MovieService.Save(Movie);
        ModalInstance.CloseAsync(ModalResult.Ok<Movie>(Movie));
    }

}
```

### Modal Reference

When you open a modal you can capture a reference to it and await the result of that modal. This is useful when you want to perform an action when a modal is closed or cancelled.

```razor
@page "/"

<h1>My Movies</h1>

<button @onclick="ShowModal" class="btn btn-primary">View Movies</button>

@code {

    [CascadingParameter] public IModalService Modal { get; set; }

    async Task ShowModal()
    {
        var moviesModal = Modal.Show<Movies>("My Movies");
        var result = await moviesModal.Result;

        if (result.Cancelled)
        {
            Console.WriteLine("Modal was cancelled");
        }
        else
        {
            Console.WriteLine("Modal was closed");
        }
    }
}
```

### Returning objects back to the calling code

It is common to want to return messages or objects back from a modal to the calling code. This is achieved using the `ModalResult` class.

In the example below, when the form is submitted a `ModalResult.Ok` containing the string "Form was submitted successfully." will be returned back to the calling code. If it is cancelled a `ModalResult.Cancelled` will be returned.

```razor
<div class="simple-form">

    <div class="form-group">
        <label for="first-name">First Name</label>
        <input @bind="FirstName" type="text" class="form-control" id="first-name" placeholder="Enter First Name" />
    </div>

    <div class="form-group">
        <label for="last-name">Last Name</label>
        <input @bind="LastName" type="text" class="form-control" id="last-name" placeholder="Enter Last Name" />
    </div>

    <button @onclick="SubmitForm" class="btn btn-primary">Submit</button>
    <button @onclick="Cancel" class="btn btn-secondary">Cancel</button>
</div>

@code {

    [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }

    bool ShowForm { get; set; } = true;
    string FirstName { get; set; }
    string LastName { get; set; }
    int FormId { get; set; }

    void SubmitForm()
    {
        ModalInstance.CloseAsync(ModalResult.Ok($"Form was submitted successfully."));
    }

    void Cancel()
    {
        ModalInstance.CancelAsync();
    }

}
```

Below is the caller for the component above. When the result is returned the string set in the `Ok` method can be access via the `Data` property on the `ModalResult`.

```razor
@page "/"

<button @onclick="ShowModal" class="btn btn-primary">View Form</button>

@code {

    [CascadingParameter] public IModalService Modal { get; set; }

    async Task ShowModal()
    {
        var formModal = Modal.Show<SignUpForm>("Please SignUp");
        var result = await formModal.Result;

        if (result.Cancelled)
        {
            Console.WriteLine("Modal was cancelled");
        }
        else
        {
            Console.WriteLine(result.Data);
        }
    }
}
```

The example above is only using a string value but you can also pass complex objects back as well. 

### Customizing the modal

The modals can be customized to fit a wide variety of uses. These options can be set globally or changed programatically on a per modal basis.

#### Hiding the close button

A modal has a close button in the top right hand corner by default. The close button can be hidden by using the `HideCloseButton` parameter:

`<CascadingBlazoredModal HideCloseButton="true" />`

Or in the `Modal.Show()` method:

```razor
@code {
    void ShowModal()
    {
        var options = new ModalOptions()
        {
            HideCloseButton = false
        };

        Modal.Show<Movies>("My Movies", options);
    }
}
```

#### Disabling background click cancellation

You can disable cancelling the modal by clicking on the background using the `DisableBackgroundCancel` parameter.

`<CascadingBlazoredModal DisableBackgroundCancel="true" />`

Or in the `Modal.Show()` method:

```razor
@code {
    void ShowModal()
    {
        var options = new ModalOptions()
        {
            DisableBackgroundCancel = true
        };

        Modal.Show<Movies>("My Movies", options);
    }
}
```

#### Styling the modal

You can set an alternative CSS class for the modal if you want to customize the look and feel. This is useful when your web application requires different kinds of modals, like a warning, confirmation or an input form.

Use the `Class` parameter to set the custom styling globally:

`<CascadingBlazoredModal Class="custom-modal" />`

Or in the `Modal.Show()` method:

```razor
@code {
    void ShowModal()
    {
        var options = new ModalOptions()
        {
            Class = "blazored-modal-movies"
        };

        Modal.Show<Movies>("My Movies", options);
    }
}
```

Note that supplying a custom CSS class will remove **all** of the default parameters and values used to display modals, specifically by overwriting the `.blazored-modal` class in `blazored-modal.css` with a class you supply. All default values for layout and background will be lost.

Unexpected behavior may result if certain CSS parameters are not provided to replace the default values, such as:
* `flex-direction: column;` while using a header 
    * The modal header and content use a `flexbox`. If you display a header but do not specify that the modal should be a `column`, the header and content will be displayed side-by-side in a row.
* `z-index: 102;`
    * The z-index needs to be greater than the background overlay, otherwise the modal and overlay will be displayed together and you cannot interact with any modal elements, such as forms or buttons. If the overlay click to close is disabled, this will lock the modal open until the page is refreshed. 
* `background-color: #fff;` and `border`.
    * If no background color or border is provided, the modal background will be transparent (which may be desired).

#### Setting the position

Modals are shown in the center of the viewport by default. The modal can be shown in different positions if needed.

The following positions are available out of the box: `ModalPosition.Center`, `ModalPosition.TopLeft`, `ModalPosition.TopRight`, `ModalPosition.BottomLeft` and `ModalPosition.BottomRight`.

Use the `Position` parameter to set the position globally:

`<CascadingBlazoredModal Position="ModalPosition.BottomLeft" />`

Or in the `Modal.Show()` method:

```razor
@code {
    void ShowModal()
    {
        var options = new ModalOptions()
        {
            Position = "ModalPosition.BottomLeft"
        };

        Modal.Show<Movies>("My Movies", options);
    }
}
```

If you need to use a custom position use `ModalPosition.Custom` and set the CSS class to use in `PositionCustomClass`.

```razor
@code {
    void ShowModal()
    {
        var options = new ModalOptions()
        {
            Position = ModalPosition.Custom
            PositionCustomClass = "custom-position-class";
        };
        Modal.Show<Movies>("My Movies", options);
    }
}
```

#### Animation
The modal also supports some animations.

The following animation types are available out of the box: `ModalAnimation.FadeIn`, `ModalAnimation.FadeOut` and `ModalAnimation.FadeInOut`.

Use the `Animation` parameter to set the custom styling globally:

`<CascadingBlazoredModal Animation="@ModalAnimation.FadeIn(2)"/>`

Or in the `Modal.Show()` method:

```razor
@code {
    void ShowModal()
    {
        var options = new ModalOptions() 
        { 
            Animation = ModalAnimation.FadeInOut(1)
        };

        Modal.Show<Movies>("My Movies", options);
    }
}
```

### Multiple Modals

It's possible to have multiple active modal instances at a time. You can find a working example of this in the sample projects but here is some sample code.

Below is a component which being displayed inside a Blazored Modal instance. When a user clicks on the _Delete_ button the `Yes` method is invoked and creates a new modal instance.  

```razor
<div class="simple-form">
    <div class="form-group">
        Are you sure you want to delete the record?
    </div>

    <button @onclick="Yes" class="btn btn-outline-danger">Delete</button>
    <button @onclick="No" class="btn btn-primary">Cancel</button>
</div>

@code {

    [CascadingParameter] IModalService Modal { get; set; }
    [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }

    async Task Yes()
    {
        var confirmationModal = ModalService.Show<ConfirmationPrompt>();
        var result = await confirmationModal.Result;

        if (result.Cancelled)
            return;

        ModalInstance.CloseAsync(ModalResult.Ok($"The user said 'Yes'"));
    }

    void No()
    {
        ModalInstance.CloseAsync(ModalResult.Cancel());
    }

}
```
