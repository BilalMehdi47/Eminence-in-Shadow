using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GrenadeThrowover : MonoBehaviour
{
    public float throwForce = 10f;
    public GameObject grenadePrefab;
    public Transform grenadeArea;
    public Animator anim;
    public GameManager gameManager;


    private void Update()
    {
        if(CrossPlatformInputManager.GetButtonDown("Attack") && gameManager.numberofGrenades > 0)
        {
            StartCoroutine(GrenadeAnim());
            gameManager.numberofGrenades -= 1;
        }
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, grenadeArea.transform.position, grenadeArea.transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(grenadeArea.transform.forward * throwForce, ForceMode.VelocityChange);
    }

    IEnumerator GrenadeAnim()
    {
        anim.SetBool("GrenadeInAir", true);
        yield return new WaitForSeconds(0.5f);

        ThrowGrenade();
        yield return new WaitForSeconds(1f);
        anim.SetBool("GrenadeInAir", false);
    }
}
