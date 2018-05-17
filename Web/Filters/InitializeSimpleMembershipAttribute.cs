using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using Web.Models.Account;
using WebMatrix.WebData;
using Web.Models;

namespace Web.Filters
{



    public class AvutoxDBContextDatabaseInitializer : CreateDatabaseIfNotExists<UsersContext>
    {
        protected override void Seed(UsersContext context)
        {
            WebSecurity.InitializeDatabaseConnection("AvutoxDB", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            if (!Roles.RoleExists("SuperAdministrator"))
            {
                Roles.CreateRole("SuperAdministrator");
            }
            if (!Roles.RoleExists("Checker"))
            {
                Roles.CreateRole("Checker");
            }
            if (!Roles.RoleExists("Maker"))
            {
                Roles.CreateRole("Maker");
            }
            if (!WebSecurity.UserExists("admin"))
            {
                WebSecurity.CreateUserAndAccount("admin", "Newpw123", new { UserName = "admin", Email = "surenth.j@sagitec.com", Role = "SuperAdministrator", FirstName = "Admin", LastName = "LastName", IsConfirmed = true });

            }
            if (!Roles.GetRolesForUser("admin").Contains("SuperAdministrator"))
            {
                Roles.AddUsersToRoles(new[] { "admin" }, new[] { "SuperAdministrator" });
            }

        }

    }




    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<UsersContext>(new AvutoxDBContextDatabaseInitializer());

                try
                {

                    var context = new UsersContext();
                    context.Database.Initialize(true);
                    if (!WebSecurity.Initialized)
                    {
                        WebSecurity.InitializeDatabaseConnection("AvutoxDB", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("DB Connectivity issue occured", ex);
                }
            }
        }
    }
}
