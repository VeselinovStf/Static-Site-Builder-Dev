using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppTemplateElementsService<T>
    {
        Task AddTemplateElementsAsync(string templateId, IList<T> elements);
    }
}
