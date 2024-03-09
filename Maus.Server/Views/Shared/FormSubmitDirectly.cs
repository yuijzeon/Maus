namespace Maus.Server.Views.Shared;

public class FormSubmitDirectly
{
    public required string ActionUrl { get; set; }
    public IEnumerable<KeyValuePair<string, string>> NameValues { get; set; } = [];
}