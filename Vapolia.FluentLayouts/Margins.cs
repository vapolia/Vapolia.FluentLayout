namespace Vapolia.FluentLayouts;

public class Margins
{
    public float Top { get; set; }
    public float Bottom { get; set; }
    public float Left { get; set; }
    public float Right { get; set; }
    public float VSpacing { get; set; }
    public float HSpacing { get; set; }

    public Margins()
    {
    }

    public Margins(float all)
    {
        Top = all;
        Bottom = all;
        Right = all;
        Left = all;
        VSpacing = all;
        HSpacing = all;
    }

    public Margins(float allHorizontal, float allVertical)
    {
        Top = allVertical;
        Bottom = allVertical;
        Right = allHorizontal;
        Left = allHorizontal;
        VSpacing = allVertical;
        HSpacing = allHorizontal;
    }

    public Margins(float left, float top, float right, float bottom, float hspacing = 0, float vspacing = 0)
    {
        Top = top;
        Bottom = bottom;
        Right = right;
        Left = left;
        VSpacing = vspacing;
        HSpacing = hspacing;
    }
}