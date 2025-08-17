namespace Vapolia.FluentLayouts;

public class UIViewAndLayoutAttribute
{
    public UIViewAndLayoutAttribute(UIView view, NSLayoutAttribute attribute)
    {
        Attribute = attribute;
        View = view;
    }

    public UIView View { get; }
    public NSLayoutAttribute Attribute { get; }

    public FluentLayout EqualTo(nfloat constant = default) =>
        new (View, Attribute, NSLayoutRelation.Equal, constant);

    public FluentLayout GreaterThanOrEqualTo(nfloat constant = default) =>
        new (View, Attribute, NSLayoutRelation.GreaterThanOrEqual, constant);

    public FluentLayout LessThanOrEqualTo(nfloat constant = default) =>
        new (View, Attribute, NSLayoutRelation.LessThanOrEqual, constant);
}