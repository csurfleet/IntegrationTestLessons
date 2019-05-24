using System;
using System.Collections.Generic;

namespace Lesson01.RepairRequestApi.Models
{
    public class User
    {
        public User() {}

        public User(int id, ICollection<UserRole> userRoles)
        {
            Id = id;
            UserRoles = userRoles ?? throw new ArgumentNullException(nameof(userRoles));
        }

        public int Id { get; }

        public ICollection<UserRole> UserRoles { get; }
    }
}
