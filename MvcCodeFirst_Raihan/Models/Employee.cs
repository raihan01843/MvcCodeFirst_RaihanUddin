using MvcCodeFirst_Raihan.CustomAttribute;
using MvcCodeFirst_Raihan.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcCodeFirst_Raihan.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        [Required(ErrorMessage = "This filed must be full")]
        public string Name { get; set; }
        public string Designation { get; set; }
        [Required(ErrorMessage = "This filed must be full")]
        [MyAttribute]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "This filed must be full")]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        [Display(Name ="Joining Date")]
       [DateAttribute]
        public DateTime JoiningDate { get; set; }
        [Display(Name = "Picture")]
        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
        public Employee()
        {
            ImagePath = "~/ AppFiles / Images ";
        }
    }
}