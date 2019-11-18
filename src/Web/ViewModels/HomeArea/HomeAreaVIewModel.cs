using System.Collections.Generic;

namespace Web.ViewModels.HomeArea
{
    public class HomeAreaViewModel
    {
        public string ClientId { get; set; }

        public IList<HomeAreaPostsViewModel> HomePosts { get; set; }
    }
}