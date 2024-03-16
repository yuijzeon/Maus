namespace Maus.Domain.Payment.Core;

public enum MethodCode
{
    Unspecified = 0,
    BankTransfer,
    Barcode,
    CreditCard,
    EWallet,
    InternetBanking,
    Kiosk,
    PayLater,
}