using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Task_completion { get; set; } = "Not started";
        [Required]
        public DateTime Task_create_date { get; set; } = DateTime.Now;
        [Required]
        public DateTime Task_complete_date { get; set; } = DateTime.Now;

        [Required]
        public DateTime Task_deadline { get; set; } = DateTime.Now;
        [Required]
        public string Link_to_media { get; set; }

        [ForeignKey("Project")]
        public int Project_Id { get; set; }
        public Project? Project { get; set; }
    }
}
