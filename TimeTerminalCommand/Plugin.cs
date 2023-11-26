using BepInEx;
using HarmonyLib;
using System.Reflection;
using TerminalApi;
using static TerminalApi.TerminalApi;

namespace TimeTerminalCommand
{
	[BepInPlugin("atomic.timecommand", "Time Command", "1.0.0")]
	[BepInDependency("atomic.terminalapi")]
	public class Plugin : BaseUnityPlugin
	{
		private void Awake()
		{
			Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
			Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
			AddKeywords();
		}

		public void AddKeywords()
		{
			TerminalNode triggerNode = CreateTerminalNode($"You're not on a moon. There is no time here.\n", true);
			TerminalKeyword verbKeyword = CreateTerminalKeyword("check", true);
			TerminalKeyword nounKeyword = CreateTerminalKeyword("time");

			verbKeyword = verbKeyword.AddCompatibleNoun(nounKeyword, triggerNode);
			nounKeyword.defaultVerb = verbKeyword;

			AddTerminalKeyword(verbKeyword);
			AddTerminalKeyword(nounKeyword);
		}

		public static void UpdateKeywords()
		{
			UpdateKeywordCompatibleNoun("check", "time", CreateTerminalNode($"The time is currently {OnSetClock.CurrentTime}.\n", true));
		}
	}
}