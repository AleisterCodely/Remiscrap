using BepInEx;
using HarmonyLib;
using LethalLib.Modules;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Remiscrap
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency(LethalLib.Plugin.ModGUID)] 
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin instance;
        private void Awake()
        {
            // Plugin startup logic
            // Get assets
            string RemicolaAsset = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "remi-cola");
            string AlienFumoAsset = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "alien-fumo");
            AssetBundle Remicolabundle = AssetBundle.LoadFromFile(RemicolaAsset);
            AssetBundle Alienbundle = AssetBundle.LoadFromFile(AlienFumoAsset);
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PluginInfo.PLUGIN_GUID);

            // Register assets
            Item Remicola = Remicolabundle.LoadAsset<Item>("Assets/Remicola/RemicolaCanItem.asset");
            Item AlienFumo = Alienbundle.LoadAsset<Item>("Assets/Alien Fumo/AlienMiladyFumo.asset");
            NetworkPrefabs.RegisterNetworkPrefab(Remicola.spawnPrefab);
            NetworkPrefabs.RegisterNetworkPrefab(AlienFumo.spawnPrefab);
            Utilities.FixMixerGroups(Remicola.spawnPrefab);
            Utilities.FixMixerGroups(AlienFumo.spawnPrefab);
            Items.RegisterScrap(Remicola, 50, Levels.LevelTypes.All);
            Items.RegisterScrap(AlienFumo, 15, Levels.LevelTypes.All);

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PluginInfo.PLUGIN_GUID);
            Logger.LogInfo("Remiloot is on!");
        }
    }
}
