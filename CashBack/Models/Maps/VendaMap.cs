using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashBack.Models.Maps
{
    public class VendaMap : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {            
            builder.HasKey(pk => pk.VendaID).HasName("VendaId");
            builder.Property(p => p.VendaID).IsRequired().UseSqlServerIdentityColumn();
            builder.HasMany(p => p.Itens).WithOne(p => p.Venda).HasForeignKey(p => p.VendaID).HasPrincipalKey(p => p.VendaID);
        }
    }
}
