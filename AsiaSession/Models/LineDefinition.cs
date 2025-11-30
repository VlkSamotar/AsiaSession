using AsiaSession.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsiaSession.Models
{
	public class LineDefinition
	{
		public string Id { get; }
		public DateTime StartTime { get; }
		public double StartPrice { get; }
		public DateTime EndTime { get; }
		public double EndPrice { get; }
		public MyLineColor Color { get; }
		public MyLineStyle Style { get; }
		public MyLineThickness Thickness { get; }

		public LineDefinition(string id, DateTime startTime, double startPrice,
							  DateTime endTime, double endPrice,
							  MyLineColor color, MyLineStyle style, MyLineThickness thickness)
		{
			Id = id;
			StartTime = startTime;
			StartPrice = startPrice;
			EndTime = endTime;
			EndPrice = endPrice;
			Color = color;
			Style = style;
			Thickness = thickness;
		}
	}
}
