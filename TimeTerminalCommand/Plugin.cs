using BepInEx;
using HarmonyLib;
using System.Reflection;
using TerminalApi;
using static TerminalApi.TerminalApi;

namespace TimeTerminalCommand
{
	[BepInPlugin("atomic.timecommand", "Time Command", "1.0.1")]
	[BepInDependency("atomic.terminalapi", MinimumDependencyVersion: "1.2.0")]
	public class Plugin : BaseUnityPlugin
	{
		private void Awake()
		{
			Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
			Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

			// Adds time command, 'check' is the verb. Verbs are optional
            AddCommand("time", "You're not on a moon. There is no time here.\n", "check", true);

        }

		public static void UpdateKeywords()
		{
			UpdateKeywordCompatibleNoun("check", "time", CreateTerminalNode($"The time is currently {OnSetClock.CurrentTime}.\n", true));
		}
	}
}