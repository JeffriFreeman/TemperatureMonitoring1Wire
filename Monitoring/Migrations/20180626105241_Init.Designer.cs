﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Monitoring.Model;

namespace Monitoring.Migrations
{
    [DbContext(typeof(SensorsContext))]
    [Migration("20180626105241_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846");

            modelBuilder.Entity("Monitoring.SensorsTemp", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CRC");

                    b.Property<DateTime>("LastGet");

                    b.Property<string>("Mount");

                    b.Property<int>("Number");

                    b.Property<int>("Tempreture");

                    b.HasKey("Id");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("Monitoring.SensorsTempData", b =>
                {
                    b.Property<string>("Id");

                    b.Property<DateTime>("LastGet");

                    b.Property<bool>("CRC");

                    b.Property<string>("Mount");

                    b.Property<int>("Number");

                    b.Property<int>("Tempreture");

                    b.HasKey("Id", "LastGet");

                    b.ToTable("SensorsData");
                });
#pragma warning restore 612, 618
        }
    }
}