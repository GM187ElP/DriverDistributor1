using Microsoft.Extensions.Localization;
using MudBlazor;

internal class CustomeMudLocalizer : MudLocalizer
{
    private Dictionary<string, string> _localization;

    public CustomeMudLocalizer()
    {
        _localization = new()
        {
            { "MudDataGrid_Save", "ذخیره" },
            { "MudDataGrid_Cancel", "انصراف" },
        };
    }

    public override LocalizedString this[string key]
    {
        get
        {
            var currentCulture = Thread.CurrentThread.CurrentUICulture.Parent.TwoLetterISOLanguageName;
            if (currentCulture.Equals("fa", StringComparison.InvariantCultureIgnoreCase)
                && _localization.TryGetValue(key, out var res))
            {
                return new(key, res);
            }
            else
            {
                return new(key, key, true);
            }
        }
    }
}
