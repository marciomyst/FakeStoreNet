namespace FakeStoreNet.Domain.Common
{
    /// <summary>
    /// Base class for entities with identity equality.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Gets the unique identifier for this entity.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Determines whether the specified object is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns>True if the specified object is equal to this instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is not Entity other || GetType() != other.GetType())
                return false;

            // Two entities are equal if they have the same Id (and are not transient).
            return Id != default && Id == other.Id;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => (GetType().ToString() + Id).GetHashCode();
    }
}
