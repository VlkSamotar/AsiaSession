using AsiaSession.Enums;
using cAlgo.API;

namespace AsiaSession.Helpers
{
	public static class TimeFrameMapper
	{
		public static TimeFrame FromPossibleTimeFrames(PossibleTimeFrames possibleTimeFrames)
		{
			return possibleTimeFrames switch
			{
				PossibleTimeFrames.Minute1 => TimeFrame.Minute,
				PossibleTimeFrames.Minute5 => TimeFrame.Minute5,
				PossibleTimeFrames.Minute15 => TimeFrame.Minute15,
				PossibleTimeFrames.Minute30 => TimeFrame.Minute30,
				PossibleTimeFrames.Hour1 => TimeFrame.Hour,
				PossibleTimeFrames.Hour4 => TimeFrame.Hour4,
				_ => TimeFrame.Hour,
			};
		}
	}
}
