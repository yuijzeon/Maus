using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maus.Infrastructure;

public static class Extensions
{
    public static PropertyBuilder<TColumn> PropertyAsVarchar15<TEntity, TColumn>(this EntityTypeBuilder<TEntity> entity, Expression<Func<TEntity, TColumn>> expression) where TEntity : class
    {
        return entity.Property(expression).HasConversion<string>().IsUnicode(false).HasMaxLength(15);
    }

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