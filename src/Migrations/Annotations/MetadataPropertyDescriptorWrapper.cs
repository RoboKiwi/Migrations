using System;
using System.ComponentModel;
using System.Linq;

namespace Migrations.Annotations
{
    class MetadataPropertyDescriptorWrapper : PropertyDescriptor
    {
        readonly PropertyDescriptor descriptor;
        readonly bool isReadOnly;

        public MetadataPropertyDescriptorWrapper(PropertyDescriptor descriptor, Attribute[] newAttributes) : base(descriptor, newAttributes)
        {
            this.descriptor = descriptor;
            var readOnlyAttribute = newAttributes.OfType<ReadOnlyAttribute>().FirstOrDefault();
            isReadOnly = readOnlyAttribute != null ? readOnlyAttribute.IsReadOnly : false;
        }

        public override Type ComponentType => descriptor.ComponentType;

        public override bool IsReadOnly => isReadOnly || descriptor.IsReadOnly;

        public override Type PropertyType => descriptor.PropertyType;

        public override bool SupportsChangeEvents => descriptor.SupportsChangeEvents;

        public override void AddValueChanged(object component, EventHandler handler)
        {
            descriptor.AddValueChanged(component, handler);
        }

        public override bool CanResetValue(object component)
        {
            return descriptor.CanResetValue(component);
        }

        public override object GetValue(object component)
        {
            return descriptor.GetValue(component);
        }

        public override void RemoveValueChanged(object component, EventHandler handler)
        {
            descriptor.RemoveValueChanged(component, handler);
        }

        public override void ResetValue(object component)
        {
            descriptor.ResetValue(component);
        }

        public override void SetValue(object component, object value)
        {
            descriptor.SetValue(component, value);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return descriptor.ShouldSerializeValue(component);
        }
    }
}