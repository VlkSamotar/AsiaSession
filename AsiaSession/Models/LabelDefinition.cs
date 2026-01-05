using AsiaSession.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsiaSession.Models
{
	public class LabelDefinition
	{
		public string Id { get; }
		public DateTime Time { get; }
		public double Price { get; }
		public string Text { get; }
		public MyLineColor Color { get; }
		public LabelDefinition(string id, DateTime time, double price, string text, MyLineColor color)
		{
			Id = id;
			Time = time;
			Price = price;
			Text = text;
			Color = color;
		}
	}
}
