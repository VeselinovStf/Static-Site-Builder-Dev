using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IModelState
    {
         bool IsValid { get; set; }

         string ErrorMessage { get; set; }
    }
}
