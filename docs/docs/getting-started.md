---
slug: /
title: Getting Started
sidebar_position: 1
---

# Blazored Modal Docs

[![Nuget](https://img.shields.io/nuget/v/blazored.modal.svg?logo=nuget)](https://www.nuget.org/packages/Blazored.Modal/)
[![Issues](https://img.shields.io/github/issues/Blazored/Modal?logo=github)](https://github.com/Blazored/Modal/issues)
![Nuget](https://img.shields.io/nuget/dt/Blazored.Modal?logo=nuget)

Blazored Modal is a powerful and customizable modal implementation for [Blazor applications](https://blazor.net).

## Getting Started

The first step is to install the Blazored.Modal NuGet package into your project. You can install the package via the nuget package manager, just search for *Blazored.Modal*. You can also install via powershell using the following command.

```powershell
Install-Package Blazored.Modal
```

Or via the dotnet CLI.

```bash
dotnet add package Blazored.Modal
```

### Register Services

Blazored Modal uses a service to coordinate modals. To register this service you need to add the following using statement and call to `AddBlazoredModal` in your applications `Program.cs` file.

```csharp
using Blazored.Modal;
```

```csharp
builder.Services.AddBlazoredModal();
```

:::info

The above code assumes the use of top level statements. If your application is not using them, please add the call to `AddBlazoredModal` where you're registering services for your app.

:::

### Add Imports

To avoid having to add using statements for Blazored Modal to lots of components in your project, it's recommended that you add the following to your root *_Imports.razor* file. This will make the following usings available to all component in that project.

```razor
@using Blazored.Modal
@using Blazored.Modal.Services
```

### Add CSS Reference

Blazored Modal uses CSS isolation. If your application is already using CSS isolation then the styles for Modal will be included automatically and you can skip this step. However, if your application isn't using isolated CSS, you will need to add a reference to the CSS bundle. You can checkout the [Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/css-isolation?view=aspnetcore-6.0#css-isolation-bundling) for additional details.

```html
<link href="{YOUR APP ASSEMBLY NAME}.styles.css" rel="stylesheet">
```

### Add the CascadingBlazoredModal Component

The `<CascadingBlazoredModal />` component cascades an instance of the `IModalService` to all decendant components. This should be added to the root component of your application (usually `App.razor`) wrapping the Router as per the example below.

```html
<CascadingBlazoredModal>
    <Router AppAssembly="typeof(Program).Assembly">
        ...
    </Router>
</CascadingBlazoredModal>
```

### Displaying a modal

In order to display a modal, you must define a cascading parameter on the component that will invoke the modal:

```csharp
[CascadingParameter] public IModalService Modal { get; set; } = default!;
```

Once you have the cascading parameter setup, you can call the `Show` method on the `IModalService` passing in the title for the modal and the type of the component you want the modal to display. For example, if you have a component called `Movies` that you want to display in a modal from the `Home` component on a button click.

```html
@page "/"

<h1>Hello, world!</h1>

Welcome to Blazored Modal.

<button @onclick="@(() => Modal.Show<Movies>("My Movies"))">View Movies</button>

@code {
    [CascadingParameter] public IModalService Modal { get; set; }
}
```