using System.Drawing;

public static class Bezier2D
{
    // Linear interpolation function for 2D points
    public static PointF Lerp(PointF start, PointF end, float t)
    {
        float x = start.X + t * (end.X - start.X);
        float y = start.Y + t * (end.Y - start.Y);
        return new PointF(x, y);
    }

    static int i = 0;
    public static PointF[] Lerp(float t, params PointF[] points)
    {
        if (points.Length == 1) return points;
        PointF[] Less = new PointF[points.Length - 1];
        for (int i = 0; i < points.Length - 1; i++)
        {
            Less[i] = Lerp(points[i], points[i + 1], t);
        }
        return Lerp(t, Less);
    }

    //// Quadratic Bezier curve calculation for 2D points
    //public static PointF QuadraticBezier(PointF p0, PointF p1, PointF p2, float t)
    //{
    //    PointF q0 = Lerp(p0, p1, t);
    //    PointF q1 = Lerp(p1, p2, t);

    //    return Lerp(q0, q1, t);
    //}

    //// Cubic Bezier curve calculation for 2D points
    //public static PointF CubicBezier(PointF p0, PointF p1, PointF p2, PointF p3, float t)
    //{
    //    PointF q0 = Lerp(p0, p1, t);
    //    PointF q1 = Lerp(p1, p2, t);
    //    PointF q2 = Lerp(p2, p3, t);

    //    PointF r0 = Lerp(q0, q1, t);
    //    PointF r1 = Lerp(q1, q2, t);

    //    return Lerp(r0, r1, t);
    //}
}