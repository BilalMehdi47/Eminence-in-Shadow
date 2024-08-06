using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float grenadeTimer = 5f;
    float countDown;
    public float radius = 5f;


    bool hasExploded = false;
    public float giveDamage = 120f;
    public GameObject explosionEffect;
    //public AudioClip shootingSound;
   // public AudioSource audioSource;


   private void Start()
    {
        countDown = grenadeTimer;
    }

    private void Update()
    {
        countDown -= Time.deltaTime;

        if(countDown <= 0f && !hasExploded)
        {

            Explode();
            hasExploded =true;

        }
    }  
    void Explode()
    {

        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
      //  audioSource.PlayOneShot(shootingSound);

        foreach (Collider nearbyObject in colliders)
        {
            Object obj = nearbyObject.GetComponent<Object>();

            if(obj != null)
            {
                obj.objectHitDamage(giveDamage);
            }

            KnightAI knightAI = nearbyObject.GetComponent<KnightAI>();
    KnightAI2 knightAI2 = nearbyObject.GetComponent<KnightAI2>();
     BossAI bossAI = nearbyObject.GetComponent<BossAI>();

    if (knightAI != null)
    {
        knightAI.TakeDamage(giveDamage);
    }

    if (knightAI2 != null)
    {
        knightAI2.TakeDamage(giveDamage);
    }
    if (bossAI != null)
    {
        bossAI.TakeDamage(giveDamage);
    }

            
        }
          Destroy(gameObject);
       
    }
}
