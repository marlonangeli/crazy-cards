﻿// <auto-generated />
using System;
using CrazyCards.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CrazyCards.Persistence.Migrations
{
    [DbContext(typeof(CrazyCardsDbContext))]
    [Migration("20230616030540_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CrazyCards.Domain.Entities.Card.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClassId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ManaCost")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                    b.Property<int>("Rarity")
                        .HasColumnType("int");

                    b.Property<Guid>("SkinId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("ImageId");

                    b.HasIndex("Name");

                    b.HasIndex("SkinId");

                    b.ToTable("Card", (string)null);

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Card.Class", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                    b.Property<Guid>("SkinId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("SkinId");

                    b.ToTable("Class", (string)null);
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Card.Hability.Hability", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("Hability", (string)null);

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Card.Hero", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClassId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<Guid>("SkinId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("WeaponId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("ImageId");

                    b.HasIndex("SkinId");

                    b.HasIndex("WeaponId");

                    b.ToTable("Hero", (string)null);
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Deck.BattleDeck", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("HeroId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<Guid>("PlayerDeckId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("HeroId");

                    b.HasIndex("PlayerDeckId");

                    b.ToTable("BattleDeck", (string)null);
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Deck.BattleDeckCard", b =>
                {
                    b.Property<Guid>("DeckId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CardId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DeckId", "CardId");

                    b.HasIndex("CardId");

                    b.ToTable("BattleDeckCard");
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Deck.PlayerDeck", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("PlayerDeck", (string)null);
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Player.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<Guid>("PlayerDeckId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Player", (string)null);
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Shared.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ImageType")
                        .HasColumnType("int");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Image", (string)null);

                    b.HasDiscriminator<int>("ImageType").HasValue(0);

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Card.MinionCard", b =>
                {
                    b.HasBaseType("CrazyCards.Domain.Entities.Card.Card");

                    b.Property<int>("Attack")
                        .HasColumnType("int");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Card.SpellCard", b =>
                {
                    b.HasBaseType("CrazyCards.Domain.Entities.Card.Card");

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<int>("Heal")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Card.TotenCard", b =>
                {
                    b.HasBaseType("CrazyCards.Domain.Entities.Card.Card");

                    b.Property<int>("Heal")
                        .HasColumnType("int");

                    b.Property<int>("Shield")
                        .HasColumnType("int");

                    b.ToTable("Card", t =>
                        {
                            t.Property("Heal")
                                .HasColumnName("TotenCard_Heal");
                        });

                    b.HasDiscriminator().HasValue(4);
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Card.WeaponCard", b =>
                {
                    b.HasBaseType("CrazyCards.Domain.Entities.Card.Card");

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<int>("Durability")
                        .HasColumnType("int");

                    b.Property<int>("Shield")
                        .HasColumnType("int");

                    b.ToTable("Card", t =>
                        {
                            t.Property("Damage")
                                .HasColumnName("WeaponCard_Damage");

                            t.Property("Shield")
                                .HasColumnName("WeaponCard_Shield");
                        });

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Card.Hability.BattlecryHability", b =>
                {
                    b.HasBaseType("CrazyCards.Domain.Entities.Card.Hability.Hability");

                    b.HasDiscriminator().HasValue(11);
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Card.Hability.LastBreathHability", b =>
                {
                    b.HasBaseType("CrazyCards.Domain.Entities.Card.Hability.Hability");

                    b.HasDiscriminator().HasValue(12);
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Card.Hability.TauntHability", b =>
                {
                    b.HasBaseType("CrazyCards.Domain.Entities.Card.Hability.Hability");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Card.Skin", b =>
                {
                    b.HasBaseType("CrazyCards.Domain.Entities.Shared.Image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Card.Card", b =>
                {
                    b.HasOne("CrazyCards.Domain.Entities.Card.Class", "Class")
                        .WithMany("Cards")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CrazyCards.Domain.Entities.Shared.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CrazyCards.Domain.Entities.Card.Skin", "Skin")
                        .WithMany()
                        .HasForeignKey("SkinId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Class");

                    b.Navigation("Image");

                    b.Navigation("Skin");
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Card.Class", b =>
                {
                    b.HasOne("CrazyCards.Domain.Entities.Shared.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CrazyCards.Domain.Entities.Card.Skin", "Skin")
                        .WithMany()
                        .HasForeignKey("SkinId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Image");

                    b.Navigation("Skin");
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Card.Hability.Hability", b =>
                {
                    b.HasOne("CrazyCards.Domain.Entities.Card.Card", "Card")
                        .WithMany("Habilities")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("CrazyCards.Domain.Entities.Card.Hability.Action", "Action", b1 =>
                        {
                            b1.Property<Guid>("HabilityId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int?>("Damage")
                                .HasColumnType("int");

                            b1.Property<bool>("DamageToAll")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bit")
                                .HasDefaultValue(false);

                            b1.Property<string>("Description")
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)");

                            b1.Property<int?>("Heal")
                                .HasColumnType("int");

                            b1.Property<bool>("HealToAll")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bit")
                                .HasDefaultValue(false);

                            b1.Property<Guid?>("InvokeCardId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int?>("InvokeCardType")
                                .HasColumnType("int");

                            b1.Property<int?>("Shield")
                                .HasColumnType("int");

                            b1.Property<bool>("ShieldToAll")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bit")
                                .HasDefaultValue(false);

                            b1.HasKey("HabilityId");

                            b1.HasIndex("InvokeCardId");

                            b1.ToTable("Hability");

                            b1.WithOwner()
                                .HasForeignKey("HabilityId");

                            b1.HasOne("CrazyCards.Domain.Entities.Card.Card", "InvokeCard")
                                .WithMany()
                                .HasForeignKey("InvokeCardId")
                                .OnDelete(DeleteBehavior.SetNull);

                            b1.Navigation("InvokeCard");
                        });

                    b.Navigation("Action");

                    b.Navigation("Card");
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Card.Hero", b =>
                {
                    b.HasOne("CrazyCards.Domain.Entities.Card.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CrazyCards.Domain.Entities.Shared.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CrazyCards.Domain.Entities.Card.Skin", "Skin")
                        .WithMany()
                        .HasForeignKey("SkinId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CrazyCards.Domain.Entities.Card.WeaponCard", "Weapon")
                        .WithMany()
                        .HasForeignKey("WeaponId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Class");

                    b.Navigation("Image");

                    b.Navigation("Skin");

                    b.Navigation("Weapon");
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Deck.BattleDeck", b =>
                {
                    b.HasOne("CrazyCards.Domain.Entities.Card.Hero", "Hero")
                        .WithMany()
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CrazyCards.Domain.Entities.Deck.PlayerDeck", "PlayerDeck")
                        .WithMany("BattleDecks")
                        .HasForeignKey("PlayerDeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hero");

                    b.Navigation("PlayerDeck");
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Deck.BattleDeckCard", b =>
                {
                    b.HasOne("CrazyCards.Domain.Entities.Card.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CrazyCards.Domain.Entities.Deck.BattleDeck", "Deck")
                        .WithMany()
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Deck");
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Deck.PlayerDeck", b =>
                {
                    b.HasOne("CrazyCards.Domain.Entities.Player.Player", "Player")
                        .WithOne("PlayerDeck")
                        .HasForeignKey("CrazyCards.Domain.Entities.Deck.PlayerDeck", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Card.Card", b =>
                {
                    b.Navigation("Habilities");
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Card.Class", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Deck.PlayerDeck", b =>
                {
                    b.Navigation("BattleDecks");
                });

            modelBuilder.Entity("CrazyCards.Domain.Entities.Player.Player", b =>
                {
                    b.Navigation("PlayerDeck")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
