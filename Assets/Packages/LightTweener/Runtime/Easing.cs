using UnityEngine;
// ReSharper disable CheckNamespace

namespace LightTweener;

public static class Easing
{
    private const float Pi = Mathf.PI;

    public static float Linear(float t) => t;
    public static float EaseIn(float t) => EaseInQuad(t);
    public static float EaseOut(float t) => EaseOutQuad(t);
    public static float EaseInOut(float t) => EaseInOutQuad(t);

    public static float EaseInQuad(float t) => t * t;
    public static float EaseOutQuad(float t) => 1f - (1f - t) * (1f - t);
    public static float EaseInOutQuad(float t) => t < 0.5f ? 2f * t * t : 1f - Mathf.Pow(-2f * t + 2f, 2f) / 2f;

    public static float EaseInCubic(float t) => t * t * t;
    public static float EaseOutCubic(float t) => 1f - Mathf.Pow(1f - t, 3f);
    public static float EaseInOutCubic(float t) => t < 0.5f
        ? 4f * t * t * t
        : 1f - Mathf.Pow(-2f * t + 2f, 3f) / 2f;

    public static float EaseInQuart(float t) => t * t * t * t;
    public static float EaseOutQuart(float t) => 1f - Mathf.Pow(1f - t, 4f);
    public static float EaseInOutQuart(float t) => t < 0.5f
        ? 8f * t * t * t * t
        : 1f - Mathf.Pow(-2f * t + 2f, 4f) / 2f;

    public static float EaseInQuint(float t) => t * t * t * t * t;
    public static float EaseOutQuint(float t) => 1f - Mathf.Pow(1f - t, 5f);
    public static float EaseInOutQuint(float t) => t < 0.5f
        ? 16f * t * t * t * t * t
        : 1f - Mathf.Pow(-2f * t + 2f, 5f) / 2f;

    public static float EaseInSine(float t) => 1f - Mathf.Cos(t * Pi / 2f);
    public static float EaseOutSine(float t) => Mathf.Sin(t * Pi / 2f);
    public static float EaseInOutSine(float t) => -(Mathf.Cos(Pi * t) - 1f) / 2f;

    public static float EaseInExpo(float t) => t == 0f ? 0f : Mathf.Pow(2f, 10f * t - 10f);
    public static float EaseOutExpo(float t) => t == 1f ? 1f : 1f - Mathf.Pow(2f, -10f * t);
    public static float EaseInOutExpo(float t)
    {
        if (t == 0f || t == 1f) return t;
        return t < 0.5f
            ? Mathf.Pow(2f, 20f * t - 10f) / 2f
            : (2f - Mathf.Pow(2f, -20f * t + 10f)) / 2f;
    }

    public static float EaseInCirc(float t) => 1f - Mathf.Sqrt(Mathf.Max(0f, 1f - t * t));
    public static float EaseOutCirc(float t) => Mathf.Sqrt(Mathf.Max(0f, 1f - Mathf.Pow(t - 1f, 2f)));
    public static float EaseInOutCirc(float t) => t < 0.5f
        ? (1f - Mathf.Sqrt(Mathf.Max(0f, 1f - 4f * t * t))) / 2f
        : (Mathf.Sqrt(Mathf.Max(0f, 1f - Mathf.Pow(-2f * t + 2f, 2f))) + 1f) / 2f;

    public static float EaseInBack(float t)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1f;
        return c3 * t * t * t - c1 * t * t;
    }

    public static float EaseOutBack(float t)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1f;
        return 1f + c3 * Mathf.Pow(t - 1f, 3f) + c1 * Mathf.Pow(t - 1f, 2f);
    }

    public static float EaseInOutBack(float t)
    {
        const float c1 = 1.70158f;
        const float c2 = c1 * 1.525f;
        return t < 0.5f
            ? Mathf.Pow(2f * t, 2f) * ((c2 + 1f) * 2f * t - c2) / 2f
            : (Mathf.Pow(2f * t - 2f, 2f) * ((c2 + 1f) * (t * 2f - 2f) + c2) + 2f) / 2f;
    }

    public static float EaseInBounce(float t) => 1f - EaseOutBounce(1f - t);

    public static float EaseOutBounce(float t)
    {
        const float n1 = 7.5625f;
        const float d1 = 2.75f;

        if (t < 1f / d1) return n1 * t * t;
        if (t < 2f / d1)
        {
            t -= 1.5f / d1;
            return n1 * t * t + 0.75f;
        }
        if (t < 2.5f / d1)
        {
            t -= 2.25f / d1;
            return n1 * t * t + 0.9375f;
        }

        t -= 2.625f / d1;
        return n1 * t * t + 0.984375f;
    }

    public static float EaseInOutBounce(float t) => t < 0.5f
        ? (1f - EaseOutBounce(1f - 2f * t)) / 2f
        : (1f + EaseOutBounce(2f * t - 1f)) / 2f;

    public static float EaseInElastic(float t)
    {
        const float c4 = 2f * Pi / 3f;
        if (t == 0f || t == 1f) return t;
        return -Mathf.Pow(2f, 10f * t - 10f) * Mathf.Sin((t * 10f - 10.75f) * c4);
    }

    public static float EaseOutElastic(float t)
    {
        const float c4 = 2f * Pi / 3f;
        if (t == 0f || t == 1f) return t;
        return Mathf.Pow(2f, -10f * t) * Mathf.Sin((t * 10f - 0.75f) * c4) + 1f;
    }

    public static float EaseInOutElastic(float t)
    {
        const float c5 = 2f * Pi / 4.5f;
        if (t == 0f || t == 1f) return t;
        return t < 0.5f
            ? -(Mathf.Pow(2f, 20f * t - 10f) * Mathf.Sin((20f * t - 11.125f) * c5)) / 2f
            : Mathf.Pow(2f, -20f * t + 10f) * Mathf.Sin((20f * t - 11.125f) * c5) / 2f + 1f;
    }
}
