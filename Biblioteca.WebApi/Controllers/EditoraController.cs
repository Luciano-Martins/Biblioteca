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
    public class EditoraController : ControllerBase
    {
        private readonly IEditoraService _service;

        // Injeção de Dependência do Serviço
        public EditoraController(IEditoraService service)
        {
            _service = service;
        }

        // -----------------------------------------------------------------
        // 1. GET (READ ALL) - Rota: GET api/Assunto
        // -----------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EditoraDto>>> Get()
        {
            var editora = await _service.GetAll();
            return Ok(editora);
        }

        // -----------------------------------------------------------------
        // 2. GET BY ID (READ SINGLE) - Rota: GET api/Assunto/5
        // -----------------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult<EditoraDto>> Get(int id)
        {
            var editora = await _service.GetById(id);

            if (editora == null)
            {
                // Retorna 404 Not Found se o recurso não existir
                return NotFound();
            }
            return Ok(editora);
        }

        // -----------------------------------------------------------------
        // 3. POST (CREATE) - Rota: POST api/Assunto
        // -----------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<EditoraDto>> Post([FromBody] EditoraDto editora)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna 400 Bad Request se o modelo for inválido
            }

            var novaEditora = await _service.Create(editora);

            // Retorna 201 Created com o recurso criado e o link para ele
            return CreatedAtAction(nameof(Get), new { id = novaEditora.EditoraId }, novaEditora);
        }

        // -----------------------------------------------------------------
        // 4. PUT (UPDATE) - Rota: PUT api/Assunto/5
        // -----------------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EditoraDto editora)
        {
            // A ID na rota deve ser igual à ID no corpo da requisição
            if (id != editora.EditoraId)
            {
                return BadRequest("O ID na URL e o ID do corpo da requisição não correspondem.");
            }
            try
            {
                var editoraAtualizado = await _service.Update(editora);
                // Retorna 204 No Content para uma atualização bem-sucedida,
                // ou 200 OK se preferir retornar a entidade atualizada (assuntoAtualizado).
                return Ok(editoraAtualizado);
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
                DeleteResult.Success => Ok(new { message = $"Editora com ID {id} excluído com sucesso." }),
                DeleteResult.NotFound => NotFound(new { message = $"Erro: Editora com ID {id} não foi encontrado." }),
                _ => StatusCode(500, new { message = "Ocorreu um erro interno na exclusão." })
            };
        }

    }
}