using Microsoft.OpenApi.Models;
using NetMovilAPI.Application.DTOs.Requests;
using NetMovilAPI.Application.Presenters.ViewModels;
using NetMovilAPI.Application.UseCases.CategoryUseCases;
using NetMovilAPI.Domain.Entities.BaseEntities;
using NetMovilAPI.Domain.Entities.Shared;
using NetMovilAPI.Infraestructure.Models.Shared;

namespace NetMovilAPI.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/categories");


        group.MapGet("", async Task<IResult> (GetCategoryUseCase<Category, CategoryEntity, CategoryViewModel> useCase) =>
        {
            var result = await useCase.ExecuteAsync(c => c.CategoryStatusID != 0);
            if (result == null || result.Any())
            {
                return TypedResults.NotFound(new ApiResponse<IEnumerable<CategoryViewModel>>("No se pudieron recuperar correctamente los registros"));
            }
            var response = new ApiResponse<IEnumerable<CategoryViewModel>>(result, "Se han recuperado con éxito los registros");
            return TypedResults.Ok(response);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Get all categories";
            operation.Description = "Retrieves all the categories";
            operation.Responses["404"] = new OpenApiResponse
            {
                Description = "No fue posible recuperar los registros"
            };
            return operation;
        });

        group.MapGet("/{id:int}", async Task<IResult> (int id, GetCategoryByIdUseCase<Category, CategoryEntity, CategoryViewModel> useCase) =>
        {
            ApiResponse<CategoryViewModel> response;
            if (id < 1)
            {
                response = new("El id debe ser mayor o igual a 1");
                return TypedResults.BadRequest(response);
            }
            var result = await useCase.ExecuteAsync(c => c.CategoryID == id);
            if (result.CategoryID == 0)
            {
                response = new("El id solicitado no fue encontrado");
                return TypedResults.NotFound(response);
            }
            response = new(result, "Recuperación exitosa");
            return TypedResults.Ok(response);
        })
        .WithOpenApi(operation =>
        {
            operation.Parameters[0].Description = "Id of the category to retrieve";
            operation.Summary = "Get category by Id";
            operation.Description = "Retrieves a category by its Id";
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

        group.MapPost("", async Task<IResult> (CategoryRequestDTO dto, PostCategoryUseCase<CategoryRequestDTO, CategoryEntity, CategoryViewModel> useCase) =>
        {
            /*
             Aquí debería usar FluentValidation para validar el DTO
            if (!ModelState.IsValid)
            {
                return Results.ValidationProblem(ModelState);
            }
             */
            var result = await useCase.ExecuteAsync(dto);
            ApiResponse<CategoryViewModel> response = result.CategoryID is 0
                                                        ? new(result.Description!)
                                                        : new(result, "Categoría creada con éxito");
            return result is null
                ? TypedResults.BadRequest(response)
                : TypedResults.Created($"/api/categories/{result.CategoryID}", response);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Saves a new Category";
            operation.Description = "Creates a new Category record in Database";
            operation.Responses["400"] = new OpenApiResponse
            {
                Description = "Solicitud incorrecta"
            };
            return operation;
        });

        group.MapPut("", async Task<IResult> (CategoryRequestDTO dto, UpdateCategoryUseCase<CategoryRequestDTO, CategoryEntity, CategoryViewModel> useCase) =>
        {
            ApiResponse<CategoryViewModel> response;
            if (dto.CategoryID <= 0)
            {
                response = new("El id debe ser mayor o igual a 1");
                return TypedResults.BadRequest(response);
            }
            var result = await useCase.ExecuteAsync(dto);
            if (result.CategoryID == 0)
            {
                response = new("El id solicitado no fue encontrado");
                return TypedResults.NotFound(response);
            }
            else if (result.CategoryID < 0)
            {
                // Se asume que un id negativo indica un error en la actualización
                response = new("No se pudo actualizar la categoría. Verifique los datos enviados y vuelva a intentarlo.");
                return TypedResults.BadRequest(response);

            }
            response = new(result, "Recuperación exitosa");
            // Indicar que la categoría fue actualizada correctamente
            return TypedResults.Accepted($"/api/categories/{result.CategoryID}", response);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Update a category";
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

        group.MapDelete("/{id:int}", async Task<IResult> (int id, int idUser, DeleteCategoryUseCase<CategoryEntity, CategoryViewModel> useCase) =>
        {
            ApiResponse<CategoryViewModel> response;
            if (id < 1)
            {
                response = new("El id debe ser mayor o igual a 1");
                return TypedResults.BadRequest(response);
            }
            var result = await useCase.ExecuteAsync(id, idUser);
            if (result.Data!.CategoryID == 0)
            {
                response = new("El id solicitado no fue encontrado");
                return TypedResults.NotFound(response);
            }
            return TypedResults.NoContent();
        })
        .WithOpenApi(operation =>
        {
            operation.Parameters[0].Description = "Id of the category to delete";
            operation.Summary = "Delete category by Id";
            operation.Description = "Deletes a category by its Id";
            operation.Responses["400"] = new OpenApiResponse
            {
                Description = "Solicitud incorrecta"
            };
            operation.Responses["404"] = new OpenApiResponse
            {
                Description = "Recurso no encontrado"
            };
            operation.Responses["204"] = new OpenApiResponse
            {
                Description = "Recurso eliminado con éxito"
            };
            return operation;
        });
    }
}

