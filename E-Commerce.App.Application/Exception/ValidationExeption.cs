using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Application.Exception
{
    public class ValidationExeption :BadRequestException
    {
        public required IEnumerable<string> Errors { get; set; }
        public ValidationExeption(string message = "Bad Request ") 
            :base(message)
        {
            
        }
    }
}
