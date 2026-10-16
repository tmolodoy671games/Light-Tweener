using System.Collections;
using System.Collections.Generic;
using LightTweener;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

// ReSharper disable CheckNamespace

namespace LightTweener.Tests;

public sealed class LightTweenerTweenTests
{
    private GameObject _holder = null!;

    [SetUp]
    public void SetUp() => _holder = new GameObject("LightTweener test holder");

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        if (_holder != null) Object.Destroy(_holder);
        yield return null;
    }

    [Test]
    public void ZeroDurationTo_EmitsStartAndTarget()
    {
        var values = new List<float>();

        ITween.Create<float>()
            .From(2f)
            .To(8f)
            .Holder(_holder)
            .OnUpdate(values.Add)
            .In(0f);

        CollectionAssert.AreEqual(new[] { 2f, 8f }, values);
    }

    [Test]
    public void FromOverridesInitialValue()
    {
        var value = -1f;

        ITween.Create<float>()
            .From(4f)
            .To(9f)
            .Holder(_holder)
            .OnUpdate(next => value = next)
            .In(0f);

        Assert.That(value, Is.EqualTo(9f));
    }

    [Test]
    public void ByAddsDeltaToStart()
    {
        var value = Vector3.zero;

        ITween.Create<Vector3>()
            .From(new Vector3(1f, 2f, 3f))
            .By(new Vector3(3f, -1f, 2f))
            .Holder(_holder)
            .OnUpdate(next => value = next)
            .In(0f);

        Assert.That(value, Is.EqualTo(new Vector3(4f, 1f, 5f)));
    }

    [Test]
    public void PlayWithoutTarget_Throws()
    {
        var tween = ITween.Create<float>()
            .From(0f)
            .OnUpdate(_ => { })
            .Holder(_holder);

        Assert.Throws<System.InvalidOperationException>(() => tween.Play());
    }

    [Test]
    public void UnsupportedType_Throws()
    {
        Assert.Throws<System.NotSupportedException>(() => ITween.Create<string>());
    }

    [UnityTest]
    public IEnumerator CustomInterpolator_CanTweenCustomType()
    {
        var values = new List<string>();
        var tween = ITween.Create<string>((from, to, t) => "custom");

        tween.From("start")
            .To("target")
            .OnUpdate(values.Add)
            .Holder(_holder)
            .In(1f);

        yield return null;

        CollectionAssert.Contains(values, "custom");
        tween.Cancel();
    }

    [UnityTest]
    public IEnumerator DurationTween_ReachesExactTarget()
    {
        var value = 0f;

        ITween.Create<float>()
            .From(0f)
            .To(1f)
            .Holder(_holder)
            .OnUpdate(next => value = next)
            .In(0.05f);

        yield return new WaitForSeconds(0.1f);

        Assert.That(value, Is.EqualTo(1f));
    }

    [UnityTest]
    public IEnumerator Easing_IsAppliedToIntermediateValues()
    {
        var values = new List<float>();

        ITween.Create<float>()
            .From(0f)
            .To(1f)
            .Easing(_ => 0.25f)
            .Holder(_holder)
            .OnUpdate(values.Add)
            .In(0.1f);

        yield return null;
        yield return new WaitForSeconds(0.15f);

        CollectionAssert.Contains(values, 0.25f);
        Assert.That(values[values.Count - 1], Is.EqualTo(1f));
    }

    [UnityTest]
    public IEnumerator Cancel_StopsFurtherUpdates()
    {
        var value = 0f;
        var tween = ITween.Create<float>()
            .From(0f)
            .To(10f)
            .Holder(_holder)
            .OnUpdate(next => value = next)
            .In(1f);

        yield return null;
        tween.Cancel();
        var valueAtCancel = value;
        yield return new WaitForSeconds(0.05f);

        Assert.That(value, Is.EqualTo(valueAtCancel));
        Assert.That(value, Is.LessThan(10f));
    }

    [Test]
    public void TransformPositionToAndBy_SetFinalWorldPosition()
    {
        var transform = _holder.transform;
        transform.position = new Vector3(1f, 2f, 3f);

        transform.TweenPositionTo(new Vector3(5f, 6f, 7f), 0f);
        Assert.That(transform.position, Is.EqualTo(new Vector3(5f, 6f, 7f)));

        transform.TweenPositionBy(new Vector3(2f, -1f, 3f), 0f);
        Assert.That(transform.position, Is.EqualTo(new Vector3(7f, 5f, 10f)));
    }

    [Test]
    public void TransformLocalPositionAndScaleExtensions_SetFinalValues()
    {
        var transform = _holder.transform;

        transform.TweenLocalPositionTo(new Vector3(2f, 3f, 4f), 0f);
        transform.TweenLocalPositionBy(Vector3.one, 0f);
        transform.TweenScaleTo(new Vector3(2f, 3f, 4f), 0f);

        Assert.That(transform.localPosition, Is.EqualTo(new Vector3(3f, 4f, 5f)));
        Assert.That(transform.localScale, Is.EqualTo(new Vector3(2f, 3f, 4f)));
    }

    [Test]
    public void TransformRotationExtensions_SetFinalRotations()
    {
        var transform = _holder.transform;
        var rotation = Quaternion.Euler(10f, 20f, 30f);
        var localRotation = Quaternion.Euler(0f, 90f, 0f);

        transform.TweenRotationTo(rotation, 0f);
        Assert.That(Quaternion.Angle(transform.rotation, rotation), Is.LessThan(0.001f));

        transform.TweenLocalRotationTo(localRotation, 0f);
        Assert.That(Quaternion.Angle(transform.localRotation, localRotation), Is.LessThan(0.001f));
    }
}