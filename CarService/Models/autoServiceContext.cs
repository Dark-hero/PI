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
        public virtual DbSet<Auto> Auto { get; set; }
        public virtual DbSet<AutoToPart> AutoToPart { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Masters> Masters { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Parts> Parts { get; set; }
        public virtual DbSet<Records> Records { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<OrdersToParts> OrdersToParts { get; set; }
        public virtual DbSet<OrdersToWorks> OrdersToWorks { get; set; }
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

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.ActivationCode).HasColumnName("activationCode");

                entity.Property(e => e.DateOfRegistration)
                    .HasColumnName("dateOfRegistration")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(70);

                entity.Property(e => e.IdCard).HasColumnName("id_card");

                entity.Property(e => e.IdRole).HasColumnName("id_role");

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

                entity.Property(e => e.ResetPasswordCode).HasColumnName("resetPasswordCode");

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasMaxLength(50);

                entity.Property(e => e.Verified).HasColumnName("verified");

                entity.HasOne(d => d.Roles)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Roles");
            });

            modelBuilder.Entity<Auto>(entity =>
            {
                entity.HasKey(e => e.VinCode);

                entity.ToTable("auto");

                entity.Property(e => e.VinCode)
                    .HasColumnName("vinCode")
                    .HasMaxLength(20)
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
                    .HasMaxLength(10);

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<AutoToPart>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Artikul).HasColumnName("artikul");

                entity.Property(e => e.VinCode)
                    .IsRequired()
                    .HasColumnName("vinCode")
                    .HasMaxLength(20);

                entity.HasOne(d => d.ArtikulNavigation)
                    .WithMany(p => p.AutoToPart)
                    .HasForeignKey(d => d.Artikul)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AutoToPart_Parts");

                entity.HasOne(d => d.VinCodeNavigation)
                    .WithMany(p => p.AutoToPart)
                    .HasForeignKey(d => d.VinCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AutoToPart_auto");
            });


            modelBuilder.Entity<Clients>(entity =>
            {
                entity.HasKey(e => e.IdClient);

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
                    .HasConstraintName("FK_comments_Account");
            });

            modelBuilder.Entity<Masters>(entity =>
            {
                entity.HasKey(e => e.IdMaster);

                entity.ToTable("masters");

                entity.Property(e => e.IdMaster).HasColumnName("id_master");

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

                entity.Property(e => e.IsWork).HasColumnName("isWork");


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

            

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date")
                    .HasColumnType("date");

                entity.Property(e => e.IdClient).HasColumnName("idClient");

                entity.Property(e => e.IdMaster).HasColumnName("id_master");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.OrderCost).HasColumnName("order_cost");

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("date");

                entity.Property(e => e.VinCode)
                    .IsRequired()
                    .HasColumnName("vinCode")
                    .HasMaxLength(20);

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orders_Clients");

                entity.HasOne(d => d.IdMasterNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdMaster)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orders_masters");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orders_Account");

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
                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            modelBuilder.Entity<OrdersToParts>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Artikul).HasColumnName("artikul");

                entity.Property(e => e.IdOrder).HasColumnName("id_order");

                entity.HasOne(d => d.OrdersNavigation)
                    .WithMany(p => p.OrdersToParts)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdersToParts_Orders");

                entity.HasOne(d => d.PartsNavigation)
                    .WithMany(p => p.OrdersToParts)
                    .HasForeignKey(d => d.Artikul)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdersToParts_Parts");
            });

            modelBuilder.Entity<OrdersToWorks>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.JobCode).HasColumnName("jobCode");

                entity.Property(e => e.IdOrder).HasColumnName("id_order");

                entity.HasOne(d => d.OrdersNavigation)
                    .WithMany(p => p.OrdersToWorks)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdersToWorks_Orders");

                entity.HasOne(d => d.TypeOfWorksNavigation)
                    .WithMany(p => p.OrdersToWorks)
                    .HasForeignKey(d => d.JobCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdersToWorks_type_of_works");
            });

            modelBuilder.Entity<Records>(entity =>
            {
                entity.HasKey(e => e.IdRecod);

                entity.Property(e => e.IdRecod).HasColumnName("idRecod");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ThemeColor).HasMaxLength(10);

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.IdClient)
                    .HasConstraintName("FK_Records_Clients");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Records_Account");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.Property(e => e.IdRole)
                    .HasColumnName("id_role")
                    .ValueGeneratedNever();

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("role")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<TypeOfWorks>(entity =>
            {
                entity.HasKey(e => e.JobCode);

                entity.ToTable("type_of_works");

                entity.Property(e => e.JobCode).HasColumnName("jobCode");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.TypeOfWork)
                    .IsRequired()
                    .HasColumnName("type_of_work")
                    .HasMaxLength(200);
            });
        }
    }
}
