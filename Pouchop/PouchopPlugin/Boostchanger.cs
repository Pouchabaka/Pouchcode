using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using Pouchop.Patches;
using Reptile;
using System.IO;
using UnityEngine;


namespace Pouchop
{

    [BepInPlugin(MyGUID, PluginName, VersionString)]
    public class PouchopPlugin : BaseUnityPlugin
    {
        private const string MyGUID = "com.Pouchabaka.Pouchop";
        private const string PluginName = "Pouchop";
        private const string VersionString = "1.0.0";
        public static PouchopPlugin Instance;

        private Harmony harmony;
        public static Player player;




        private void Awake()
        {
            harmony = new Harmony(MyGUID);
            harmony.PatchAll(typeof(PouchopPlugin));
            harmony.PatchAll(typeof(BoostAbilityPatch));
            harmony.PatchAll(typeof(CharacterVisual));
            harmony.PatchAll();
            Instance = this;

            string dir = Path.Combine(Path.GetDirectoryName(Info.Location), "Cosmetics");


            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string hatbefore = Path.Combine(Path.GetDirectoryName(Info.Location), "Hatstoconvert");


            if (!Directory.Exists(hatbefore))
            {
                Directory.CreateDirectory(hatbefore);
            }
        }
    }
}


namespace Pouchop.Patches
{
    internal static class BoostAbilityPatch
    {
        private static float defaultBoostSpeed;

        [HarmonyPatch(typeof(BoostAbility), nameof(BoostAbility.Init))]
        [HarmonyPostfix]
        private static void Boostchange(BoostAbility __instance)
        {
            if (!__instance.p.isAI)
            {
                __instance.p.normalBoostSpeed = 120f;
            }
        }

    }
    public class createconfig
    {
        public static createconfig Instance = null;

        public ConfigEntry<bool> configfile;

        public createconfig(ConfigFile file)
        {
            var section = "General";
            configfile = file.Bind(
                section,
                nameof(configfile),
                true);

        }
    }
}

namespace thanksduchess
{

    [HarmonyPatch(typeof(StageManager))]
    internal static class StageManagerPatch

    {

        [HarmonyPrefix]
        [HarmonyPatch(nameof(StageManager.SetupWorldHandler))]
        static void SetupWorldHandler_Prefix(Stage newStage, Stage prevStage)
        {

            var tvbundlePath = Path.Combine(Path.GetDirectoryName(Pouchop.PouchopPlugin.Instance.Info.Location), "Cosmetics/bigtv");
            var testPath = Path.Combine(Path.GetDirectoryName(Pouchop.PouchopPlugin.Instance.Info.Location), "Cosmetics/cubee");

            var tvbundle = AssetBundle.LoadFromFile(tvbundlePath);
            var testload = AssetBundle.LoadFromFile(testPath);


            if (newStage == Stage.hideout)
            {
                var test1 = testload.LoadAsset<GameObject>("test");
                var test2 = tvbundle.LoadAsset<GameObject>("twitchtv");
                UnityEngine.Object.Instantiate(test1);
                UnityEngine.Object.Instantiate(test2);
            }

        }


    }
}

