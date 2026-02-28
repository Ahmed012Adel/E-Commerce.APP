using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Application.Exception
{
    public class UnAuthorizedExeption :ApplicationException
    {
        public UnAuthorizedExeption(string? Message = null) : base(Message)
        {
            
        }
    }
}
