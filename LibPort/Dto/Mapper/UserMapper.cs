using LibPort.Dto.Request;
using LibPort.Models;
using LibPort.Exceptions;

namespace LibPort.Dto.Mapper
{
    public static class UserMapper
    {
        public static User ToEntity(this RequestUser request)
        {
            if (Guid.TryParse(request.Id, out Guid userId))
                throw new NotValidIdException($"Id {request.Id} is not a valid Guid");
            return new User
            {
                Id = userId,
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
            };
        }
    }
}
