using System.ComponentModel.DataAnnotations;

namespace ESLearn.API.DTOs
{
    public class CreateUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}