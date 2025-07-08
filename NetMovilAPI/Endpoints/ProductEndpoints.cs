using Microsoft.OpenApi.Models;
using NetMovilAPI.Application.DTOs.Requests;
using NetMovilAPI.Application.Presenters.ViewModels;
using NetMovilAPI.Application.UseCases.ProductUseCases;
using NetMovilAPI.Domain.Entities.BaseEntities;
using NetMovilAPI.Domain.Entities.Product;
using NetMovilAPI.Infraestructure.Models.ProductModels;

namespace NetMovilAPI.Endpoints;
public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/products");

        group.MapGet("", async Task<IResult> (GetProductUseCase<Product, ProductEntity, ProductViewModel> useCase) =>
        {
            var result = await useCase.ExecuteAsync(c => c.ProductStatusID > 1);
            if (result == null || result.Count() == 0)
            {
                return TypedResults.NotFound(new ApiResponse<IEnumerable<ProductViewModel>>("No se pudieron recuperar correctamente los registros"));
            }
            var response = new ApiResponse<IEnumerable<ProductViewModel>>(result, "Se han recuperado con éxito los registros");
            return TypedResults.Ok(response);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Get all products";
            operation.Description = "Retrieves all the products";
            operation.Responses["400"] = new OpenApiResponse
            {
                Description = "It was not possible to retrieve products."
            };
            return operation;
        });

        group.MapGet("/{id:int}", async Task<IResult> (int id, GetProductByIdUseCase<Product, ProductEntity, ProductViewModel> useCase) =>
        {
            ApiResponse<ProductViewModel> response;
            if (id < 1)
            {
                response = new("El id debe ser mayor o igual a 1");
                return TypedResults.BadRequest(response);
            }
            var result = await useCase.ExecuteAsync(c => c.ProductID == id);
            if (result.ProductID == 0)
            {
                response = new("El id solicitado no fue encontrado");
                return TypedResults.NotFound(response);
            }
            response = new(result, "Recuperación exitosa");
            return TypedResults.Ok(response);
        })
        .WithOpenApi(operation =>
        {
            operation.Parameters[0].Description = "Id of the product to retrieve";
            operation.Summary = "Get product by Id";
            operation.Description = "Retrieves a product by its Id";
            operation.Responses["400"] = new OpenApiResponse
            {
                Description = "Datos incorrectos"
            };
            operation.Responses["404"] = new OpenApiResponse
            {
                Description = "Producto no encontrado"
            };
            return operation;
        });

        group.MapPost("", async Task<IResult> (ProductRequestDTO dto, PostProductUseCase<ProductRequestDTO, ProductEntity, ProductViewModel> useCase) =>
        {
            /*
             Aquí debería usar FluentValidation para validar el DTO
            if (!ModelState.IsValid)
            {
                return Results.ValidationProblem(ModelState);
            }
             */
            var result = await useCase.ExecuteAsync(dto);
            ApiResponse<ProductViewModel> response;
            if (result.Errors != null && result.Errors.Count > 0 || result.ProductID == 0)
            {
            }
            response = new(result, "Producto creado con éxito.");
            return TypedResults.Created($"/api/products/{result.ProductID}", response);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Saves a new Product";
            operation.Description = "Creates a new Product record in Database";
            operation.Responses["400"] = new OpenApiResponse
            {
                Description = "Solicitud incorrecta, revisar los datos solicitados"
            };
            return operation;
        });

        group.MapPut("", async Task<IResult> (ProductRequestDTO dto, UpdateProductUseCase<ProductRequestDTO, ProductEntity, ProductViewModel> useCase) =>
        {
            ApiResponse<ProductViewModel> response;
            if (dto.ProductID <= 0)
            {
                response = new("El id debe ser mayor o igual a 1");
                return TypedResults.BadRequest(response);
            }
            var result = await useCase.ExecuteAsync(dto);
            if (result.ProductID == 0)
            {
                response = new("El id solicitado no fue encontrado");
                return TypedResults.NotFound(response);
            }
            else if (result.ProductID < 0)
            {
                // Se asume que un id negativo indica un error en la actualización
                response = new("No se pudo actualizar la categoría. Verifique los datos enviados y vuelva a intentarlo.");
                return TypedResults.BadRequest(response);

            }
            response = new(result, "Recuperación exitosa");
            // Indicar que la categoría fue actualizada correctamente
            return TypedResults.Accepted($"/api/categories/{result.ProductID}", response);
        })
            .WithOpenApi(operation =>
            {
                operation.Summary = "Update a product";
                operation.Responses["400"] = new OpenApiResponse
                {
                    Description = "Solicitud incorrecta"
                };
                operation.Responses["404"] = new OpenApiResponse
                {
                    Description = "Recurso no encontrado"
                };
                return operation;
            });

        group.MapDelete("/{id:int}", async Task<IResult> (int id, int idUser, DeleteProductUseCase<ProductEntity, ProductViewModel> useCase) =>
        {
            ApiResponse<ProductViewModel> response;
            if (id < 1)
            {
                response = new("El id debe ser mayor o igual a 1");
                return TypedResults.BadRequest(response);
            }
            var result = await useCase.ExecuteAsync(id, idUser);
            if (!result.Success)
            {
                return TypedResults.UnprocessableEntity(result);
            }
            return TypedResults.NoContent();
        })
        .WithOpenApi(operation =>
        {
            operation.Parameters[0].Description = "Id of the product to delete";
            operation.Description = "Deletes a product by its Id";
            operation.Responses["400"] = new OpenApiResponse
            {
                Description = "El id no cumple con el formato requerido"
            };
            operation.Responses["404"] = new OpenApiResponse
            {
                Description = "No existe un producto con el Id especificado"
            };
            return operation;
        });
    }
}