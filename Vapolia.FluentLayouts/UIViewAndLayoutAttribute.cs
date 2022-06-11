using System.Runtime.InteropServices;
using UIKit;

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

    public FluentLayout EqualTo(NFloat constant = default) =>
        new FluentLayout(View, Attribute, NSLayoutRelation.Equal, constant);

    public FluentLayout GreaterThanOrEqualTo(NFloat constant = default) =>
        new FluentLayout(View, Attribute, NSLayoutRelation.GreaterThanOrEqual, constant);

    public FluentLayout LessThanOrEqualTo(NFloat constant = default) =>
        new FluentLayout(View, Attribute, NSLayoutRelation.LessThanOrEqual, constant);
}