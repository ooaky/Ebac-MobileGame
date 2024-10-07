using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : PowerUpBase
{
    [Header("Power Up Speed Up")]
    public float speedUp;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.PowerUpSpeedUp(speedUp); //vira a nova velocidade
        PlayerController.Instance.SetPowerUpText("Speed up");
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.ResetSpeed();
        PlayerController.Instance.SetPowerUpText("");
    }
}
