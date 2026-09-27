using BaseLib.Abstracts;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Singletons;

public class PlinkingSingleton() : CustomSingletonModel(HookType.Combat)
{
    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay card)
    {
        PlayerCombatState state = card.Card.Owner.PlayerCombatState;
        if (!card.ValidGun(true) && !card.ValidBlade() && state != null)
        {
            return;
        }
        
        if (card.ValidGun(true))
        {
            DrifterSpireFields.Plinking.Set(state, true);
        }

        if (card.ValidBlade() && DrifterSpireFields.Plinking.Get(state))
        {
            await CustomResources<ChargeResource>.Get(card.Card.Owner.PlayerCombatState).Spend<ChargeResource>(card.Card.CombatState, card.Card, -1, false);
            DrifterSpireFields.Plinking.Set(state, false);
        }
    }
}