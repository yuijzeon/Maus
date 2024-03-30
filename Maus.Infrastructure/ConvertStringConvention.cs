using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace Maus.Infrastructure;

public class ConvertStringConvention(ProviderConventionSetBuilderDependencies dependencies) : PropertyAttributeConventionBase<ConvertStringAttribute>(dependencies)
{
    protected override void ProcessPropertyAdded(IConventionPropertyBuilder propertyBuilder, ConvertStringAttribute attribute, MemberInfo clrMember, IConventionContext context)
    {
        propertyBuilder.HasConversion(typeof(string));
        propertyBuilder.IsUnicode(attribute.IsUnicode);
        propertyBuilder.HasMaxLength(attribute.MaxLength);
    }
}