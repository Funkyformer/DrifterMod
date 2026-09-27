using BaseLib.Abstracts;
using BaseLib.Utils;
using Drifter.DrifterCode.Cards.Ancients;
using Drifter.DrifterCode.Character;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
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
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(3,ValueProp.Move)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        // await Cmd.Wait(0.25f);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(1);
    }
}