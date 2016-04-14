using System;

namespace Framework.Common.Domain
{
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public Entity() { }

        public Entity(TPrimaryKey id)
        {
            this.Id = id;
        }

        public TPrimaryKey Id
        {
            get;
            protected set;
        }

        public override Boolean Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TPrimaryKey>)) { return false; }

            if (object.ReferenceEquals(this, obj)) { return true; }

            var item = (Entity<TPrimaryKey>)obj;

            return item.Id.Equals(this.Id);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        /// <inheritdoc />
        public static bool operator ==(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            return object.Equals(left, null) ? (object.Equals(right, null)) : left.Equals(right);
        }

        /// <inheritdoc />
        public static bool operator !=(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            return !(left == right);
        }
    }
}