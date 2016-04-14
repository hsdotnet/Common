using System;
using System.Linq;
using System.Reflection;

namespace Framework.Common.Domain
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValueObject"></typeparam>
    public abstract class ValueObject<TValueObject> : IEquatable<TValueObject>
         where TValueObject : ValueObject<TValueObject>
    {
        public bool Equals(TValueObject other)
        {
            if (other == null) { return false; }

            PropertyInfo[] publicProperties = this.GetType().GetProperties();

            if (publicProperties != null && publicProperties.Any())
            {
                bool result = true;

                foreach (PropertyInfo property in publicProperties)
                {
                    if (!property.GetValue(this, null).Equals(property.GetValue(other, null)))
                    {
                        result = false;
                        break;
                    }
                }

                return result;
            }
            else { return true; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) { return false; }

            ValueObject<TValueObject> valueObject = obj as ValueObject<TValueObject>;

            return valueObject != null ? Equals((TValueObject)valueObject) : false;
        }

        public override int GetHashCode()
        {
            int hashCode = 31;

            bool changeMultiplier = false;

            int index = 1;

            PropertyInfo[] publicProperties = this.GetType().GetProperties();

            if (publicProperties != null && publicProperties.Any())
            {
                foreach (PropertyInfo property in publicProperties)
                {
                    object value = property.GetValue(this, null);

                    if (value != null)
                    {
                        hashCode = hashCode * ((changeMultiplier) ? 59 : 114) + value.GetHashCode();

                        changeMultiplier = !changeMultiplier;
                    }
                    else { hashCode = hashCode ^ (index * 13); }
                }
            }

            return hashCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
        {
            if (ReferenceEquals(left, right)) { return true; }

            if ((left == null) || (right == null)) { return false; }

            return left.Equals(right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
        {
            return !(left == right);
        }

        public virtual TValueObject Clone()
        {
            return this.MemberwiseClone() as TValueObject;
        }
    }
}