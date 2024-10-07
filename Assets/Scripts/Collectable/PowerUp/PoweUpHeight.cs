using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PoweUpHeight : PowerUpBase
{
    [Header("Power Up Height")]
    public float amountHeight = 2;
    public float animationDuration = .1f;
    public DG.Tweening.Ease ease = DG.Tweening.Ease.OutBack;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.SetPowerUpText("Flying");
        PlayerController.Instance.ChangeHeight(amountHeight, duration, animationDuration, ease); //quanto vai subir, por quanto tempo, duração da animação, tipo de animação
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.ResetHeight(animationDuration);
    }
}
