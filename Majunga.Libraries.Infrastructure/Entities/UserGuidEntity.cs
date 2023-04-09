using System;
using System.ComponentModel.DataAnnotations;

namespace Majunga.Libraries.Infrastructure.Entities
{
    public abstract class UserGuidEntity : GuidEntity, IUserEntity
    {
        protected UserGuidEntity() : base()
        {
        }

        protected UserGuidEntity(Guid id) : base(id)
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
