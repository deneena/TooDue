using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TooDue.Models
{
    public class Project_User_Role
    {
        [Key]
        public int Pur_id { get; set; }
        public string Role {  get; set; }
        [ForeignKey("Projects")]
        public virtual Project Project { get; set; }
        [ForeignKey("Users")]
        public virtual User User { get; set; }
    }
}
