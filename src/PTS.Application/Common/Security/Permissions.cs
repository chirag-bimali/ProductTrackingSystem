using System.Reflection;

namespace PTS.Application.Common.Security;

public static class Permissions
{
    public static IReadOnlyList<string> All { get; } =
        typeof(Permissions)
            .GetNestedTypes()
            .SelectMany(t => t.GetFields(BindingFlags.Public | BindingFlags.Static))
            .Where(f => f.IsLiteral && f.FieldType == typeof(string))
            .Select(f => (string)f.GetRawConstantValue()!)
            .ToList();

    public static class No
    {
        public const string Permission = "no.permission";
    }

    public static class Users
    {
        public const string Create = "users.create";
        public const string Read = "users.read";
    }

    public static class Roles
    {
        public const string Read = "roles.read";
    }

    public static class SuperAdmin
    {
        public const string Access = "super_admin.access";
    }

    public static class Farmer
    {
        public const string Access = "farmer.access";
    }

    public static class LocalAdmin
    {
        public const string Access = "local_admin.access";
        public const string Create = "local_admin.create";
        public const string ReadAll = "local_admin.read_all";
    }
}
