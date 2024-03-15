namespace Maus.Domain.Payment.Core;

public class PaymentException(string message) : Exception(message);