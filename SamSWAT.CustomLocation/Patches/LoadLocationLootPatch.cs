using System.Collections;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using HarmonyLib;

namespace SamSWAT.CustomLocation.Patches
{
    public class LoadLocationLootPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return ReflectionUtils.SessionType.GetMethod("LoadLocationLoot");
        }

        [PatchPrefix]
        private static bool PatchPrefix(ISession __instance, ref object __result, string locationId)
        {
            if (locationId == "debuglocation")
            {
                var locations = Traverse.Create(__instance.LocationSettings).Field("locations").GetValue<IDictionary>();
                __result = Task.FromResult(locations[locationId]);
                return false;
            }

            return true;
        }
    }
}