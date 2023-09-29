using System.ComponentModel.DataAnnotations;

namespace FilmeAPI.Data.Dtos;

public class CreateFilmeDto
{   

    [Required(ErrorMessage = "Titulo do filme é obrigatorio")]
    public string? Titulo { get; set; }

    [Required(ErrorMessage = "O genero do filme é obrigatorio")]
    [StringLength(50, ErrorMessage = "O nome do gênero não pode exceder 50 caracteres")]//Maximo de caracteres
    public string? Genero { get; set; }

    [Required(ErrorMessage = "O tempo do filme é obrigatorio")]
    [Range(70, 600, ErrorMessage = "A duração deve ter entre 70 e 600 minuto")]//Delimitando o minimo e maximo
    public int Duracao { get; set; }
}
