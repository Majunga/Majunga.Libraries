using System.ComponentModel.DataAnnotations;

namespace Majunga.Libraries.Infrastructure.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }

    public abstract class EntityBase<TKey> : IEntity<TKey>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected EntityBase()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        protected EntityBase(TKey id)
        {
            Id = id;
        }

        [Key]
        public TKey Id { get; set; }

        internal void SetId(TKey id)
        {
            Id = id;
        }
    }
}
