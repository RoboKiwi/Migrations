using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Migrations.Annotations
{
    class AssociatedMetadataTypeTypeDescriptor : CustomTypeDescriptor
    {
        public AssociatedMetadataTypeTypeDescriptor(ICustomTypeDescriptor parent, Type type, Type associatedMetadataType) : base(parent)
        {
            AssociatedMetadataType = associatedMetadataType ?? TypeDescriptorCache.GetAssociatedMetadataType(type);
            IsSelfAssociated = type == AssociatedMetadataType;
            if (AssociatedMetadataType != null) TypeDescriptorCache.ValidateMetadataType(type, AssociatedMetadataType);
        }

        Type AssociatedMetadataType { get; }

        bool IsSelfAssociated { get; }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetPropertiesWithMetadata(base.GetProperties(attributes));
        }

        public override PropertyDescriptorCollection GetProperties()
        {
            return GetPropertiesWithMetadata(base.GetProperties());
        }

        PropertyDescriptorCollection GetPropertiesWithMetadata(PropertyDescriptorCollection originalCollection)
        {
            if (AssociatedMetadataType == null) return originalCollection;

            var customDescriptorsCreated = false;
            var tempPropertyDescriptors = new List<PropertyDescriptor>();
            foreach (PropertyDescriptor propDescriptor in originalCollection)
            {
                var newMetadata = TypeDescriptorCache.GetAssociatedMetadata(AssociatedMetadataType, propDescriptor.Name);
                var descriptor = propDescriptor;
                if (newMetadata.Length > 0)
                {
                    // Create a metadata descriptor that wraps the property descriptor
                    descriptor = new MetadataPropertyDescriptorWrapper(propDescriptor, newMetadata);
                    customDescriptorsCreated = true;
                }

                tempPropertyDescriptors.Add(descriptor);
            }

            if (customDescriptorsCreated)
                return new PropertyDescriptorCollection(tempPropertyDescriptors.ToArray(), true);

            return originalCollection;
        }

        public override AttributeCollection GetAttributes()
        {
            // Since normal TD behavior is to return cached attribute instances on subsequent
            // calls to GetAttributes, we must be sure below to use the TD APIs to get both
            // the base and associated attributes
            var attributes = base.GetAttributes();
            if (AssociatedMetadataType != null && !IsSelfAssociated)
            {
                // Note that the use of TypeDescriptor.GetAttributes here opens up the possibility of
                // infinite recursion, in the corner case of two Types referencing each other as
                // metadata types (or a longer cycle), though the second condition above saves an immediate such
                // case where a Type refers to itself.
                var newAttributes = TypeDescriptor.GetAttributes(AssociatedMetadataType).OfType<Attribute>().ToArray();
                attributes = AttributeCollection.FromExisting(attributes, newAttributes);
            }

            return attributes;
        }

        static class TypeDescriptorCache
        {
            static readonly Attribute[] emptyAttributes = new Attribute[0];

            // Stores the associated metadata type for a type
            static readonly ConcurrentDictionary<Type, Type> metadataTypeCache = new();

            // Stores the attributes for a member info
            static readonly ConcurrentDictionary<Tuple<Type, string>, Attribute[]> typeMemberCache = new();

            // Stores whether or not a type and associated metadata type has been checked for validity
            static readonly ConcurrentDictionary<Tuple<Type, Type>, bool> validatedMetadataTypeCache = new();

            public static void ValidateMetadataType(Type type, Type associatedType)
            {
                var typeTuple = new Tuple<Type, Type>(type, associatedType);
                if (!validatedMetadataTypeCache.ContainsKey(typeTuple))
                {
                    CheckAssociatedMetadataType(type, associatedType);
                    validatedMetadataTypeCache.TryAdd(typeTuple, true);
                }
            }

            public static Type GetAssociatedMetadataType(Type type)
            {
                Type associatedMetadataType = null;
                if (metadataTypeCache.TryGetValue(type, out associatedMetadataType)) return associatedMetadataType;

                // Try association attribute
                var attribute = (MetadataTypeAttribute) Attribute.GetCustomAttribute(type, typeof(MetadataTypeAttribute));
                if (attribute != null) associatedMetadataType = attribute.MetadataClassType;

                metadataTypeCache.TryAdd(type, associatedMetadataType);
                return associatedMetadataType;
            }

            static void CheckAssociatedMetadataType(Type mainType, Type associatedMetadataType)
            {
                // Only properties from main type
                var mainTypeMemberNames = new HashSet<string>(mainType.GetProperties().Select(p => p.Name));

                // Properties and fields from buddy type
                var buddyFields = associatedMetadataType.GetFields().Select(f => f.Name);
                var buddyProperties = associatedMetadataType.GetProperties().Select(p => p.Name);
                var buddyTypeMembers = new HashSet<string>(buddyFields.Concat(buddyProperties), StringComparer.Ordinal);

                // Buddy members should be a subset of the main type's members
                if (!buddyTypeMembers.IsSubsetOf(mainTypeMemberNames))
                {
                    // Reduce the buddy members to the set not contained in the main members
                    buddyTypeMembers.ExceptWith(mainTypeMemberNames);

                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,
                        "The associated metadata type for type '{0}' contains the following unknown properties or fields: {1}. Please make sure that the names of these members match the names of the properties on the main type.",
                        mainType.FullName, string.Join(", ", buddyTypeMembers.ToArray())));
                }
            }

            public static Attribute[] GetAssociatedMetadata(Type type, string memberName)
            {
                var memberTuple = new Tuple<Type, string>(type, memberName);
                Attribute[] attributes;
                if (typeMemberCache.TryGetValue(memberTuple, out attributes)) return attributes;

                // Allow fields and properties
                var allowedMemberTypes = MemberTypes.Property | MemberTypes.Field;
                // Only public static/instance members
                var searchFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
                // Try to find a matching member on type
                var matchingMember = type.GetMember(memberName, allowedMemberTypes, searchFlags).FirstOrDefault();
                if (matchingMember != null)
                    attributes = Attribute.GetCustomAttributes(matchingMember, true /* inherit */);
                else attributes = emptyAttributes;

                typeMemberCache.TryAdd(memberTuple, attributes);
                return attributes;
            }
        }
    }
}