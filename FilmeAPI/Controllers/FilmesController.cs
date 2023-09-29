namespace FilmeAPI.Controllers;

using AutoMapper;
using FilmeAPI.Data;
using FilmeAPI.Data.Dtos;
using FilmeAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class FilmesController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public FilmesController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um filme ao B.D
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessarios para a criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);

        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperarFilmePorId), new { id =filme.Id}, filme);//Retorna o caminho que as info foram guardadas
        //recuperar informações da API através da criação de actions.
    }

    /// <summary>
    /// Exibir lista de todos os filmes salvos no B.D, declarando a quantidade de filmes 
    /// </summary>
    /// <param name="skip">Objeto necessarios para informar a partir de qual filme que começar a lista</param>
    /// <param name="take">>Objeto necessarios para informar quantos filmes que de retorno</param>
    /// <returns>IEnumerable</returns>
    /// <response code="200">Caso a consulta seja feita com sucesso.</response>
    [HttpGet]
    public IEnumerable<ReadFilmeDto> RecuperaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 40, [FromQuery] string? nomeCinema = null)//Com valor padrão determinado
    {//CONCEITO "PAGINAÇÃO"
        if(nomeCinema == null)
        {
            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take).ToList());
            //Skip --> QUANTOS EU QURO PULAR(apartir de qual eu começo)   //Take --> QUANTOS EU QUERO PEAGR
            //Não se coloca NotFound no caso da lista esta vazia, pq no caso a lista está vazia e não é que ele nn tenha encontrado...
        }
        return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take)
            .Where(filme => filme.Sessoes.Any(sessao => sessao.Cinema.Nome == nomeCinema)).ToList());

    }

    /// <summary>
    /// Exibir lista do filme selecionado por ID
    /// </summary>
    /// <param name="id">Objeto necessarios para a busca do filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso.</response>
    [HttpGet("{id}")]// Só é executado caso passe o ID
    public IActionResult RecuperarFilmePorId(int id)//? --> pode ter UM DADO ou ser NULO
    {
        var filme= _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();//Arquitetura REST(Ex: NotFound,Ok)
        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
        return Ok(filmeDto);
    }

    /// <summary>
    /// Atualizar informações do filme
    /// </summary>
    /// <param name="id">Objeto necessarios para a atualização do filme</param>
    /// <param name="filmeDto">Objeto necessarios para informar quais dados seram atualizados</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso atualização seja feita com sucesso.</response>
    [HttpPut("{id}")]
    public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);//Comparação

        if (filme == null) return NotFound();
        _mapper.Map(filmeDto,filme);
        _context.SaveChanges();
        return NoContent();        
    }

    /// <summary>
    /// Atualizar o filme parsialmente
    /// </summary>
    /// <param name="id">Objeto necessarios para informar o filme</param>
    /// <param name="patch">Objeto necessarios para informar quais dados seram atualizados</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a consulta seja feita com sucesso.</response>
    [HttpPatch("{id}")]
    public IActionResult AtualizarFilmeParsial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);//Comparação

        if (filme == null) return NotFound();

        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Deletar flme do B.D
    /// </summary>
    /// <param name="id">Objeto necessarios para selecionar o filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso o delete seja feita com sucesso.</response>
    [HttpDelete("{id}")]
    public IActionResult DeletaFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);//Comparação

        if (filme == null) return NotFound();
        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}
