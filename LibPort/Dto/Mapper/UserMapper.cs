using LibPort.Dto.Request;
using LibPort.Models;
using LibPort.Exceptions;
using LibPort.Dto.Response;

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

        public static ShowUser ToShow(this User entity)
        {
            return new ShowUser
            {
                Id = entity.Id,
                Username = entity.Username,
                Email = entity.Email,
                UserType = entity.UserType
            };
        }

        public static User ToEntity(this ShowUser show)
        {
            return new User
            {
                Id = show.Id,
                Username = show.Username,
                Email = show.Email,
                UserType = show.UserType
            };
        }
    }
}
