using System.ComponentModel.DataAnnotations;

namespace WebFramework.Api;

public enum ApiResultStatusCode
{
    [Display(Name = "Operation successful.")]
    Success = 0,
    
    [Display(Name = "Server error.")]
    ServerError = 1,
    
    [Display(Name = "The parameters are not valid.")]
    BadRequest = 2,
    
    [Display(Name = "Entity not found")]
    NotFound = 3,
    
    [Display(Name = "The list is empty")]
    ListEmpty = 4,
}