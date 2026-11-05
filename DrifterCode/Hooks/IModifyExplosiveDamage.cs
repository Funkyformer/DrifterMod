using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Hooks;

public interface IModifyExplosiveDamage
{
    public decimal ModifyExplosiveDamage(Decimal damage, Creature applier);
}