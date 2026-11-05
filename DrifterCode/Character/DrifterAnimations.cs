namespace Drifter.DrifterCode.Character;

public static class DrifterAnimations
{
    public const float RegularDelay = 0.2f;
    public const float LongDelay = 0.5f;
    
    
    
    public const string Grenade = "specialgrenade";
    public const float GrenadeDelay = 0.1f;
    public static readonly AnimHolder GrenadeHolder = new (Grenade, GrenadeDelay);
    
    public const string HeavyAttack =  "heavyattack";
    public static readonly AnimHolder HeavyAttackHolder = new(HeavyAttack, LongDelay);
    
    
    
    public const string Pistol = "aimpistol";
    public static readonly AnimHolder PistolHolder = new(Pistol, RegularDelay);
    
    public const string Shotgun = "aimshotgun";
    public static readonly AnimHolder ShotgunHolder = new(Shotgun, RegularDelay);
    
    public const string Railgun = "aimrailgun";
    public static readonly AnimHolder RailgunHolder = new(Railgun, LongDelay);
    
    public const string DiamondShot = "aimdiamondshot";
    public static readonly  AnimHolder DiamondShotHolder = new(DiamondShot, RegularDelay);
    
    public const string Zeliska = "aimzeliska";
    public static readonly AnimHolder ZeliskaHolder = new(Zeliska, RegularDelay);
    
    public const string ImpactRailgun = "aimimpactrailgun";
    public static readonly AnimHolder ImpactRailgunHolder = new(ImpactRailgun, LongDelay);
    
}

public class AnimHolder(string animName, float delay)
{
    public string AnimName { get; } = animName;
    public float Delay { get; } = delay;
}