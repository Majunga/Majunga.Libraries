namespace Majunga.Libraries.Infrastructure.Entities
{

    public abstract class Entity : EntityBase<int>
    {
        protected Entity() : base()
        {
        }

        protected Entity(int id) : base(id)
        {
        }
    }
}
