using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Produccion.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string FirstName { get; set; } = "DefaultFirstName";

        public string LastName { get; set; } = "DefaultLastName";
    }
}
