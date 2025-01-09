using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using TooDue.Models;

// PASUL 1: useri si roluri

namespace TooDue.Models
{
    public class ApplicationUser : IdentityUser
    {
        // PASUL 6: useri si roluri
        // un user poate posta mai multe comentarii
        public virtual ICollection<Comment>? Comments { get; set; }

        // un user poate posta mai multe articole
        public virtual ICollection<Project>? Articles { get; set; }

        // un user poate sa creeze mai multe colectii
        public virtual ICollection<TooDue.Models.Task>? Bookmarks { get; set; }

        // atribute suplimentare adaugate pentru user
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        // variabila in care vom retine rolurile existente in baza de date
        // pentru popularea unui dropdown list
        [NotMapped]
        public IEnumerable<SelectListItem>? AllRoles { get; set; }

    }
}
