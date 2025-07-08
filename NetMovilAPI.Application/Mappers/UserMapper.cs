using NetMovilAPI.Application.DTOs.Requests;
using NetMovilAPI.Domain.Entities.User;
using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.Mappers;

public class UserMapper : IMapper<UserRequestDTO, UserEntity>
{
    public UserEntity ToEntity(UserRequestDTO dto)
    {
        return new UserEntity
        {
            Id = dto.Id,
            FirstName = dto.FirstName ?? string.Empty,
            LastName = dto.LastName ?? string.Empty,
            Email = dto.Email ?? string.Empty,
            PhoneNumber = dto.PhoneNumber,
            UserStatusID = dto.UserStatusID,
            UserName = dto.UserName ?? string.Empty,
        };
    }
    public IEnumerable<UserEntity> ToEntity(List<UserRequestDTO> dtos) => [.. dtos.Select(ToEntity)];
}

