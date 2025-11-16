using System.Collections;
using UnityEngine;

public class AresMaskCommand : ICommand
{
    private MaskSetting _settings;
    private GameObject _mask;
    private PlayerAttackController _controller;
    private Coroutine _Attackcoroutine;

    public AresMaskCommand(MaskSetting settings, GameObject weapon,
        PlayerAttackController controller)
    {
        _settings = settings;
        _mask = weapon;
        _mask.SetActive(false);
        _controller = controller;
        _Attackcoroutine = null;
    }
    public void Execute()
    {
        //Debug.Log("Ejecutando Ataque Ares");
        if (_Attackcoroutine == null)
        {
            Debug.Log("Ejecutando ataque Ares");
            _Attackcoroutine = _controller.StartCoroutine(AttackCoroutine());
        }
    }
    public IEnumerator AttackCoroutine()
    {
        _controller.SetAttacking(true);
        _mask.SetActive(true);
        Debug.Log("Ejecutando ataque Ares");
        yield return new WaitForSeconds(_settings.duration);
        _controller.SetAttacking(false);
        _mask.SetActive(false);
        _Attackcoroutine = null;

    }
}
