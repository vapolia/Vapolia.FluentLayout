using System.Runtime.InteropServices;

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
		NFloat constant = default)
	{
		Constraint = new(CreateConstraint);
		View = view;
		Attribute = attribute;
		Relation = relation;
		Constant = constant;
		Priority = (float) UILayoutPriority.Required;
	}

	public UIView View { get; }

	public NFloat Multiplier { get; private set; } = 1f;

	private NFloat _constant;
	public NFloat Constant 
	{ 
		get => _constant;
		set
		{
			_constant = value;

			if (Constraint.IsValueCreated)
				Constraint.Value.Constant = _constant;
		}
	}

	private float _priority;
	public float Priority 
	{ 
		get { return _priority; }
		set
		{
			_priority = value;

			if (Constraint.IsValueCreated)
				Constraint.Value.Priority = _priority;
		}
	}

	private bool _active = true;
	public bool Active
	{
		get => _active;
		set
		{
			_active = value;

			if (Constraint.IsValueCreated)
				Constraint.Value.Active = _active;
		}
	}

	private string _identifier;
	public string Identifier
	{
		get => _identifier;
		set
		{
			_identifier = value;

			if (Constraint.IsValueCreated)
				Constraint.Value.SetIdentifier(_identifier);
		}
	}

	public NSLayoutAttribute Attribute { get; }
	public NSLayoutRelation Relation { get; }
	public NSObject SecondItem { get; private set; }
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

	public FluentLayout Plus(NFloat constant)
	{
		Constant += constant;
		return this;
	}

	public FluentLayout Minus(NFloat constant)
	{
		Constant -= constant;
		return this;
	}

	public FluentLayout WithMultiplier(NFloat multiplier)
	{
		Multiplier = multiplier;
		return this;
	}

	public FluentLayout SetPriority(float priority)
	{
		Priority = priority;
		return this;
	}

	public FluentLayout SetPriority(UILayoutPriority priority)
	{
		Priority = (float) priority;
		return this;
	}

	public FluentLayout SetActive(bool active)
	{
		Active = active;
		return this;
	}

	public FluentLayout WithIdentifier(string identifier)
	{
		Identifier = identifier;
		return this;
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