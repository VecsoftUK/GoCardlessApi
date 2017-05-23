using System;
using System.Globalization;

namespace Vecsoft.GoCardlessApi
	{
	internal class ParseUtilities
		{
		internal static DateTime? ParseNullableDate(Object value)
			{
			if (value == null)
				return null;

			return DateTime.ParseExact((String)value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
			}

		internal static DateTime ParseTimestamp(Object value)
			{
			return DateTime.ParseExact((String)value, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.None);
			}
		}
	}
