namespace Drifter.DrifterCode.Character;

public static class DrifterAnimations
{
    public const float RegularDelay = 0.2f;
    public const float LongDelay = 0.5f;
    public const float NoDelay = 0.01f;
    
    
    
    public const string Grenade = "specialgrenade";
    public const float GrenadeDelay = 0.1f;
    public static readonly AnimHolder GrenadeHolder = new (Grenade, GrenadeDelay);
    
    public const string HeavyAttack =  "heavyattack";
    public static readonly AnimHolder HeavyAttackHolder = new(HeavyAttack, LongDelay);
    
    
    
    public const string Pistol = "aimpistol";
    public static readonly AnimHolder PistolHolder = new(Pistol, LongDelay);
    public static readonly AnimHolder PistolPreHolder = new(Pistol, NoDelay);
    
    public const string Shotgun = "aimshotgun";
    public static readonly AnimHolder ShotgunHolder = new(Shotgun, LongDelay);
    public static readonly AnimHolder ShotgunPreHolder = new(Pistol, NoDelay);
    
    public const string Railgun = "aimrailgun";
    public static readonly AnimHolder RailgunHolder = new(Railgun, LongDelay);
    public static readonly AnimHolder RailgunPreHolder = new(Pistol, NoDelay);
    
    public const string DiamondShot = "aimdiamondshot";
    public static readonly AnimHolder DiamondShotHolder = new(DiamondShot, LongDelay);
    public static readonly AnimHolder DiamondShotPreHolder = new(Pistol, NoDelay);
    
    public const string Zeliska = "aimzeliska";
    public static readonly AnimHolder ZeliskaHolder = new(Zeliska, LongDelay);
    public static readonly AnimHolder ZeliskaPreHolder = new(Pistol, NoDelay);
    
    public const string ImpactRailgun = "aimimpactrailgun";
    public static readonly AnimHolder ImpactRailgunHolder = new(ImpactRailgun, LongDelay);
    public static readonly AnimHolder ImpactRailgunPreHolder = new(Pistol, NoDelay);
    
}

public class AnimHolder(string animName, float delay)
{
    public string AnimName { get; } = animName;
    public float Delay { get; } = delay;
}