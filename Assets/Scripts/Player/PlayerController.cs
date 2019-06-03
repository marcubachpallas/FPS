using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController cc;

    public float mSpeed = 5, rSpeed = 5, jSpeed = 10;
    public float gravity = 20;

    private Vector3 mDir = Vector3.zero;
    private Vector3 rotation = Vector3.zero;

    public GameObject[] weapons;
    int currentWeapon = -1;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        for(int i = 0; i < weapons.Length; i++)
        {
            if (currentWeapon != -1 && weapons[i].activeInHierarchy)
                weapons[i].SetActive(false);

            else if (weapons[i].activeInHierarchy)
                currentWeapon = i;
        }

        if (currentWeapon == -1 && weapons.Length > 0)
            weapons[0].SetActive(true);
    }

    void Update()
    {
        //if is in flor calculate movment and enalbe jump
        if (cc.isGrounded)
        {
            mDir = transform.forward * Input.GetAxis("Vertical");
            mDir *= mSpeed;

            if (Input.GetButton("Jump"))
            {
                mDir.y = jSpeed;
            }
        }

        mDir.y -= gravity * Time.deltaTime;

        //Set rotation and movment
        transform.Rotate(transform.up * -Input.GetAxis("Horizontal") * rSpeed * Time.deltaTime);

        cc.Move(mDir * Time.deltaTime);

        //Weapon managment
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            SwapWeapon(0);
        }else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            SwapWeapon(1);
        }else if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(weapons[currentWeapon].GetComponent<Weapon>().Reload());
        }

        //Shoot currWeapon
        if (Input.GetMouseButton(0))
            weapons[currentWeapon].GetComponent<Weapon>().Shoot();
        else if (Input.GetMouseButtonUp(0))
            weapons[currentWeapon].GetComponent<Weapon>().StopShoot();

    }

    void SwapWeapon(int selected)
    {
        weapons[currentWeapon].SetActive(false);
        weapons[selected].SetActive(true);
        currentWeapon = selected;
    }
}
