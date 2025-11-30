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
	public static class LineDrawer
	{
		public static void DrawLine(Chart chart, LineDefinition line, bool showHistory, DateTime dayUtc)
		{
			string tag = GetTag(line.Id, dayUtc, showHistory);
			chart.DrawTrendLine(tag,
				line.StartTime, line.StartPrice,
				line.EndTime, line.EndPrice,
				LineVisualMapper.ToColor(line.Color),
				LineVisualMapper.ToThickness(line.Thickness),
				LineVisualMapper.ToLineStyle(line.Style));
		}

		private static string GetTag(string id, DateTime dayUtc, bool showHistory)
		{
			return showHistory ? $"Asia_{id}_{dayUtc:yyyyMMdd}" : $"Asia_{id}";
		}
	}
}
