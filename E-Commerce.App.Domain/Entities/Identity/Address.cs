using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Domain.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string lastName { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }

        public required string UserId { get; set; }
        public virtual required ApplicationsUser User { get; set; }
    }
}
