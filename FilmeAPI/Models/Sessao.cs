using System.ComponentModel.DataAnnotations;

namespace FilmeAPI.Models;

public class Sessao
{
    //Relação: Filme --> Sessao  1:N
    public virtual Filme Filme { get; set; }
    public int? FilmeId { get; set; }


    //Relação: Filme --> Cinema  1:N
    public virtual Cinema Cinema { get; set; }
    public int? CinemaId { get; set; }// Esta que pode ser NULO por causa do Index(0) no B.D

}
