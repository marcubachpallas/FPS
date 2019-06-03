using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    public static event Action<int> AmmoEvent;

    public float shootRate, reloadtime, bulletDestroyTime;
    public int bulletsXshot, ammo; //bulltesXshot number of bullets until release buttn (pistol = 1, rilfe = ammo)
    

    bool reloading = false, endShootRate = false;
    int shootCount = 0, currentAmmo;

    GameObject[] ammoPool;

    public GameObject bulletPositioner, ammoPrefab, bulletsHolder;

    Ray ray;

    float timer;

    void Start()
    {
        currentAmmo = ammo;

        int poolSize = Mathf.RoundToInt(bulletDestroyTime / shootRate) + 1;
        if (poolSize >= ammo) poolSize = ammo;

        //pool bullets
        ammoPool = new GameObject[poolSize];

        for(int i = 0; i < poolSize; i++)
        {
            ammoPool[i] = Instantiate(ammoPrefab);
            ammoPool[i].transform.parent = bulletsHolder.transform;
            ammoPool[i].transform.localPosition = Vector3.zero;
            ammoPool[i].transform.localRotation = Quaternion.Euler(Vector3.zero);
            ammoPool[i].SetActive(false);
        }

        AmmoEvent(currentAmmo);
    }

    private void OnEnable()
    {
        if (AmmoEvent != null)
            AmmoEvent(currentAmmo);
        if (currentAmmo <= 0)
        {
            reloading = true;
            StartCoroutine(Reload());
        }
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.parent.LookAt(ray.direction*100);
    }

    public void Shoot()
    {
        if (reloading || endShootRate) return;
        if (timer < shootRate) return;

        timer = 0;

        //pool bullets until enought ballet for fill bulletsXshot or get out of ammo
        for (int i = 0; i < ammoPool.Length; i++)
        {
            if (!ammoPool[i].activeInHierarchy)
            {
                shootCount++;
                currentAmmo--;

                AmmoEvent(currentAmmo);
                ammoPool[i].transform.position = bulletPositioner.transform.position;
                ammoPool[i].SetActive(true);

                Debug.DrawRay(bulletPositioner.transform.position, ray.direction * 50, Color.red, 1);

                ammoPool[i].GetComponent<Bullet>().direction = ray.direction *50;
                break;
            }
        }


        if (shootCount == bulletsXshot)
            endShootRate = true;
        if (currentAmmo <= 0)
        {
            reloading = true;
            StartCoroutine(Reload());
        }
        //then reload
    }

    public void StopShoot()
    {
        endShootRate = false;
        shootCount = 0;
        
    }

    public IEnumerator Reload()
    {
        Debug.Log("reloading");
        reloading = true;

        yield return new WaitForSeconds(reloadtime);

        reloading = false;
        endShootRate = false;
        shootCount = 0;
        currentAmmo = ammo;
        AmmoEvent(currentAmmo);
        Debug.Log("End reload");
        yield return true;
    }
}
