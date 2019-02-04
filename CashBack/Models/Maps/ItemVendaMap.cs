using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashBack.Models.Maps
{
    public class ItemVendaMap : IEntityTypeConfiguration<ItemVenda>
    {
        public void Configure(EntityTypeBuilder<ItemVenda> builder)
        {
            builder.HasKey(p => p.ID);
            builder.Property(p => p.ID).IsRequired().UseSqlServerIdentityColumn();
            builder.Property(p => p.VendaID).IsRequired();
            builder.HasOne(p => p.Venda).WithMany(p => p.Itens).HasForeignKey(p => p.VendaID).HasPrincipalKey(p=>p.VendaID);            
        }
    }
}
