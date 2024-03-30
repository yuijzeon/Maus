using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Maus.Infrastructure;

public static class Extensions
{
    public static void UseConvertStringAttribute(this ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.Model
            .GetEntityTypes().Select(x => x.ClrType)
            .SelectMany(type => type.GetProperties().SelectMany(propertyInfo =>
            {
                var convertStringAttribute = propertyInfo.GetCustomAttribute<ConvertStringAttribute>();
                return convertStringAttribute is null
                    ? Enumerable.Empty<(Type, PropertyInfo, ConvertStringAttribute)>()
                    : [(type, propertyInfo, convertStringAttribute)];
            }));

        foreach (var (type, property, convertStringAttribute) in entityTypes)
        {
            modelBuilder.Entity(type).Property(property.Name)
                .HasConversion<string>()
                .IsUnicode(convertStringAttribute.IsUnicode)
                .HasMaxLength(convertStringAttribute.MaxLength);
        }
    }
}