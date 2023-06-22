using System;
using System.IO;
using System.Reflection;
using BepInEx;
using SamSWAT.CustomLocation.Patches;
using UnityEngine;

namespace SamSWAT.CustomLocation
{
    [BepInPlugin("com.samswat.customlocation", "SamSWAT.CustomLocation", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static string RelativePath;
        
        private void Awake()
        {
            SetDirectories();
            new ScenePresetLoaderPatch().Enable();
            new CacheLocationLootPatch().Enable();
            new LoadingScreenPatch().Enable();
        }

        private static void SetDirectories()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\";
            var pluginDirUri = new Uri(directory);
            var eftUri = new Uri(Application.streamingAssetsPath + "\\Windows\\");
            RelativePath = Uri.UnescapeDataString(eftUri.MakeRelativeUri(pluginDirUri).OriginalString);
        }
    }
}