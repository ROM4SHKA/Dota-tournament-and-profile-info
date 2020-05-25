using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace KursachV2.Models
{
    public class User : IdentityUser
    {
        public int? Account_id { get; set; }
    }
}
