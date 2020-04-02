# Long running task
How to open a modal, execute a long-running task and close the modal after it.

---

We can open a modal and using its `ModalReference` we can also close it from outside of the actual Modal itself.

```html
<button @onclick="ShowModal">Execute long-running task</button>

@code {

    string _message;

    async Task ShowModal()
    {
        var loadingModal = Modal.Show<Loading>();
		
        await Task.Delay(5000); //Do your long running task, here we just wait 5 seconds
		
        loadingModal.Close();
		
        var result = await loadingModal.Result;
        //Do something with the result. 
        //In this case we won't as we just showed a loading dialog
    }

}
```