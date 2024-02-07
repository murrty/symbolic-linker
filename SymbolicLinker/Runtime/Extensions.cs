#nullable enable
namespace SymbolicLinker;
using System.Diagnostics.CodeAnalysis;
internal static class Extensions {
    public static bool IsNullEmptyWhitespace([NotNullWhen(false)] this string? value) {
        if (value == null) {
            return true;
        }
        return IsEmptyWhitespace(value);
    }
    public static bool IsEmptyWhitespace(this string value) {
        if (value.Length == 0) {
            return true;
        }

        for (int pos = 0; pos < value.Length; pos++) {
            if (!char.IsWhiteSpace(value[pos])) {
                return false;
            }
        }

        return true;
    }
}
