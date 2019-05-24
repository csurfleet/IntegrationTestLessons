using Lesson01.UsersApi.Models;
using System;

namespace Lesson01.UsersApi.Repositories
{
    public interface IUserRepository
    {
        User Get(int id);
    }

    internal class BrokenUserRepository : IUserRepository
    {
        public User Get(int id) => throw new Exception($"Some error retrieving user {id}, oh no");
    }
}
