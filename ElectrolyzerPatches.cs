using HarmonyLib;
using UnityEngine;


public class ElectrolyzerPatches
{
    [HarmonyPatch(typeof(ElectrolyzerConfig), nameof(ElectrolyzerConfig.CreateBuildingDef))]
    public class ElectrolyzerConfig_CreateBuildingDef_Patch
    {
        public static void Postfix(BuildingDef __result)
        {
            __result.EnergyConsumptionWhenActive = 150f;
        }
    }

    [HarmonyPatch(typeof(ElectrolyzerConfig), nameof(ElectrolyzerConfig.ConfigureBuildingTemplate))]
    public class ElectrolyzerConfig_ConfigureBuildingTemplate_Patch
    {
        public static void Postfix(GameObject go) 
        {
            var gasOutputPos = new CellOffset(0, 1);
            var ec = go.GetComponent<ElementConverter>();
            ec.consumedElements = new ElementConverter.ConsumedElement[1]
            {
                new ElementConverter.ConsumedElement(new Tag("Water"), 0.750f)
            };
            ec.outputElements = new ElementConverter.OutputElement[2]
            {
                new ElementConverter.OutputElement(0.666f, SimHashes.Oxygen, Temp.C(70f), outputElementOffsetx: ((float) gasOutputPos.x), outputElementOffsety: ((float) gasOutputPos.y)),
                new ElementConverter.OutputElement(0.084f, SimHashes.Hydrogen, Temp.C(70f), outputElementOffsetx: ((float) gasOutputPos.x), outputElementOffsety: ((float) gasOutputPos.y))
            };
        }
    }
}
