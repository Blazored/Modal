# Blazored Modal
This is a JavaScript free modal implementation for [Blazor](https://blazor.net) and Razor Components applications.

[![Build Status](https://dev.azure.com/blazored/Modal/_apis/build/status/Blazored.Modal?branchName=master)](https://dev.azure.com/blazored/Modal/_build/latest?definitionId=4&branchName=master)

![Nuget](https://img.shields.io/nuget/v/blazored.modal.svg)

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
Second, add the following to your *_ViewImports.cshtml*

```csharp
@using Blazored
@using Blazored.Modal.Services

@addTagHelper *, Blazored.Modal
```

### 3. Add Modal Component
Third and finally, you will need to add the `<BlazoredModal />` component in your applications *MainLayout.cshtml*.

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

}
```

### Modal Closed Event
If you need to know when the modal has closed, for example to trigger an update of data. The modal service exposes a `OnClose` event which you can attach to. 

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

    void ModalClosed()
    {
        Console.WriteLine("Modal has closed");
        Modal.OnClose -= ModalClosed;
    }

}
```
