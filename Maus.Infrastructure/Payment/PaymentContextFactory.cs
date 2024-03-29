using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Maus.Infrastructure.Payment;

public class PaymentContextFactory : IDesignTimeDbContextFactory<PaymentContext>
{
    public PaymentContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PaymentContext>();
        optionsBuilder.UseSqlServer("Server=.;Database=MausPayment;TrustServerCertificate=True;Integrated Security=True");
        return new PaymentContext(optionsBuilder.Options);
    }
}