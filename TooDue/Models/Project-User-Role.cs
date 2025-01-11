using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TooDue.Models
{
    public class Project_User_Role
    {
        [Key]
        public int Pur_id { get; set; }
        public string Role { get; set; }

        public int Related_project_id { get; set; }
        
        public string Related_user_id { get; set; }
        
        
    }
}
