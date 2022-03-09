using DevSkill.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Membership.Entities
{
    public class Teacher :IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
    }
}
