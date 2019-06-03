using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    ///<summary>
    /// shot
    /// </summary>
    void Shoot();

    /// <summary>
    /// has the player sotp shooting?
    /// </summary>
    void StopShoot();

    ///<summary>
    /// reload weapon
    /// </summary>
    IEnumerator Reload();
}

// <-----------------Enemys-------------->
public interface IKilleable
{
    void Kill();
}

public interface IDamageable<T>
{
    void Damage(T dmgTaken);
}