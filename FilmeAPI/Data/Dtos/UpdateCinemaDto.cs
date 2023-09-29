using System.ComponentModel.DataAnnotations;

namespace FilmeAPI.Data.Dtos;

public class UpdateCinemaDto
{
    [Required(ErrorMessage = "O campo é obrigatorio.")]
    public string Nome { get; set; }
}
