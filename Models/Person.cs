using System;
using System.Collections.Generic;

namespace FamilyTree.Models
{
    public partial class Person
    {
        public int PersonId { get; set; }
        public string FullName { get; set; }
        public Status IsDeceased { get; set; }
        public string Mother { get; set; }
        public string Father { get; set; }
        public short IsParent { get; set; }
    }

    public enum Status : short
    {
        Alive,
        Dead
    }
}
