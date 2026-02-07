namespace Solar.Repositories
{
    public interface IUserRepository
    {
        User? GetByEmail(string email);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(int id);
        Task<List<User>> GetAllAsync();
    }
}
