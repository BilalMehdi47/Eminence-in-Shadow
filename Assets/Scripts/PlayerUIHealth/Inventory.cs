using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Inventory : MonoBehaviour
{
[Header("Weapon 1 Slot")]
public GameObject Weapon1;
public bool isweapon1Picked = false;
public bool isweapon1Active = false;
public SingleMeleeAttack SMAS;

public bool fistFightMode = false;

[Header("Weapon 2 Slot")]
public GameObject Weapon2;
public bool isweapon2Picked = false;
public bool isweapon2Active = false;
public Rifle rifle;

[Header("Weapon 3 Slot")]
public GameObject Weapon3;
public bool isweapon3Picked = false;
public bool isweapon3Active = false;
public Bazooka bazooka;

[Header("Weapon 4 Slot")]
public GameObject Weapon4;
public bool isweapon4Picked = false;
public bool isweapon4Active = false;
public GrenadeThrowover grenadethrower;

[Header("Scripts")]
public FistFight fistFight;
public PlayerScript playerScript;
public GameManager GM;
public Animator anim;

[Header("Current Weapons")]
public GameObject NoWeapon;
public GameObject CurrentWeapon1;
public GameObject CurrentWeapon2;
public GameObject CurrentWeapon3;
public GameObject CurrentWeapon4;


private void Update()
{
    if (isweapon1Active == false && isweapon2Active == false && isweapon3Active == false && isweapon4Active == false && fistFightMode == false)
    {
        
        NoWeapon.SetActive(true);
    }


    if (CrossPlatformInputManager.GetButtonDown("Attack") && isweapon1Active == false && isweapon2Active == false && isweapon3Active == false && isweapon4Active == false && fistFightMode == false)
    {
        fistFightMode = true;
        isRifleActive();
    }

    if (CrossPlatformInputManager.GetButtonDown("1") && isweapon1Active == false && isweapon2Active == false && isweapon3Active == false && isweapon4Active == false && isweapon1Picked == true)
    {
        isweapon1Active = true;
        isRifleActive();
        CurrentWeapon1.SetActive(true);
        NoWeapon.SetActive(false);
    }
    else if (CrossPlatformInputManager.GetButtonDown("1") && isweapon1Active == true)
    {
        isweapon1Active = false;
        isRifleActive();
        CurrentWeapon1.SetActive(false);
    }

    if (CrossPlatformInputManager.GetButtonDown("2") && isweapon1Active == false && isweapon2Active == false && isweapon3Active == false && isweapon4Active == false && isweapon2Picked == true)
    {
        isweapon2Active = true;
        isRifleActive();
        CurrentWeapon2.SetActive(true);
        NoWeapon.SetActive(false);
    }
    else if (CrossPlatformInputManager.GetButtonDown("2") && isweapon2Active == true)
    {
        isweapon2Active = false;
        isRifleActive();
        CurrentWeapon2.SetActive(false);
    }

     if (CrossPlatformInputManager.GetButtonDown("3") && isweapon1Active == false && isweapon2Active == false && isweapon3Active == false && isweapon4Active == false && isweapon3Picked == true)
    {
        isweapon3Active = true;
        isRifleActive();
        CurrentWeapon3.SetActive(true);
        NoWeapon.SetActive(false);
    }
    else if (CrossPlatformInputManager.GetButtonDown("3") && isweapon3Active == true)
    {
        isweapon3Active = false;
        isRifleActive();
        CurrentWeapon3.SetActive(false);
    }

     if (CrossPlatformInputManager.GetButtonDown("4") && isweapon1Active == false && isweapon2Active == false && isweapon3Active == false && isweapon4Active == false && isweapon4Picked == true)
    {
        isweapon4Active = true;
        isRifleActive();
        CurrentWeapon4.SetActive(true);
        NoWeapon.SetActive(false);
    }
    else if (CrossPlatformInputManager.GetButtonDown("4") && isweapon4Active == true)
    {
        isweapon4Active = false;
        isRifleActive();
        CurrentWeapon4.SetActive(false);
    }

    if (GM.numberofGrenades <= 0 && isweapon4Active == true)
{
    Weapon4.SetActive(false);
    isweapon4Active = false;
    CurrentWeapon4.SetActive(false);
    isRifleActive();
}


    if (CrossPlatformInputManager.GetButtonDown("5") && !isweapon1Active && !isweapon2Active && !isweapon3Active && !isweapon4Active && GM.numberofHealth > 0 && playerScript.presentHealth < 80)
{
    StartCoroutine(IncreaseHealth());
}

if (CrossPlatformInputManager.GetButtonDown("6") && !isweapon1Active && !isweapon2Active && !isweapon3Active && !isweapon4Active && GM.numberofEnergy > 0 && playerScript.presentEnergy < 80)
{
    StartCoroutine(IncreaseEnergy());
}

}

void isRifleActive()
{
    if (fistFightMode == true)
    {
        fistFight.GetComponent<FistFight>().enabled = true;
    }

    if (isweapon1Active == true)
    {
        StartCoroutine(Weapon1GO());
        SMAS.GetComponent<SingleMeleeAttack>().enabled = true;
        anim.SetBool("SingleHandAttackActive", true);
    }

    if (isweapon1Active == false)
    {
        StartCoroutine(Weapon1GO());
        SMAS.GetComponent<SingleMeleeAttack>().enabled = false;
        anim.SetBool("SingleHandAttackActive", false);
    }

    if (isweapon2Active == true)
    {
        StartCoroutine(Weapon2GO());
        rifle.GetComponent<Rifle>().enabled = true;
        anim.SetBool("RifleActive", true);
    }

    if (isweapon2Active == false)
    {
        StartCoroutine(Weapon2GO());
        rifle.GetComponent<Rifle>().enabled = false;
        anim.SetBool("RifleActive", false);
    }

    if (isweapon3Active == true)
{
    StartCoroutine(Weapon3GO());
    bazooka.GetComponent<Bazooka>().enabled = true;
    anim.SetBool("BazookaActive", true);
}

if (isweapon3Active == false)
{
    StartCoroutine(Weapon3GO());
    bazooka.GetComponent<Bazooka>().enabled = false;
    anim.SetBool("BazookaActive", false);
}

if (isweapon4Active == true)
{
    StartCoroutine(Weapon4GO());
    grenadethrower.GetComponent<GrenadeThrowover>().enabled = true;
}

if (isweapon4Active == false)
{
    StartCoroutine(Weapon4GO());
    grenadethrower.GetComponent<GrenadeThrowover>().enabled = false;
}

}

IEnumerator Weapon1GO()
{
    if (!isweapon1Active)
    {
        Weapon1.SetActive(false);
    }

    yield return new WaitForSeconds(0.1f);

    if (isweapon1Active)
    {
        Weapon1.SetActive(true);
    }
}

IEnumerator Weapon2GO()
{
    if (!isweapon2Active)
    {
        Weapon2.SetActive(false);
    }

    yield return new WaitForSeconds(0.1f);

    if (isweapon2Active)
    {
        Weapon2.SetActive(true);
    }
}

IEnumerator Weapon3GO()
{
    if (!isweapon3Active)
    {
        Weapon3.SetActive(false);
    }

    yield return new WaitForSeconds(0.1f);

    if (isweapon3Active)
    {
        Weapon3.SetActive(true);
    }
}

IEnumerator Weapon4GO()
{
    if (!isweapon4Active)
    {
        Weapon4.SetActive(false);
    }

    yield return new WaitForSeconds(0.1f);

    if (isweapon4Active)
    {
        Weapon4.SetActive(true);
    }
}


IEnumerator IncreaseHealth()
{
    anim.SetBool("Drink", true);
    yield return new WaitForSeconds(1.5f);
    anim.SetBool("Drink", false);
    GM.numberofHealth -= 1;
    playerScript.presentHealth = 200f;
    playerScript.healthbar.GiveFullHealth(200f);
}

IEnumerator IncreaseEnergy()
{
    anim.SetBool("Drink", true);
    yield return new WaitForSeconds(1.5f);
    anim.SetBool("Drink", false);
    GM.numberofEnergy -= 1;
    playerScript.presentEnergy = 100f;
    playerScript.energybar.GiveFullenergy(100f);
}



}
