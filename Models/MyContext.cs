using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KursachV2.Models
{
    public class MyContext :IdentityDbContext<User>
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
