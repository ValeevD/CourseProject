using UnityEngine;

public static class PositionChecker {
    public static Vector2 CheckStartPosition(Vector2 curPosition, float radius, int partOfCircle)
    {
        float borderValue;

        if(partOfCircle == 1)
            borderValue = Mathf.Max(curPosition.x, 0);
        else
            borderValue = Mathf.Min(curPosition.x, 0);

        return NormalizeOnRadius(new Vector2(borderValue, curPosition.y), radius);

    }

    public static Vector2 CheckPosition(Vector2 curPosition, Vector2 newPosition, float radius, float smallRadius)
    {
        Vector2 borderVectorOnSmallRadius = curPosition + NormalizeOnRadius(newPosition - curPosition, smallRadius);

        if(borderVectorOnSmallRadius.magnitude <= radius)
            return borderVectorOnSmallRadius;

        return GetLineCircleCrossPoint(curPosition, borderVectorOnSmallRadius, radius);

    }

    private static Vector2 GetLineCircleCrossPoint(Vector2 v1, Vector2 v2, float radius)
    {
        float A = v1.y - v2.y,
              B = v2.x - v1.x,
              C = v1.x * v2.y - v2.x * v1.y;

        float d = Mathf.Sqrt(radius * radius - (C * C / (A * A + B * B)));

        float mult = Mathf.Sqrt(d * d / (A * A + B * B));

        float x0 = -A * C / (A * A + B* B),
              y0 = -B * C / (A * A + B* B);

        return new Vector2(x0 + B * mult, y0 - A * mult);
    }

    public static (float, float, float) GetLineEQ(Vector2 v1, Vector2 v2)
    {
        float A = v1.y - v2.y,
              B = v2.x - v1.x,
              C = v1.x * v2.y - v2.x * v1.y;

        return (A, B, C);
    }

    public static float GetDistance(Vector2 v1, Vector2 v2, Vector2 centre)
    {
        (float, float, float) tuple = GetLineEQ(v1, v2);

        return Mathf.Abs(tuple.Item1 * centre.x + tuple.Item2 * centre.y + tuple.Item3) / Mathf.Sqrt(tuple.Item1 * tuple.Item1 + tuple.Item2 * tuple.Item2);
    }

    private static Vector2 NormalizeOnRadius(Vector2 pos, float radius)
    {
        Vector2 newVector = pos;

        if(pos.magnitude > radius)
            newVector = newVector.normalized * radius;

        return newVector;
    }
}
