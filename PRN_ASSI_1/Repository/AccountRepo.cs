using BO.Models;
using DAO;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepo : IAccountRepo
    {
        private readonly SilverJewelry2023DbContext context;

        public AccountRepo(SilverJewelry2023DbContext context)
        {
            this.context = context;
        }

        public async Task<BranchAccount> Login(string email, string password)
        {
            return await context.BranchAccounts.Where(x => x.EmailAddress == email && x.AccountPassword == password).SingleOrDefaultAsync();
        }
    }
}
