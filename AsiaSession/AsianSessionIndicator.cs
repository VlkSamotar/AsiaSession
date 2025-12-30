/*
  Asia Session Indicator for cTrader
  ----------------------------------
  Author: Jakub Březa
  Date: December 2025
  License: MIT (see LICENSE file for details)

  Description:
  This indicator highlights the Asian trading session by drawing a configurable
  background region between user‑defined start and end times. It supports custom
  colors, opacity, line styles, and optional historical rendering for accurate
  backtesting. The session is aligned using a global UTC offset to ensure
  consistent timing across different brokers and server timezones.

  Features:
  - Configurable session start and end times
  - Customizable color, opacity, and border style
  - Optional historical mode for backtesting
  - Global UTC offset for consistent session alignment
  - Clean, modular architecture for easy maintenance
*/

using AsiaSession.Enums;
using AsiaSession.Models;
using AsiaSession.Services;
using cAlgo.API;
using System;

namespace AsiaSessionIndicator
{
	[Indicator(IsOverlay = true, TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
	public class AsianSessionIndicator : Indicator
	{
		[Parameter("Session Start Hour", DefaultValue = 1)]
		public int SessionStartHour { get; set; }

		[Parameter("Session End Hour", DefaultValue = 7)]
		public int SessionEndHour { get; set; }

		[Parameter("Extend Until Hour", DefaultValue = 17)]
		public int ExtendUntilHour { get; set; }

		[Parameter("Show History", DefaultValue = false)]
		public bool ShowHistory { get; set; }

		[Parameter("UTC Offset (hours)", DefaultValue = 1)]
		public int TimezoneOffsetHours { get; set; }


		[Parameter("Top Line Color", DefaultValue = MyLineColor.Orange)]
		public MyLineColor TopLineColor { get; set; }
		[Parameter("Top Line Style", DefaultValue = MyLineStyle.Lines)]
		public MyLineStyle TopLineStyle { get; set; }
		[Parameter("Top Line Thickness", DefaultValue = MyLineThickness.One)]
		public MyLineThickness TopLineThickness { get; set; }

		[Parameter("Bottom Line Color", DefaultValue = MyLineColor.Orange)]
		public MyLineColor BottomLineColor { get; set; }
		[Parameter("Bottom Line Style", DefaultValue = MyLineStyle.Lines)]
		public MyLineStyle BottomLineStyle { get; set; }
		[Parameter("Bottom Line Thickness", DefaultValue = MyLineThickness.One)]
		public MyLineThickness BottomLineThickness { get; set; }

		[Parameter("Mid Line Color", DefaultValue = MyLineColor.Orange)]
		public MyLineColor MidLineColor { get; set; }
		[Parameter("Mid Line Style", DefaultValue = MyLineStyle.Dotted)]
		public MyLineStyle MidLineStyle { get; set; }
		[Parameter("Mid Line Thickness", DefaultValue = MyLineThickness.One)]
		public MyLineThickness MidLineThickness { get; set; }

		[Parameter("Left Line Color", DefaultValue = MyLineColor.White)]
		public MyLineColor LeftLineColor { get; set; }
		[Parameter("Left Line Style", DefaultValue = MyLineStyle.Dotted)]
		public MyLineStyle LeftLineStyle { get; set; }
		[Parameter("Left Line Thickness", DefaultValue = MyLineThickness.One)]
		public MyLineThickness LeftLineThickness { get; set; }

		[Parameter("Right Line Color", DefaultValue = MyLineColor.White)]
		public MyLineColor RightLineColor { get; set; }
		[Parameter("Right Line Style", DefaultValue = MyLineStyle.Dotted)]
		public MyLineStyle RightLineStyle { get; set; }
		[Parameter("Right Line Thickness", DefaultValue = MyLineThickness.One)]
		public MyLineThickness RightLineThickness { get; set; }

		protected override void Initialize()
		{
			var today = Server.Time.Date;
			if (today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday)
				return;

			DrawSession(today);
		}

		public override void Calculate(int index)
		{
			var currentTime = Bars.OpenTimes[index];
			if (!ShowHistory && currentTime.Date != Server.Time.Date) return;
			if (currentTime.DayOfWeek == DayOfWeek.Saturday || currentTime.DayOfWeek == DayOfWeek.Sunday) return;

			DrawSession(currentTime.Date);
		}

		private void DrawSession(DateTime date)
		{
			var sessionStart = date.AddHours(SessionStartHour - TimezoneOffsetHours);
			var sessionEnd = date.AddHours(SessionEndHour - TimezoneOffsetHours);
			var extendEnd = date.AddHours(ExtendUntilHour - TimezoneOffsetHours);

			double high = double.MinValue;
			double low = double.MaxValue;

			for (int i = Bars.Count - 1; i >= 0; i--)
			{
				var t = Bars.OpenTimes[i];
				if (t < sessionStart) break;
				if (t <= sessionEnd)
				{
					high = Math.Max(high, Bars.HighPrices[i]);
					low = Math.Min(low, Bars.LowPrices[i]);
				}
			}

			if (high == double.MinValue || low == double.MaxValue)
				return;

			double mid = (high + low) / 2;

			var highLine = new LineDefinition("HighLine", sessionStart, high, extendEnd, high,
											  TopLineColor, TopLineStyle, TopLineThickness);
			LineDrawer.DrawLine(Chart, highLine, ShowHistory, date);

			var lowLine = new LineDefinition("LowLine", sessionStart, low, extendEnd, low,
											 BottomLineColor, BottomLineStyle, BottomLineThickness);
			LineDrawer.DrawLine(Chart, lowLine, ShowHistory, date);

			var midLine = new LineDefinition("MidLine", sessionStart, mid, extendEnd, mid,
											 MidLineColor, MidLineStyle, MidLineThickness);
			LineDrawer.DrawLine(Chart, midLine, ShowHistory, date);

			var leftLine = new LineDefinition("LeftLine", sessionStart, high, sessionStart, low,
											  LeftLineColor, LeftLineStyle, LeftLineThickness);
			LineDrawer.DrawLine(Chart, leftLine, ShowHistory, date);

			var rightLine = new LineDefinition("RightLine", sessionEnd, high, sessionEnd, low,
											   RightLineColor, RightLineStyle, RightLineThickness);
			LineDrawer.DrawLine(Chart, rightLine, ShowHistory, date);
		}
	}
}
