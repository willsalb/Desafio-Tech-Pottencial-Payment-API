using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tech_test_payment_api.Context;
using tech_test_payment_api.models;
using Microsoft.AspNetCore.Mvc;

namespace tech_test_payment_api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class VendaController : ControllerBase
  {
    private readonly PagamentosContext __context;

    public VendaController(PagamentosContext context)
    {
      __context = context;
    }

    [HttpGet("{id}")]
    public IActionResult ObterPorId(int id)
    {
      var venda = __context.Vendas.Find(id);

      if (venda == null)
        return NotFound();

      var vendedor = __context.Vendedores.Find(Venda.VendedorId);
      if (vendedor == null)
        return NotFound();

      var itens = __context.Items.Where(i => i.VendaID == venda.Id).ToList();

      Facade.Venda.Response.Venda fVenda = new Facade.Venda.Response.Venda()
      {
        VendaID = venda.Id,
        Data = venda.Data,
        Status = venda.Status,
        Vendedor = new Facade.Venda.Vendedor()
        {
          Nome = vendedor.Nome,
          CPF = vendedor.CPF,
          Email = vendedor.Email,
          Fone = vendedor.Fone,
        }
      };

      foreach (Item item in itens)
      {
        fVenda.Items.Add(new Facade.Venda.Item()
        {
          Nome = item.Nome,
          Quantidade = item.Quantidade,
          Valor = item.Valor
        });
      }

      return Ok(fVenda);
    }

    [HttpPost]
    public IActionResult Criar(Facade.Venda.Request.CriarVenda venda)
    {
      List<string> error = new List<string>();
      if (venda != null)
      {
        if (venda.Vendedor != null && venda.Vendedor.Nome.Length > 0 && venda.Vendedor.CPF.Length > 0 && venda.Vendedor.Fone.Length > 0)
        {
          if (venda.Items.Count == 0)
          {
            error.Add("Informe pelo meno um item vendido!");
          }
        }
        else
        {
          error.Add("Informe os dados do vendedor!!");
        }
      }
      else
      {
        error.Add("Parâmetros não informados!!");
      }

      if (error.Count > 0)
      {
        return BadRequest(new { Erro = string.Join(", ", error) });
      }
      else
      {
        Venda vendaBanco = new Venda()
        {
          Data = venda.Data,
          Status = "Finalizado",

          Vendedor = new Vendedor()
          {
            Nome = venda.Vendedor.Nome,
            CPF = venda.Vendedor.CPF.Replace(" ", "").Replace(".", "").Replace("-", ""),
            Email = venda.Vendedor.Email,
            Fone = venda.Vendedor.Fone
          }
        };

        __context.Vendas.Add(vendaBanco);
        __context.SaveChanges();

        foreach (Facade.Venda.Item item in venda.Items)
        {
          __context.Itens.Add(new Item()
          {
            Nome = Item.Nome,
            Quantidade = Item.Quantidade,
            Valor = Item.Valor,
            VendaID = vendaBanco.Id
          });
        }

        __context.SaveChanges();

        return Ok(vendaBanco.Id);
      }
    }

    [HttpPost("AlterarStatus")]
    public IActionResult AlterarStatus(Facade.Venda.Request.AlterarStatus req)
    {
      var Venda = __context.Vendas.Find(req.VendaID);
      if (Venda == null)
        return NotFound("Id não encontrado");

      switch (req.Status)
      {
        case "Pagamento Aprovado":
          if (!Venda.Status.Equals("Aguardando pagamento"))
          {
            return BadRequest(new { Erro = "Sua venda precisa estar no status [Aguardando pagamento] para poder ser alterada para este novo status" });
          }
          else
          {
            Venda.Status = req.Status;
          }
          break;

        case "Enviando para transportadora":
          if (!Venda.Status.Equals("Pagamento aprovado"))
          {
            return BadRequest(new { Erro = "Sua venda precisa estar no status [Enviado para a transportadora] para poder ser alterada para este novo status" });
          }
          else
          {
            Venda.Status = req.Status;
          }
          break;

        case "Entregue":
          if (!Venda.Status.Equals("Enviado para a transportadora"))
          {
            return BadRequest(new { Erro = "Sua venda precisa estar no status [Enviado para a transportadora] para poder ser alterada para este novo status" });
          }
          else
          {
            Venda.Status = req.Status;
          }
          break;

        case "Cancelada":
          if (!Venda.status.Equals("Enviado para a transportadora"))
          {
            return BadRequest(new { Erro = "Como esta venda já está na transportadora, ela não pode ser cancelada" });
          }
          else
          {
            Venda.status = req.Status;
          }
          break;

        default:
          return BadRequest(new { Erro = "Informe um estatus válido: [Pagamento aprovado, Enviado para a transportadora, Entregue ou Cancelada]" });
      }

      __context.Vendas.Update(venda);
      __context.SaveChanges();

      return Ok(venda);
    }

  }
}