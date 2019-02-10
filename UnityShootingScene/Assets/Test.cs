using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public float SpeedForward;
    public float SpeedSideward;
    public bool Shoot;
    public bool WeaponAK;
    public bool WeaponGun;

    public GameObject AKBack;
    public GameObject AKHand;
    public GameObject PistolBack;
    public GameObject PistolHand;

    protected Animator Animator;

    // Start is called before the first frame update
    void Awake()
    {
        Animator = GetComponent<Animator>();
        AKBack.SetActive(true);
        AKHand.SetActive(false);
        PistolBack.SetActive(true);
        PistolHand.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Animator.SetFloat("SpeedForward", SpeedForward);
        Animator.SetFloat("SpeedSideward", SpeedSideward);

        if (Shoot == true)
        {
            Shoot = false;
            Animator.SetTrigger("Shoot");
        }

        if (WeaponAK == true)
        {
            WeaponAK = false;
            Animator.SetBool("AK", true);
            Animator.SetBool("Pistol", false);
        }

        if (WeaponGun == true)
        {
            WeaponGun = false;
            Animator.SetBool("AK", false);
            Animator.SetBool("Pistol", true);
        }
    }
}
