using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tech_test_payment_api.models;

namespace tech_test_payment_api.Facade
{
    public class Venda
    {
        public class Request
        {
            public class CriarVenda
            {
                public Vendedor Vendedor {get; set;}
                public DateTime Data {get; set;}
                public List<Item> Items {get; set;}
            }

            public class AlterarStatus
            {
                public int VendaID {get; set;}
                public string Status {get; set;}
            }
        }

        public class Response
        {
            public class Venda
            {
                public int VendaID {get; set;}
                public DateTime Data {get; set;}
                public string Status {get; set;}
                public Vendedor Vendedor {get; set;} = new Vendedor();
                public List<Item> Items {get; set;} = new List<Item>();
            }
        }

        public class Item
        {
            public string Nome {get; set;}
            public int Quantidade {get; set;}
            public decimal Valor {get; set;}
        }

        public class Vendedor
        {
            public string Nome {get; set;}
            public string CPF {get; set;}
            public string Email {get; set;}
            public string Fone {get; set;}
        }
    }
}