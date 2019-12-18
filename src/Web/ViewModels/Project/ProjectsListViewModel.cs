using System.Collections.Generic;


namespace Web.ViewModels.Project
{
    public class ProjectsListViewModel
    {
        public string ClientId { get; set; }
       
        public IEnumerable<SiteProjectViewModel> Projects { get; set; }
    }
}