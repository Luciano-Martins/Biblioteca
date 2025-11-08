using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTOs;
using Service.IService;
using Service.IServices;

namespace Biblioteca.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _service;

        // Injeção de Dependência do Serviço
        public AutorController(IAutorService service)
        {
            _service = service;
        }

        // -----------------------------------------------------------------
        // 1. GET (READ ALL) - Rota: GET api/Assunto
        // -----------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDto>>> Get()
        {
            var autor = await _service.GetAll();
            return Ok(autor);
        }

        // -----------------------------------------------------------------
        // 2. GET BY ID (READ SINGLE) - Rota: GET api/Assunto/5
        // -----------------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDto>> Get(int id)
        {
            var autor = await _service.GetById(id);

            if (autor == null)
            {
                // Retorna 404 Not Found se o recurso não existir
                return NotFound();
            }
            return Ok(autor);
        }

        // -----------------------------------------------------------------
        // 3. POST (CREATE) - Rota: POST api/Assunto
        // -----------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<AutorDto>> Post([FromBody] AutorDto autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna 400 Bad Request se o modelo for inválido
            }

            var novaAutor = await _service.Create(autor);

            // Retorna 201 Created com o recurso criado e o link para ele
            return CreatedAtAction(nameof(Get), new { id = novaAutor.AutorId }, novaAutor);
        }

        // -----------------------------------------------------------------
        // 4. PUT (UPDATE) - Rota: PUT api/Assunto/5
        // -----------------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AutorDto autor)
        {
            // A ID na rota deve ser igual à ID no corpo da requisição
            if (id != autor.AutorId)
            {
                return BadRequest("O ID na URL e o ID do corpo da requisição não correspondem.");
            }

            try
            {
                var autorAtualizado = await _service.Update(autor);

                // Retorna 204 No Content para uma atualização bem-sucedida,
                // ou 200 OK se preferir retornar a entidade atualizada (assuntoAtualizado).
                return Ok(autorAtualizado);
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
            var result = await _service.Delete(id);
            return result switch
            {
                DeleteResult.Success => Ok(new { message = $"Autor com ID {id} excluído com sucesso." }),
                DeleteResult.NotFound => NotFound(new { message = $"Erro: Autor com ID {id} não foi encontrado." }),
                _ => StatusCode(500, new { message = "Ocorreu um erro interno na exclusão." })
            };
        }
    }
}