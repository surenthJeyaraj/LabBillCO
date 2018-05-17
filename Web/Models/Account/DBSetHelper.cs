using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Web.Models.Account
{
    public static class DBSetHelper
    {
        public static IQueryable<UserProfile> WithName(this DbSet<UserProfile> userProfiles, string username)
        {
            if (!string.IsNullOrWhiteSpace(username))
            {
                return userProfiles.Where(x => x.UserName == username);
            }
            return userProfiles;
        }
        public static IQueryable<UserProfile> WithEmail(this IQueryable<UserProfile> userProfiles, string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                return userProfiles.Where(x => x.Email == email);
            }
            return userProfiles;
        }

        public static IQueryable<UserProfile> WithGlobalValue(this IQueryable<UserProfile> userProfiles, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return
                    userProfiles.Where(
                        x =>
                            x.UserName.Contains(value) || 
                            x.Email.Contains(value) || 
                            x.FirstName.Contains(value) ||
                            x.LastName.Contains(value));
            }
            return userProfiles;
        }

        public static IQueryable<UserProfile> WithRoles(this IQueryable<UserProfile> userProfiles, IList<string> roles)
        {
            if (roles != null && roles.Any())
            {
                return userProfiles.Where(x => roles.Contains(x.Role));
            }
            return userProfiles;
        }

        public static IQueryable<UserProfile> WithPagination(this IQueryable<UserProfile> userProfiles, int page,int perpage)
        {
            return userProfiles.OrderByDescending(x=>x.UserId).Skip(page).Take(perpage);
        }
    }
}