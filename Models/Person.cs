using System;
using System.Collections.Generic;

namespace FamilyTree.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FullName { get; set; }
        public Status IsDeceased { get; set; }
        public string Mother { get; set; }
        public string Father { get; set; }
        public IsParent IsParent { get; set; }
        public Role ParentalRole { get; set; }
    }

    public enum Status : short
    {
        Alive,
        Dead,
        Unknown
    }

    public enum IsParent : short
    {
        False,
        True
    }

    public enum Role : short
    {
        None,
        Mother,
        Father
    }
}
