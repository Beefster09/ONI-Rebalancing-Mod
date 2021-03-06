using HarmonyLib;
using UnityEngine;

 class WoodBurnerPatches
{
    [HarmonyPatch(typeof(WoodGasGeneratorConfig), nameof(WoodGasGeneratorConfig.CreateBuildingDef))]
    class WoodGasGeneratorConfig_CreateBuildingDef_Patch
    {
        public static void Postfix(BuildingDef __result)
        {
            __result.SelfHeatKilowattsWhenActive = 4.0f;
            __result.ExhaustKilowattsWhenActive = 1.0f;
        }
    }

    [HarmonyPatch(typeof (WoodGasGeneratorConfig), nameof(WoodGasGeneratorConfig.DoPostConfigureComplete))]
    class WoodGasGeneratorConfig_DoPostConfigureComplete_Patch
    {
        public static void Postfix(GameObject go)
        {
            var energyGenerator = go.GetComponent<EnergyGenerator>();
            // reduce wood consumption rate and CO2 production rate
            energyGenerator.formula = EnergyGenerator.CreateSimpleFormula(WoodLogConfig.TAG, 0.750f, 720f, SimHashes.CarbonDioxide, 0.1f, false, new CellOffset(0, 1), Temp.C(95f));
        }
    }
}
