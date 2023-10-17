using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Category: BaseEntity
{
    public int? ParentId { get; set; }
    
    /** This is: Data Annotations | Attributes */
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    
    [ForeignKey(nameof(ParentId))]
    public Category Parent { get; set; }
    
    public ICollection<Category> Children { get; set; }
    
    public ICollection<Post> Posts { get; set; }
}