using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blazored.Modal
{
    public partial class ModalGeneric
    {
        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; }




        private void Close(bool result)
        {
            BlazoredModal.Close(ModalResult.Ok(result));
        }
        private void Cancel()
        {
            BlazoredModal.Cancel();
        }

    }
}
