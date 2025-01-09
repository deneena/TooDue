using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TooDue.Models
{
    public class Project_User_Task
    {
        [Key]
        public int Put_id { get; set; }
        public bool Task_completion { get; set; }
        [ForeignKey("Projects")]
        public virtual Project Project { get; set; }
        [ForeignKey("Users")]
        public virtual User User { get; set; }
        [ForeignKey("Tasks")]
        public virtual Task Task { get; set; }
    }
}
