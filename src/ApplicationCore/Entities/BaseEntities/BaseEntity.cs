using ApplicationCore.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.BaseEntities
{
    public class BaseEntity : IDeletable, IModifiable
    {
        [Key]
        public string Id { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}