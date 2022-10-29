using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tech_test_payment_api.models
{
    public class Item
    {
        public int Id {get; set;}
        public string Nome {get; set;}
        public int Quantidade {get; set;}
        public decimal Valor {get; set;}

        public int VendaID {get; set;}
    }
}