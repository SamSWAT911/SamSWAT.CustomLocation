using System.Linq;
using System.Reflection;
using Aki.Reflection.Patching;
using Comfort.Common;
using EFT;
using EFT.UI.Matchmaker;
using HarmonyLib;

namespace SamSWAT.CustomLocation.Patches
{
    public class LoadingScreenPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(MatchmakerTimeHasCome).GetMethods(AccessTools.all).Single(x => x.ReturnType == typeof(bool));
        }

        [PatchPostfix]
        private static async void PatchPostfix(bool __result, RaidSettings ___raidSettings_0)
        {
            if (___raidSettings_0.LocationId != "testmap")
            {
                return;
            }

            __result = false;
            var path = $"{Plugin.RelativePath}bundles/dev.bundle";
            await ((AssetsManagerClass) Singleton<IAssetsManager>.Instance).BundlesManager.LoadBundleAsync(path);
            __result = true;
        }
    }
}