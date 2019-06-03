using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour, IKilleable, IDamageable<int>
{
    public int life = 10;

    public void Damage(int dmgTaken)
    {
        life += dmgTaken;
        if (life < 0) Kill();
    }

    public void Kill()
    {
        Destroy(this.gameObject);
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
            Damage(-1);
    }
}
