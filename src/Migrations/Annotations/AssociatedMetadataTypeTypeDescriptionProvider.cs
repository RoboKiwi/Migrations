using System;
using System.ComponentModel;

namespace Migrations.Annotations
{
    public class AssociatedMetadataTypeTypeDescriptionProvider : TypeDescriptionProvider
    {
        readonly Type associatedMetadataType;

        public AssociatedMetadataTypeTypeDescriptionProvider(Type type) : base(TypeDescriptor.GetProvider(type))
        {
        }

        public AssociatedMetadataTypeTypeDescriptionProvider(Type type, Type associatedMetadataType) : this(type)
        {
            this.associatedMetadataType = associatedMetadataType ?? throw new ArgumentNullException(nameof(associatedMetadataType));
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            var baseDescriptor = base.GetTypeDescriptor(objectType, instance);
            return new AssociatedMetadataTypeTypeDescriptor(baseDescriptor, objectType, associatedMetadataType);
        }
    }
}