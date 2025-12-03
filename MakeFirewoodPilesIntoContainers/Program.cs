using System.Collections.Generic;
using System.Threading.Tasks;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using static Mutagen.Bethesda.FormKeys.SkyrimSE.Skyrim.Static;
using static Mutagen.Bethesda.FormKeys.SkyrimSE.WiZkiD_Lootable_FireWood_Piles.Container;

namespace MakeFirewoodPilesIntoContainers
{
    class Program
    {
        private static readonly Dictionary<IFormLinkGetter<ISkyrimMajorRecordGetter>, IFormLink<IContainerGetter>> _replacements = new()
        {
            {FirewoodPileSmall01, wiz_FireWoodpileSmall01},
            {FirewoodPileLarge01, wiz_FireWoodpileLarge01},
            {FirewoodPileHuge1, wiz_FireWoodPileHuge1},
            {FirewoodPileHuge1_LightSN, wiz_FireWoodPileHuge1_LightSN},
            {FirewoodPileSmall01_SparseSN, wiz_FireWoodpileSmall01_SparseSN},
            {FirewoodPileLarge01_SparseSN, wiz_FireWoodpileLarge01_SparseSN},
            {FirewoodPileMedium01_LightSN, wiz_FireWoodpileMedium01_LightSN},
            {FirewoodPileLarge01_LightSN, wiz_FireWoodpileLarge01_LightSN},
            {FirewoodPileSmall01_LightSN, wiz_FireWoodpileSmall01_LightSN},
            {FirewoodPileMedium01, wiz_FireWoodpileMedium01},
        };
        
        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance
                .SetTypicalOpen(GameRelease.SkyrimSE, "WiZkiD Lootable FireWood Piles_patch.esp")
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .Run(args);
        }

        private static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            foreach (var placed in state.LoadOrder.PriorityOrder.PlacedObject().WinningContextOverrides(state.LinkCache))
            {
                if (_replacements.TryGetValue(placed.Record.Base, out var found))
                {
                    placed.GetOrAddAsOverride(state.PatchMod)
                          .Base.SetTo(found);
                }
            }
        }
    }
}
