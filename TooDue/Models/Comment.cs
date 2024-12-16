using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TooDue.Models
{
    public class Comment
    {
        [Key]
        public int Comment_id { get; set; }
        public string Comment_text { get; set; }
        public DateTime Comment_Date { get; set; }
        [ForeignKey("Users")]
        public virtual User User { get; set; }
    }
}
