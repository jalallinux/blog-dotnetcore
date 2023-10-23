using System.ComponentModel.DataAnnotations;
using Entities.Contracts;

namespace Entities.Models;

public class User: BaseEntity
{
    [Required]
    [StringLength(100)]
    public string UserName { get; set; }
    
    [Required]
    [StringLength(500)]
    public string PasswordHash { get; set; }
    
    [Required]
    [StringLength(100)]
    public string FullName { get; set; }
    
    [Required]
    public int Age { get; set; }
    
    public GenderType Gender { get; set; }
    
    public bool IsActive { get; set; }
    
    public DateTimeOffset? LastLoginDate { get; set; }

    public ICollection<Post>? Posts { get; set; }
    

    public enum GenderType
    {
        [Display(Name = "زن")]
        Female = 0,
        
        [Display(Name = "مرد")]
        Male = 1
    }
    
    public User()
    {
        IsActive = true;
    }
}