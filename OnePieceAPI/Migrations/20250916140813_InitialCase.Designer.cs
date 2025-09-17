using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OnePieceAPI.Data;

#nullable disable

namespace OnePieceAPI.Migrations
{
    [DbContext(typeof(OnePieceContext))]
    [Migration("20250916140813_InitialCase")]
    partial class InitialCase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OnePieceAPI.Models.Entities.FrutaDelDiablo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.ToTable("FrutasDelDiablo");
                });

            modelBuilder.Entity("OnePieceAPI.Models.Entities.Pirata", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int?>("FrutaDelDiabloId")
                        .HasColumnType("integer");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<long>("Recompensa")
                        .HasColumnType("bigint");

                    b.Property<int?>("TripulacionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FrutaDelDiabloId");

                    b.HasIndex("TripulacionId");

                    b.ToTable("Piratas");
                });

            modelBuilder.Entity("OnePieceAPI.Models.Entities.Tripulacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CapitanId")
                        .HasColumnType("integer");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<long>("RecompensaTotal")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CapitanId");

                    b.ToTable("Tripulaciones");
                });

            modelBuilder.Entity("OnePieceAPI.Models.Entities.Pirata", b =>
                {
                    b.HasOne("OnePieceAPI.Models.Entities.FrutaDelDiablo", "FrutaDelDiablo")
                        .WithMany()
                        .HasForeignKey("FrutaDelDiabloId");

                    b.HasOne("OnePieceAPI.Models.Entities.Tripulacion", "Tripulacion")
                        .WithMany("Miembros")
                        .HasForeignKey("TripulacionId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("FrutaDelDiablo");

                    b.Navigation("Tripulacion");
                });

            modelBuilder.Entity("OnePieceAPI.Models.Entities.Tripulacion", b =>
                {
                    b.HasOne("OnePieceAPI.Models.Entities.Pirata", "Capitan")
                        .WithMany()
                        .HasForeignKey("CapitanId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Capitan");
                });

            modelBuilder.Entity("OnePieceAPI.Models.Entities.Tripulacion", b =>
                {
                    b.Navigation("Miembros");
                });
#pragma warning restore 612, 618
        }
    }
}
