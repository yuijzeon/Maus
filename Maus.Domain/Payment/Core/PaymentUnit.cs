namespace Maus.Domain.Payment.Core;

public class PaymentUnit
{
    public required PaymentType PaymentType { get; set; }
    public required MethodCode MethodCode { get; set; }
    public required SubMethodCode SubMethodCode { get; set; }
    public required ProviderCode ProviderCode { get; set; }
}