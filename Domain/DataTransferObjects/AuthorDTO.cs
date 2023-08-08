using System.ComponentModel.DataAnnotations;

namespace Domain.DataTransferObjects
{
    public record AuthorDTO(
        [Required]
        int Id,

        string Name);
}
