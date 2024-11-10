using BOs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class BranhAccounDAO
    {
        private static BranhAccounDAO instance;
        private readonly SilverJewelry2023DbContext context;

        private BranhAccounDAO()
        {
            context = new SilverJewelry2023DbContext();
        }

        public static BranhAccounDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BranhAccounDAO();
                }
                return instance;
            }
        }

        public async Task<BranchAccount> login(string email, string password)
        {
            return await context.BranchAccounts.Where(x => x.EmailAddress.Equals(email) && x.AccountPassword.Equals(password)).SingleOrDefaultAsync();
        }

    }
}
