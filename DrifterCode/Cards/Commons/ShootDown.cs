using BaseLib.Abstracts;
using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Commons;

public class ShootDown : DrifterCard
{
    public ShootDown() : base(0,
        CardType.Attack, CardRarity.Uncommon,
        TargetType.Self)
    {
        CustomResources<ChargeResource>.SetCanonicalCost(this, 5);
    }

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [DrifterEnums.Gun, DrifterEnums.SemiAuto];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var cmd = new AttackCommand(0).FromCard(this, play);
        await Hook.BeforeAttack(CombatState, cmd);
        var improved = Hook.ModifyDamage(RunState, CombatState, null, Owner.Creature,
            DynamicVars.Damage.BaseValue, ValueProp.Move, this, play, ModifyDamageHookType.All, CardPreviewMode.Normal, out var modifiers);
        await CreatureCmd.GainBlock(Owner.Creature, improved, ValueProp.Unpowered, play);
        await Hook.AfterAttack(CombatState, choiceContext, cmd);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(StaticHoverTip.Block)];
}