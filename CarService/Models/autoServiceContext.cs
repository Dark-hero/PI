using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarService.Models
{
    public partial class autoServiceContext : DbContext
    {
        public autoServiceContext()
        {
        }

        public autoServiceContext(DbContextOptions<autoServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<ArtikulParts> ArtikulParts { get; set; }
        public virtual DbSet<Auto> Auto { get; set; }
        public virtual DbSet<BonusCard> BonusCard { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<Masters> Masters { get; set; }
        public virtual DbSet<OrderingServices> OrderingServices { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Parts> Parts { get; set; }
        public virtual DbSet<TypeOfWorks> TypeOfWorks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LENOVO;Database=autoService;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("account");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__account__AB6E616426561D1E")
                    .IsUnique();

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.ActivationCode).HasColumnName("activation_code");

                entity.Property(e => e.DateOfRegistration)
                    .HasColumnName("date_of_registration")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(70);

                entity.Property(e => e.IdCard).HasColumnName("id_card");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.Patronymic)
                    .HasColumnName("patronymic")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(35);

                entity.Property(e => e.ResetPasswordCode)
                    .HasColumnName("reset_password_code")
                    .HasMaxLength(100);

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasMaxLength(50);

                entity.Property(e => e.Verified).HasColumnName("verified");

                entity.HasOne(d => d.IdCardNavigation)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.IdCard)
                    .HasConstraintName("FK_account_bonusCard");
            });

            modelBuilder.Entity<ArtikulParts>(entity =>
            {
                entity.HasKey(e => e.Artikul);

                entity.ToTable("artikulParts");

                entity.Property(e => e.Artikul)
                    .HasColumnName("artikul")
                    .ValueGeneratedNever();

                entity.Property(e => e.VinCode)
                    .IsRequired()
                    .HasColumnName("vinCode")
                    .HasMaxLength(15);

                entity.HasOne(d => d.VinCodeNavigation)
                    .WithMany(p => p.ArtikulParts)
                    .HasForeignKey(d => d.VinCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_artikulParts_auto");
            });

            modelBuilder.Entity<Auto>(entity =>
            {
                entity.HasKey(e => e.VinCode);

                entity.ToTable("auto");

                entity.Property(e => e.VinCode)
                    .HasColumnName("vinCode")
                    .HasMaxLength(15)
                    .ValueGeneratedNever();

                entity.Property(e => e.EngineСapacity).HasColumnName("engineСapacity");

                entity.Property(e => e.Mark)
                    .IsRequired()
                    .HasColumnName("mark")
                    .HasMaxLength(20);

                entity.Property(e => e.Mileage).HasColumnName("mileage");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasColumnName("model")
                    .HasMaxLength(20);

                entity.Property(e => e.RegisterSign)
                    .IsRequired()
                    .HasColumnName("registerSign")
                    .HasMaxLength(7);

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<BonusCard>(entity =>
            {
                entity.HasKey(e => e.IdCard);

                entity.ToTable("bonusCard");

                entity.Property(e => e.IdCard)
                    .HasColumnName("id_card")
                    .ValueGeneratedNever();

                entity.Property(e => e.DateOfPurchase)
                    .HasColumnName("date_of_purchase")
                    .HasColumnType("date");

                entity.Property(e => e.Discount).HasColumnName("discount");
            });

            modelBuilder.Entity<Clients>(entity =>
            {
                entity.HasKey(e => e.IdClient);

                entity.ToTable("clients");

                entity.Property(e => e.IdClient).HasColumnName("idClient");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(70);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Patronimyc)
                    .IsRequired()
                    .HasColumnName("patronimyc")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(35);

                entity.Property(e => e.Problem).HasColumnName("problem");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => e.IdComment);

                entity.ToTable("comments");

                entity.Property(e => e.IdComment).HasColumnName("idComment");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasMaxLength(500);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_comments_account");
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.HasKey(e => e.IdEquipment);

                entity.ToTable("equipment");

                entity.Property(e => e.IdEquipment)
                    .HasColumnName("id_equipment")
                    .ValueGeneratedNever();

                entity.Property(e => e.CoefficientOfLoading).HasColumnName("coefficient_of_loading");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.DatуOfManufacture)
                    .HasColumnName("datу_of_manufacture")
                    .HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Warranty)
                    .HasColumnName("warranty")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Masters>(entity =>
            {
                entity.HasKey(e => e.IdMaster);

                entity.ToTable("masters");

                entity.Property(e => e.IdMaster)
                    .HasColumnName("id_master")
                    .ValueGeneratedNever();

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("date");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(15);

                entity.Property(e => e.DateOfEmployment)
                    .HasColumnName("date_of_employment")
                    .HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.NumberFlat)
                    .IsRequired()
                    .HasColumnName("number_flat")
                    .HasMaxLength(7);

                entity.Property(e => e.NumberHouse)
                    .IsRequired()
                    .HasColumnName("number_house")
                    .HasMaxLength(7);

                entity.Property(e => e.PassportId)
                    .IsRequired()
                    .HasColumnName("passport_id")
                    .HasMaxLength(50);

                entity.Property(e => e.Patronymic)
                    .HasColumnName("patronymic")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(20);

                entity.Property(e => e.Postcode)
                    .IsRequired()
                    .HasColumnName("postcode")
                    .HasMaxLength(10);

                entity.Property(e => e.Specialty)
                    .IsRequired()
                    .HasColumnName("specialty")
                    .HasMaxLength(50);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnName("street")
                    .HasMaxLength(50);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<OrderingServices>(entity =>
            {
                entity.ToTable("orderingServices");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Artikul).HasColumnName("artikul");

                entity.Property(e => e.IdMaster).HasColumnName("id_master");

                entity.Property(e => e.JobCode).HasColumnName("jobCode");

                entity.HasOne(d => d.ArtikulNavigation)
                    .WithMany(p => p.OrderingServices)
                    .HasForeignKey(d => d.Artikul)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orderingServices_Parts");

                entity.HasOne(d => d.IdMasterNavigation)
                    .WithMany(p => p.OrderingServices)
                    .HasForeignKey(d => d.IdMaster)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orderingServices_masters");

                entity.HasOne(d => d.JobCodeNavigation)
                    .WithMany(p => p.OrderingServices)
                    .HasForeignKey(d => d.JobCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orderingServices_type_of_works");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(10)
                    .ValueGeneratedNever();

                entity.Property(e => e.AdmissionDate)
                    .HasColumnName("admission_date")
                    .HasColumnType("date");

                entity.Property(e => e.IdClient).HasColumnName("idClient");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.OrderCost).HasColumnName("order_cost");

                entity.Property(e => e.VinCode)
                    .IsRequired()
                    .HasColumnName("vinCode")
                    .HasMaxLength(15);

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orders_clients");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orders_account");

                entity.HasOne(d => d.VinCodeNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.VinCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orders_auto");
            });

            modelBuilder.Entity<Parts>(entity =>
            {
                entity.HasKey(e => e.Artikul);

                entity.Property(e => e.Artikul)
                    .HasColumnName("artikul")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.DateOfDelivery)
                    .HasColumnName("date_of_delivery")
                    .HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.HasOne(d => d.ArtikulNavigation)
                    .WithOne(p => p.Parts)
                    .HasForeignKey<Parts>(d => d.Artikul)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Parts_artikulParts");
            });

            modelBuilder.Entity<TypeOfWorks>(entity =>
            {
                entity.HasKey(e => e.JobCode);

                entity.ToTable("type_of_works");

                entity.Property(e => e.JobCode)
                    .HasColumnName("jobCode")
                    .ValueGeneratedNever();

                entity.Property(e => e.Deadline).HasColumnName("deadline");

                entity.Property(e => e.HourСost).HasColumnName("hourСost");

                entity.Property(e => e.IdEquipment).HasColumnName("id_equipment");

                entity.Property(e => e.TypeOfWork)
                    .IsRequired()
                    .HasColumnName("type_of_work")
                    .HasMaxLength(200);

                entity.HasOne(d => d.IdEquipmentNavigation)
                    .WithMany(p => p.TypeOfWorks)
                    .HasForeignKey(d => d.IdEquipment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_type_of_works_equipment");
            });
        }
    }
}
