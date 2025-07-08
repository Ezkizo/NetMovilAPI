using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NetMovilAPI.Application.DTOs.Requests;
using NetMovilAPI.Application.Mappers;
using NetMovilAPI.Application.Presenters;
using NetMovilAPI.Application.Presenters.ViewModels;
using NetMovilAPI.Application.UseCases.CategoryUseCases;
using NetMovilAPI.Application.UseCases.OrderUseCases;
using NetMovilAPI.Application.UseCases.ProductUseCases;
using NetMovilAPI.Application.UseCases.SaleUseCases;
using NetMovilAPI.Domain.Entities.Order;
using NetMovilAPI.Domain.Entities.Product;
using NetMovilAPI.Domain.Entities.Sale;
using NetMovilAPI.Domain.Entities.Shared;
using NetMovilAPI.Domain.Interfaces;
using NetMovilAPI.Endpoints;
using NetMovilAPI.Infraestructure.DataAccess;
using NetMovilAPI.Infraestructure.DataAccess.Repositories.CategoryRepositories;
using NetMovilAPI.Infraestructure.DataAccess.Repositories.OrderRepositories;
using NetMovilAPI.Infraestructure.DataAccess.Repositories.ProductRepositories;
using NetMovilAPI.Infraestructure.DataAccess.Repositories.SaleRepositories;
using NetMovilAPI.Infraestructure.Models.OrderModels;
using NetMovilAPI.Infraestructure.Models.ProductModels;
using NetMovilAPI.Infraestructure.Models.SaleModels;
using NetMovilAPI.Infraestructure.Models.Shared;
using NetMovilAPI.Infraestructure.Models.UserModels;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración de entidades de IdentityDbContext
builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<AppDbContext>();

// Configuración de policies basadas en claims
builder.Services.AddAuthorization(options =>
{
    // Policy para eliminar o editar (requiere claim "Permission" con valor "Edit" o "Delete")
    //options.AddPolicy("CanEditOrDelete", policy =>
    //    policy.RequireClaim("Permission", "Edit", "Delete"));

    //// Policy para consultar (GET) requiere claim "Permission" con valor "View"
    //options.AddPolicy("CanView", policy =>
    //    policy.RequireClaim("Permission", "View"));

    //// Policy para crear (POST) requiere claim "Permission" con valor "Create"
    //options.AddPolicy("CanCreate", policy =>
    //    policy.RequireClaim("Permission", "Create"));

    //// Policy para actualizar (PUT) requiere claim "Permission" con valor "Update"
    //options.AddPolicy("CanUpdate", policy =>
    //    policy.RequireClaim("Permission", "Update"));

    // Policies de roles existentes
    options.AddPolicy("IsEmployee", policy => policy.RequireRole("Admin", "Employee", "Supervisor"));
    options.AddPolicy("IsCustomer", policy => policy.RequireRole("Customer"));
});
// Configuración de autenticación JWT
var key = builder.Configuration["APIKeyPruebas"];
if (string.IsNullOrEmpty(key))
{
    throw new InvalidOperationException("APIKeyPruebas no está configurado en appsettings.json");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Validación de la firma
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),

        // Validación de emisor y audiencia (opcional, pero recomendable)
        ValidateIssuer = false,
        //ValidIssuer = builder.Configuration["Jwt:Issuer"],      // p.ej. "mi-api"
        ValidateAudience = false,
        //ValidAudience = builder.Configuration["Jwt:Audience"],    // p.ej. "mis-clientes"

        // Control de tiempo de vida
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(2)
    };
});

// Add category services to the container.
builder.Services.AddScoped<IQueryRepository<Category, CategoryEntity>, CategoryQueryRepository>();
builder.Services.AddScoped<IActionRepository<CategoryEntity>, CategoryActionRepository>();
builder.Services.AddScoped<IPresenter<CategoryEntity, CategoryViewModel>, CategoryPresenter>();
builder.Services.AddScoped<IMapper<CategoryRequestDTO, CategoryEntity>, CategoryMapper>();
builder.Services.AddScoped<GetCategoryUseCase<Category, CategoryEntity, CategoryViewModel>>();
builder.Services.AddScoped<GetCategoryByIdUseCase<Category, CategoryEntity, CategoryViewModel>>();
builder.Services.AddScoped<PostCategoryUseCase<CategoryRequestDTO, CategoryEntity, CategoryViewModel>>();
builder.Services.AddScoped<UpdateCategoryUseCase<CategoryRequestDTO, CategoryEntity, CategoryViewModel>>();
builder.Services.AddScoped<DeleteCategoryUseCase<CategoryEntity, CategoryViewModel>>();

// Add product services to the container
builder.Services.AddScoped<IQueryRepository<Product, ProductEntity>, ProductQueryRepository>();
builder.Services.AddScoped<IActionRepository<ProductEntity>, ProductActionRepository>();
builder.Services.AddScoped<IPresenter<ProductEntity, ProductViewModel>, ProductPresenter>();
builder.Services.AddScoped<IMapper<ProductRequestDTO, ProductEntity>, ProductMapper>();
builder.Services.AddScoped<GetProductUseCase<Product, ProductEntity, ProductViewModel>>();
builder.Services.AddScoped<GetProductByIdUseCase<Product, ProductEntity, ProductViewModel>>();
builder.Services.AddScoped<PostProductUseCase<ProductRequestDTO, ProductEntity, ProductViewModel>>();
builder.Services.AddScoped<UpdateProductUseCase<ProductRequestDTO, ProductEntity, ProductViewModel>>();
builder.Services.AddScoped<DeleteProductUseCase<ProductEntity, ProductViewModel>>();

// Add order services to the container.
builder.Services.AddScoped<IQueryRepository<Order, OrderEntity>, OrderQueryRepository>();
builder.Services.AddScoped<IActionRepository<OrderEntity>, OrderActionRepository>();
builder.Services.AddScoped<IMapper<OrderRequestDTO, OrderEntity>, OrderMapper>();
builder.Services.AddScoped<IPresenter<OrderEntity, OrderViewModel>, OrderPresenter>();
builder.Services.AddScoped<GetOrderUseCase<Order, OrderEntity, OrderViewModel>>();
builder.Services.AddScoped<GetOrderByIdUseCase<Order, OrderEntity, OrderViewModel>>();
builder.Services.AddScoped<PostOrderUseCase<OrderRequestDTO, OrderEntity, OrderViewModel>>();
builder.Services.AddScoped<UpdateOrderUseCase<OrderRequestDTO, OrderEntity, OrderViewModel>>();
builder.Services.AddScoped<DeleteOrderUseCase<OrderEntity, OrderViewModel>>();

// Add sale services to the container.
builder.Services.AddScoped<IQueryRepository<Sale, SaleEntity>, SaleQueryRepository>();
builder.Services.AddScoped<IActionRepository<SaleEntity>, SaleActionRepository>();
builder.Services.AddScoped<IMapper<SaleRequestDTO, SaleEntity>, SaleMapper>();
builder.Services.AddScoped<IPresenter<SaleEntity, SaleViewModel>, SalePresenter>();
builder.Services.AddScoped<GetSaleUseCase<Sale, SaleEntity, SaleViewModel>>();
builder.Services.AddScoped<GetSaleByIdUseCase<Sale, SaleEntity, SaleViewModel>>();
builder.Services.AddScoped<PostSaleUseCase<SaleRequestDTO, SaleEntity, SaleViewModel>>();
builder.Services.AddScoped<UpdateSaleUseCase<SaleRequestDTO, SaleEntity, SaleViewModel>>();
builder.Services.AddScoped<DeleteSaleUseCase<SaleEntity, SaleViewModel>>();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Añadir roles Admin y Employee
// Rol para el dueño de la tienda y manager? el rol en ingles para el dueño de la tienda sería "Owner" o "Manager" y para un encargado de tienda sería "StoreManager" o "Supervisor". 
var roles = new[] { "Admin", "Employee", "Customer", "Supervisor" };

app.MapPost("/api/login", async (UserRequestDTO dto, UserManager<User> userManager) =>
{
    if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
    {
        return Results.BadRequest("Faltan campos requeridos para el inicio de sesión.");
    }

    // Buscar el usuario por nombre de usuario
    var user = await userManager.FindByEmailAsync(dto.Email);
    if (user == null)
    {
        return Results.NotFound("Usuario no encontrado.");
    }
    // Validar que el usuario no esté bloqueado
    if (user.LockoutEnabled && user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTimeOffset.UtcNow)
    {
        return Results.BadRequest("El usuario está bloqueado temporalmente.");
    }
    // Verificar la contraseña del usuario
    var isPasswordValid = await userManager.CheckPasswordAsync(user, dto.Password);
    if (!isPasswordValid)
    {
        // Si la contraseña es incorrecta, incrementar el contador de intentos fallidos
        await userManager.AccessFailedAsync(user);
        // Verificar si el usuario ha alcanzado el límite de intentos fallidos
        if (await userManager.GetAccessFailedCountAsync(user) >= 5)
        {
            // Bloquear al usuario si ha alcanzado el límite de intentos fallidos
            await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(15)); // Bloquear por 15 minutos
            return Results.BadRequest("El usuario ha sido bloqueado temporalmente por demasiados intentos fallidos.");
        }
        return Results.BadRequest("Datos incorrectos verifique el usuario y contraseña.");
    }
    // Recuperar los roles y permisos del usuario 
    var userRoles = await userManager.GetRolesAsync(user);
    var userClaims = await userManager.GetClaimsAsync(user);
    if (userRoles.Count == 0 && userClaims.Count == 0)
    {
        return Results.BadRequest("El usuario no tiene roles ni claims asignados.");
    }
    // Generar un token JWT para el usuario
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName!)
    };
    claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
    claims.AddRange(userClaims.Select(claim => new Claim(claim.Type, claim.Value)));
    var credsKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["APIKeyPruebas"]!));
    var creds = new SigningCredentials(credsKey, SecurityAlgorithms.HmacSha256);
    var token = new JwtSecurityToken(
        issuer: null, // Especificar un emisor si se desea
        audience: null, // Especificar una audiencia si se desea
        claims: claims,
        expires: DateTime.UtcNow.AddHours(14), // El token expira en 14 horas
        signingCredentials: creds
    );
    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
    return Results.Ok(new { token = tokenString });
});

// Update the line causing the error by explicitly setting the properties of the Role object
app.MapPost("/api/create-roles", async (RoleManager<IdentityRole<int>> roleManager) =>
{
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            var newRole = new IdentityRole<int>
            {
                Name = role
            };
            await roleManager.CreateAsync(newRole);
        }
    }
    return TypedResults.Ok("Roles created successfully!");
})
.RequireAuthorization("IsEmployee");

app.MapPost("/api/create-user", async (UserRequestDTO userDto, UserManager<User> userManager) =>
{
    if (string.IsNullOrWhiteSpace(userDto.UserName) ||
        string.IsNullOrWhiteSpace(userDto.Email) ||
        string.IsNullOrWhiteSpace(userDto.Password) ||
        string.IsNullOrWhiteSpace(userDto.FirstName) ||
        string.IsNullOrWhiteSpace(userDto.LastName) ||
        userDto.UserStatusID == 0)
    {
        return Results.BadRequest("Faltan campos requeridos para el registro.");
    }

    var user = new User
    {
        UserName = userDto.UserName,
        Email = userDto.Email,
        PhoneNumber = userDto.PhoneNumber,
        FirstName = userDto.FirstName,
        LastName = userDto.LastName,
        UserStatusID = userDto.UserStatusID,
        BranchID = userDto.BranchID
    };

    var result = await userManager.CreateAsync(user, userDto.Password);

    if (!result.Succeeded)
    {
        var errors = string.Join("; ", result.Errors.Select(e => e.Description));
        return Results.BadRequest($"No se pudo crear el usuario: {errors}");
    }

    return Results.Ok("Usuario creado exitosamente.");
})
.RequireAuthorization("IsEmployee");

app.MapPost("/api/assign-role", async (string roleToAssign, string userEmail, UserManager<User> userManager) =>
{
    var user = await userManager.FindByEmailAsync(userEmail); //new User { UserName = "testuser@example.com", Email = "testuser@example.com" };
    if (user == null)
        return TypedResults.NotFound("User not found.");
    await userManager.AddToRoleAsync(user, roleToAssign);

    var isUserInRole = await userManager.IsInRoleAsync(user, "Admin");

    return isUserInRole ? Results.Ok($"User is in role: {isUserInRole}") : Results.BadRequest("User is not in role.");
})
.RequireAuthorization("IsEmployee");

app.MapCategoryEndpoints();
app.MapProductEndpoints();
app.MapOrderEndpoints();


app.Run();
