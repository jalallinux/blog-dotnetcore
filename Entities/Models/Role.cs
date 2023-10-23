using System.ComponentModel.DataAnnotations;
using Entities.Contracts;

namespace Entities.Models;

public class Role: BaseEntity
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Description { get; set; }
}