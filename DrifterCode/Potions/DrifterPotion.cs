using BaseLib.Abstracts;
using BaseLib.Utils;
using Drifter.DrifterCode.Character;

namespace Drifter.DrifterCode.Potions;

[Pool(typeof(DrifterPotionPool))]
public abstract class DrifterPotion : CustomPotionModel;