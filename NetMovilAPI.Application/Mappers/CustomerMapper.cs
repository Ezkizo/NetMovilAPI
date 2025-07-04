using NetMovilAPI.Application.DTOs.Requests;
using NetMovilAPI.Domain.Entities.User;
using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.Mappers;
public class CustomerMapper : IMapper<CustomerRequestDTO, CustomerEntity>
{
    public CustomerEntity ToEntity(CustomerRequestDTO dto)
    {
        return new CustomerEntity
        {
            CustomerID = dto.CustomerID,
            UserID = dto.UserID,
        };
    }

    public IEnumerable<CustomerEntity> ToEntity(List<CustomerRequestDTO> dtos) => [.. dtos.Select(ToEntity)];
}

public class CustomerAddressMapper : IMapper<CustomerAddressRequestDTO, CustomerAddressEntity>
{
    public CustomerAddressEntity ToEntity(CustomerAddressRequestDTO dto)
    {
        return new CustomerAddressEntity
        {
            CustomerAddressID = dto.CustomerAddressID,
            CustomerID = dto.CustomerID,
            Street = dto.Street,
            City = dto.City,
            DeliveryReferences = dto.DeliveryReferences,
            PostalCode = dto.PostalCode,
            State = dto.State,
            Country = dto.Country
        };
    }

    public IEnumerable<CustomerAddressEntity> ToEntity(List<CustomerAddressRequestDTO> dtos) =>
        [.. dtos.Select(ToEntity)];
}