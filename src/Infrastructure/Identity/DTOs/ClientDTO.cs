using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Identity.DTOs
{
    public class ClientDTO : ClientTokenDTO, IModelState
    {
        public string UserName { get; set; }

        public string Email { get; set; }
       
        public bool IsValid { get; set; }

        public string ErrorMessage { get; set; }
    }
}
