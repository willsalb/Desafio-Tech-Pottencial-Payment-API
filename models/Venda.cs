using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tech_test_payment_api.models
{
    public class Venda
    {
        public int Id {get; set;}
        public DateTime Data {get; set;}
        public string Status {get; set;}
        public int VendedorId {get; set;}

        public List<Item> Itens {get; set;}
        public Vendedor Vendedor {get; set;}
    }
}