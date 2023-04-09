using System;
using System.ComponentModel.DataAnnotations;

namespace Majunga.Libraries.Infrastructure.Entities
{
    public abstract class UserEntity : Entity, IUserEntity
    {
        protected UserEntity() : base()
        {
        }

        protected UserEntity(int id) : base(id)
        {
        }

        [MaxLength(36)]
        public string? UserId { get; set; }

        public void SetUserId(string? userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new UnauthorizedAccessException();
            UserId = userId;
        }
    }
}
