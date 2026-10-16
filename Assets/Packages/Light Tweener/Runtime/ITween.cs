using System;
using System.Collections;
using UnityEngine;

namespace Packages.Light_Tweener.Runtime;

/// <summary>A small coroutine-driven tween. Configure it and call Play(), or use a Transform extension.</summary>
public interface ITween<T>
{
    ITween<T> From(T start);
    ITween<T> To(T target);
    ITween<T> By(T delta);
    ITween<T> In(float duration);
    ITween<T> Easing(Func<float, float>? easing);
    ITween<T> Holder(GameObject? target);
    ITween<T> OnUpdate(Action<T>? callback);
    ITween<T> Play();
    void Cancel();

    public static ITween<T> Create(T start, Action<T> onUpdate) => new Tween<T>(start, onUpdate);
}

/// <summary>Built-in interpolations are provided for float, Vector2/3/4, Quaternion and Color.</summary>
public sealed class Tween<T> : ITween<T>
{
    private readonly Func<T, T, float, T> _interpolate;
    private T _start;
    private T _end = default!;
    private T _delta = default!;
    private bool _hasEnd;
    private bool _relative;
    private float _duration = 1f;
    private Func<float, float> _easing = EaseLinear;
    private Action<T> _onUpdate;
    private GameObject? _holder;
    private Coroutine? _coroutine;
    private bool _cancelled;

    public Tween(T start, Action<T> onUpdate)
    {
        _start = start;
        _onUpdate = onUpdate ?? throw new ArgumentNullException(nameof(onUpdate));
        _interpolate = ResolveInterpolator();
    }

    public ITween<T> From(T start) { _start = start; return this; }
    public ITween<T> To(T target) { _end = target; _hasEnd = true; _relative = false; return this; }
    public ITween<T> By(T delta) { _delta = delta; _hasEnd = true; _relative = true; return this; }
    public ITween<T> In(float duration) { _duration = Mathf.Max(0f, duration); return Play(); }
    public ITween<T> Easing(Func<float, float>? easing) { _easing = easing ?? EaseLinear; return this; }
    public ITween<T> Holder(GameObject? target) { _holder = target; return this; }
    public ITween<T> OnUpdate(Action<T>? callback) { _onUpdate = callback ?? throw new ArgumentNullException(nameof(callback)); return this; }

    public ITween<T> Play()
    {
        Cancel();
        _cancelled = false;
        if (!_hasEnd) throw new InvalidOperationException("Set To() or By() before playing a tween.");
        if (_relative) _end = Add(_start, _delta);
        _coroutine = TweenRunner.Run(_holder, Animate());
        return this;
    }

    public void Cancel()
    {
        _cancelled = true;
        if (_coroutine != null) TweenRunner.Stop(_holder, _coroutine);
        _coroutine = null;
    }

    private IEnumerator Animate()
    {
        _onUpdate(_start);
        if (_duration <= 0f) { _onUpdate(_end); _coroutine = null; yield break; }
        var elapsed = 0f;
        while (elapsed < _duration && !_cancelled)
        {
            elapsed += Time.deltaTime;
            var t = Mathf.Clamp01(elapsed / _duration);
            _onUpdate(_interpolate(_start, _end, _easing(t)));
            yield return null;
        }
        if (!_cancelled) _onUpdate(_end);
        _coroutine = null;
    }

    private static Func<T, T, float, T> ResolveInterpolator()
    {
        if (typeof(T) == typeof(float)) return (a, b, t) => Box(Mathf.LerpUnclamped(Unbox<float>(a), Unbox<float>(b), t));
        if (typeof(T) == typeof(Vector2)) return (a, b, t) => Box(Vector2.LerpUnclamped(Unbox<Vector2>(a), Unbox<Vector2>(b), t));
        if (typeof(T) == typeof(Vector3)) return (a, b, t) => Box(Vector3.LerpUnclamped(Unbox<Vector3>(a), Unbox<Vector3>(b), t));
        if (typeof(T) == typeof(Vector4)) return (a, b, t) => Box(Vector4.LerpUnclamped(Unbox<Vector4>(a), Unbox<Vector4>(b), t));
        if (typeof(T) == typeof(Color)) return (a, b, t) => Box(Color.LerpUnclamped(Unbox<Color>(a), Unbox<Color>(b), t));
        if (typeof(T) == typeof(Quaternion)) return (a, b, t) => Box(Quaternion.SlerpUnclamped(Unbox<Quaternion>(a), Unbox<Quaternion>(b), t));
        throw new NotSupportedException($"No interpolation is registered for {typeof(T).Name}.");
    }

    private static T Add(T a, T b)
    {
        if (typeof(T) == typeof(float)) return Box(Unbox<float>(a) + Unbox<float>(b));
        if (typeof(T) == typeof(Vector2)) return Box(Unbox<Vector2>(a) + Unbox<Vector2>(b));
        if (typeof(T) == typeof(Vector3)) return Box(Unbox<Vector3>(a) + Unbox<Vector3>(b));
        if (typeof(T) == typeof(Vector4)) return Box(Unbox<Vector4>(a) + Unbox<Vector4>(b));
        if (typeof(T) == typeof(Color)) return Box(Unbox<Color>(a) + Unbox<Color>(b));
        throw new NotSupportedException($"Relative tweening is not supported for {typeof(T).Name}.");
    }

    private static TValue Unbox<TValue>(T value) => (TValue)(object)value!;
    private static T Box<TValue>(TValue value) => (T)(object)value!;

    private static float EaseLinear(float t) => t;
}

internal sealed class TweenRunner : MonoBehaviour
{
    private static TweenRunner? _instance;

    internal static Coroutine Run(GameObject? holder, IEnumerator routine)
    {
        if (holder != null) return GetRunner(holder).StartCoroutine(routine);
        if (_instance == null)
        {
            var go = new GameObject("LightTweener");
            go.hideFlags = HideFlags.HideInHierarchy;
            DontDestroyOnLoad(go);
            _instance = go.AddComponent<TweenRunner>();
        }
        return _instance.StartCoroutine(routine);
    }

    internal static void Stop(GameObject? holder, Coroutine? coroutine)
    {
        if (coroutine == null) return;
        var runner = holder != null ? holder.GetComponent<TweenRunner>() : _instance;
        if (runner != null) runner.StopCoroutine(coroutine);
    }

    private static TweenRunner GetRunner(GameObject holder)
    {
        if (!holder.TryGetComponent<TweenRunner>(out var runner)) runner = holder.AddComponent<TweenRunner>();
        return runner;
    }
}
