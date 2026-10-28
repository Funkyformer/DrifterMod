using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using Drifter.DrifterCode.Cards.Statuses;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Commands;
using Drifter.DrifterCode.DynamicVars;
using Drifter.DrifterCode.Hooks;
using Drifter.DrifterCode.Relics;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Cards;

public abstract class ModularCard : DrifterCard
{
    private readonly int _modular;
    private readonly int _barrage;
    private readonly bool _doesDamage;


    protected ModularCard(int cost, CardType type, CardRarity rarity, TargetType target, int modular, int barrage = 0, bool doesDamage = true) : base(cost, type, rarity, target)
    {
        _modular = modular;
        _barrage = barrage;
        _doesDamage = doesDamage;
        CustomResources<ChargeResource>.SetXCost(this);
    }

    protected virtual IEnumerable<DynamicVar> BonusVars => [];
    
    protected sealed override IEnumerable<DynamicVar> CanonicalVars => [
        new ModularVar(_modular), 
        ..GetBarrageVar(), 
        new ("HitsBase", 0),
        new ("HitsExtra",1),
        new CustomCalculatedVar("Hits").WithMultiplier((Func<CardModel, Creature, Decimal>)
            ((card, target) => CustomResources<ChargeResource>.Get(card.Owner.PlayerCombatState).Amount / card.DynamicVars[ModularVar.Key].IntValue)),
        ..BonusVars];
    

    private IEnumerable<DynamicVar> GetBarrageVar()
    {
        if (_barrage > 0)
        {
            yield return new BarrageVar(_barrage);
        }
    }

    protected abstract Task ModularEffect(PlayerChoiceContext choiceContext, CardPlay play, int repeats, AttackContext? attackContext = null, IEnumerable<DamageResult>? damageResults = null);

    protected virtual Task BarrageEffect(PlayerChoiceContext choiceContext, CardPlay play, int repeats, AttackContext? attackContext = null, IEnumerable<DamageResult>? damageResults = null)
    {
        return Task.CompletedTask;
    }


    protected sealed override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (DynamicVars.ContainsKey(JudgmentVar.Key))
            await JudgmentCmd.Judgment(CombatState, choiceContext, this, DynamicVars[JudgmentVar.Key].BaseValue, Owner);
        var attackContext = _doesDamage ? await AttackCommand.CreateContextAsync(CombatState, choiceContext, cardPlay) : null;
        IEnumerable<DamageResult> damageResults = [];
        int numRepeats = (int)(CustomResources<ChargeResource>.Cost(this).ResolveXValue() /
                               DynamicVars[ModularVar.Key].BaseValue);
        if (Owner.Relics.Any(r => r is PinksHelmet) &&
            CustomResources<ChargeResource>.Get(Owner.PlayerCombatState).Amount > 0) numRepeats++;

        for (int i = 1; i <= numRepeats; i++)
        {
            await ModularEffect(choiceContext, cardPlay, i - 1, attackContext, damageResults);
            if (DynamicVars.ContainsKey(BarrageVar.Key) && i % DynamicVars[BarrageVar.Key].BaseValue == 0)
                await BarrageEffect(choiceContext, cardPlay, i / DynamicVars[BarrageVar.Key].IntValue, attackContext, damageResults);
            await DrifterHooks.AfterModularRepeat(CombatState, choiceContext, Owner, this, i);
        }
    }
}