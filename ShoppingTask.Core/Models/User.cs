using Microsoft.AspNetCore.Identity;

namespace ShoppingTask.Core.Models;

public class User : IdentityUser<Guid>
{
  
    public bool IsDeleted { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>(); 
}
