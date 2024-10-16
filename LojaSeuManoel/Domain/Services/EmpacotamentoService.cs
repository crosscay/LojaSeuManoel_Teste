using LojaSeuManoel.Application.Interfaces;
using LojaSeuManoel.Domain.Models;

namespace LojaSeuManoel.Domain.Services
{
    public class EmpacotamentoService : IEmpacotamentoService // Implementa a interface
    {
        private readonly List<Caixa> caixasDisponiveis;

        public EmpacotamentoService()
        {
            caixasDisponiveis = new List<Caixa>
            {
                new Caixa { Nome = "Caixa 1", Altura = 30, Largura = 40, Comprimento = 80 },
                new Caixa { Nome = "Caixa 2", Altura = 80, Largura = 50, Comprimento = 40 },
                new Caixa { Nome = "Caixa 3", Altura = 50, Largura = 80, Comprimento = 60 }
            };
        }

        public PedidoSaida EmpacotarPedido(PedidoEntrada pedidoEntrada)
        {
            var resultado = new PedidoSaida { Pedidos = new List<ResultadoPedido>() };

            foreach (var pedido in pedidoEntrada.Pedidos)
            {
                var resultadoPedido = new ResultadoPedido { PedidoId = pedido.PedidoId, Caixas = new List<CaixaSaida>() };

                // Calcular o volume de cada produto
                var produtosComVolume = pedido.Produtos
                    .Select(p => new { Produto = p, Volume = p.Dimensoes.Altura * p.Dimensoes.Largura * p.Dimensoes.Comprimento })
                    .OrderByDescending(p => p.Volume) // Ordenar produtos por volume (do maior para o menor).
                    .ToList();

                var caixasUsadas = new List<Caixa>();

                // Tentar empacotar os produtos otimizando o uso do espaço nas caixas.
                foreach (var produtoComVolume in produtosComVolume)
                {
                    bool empacado = false;

                    // Tentar empacotar nas caixas existentes.
                    foreach (var caixa in caixasUsadas)
                    {
                        if (PodeEmpacotarProdutoNaCaixa(produtoComVolume, caixa))
                        {
                            AdicionarProdutoNaCaixa(resultadoPedido, caixa, produtoComVolume.Produto.ProdutoId);
                            empacado = true;
                            break;
                        }
                    }

                    // Se não for possível empacotar em nenhuma caixa usada, procurar uma nova.
                    if (!empacado)
                    {
                        var caixaNueva = EncontrarCaixaParaProduto(produtoComVolume.Produto);
                        if (caixaNueva != null)
                        {
                            caixasUsadas.Add(caixaNueva);
                            AdicionarProdutoNaCaixa(resultadoPedido, caixaNueva, produtoComVolume.Produto.ProdutoId);
                        }
                        else
                        {
                            // Produto que não cabe em nenhuma caixa disponível.
                            resultadoPedido.Caixas.Add(new CaixaSaida
                            {
                                CaixaId = null,
                                Produtos = new List<string> { produtoComVolume.Produto.ProdutoId },
                                Observacao = "Produto não cabe em nenhuma caixa disponível."
                            });
                        }
                    }
                }

                resultado.Pedidos.Add(resultadoPedido);
            }

            return resultado;
        }

        private bool PodeEmpacotarProdutoNaCaixa(dynamic produtoComVolume, Caixa caixa)
        {
            return produtoComVolume.Produto.Dimensoes.Altura <= caixa.Altura &&
                produtoComVolume.Produto.Dimensoes.Largura <= caixa.Largura &&
                produtoComVolume.Produto.Dimensoes.Comprimento <= caixa.Comprimento;
        }

        private void AdicionarProdutoNaCaixa(ResultadoPedido resultadoPedido, Caixa caixa, string produtoId)
        {
            var caixaSaida = resultadoPedido.Caixas.FirstOrDefault(c => c.CaixaId == caixa.Nome);
            if (caixaSaida == null)
            {
                caixaSaida = new CaixaSaida
                {
                    CaixaId = caixa.Nome,
                    Produtos = new List<string>()
                };
                resultadoPedido.Caixas.Add(caixaSaida);
            }
            caixaSaida.Produtos.Add(produtoId);
        }

        private Caixa EncontrarCaixaParaProduto(ProdutoEntrada produto)
        {
            // Buscar a caixa que melhor se ajuste às dimensões do produto
            return caixasDisponiveis
                .Where(c => produto.Dimensoes.Altura <= c.Altura &&
                            produto.Dimensoes.Largura <= c.Largura &&
                            produto.Dimensoes.Comprimento <= c.Comprimento)
                .OrderBy(c => c.Altura * c.Largura * c.Comprimento) // Elegir la caja con menor volumen posible
                .FirstOrDefault();
        }


        // public List<CaixaSalida> EmpacotarPedido(Pedido pedido)
        // {
        //     List<CaixaSalida> caixasUsadas = new List<CaixaSalida>();
        //     List<ProdutoEntrada> produtosNaoEmpacotados = new List<ProdutoEntrada>(pedido.Produtos);

        //     while (produtosNaoEmpacotados.Any())
        //     {
        //         CaixaSalida melhorCaixa = null;
        //         List<ProdutoEntrada> produtosNaMelhorCaixa = null;
        //         double melhorEficiencia = 0;

        //         foreach (var caixa in caixasDisponiveis)
        //         {
        //             var (produtosNaCaixa, eficiencia) = EncaixarProdutos(caixa, produtosNaoEmpacotados);
        //             if (produtosNaCaixa.Any() && eficiencia > melhorEficiencia)
        //             {
        //                 melhorCaixa = new CaixaSalida
        //                 {
        //                     CaixaId = caixa.Nome,
        //                     Produtos = produtosNaCaixa.Select(p => p.ProdutoId).ToList()
        //                 };
        //                 produtosNaMelhorCaixa = produtosNaCaixa;
        //                 melhorEficiencia = eficiencia;
        //             }
        //         }

        //         if (melhorCaixa != null)
        //         {
        //             caixasUsadas.Add(melhorCaixa);
        //             foreach (var produto in produtosNaMelhorCaixa)
        //             {
        //                 produtosNaoEmpacotados.Remove(produto);
        //             }
        //         }
        //         else
        //         {
        //             // Producto que no cabe en ninguna caja
        //             var produtoSemCaixa = produtosNaoEmpacotados[0];
        //             caixasUsadas.Add(new CaixaSalida
        //             {
        //                 CaixaId = null,
        //                 Produtos = new List<string> { produtoSemCaixa.ProdutoId },
        //                 Observacao = "Produto não cabe em nenhuma caixa disponível."
        //             });
        //             produtosNaoEmpacotados.RemoveAt(0);
        //         }
        //     }

        //     return caixasUsadas;
        // }


        // private (List<ProdutoEntrada>, double) EncaixarProdutos(Caixa caixa, List<ProdutoEntrada> produtos)
        // {
        //     var produtosQueEncaixam = new List<ProdutoEntrada>();
        //     double volumeOcupado = 0;
        //     double volumeCaixa = caixa.Altura * caixa.Largura * caixa.Comprimento;

        //     foreach (var produto in produtos)
        //     {
        //         if (produto.Dimensoes.Altura <= caixa.Altura &&
        //             produto.Dimensoes.Largura <= caixa.Largura &&
        //             produto.Dimensoes.Comprimento <= caixa.Comprimento)
        //         {
        //             produtosQueEncaixam.Add(produto);
        //             volumeOcupado += produto.Dimensoes.Altura * produto.Dimensoes.Largura * produto.Dimensoes.Comprimento;
        //         }
        //     }

        //     double eficiencia = volumeOcupado / volumeCaixa;
        //     return (produtosQueEncaixam, eficiencia);
        // }

    }
}
