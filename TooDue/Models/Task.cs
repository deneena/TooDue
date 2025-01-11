using System.ComponentModel.DataAnnotations;

namespace TooDue.Models
{
    public class Task
    {
        [Key]
        public int Task_id {  get; set; }
        [Required]
        public string Task_name {  get; set; }
        [Required]
        public string Task_description { get; set; }
        [Required]
        public string Task_completion { get; set; }
        [Required]
        public DateTime Task_create_date {get; set; }
        [Required]
        public DateTime Task_complete_date { get; set; }
        [Required]
        public string Link_to_media { get; set; }

        public int Project_Id;


    }
}
