using Microsoft.OpenApi.Models;
using NetMovilAPI.Application.DTOs.Requests;
using NetMovilAPI.Application.Presenters.ViewModels;
using NetMovilAPI.Application.UseCases.OrderUseCases;
using NetMovilAPI.Domain.Entities.BaseEntities;
using NetMovilAPI.Domain.Entities.Order;
using NetMovilAPI.Infraestructure.Models.OrderModels;

namespace NetMovilAPI.Endpoints;
public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/orders");

        group.MapGet("/{branchID:int}", async Task<IResult> (int branchID, GetOrderUseCase<Order, OrderEntity, OrderViewModel> useCase) =>
        {
            var result = await useCase.ExecuteAsync(o => o.OrderStatusID > 1 && o.BranchID == branchID);
            if (result == null || result.Count() == 0)
            {
                return TypedResults.NotFound(new ApiResponse<IEnumerable<OrderViewModel>>("No se pudieron recuperar correctamente los registros de órdenes"));
            }
            var response = new ApiResponse<IEnumerable<OrderViewModel>>(result, "Se han recuperado con éxito los registros de órdenes");
            return TypedResults.Ok(response);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Get all orders";
            operation.Description = "Retrieves all the orders";
            operation.Responses["400"] = new OpenApiResponse
            {
                Description = "No fue posible recuperar las órdenes"
            };
            return operation;
        });

        group.MapGet("/{id:int}", async Task<IResult> (int id, GetOrderByIdUseCase<Order, OrderEntity, OrderViewModel> useCase) =>
        {
            ApiResponse<OrderViewModel> response;
            if (id < 1)
            {
                response = new("El id debe ser mayor o igual a 1");
                return TypedResults.BadRequest(response);
            }
            var result = await useCase.ExecuteAsync(o => o.OrderID == id);
            if (result.OrderID == 0)
            {
                response = new("El id solicitado no fue encontrado");
                return TypedResults.NotFound(response);
            }
            response = new(result, "Recuperación exitosa de la orden");
            return TypedResults.Ok(response);
        })
        .WithOpenApi(operation =>
        {
            operation.Parameters[0].Description = "Id of the order to retrieve";
            operation.Summary = "Get order by Id";
            operation.Description = "Retrieves an order by its Id";
            operation.Responses["400"] = new OpenApiResponse
            {
                Description = "Datos incorrectos"
            };
            operation.Responses["404"] = new OpenApiResponse
            {
                Description = "Orden no encontrada"
            };
            return operation;
        });

        group.MapPost("", async Task<IResult> (OrderRequestDTO dto, PostOrderUseCase<OrderRequestDTO, OrderEntity, OrderViewModel> useCase) =>
        {
            /*
             Aquí debería usar FluentValidation para validar el DTO
            if (!ModelState.IsValid)
            {
                return Results.ValidationProblem(ModelState);
            }
             */
            var result = await useCase.ExecuteAsync(dto);
            ApiResponse<OrderViewModel> response;
            if (result.Errors != null && result.Errors.Count > 0 || result.OrderID == 0)
            {
                response = new()
                {
                    Message = "Fallo al guardar la orden. Verifique los datos ingresados",
                    Success = false,
                    Errors = result.Errors
                };
                return TypedResults.BadRequest(response);
            }
            response = new()
            {
                Message = "Orden creada con éxito.",
                Data = result,
                Success = true
            }
            ;
            return TypedResults.Created($"/api/orders/{result.OrderID}", result);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Saves a new Order";
            operation.Description = "Creates a new Order record in Database";
            operation.Responses["400"] = new OpenApiResponse
            {
                Description = "Solicitud incorrecta, revisar los datos solicitados"
            };
            return operation;
        });

        group.MapPut("", async Task<IResult> (OrderRequestDTO dto, UpdateOrderUseCase<OrderRequestDTO, OrderEntity, OrderViewModel> useCase) =>
        {
            /*
             Aquí debería usar FluentValidation para validar el DTO
            if (!ModelState.IsValid)
            {
                return Results.ValidationProblem(ModelState);
            }
             */
            ApiResponse<OrderViewModel> response;
            if (dto.OrderID <= 0)
            {
                response = new("El id debe ser mayor o igual a 1");
                return TypedResults.BadRequest(response);
            }
            var result = await useCase.ExecuteAsync(dto);
            if (result.OrderID == 0)
            {
                response = new("El id solicitado no fue encontrado");
                return TypedResults.NotFound(response);
            }
            else if (result.OrderID < 0)
            {
                // Se asume que un id negativo indica un error en la actualización
                response = new("No se pudo actualizar la categoría. Verifique los datos enviados y vuelva a intentarlo.");
                return TypedResults.BadRequest(response);

            }
            response = new(result, "Recuperación exitosa");
            // Indicar que la categoría fue actualizada correctamente
            return TypedResults.Accepted($"/api/categories/{result.OrderID}", response);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Update an order";
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

        group.MapDelete("/{id:int}", async Task<IResult> (int id, int idUser, DeleteOrderUseCase<OrderEntity, OrderViewModel> useCase) =>
        {
            ApiResponse<OrderViewModel> response;
            if (id < 1)
            {
                response = new("El id debe ser mayor o igual a 1");
                return TypedResults.BadRequest(response);
            }
            if (idUser <= 0)
            {
                response = new("No fue posible encontrar al usuario.");
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
            operation.Parameters[0].Description = "Id of the order to delete";
            operation.Description = "Deletes an order by its Id";
            operation.Responses["400"] = new OpenApiResponse
            {
                Description = "El id no cumple con el formato requerido"
            };
            operation.Responses["404"] = new OpenApiResponse
            {
                Description = "No existe una orden con el Id especificado"
            };
            return operation;
        });
    }
}
