using BepInEx;
using HarmonyLib;

[BepInPlugin("com.aus.auspeacefulsubnautica", "AusPeacefulSubnautica", "1.0.0")]
public class AusPeacefulSubnauticaPlugin : BaseUnityPlugin
{
    private void Awake()
    {
        var harmony = new Harmony("com.aus.auspeacefulsubnautica");
        harmony.PatchAll();
        Logger.LogInfo("AusPeacefulSubnautica loaded!");
    }
}

[HarmonyPatch(typeof(Creature), "Start")]
public static class Creature_Start_Patch
{
    static void Prefix(Creature __instance)
    {
        // Make creature peaceful toward player
        __instance.friendlyToPlayer = true;

        // Reduce aggression if the field exists
        var aggressionField = __instance.GetType().GetField("aggression");
        if (aggressionField != null)
        {
            var aggressionValue = aggressionField.GetValue(__instance);
            if (aggressionValue is float)
                aggressionField.SetValue(__instance, 0f);
        }
    }
}
