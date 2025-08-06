namespace DriverDistributor.Services;

[AttributeUsage(AttributeTargets.Property)]
public class ForeignKeyLinkAttribute : Attribute
{
    public string NavigationProperty { get; }
    public string NavigationKeyProperty { get; }

    public ForeignKeyLinkAttribute(string navigationProperty, string navigationKeyProperty )
    {
        NavigationProperty = navigationProperty;
        NavigationKeyProperty = navigationKeyProperty;
    }
}

