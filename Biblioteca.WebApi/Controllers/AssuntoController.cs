using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model; // Certifique-se de que a referência ao seu modelo está correta
using Model.DTOs;
using Service.IService;
using Service.IServices; // Corrigindo para o seu namespace Service.IServices

namespace Biblioteca.WebApi.Controllers
{
    [ApiController] // Importante para que o Controller saiba lidar com APIs
    [Route("api/[controller]")] // Rota base será: /api/Assunto
    public class AssuntoController : ControllerBase // Use ControllerBase para APIs
    {
        private readonly IAssuntoService _service;

        // Injeção de Dependência do Serviço
        public AssuntoController(IAssuntoService service)
        {
            _service = service;
        }

        // -----------------------------------------------------------------
        // 1. GET (READ ALL) - Rota: GET api/Assunto
        // -----------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssuntoDto>>> Get()
        {
            var assuntos = await _service.GetAll();
            return Ok(assuntos);
        }

        // -----------------------------------------------------------------
        // 2. GET BY ID (READ SINGLE) - Rota: GET api/Assunto/5
        // -----------------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult<AssuntoDto>> Get(int id)
        {
            var assunto = await _service.GetById(id);

            if (assunto == null)
            {
                // Retorna 404 Not Found se o recurso não existir
                return NotFound();
            }

            return Ok(assunto);
        }

        // -----------------------------------------------------------------
        // 3. POST (CREATE) - Rota: POST api/Assunto
        // -----------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<AssuntoDto>> Post([FromBody] AssuntoDto assunto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna 400 Bad Request se o modelo for inválido
            }

            var novoAssunto = await _service.Create(assunto);

            // Retorna 201 Created com o recurso criado e o link para ele
            return CreatedAtAction(nameof(Get), new { id = novoAssunto.AssuntoId }, novoAssunto);
        }

        // -----------------------------------------------------------------
        // 4. PUT (UPDATE) - Rota: PUT api/Assunto/5
        // -----------------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AssuntoDto assunto)
        {
            // A ID na rota deve ser igual à ID no corpo da requisição
            if (id != assunto.AssuntoId)
            {
                return BadRequest("O ID na URL e o ID do corpo da requisição não correspondem.");
            }

            try
            {
                var assuntoAtualizado = await _service.Update(assunto);

                // Retorna 204 No Content para uma atualização bem-sucedida,
                // ou 200 OK se preferir retornar a entidade atualizada (assuntoAtualizado).
                return Ok(assuntoAtualizado);
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
                DeleteResult.Success => Ok(new { message = $"Assunto com ID {id} excluído com sucesso." }),
                DeleteResult.NotFound => NotFound(new { message = $"Erro: Assunto com ID {id} não foi encontrado." }),
                _ => StatusCode(500, new { message = "Ocorreu um erro interno na exclusão." })
            };
        }
    }
}