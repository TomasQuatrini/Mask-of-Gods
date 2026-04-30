using TMPro;
using UnityEngine;
public class HUD_LevelAndExperienceView : MonoBehaviour
{ 
    [SerializeField] private TMP_Text levelValueText;
    [SerializeField] private TMP_Text experienceValueText;
    [SerializeField] private PlayerLevel _playerLevel;

    private void Start()
    {
        _playerLevel.onExperienceChanged += SetExperiencie;
        _playerLevel.onLevelChanged += SetLevel;
    }
    public void SetLevel(float level)
    {
        levelValueText.text = $"Level: {level}";
        Debug.Log("se modifico el nivel");
    }

    public void SetExperiencie(float experience)
    {
        experienceValueText.text = $"Experience: {experience}";
        Debug.Log("se modifico la experiencia");

    }

    private void OnDisable()
    {
        _playerLevel.onExperienceChanged -= SetExperiencie;
        _playerLevel.onLevelChanged -= SetLevel;
    }
}
