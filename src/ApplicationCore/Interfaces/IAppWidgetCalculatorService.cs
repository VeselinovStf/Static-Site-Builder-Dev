using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppWidgetCalculatorService
    {
        Task<bool> TakeTokensAsync(string clientId, string widgetId);
    }
}
