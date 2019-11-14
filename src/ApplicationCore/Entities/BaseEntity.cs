using ApplicationCore.Interfaces;
using System;

namespace ApplicationCore.Entities
{
    public class BaseEntity : IDeletable, IModifiable
    {
        public string Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}