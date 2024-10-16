using LojaSeuManoel.Domain.Models;

namespace LojaSeuManoel.Application.Interfaces
{
    public interface IEmpacotamentoService
    {
        PedidoSaida EmpacotarPedido(PedidoEntrada pedido);
    }
}