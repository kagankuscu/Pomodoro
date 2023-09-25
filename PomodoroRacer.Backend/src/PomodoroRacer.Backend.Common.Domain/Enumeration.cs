using System.Collections.Concurrent;
using System.Reflection;
using PomodoroRacer.Backend.Common.Domain.Attributes;

namespace PomodoroRacer.Backend.Common.Domain;

/// <summary>
/// For more info see https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
/// </summary>
public abstract class Enumeration : IComparable
{
    private static readonly ConcurrentDictionary<Type, IEnumerable<object>> EnumCache
        = new ConcurrentDictionary<Type, IEnumerable<object>>();

    public int Value { get; }

    public string Name { get; }

    protected Enumeration(int value, string name)
    {
        this.Value = value;
        this.Name = name;
    }

    public override string ToString() => this.Name;

    public static string ToStringLocalized<T>(string language, int value, bool shouldTakeAbbreviation = false) where T: Enumeration
    {
        var enumType = typeof(Enumeration);
        var attributeType = typeof(DisplayNameAttribute);

        var enumImplementations = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName!.Contains("Claudit"))
                .ToList().SelectMany(s => s.GetExportedTypes())
                .Where(t => t.IsClass
                        && !t.IsAbstract
                        && t.BaseType != null
                        && t.BaseType!.Equals(enumType)
                        && t.Name.Equals(typeof(T).Name))
                .FirstOrDefault();

        var translatedResult = "";

        if (enumImplementations != null)
        {
            var displayName = enumImplementations.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                                                 .Where(p => p.FieldType == enumImplementations &&
                                                                            p.Name == FromValue<T>(value).Name &&
                                                                            p.CustomAttributes.Any(a => a.AttributeType == attributeType))
                                                 .Select(p => p.GetCustomAttributes()
                                                               .Where(x => (x as DisplayNameAttribute)!.GetLanguage().Equals(language))
                                                               .FirstOrDefault())
                                                 .FirstOrDefault();

            if (displayName != null)
            {
                translatedResult = shouldTakeAbbreviation ?
                    (displayName! as DisplayNameAttribute)!.GetAbbreviation() :
                    (displayName! as DisplayNameAttribute)!.GetDisplayName();
            }

        }

        return translatedResult ?? "";
    }

    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        var type = typeof(T);

        var values = EnumCache.GetOrAdd(type, _ => type
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(x => !x.IsLiteral && x.IsInitOnly)
            .Select(f => f.GetValue(null)!));

        return values.Cast<T>();
    }

    public static T FromValue<T>(int value) where T : Enumeration
        => Parse<T, int>(value, "value", item => item.Value == value);

    public static T FromName<T>(string name) where T : Enumeration
        => Parse<T, string>(name, "name", item => item.Name.Trim().ToUpper() == name.Trim().ToUpper());

    public static string NameFromValue<T>(int value) where T : Enumeration
        => FromValue<T>(value).Name;

    public static bool HasValue<T>(int? value) where T : Enumeration
    {
        try
        {
            if (!value.HasValue)
                return false;

            FromValue<T>(value!.Value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool HasValue<T>(int value) where T : Enumeration
    {
        try
        {
            FromValue<T>(value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static T Parse<T, TValue>(TValue value, string description, Func<T, bool> predicate) where T : Enumeration
    {
        var matchingItem = GetAll<T>().FirstOrDefault(predicate);

        if (matchingItem == null)
        {
            throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");
        }

        return matchingItem;
    }

    public override bool Equals(object? obj)
    {
        if (!(obj is Enumeration otherValue))
        {
            return false;
        }

        var typeMatches = this.GetType() == obj.GetType();
        var valueMatches = this.Value.Equals(otherValue.Value);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode() => (this.GetType().ToString() + this.Value).GetHashCode();

    public int CompareTo(object? other) => this.Value.CompareTo(((Enumeration)other!).Value);
}