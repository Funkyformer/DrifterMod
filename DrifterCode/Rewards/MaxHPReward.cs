using BaseLib.Abstracts;
using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace Drifter.DrifterCode.Rewards;

public class MaxHPReward(Player player, int hpAmount) : CustomReward(player)
{
    [CustomEnum] public static RewardType MaxHP;

    public override void Populate()
    {
    }
    public override bool IsPopulated => true;

    private int HPAmount { get; } = hpAmount;

    protected override async Task<bool> OnSelect()
    {
        await CreatureCmd.GainMaxHp(Player.Creature, HPAmount);
        Log.Info($"Player {Player.NetId} obtained {HPAmount} Max HP from reward");
        return true;
    }

    public override void MarkContentAsSeen()
    {
    }

    public override SerializableReward ToSerializable()
    {
        return new SerializableReward()
        {
            RewardType = MaxHP,
            GoldAmount = HPAmount
        };
    }

    protected override RewardType RewardType => MaxHP;
    public override CreateRewardFromSave<CustomReward> DeserializeMethod => CreateFromSerializable;
    public override LocString Description {
        get
        {
            var loc = GetLoc();
            loc.Add("Amount", HPAmount);
            return loc;
        }
    }

    protected override string IconPath => "res://Drifter/images/charui/map_marker_drifter.png";

    private static MaxHPReward CreateFromSerializable(SerializableReward save, Player player)
    {
        return new MaxHPReward(player, save.GoldAmount);
    }
}