# Blazored Modal
This is a JavaScript free modal implementation for [Blazor](https://blazor.net) and Razor Components applications.

[![Build Status](https://dev.azure.com/blazored/Modal/_apis/build/status/Blazored.Modal?branchName=master)](https://dev.azure.com/blazored/Modal/_build/latest?definitionId=4&branchName=master)

![Nuget](https://img.shields.io/nuget/v/blazored.modal.svg)

## Important Notice For ASP.NET Core Razor Components Apps
There is currently an issue with [ASP.NET Core Razor Components apps](https://devblogs.microsoft.com/aspnet/aspnet-core-3-preview-2/#sharing-component-libraries) (not Blazor). They are unable to import static assets from component libraries such as this one. 

You can still use this package, however, you will need to manually add the CSS to your apps `wwwroot` folder. You will then need to add a reference to it in the `head` tag of your apps `index.html` page.

Alternatively, there is a great package by [Mister Magoo](https://github.com/SQL-MisterMagoo/BlazorEmbedLibrary) which offers a solution to this problem without having to manually copy files.

## Getting Setup
You can install the package via the nuget package manager just search for *Blazored.Modal*. You can also install via powershell using the following command.

```powershell
Install-Package Blazored.Modal
```

Or via the dotnet CLI.

```bash
dotnet add package Blazored.Modal
```

### 1. Register Services
First, you will need to add the following line to your applications `Startup.ConfigureServices` method.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddBlazoredModal();
}
```

### 2. Add Imports
Second, add the following to your *_Imports.razor*

```csharp
@using Blazored
@using Blazored.Modal
@using Blazored.Modal.Services
```

### 3. Add Modal Component
Third and finally, you will need to add the `<BlazoredModal />` component in your applications *MainLayout.razor*.

## Usage
### Displaying the modal
In order to show the modal, you have to inject the `IModalService` into the component or service you want to invoke the modal. You can then call the `Show` method passing in the title for the modal and the type of the component you want the modal to display. 

For example, say I have a component called `Movies` which I want to display in the modal and I want to call it from the `Index` component on a button click.

```html
@page "/"
@inject IModalService Modal

<h1>Hello, world!</h1>

Welcome to Blazored Modal.

<button onclick="@(() => Modal.Show("My Movies", typeof(Movies)))" class="btn btn-primary">View Movies</button>
```

### Passing Parameters
If you need to pass values to the component you are displaying in the modal, then you can use the `ModalParameters` object. Any component which is displayed in the modal has access to this object as a `[CascadingParameter]`. 

**Index Component**
```html
@page "/"
@inject IModalService Modal

<h1>My Movies</h1>

<ul>
    @foreach (var movie in Movies)
    {
        <li>@movie.Name (@movie.Year) - <button onclick="@(() => ShowEditMovie(movie.Id))" class="btn btn-primary">Edit Movie</button></li>
    }
</ul>

@functions {

    List<Movies> Movies { get; set; }

    void ShowEditMovie(int movieId)
    {
        var parameters = new ModalParameters();
        parameters.Add("MovieId", movieId);

        Modal.Show("Edit Movie", typeof(EditMovie), parameters);
    }

}
```

**EditMovie Component**
```html
@inject IMovieService MovieService
@inject IModalService ModalService

<div class="simple-form">

    <div class="form-group">
        <label for="movie-name">Movie Name</label>
        <input bind="@Movie.Name" type="text" class="form-control" id="movie-name" />
    </div>

    <div class="form-group">
        <label for="year">Year</label>
        <input bind="@Movie.Year" type="text" class="form-control" id="year" />
    </div>

    <button onclick="@SaveMovie" class="btn btn-primary">Submit</button>
    <button onclick="@Cancel" class="btn btn-secondary">Cancel</button>
</div>

@functions {

    [CascadingParameter] ModalParameters Parameters { get; set; }
    
    int MovieId { get; set; }
    Movie Movie { get; set; }

    protected override void OnInit()
    {
        MovieId = Parameters.Get<int>("MovieId");
        LoadMovie(MovieId);
    }

    void LoadMovie(int movieId)
    {
        MovieService.Load(movieId);
    }

    void SaveMovie()
    {
        MovieService.Save(Movie);
    }

    void Cancel()
    {
        ModalService.Cancel();
    }

}
```

### Modal Closed Event
If you need to know when the modal has closed, for example to trigger an update of data. The modal service exposes a `OnClose` event which returns a `ModalResult` type. This type is used to identify how the modal was closed. If the modal was cancelled you can return `ModalResult.Cancelled()`. If you want to return a object from your modal you can return `ModalResult.Ok(myResultObject)` which can be accessed via the `ModalResult.Data` property. There is also a `ModalResult.DataType` property which contains the type of the data property, if required.

```html
@page "/"
@inject IModalService Modal

<h1>My Movies</h1>

<button onclick="@ShowModal" class="btn btn-primary">View Movies</button>

@functions {

    void ShowModal()
    {
        Modal.OnClose += ModalClosed;
        Modal.Show("My Movies", typeof(Movies));
    }

    void ModalClosed(ModalResult modalResult)
    {
        if (modalResult.Cancelled)
        {
            Console.WriteLine("Modal was cancelled");
        }
        else
        {
            Console.WriteLine(modalResult.Data);
        }

        Modal.OnClose -= ModalClosed;
    }

}
```
