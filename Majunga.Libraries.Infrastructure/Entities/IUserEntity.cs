namespace Majunga.Libraries.Infrastructure.Entities
{
    public interface IUserEntity
    {
        public string? UserId { get; }

        public void SetUserId(string? userId);
    }
}