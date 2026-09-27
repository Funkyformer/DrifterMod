
using Drifter.DrifterCode.DynamicVars;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using SmartFormat.Core.Extensions;

namespace Drifter.DrifterCode.Formatters;

public class ChargeIconFormatter : IFormatter
{

    public string Name { get => "chargeIcon"; set => throw new NotImplementedException(); }
    public bool CanAutoDetect { get; set; }
    public bool TryEvaluateFormat(IFormattingInfo formattingInfo)
    {
        string iconText;
        int amount;
        switch (formattingInfo.CurrentValue)
        {
            case ChargeVar chargeVar:
                amount = Convert.ToInt32(chargeVar.PreviewValue);
                break;
            case CalculatedVar calculatedVar:
                amount = Convert.ToInt32(calculatedVar.PreviewValue);
                break;
            case DynamicVar dynVar:
                amount = Convert.ToInt32(dynVar.PreviewValue);
                break;
            case int integer:
                amount = integer;
                break;
            case Decimal deci:
                amount = (int) deci;
                break;
            case string str:
                if (!int.TryParse(formattingInfo.FormatterOptions, out amount))
                    return false;
                amount = int.Parse(formattingInfo.FormatterOptions);
                break;
            default:
                throw new LocException($"Unknown value='{formattingInfo.CurrentValue}' type={formattingInfo.CurrentValue?.GetType()}");
        }

        string resource = "[img]res://Drifter/images/charui/ammo24.png[/img]";
        if (amount > 0 && amount < 5) 
            iconText = string.Concat(Enumerable.Repeat<string>(resource, amount));
        else if (formattingInfo.CurrentValue is DynamicVar currentValue)
            iconText = currentValue.ToHighlightedString(false) +resource;
        else if (amount == 0)
            iconText = $"{resource}";
        else 
            iconText = $"{amount}{resource}";
        formattingInfo.Write(iconText);
        return true;
    }
}