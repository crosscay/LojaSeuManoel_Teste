using Microsoft.AspNetCore.Mvc;
using LojaSeuManoel.Application.Interfaces;
using LojaSeuManoel.Domain.Models;

namespace LojaSeuManoel.WebApi.Controllers
{
    [ApiController]
    [Route("api/pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly IEmpacotamentoService _empacotamentoService;

        // Injeção de dependências através do construtor
        public PedidosController(IEmpacotamentoService empacotamentoService)
        {
            _empacotamentoService = empacotamentoService;
        }

        [HttpPost]
        public IActionResult ProcessarPedidos([FromBody] PedidoEntrada pedidoEntrada)
        {
            // Validação da entrada
            if (pedidoEntrada == null || pedidoEntrada.Pedidos == null || !pedidoEntrada.Pedidos.Any())
            {
                return BadRequest("O pedido está vazio ou mal formatado.");
            }

            // Validação de produtos em cada pedido.
            foreach (var pedido in pedidoEntrada.Pedidos)
            {
                if (pedido.Produtos == null || !pedido.Produtos.Any())
                {
                    return BadRequest("O pedido contém produtos inválidos ou está vazio.");
                }
            }

            // Chamada ao serviço de empacotamento.
            var resultadoEmpacotado = _empacotamentoService.EmpacotarPedido(pedidoEntrada);

            // Retornar a resposta como JSON
            return Ok(resultadoEmpacotado);
        }
        
    }
}