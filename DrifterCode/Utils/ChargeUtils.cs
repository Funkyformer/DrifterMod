using BaseLib.Abstracts;
using Drifter.DrifterCode.Character;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Utils;

public static class ChargeUtils
{
    public static ChargeResource Get(Player player)
    {
        return CustomResources<ChargeResource>.Get(player.PlayerCombatState);
    }

    public static CustomResourceCost<ChargeResource>? Cost(CardModel card)
    {
        return CustomResources<ChargeResource>.Cost(card);
    }

    public static async Task Spend(Player player, decimal amt, AbstractModel? spender = null, bool optional = false)
    {
        await Get(player).Spend<ChargeResource>(player.Creature.CombatState, spender, (int)amt, optional);
    }
    public static async Task Spend(Player player, DynamicVar amt, AbstractModel? spender = null, bool optional = false)
    {
        await Get(player).Spend<ChargeResource>(player.Creature.CombatState, spender, amt.IntValue, optional);
    }
    
    public static async Task Gain(Player player, decimal amt, AbstractModel? spender = null, bool optional = false)
    {
        await Get(player).Spend<ChargeResource>(player.Creature.CombatState, spender, -(int)amt, optional);
    }
    public static async Task Gain(Player player, DynamicVar amt, AbstractModel? spender = null, bool optional = false)
    {
        await Get(player).Spend<ChargeResource>(player.Creature.CombatState, spender, -amt.IntValue, optional);
    }
}