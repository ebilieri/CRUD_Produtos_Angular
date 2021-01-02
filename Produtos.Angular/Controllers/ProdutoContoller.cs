using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GroceryShop.Dominio.Contratos;
using GroceryShop.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GroceryShop.Angular.Controllers
{
    /// <summary>
    /// Controller para manipular Produtos
    /// </summary>
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _hostingEnvironment;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="produtoRepositorio"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="hostingEnvironment"></param>
        public ProdutoController(IProdutoRepositorio produtoRepositorio,
                                    IHttpContextAccessor httpContextAccessor,
                                    IWebHostEnvironment hostingEnvironment)
        {
            _produtoRepositorio = produtoRepositorio;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
        }

        private List<string> ObterListaErros(Exception ex)
        {
            var erros = new List<string>
            {
                ex.Message
            };
            if (ex.InnerException != null)
                erros.Add(ex.InnerException.Message);

            return erros;
        }

        /// <summary>
        /// Obter produtos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_produtoRepositorio.ObterTodos());
            }
            catch (Exception ex)
            {
                return BadRequest(ObterListaErros(ex));
            }
        }

        /// <summary>
        /// Deletar produtos por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var produto = _produtoRepositorio.ObterPorId(id);
                if (produto == null)
                {
                    return NotFound();
                }

                _produtoRepositorio.Remover(produto);
                // retornar lista atualizada
                return Ok(_produtoRepositorio.ObterTodos());
            }
            catch (Exception ex)
            {
                return BadRequest(ObterListaErros(ex));
            }
        }

        /// <summary>
        /// Salvar produto
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] Produto produto)
        {
            try
            {
                produto.Validate();
                if (!produto.EhValido)
                {
                    return BadRequest(produto.ObterMensageValidacao());
                }

                if (produto.Id > 0)
                {
                    _produtoRepositorio.Atualizar(produto);
                    return Ok(produto);
                }
                else
                {
                    _produtoRepositorio.Adicionar(produto);
                    return Created("api/produto", produto);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ObterListaErros(ex));
            }
        }

        /// <summary>
        /// Salvarf imagem produto
        /// </summary>
        /// <returns></returns>
        [HttpPost("EnviarArquivo")]
        public IActionResult EnviarArquivo()
        {
            try
            {
                var formFile = _httpContextAccessor.HttpContext.Request.Form.Files["arquivoEnviado"];
                var nomeArquivo = formFile.FileName;
                var extensao = nomeArquivo.Split(".").Last();
                var arraryNomeArquivo = Path.GetFileNameWithoutExtension(nomeArquivo).Take(10).ToArray();
                var novoNomeArquivo = new string(arraryNomeArquivo).Replace(" ", "_") + $"_{Guid.NewGuid()}.{extensao}";
                var pastaArquivos = _hostingEnvironment.WebRootPath + "\\arquivos\\";
                var nomeCompleto = pastaArquivos + novoNomeArquivo;

                using (var streamFile = new FileStream(nomeCompleto, FileMode.Create))
                {
                    formFile.CopyTo(streamFile);
                }

                return Json(novoNomeArquivo);
            }
            catch (Exception ex)
            {
                return BadRequest(ObterListaErros(ex));
            }
        }
    }
}
