using System;
using System.ComponentModel.DataAnnotations;

namespace TooDue.Models
{
    public class Comment
    {
        [Key]
        public int Comment_id { get; set; }
        public string Comment_text { get; set; }
        public DateTime Comment_Date { get; set; }
        public string UserId { get; set; }
        public int TaskId { get; set; }

   
        public virtual ApplicationUser? User { get; set; }
    }
}
