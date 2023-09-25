namespace PomodoroRacer.Backend.Common.Domain.Attributes;

[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true)]
public class DisplayNameAttribute : Attribute
{
    private readonly string _language;
    private readonly string _displayName;
    private readonly string? _abbreviation;

    public DisplayNameAttribute(string language, string displayName)
    {
        this._language = language.ToUpper().Trim();
        this._displayName = displayName;
    }

    public DisplayNameAttribute(string language, string displayName, string abbreviation)
        : this(language, displayName)
    {
        this._abbreviation = abbreviation;
    }

    public string GetLanguage() => this._language;

    public string GetDisplayName() => this._displayName;

    public string? GetAbbreviation() => this._abbreviation;
}