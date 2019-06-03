using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static event Action<int> ActiveBalls;

    Coroutine coroutine;
    Rigidbody rb;
    public Vector3 direction;

    public float bulletDestroyTime = 0.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        coroutine = StartCoroutine(Desactivate(0.5f));
        ActiveBalls(1);
    }

    private void OnDisable()
    {
        ActiveBalls(-1);
    }

    private void FixedUpdate()
    {
        transform.position += direction * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        StopCoroutine(coroutine);

        StartCoroutine(Desactivate(0));
    }

    IEnumerator Desactivate(float time)
    {
        yield return new WaitForSeconds(time);
        rb.AddForce(Vector3.zero,ForceMode.VelocityChange);
        this.gameObject.SetActive(false);
    }
}
