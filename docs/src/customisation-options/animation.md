# Animation
How to customise the animation of modals.

---

By default, Blazored Modal supports 2 different animations: Fade-in and Fade-out. These 2 can also be combined to both fade-in and fade-out. By default no animation is used. If you want to use an animation for the modal, you can set one globally or per modal. 

## Setting Animation Globally

To set a global animation for all modals in your application, you can set the `Animation` parameter on the `BlazoredModal` component in your `MainLayout`. Below sets a Fade-in animation with a duration of 2 seconds.

```html
@inherits LayoutComponentBase

<BlazoredModal Animation="@ModalAnimation.FadeIn(2)"/>

<!-- Other code removed for brevity -->
```

## Setting Animation Per Modal

To set the Animation of a specific modal instance you can pass in a `ModalOptions` object when calling `Show`. Below exapmle will add a Fade-in and Fade-out animation, which will take 1 second each.

```csharp
var options = new ModalOptions { Animation = ModalAnimation.FadeInOut(1)};

_ = Modal.Show<Confirm>("My Modal", options);
```

