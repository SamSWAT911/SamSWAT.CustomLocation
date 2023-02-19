using System.Linq;
using System.Reflection;
using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using HarmonyLib;

namespace SamSWAT.CustomLocation.Patches
{
    public class CacheLocationLootPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return PatchConstants.LocalGameType.BaseType.GetMethods(AccessTools.all).Single(IsTargetMethod);
        }

        private bool IsTargetMethod(MethodInfo arg)
        {
            var parameters = arg.GetParameters();
            return parameters.Length == 3
                   && parameters[0].Name == "backendUrl"
                   && parameters[1].Name == "locationId"
                   && parameters[2].Name == "variantId";
        }

        [PatchPrefix]
        private static bool PatchPrefix(string locationId)
        {
            return locationId != "debuglocation";
        }
    }
}