using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRegistration.Models
{
    public class Visitor
    {
        public string Name { get; }
        public Guid Id { get; }
        public Visitor(string name) { 
            Name = name;
            Id = Guid.NewGuid();
        }
    }
}
