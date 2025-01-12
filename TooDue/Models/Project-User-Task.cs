using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Project_User_Task
{
    [Key]
    public int Put_id { get; set; }

    [Column("ProjectId")]
    public int Project_id { get; set; }

    public string User_id { get; set; }
    public int Task_id { get; set; }
}
