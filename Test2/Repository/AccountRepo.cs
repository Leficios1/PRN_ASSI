using BOs.Models;
using DAO;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepo : IAccountRepo
    {
        public async Task<BranchAccount> Login(string username, string password)
        {
            return await BranhAccounDAO.Instance.login(username, password);
        }
    }
}
