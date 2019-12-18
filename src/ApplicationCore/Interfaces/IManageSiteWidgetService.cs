using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IManageSiteWidgetService
    {
        Task<string> ConfirmUsebleWidget(string widgetId, string templateName);
    }
}
