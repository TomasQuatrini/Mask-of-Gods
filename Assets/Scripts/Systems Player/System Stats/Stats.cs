using Game.Stats;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsComponent : MonoBehaviour
{
    public PlayerStats Stats { get; private set; }

    public void Awake()
    {
        Stats = new PlayerStats();
        Stats.Init();
    }
}

namespace Game.Stats
{
    public enum StatType { Health, Stamina, Damage, Defense }

    public class PlayerStats
    {
        public Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>();
        public Stat Get(StatType t)
        {
            stats.TryGetValue(t, out var stat);
            return stat;
        }

        public void StatUp(StatType t)
        {             
            if (stats.TryGetValue(t, out var stat))
            {
                stat.AddValue(1f);
            }
        }

        public void Init()
        {
            stats[StatType.Health] = new Stat("Health", 1f);
            stats[StatType.Stamina] = new Stat("Stamina", 1f);           
        }
    }

    public class Stat
    {
        public string Name { get; }
        public float Base { get; private set; }
        public float Value => Base + _bonusTotal;
        public event Action<float> OnValueChanged;

        private readonly Dictionary<object, float> _bonuses = new();
        private float _bonusTotal = 0f;

        public Stat(string name, float baseValue)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException(nameof(name));
            if (baseValue < 0f) throw new ArgumentOutOfRangeException(nameof(baseValue));
            Name = name;
            Base = baseValue;
        }

        public void SetBase(float newBase)
        {
            if (newBase < 0f) throw new ArgumentOutOfRangeException(nameof(newBase));
            Base = newBase;
            OnValueChanged?.Invoke(Value);
        }

        public void AddBonus(object source, float amount)
        {
            if (_bonuses.TryGetValue(source, out var prev)) _bonusTotal -= prev;
            _bonuses[source] = amount;
            _bonusTotal += amount;
            OnValueChanged?.Invoke(Value);
        }

        public void RemoveBonusBySource(object source)
        {
            if (_bonuses.TryGetValue(source, out var prev))
            {
                _bonusTotal -= prev;
                _bonuses.Remove(source);
                OnValueChanged?.Invoke(Value);
            }
        }

        public void AddValue(float amount)
        {
            SetBase(Base + amount);
        }   

        public void RemoveValue(float amount)
        {
            SetBase(Math.Max(0f, Base - amount));
        }
    }
}