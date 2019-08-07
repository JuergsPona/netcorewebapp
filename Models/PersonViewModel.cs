using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.Models
{
    public class PersonViewModel
    {
        public Person person { get; set; }
        public List<Person> parents { get; set; }
    }
}
