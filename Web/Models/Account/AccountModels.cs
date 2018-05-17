using System.Data.Entity;

namespace Web.Models.Account
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("AvutoxDB")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }


    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
