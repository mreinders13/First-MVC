using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment1.Models
{
    public class Student
    {
        public Student()
        {

        }

        [Display(Name = "Student ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentID { get; set; }

        [StringLength(25)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(25)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Range(0, 4)]
        [Display(Name = "GPA")]
        public double GPA { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Graduation Date")]
        public System.DateTime GraduationDate { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        
    }
}
