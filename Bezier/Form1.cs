namespace Bezier;

public partial class Form1 : Form
{
    const int radius = 2;
    Button b;
    float X;
    float HEIGHT;
    float Y;

    Graphics g;

    List<PointF> points = new();

    private void DrawLines_Click(object? sender, EventArgs e)
    {
        for (float t = 0; t <= 1; t = t + .001F)
        {
            PointF p = Bezier2D.Lerp(t, points.ToArray())[0];
            DrawPoint(p, Brushes.Red, false);
            Thread.Sleep(0);
        }

        //DrawPoint(p, Brushes.Red, false);
        //if (points.Count == 3)
        //{
        //    DrawLine(points[0],points[1], Brushes.White);
        //    DrawLine(points[1], points[2], Brushes.White);

        //    for (float t = 0; t <= 1; t = t + .001F)
        //    {
        //        var p = Bezier2D.QuadraticBezier(points[0], points[1], points[2], t);
        //        DrawPoint(p, Brushes.Red, false);
        //        Thread.Sleep(1);
        //    }
        //} else if (points.Count==4)
        //{
        //    DrawLine(points[0], points[1], Brushes.White);
        //    DrawLine(points[1], points[2], Brushes.White);
        //    DrawLine(points[2], points[3], Brushes.White);
        //    for (float t = 0; t <= 1; t = t + .001F)
        //    {
        //        var p = Bezier2D.CubicBezier(points[0], points[1], points[2], points[3], t);
        //        DrawPoint(p, Brushes.Red, false);
        //        //Thread.Sleep(1);
        //    }

        //}
        //for (int i = 0; i < points.Count - 1; i++)
        //{
        //    DrawLine(points[i], points[i + 1]);
        //}
    }

    #region UI Event Handlers
    public Form1()
    {
        InitializeComponent();
        this.BackColor = Color.Black;
        MakeButton("Draw Lines", this.DrawLines_Click, 10, 10);
        MakeButton("Clear", this.Clear_Click, 10, 40);
    }
    private Button MakeButton(string text, EventHandler handler, int left, int top)
    {
        b = new Button();
        b.Text = text;
        b.Left = left;
        b.Top = top;
        b.Click += handler;
        b.BackColor = Color.White;
        b.ForeColor = Color.Blue;
        b.Visible = true;
        this.Controls.Add(b);
        return b;
    }
    private void Clear_Click(object? sender, EventArgs e)
    {
        points.Clear();
        this.Invalidate();
        this.Text = "0 points";
    }
    private void Form1_Load(object sender, EventArgs e)
    {
        Form1_Resize(this, EventArgs.Empty);
    }

    private void Form1_Resize(object sender, EventArgs e)
    {
        X = this.Width / 2.0F;
        HEIGHT = this.Height - 30;
        Y = HEIGHT / 2;

        g = this.CreateGraphics();
        repaint(g);
    }
    void repaint(Graphics g)
    {
        DrawAxes(g);

        for (int i = 0; i < points.Count; i++)
        {
            DrawPoint(points[i], Brushes.White);
        }
    }
    private void Form1_MouseUp(object sender, MouseEventArgs e)
    {
        this.MouseUp -= new MouseEventHandler(this.Form1_MouseUp);
        Point p = new Point(e.X - (int)X, (int)Y - e.Y);
        points.Add(p);
        this.Invalidate();
        this.MouseUp += new MouseEventHandler(this.Form1_MouseUp);
        this.Text = $"{points.Count} points";
    }

    private void Form1_Paint(object sender, PaintEventArgs e)
    {
        repaint(e.Graphics);
    }
    #endregion

    #region Drawing tools
    float getX(float x) => X + x;
    float getY(float y) => Y - y;
    void DrawLine(PointF p1, PointF p2, Brush brush)
    {
        (float m, float b) = Line(p1, p2);
        for (float x = p1.X; x < p2.X; x = x + 2)
        {
            float y = m * x + b;
            DrawPoint(x, y, brush, false);
        }
    }
    void DrawPoint(float x, float y, Brush brush, bool text = true)
    {
        Pen p = Pens.White;
        if (brush == Brushes.Red)
            p = Pens.Red;

        float _x = getX(x);
        float _y = getY(y);
        g.DrawEllipse(p, _x, _y, radius, radius);
        if (text)
        {
            g.DrawString($"({x}.{y})", this.Font, brush, _x + 10, _y - 10);
        }
    }
    void DrawPoint(PointF p, Brush brush, bool text = true) => DrawPoint(p.X, p.Y, brush, text);
    (float m, float b) Line(PointF p1, PointF p2)
    {
        //y=mx+b
        //b=y-mx
        float m = (p2.Y - p1.Y) / (p2.X - p1.X);
        float b = p1.Y - m * p1.X;
        return (m, b);
    }
    #endregion
    private void DrawAxes(Graphics g)
    {
        g.Clear(Color.Black);
        g.DrawLine(Pens.White, X, 0, X, HEIGHT);
        g.DrawLine(Pens.White, 0, Y, this.Width, Y);
    }
}