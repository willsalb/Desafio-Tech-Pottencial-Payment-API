using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tech_test_payment_api.models;
using Microsoft.EntityFrameworkCore;

namespace tech_test_payment_api.Context
{
    public class PagamentosContext: DbContext
    {
        public PagamentosContext(DbContextOptions<PagamentosContext> options) : base(options)
        {

        }

        public DbSet<Venda> Vendas {get; set;}
        public DbSet<Vendedor> Vendedores {get; set;}
        public DbSet<Item> Items {get; set;}
    }
}