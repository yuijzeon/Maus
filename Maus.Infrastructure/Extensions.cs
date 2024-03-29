using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maus.Infrastructure;

public static class Extensions
{
    public static PropertyBuilder<TColumn> PropertyAsString15<TEntity, TColumn>(this EntityTypeBuilder<TEntity> entity, Expression<Func<TEntity, TColumn>> expression) where TEntity : class
    {
        return entity.Property(expression).HasMaxLength(15).HasConversion<string>();
    }
}