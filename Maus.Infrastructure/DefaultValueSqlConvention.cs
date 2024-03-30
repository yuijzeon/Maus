using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace Maus.Infrastructure;

public class DefaultValueSqlConvention(ProviderConventionSetBuilderDependencies dependencies) : PropertyAttributeConventionBase<DefaultValueSqlAttribute>(dependencies)
{
    protected override void ProcessPropertyAdded(IConventionPropertyBuilder propertyBuilder, DefaultValueSqlAttribute attribute, MemberInfo clrMember, IConventionContext context)
    {
        propertyBuilder.HasDefaultValueSql(attribute.Sql);
    }
}