using System;

namespace Migrations.Annotations
{
    /// <summary>
    ///     Used for associating a metadata class with the entity class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MetadataTypeAttribute : Attribute
    {
        readonly Type metadataClassType;

        public MetadataTypeAttribute(Type metadataClassType)
        {
            this.metadataClassType = metadataClassType;
        }

        public Type MetadataClassType
        {
            get
            {
                if (metadataClassType == null) throw new InvalidOperationException("Type cannot be null");
                return metadataClassType;
            }
        }
    }
}