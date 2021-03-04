using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcCodeFirst_Raihan.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        [Required(ErrorMessage = "This filed must be fillup!!!")]
        public string StudentName { get; set; }
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "This filed must be fillup!!!")]
        public string CellPhone { get; set; }
        [Required(ErrorMessage = "This filed must be fillup!!!")]
        public string ContactAddress { get; set; }
    }
}