namespace Maus.Infrastructure;

public class ConvertStringAttribute(bool isUnicode, int maxLength) : Attribute
{
    public bool IsUnicode { get; set; } = isUnicode;
    public int MaxLength { get; set; } = maxLength;
}