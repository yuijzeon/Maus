using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Maus.Infrastructure;

public static class SchemaExtensions
{
    public static void Add(this ConventionSetBuilder builder, Func<ProviderConventionSetBuilderDependencies, IConvention> func)
    {
        builder.Add(x =>
        {
            var dependencies = x.GetRequiredService<ProviderConventionSetBuilderDependencies>();
            return func(dependencies);
        });
    }
}