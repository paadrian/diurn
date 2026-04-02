namespace Kernel;

public static class GuidExtensions
{
    public static Guid ToGuid(this string value) => Guid.Parse(value);
}