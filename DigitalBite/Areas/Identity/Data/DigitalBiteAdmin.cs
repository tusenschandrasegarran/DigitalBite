using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DigitalBite.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the DigitalBiteAdmin class
    public class DigitalBiteAdmin : IdentityUser
    {
        [PersonalData]
        public string RestaurantName { get; set; }
        [PersonalData]
        public string Address { get; set; }
    }
}
