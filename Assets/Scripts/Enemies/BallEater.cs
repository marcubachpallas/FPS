using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEater : MonoBehaviour, IDamageable<float>
{
    public float life = 1;

    public void Damage(float dmgTaken)
    {
        Vector3 tempScale = transform.localScale;
        this.transform.localScale = new Vector3(tempScale.x + dmgTaken, tempScale.y + dmgTaken, tempScale.z + dmgTaken);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
            Damage(0.1f);
    }
}
