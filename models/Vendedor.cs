using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tech_test_payment_api.models 
{
    public class Vendedor
    {
        public int Id {get; set;}
        public string Nome {get; set;}
        public string CPF {get; set;}
        public string Email {get; set;}
        public string Fone {get; set;}

        public List<Venda> vendas {get; set;}
    }
}