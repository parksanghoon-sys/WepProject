﻿using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {        
        public AuthenticationService(IClient client) : base(client)
        {
        }

        public Task<bool> AuthenticateAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterAsync(string firstName, string lastName, string userName, string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
