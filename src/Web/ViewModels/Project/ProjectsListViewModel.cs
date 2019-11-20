using System.Collections.Generic;
using Web.ViewModels.Walktry;

namespace Web.ViewModels.Project
{
    public class ProjectsListViewModel
    {
        public string ClientId { get; set; }
        public WalkTryPageDisplayViewModel WalkTry { get; set; }
        public IEnumerable<SiteProjectViewModel> Projects { get; set; }
    }
}