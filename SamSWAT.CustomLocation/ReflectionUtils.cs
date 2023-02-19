using System;
using System.Linq;
using HarmonyLib;
using Newtonsoft.Json;
using static Aki.Reflection.Utils.PatchConstants;

namespace SamSWAT.CustomLocation
{
    internal static class ReflectionUtils
    {
        internal static readonly Type SessionType;
        internal static readonly Type LocationType;
        internal static readonly JsonSerializerSettings SerializerSettings;
        
        static ReflectionUtils()
        {
            SessionType = EftTypes.Single(x => typeof(ISession).IsAssignableFrom(x) && x.IsNotPublic);
            LocationType = typeof(LocationSettingsClass).GetNestedTypes()[1];
            
            var t = EftTypes.Single(x => x.GetField("SerializerSettings", AccessTools.all) != null);
            SerializerSettings = Traverse.Create(t).Field<JsonSerializerSettings>("SerializerSettings").Value;
        }
    }
}