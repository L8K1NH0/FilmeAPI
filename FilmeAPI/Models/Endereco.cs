using System.ComponentModel.DataAnnotations;

namespace FilmeAPI.Models;

public class Endereco
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string Logradouro { get; set; }
    public int Numero { get; set; }

    //Relação com cinema 1:1
    public virtual Cinema Cinema { get; set; }
}
