using System.ComponentModel.DataAnnotations;

namespace DomainClass
{
    public abstract class BaseEntity<TId>
    {

        [Required]
        [Key]
        public TId Id { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
