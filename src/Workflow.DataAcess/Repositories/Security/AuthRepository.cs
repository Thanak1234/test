using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Security;

namespace Workflow.DataAcess.Repositories.Security
{
    public class AuthRepository : RepositoryBase<UserAuth>, IDisposable
    {
        
        private UserManager<IdentityUser> userManager;

        public AuthRepository() : base(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow)) 
        {
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(DbContext));
        }

        public async Task<IdentityResult> RegisterUser(UserAuth userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await userManager.CreateAsync(user, userModel.Password);
            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            return await userManager.FindAsync(userName, password);
        }

        public void Dispose()
        {
            userManager.Dispose();
        }
    }
}
