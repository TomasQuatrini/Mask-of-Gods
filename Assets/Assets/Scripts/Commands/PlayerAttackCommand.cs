using System;
using System.Collections;
using UnityEngine;

public class PlayerAttackCommand : ICommand
{
    private AttackSetting _settings;
    private GameObject _weapon;
    private PlayerAttackController _controller;
    private Coroutine _Attackcoroutine;

    public PlayerAttackCommand(AttackSetting settings, GameObject weapon,
        PlayerAttackController controller)
    {
        _settings = settings;
        _weapon = weapon;
        _weapon.SetActive(false);
        _controller = controller;
        _Attackcoroutine = null;

    }
    public void Execute()
    {
        Debug.Log("Ejecutando Ataque");
        if (_Attackcoroutine == null)
        {
            Debug.Log("Ejecutando ataque");
            _Attackcoroutine = _controller.StartCoroutine(AttackCoroutine());
        }
    }
    public IEnumerator AttackCoroutine()
    {
        _weapon.SetActive(true);
        yield return new WaitForSeconds(_settings.attackDuration);
        _weapon.SetActive(false);
        _Attackcoroutine = null;
    }
}
