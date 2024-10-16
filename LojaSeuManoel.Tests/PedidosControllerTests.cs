using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using LojaSeuManoel.WebApi.Controllers;
using LojaSeuManoel.Application.Interfaces;
using LojaSeuManoel.Domain.Models;
using System.Collections.Generic;

namespace LojaSeuManoel.Tests
{
    public class PedidosControllerTests
    {
        private readonly Mock<IEmpacotamentoService> _mockEmpacotamentoService;
        private readonly PedidosController _controller;

        public PedidosControllerTests()
        {
            _mockEmpacotamentoService = new Mock<IEmpacotamentoService>();
            _controller = new PedidosController(_mockEmpacotamentoService.Object);
        }
        

        [Fact]
        public void ProcessarPedidos_EntradaValida_RetornaOk()
        {
            // Arrange
            var pedidoEntrada = new PedidoEntrada
            {
                Pedidos = new List<Pedido>
                {
                    new Pedido
                    {
                        PedidoId = 1,
                        Produtos = new List<ProdutoEntrada>
                        {
                            new ProdutoEntrada { ProdutoId = "1", Dimensoes = new Dimensoes { Altura = 10, Largura = 20, Comprimento = 30 } }
                        }
                    }
                }
            };

            var resultadoSaida = new PedidoSaida
            {
                Pedidos = new List<ResultadoPedido>
                {
                    new ResultadoPedido { PedidoId = 1, Caixas = new List<CaixaSalida>() }
                }
            };

            _mockEmpacotamentoService.Setup(s => s.EmpacarPedidos(pedidoEntrada)).Returns(resultadoSaida);

            // Act
            var result = _controller.ProcessarPedidos(pedidoEntrada) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(resultadoSaida, result.Value);
        }

        [Fact]
        public void ProcessarPedidos_PedidoVacio_RetornaBadRequest()
        {
            // Act
            var result = _controller.ProcessarPedidos(null) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("O pedido está vazio ou mal formatado.", result.Value);
        }

        [Fact]
        public void ProcessarPedidos_PedidoSinProductos_RetornaBadRequest()
        {
            // Arrange
            var pedidoEntrada = new PedidoEntrada
            {
                Pedidos = new List<Pedido>
                {
                    new Pedido
                    {
                        PedidoId = 1,
                        Produtos = null
                    }
                }
            };

            // Act
            var result = _controller.ProcessarPedidos(pedidoEntrada) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("O pedido contém produtos inválidos ou está vazio.", result.Value);
            // Assert.Equal("O pedido está vazio ou mal formatado.", result.Value);
        }
    }
}
