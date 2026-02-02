using Solar.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Solar
{// Exceção de domínio para usuário já existente
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException() { }

        public UserAlreadyExistsException(string message) 
            : base(message) { }

        public UserAlreadyExistsException(string message, Exception inner) 
            : base(message, inner) { }
    }

    public class UserService
    {
        private readonly AppDbContext _db;

        private bool IsUniqueConstraintViolation(DbUpdateException ex)
        {
            return ex.InnerException != null && ex.InnerException.Message.Contains("UNIQUE constraint failed");
        }
        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User> CreateUserAsync(CreateUserDto userDto)
        {
            try
            {                
                var user = new User
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
                };

                _db.Users.Add(user);
                await _db.SaveChangesAsync();

                return user;
            }
            catch (DbUpdateException ex)
            {
                if (IsUniqueConstraintViolation(ex))
                {
                    throw new UserAlreadyExistsException("A user with the same email or name already exists.");
                }
                throw;
            }
        }

        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            return await _db.Users
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email
                })
                .ToListAsync();
        }
    }
}