using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TooDue.Models
{
    public class User
    {
        [Key]
        public int User_id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string User_displayname { get; set; }
        public string User_status { get; set; }
        public string User_bio { get; set; }
        public string Profile_picture { get; set; }
        public string Banner_picture { get; set; }
        [ForeignKey("Themes")]
        public virtual Theme Theme { get; set; }
    }
}
