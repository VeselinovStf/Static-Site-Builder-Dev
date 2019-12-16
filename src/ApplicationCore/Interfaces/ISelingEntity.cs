using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface ISelingEntity
    {
         bool IsFree { get; set; }

         decimal Price { get; set; }
    }
}
