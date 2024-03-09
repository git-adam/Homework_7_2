using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_7_2.Models.Wrappers
{
    public class EmployeeWrapper
    {
        public EmployeeWrapper()
        {
            Status = new StatusWrapper();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Number { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? DismissDate { get; set; }
        public decimal Salary { get; set; }
        public bool Bonus { get; set; }
        public string Comments { get; set; }
        public StatusWrapper Status { get; set; }

    }
}
