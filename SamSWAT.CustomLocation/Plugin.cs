using System;
using System.IO;
using System.Reflection;
using BepInEx;
using SamSWAT.CustomLocation.Database;
using SamSWAT.CustomLocation.Patches;
using UnityEngine;

namespace SamSWAT.CustomLocation
{
    [BepInPlugin("com.samswat.customlocation", "SamSWAT.CustomLocation", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static string RelativePath;
        public static string Directory;
        
        private void Awake()
        {
            SetDirectories();
            new AddLocationToDatabasePatch().Enable();
            new ScenePresetLoaderPatch().Enable();
            new CacheLocationLootPatch().Enable();
            new LoadLocationLootPatch().Enable();
            new OfflineRaidSavePatch().Enable();
            new LoadingScreenPatch().Enable();
        }

        private static void SetDirectories()
        {
            Directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\";
            var pluginDirUri = new Uri(Directory);
            var eftUri = new Uri(Application.streamingAssetsPath + "\\Windows\\");
            RelativePath = Uri.UnescapeDataString(eftUri.MakeRelativeUri(pluginDirUri).OriginalString);
        }
    }
}