using BookingManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingManager.DAL.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Room");

            builder.Property(b => b.ImageUrl)
                .HasColumnName("image")
                .HasColumnType("varchar(max)");

            builder.HasIndex(b => b.ImageUrl).IsUnique();

            // configuration de la table intermédiaire
            builder.HasMany(r => r.Options)
                .WithMany(o => o.Rooms)
                //.UsingEntity(
                //    "RoomOption",
                //    l => l.HasOne(typeof(Room)).WithMany().HasForeignKey("RoomId")
                //        .HasPrincipalKey(nameof(Room.RoomId)),
                //    r => r.HasOne(typeof(Option)).WithMany().HasForeignKey("OptionId")
                //        .HasPrincipalKey(nameof(Option.OptionId))
                //);
                //.UsingEntity(
                //    "RoomOption",
                //    j => {
                //        j.Property("RoomId").HasColumnName("RoomId");
                //        j.Property("OptionId").HasColumnName("OptionId");
                //    }
                //);
                ;
        }
    }
}
