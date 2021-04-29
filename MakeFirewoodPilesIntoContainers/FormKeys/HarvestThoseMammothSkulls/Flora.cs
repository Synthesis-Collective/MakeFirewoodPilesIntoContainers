using Mutagen.Bethesda.Skyrim;

namespace Mutagen.Bethesda.FormKeys.SkyrimSE
{
    public static partial class HarvestThoseMammothSkulls
    {
        public class Flora
        {
            private static FormLink<IFloraGetter> Construct(uint id) => new(ModKey.MakeFormKey(id));
            public static FormLink<IFloraGetter> cm_BoneMammothSkull1 = Construct(0x000800);
            public static FormLink<IFloraGetter> cm_BoneMammothSkull2 = Construct(0x000801);
            public static FormLink<IFloraGetter> cm_BoneMammothSkull3 = Construct(0x000802);
            public static FormLink<IFloraGetter> cm_BoneMammothSkull4 = Construct(0x000803);
        }
    }
}