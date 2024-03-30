namespace Maus.Infrastructure;

[AttributeUsage(AttributeTargets.Property)]
public class DefaultValueSqlAttribute(string sql) : Attribute
{
    public string Sql { get; set; } = sql;
}