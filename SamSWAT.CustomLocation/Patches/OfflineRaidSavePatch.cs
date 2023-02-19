using System.Reflection;
using Aki.Reflection.Patching;
using Aki.SinglePlayer.Patches.Progression;
using EFT;
using HarmonyLib;

namespace SamSWAT.CustomLocation.Patches
{
    public class OfflineRaidSavePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(OfflineSaveProfilePatch), "PatchPrefix");
        }

        [PatchPrefix]
        private static bool PatchPrefix(object[] __args)
        {
            return (__args[1] as RaidSettings).Location != "Debug Location";
        }
    }
}