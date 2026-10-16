using UnityEngine;

namespace Packages.Light_Tweener.Runtime;

public static class TransformExtensions
{
    public static ITween<Vector3> TweenPositionTo(this Transform transform, Vector3 target, float duration, GameObject? holder = null) =>
        Create(transform, transform.position, target, duration, value => transform.position = value, holder);

    public static ITween<Vector3> TweenPositionBy(this Transform transform, Vector3 offset, float duration, GameObject? holder = null) =>
        CreateBy(transform, transform.position, offset, duration, value => transform.position = value, holder);

    public static ITween<Vector3> TweenLocalPositionTo(this Transform transform, Vector3 target, float duration, GameObject? holder = null) =>
        Create(transform, transform.localPosition, target, duration, value => transform.localPosition = value, holder);

    public static ITween<Vector3> TweenLocalPositionBy(this Transform transform, Vector3 offset, float duration, GameObject? holder = null) =>
        CreateBy(transform, transform.localPosition, offset, duration, value => transform.localPosition = value, holder);

    public static ITween<Vector3> TweenScaleTo(this Transform transform, Vector3 target, float duration, GameObject? holder = null) =>
        Create(transform, transform.localScale, target, duration, value => transform.localScale = value, holder);

    public static ITween<Vector3> TweenScaleBy(this Transform transform, Vector3 delta, float duration, GameObject? holder = null) =>
        CreateBy(transform, transform.localScale, delta, duration, value => transform.localScale = value, holder);

    public static ITween<Quaternion> TweenRotationTo(this Transform transform, Quaternion target, float duration, GameObject? holder = null) =>
        Create(transform, transform.rotation, target, duration, value => transform.rotation = value, holder);

    public static ITween<Quaternion> TweenLocalRotationTo(this Transform transform, Quaternion target, float duration, GameObject? holder = null) =>
        Create(transform, transform.localRotation, target, duration, value => transform.localRotation = value, holder);

    public static ITween<Vector3> TweenEulerAnglesTo(this Transform transform, Vector3 target, float duration, GameObject? holder = null) =>
        Create(transform, transform.eulerAngles, target, duration, value => transform.eulerAngles = value, holder);

    public static ITween<Vector3> TweenEulerAnglesBy(this Transform transform, Vector3 delta, float duration, GameObject? holder = null) =>
        CreateBy(transform, transform.eulerAngles, delta, duration, value => transform.eulerAngles = value, holder);

    public static ITween<Vector3> TweenLocalEulerAnglesTo(this Transform transform, Vector3 target, float duration, GameObject? holder = null) =>
        Create(transform, transform.localEulerAngles, target, duration, value => transform.localEulerAngles = value, holder);

    public static ITween<Vector3> TweenLocalEulerAnglesBy(this Transform transform, Vector3 delta, float duration, GameObject? holder = null) =>
        CreateBy(transform, transform.localEulerAngles, delta, duration, value => transform.localEulerAngles = value, holder);

    private static ITween<Vector3> Create(Transform transform, Vector3 from, Vector3 to, float duration, System.Action<Vector3> setter, GameObject? holder) =>
        ITween<Vector3>.Create(from, setter).To(to).Holder(holder ?? transform.gameObject).In(duration);

    private static ITween<Vector3> CreateBy(Transform transform, Vector3 from, Vector3 delta, float duration, System.Action<Vector3> setter, GameObject? holder) =>
        ITween<Vector3>.Create(from, setter).By(delta).Holder(holder ?? transform.gameObject).In(duration);

    private static ITween<Quaternion> Create(Transform transform, Quaternion from, Quaternion to, float duration, System.Action<Quaternion> setter, GameObject? holder) =>
        ITween<Quaternion>.Create(from, setter).To(to).Holder(holder ?? transform.gameObject).In(duration);
}
