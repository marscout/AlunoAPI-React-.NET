using AlunoApi.Models;
using AlunoApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AlunosController : ControllerBase
    {
        private IAlunoService _alunoService;

        public AlunosController(IAlunoService service)
        {
            _alunoService = service;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                var alunos = await _alunoService.GetAlunos();
                return Ok(alunos);
            }
            catch
            {
                //return BadRequest("Request Inválida");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunoByNome([FromQuery] string nome)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nome)) return BadRequest("Nome inválido");
                var aluno = await _alunoService.GetAlunoByNome(nome);
                if (aluno.Count() == 0) return NotFound("Aluno não encontrado");
                return Ok(aluno);
            }
            catch
            {
                //return BadRequest("Request Inválida");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{id:int}", Name = "GetAluno")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            try
            {
                if (id < 0) return BadRequest("Valor do Id é inválido");
                var aluno = await _alunoService.GetAluno(id);
                if (aluno == null) return NotFound("Aluno não encontrado");
                return Ok(aluno);
            }
            catch
            {
                //return BadRequest("Request Inválida");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(Aluno aluno)
        {
            try
            {
                await _alunoService.CreateAluno(aluno);
                return CreatedAtAction(nameof(GetAluno), new { Id = aluno.Id }, aluno);
            }
            catch
            {
                return BadRequest("Request Inválida");
                //return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id,Aluno aluno)
        {
            try
            {
                if(id == aluno.Id)
                {
                    await _alunoService.UpdateAluno(aluno);

                    return Ok($"Aluno com Id:{id} foi alterado.");
                }
                else
                {
                    return BadRequest("Dados inconsistentes");
                }
            }
            catch
            {
                return BadRequest("Request Inválida");
                //return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if(aluno != null)
                {
                    await _alunoService.DeleteAluno(aluno);
                    return Ok($"Aluno com Id:{id} foi excluido.");
                }
                else
                {
                    return NotFound("Aluno não encontrado");
                }

            }
            catch
            {
                return BadRequest("Request Inválida");
                //return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}