using System;

public interface ILevel
{
    event Action<float> onLevelChanged;
    event Action<float> onExperienceChanged;
}