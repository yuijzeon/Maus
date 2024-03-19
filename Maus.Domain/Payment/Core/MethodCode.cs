namespace Maus.Domain.Payment.Core;

public enum MethodCode
{
    Unspecified = 0,
    BankTransfer,
    Barcode,
    CreditCard,
    InternetBanking,
    Kiosk,
    PayLater,
    QrCode,
}