using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TooDue.Models
{
    public class Project
    {
        [Key]
        public int Project_id { get; set; }

        [Required]
        public string Project_name { get; set; }

        [Required]
        public string Project_description { get; set; }

        public string Project_status { get; set; } = "in work";

        public DateTime Project_create_date { get; set; } = DateTime.Now;

        public string CreatedByUserId { get; set; } // Add this property

        public virtual ApplicationUser? CreatedByUser { get; set; }
    }

}
