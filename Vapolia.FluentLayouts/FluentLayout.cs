namespace Vapolia.FluentLayouts;

public class FluentLayout
{
	public FluentLayout(
		UIView view,
		NSLayoutAttribute attribute,
		NSLayoutRelation relation,
		NSObject secondItem,
		NSLayoutAttribute secondAttribute)
	{
		Constraint = new(CreateConstraint);
		View = view;
		Attribute = attribute;
		Relation = relation;
		SecondItem = secondItem;
		SecondAttribute = secondAttribute;
		Priority = (float) UILayoutPriority.Required;
	}

	public FluentLayout(UIView view,
		NSLayoutAttribute attribute,
		NSLayoutRelation relation,
		nfloat constant = default)
	{
		Constraint = new(CreateConstraint);
		View = view;
		Attribute = attribute;
		Relation = relation;
		Constant = constant;
		Priority = (float) UILayoutPriority.Required;
	}

	public UIView View { get; }

	public nfloat Multiplier { get; internal set; } = 1f;

	private nfloat constant;
	public nfloat Constant 
	{ 
		get => constant;
		set
		{
			constant = value;

			if (Constraint.IsValueCreated)
				Constraint.Value.Constant = constant;
		}
	}

	private float priority;
	public float Priority 
	{ 
		get => priority;
		set
		{
			priority = value;

			if (Constraint.IsValueCreated)
				Constraint.Value.Priority = priority;
		}
	}

	private bool active = true;
	public bool Active
	{
		get => active;
		set
		{
			active = value;

			if (Constraint.IsValueCreated)
				Constraint.Value.Active = active;
		}
	}

	private string? identifier;
	public string? Identifier
	{
		get => identifier;
		set
		{
			identifier = value;

			if (Constraint.IsValueCreated)
				Constraint.Value.SetIdentifier(identifier);
		}
	}

	public NSLayoutAttribute Attribute { get; }
	public NSLayoutRelation Relation { get; }
	public NSObject? SecondItem { get; private set; }
	public NSLayoutAttribute SecondAttribute { get; private set; }

	internal Lazy<NSLayoutConstraint> Constraint { get; }

	private NSLayoutConstraint CreateConstraint()
	{
		var constraint = NSLayoutConstraint.Create(
			View,
			Attribute,
			Relation,
			SecondItem,
			SecondAttribute,
			Multiplier,
			Constant);
		constraint.Priority = Priority;

		if (!string.IsNullOrWhiteSpace(Identifier))
			constraint.SetIdentifier(Identifier);

		return constraint;
	}


	public FluentLayout LeftOf(NSObject view2) => SetSecondItem(view2, NSLayoutAttribute.Left);

	public FluentLayout RightOf(NSObject view2) => SetSecondItem(view2, NSLayoutAttribute.Right);

	public FluentLayout TopOf(NSObject view2) => SetSecondItem(view2, NSLayoutAttribute.Top);

	public FluentLayout BottomOf(NSObject view2) => SetSecondItem(view2, NSLayoutAttribute.Bottom);

	public FluentLayout BaselineOf(NSObject view2) => SetSecondItem(view2, NSLayoutAttribute.Baseline);

	public FluentLayout TrailingOf(NSObject view2) => SetSecondItem(view2, NSLayoutAttribute.Trailing);

	public FluentLayout LeadingOf(NSObject view2) => SetSecondItem(view2, NSLayoutAttribute.Leading);

	public FluentLayout CenterXOf(NSObject view2) => SetSecondItem(view2, NSLayoutAttribute.CenterX);

	public FluentLayout CenterYOf(NSObject view2) => SetSecondItem(view2, NSLayoutAttribute.CenterY);

	public FluentLayout HeightOf(NSObject view2) => SetSecondItem(view2, NSLayoutAttribute.Height);

	public FluentLayout WidthOf(NSObject view2) => SetSecondItem(view2, NSLayoutAttribute.Width);

	public FluentLayout TrailingMarginOf(NSObject view2) => SetSecondItem(view2, NSLayoutAttribute.TrailingMargin);

	public FluentLayout LeadingMarginOf(NSObject view2) => SetSecondItem(view2, NSLayoutAttribute.LeadingMargin);

	public FluentLayout TopMarginOf(NSObject view2) => SetSecondItem(view2, NSLayoutAttribute.TopMargin);

	public FluentLayout BottomMarginOf(NSObject view2) => SetSecondItem(view2, NSLayoutAttribute.BottomMargin);

	private FluentLayout SetSecondItem(NSObject view2, NSLayoutAttribute attribute2)
	{
		ThrowIfSecondItemAlreadySet();
		SecondAttribute = attribute2;
		SecondItem = view2;
		return this;
	}

	private void ThrowIfSecondItemAlreadySet()
	{
		if (SecondItem != null)
			throw new("You cannot set the second item in a layout relation more than once");
	}

	[Obsolete("This method will be removed in future versions, please let us know if you still need it!")]
	public IEnumerable<NSLayoutConstraint> ToLayoutConstraints()
	{
		yield return Constraint.Value;
	}
}

public static class FluentLayoutExtensions2
{
	public static FluentLayout Plus(this FluentLayout s, nfloat constant)
	{
		s.Constant += constant;
		return s;
	}

	public static FluentLayout Minus(this FluentLayout s, nfloat constant)
	{
		s.Constant -= constant;
		return s;
	}

	public static FluentLayout WithMultiplier(this FluentLayout s, nfloat multiplier)
	{
		s.Multiplier = multiplier;
		return s;
	}

	public static FluentLayout SetPriority(this FluentLayout s, float priority)
	{
		s.Priority = priority;
		return s;
	}

	public static FluentLayout SetPriority(this FluentLayout s, UILayoutPriority priority)
	{
		s.Priority = (float) priority;
		return s;
	}

	public static FluentLayout SetActive(this FluentLayout s, bool active)
	{
		s.Active = active;
		return s;
	}

	public static FluentLayout WithIdentifier(this FluentLayout s, string identifier)
	{
		s.Identifier = identifier;
		return s;
	}
}