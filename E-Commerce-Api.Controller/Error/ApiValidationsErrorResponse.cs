using E_Commerce.App.Application.Abstruction.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Api.Controller.Error
{
    public class ApiValidationsErrorResponse : ApiResponse
    {
        public IEnumerable<ValidationError>? Errors { get; set; }

        public ApiValidationsErrorResponse(string? message = null) : base(400, message)
        {
            
        }
        public class ValidationError
        {
            public required string Field { get; set; }
            public required IEnumerable<string> Errors { get; set; }
        }

    }
}
