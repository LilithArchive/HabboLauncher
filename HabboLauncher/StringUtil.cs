using System;
using System.Collections.Generic;
using System.Text;

namespace HabboLauncher
{
	public static class StringUtil
	{
		private static readonly StringBuilder stringBuilder = new StringBuilder();

		public static bool HasUpper(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return false;
			}
			for (int i = 0; i < str.Length; i++)
			{
				if (char.IsUpper(str[i]))
				{
					return true;
				}
			}
			return false;
		}

		public static bool StartsWithExcept(string str, string other, string except, bool ignoreCase = false)
		{
			if (str.Length < other.Length - except.Length)
			{
				return false;
			}
			for (int i = 0; i < other.Length - except.Length; i++)
			{
				int index = i + except.Length;
				char num = ignoreCase ? char.ToUpperInvariant(str[i]) : str[i];
				char c = ignoreCase ? char.ToUpperInvariant(other[index]) : other[index];
				if (num != c)
				{
					return false;
				}
			}
			return true;
		}

		public static int ToInt(string str, int startIndex, int endIndex)
		{
			int num = 0;
			int num2 = 1;
			for (int num3 = endIndex - 1; num3 >= startIndex; num3--)
			{
				if (num3 < endIndex - 2)
				{
					num2 *= 10;
				}
				else if (num3 < endIndex - 1)
				{
					num2 = 10;
				}
				num += (int)char.GetNumericValue(str[num3]) * num2;
			}
			return num;
		}

		public static void Split(string str, char delim, List<string> strings, int startIndex = 0, bool trim = false)
		{
			if (str == null)
			{
				return;
			}
			for (int i = startIndex; i <= str.Length; i++)
			{
				if (i == str.Length || str[i] == delim)
				{
					if (trim)
					{
						strings.Add(str.Substring(startIndex, i - startIndex).Trim());
					}
					else
					{
						strings.Add(str.Substring(startIndex, i - startIndex));
					}
					startIndex = i + 1;
				}
			}
		}

		public static int ParseInt(string value, int defaultIntValue = 0)
		{
			if (int.TryParse(value, out int result))
			{
				return result;
			}
			return defaultIntValue;
		}

		public static long ParseLong(string value, long defaultValue = 0L)
		{
			if (long.TryParse(value, out long result))
			{
				return result;
			}
			return defaultValue;
		}

		public static bool ParseBool(string value, bool defaultBoolValue = false)
		{
			if (bool.TryParse(value, out bool result))
			{
				return result;
			}
			return defaultBoolValue;
		}

		public static int GetDamerauLevenshteinDistance(string s, string t)
		{
			var anon = new
			{
				Height = s.Length + 1,
				Width = t.Length + 1
			};
			int[,] array = new int[anon.Height, anon.Width];
			for (int i = 0; i < anon.Height; i++)
			{
				array[i, 0] = i;
			}
			for (int j = 0; j < anon.Width; j++)
			{
				array[0, j] = j;
			}
			for (int k = 1; k < anon.Height; k++)
			{
				for (int l = 1; l < anon.Width; l++)
				{
					int num = (s[k - 1] != t[l - 1]) ? 1 : 0;
					int val = array[k, l - 1] + 1;
					int val2 = array[k - 1, l] + 1;
					int val3 = array[k - 1, l - 1] + num;
					int num2 = Math.Min(val, Math.Min(val2, val3));
					if (k > 1 && l > 1 && s[k - 1] == t[l - 2] && s[k - 2] == t[l - 1])
					{
						num2 = Math.Min(num2, array[k - 2, l - 2] + num);
					}
					array[k, l] = num2;
				}
			}
			return array[anon.Height - 1, anon.Width - 1];
		}
	}
}
