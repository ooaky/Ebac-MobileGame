using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : CollectableBase
{
    [Header("Power Up Duration")]
    public float duration;


    private void Start()
    {
        PowerUpAnimatorManager.Instance.RegisterPowerUp(this);
    }

    protected override void OnCollect() // override = sobreescreve a função orgiinal que está sendo herdada, adicionando comandos
    { 
        base.OnCollect();
        PlayerController.Instance.Bounce();
        StartPowerUp(); 
    }

    protected virtual void StartPowerUp() 
    { 
        Debug.Log("Start Power Up");
        Invoke(nameof(EndPowerUp), duration); 
    }

    protected virtual void EndPowerUp() 
    { 
        Debug.Log("End Power Up"); 
    }







}
