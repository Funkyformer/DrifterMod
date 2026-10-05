using BaseLib.Abstracts;
using BaseLib.Utils;
using Drifter.DrifterCode.Cards.Ancients;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Basics;
public class PistolShot : DrifterCard, ITranscendenceCard
{
    public PistolShot() : base(0,
        CardType.Attack, CardRarity.Basic,
        TargetType.AnyEnemy)
    {
        CustomResources<ChargeResource>.SetCanonicalCost(this, 3);
    }
    public CardModel GetTranscendenceTransformedCard() => ModelDb.Card<ZeliskaSlug>();

    public override IEnumerable<CardKeyword> CanonicalKeywords => [DrifterEnums.Gun];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(3,ValueProp.Move), new PowerVar<WeakPower>(1)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        if (!play.IsWeave(WeaveEnum.Gun))
            return;
        await PowerCmd.Apply<WeakPower>(choiceContext, play.Target, DynamicVars.Weak.BaseValue, Owner.Creature, this);
        // await Cmd.Wait(0.25f);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(1);
        DynamicVars.Weak.UpgradeValueBy(1);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<WeakPower>(), HoverTipFactory.Static(DrifterEnums.Weaving)];
}