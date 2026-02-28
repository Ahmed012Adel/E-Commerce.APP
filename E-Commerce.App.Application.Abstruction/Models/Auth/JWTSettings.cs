using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Application.Abstruction.Models.Auth
{
    public class JWTSettings
    {
        public required string Key { get; set; }
        public required string Audience { get; set; }
        public required string Issuer { get; set; }
        public required Double DuerationInMinutes { get; set; }
    }
}
