using Lanches.Models;
using Lanches.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lanches.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Pedido pedido) 
        {
            int totalItensPedido = 0;
            decimal preocoTotalPedido = 0.0m;

            //OBTEM OS ITENS DO CARRINHO DE COMPRA DO CLIENTE
            List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItems();
            _carrinhoCompra.CarrinhoCompraItens = items;

            //VERIFICA SE EXISTEM ITENS DE PEDIDO
            if (_carrinhoCompra.CarrinhoCompraItens.Count == 0) 
            {
                ModelState.AddModelError("", "Seu carrinho esta vazio,que tal incluir um lanche...");
            }

            //CALCULAR O TOTAL DE ITENS E O TOTAL DO PEDIDO
            foreach (var item in items) 
            {
                totalItensPedido += item.Quantidade;
                preocoTotalPedido += (item.Lanche.Preco * item.Quantidade);
            }

            //ATRIBUIR OS VALORES OBTIDOS AO PEDIDO
            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = preocoTotalPedido;

            //VALIDA OS DADOS DO PEDIDO
            if (ModelState.IsValid) 
            {
                //CRIAR OS PEDIDOS E OS DETALHES
                _pedidoRepository.CriarPedido(pedido);

                //DEFINE MENSAGENS AO CLIENTE
                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                //LIMPA CARRINHO DO CLIENTE
                _carrinhoCompra.LimparCarrino();

                //EXIBE A VIEW COM DADOS DO CLIENTE E DO PEDIDO
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }
            return View();
        }  
    }
}
