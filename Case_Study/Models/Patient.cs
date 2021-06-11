using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Case_Study.Models
{
    public class Patient
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PatientID { get; set; }
        public String Name { get; set; }
        public long Contact { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
    }
}
