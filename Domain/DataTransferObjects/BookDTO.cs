using System.ComponentModel.DataAnnotations;

namespace Domain.DataTransferObjects
{
    public record BookDTO(
        [Required]
        int Id,

        [Required]
        string Title,

        [Required]
        string Description,

        [Required]
        byte[] CoverPhoto,

        [Required, MinLength(1)]
        IList<AuthorDTO> Authors);
}
