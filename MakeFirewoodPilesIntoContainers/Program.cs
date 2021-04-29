using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mutagen.Bethesda;
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
            {FirewoodPileSmall01, wiz_FireWoodpileSmall},
            {FirewoodPileLarge01, wiz_FireWoodpileLarge},
            {FirewoodPileHuge1, wiz_FireWoodPileHuge},
            {FirewoodPileHuge1_LightSN, wiz_FireWoodpileLarge_LightSN},
            {FirewoodPileSmall01_SparseSN, wiz_FireWoodpileSmall_SparseSN},
            {FirewoodPileLarge01_SparseSN, wiz_FireWoodpileLarge_SparseSN},
            {FirewoodPileMedium01_LightSN, wiz_FireWoodpileMedium_LightSN},
            {FirewoodPileLarge01_LightSN, wiz_FireWoodpileLarge_LightSN},
            {FirewoodPileSmall01_LightSN, wiz_FireWoodpileSmall_LightSN},
            {FirewoodPileMedium01, wiz_FireWoodpileMedium},
        };
        
        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance.AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch).Run(args,
                new RunPreferences
                {
                    ActionsForEmptyArgs = new RunDefaultPatcher
                    {
                        IdentifyingModKey = "WiZkiD Lootable FireWood Piles_patch.esp",
                        TargetRelease = GameRelease.SkyrimSE,
                    }
                });

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