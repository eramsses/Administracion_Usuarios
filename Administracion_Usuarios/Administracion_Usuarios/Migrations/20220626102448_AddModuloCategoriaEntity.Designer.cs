﻿// <auto-generated />
using System;
using Administracion_Usuarios.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Administracion_Usuarios.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220626102448_AddModuloCategoriaEntity")]
    partial class AddModuloCategoriaEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Administracion_Usuarios.Data.Entities.Modulo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("Nombre")
                        .IsUnique();

                    b.ToTable("Modulos");
                });

            modelBuilder.Entity("Administracion_Usuarios.Data.Entities.ModuloCategoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ModuloCategoria");
                });

            modelBuilder.Entity("Administracion_Usuarios.Data.Entities.Operacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ModuloId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ModuloId");

                    b.HasIndex("Nombre")
                        .IsUnique();

                    b.ToTable("Operaciones");
                });

            modelBuilder.Entity("Administracion_Usuarios.Data.Entities.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstadoEnum")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Administracion_Usuarios.Data.Entities.RolOperacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("OperacionId")
                        .HasColumnType("int");

                    b.Property<int?>("RolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OperacionId");

                    b.HasIndex("RolId");

                    b.ToTable("RolOperaciones");
                });

            modelBuilder.Entity("Administracion_Usuarios.Data.Entities.Modulo", b =>
                {
                    b.HasOne("Administracion_Usuarios.Data.Entities.ModuloCategoria", "Categoria")
                        .WithMany("Modulos")
                        .HasForeignKey("CategoriaId");

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("Administracion_Usuarios.Data.Entities.Operacion", b =>
                {
                    b.HasOne("Administracion_Usuarios.Data.Entities.Modulo", "Modulo")
                        .WithMany("Operaciones")
                        .HasForeignKey("ModuloId");

                    b.Navigation("Modulo");
                });

            modelBuilder.Entity("Administracion_Usuarios.Data.Entities.RolOperacion", b =>
                {
                    b.HasOne("Administracion_Usuarios.Data.Entities.Operacion", "Operacion")
                        .WithMany()
                        .HasForeignKey("OperacionId");

                    b.HasOne("Administracion_Usuarios.Data.Entities.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId");

                    b.Navigation("Operacion");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("Administracion_Usuarios.Data.Entities.Modulo", b =>
                {
                    b.Navigation("Operaciones");
                });

            modelBuilder.Entity("Administracion_Usuarios.Data.Entities.ModuloCategoria", b =>
                {
                    b.Navigation("Modulos");
                });
#pragma warning restore 612, 618
        }
    }
}
