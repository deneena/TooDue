using System.ComponentModel.DataAnnotations;

namespace TooDue.Models
{
    public class Project
    {
        [Key]
        public int Project_id { get; set; }
        public string Project_name { get; set; }    
        public string Project_description { get; set; }
        public string Project_status { get; set; }  
        public DateTime Project_create_date { get; set; }
    }
}
