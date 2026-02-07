using Solar.Dtos;
using Solar.Repositories;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Solar
{
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
    public class LoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public LoginService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key not configured"))
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string? Login(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);

            if (user == null)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            return GenerateJwtToken(user);
        }
    }
}