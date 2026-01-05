using AsiaSession.Helpers;
using AsiaSession.Models;
using cAlgo.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsiaSession.Services
{
	public static class LabelDrawer
	{		
		public static void DrawLabel(Chart chart, LabelDefinition label, bool showHistory, DateTime dayUtc)
		{
			string tag = GetTag(label.Id, dayUtc, showHistory);
			chart.DrawText(tag,
				label.Text,
				label.Time,
				label.Price,
				LineVisualMapper.ToColor(label.Color));
		}
		private static string GetTag(string id, DateTime dayUtc, bool showHistory)
		{
			return showHistory ? $"Asia_Label_{id}_{dayUtc:yyyyMMdd}" : $"Asia_Label_{id}";
		}
	}
}
