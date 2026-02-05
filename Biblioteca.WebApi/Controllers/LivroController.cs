using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.DTOs;
using Repositorio.Data;
using Service.IService;
using Service.IServices;

namespace Biblioteca.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _service;
        private readonly AppDbContexto _context;

        // Injeção de Dependência do Serviço
        public LivroController(ILivroService service, AppDbContexto appDbContext)
        {
            _service = service;
            _context = appDbContext;        }

        // -----------------------------------------------------------------
        // 1. GET (READ ALL) - Rota: GET api/Assunto
        // -----------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroDto>>> Get()
        {
            var livro = await _service.GetAll();
            return Ok(livro);
        }

        // -----------------------------------------------------------------
        // 2. GET BY ID (READ SINGLE) - Rota: GET api/Assunto/5
        // -----------------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult<LivroDto>> Get(int id)
        {
            var livro = await _service.GetById(id);

            if (livro == null)
            {
                // Retorna 404 Not Found se o recurso não existir
                return NotFound();
            }
            return Ok(livro);
        }

        // -----------------------------------------------------------------
        // 3. POST (CREATE) - Rota: POST api/Assunto
        // -----------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<LivroDto>> Post([FromBody] LivroDto livro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna 400 Bad Request se o modelo for inválido
            }

            var novaLivro = await _service.Create(livro);

            // Retorna 201 Created com o recurso criado e o link para ele
            return CreatedAtAction(nameof(Get), new { id = novaLivro.LivroId }, novaLivro);
        }

        // -----------------------------------------------------------------
        // 4. PUT (UPDATE) - Rota: PUT api/Assunto/5
        // -----------------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] LivroDto livro)
        {
            // A ID na rota deve ser igual à ID no corpo da requisição
            if (id != livro.LivroId)
            {
                return BadRequest("O ID na URL e o ID do corpo da requisição não correspondem.");
            }

            try
            {
                var livroAtualizado = await _service.Update(livro);

                // Retorna 204 No Content para uma atualização bem-sucedida,
                // ou 200 OK se preferir retornar a entidade atualizada (assuntoAtualizado).
                return Ok(livroAtualizado);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // Retorna 404 se a entidade não foi encontrada pelo serviço
            }
            catch (Exception ex)
            {
                // Captura outras exceções do serviço ou da validação
                return StatusCode(500, ex.Message);
            }
        }

        // -----------------------------------------------------------------
        // 5. DELETE - Rota: DELETE api/Assunto/5
        // -----------------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // // O serviço trata de verificar se a entidade existe e a exclui.
            // var sucesso = await _service.Delete(id);

            // // Mesmo que o ID não exista, a operação é considerada um sucesso HTTP,
            // // pois o estado final (recurso inexistente) é alcançado.
            // return NoContent(); // Retorna 204 No Content para uma exclusão bem-sucedida
            var result = await _service.Delete(id);

            return result switch
            {
                DeleteResult.Success => Ok(new { message = $"Livro com ID {id} excluído com sucesso." }),
                DeleteResult.NotFound => NotFound(new { message = $"Erro: Livro com ID {id} não foi encontrado." }),
                _ => StatusCode(500, new { message = "Ocorreu um erro interno na exclusão." })
            };
        }

    }
}