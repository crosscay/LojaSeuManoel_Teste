using Xunit;
using LojaSeuManoel.Application.Interfaces;
using LojaSeuManoel.Domain.Models;
using LojaSeuManoel.Domain.Services;
using System.Collections.Generic;

namespace LojaSeuManoel.Tests
{
    public class EmpacotamentoServiceTests
    {
        private readonly IEmpacotamentoService _empacotamentoService;

        public EmpacotamentoServiceTests()
        {
            _empacotamentoService = new EmpacotamentoService();
        }

        [Fact]
        public void EmpacarPedidos_PedidoConProducto_CreaSalidaConCaixa()
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

            // Act
            var resultado = _empacotamentoService.EmpacotarPedido(pedidoEntrada);

            // Assert
            Assert.NotNull(resultado);
            Assert.Single(resultado.Pedidos);
            Assert.NotEmpty(resultado.Pedidos[0].Caixas);
        }

        [Fact]
        public void EmpacarPedidos_ProductoQueNoCabe_RetornaObservacion()
        {
            // Arrange
            var pedidoEntrada = new PedidoEntrada
            {
                Pedidos = new List<Pedido>
                {
                    new Pedido
                    {
                        PedidoId = 2,
                        Produtos = new List<ProdutoEntrada>
                        {
                            new ProdutoEntrada { ProdutoId = "2", Dimensoes = new Dimensoes { Altura = 100, Largura = 200, Comprimento = 300 } }
                        }
                    }
                }
            };

            // Act
            var resultado = _empacotamentoService.EmpacotarPedido(pedidoEntrada);

            // Assert
            Assert.NotNull(resultado);
            Assert.Single(resultado.Pedidos);
            Assert.Single(resultado.Pedidos[0].Caixas);
            Assert.Equal("Produto não cabe em nenhuma caixa disponível.", resultado.Pedidos[0].Caixas[0].Observacao);
        }
    }
}