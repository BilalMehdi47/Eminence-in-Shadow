using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Rifle : MonoBehaviour
{
    [Header("Rifle Things")]
    public Transform shootingArea;
    public float giveDamage = 30f;
    public float shootingRange = 100f;
    public Animator animator;
    public bool isMoving;
    public PlayerScript playerScript;
    public GameObject crosshair;
    
    [Header("Rifle Ammunition and reloading")]
    private int maximumAmmunition = 3;
    public int presentAmmunition;
    public int mag;
    public float reloadingTime;
    private bool setReloading;

    [Header("Muzzle Spark")]

    public ParticleSystem muzzleSpark;
    public GameObject goreEffect;
    public AudioClip shootingSound;
    public AudioClip reloadingSound;
    public AudioSource audioSource;


    private bool isAiming = false; 

    private void Start()
    {
        presentAmmunition = maximumAmmunition;
    }

    private void Update()
    {
        if(animator.GetFloat("movementValue") > 0.001f)
        {
            isMoving = true;
        }

        else if(animator.GetFloat("movementValue") < 0.0999999f)
        {
            isMoving = false;
        }

        if(setReloading)
        return;

        if(presentAmmunition <= 0 && mag > 0)
        {
            StartCoroutine(Reload());
            return;

        }
        if(CrossPlatformInputManager.GetButtonDown("Attack") && isMoving == false)
        {
            animator.SetBool("RifleActive", true);
            animator.SetBool("Shooting", true);
            Shoot();
        }
        else if(CrossPlatformInputManager.GetButtonDown("Attack"))
        {
            animator.SetBool("Shooting", false);
        }

    //     if(CrossPlatformInputManager.GetButtonDown("Cross"))
    //     {
    //         animator.SetBool("RifleAim", true);
    //         crosshair.SetActive(true);
    //     }
    //    else if(!CrossPlatformInputManager.GetButtonDown("Cross"))
    //     {
    //         animator.SetBool("RifleAim", false);
    //         crosshair.SetActive(false);
    //     }

    if(CrossPlatformInputManager.GetButtonDown("Cross"))
{
    isAiming = !isAiming;  // Toggle the aiming state
    animator.SetBool("RifleAim", isAiming);
    crosshair.SetActive(isAiming);
}

    }

    void Shoot()
    {
        if (mag <= 0)
        {
            // show out UI
            return;
        }
        presentAmmunition--;

        if (presentAmmunition == 0)
        {
            mag--;
        }

        muzzleSpark.Play();
        audioSource.PlayOneShot(shootingSound);


        RaycastHit hitInfo;

        if (Physics.Raycast(shootingArea.position, shootingArea.forward, out hitInfo, shootingRange))
        {
            // KnightAI knightAI = hitInfo.transform.GetComponent<KnightAI>();

            // if (knightAI != null)
            // {
            //     knightAI.TakeDamage(giveDamage);
            // }
            KnightAI knightAI = hitInfo.transform.GetComponent<KnightAI>();
    KnightAI2 knightAI2 = hitInfo.transform.GetComponent<KnightAI2>();
    BossAI bossAI = hitInfo.transform.GetComponent<BossAI>();

    // if (knightAI != null)
    // {
    //     knightAI.TakeDamage(giveDamage);
    //    // GameObject goreEffectGo = Instantiate(goreEffectGo, hitInfo.point, Quanternion.LookRotation(hitInfo.normal));
    //        }

    // if (knightAI2 != null)
    // {
    //     knightAI2.TakeDamage(giveDamage);
    // }

    if (knightAI != null)
    {
        knightAI.TakeDamage(giveDamage);
        GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
    }

    if (knightAI2 != null)
    {
        knightAI2.TakeDamage(giveDamage);
       GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

    }
    if (bossAI != null)
    {
        bossAI.TakeDamage(giveDamage);
       GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

    }
        }
    }

    IEnumerator Reload()
    {
        setReloading = true;
        animator.SetFloat("movementValue", 0f);
        playerScript.movementSpeed = 0f;
        animator.SetBool("ReloadRifle", true);
        audioSource.PlayOneShot(reloadingSound);

        yield return new WaitForSeconds(reloadingTime);

        animator.SetBool("ReloadRifle", false);
        presentAmmunition = maximumAmmunition;
        setReloading = false;
        animator.SetFloat("movementValue", 0f);
        playerScript.movementSpeed = 5f;
    }
}
