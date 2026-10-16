using UnityEngine;

// ReSharper disable CheckNamespace

namespace LightTweener;

public static class TransformExtensions
{
    public static ITween<Vector3> TweenPositionTo(
        this Transform transform,
        Vector3 target,
        float duration,
        GameObject? holder = null
    )
    {
        return ITween.Create<Vector3>()
            .From(transform.position)
            .To(target)
            .Holder(holder ?? transform.gameObject)
            .OnUpdate(value => transform.position = value)
            .In(duration);
    }

    public static ITween<Vector3> TweenPositionBy(
        this Transform transform,
        Vector3 offset,
        float duration,
        GameObject? holder = null
    )
    {
        return ITween.Create<Vector3>()
            .From(transform.position)
            .By(offset)
            .Holder(holder ?? transform.gameObject)
            .OnUpdate(value => transform.position = value)
            .In(duration);
    }

    public static ITween<Vector3> TweenLocalPositionTo(
        this Transform transform,
        Vector3 target,
        float duration,
        GameObject? holder = null
    )
    {
        return ITween.Create<Vector3>()
            .From(transform.localPosition)
            .To(target)
            .Holder(holder ?? transform.gameObject)
            .OnUpdate(value => transform.localPosition = value)
            .In(duration);
    }

    public static ITween<Vector3> TweenLocalPositionBy(
        this Transform transform,
        Vector3 offset,
        float duration,
        GameObject? holder = null
    )
    {
        return ITween.Create<Vector3>()
            .From(transform.localPosition)
            .By(offset)
            .Holder(holder ?? transform.gameObject)
            .OnUpdate(value => transform.localPosition = value)
            .In(duration);
    }

    public static ITween<Vector3> TweenScaleTo(
        this Transform transform,
        Vector3 target,
        float duration,
        GameObject? holder = null
    )
    {
        return ITween.Create<Vector3>()
            .From(transform.localScale)
            .To(target)
            .Holder(holder ?? transform.gameObject)
            .OnUpdate(value => transform.localScale = value)
            .In(duration);
    }

    public static ITween<Vector3> TweenScaleBy(
        this Transform transform,
        Vector3 delta,
        float duration,
        GameObject? holder = null
    )
    {
        return ITween.Create<Vector3>()
            .From(transform.localScale)
            .By(delta)
            .Holder(holder ?? transform.gameObject)
            .OnUpdate(value => transform.localScale = value)
            .In(duration);
    }

    public static ITween<Quaternion> TweenRotationTo(
        this Transform transform,
        Quaternion target,
        float duration,
        GameObject? holder = null
    )
    {
        return ITween.Create<Quaternion>()
            .From(transform.rotation)
            .To(target)
            .Holder(holder ?? transform.gameObject)
            .OnUpdate(value => transform.rotation = value)
            .In(duration);
    }

    public static ITween<Quaternion> TweenLocalRotationTo(
        this Transform transform,
        Quaternion target,
        float duration,
        GameObject? holder = null
    )
    {
        return ITween.Create<Quaternion>()
            .From(transform.localRotation)
            .To(target)
            .Holder(holder ?? transform.gameObject)
            .OnUpdate(value => transform.localRotation = value)
            .In(duration);
    }

    public static ITween<Vector3> TweenEulerAnglesTo(
        this Transform transform,
        Vector3 target,
        float duration,
        GameObject? holder = null
    )
    {
        return ITween.Create<Vector3>()
            .From(transform.eulerAngles)
            .To(target)
            .Holder(holder ?? transform.gameObject)
            .OnUpdate(value => transform.eulerAngles = value)
            .In(duration);
    }

    public static ITween<Vector3> TweenEulerAnglesBy(
        this Transform transform,
        Vector3 delta,
        float duration,
        GameObject? holder = null
    )
    {
        return ITween.Create<Vector3>()
            .From(transform.eulerAngles)
            .By(delta)
            .Holder(holder ?? transform.gameObject)
            .OnUpdate(value => transform.eulerAngles = value)
            .In(duration);
    }

    public static ITween<Vector3> TweenLocalEulerAnglesTo(
        this Transform transform,
        Vector3 target,
        float duration,
        GameObject? holder = null
    )
    {
        return ITween.Create<Vector3>()
            .From(transform.localEulerAngles)
            .To(target)
            .Holder(holder ?? transform.gameObject)
            .OnUpdate(value => transform.localEulerAngles = value)
            .In(duration);
    }

    public static ITween<Vector3> TweenLocalEulerAnglesBy(
        this Transform transform,
        Vector3 delta,
        float duration,
        GameObject? holder = null
    )
    {
        return ITween.Create<Vector3>()
            .From(transform.localEulerAngles)
            .By(delta)
            .Holder(holder ?? transform.gameObject)
            .OnUpdate(value => transform.localEulerAngles = value)
            .In(duration);
    }
}
