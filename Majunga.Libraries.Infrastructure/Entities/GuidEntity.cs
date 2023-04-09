using System;

namespace Majunga.Libraries.Infrastructure.Entities
{
    public abstract class GuidEntity : EntityBase<Guid>
    {
        protected GuidEntity() : base()
        {
        }

        protected GuidEntity(Guid id) : base(id)
        {
        }
    }
}
