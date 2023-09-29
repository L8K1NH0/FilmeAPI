using System.ComponentModel.DataAnnotations;

namespace FilmeAPI.Models;

public class Cinema
{
    [Key]
    [Required]
    public int id { get; set; }
    [Required(ErrorMessage = "O campo é obrigatorio.")]
    public string Nome { get; set; }

    //Relação com endereço 1:1
    public int EnderecoId { get; set; }
    public virtual Endereco Endereco { get; set; }

    //Relação: Filme --> Cinema  1:N
    public virtual ICollection<Sessao> Sessoes { get; set; }

}
