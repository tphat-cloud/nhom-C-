using System.Drawing.Drawing2D;

namespace QuanLyDesktop;

internal sealed class DashboardStatCard : Panel
{
    private Color _accentColor = Color.FromArgb(13, 71, 161);

    public Color AccentColor
    {
        get => _accentColor;
        set
        {
            _accentColor = value;
            Invalidate();
        }
    }

    protected override void OnResize(EventArgs eventargs)
    {
        base.OnResize(eventargs);
        UpdateRoundedRegion();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        Rectangle borderBounds = ClientRectangle;
        borderBounds.Width -= 1;
        borderBounds.Height -= 1;

        using GraphicsPath borderPath = CreateRoundedPath(borderBounds, 8);
        using Pen borderPen = new Pen(Color.FromArgb(227, 232, 240));
        using Brush accentBrush = new SolidBrush(AccentColor);
        e.Graphics.DrawPath(borderPen, borderPath);
        e.Graphics.FillEllipse(accentBrush, 20, 22, 10, 10);
    }

    private void UpdateRoundedRegion()
    {
        if (ClientSize.Width <= 0 || ClientSize.Height <= 0)
        {
            return;
        }

        using GraphicsPath path = CreateRoundedPath(ClientRectangle, 8);
        Region? previousRegion = Region;
        Region = new Region(path);
        previousRegion?.Dispose();
    }

    private static GraphicsPath CreateRoundedPath(Rectangle bounds, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        int diameter = radius * 2;

        path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
        path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
        path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();

        return path;
    }
}
