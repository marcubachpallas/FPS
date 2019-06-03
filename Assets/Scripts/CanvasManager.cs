using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI currentBulletsTMP, restAmmoTMP;
    int currBullets = 0;
    private void OnEnable()
    {
        Weapon.AmmoEvent += Weapon_AmmoEvent;
        Bullet.ActiveBalls += Bullet_ActiveBalls;
    }

    private void Bullet_ActiveBalls(int bullets)
    {
        currBullets += bullets;
        currentBulletsTMP.text = currBullets.ToString();
    }

    private void Weapon_AmmoEvent(int ammo)
    {
        restAmmoTMP.text = ammo.ToString();
    }
}
