using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Aki.Reflection.Patching;
using Comfort.Common;
using EFT;
using EFT.UI;
using EFT.UI.Matchmaker;
using HarmonyLib;

namespace SamSWAT.CustomLocation.Patches
{
    public class LoadingScreenPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            var flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
            return typeof(MatchmakerTimeHasCome).GetMethods(flags).Single(x => x.ReturnType == typeof(bool));
        }

        [PatchPostfix]
        private static async void PatchPostfix(
            bool __result, 
            CustomTextMeshProUGUI ____locationName,
            PlayerModelView ____playerModelView)
        {
            if (____locationName.text != "DebugLocation Name") return;

            var session = Singleton<ClientApplication<ISession>>.Instance.GetClientBackEndSession().Profile;
            ____playerModelView.Show(session);
            __result = false;
            var path = $"{Plugin.RelativePath}bundles/dev_location.bundle";
            await ((AssetsManagerClass) Singleton<IAssetsManager>.Instance).BundlesManager.LoadBundleAsync(path);

            __result = true;
        }
    }
}