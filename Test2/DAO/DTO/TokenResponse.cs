using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DTO
{
    public class TokenResponse
    {
        public string TokenString { get; set; } = null!;
        public int? RoleId { get; set; }
        public int AccountId { get; set; }
    }
}
