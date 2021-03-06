﻿// <auto-generated />
using System;
using ContactManager.Core.Repositories.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContactManager.Core.Repositories.DatabaseContext.Migrations
{
    [DbContext(typeof(ContactManagerDbContext))]
    [Migration("20180826083124_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932");

            modelBuilder.Entity("ContactManager.Core.Entities.PhoneBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("PhoneBooks");
                });

            modelBuilder.Entity("ContactManager.Core.Entities.PhoneBookEntry", b =>
                {
                    b.Property<int>("PhoneBookId");

                    b.Property<int>("PhoneEntryId");

                    b.Property<DateTime>("DateCreated");

                    b.HasKey("PhoneBookId", "PhoneEntryId");

                    b.HasIndex("PhoneEntryId");

                    b.ToTable("PhoneBookEntry");
                });

            modelBuilder.Entity("ContactManager.Core.Entities.PhoneEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("PhoneEntries");
                });

            modelBuilder.Entity("ContactManager.Core.Entities.PhoneBookEntry", b =>
                {
                    b.HasOne("ContactManager.Core.Entities.PhoneBook", "PhoneBook")
                        .WithMany("BookEntries")
                        .HasForeignKey("PhoneBookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ContactManager.Core.Entities.PhoneEntry", "PhoneEntry")
                        .WithMany("BookEntries")
                        .HasForeignKey("PhoneEntryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
