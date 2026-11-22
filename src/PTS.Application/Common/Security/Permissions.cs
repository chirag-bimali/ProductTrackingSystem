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

    public static class Units
    {
        public const string Create = "units.create";
        public const string Update = "units.update";
        public const string Delete = "units.delete";
    }

    public static class MaterialCategories
    {
        public const string Create = "material_categories.create";
        public const string Update = "material_categories.update";
        public const string Delete = "material_categories.delete";
    }

    public static class Materials
    {
        public const string Create = "materials.create";
        public const string Update = "materials.update";
        public const string Delete = "materials.delete";
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
