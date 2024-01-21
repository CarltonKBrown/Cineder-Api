using PreventR;

namespace Cineder_Api.Core.Entities
{
    public abstract class Entity
    {
        protected Entity(long id, string name)
        {
            Id = id;
            Name = name.Prevent(nameof(name)).Null();
        }

        protected Entity() : this(0, string.Empty)
        {

        }

        public long Id { get; protected set; }
        public string Name { get; protected set; }
    }
}
