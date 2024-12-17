using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcwithado.Models
{
    public class product
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int age { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string course { get; set; }
        [Required]
        public string dob { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        //public HttpPostedFileBase photo { get; set; }
        //public HttpPostedFileBase resume { get; set; }
    }
}