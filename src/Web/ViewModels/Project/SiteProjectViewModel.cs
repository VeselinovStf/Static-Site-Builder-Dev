using System;

namespace Web.ViewModels.Project
{
    public class SiteProjectViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ProjectType { get; set; }
        public int ItemsCount { get; set; }
    }
}