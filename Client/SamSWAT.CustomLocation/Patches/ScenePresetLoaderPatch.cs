using System.Linq;
using System.Reflection;
using Aki.Reflection.Patching;
using Aki.Reflection.Utils;

namespace SamSWAT.CustomLocation.Patches
{
    public class ScenePresetLoaderPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            const string methodName = "LoadBundleAsync";
            return PatchConstants.EftTypes.Single(x => x.GetMethod(methodName) != null).GetMethod(methodName);
        }

        [PatchPrefix]
        private static void PatchPrefix(ref string bundleName)
        {
            if (bundleName == "maps/dev_preset.bundle")
            {
                bundleName = $"{Plugin.RelativePath}bundles/{bundleName}";
            }
        }
    }
}