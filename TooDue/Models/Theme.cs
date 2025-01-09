using System.ComponentModel.DataAnnotations;

namespace TooDue.Models
{
    public class Theme
    {
        [Key]
        public int Theme_id { get; set; }
        public string Main_color { get; set; }
        public string Light_accent_color { get; set; }
        public string Dark_accent_color { get; set; }
        public string Transparent_color { get; set; }
        public string Animal { get; set; }
        public string Flower { get; set; }
        public string Background_repeat { get; set; }
    }
}
