using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class AccountDAO
    {
        private SilverJewelry2023DbContext context;
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }
        public AccountDAO()
        {
            context = new SilverJewelry2023DbContext();
        }

        public BranchAccount GetBranchAccount(string email, string password)
        {
            return context.BranchAccounts.FirstOrDefault(ba => ba.EmailAddress == email && ba.AccountPassword == password);
        }
    }
}
