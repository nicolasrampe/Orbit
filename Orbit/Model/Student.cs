using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Orbit.Model
{
    [Table("Student")]
    public class Student
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [StringLength(20)]
        public string Username { get; set; }
        [StringLength(20)]
        public string Firstname { get; set; }
        [StringLength(20)]
        public string Lastname { get; set; }
        public int Age { get; set; }
        [StringLength(20)]
        public string Career { get; set; }

    }
}
