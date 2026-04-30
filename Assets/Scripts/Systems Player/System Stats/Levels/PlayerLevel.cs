using UnityEngine;
using System;

public class PlayerLevel : MonoBehaviour, ILevel
{
    private float _experiencePoints;
    private float _level;
    [SerializeField] private float _expLevelUp;

    [Header("Dependencies")]
    private PlayerStatsComponent _statsComponent;
    private PlayerContext _ctx;
    [SerializeField] private HUD_LevelAndExperienceView _levelAndExperienceView;

    public event Action<float> onLevelChanged;
    public event Action<float> onExperienceChanged;

    [Header("UI")]
    public float Level => _level;
    public float Experience => _experiencePoints;

    void Start()
    {
        _ctx = GetComponentInParent<PlayerContext>();
        _statsComponent = _ctx.StatsComponent;
        _experiencePoints = 0f;
        _level = 1f;
        onLevelChanged?.Invoke(_level);
        Debug.Log("se invocaron los eventos");
        onExperienceChanged?.Invoke(_experiencePoints);
    }

    public void AddExperience(float amount)
    {
        if (amount <= 0f) return;
        _experiencePoints += amount;
        onExperienceChanged?.Invoke(_experiencePoints);
        CheckLevelUp();
    }

    public void RemoveExperience(float amount)
    {

    }

    private void CheckLevelUp()
    {
        while (_experiencePoints >= _expLevelUp)
        {
            _experiencePoints -= _expLevelUp;
            _level++;
            onLevelChanged?.Invoke(_level);
            _statsComponent.Stats.StatUp(Game.Stats.StatType.Health);
            _statsComponent.Stats.StatUp(Game.Stats.StatType.Stamina);
        }
    }
}
