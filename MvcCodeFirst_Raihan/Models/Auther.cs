using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcCodeFirst_Raihan.Models
{
    public class Auther
    {
        public int AutherID { get; set; }
        public string AutherName { get; set; }

        public string Email { get; set; }


        public int Phone { get; set; }
        public DateTime DOB { get; set; }

        public string Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}