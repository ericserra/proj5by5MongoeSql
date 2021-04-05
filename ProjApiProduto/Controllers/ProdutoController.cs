using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjApiProduto.Models;
using ProjApiProduto.Services;

namespace ProjApiProduto.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;            
        }

        [HttpGet]
        public ActionResult<List<Produto>> Get() =>
            _produtoService.Get();
        
        [HttpGet("{id:length(24)}", Name = "GetCliente")]
        public ActionResult<Produto> Get(string id)
        {
            var produto = _produtoService.Get(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        [HttpPost]
        public ActionResult<Produto> Create(Produto produto)
        {
            _produtoService.Create(produto);

            return CreatedAtRoute("GetCliente", new { id = produto.Id.ToString() }, produto);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Produto produtoIn)
        {
            var produto = _produtoService.Get(id);

            if (produto == null)
            {
                return NotFound();
            }

            _produtoService.Update(id, produtoIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var produto = _produtoService.Get(id);

            if (produto == null)
            {
                return NotFound();
            }

            _produtoService.Remove(produto.Id);

            return NoContent();
        }
    }
}