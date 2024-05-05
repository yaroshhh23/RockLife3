using Microsoft.EntityFrameworkCore;
using RockLife.Models;
using RockLife.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockLife.Repository
{
    internal class UserRepository 
    {
        private readonly MyAppContext _context;

        public UserRepository(MyAppContext context)
        {
            _context = context;
        }

        
        public async Task AddUserAsync(User user)
        {
            try
            {
                // Проверка на валидность модели перед добавлением
                Validator.ValidateObject(user, new ValidationContext(user), true);
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                Console.WriteLine($"Ошибка обновления базы данных: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Внутреннее исключение: {ex.InnerException.Message}");
                }
                throw; // Переброс исключения
            }
            //// Проверка на валидность модели перед добавлением
            //Validator.ValidateObject(user, new ValidationContext(user), true);
            //await _context.Users.AddAsync(user);
            //await _context.SaveChangesAsync();
        }

        // Получение пользователя по ID
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        // Обновление данных пользователя
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // Удаление пользователя по ID
        public async Task DeleteUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        // Получение всех пользователей
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Поиск пользователей по логину
        public async Task<List<User>> FindUsersByLoginAsync(string login)
        {
            return await _context.Users
                .Where(u => u.Login.Contains(login))
                .ToListAsync();
        }

        public async Task<int?> GetUserIdByUsernameAsync(string username)
        {
            var user = await _context.Users
                .Where(u => u.Login.ToLower() == username.ToLower())
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            return user;
        }


    }
}
