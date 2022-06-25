using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Domain.AEB.DTOs
{
    public class AuthenticationRequestDTO : BaseDTO
    {
        public string ClientName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LocaleName { get; set; }
        public bool IsExternalLogon { get; set; }
    }
}
