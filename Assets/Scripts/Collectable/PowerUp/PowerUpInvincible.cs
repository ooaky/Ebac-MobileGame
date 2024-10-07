using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInvincible : PowerUpBase
{


    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.SetPowerUpText("Invincible");
        PlayerController.Instance.SetInvincible();
    }
    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.SetInvincible(false);
        PlayerController.Instance.SetPowerUpText("");
    }



}
