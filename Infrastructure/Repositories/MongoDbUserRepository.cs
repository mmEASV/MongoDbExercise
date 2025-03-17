using MongoDB.Driver;
using MongoDbExercise.Core.Domain.Interfaces;
using MongoDbExercise.Infrastructure.Data;
using MongoDbExercise.Models;

namespace MongoDbExercise.Infrastructure.Repositories;

public class MongoDbUserRepository : IUserRepository
{
    private readonly MongoDbContext _context;

    public MongoDbUserRepository(MongoDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<User>> GetUsersAsync () =>
        await _context.Users.Find(_ => true).ToListAsync();
    public async Task<User?> GetUserAsync (string id) =>
        await _context.Users.Find(u => u.Id == id).FirstOrDefaultAsync();
    public async Task CreateUserAsync (User user) =>
        await _context.Users.InsertOneAsync(user);
    public async Task UpdateUserAsync (User user) =>
        await _context.Users.ReplaceOneAsync(u => u.Id == user.Id, user);
    public async Task DeleteUserAsync (string id) =>
        await _context.Users.DeleteOneAsync(u => u.Id == id);
}