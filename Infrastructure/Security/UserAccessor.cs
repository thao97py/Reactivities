using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Security
{
    public class UserAccessor: IUserAccessor
    {
        private readonly IHttpContextAccessor _htttpContextAccessor;
        public UserAccessor(IHttpContextAccessor htttpContextAccessor)
        {
            _htttpContextAccessor = htttpContextAccessor;
            
        }

        public string GetUserName()
        {
            return _htttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }
    }
}