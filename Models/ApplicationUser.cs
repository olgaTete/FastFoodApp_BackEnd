using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FastFoodOrderingApp_BackEnd.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }  // Custom property for storing full name
        //public List<Order> Orders { get; set; } // List of associated orders for the user
       //public List<Order> Orders { get; set; } = new List<Order>();
    }
}

