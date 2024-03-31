using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Maus.Infrastructure;

public class DateTimeKindConvention() : ValueConverter<DateTime, DateTime>(
    toDb => toDb.ToUniversalTime(),
    fromDb => DateTime.SpecifyKind(fromDb, DateTimeKind.Utc).ToLocalTime()
);