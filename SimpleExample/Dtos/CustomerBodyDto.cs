using System.ComponentModel.DataAnnotations;

namespace SimpleExample.Dtos
{
    public class CustomerBodyDto
    {
        [Required]
        public string FullName { get; set; }
    }
}
