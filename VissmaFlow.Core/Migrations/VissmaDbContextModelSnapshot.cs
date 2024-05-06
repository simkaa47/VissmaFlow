﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VissmaFlow.Core.Infrastructure.DataAccess;

#nullable disable

namespace VissmaFlow.Core.Migrations
{
    [DbContext(typeof(VissmaDbContext))]
    partial class VissmaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("VissmaFlow.Core.Models.AccessControl.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Administration.PcSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PcSettings");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Communication.CommSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Baudrate")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Interface")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Parity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PortName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PortNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StopBitsNum")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("CommSettings");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Communication.RtkUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UnitId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("RtkUnits");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Event.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ActiveMessage")
                        .HasColumnType("TEXT");

                    b.Property<float>("CompareValue")
                        .HasColumnType("REAL");

                    b.Property<int>("EventCondition")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EventType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NonActiveMessage")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParameterId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RtkUnitId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParameterId");

                    b.HasIndex("RtkUnitId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Indication.IndicationCell", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MeasUnit")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParameterId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RtkUnitId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParameterId");

                    b.HasIndex("RtkUnitId");

                    b.ToTable("IndicationCells");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Logging.LogCell", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("LogSettingsId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ParameterId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RtkUnitId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LogSettingsId");

                    b.HasIndex("ParameterId");

                    b.HasIndex("RtkUnitId");

                    b.ToTable("LogCell");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Logging.LogSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MinPeriod")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LogSettingses");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Parameters.ParameterBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BitNum")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ByteOrder")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Data")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsOnlyRead")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsReadOnly")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsWriting")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ModbRegNum")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ModbusRegType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("StrLength")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserAccessLevel")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ValidationOk")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ParameterBases");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ParameterBase");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.SingleMeasures.SingleMeasurePoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AvgValueId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DestinationId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SingleMeasureSettingsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AvgValueId");

                    b.HasIndex("DestinationId");

                    b.HasIndex("SingleMeasureSettingsId");

                    b.ToTable("SingleMeasurePoint");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.SingleMeasures.SingleMeasureSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SourceId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SourceId");

                    b.ToTable("SingleMeasureSettingses");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Trends.Curve", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ParameterId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RtkUnitId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParameterId");

                    b.HasIndex("RtkUnitId");

                    b.ToTable("Curves");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Trends.TrendSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxTimeSeconds")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ScanFrequence")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("TrendSettings");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Parameters.ParameterBool", b =>
                {
                    b.HasBaseType("VissmaFlow.Core.Models.Parameters.ParameterBase");

                    b.Property<bool>("MaxValue")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MaxValueAsBool");

                    b.Property<bool>("MinValue")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MinValueAsBool");

                    b.Property<bool>("Value")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ValueAsBool");

                    b.Property<bool>("WriteValue")
                        .HasColumnType("INTEGER")
                        .HasColumnName("WriteValueAsBool");

                    b.HasDiscriminator().HasValue("ParameterBool");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Parameters.ParameterDouble", b =>
                {
                    b.HasBaseType("VissmaFlow.Core.Models.Parameters.ParameterBase");

                    b.Property<double>("MaxValue")
                        .HasColumnType("REAL")
                        .HasColumnName("MaxValueAsDouble");

                    b.Property<double>("MinValue")
                        .HasColumnType("REAL")
                        .HasColumnName("MinValueAsDouble");

                    b.Property<double>("Value")
                        .HasColumnType("REAL")
                        .HasColumnName("ValueAsoDouble");

                    b.Property<double>("WriteValue")
                        .HasColumnType("REAL")
                        .HasColumnName("WriteValueAsDouble");

                    b.HasDiscriminator().HasValue("ParameterDouble");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Parameters.ParameterFloat", b =>
                {
                    b.HasBaseType("VissmaFlow.Core.Models.Parameters.ParameterBase");

                    b.Property<float>("MaxValue")
                        .HasColumnType("REAL")
                        .HasColumnName("MaxValueAsFloat");

                    b.Property<float>("MinValue")
                        .HasColumnType("REAL")
                        .HasColumnName("MinValueAsFloat");

                    b.Property<float>("Value")
                        .HasColumnType("REAL")
                        .HasColumnName("ValueAsoFloat");

                    b.Property<float>("WriteValue")
                        .HasColumnType("REAL")
                        .HasColumnName("WriteValueAsFloat");

                    b.HasDiscriminator().HasValue("ParameterFloat");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Parameters.ParameterInt", b =>
                {
                    b.HasBaseType("VissmaFlow.Core.Models.Parameters.ParameterBase");

                    b.Property<int>("MaxValue")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MaxValueAsInt");

                    b.Property<int>("MinValue")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MinValueAsInt");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ValueAsInt");

                    b.Property<int>("WriteValue")
                        .HasColumnType("INTEGER")
                        .HasColumnName("WriteValueAsInt");

                    b.HasDiscriminator().HasValue("ParameterInt");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Parameters.ParameterShort", b =>
                {
                    b.HasBaseType("VissmaFlow.Core.Models.Parameters.ParameterBase");

                    b.Property<short>("MaxValue")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MaxValueAsShort");

                    b.Property<short>("MinValue")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MinValueAsShort");

                    b.Property<short>("Value")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ValueAsShort");

                    b.Property<short>("WriteValue")
                        .HasColumnType("INTEGER")
                        .HasColumnName("WriteValueAsShort");

                    b.HasDiscriminator().HasValue("ParameterShort");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Parameters.ParameterString", b =>
                {
                    b.HasBaseType("VissmaFlow.Core.Models.Parameters.ParameterBase");

                    b.Property<string>("MaxValue")
                        .HasColumnType("TEXT")
                        .HasColumnName("MaxValueAsString");

                    b.Property<string>("MinValue")
                        .HasColumnType("TEXT")
                        .HasColumnName("MinValueAsString");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT")
                        .HasColumnName("ValueAsString");

                    b.Property<string>("WriteValue")
                        .HasColumnType("TEXT")
                        .HasColumnName("WriteValueAsString");

                    b.HasDiscriminator().HasValue("ParameterString");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Parameters.ParameterUint", b =>
                {
                    b.HasBaseType("VissmaFlow.Core.Models.Parameters.ParameterBase");

                    b.Property<uint>("MaxValue")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MaxValueAsUint");

                    b.Property<uint>("MinValue")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MinValueAsUint");

                    b.Property<uint>("Value")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ValueAsUint");

                    b.Property<uint>("WriteValue")
                        .HasColumnType("INTEGER")
                        .HasColumnName("WriteValueAsUint");

                    b.HasDiscriminator().HasValue("ParameterUint");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Parameters.ParameterUshort", b =>
                {
                    b.HasBaseType("VissmaFlow.Core.Models.Parameters.ParameterBase");

                    b.Property<ushort>("MaxValue")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MaxValueAsUshort");

                    b.Property<ushort>("MinValue")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MinValueAsUshort");

                    b.Property<ushort>("Value")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ValueAsUshort");

                    b.Property<ushort>("WriteValue")
                        .HasColumnType("INTEGER")
                        .HasColumnName("WriteValueAsUshort");

                    b.HasDiscriminator().HasValue("ParameterUshort");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Event.Event", b =>
                {
                    b.HasOne("VissmaFlow.Core.Models.Parameters.ParameterBase", "Parameter")
                        .WithMany()
                        .HasForeignKey("ParameterId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("VissmaFlow.Core.Models.Communication.RtkUnit", "RtkUnit")
                        .WithMany()
                        .HasForeignKey("RtkUnitId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Parameter");

                    b.Navigation("RtkUnit");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Indication.IndicationCell", b =>
                {
                    b.HasOne("VissmaFlow.Core.Models.Parameters.ParameterBase", "Parameter")
                        .WithMany()
                        .HasForeignKey("ParameterId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("VissmaFlow.Core.Models.Communication.RtkUnit", "RtkUnit")
                        .WithMany()
                        .HasForeignKey("RtkUnitId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Parameter");

                    b.Navigation("RtkUnit");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Logging.LogCell", b =>
                {
                    b.HasOne("VissmaFlow.Core.Models.Logging.LogSettings", null)
                        .WithMany("Cells")
                        .HasForeignKey("LogSettingsId");

                    b.HasOne("VissmaFlow.Core.Models.Parameters.ParameterBase", "Parameter")
                        .WithMany()
                        .HasForeignKey("ParameterId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("VissmaFlow.Core.Models.Communication.RtkUnit", "RtkUnit")
                        .WithMany()
                        .HasForeignKey("RtkUnitId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Parameter");

                    b.Navigation("RtkUnit");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.SingleMeasures.SingleMeasurePoint", b =>
                {
                    b.HasOne("VissmaFlow.Core.Models.Parameters.ParameterBase", "AvgValue")
                        .WithMany()
                        .HasForeignKey("AvgValueId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("VissmaFlow.Core.Models.Parameters.ParameterBase", "Destination")
                        .WithMany()
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("VissmaFlow.Core.Models.SingleMeasures.SingleMeasureSettings", null)
                        .WithMany("Points")
                        .HasForeignKey("SingleMeasureSettingsId");

                    b.Navigation("AvgValue");

                    b.Navigation("Destination");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.SingleMeasures.SingleMeasureSettings", b =>
                {
                    b.HasOne("VissmaFlow.Core.Models.Parameters.ParameterBase", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Source");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Trends.Curve", b =>
                {
                    b.HasOne("VissmaFlow.Core.Models.Parameters.ParameterBase", "Parameter")
                        .WithMany()
                        .HasForeignKey("ParameterId");

                    b.HasOne("VissmaFlow.Core.Models.Communication.RtkUnit", "RtkUnit")
                        .WithMany()
                        .HasForeignKey("RtkUnitId");

                    b.Navigation("Parameter");

                    b.Navigation("RtkUnit");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.Logging.LogSettings", b =>
                {
                    b.Navigation("Cells");
                });

            modelBuilder.Entity("VissmaFlow.Core.Models.SingleMeasures.SingleMeasureSettings", b =>
                {
                    b.Navigation("Points");
                });
#pragma warning restore 612, 618
        }
    }
}
