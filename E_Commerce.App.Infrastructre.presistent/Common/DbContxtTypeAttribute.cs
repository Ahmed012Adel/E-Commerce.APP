using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Infrastructre.presistent.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DbContxtTypeAttribute :Attribute
    {
        public Type DbContextType { get; set; }
        public DbContxtTypeAttribute(Type type)
        {
            DbContextType = type;
        }
    }
}
