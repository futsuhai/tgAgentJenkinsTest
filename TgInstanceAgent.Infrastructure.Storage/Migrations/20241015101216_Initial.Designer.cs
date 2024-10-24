﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TgInstanceAgent.Infrastructure.Storage.Context;

#nullable disable

namespace TgInstanceAgent.Infrastructure.Storage.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241015101216_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.ForwardEntryModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("For")
                        .HasColumnType("bigint");

                    b.Property<Guid>("InstanceId")
                        .HasColumnType("uuid");

                    b.Property<bool>("SendCopy")
                        .HasColumnType("boolean");

                    b.Property<long>("To")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("InstanceId");

                    b.ToTable("ForwardEntries");
                });

            modelBuilder.Entity("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.InstanceModel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ExpirationTimeUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SystemProxyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("SystemProxySetTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SystemProxyId");

                    b.ToTable("Instances");
                });

            modelBuilder.Entity("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.ProxyModel", b =>
                {
                    b.Property<Guid>("InstanceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ExpirationTimeUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("Login")
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("Password")
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<int>("Port")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("InstanceId");

                    b.ToTable("Proxies");
                });

            modelBuilder.Entity("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.RestrictionsModel", b =>
                {
                    b.Property<Guid>("InstanceId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("CurrentDate")
                        .HasColumnType("date");

                    b.Property<int>("FileDownloadCount")
                        .HasColumnType("integer");

                    b.Property<int>("MessageCount")
                        .HasColumnType("integer");

                    b.HasKey("InstanceId");

                    b.ToTable("Restrictions");
                });

            modelBuilder.Entity("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.WebhookSettingModel", b =>
                {
                    b.Property<Guid>("InstanceId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Chats")
                        .HasColumnType("boolean");

                    b.Property<bool>("Files")
                        .HasColumnType("boolean");

                    b.Property<bool>("Messages")
                        .HasColumnType("boolean");

                    b.Property<bool>("Other")
                        .HasColumnType("boolean");

                    b.Property<bool>("Stories")
                        .HasColumnType("boolean");

                    b.Property<bool>("Users")
                        .HasColumnType("boolean");

                    b.HasKey("InstanceId");

                    b.ToTable("WebhookSettings");
                });

            modelBuilder.Entity("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.WebhookUrlModel", b =>
                {
                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<Guid>("InstanceId")
                        .HasColumnType("uuid");

                    b.HasKey("Url", "InstanceId");

                    b.HasIndex("InstanceId");

                    b.ToTable("WebhookUrls");
                });

            modelBuilder.Entity("TgInstanceAgent.Infrastructure.Storage.Entities.Reports.ReportModel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<Guid>("InstanceId")
                        .HasColumnType("uuid");

                    b.Property<long>("Received")
                        .HasColumnType("bigint");

                    b.Property<long>("Sent")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("InstanceId", "Date")
                        .IsUnique();

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("TgInstanceAgent.Infrastructure.Storage.Entities.SystemProxies.SystemProxyModel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpirationTimeUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("IdInProviderSystem")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<int>("IpVersion")
                        .HasColumnType("integer");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<int>("Port")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("SystemProxies");
                });

            modelBuilder.Entity("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.ForwardEntryModel", b =>
                {
                    b.HasOne("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.InstanceModel", "Instance")
                        .WithMany("ForwardEntries")
                        .HasForeignKey("InstanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instance");
                });

            modelBuilder.Entity("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.InstanceModel", b =>
                {
                    b.HasOne("TgInstanceAgent.Infrastructure.Storage.Entities.SystemProxies.SystemProxyModel", null)
                        .WithMany("Instances")
                        .HasForeignKey("SystemProxyId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.ProxyModel", b =>
                {
                    b.HasOne("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.InstanceModel", null)
                        .WithOne("Proxy")
                        .HasForeignKey("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.ProxyModel", "InstanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.RestrictionsModel", b =>
                {
                    b.HasOne("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.InstanceModel", null)
                        .WithOne("Restrictions")
                        .HasForeignKey("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.RestrictionsModel", "InstanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.WebhookSettingModel", b =>
                {
                    b.HasOne("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.InstanceModel", null)
                        .WithOne("WebhookSetting")
                        .HasForeignKey("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.WebhookSettingModel", "InstanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.WebhookUrlModel", b =>
                {
                    b.HasOne("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.InstanceModel", null)
                        .WithMany("WebhookUrls")
                        .HasForeignKey("InstanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TgInstanceAgent.Infrastructure.Storage.Entities.Instances.InstanceModel", b =>
                {
                    b.Navigation("ForwardEntries");

                    b.Navigation("Proxy");

                    b.Navigation("Restrictions");

                    b.Navigation("WebhookSetting");

                    b.Navigation("WebhookUrls");
                });

            modelBuilder.Entity("TgInstanceAgent.Infrastructure.Storage.Entities.SystemProxies.SystemProxyModel", b =>
                {
                    b.Navigation("Instances");
                });
#pragma warning restore 612, 618
        }
    }
}
