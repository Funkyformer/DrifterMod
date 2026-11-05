using Drifter.DrifterCode.Utils;
using MegaCrit.Sts2.Core.DevConsole;
using MegaCrit.Sts2.Core.DevConsole.ConsoleCommands;
using MegaCrit.Sts2.Core.Entities.Players;

namespace Drifter.DrifterCode.Commands;

public class ChargeConsoleCmd : AbstractConsoleCmd
{
    public override string CmdName => "charge";

    public override string Args => "<amount:int>";

    public override string Description => "Adds charge to player";

    public override bool IsNetworked => true;

    public override CmdResult Process(Player? issuingPlayer, string[] args)
    {
        if (args.Length == 0)
            return new CmdResult(false, "The first argument must be an int.");
        if (!int.TryParse(args[0], out var result))
            return new CmdResult(false, "The first argument must be an int.");
        if (issuingPlayer == null)
            return new CmdResult(false, "This command only works during a run.");
        if (issuingPlayer.PlayerCombatState == null)
            return new CmdResult(false, "This command only works in combat.");
        if (result < 0)
            return new CmdResult(false, "The charge amount cannot be negative.");
        return new CmdResult(ChargeUtils.Gain(issuingPlayer, result), true, $"Added '{result}' charge.");
    }
}