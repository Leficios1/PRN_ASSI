﻿using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IAccountServices
    {
        public Task<TokenResponse?> Login(LoginRequest dto);
    }
}