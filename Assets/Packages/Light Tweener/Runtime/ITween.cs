using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEngine;

namespace Packages.Light_Tweener.Runtime;

public interface ITween<in T>
{
    ITween<T> From(T start);
    ITween<T> To(T target);
    ITween<T> By(T delta);
    ITween<T> In(float duration);
    ITween<T> Easing(Func<float, float> easing);
    ITween<T> Holder(GameObject target);
    void Cancel();
    
    public static ITween<T> Create(T start) {}
}