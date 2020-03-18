# Getting Started
How to get up and running with Blazored Modal.

---

## 1. Installation
You can install the package via the nuget package manager just search for *Blazored.Modal*. You can also install via powershell using the following command.

```powershell
Install-Package Blazored.Modal
```

Or via the dotnet CLI.

```bash
dotnet add package Blazored.Modal
```

## 2. Register Services

### Blazor Server
You will need to add the following using statement and add a call to register the Blazored Modal services in your applications `Startup.ConfigureServices` method.

```csharp
using Blazored.Modal;

public void ConfigureServices(IServiceCollection services)
{
    services.AddBlazoredModal();
}
```

### Blazor WebAssembly
You will need to add the following using statement and add a call to register the Blazored Modal services in your applications `Program.Main` method.

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

## 3. Reference Stylesheet

Add the following line to the `head` tag of your `_Host.cshtml` (Blazor Server) or `index.html` (Blazor WebAssembly).

```html
<link href="_content/Blazored.Modal/blazored-modal.css" rel="stylesheet" />
```

## 4. Add Component To Layout

Add the `BlazoredModal` component to your applications `MainLayout.razor`.

```html
@inherits LayoutComponentBase

<BlazoredModal />

<!-- Your layout code -->
```

## 5. Import Namespaces (optional)

To make life a little easier you can add the following using statements to your root `_Imports.razor`. This save you having to reference the fully qualified name of Blazored Modal components and services in your components.

```razor
@using Blazored
@using Blazored.Modal
@using Blazored.Modal.Services
```