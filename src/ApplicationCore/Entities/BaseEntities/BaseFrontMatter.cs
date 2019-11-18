namespace ApplicationCore.Entities.BaseEntities
{
    public class BaseFrontMatter : BaseEntity
    {
        public string Layout { get; set; }

        public string PermaLink { get; set; }

        public string Include { get; set; }
    }
}