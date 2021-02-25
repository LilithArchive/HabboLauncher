using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HabboLauncher
{
	public static class TextParsing
	{
		public static string ParseTemplateString(string template)
		{
			if (!template.StartsWith("${"))
			{
				return template;
			}
			int num = template.IndexOf('}');
			return template.Substring(2, num - 2);
		}

		public static Dictionary<string, string> InitDictionaryFromInput(string input)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			UpdateDictionaryFromInput(input, dictionary);
			return dictionary;
		}

		public static void UpdateDictionaryFromInput(string input, Dictionary<string, string> texts)
		{
			StringReader stringReader = new StringReader(input);
			StringBuilder stringBuilder = new StringBuilder();
			while (true)
			{
				string text = stringReader.ReadLine();
				if (text == null)
				{
					break;
				}
				stringBuilder.Clear();
				List<string> list = new List<string>();
				StringUtil.Split(text, '=', list);
				string key = list[0];
				if (list.Count == 1)
				{
					texts[key] = string.Empty;
				}
				else
				{
					if (list.Count <= 1)
					{
						continue;
					}
					if (list.Count == 2)
					{
						texts[key] = list[1];
						continue;
					}
					for (int i = 1; i < list.Count; i++)
					{
						stringBuilder.Append(list[i]);
						if (i < list.Count - 1)
						{
							stringBuilder.Append('=');
						}
					}
					texts[key] = stringBuilder.ToString();
				}
			}
		}
	}
}
