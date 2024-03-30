using Maus.Domain.Payment.Core;
using Maus.Infrastructure.Payment;
using Maus.Infrastructure.Payment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Maus.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentPrepareEcPayController(PaymentContext context) : PreparePaymentController(context)
{
    [HttpPost]
    public async Task<IActionResult> PrepareEcPayDepositData(string operatorName)
    {
        SetParameters(PaymentType.Deposit, CurrencyCode.TWD, ProviderCode.EcPay, operatorName);

        await PrepareProvider();

        await PrepareProviderMethod(MethodCode.Unspecified, "ALL");
        await PrepareProviderMethod(MethodCode.CreditCard, "Credit");
        await PrepareProviderMethod(MethodCode.EWallet, "ApplePay");
        await PrepareProviderMethod(MethodCode.BankTransfer, "ATM");
        await PrepareProviderMethod(MethodCode.CvsKiosk, "CVS");
        await PrepareProviderMethod(MethodCode.Barcode, "BARCODE");
        await PrepareProviderMethod(MethodCode.InternetBanking, "WebATM");
        await PrepareProviderMethod(MethodCode.QrCode, "TWQR");
        await PrepareProviderMethod(MethodCode.PayLater, "BNPL");

        await PrepareProviderMethodBank(MethodCode.Unspecified, BankCode.Unspecified, null);
        await PrepareProviderMethodBank(MethodCode.CreditCard, BankCode.Unspecified, "0");
        await PrepareProviderMethodBank(MethodCode.CreditCard, BankCode.UnionPay, "1");
        await PrepareProviderMethodBank(MethodCode.CreditCard, BankCode.UnionPayWithout, "2");
        await PrepareProviderMethodBank(MethodCode.EWallet, BankCode.ApplePay, null);
        await PrepareProviderMethodBank(MethodCode.BankTransfer, BankCode.Unspecified, null);
        await PrepareProviderMethodBank(MethodCode.BankTransfer, BankCode.TSIB, "TAISHIN");
        await PrepareProviderMethodBank(MethodCode.BankTransfer, BankCode.ESUN, "ESUN");
        await PrepareProviderMethodBank(MethodCode.BankTransfer, BankCode.BKTW, "BOT");
        await PrepareProviderMethodBank(MethodCode.BankTransfer, BankCode.TPBK, "FUBON");
        await PrepareProviderMethodBank(MethodCode.BankTransfer, BankCode.CTCB, "CHINATRUST");
        await PrepareProviderMethodBank(MethodCode.BankTransfer, BankCode.FCBK, "FIRST");
        await PrepareProviderMethodBank(MethodCode.BankTransfer, BankCode.LBOT, "LAND");
        await PrepareProviderMethodBank(MethodCode.BankTransfer, BankCode.UWCB, "CATHAY");
        await PrepareProviderMethodBank(MethodCode.BankTransfer, BankCode.OURB, "TACHONG");
        await PrepareProviderMethodBank(MethodCode.BankTransfer, BankCode.BBBK, "PANHSIN");
        await PrepareProviderMethodBank(MethodCode.CvsKiosk, BankCode.Unspecified, "CVS");
        await PrepareProviderMethodBank(MethodCode.CvsKiosk, BankCode.OkMart, "OK");
        await PrepareProviderMethodBank(MethodCode.CvsKiosk, BankCode.FamilyMart, "FAMILY");
        await PrepareProviderMethodBank(MethodCode.CvsKiosk, BankCode.HiLife, "HILIFE");
        await PrepareProviderMethodBank(MethodCode.CvsKiosk, BankCode.IBon, "IBON");
        await PrepareProviderMethodBank(MethodCode.Barcode, BankCode.Unspecified, null);
        await PrepareProviderMethodBank(MethodCode.InternetBanking, BankCode.Unspecified, null);
        await PrepareProviderMethodBank(MethodCode.InternetBanking, BankCode.TSIB, "TAISHIN");
        await PrepareProviderMethodBank(MethodCode.InternetBanking, BankCode.ESUN, "ESUN");
        await PrepareProviderMethodBank(MethodCode.InternetBanking, BankCode.BKTW, "BOT");
        await PrepareProviderMethodBank(MethodCode.InternetBanking, BankCode.TPBK, "FUBON");
        await PrepareProviderMethodBank(MethodCode.InternetBanking, BankCode.CTCB, "CHINATRUST");
        await PrepareProviderMethodBank(MethodCode.InternetBanking, BankCode.FCBK, "FIRST");
        await PrepareProviderMethodBank(MethodCode.InternetBanking, BankCode.LBOT, "LAND");
        await PrepareProviderMethodBank(MethodCode.InternetBanking, BankCode.UWCB, "CATHAY");
        await PrepareProviderMethodBank(MethodCode.InternetBanking, BankCode.OURB, "TACHONG");
        await PrepareProviderMethodBank(MethodCode.InternetBanking, BankCode.SINO, "SINOPAC");
        await PrepareProviderMethodBank(MethodCode.InternetBanking, BankCode.ICBC, "MEGA");
        await PrepareProviderMethodBank(MethodCode.QrCode, BankCode.TWQR, null);
        await PrepareProviderMethodBank(MethodCode.PayLater, BankCode.Unspecified, null);

        return Ok();
    }
}

public class PreparePaymentController : ControllerBase
{
    private readonly PaymentContext _context;
    private CurrencyCode? _currencyCode;
    private PaymentType? _paymentType;
    private ProviderCode? _providerCode;
    private string _operatorName;

    public PreparePaymentController(PaymentContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    protected async Task PrepareProvider()
    {
        var exist = await _context.Providers
            .Where(x => x.ProviderCode == _providerCode)
            .AnyAsync();

        if (!exist)
        {
            await _context.Providers.AddAsync(new ProviderEntity
            {
                ProviderCode = _providerCode!.Value,
                Status = PaymentStatus.Closed,
                CreatedBy = _operatorName,
            });

            await _context.SaveChangesAsync();
        }
    }

    protected async Task PrepareProviderMethod(MethodCode methodCode, string? providerMethodCode)
    {
        var exist = await _context.ProviderMethods
            .Where(x => x.ProviderCode == _providerCode)
            .Where(x => x.MethodCode == methodCode)
            .AnyAsync();

        if (!exist)
        {
            await _context.ProviderMethods.AddAsync(new ProviderMethodEntity
            {
                ProviderMethodCode = providerMethodCode,
                Status = PaymentStatus.Normal,
                Type = _paymentType!.Value,
                CurrencyCode = _currencyCode!.Value,
                MethodCode = methodCode,
                ProviderCode = _providerCode!.Value,
                CreatedBy = _operatorName,
            });

            await _context.SaveChangesAsync();
        }
    }

    protected async Task PrepareProviderMethodBank(MethodCode methodCode, BankCode bankCode, string? providerBankCode)
    {
        var exist = await _context.ProviderMethodBanks
            .Where(x => x.ProviderCode == _providerCode)
            .Where(x => x.MethodCode == methodCode)
            .Where(x => x.BankCode == bankCode)
            .AnyAsync();

        if (!exist)
        {
            await _context.ProviderMethodBanks.AddAsync(new ProviderMethodBankEntity
            {
                BankCode = bankCode,
                ProviderBankCode = providerBankCode,
                Status = PaymentStatus.Normal,
                Type = _paymentType!.Value,
                CurrencyCode = _currencyCode!.Value,
                MethodCode = methodCode,
                ProviderCode = _providerCode!.Value,
                CreatedBy = _operatorName,
            });

            await _context.SaveChangesAsync();
        }
    }

    protected void SetParameters(PaymentType paymentType, CurrencyCode currencyCode, ProviderCode providerCode, string operatorName)
    {
        _currencyCode = currencyCode;
        _providerCode = providerCode;
        _paymentType = paymentType;
        _operatorName = operatorName;
    }
}