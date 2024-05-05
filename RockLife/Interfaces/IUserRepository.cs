using System.Collections.Generic;
using RockLife.Models;

namespace RockLife.Interfaces
{
    public interface IUserRepository
    {
        //Task<User> GetUserByIdAsync(int id);
        //Task<List<User>> GetAllUsersAsync();
        //Task AddUserAsync(User user);
        //Task UpdateUserAsync(User user);
        //Task DeleteUserByIdAsync(int id);
        //Task<List<User>> FindUsersByLoginAsync(string login);
        //Task<int?> GetUserIdByUsernameAsync(string username);

        void AddUser(User user);

        User GetUserById(int id);

        void UpdateUser(User user);

        void DeleteUserById(int id);

        List<User> GetAllUsers();

        List<User> FindUsersByLogin(string login);

        int? GetUserIdByUsername(string username);
    }
}