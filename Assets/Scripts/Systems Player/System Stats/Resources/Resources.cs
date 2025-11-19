using Game.Stats;
using System;
using UnityEngine;

public class Resource
{
    public string Name { get; set; } = "Resource";
    public float BaseMax { get; private set; }
    public float Max { get; private set; }
    public float Current { get; private set; }
    public float MultiplyLvl { get; private set; }

    public event Action<float> OnMaxChanged;
    public event Action<float> OnCurrentChanged;

    public Resource(string name, float baseMax, float multiplyLevel ,float initialCurrent = -1f)
    {
        if (baseMax <= 0f) throw new ArgumentOutOfRangeException(nameof(baseMax));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException(nameof(name));
        if (multiplyLevel < 0f) throw new ArgumentOutOfRangeException(nameof(multiplyLevel));
        MultiplyLvl = multiplyLevel;
        BaseMax = baseMax;
        Max = baseMax;
        Current = initialCurrent < 0f ? Max : Clamp(initialCurrent, 0f, Max);
        Name = name;
    }

    public void RecomputeMaxFromStat(Stat stat)
    {
        float oldMax = Max;
        float scale = Math.Max(MultiplyLvl, stat.Value);
        Max = Math.Max(1f, BaseMax * scale);

        if (!Mathf.Approximately(Max, oldMax))
        {
            Current = oldMax > 0 ? (Current * Max) / oldMax : Max;
            Current = Clamp(Current, 0f, Max);
            OnMaxChanged?.Invoke(Max);
            OnCurrentChanged?.Invoke(Current);
        }
    }

    public void Increase(float amount)
    {
        if (amount <= 0f) return;
        float old = Current;
        Current = Clamp(Current + amount, 0f, Max);
        if (!Mathf.Approximately(Current, old))
            OnCurrentChanged?.Invoke(Current);
    }

    public bool Spend(float amount)
    {
        if (amount <= 0f) return true;
        if (Current < amount) return false;
        Current -= amount;
        OnCurrentChanged?.Invoke(Current);
        return true;
    }

    public void Decrease(float amount)
    {
        if (amount <= 0f) return;
        Current -= amount;
        if (Current < 0f) Current = 0f;
        OnCurrentChanged?.Invoke(Current);
    }

    public void SetToMax()
    {         
        if (Mathf.Approximately(Current, Max)) return;
        Current = Max;
        OnCurrentChanged?.Invoke(Current);
    }

    private static float Clamp(float v, float min, float max) => v < min ? min : (v > max ? max : v);
}