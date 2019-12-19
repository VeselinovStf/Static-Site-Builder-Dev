namespace Web.ViewModels.Widgets.ProductWidget
{
    public class ProductsSingleSimpleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Collection { get; set; }

        public string Category { get; set; }

        public bool Publish { get; set; }
    }
}