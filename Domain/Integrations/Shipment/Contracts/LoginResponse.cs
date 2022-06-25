using System;
using System.Collections.Generic;
using System.Text;

namespace APIContracts
{
    public class LoginResponse
    {
        public LoginResponse()
        {

        }
        public LoginResponse(string token)
        {
            Token = token;
        }
        public string Token { get; set; }
    }
}
