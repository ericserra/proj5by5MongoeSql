using Microsoft.EntityFrameworkCore;
using ProjApiCliente.Models;

namespace ProjApiCliente.Dal
{
    public class ClienteContext : DbContext
    {
        public ClienteContext(DbContextOptions<ClienteContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Cliente { get; set; }
    }
}