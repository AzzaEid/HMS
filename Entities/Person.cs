using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital_Management_System.Entities.Enums;

namespace Hospital_Management_System.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        [MaxLength(10)]
        public string ContactNumber { get; set; }
    }
}
