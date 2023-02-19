using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Aki.Reflection.Patching;
using HarmonyLib;
using Newtonsoft.Json;

namespace SamSWAT.CustomLocation.Database
{
    public class AddLocationToDatabasePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return ReflectionUtils.SessionType.GetMethod("GetLevelSettings");
        }
        
        [PatchPostfix]
        private static async void PatchPostfix(Task<LocationSettingsClass> __result)
        {
            var settings = await __result;
            var json = File.ReadAllText($"{Plugin.Directory}/database/location.json");
            
            var newLocation = Activator.CreateInstance(ReflectionUtils.LocationType);
            JsonConvert.PopulateObject(json, newLocation, ReflectionUtils.SerializerSettings);
			
            var locations = Traverse.Create(settings).Field<IDictionary>("locations").Value;
            locations.Add("debuglocation", newLocation);
        }
    }
}