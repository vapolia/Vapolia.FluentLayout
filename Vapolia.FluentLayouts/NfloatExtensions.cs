using System.Runtime.InteropServices;

namespace Vapolia.FluentLayouts;

internal static class NfloatExtensions
{
	public static NFloat GetValueOrDefault(this NFloat? value) => value.GetValueOrDefault(0);

	public static NFloat GetValueOrDefault(this NFloat? value, NFloat defaultValue) => value ?? defaultValue;
}