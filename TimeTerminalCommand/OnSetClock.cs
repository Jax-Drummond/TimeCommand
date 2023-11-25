using HarmonyLib;

namespace TimeTerminalCommand
{
	[HarmonyPatch(typeof(HUDManager), "SetClock")]
	public static class OnSetClock
	{
		private static string _currentTime;
		public static string CurrentTime { 
			get
			{
				return _currentTime;
			} 
			set
			{
				_currentTime = value;
				Plugin.UpdateKeywords();
			}
		}
		public static void Postfix(ref HUDManager __instance)
		{
			CurrentTime = __instance.clockNumber.text.Replace('\n', ' ');
		}
	}
}
