using System.ComponentModel.DataAnnotations;

namespace TooDue.Models
{
    public class Task
    {
        [Key]
        public int Task_id {  get; set; }
        public string Task_name {  get; set; }  
        public string Task_description { get; set; }
        public string Task_status { get; set; }
        public string Task_label { get; set; }
        public bool Is_collaborative { get; set; }
        public DateTime Task_deadline { get; set; }
        public DateTime Task_create_date {get; set; }
        public DateTime Task_update_date { get; set; }
        public DateTime Task_complete_date { get; set; }
    }
}
