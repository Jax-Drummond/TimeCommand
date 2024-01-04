using BepInEx;
using HarmonyLib;
using System.Data.SqlClient;
using System.Reflection;
using TerminalApi;
using TerminalApi.Classes;
using static TerminalApi.TerminalApi;

namespace TimeTerminalCommand
{
	[BepInPlugin("atomic.timecommand", "Time Command", "1.1.0")]
	[BepInDependency("atomic.terminalapi", MinimumDependencyVersion: "1.5.0")]
	public class Plugin : BaseUnityPlugin
	{
		private void Awake()
		{
			Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
			Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

			// Adds time command, 'check' is the verb. Verbs are optional
            AddCommand("time", new CommandInfo 
			{
				Category = "other",
				Description = "Displays the current time.",
				DisplayTextSupplier = OnTimeCommand
			}, 
			"check");

        }

        private string OnTimeCommand()
		{
			return !StartOfRound.Instance.currentLevel.planetHasTime || !StartOfRound.Instance.shipDoorsEnabled ? "You're not on a moon. There is no time here.\n" : $"The time is currently {HUDManager.Instance.clockNumber.text.Replace('\n', ' ')}.\n";
        }
	}
}