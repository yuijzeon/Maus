namespace Maus.Web.Views.Payment;

public class FormSubmitDirectly
{
    public required string ActionUrl { get; set; }
    public IEnumerable<KeyValuePair<string, string>> NameValues { get; set; } = [];
}