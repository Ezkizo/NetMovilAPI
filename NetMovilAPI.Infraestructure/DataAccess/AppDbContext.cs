using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetMovilAPI.Infraestructure.Models.OrderModels;
using NetMovilAPI.Infraestructure.Models.ProductModels;
using NetMovilAPI.Infraestructure.Models.SaleModels;
using NetMovilAPI.Infraestructure.Models.Shared;
using NetMovilAPI.Infraestructure.Models.Statuses;
using NetMovilAPI.Infraestructure.Models.UserModels;

namespace NetMovilAPI.Infraestructure.DataAccess;
public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    #region Orders
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderProduct> OrderProduct { get; set; }
    public DbSet<OrderStatus> OrderStatus { get; set; }
    #endregion Orders

    #region Products
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductCategory> ProductCategory { get; set; }
    public DbSet<ProductStatus> ProductStatus { get; set; }
    #endregion Products

    #region Users
    // public DbSet<User> User { get; set; } La tabla User ya existe en IdentityDbContext
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<CustomerAddress> CustomerAddress { get; set; }
    public DbSet<UserStatus> UserStatus { get; set; }
    // public DbSet<Role> Role { get; set; } La tabla Role ya existe en IdentityDbContext
    #endregion Users

    #region Sales
    public DbSet<Sale> Sale { get; set; }
    public DbSet<SalePayment> SalePayment { get; set; }
    public DbSet<SaleStatus> SaleStatus { get; set; }
    #endregion Sales

    #region Payments
    #endregion Payments

    #region Shared
    public DbSet<PaymentMethod> PaymentMethod { get; set; }
    public DbSet<PaymentStatus> PaymentStatus { get; set; }
    public DbSet<Stock> Stock { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<CategoryStatus> CategoryStatus { get; set; }
    public DbSet<Branch> Branch { get; set; } // <--- NUEVO
    #endregion Shared

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region User
        // User Relations
        modelBuilder.Entity<User>()
            .HasOne(u => u.UserStatus)
            .WithMany()
            .HasForeignKey(u => u.UserStatusID)
            .OnDelete(DeleteBehavior.NoAction);

        // Customer Relations
        modelBuilder.Entity<Customer>().HasKey(e => e.CustomerID);
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Addresses)
            .WithOne(a => a.Customer)
            .HasForeignKey(a => a.CustomerID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.User)
            .WithOne()
            .HasForeignKey<Customer>(u => u.Id)
            .OnDelete(DeleteBehavior.NoAction);

        // Employee Relations
        modelBuilder.Entity<Employee>().HasKey(e => e.EmployeeID);
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.User)
            .WithOne()
            .HasForeignKey<Employee>(e => e.Id)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<CustomerAddress>().HasKey(e => e.CustomerAddressID);
        modelBuilder.Entity<CustomerAddress>()
            .HasMany(ca => ca.Orders)
            .WithOne(o => o.CustomerAddress)
            .HasForeignKey(o => o.CustomerAddressID)
            .OnDelete(DeleteBehavior.NoAction);
        #endregion User

        #region Order
        modelBuilder.Entity<Order>().HasKey(o => o.OrderID);
        modelBuilder.Entity<Order>() // Details
            .HasMany(o => o.OrderProducts)
            .WithOne(op => op.Order)
            .HasForeignKey(op => op.OrderID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Order>() // Status
            .HasOne(o => o.OrderStatus)
            .WithMany()
            .HasForeignKey(o => o.OrderStatusID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Order>() // Employee
            .HasOne(o => o.Employee)
            .WithMany(e => e.Orders)
            .HasForeignKey(o => o.EmployeeID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Order>() // Customer
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerID)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Order>()
            .HasOne(o => o.CustomerAddress)
            .WithMany(ca => ca.Orders)
            .HasForeignKey(o => o.CustomerAddressID)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<OrderProduct>().HasKey(op => op.OrderProductID);
        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Product)
            .WithMany()
            .HasForeignKey(op => op.ProductID)
            .OnDelete(DeleteBehavior.NoAction);

        // Relación Branch - Order
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Branch)
            .WithMany(b => b.Orders)
            .HasForeignKey(o => o.BranchID)
            .OnDelete(DeleteBehavior.NoAction);

        #endregion Order

        #region Sale
        modelBuilder.Entity<Sale>().HasKey(s => s.SaleID);
        modelBuilder.Entity<Sale>() // Order details
            .HasOne(s => s.Order)
            .WithOne()
            .HasForeignKey<Sale>(s => s.OrderID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Sale>() // Payments
            .HasMany(s => s.Payments)
            .WithOne(sp => sp.Sale)
            .HasForeignKey(sp => sp.SaleID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<SalePayment>().HasKey(s => s.SalePaymentID);
        modelBuilder.Entity<SalePayment>()
            .HasOne(sp => sp.PaymentMethod)
            .WithMany()
            .HasForeignKey(sp => sp.PaymentMethodID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<SalePayment>()
            .HasOne(sp => sp.PaymentStatus)
            .WithMany()
            .HasForeignKey(sp => sp.PaymentStatusID)
            .OnDelete(DeleteBehavior.NoAction);

        #endregion Sale

        #region Product
        modelBuilder.Entity<Product>().HasKey(p => p.ProductID);
        modelBuilder.Entity<Product>()
            .HasMany(p => p.ProductCategories)
            .WithOne(pc => pc.Product)
            .HasForeignKey(pc => pc.ProductID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductStatus)
            .WithMany()
            .HasForeignKey(p => p.ProductStatusID)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Stock)
            .WithOne(s => s.Product)
            .HasForeignKey<Product>(p => p.StockID)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ProductCategory>().HasKey(pc => new { pc.ProductID, pc.CategoryID });
        modelBuilder.Entity<ProductCategory>()
            .HasOne(pc => pc.Product)
            .WithMany(p => p.ProductCategories)
            .HasForeignKey(pc => pc.ProductID)
            .OnDelete(DeleteBehavior.NoAction);

        // Relación Branch - Product
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Branch)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BranchID)
            .OnDelete(DeleteBehavior.NoAction);

        #endregion Product

        #region Statuses
        modelBuilder.Entity<CategoryStatus>().HasKey(cs => cs.CategoryStatusID);
        modelBuilder.Entity<CategoryStatus>()
            .HasMany(cs => cs.Categories)
            .WithOne(c => c.CategoryStatus)
            .HasForeignKey(c => c.CategoryStatusID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<PaymentStatus>().HasKey(ps => ps.PaymentStatusID);

        modelBuilder.Entity<PaymentStatus>()
            .HasMany(ps => ps.Sales)
            .WithOne(s => s.PaymentStatus)
            .HasForeignKey(s => s.PaymentStatusID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ProductStatus>().HasKey(ps => ps.ProductStatusID);
        modelBuilder.Entity<ProductStatus>()
            .HasMany(ps => ps.Products)
            .WithOne(p => p.ProductStatus)
            .HasForeignKey(p => p.ProductStatusID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<UserStatus>().HasKey(e => e.UserStatusID);
        modelBuilder.Entity<UserStatus>()
            .HasMany(e => e.Users)
            .WithOne(u => u.UserStatus)
            .HasForeignKey(u => u.UserStatusID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<SaleStatus>().HasKey(ss => ss.SaleStatusID);
        modelBuilder.Entity<SaleStatus>()
            .HasMany(ss => ss.Sales)
            .WithOne(s => s.SaleStatus)
            .HasForeignKey(s => s.SaleStatusID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<OrderStatus>().HasKey(os => os.OrderStatusID);
        modelBuilder.Entity<OrderStatus>()
            .HasMany(os => os.Orders)
            .WithOne(o => o.OrderStatus)
            .HasForeignKey(o => o.OrderStatusID)
            .OnDelete(DeleteBehavior.NoAction);


        #endregion Statuses

        #region Shared
        modelBuilder.Entity<PaymentMethod>().HasKey(pm => pm.PaymentMethodID);
        modelBuilder.Entity<PaymentMethod>()
            .HasMany(pm => pm.SalePayments)
            .WithOne(sp => sp.PaymentMethod)
            .HasForeignKey(sp => sp.PaymentMethodID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Stock>().HasKey(s => s.StockID);
        modelBuilder.Entity<Stock>()
            .HasOne(s => s.Product)
            .WithOne(p => p.Stock)
            .HasForeignKey<Stock>(s => s.ProductID)
            .OnDelete(DeleteBehavior.NoAction);

        // Relación Branch - Stock
        modelBuilder.Entity<Stock>()
            .HasOne(s => s.Branch)
            .WithMany(b => b.Stocks)
            .HasForeignKey(s => s.BranchID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Category>().HasKey(c => c.CategoryID);
        modelBuilder.Entity<Category>()
            .HasOne(c => c.CategoryStatus)
            .WithMany(cs => cs.Categories)
            .HasForeignKey(c => c.CategoryStatusID)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        #endregion Shared


        // Properties Definition
        #region OrderProperties
        modelBuilder.Entity<Order>()
            .Property(o => o.TotalAmount)
            .HasColumnType("decimal(18,4)");

        modelBuilder.Entity<Order>()
            .Property(o => o.BarCode)
            .HasMaxLength(50);

        modelBuilder.Entity<Order>()
            .Property(o => o.Notes)
            .HasMaxLength(500);

        modelBuilder.Entity<OrderProduct>()
            .Property(op => op.Quantity)
            .HasColumnType("decimal(18,4)");

        #endregion OrderProperties

        #region UserProperties
        modelBuilder.Entity<User>()
            .Property(u => u.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.LastName)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.PhoneNumber)
            .HasMaxLength(15);

        // Customer
        modelBuilder.Entity<Employee>()
            .Property(e => e.EmergencyContact)
            .HasMaxLength(20);
        modelBuilder.Entity<Employee>()
            .Property(e => e.EmergencyContactName)
            .HasMaxLength(50);
        modelBuilder.Entity<Employee>()
            .Property(e => e.ProfileImage)
            .HasMaxLength(150);
        #endregion UserProperties

        #region ProductProperties
        modelBuilder.Entity<Product>()
            .Property(p => p.BasePrice)
            .HasColumnType("decimal(18,4)");

        modelBuilder.Entity<Product>()
            .Property(p => p.ProfitMargin)
            .HasColumnType("decimal(18,4)");

        modelBuilder.Entity<Product>()
            .Property(p => p.UnitPrice)
            .HasColumnType("decimal(18,4)");

        modelBuilder.Entity<Product>()
            .Property(p => p.BarCode)
            .HasMaxLength(50);

        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property(p => p.Description)
            .HasMaxLength(400);

        modelBuilder.Entity<Product>()
            .Property(p => p.ImageUrl)
            .HasMaxLength(150);
        #endregion ProductProperties

        #region CategoryProperties
        modelBuilder.Entity<Category>()
            .Property(c => c.Description)
            .HasMaxLength(50);

        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .HasMaxLength(30);

        modelBuilder.Entity<Category>()
            .Property(c => c.ImageUrl)
            .HasMaxLength(150);
        #endregion CategoryProperties

        #region SaleProperties
        modelBuilder.Entity<Sale>()
           .Property(s => s.TotalPaid)
           .HasColumnType("decimal(18,4)");

        modelBuilder.Entity<SalePayment>()
            .Property(sp => sp.Amount)
            .HasColumnType("decimal(18,4)");
        modelBuilder.Entity<SalePayment>()
            .Property(sp => sp.Reference)
            .HasMaxLength(300);
        #endregion SaleProperties

        #region StockProperties
        modelBuilder.Entity<Stock>()
            .Property(s => s.Quantity)
            .HasColumnType("decimal(18,4)");

        modelBuilder.Entity<Stock>()
            .Property(s => s.Threshold)
            .HasColumnType("decimal(18,4)");
        #endregion StockProperties

        #region Address
        modelBuilder.Entity<CustomerAddress>()
        .Property(ca => ca.Street)
        .HasMaxLength(100);

        modelBuilder.Entity<CustomerAddress>()
            .Property(ca => ca.City)
            .HasMaxLength(50);

        modelBuilder.Entity<CustomerAddress>()
            .Property(ca => ca.DeliveryReferences)
            .HasMaxLength(250)
            .HasDefaultValue("Sin referencias");

        modelBuilder.Entity<CustomerAddress>()
            .Property(ca => ca.State)
            .HasMaxLength(50);

        modelBuilder.Entity<CustomerAddress>()
            .Property(ca => ca.Country)
            .HasMaxLength(50);
        #endregion Address

        #region StatusesProperties
        modelBuilder.Entity<CategoryStatus>()
            .Property(c => c.Description)
            .HasMaxLength(30);

        modelBuilder.Entity<OrderStatus>()
            .Property(c => c.Description)
            .HasMaxLength(30);

        modelBuilder.Entity<PaymentStatus>()
            .Property(c => c.Description)
            .HasMaxLength(30);

        modelBuilder.Entity<ProductStatus>()
            .Property(c => c.Description)
            .HasMaxLength(30);

        modelBuilder.Entity<SaleStatus>()
            .Property(c => c.Description)
            .HasMaxLength(30);

        modelBuilder.Entity<UserStatus>()
            .Property(c => c.Description)
            .HasMaxLength(30);
        #endregion StatusesProperties

        modelBuilder.Entity<PaymentMethod>()
            .Property(c => c.Name)
            .HasMaxLength(50);
        modelBuilder.Entity<PaymentMethod>()
            .Property(c => c.Description)
            .HasMaxLength(300);

        #region Branch
        modelBuilder.Entity<Branch>().HasKey(b => b.BranchID);
        modelBuilder.Entity<Branch>()
            .Property(b => b.Name)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder.Entity<Branch>()
            .Property(b => b.Address)
            .HasMaxLength(200);

        modelBuilder.Entity<Branch>()
            .HasMany(b => b.Orders)
            .WithOne(o => o.Branch)
            .HasForeignKey(o => o.BranchID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Branch>()
            .HasMany(b => b.Products)
            .WithOne(p => p.Branch)
            .HasForeignKey(p => p.BranchID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Branch>()
            .HasMany(b => b.Stocks)
            .WithOne(s => s.Branch)
            .HasForeignKey(s => s.BranchID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Branch)
            .WithMany(b => b.Users)
            .HasForeignKey(u => u.BranchID)
            .OnDelete(DeleteBehavior.NoAction);

        #endregion Branch

        #region SeedData
        modelBuilder.Entity<UserStatus>().HasData(
            new UserStatus { UserStatusID = 1, Description = "Eliminado" },
            new UserStatus { UserStatusID = 2, Description = "Inactivo" },
            new UserStatus { UserStatusID = 3, Description = "Activo" }
        );

        modelBuilder.Entity<SaleStatus>().HasData(
            new SaleStatus { SaleStatusID = 1, Description = "Eliminada" },
            new SaleStatus { SaleStatusID = 2, Description = "Cancelada" },
            new SaleStatus { SaleStatusID = 3, Description = "Pendiente" },
            new SaleStatus { SaleStatusID = 4, Description = "Pagada" }
        );

        modelBuilder.Entity<ProductStatus>().HasData(
            new ProductStatus { ProductStatusID = 1, Description = "Eliminado" },
            new ProductStatus { ProductStatusID = 2, Description = "Fuera de Stock" },
            new ProductStatus { ProductStatusID = 3, Description = "En existencia" }
        );

        modelBuilder.Entity<PaymentStatus>().HasData(
            new PaymentStatus { PaymentStatusID = 1, Description = "Eliminado" },
            new PaymentStatus { PaymentStatusID = 2, Description = "Cancelado" },
            new PaymentStatus { PaymentStatusID = 3, Description = "Rechazado" },
            new PaymentStatus { PaymentStatusID = 4, Description = "En espera" },
            new PaymentStatus { PaymentStatusID = 5, Description = "Aceptado" }
        );

        var dateTime = DateTime.Now;

        modelBuilder.Entity<PaymentMethod>().HasData(
            new PaymentMethod { PaymentMethodID = 1, Name = "Efectivo", Description = "Pago en efectivo directo a caja.", IsActive = true, CreatedBy = 1, CreatedAt = dateTime },
            new PaymentMethod { PaymentMethodID = 2, Name = "Transferencia", Description = "Transferencia DIMO, banca digital o CODI.", IsActive = true, CreatedBy = 1, CreatedAt = dateTime },
            new PaymentMethod { PaymentMethodID = 3, Name = "Terminal", Description = "Pago en terminal bancaria, se abona a una cuenta digital.", IsActive = true, CreatedBy = 1, CreatedAt = dateTime }
        );

        modelBuilder.Entity<OrderStatus>().HasData(
            new OrderStatus { OrderStatusID = 1, Description = "Eliminada"},
            new OrderStatus { OrderStatusID = 2, Description = "Cancelada"},
            new OrderStatus { OrderStatusID = 3, Description = "En reclamación"},
            new OrderStatus { OrderStatusID = 4, Description = "Activa"},
            new OrderStatus { OrderStatusID = 5, Description = "Completada" }
        );

        modelBuilder.Entity<CategoryStatus>().HasData(
            new CategoryStatus { CategoryStatusID = 1, Description = "Eliminada" },
            new CategoryStatus { CategoryStatusID = 2, Description = "Inactiva" },
            new CategoryStatus { CategoryStatusID = 3, Description = "Activa" }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryID = 1, Name = "Ejemplo", Description = "Categoría de ejemplo", ImageUrl = "defaultcategory.png", CategoryStatusID = 3, CreatedBy = 1, CreatedAt = dateTime }
        );

        modelBuilder.Entity<Branch>().HasData(
            new Branch { BranchID = 1, Name = "Sucursal 1", Address = "Dirección sucursal 1"},
            new Branch { BranchID = 2, Name = "Sucursal 2", Address = "Dirección sucursal 2"}
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { ProductID = 1, Name = "Ejemplo producto", Description = "Este es un producto de prueba", BasePrice = 50.00m, ProfitMargin = 100, UnitPrice = 100, ImageUrl = "defaultproduct.png", BarCode = null, IsStock = false, ProductStatusID = 3, BranchID = 1, CreatedBy = 1, CreatedAt = dateTime }
        );

        #endregion SeedData

        base.OnModelCreating(modelBuilder);
    }
}
