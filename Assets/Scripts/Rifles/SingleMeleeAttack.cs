using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class SingleMeleeAttack : MonoBehaviour
{
     public int SingleMeleeVal;
    public Animator anim;
    public PlayerScript playerScript;


    public Transform attackArea;
    public float giveDamage = 20f;
    public float attackRadius;
    public LayerMask knightLayer;

    public GameObject goreEffect;
    public AudioClip shootingSound;

    public AudioSource audioSource;


    private void Update()
    {

        SingleMeleeModes();
    }

    void SingleMeleeModes()
    {
        if(CrossPlatformInputManager.GetButtonDown("Attack"))
        {
            SingleMeleeVal = Random.Range(1, 5);

            if(SingleMeleeVal == 1)
            {

             
              Attck();

              StartCoroutine(SingleAttack1());
            }

            if(SingleMeleeVal == 2)
            {

              
              Attck();

              StartCoroutine(SingleAttack2());
            }

             if(SingleMeleeVal == 3)
            {

              
              attackRadius = 0.7f;
              Attck();

              StartCoroutine(SingleAttack3());
            }

            if(SingleMeleeVal == 4)
            {

              
              Attck();

              StartCoroutine(SingleAttack4());
            }

            if(SingleMeleeVal == 5)
            {

              
              Attck();

              StartCoroutine(SingleAttack5());
            }
        }
    }

    void Attck()
    {
      Collider[] hitKnight = Physics.OverlapSphere(attackArea.position, attackRadius, knightLayer);
      audioSource.PlayOneShot(shootingSound);

      foreach(Collider knight in hitKnight)
      {
        // KnightAI knightAI = knight.GetComponent<KnightAI>();

        // if(!knightAI != null)
        // {
        //   knightAI.TakeDamage(giveDamage);
        // }

        KnightAI knightAI = knight.GetComponent<KnightAI>();
    KnightAI2 knightAI2 = knight.GetComponent<KnightAI2>();
    BossAI bossAI = knight.GetComponent<BossAI>();

    if (knightAI != null)
    {
        knightAI.TakeDamage(giveDamage);
        GameObject goreEffectGo = Instantiate(goreEffect, knight.transform.position, Quaternion.LookRotation(knight.transform.forward));
    }

    if (knightAI2 != null)
    {
        knightAI2.TakeDamage(giveDamage);
       GameObject goreEffectGo = Instantiate(goreEffect, knight.transform.position, Quaternion.LookRotation(knight.transform.forward));

    }
    if (bossAI != null)
    {
        bossAI.TakeDamage(giveDamage);
       GameObject goreEffectGo = Instantiate(goreEffect, knight.transform.position, Quaternion.LookRotation(knight.transform.forward));

    }
      }
    }

    private void OnDrawGizmosSelected()
    {
      if(attackArea == null)
         return;
      

      Gizmos.DrawWireSphere(attackArea.position, attackRadius);
    }

    IEnumerator SingleAttack1()
    {
      anim.SetBool("SingleAttack1", true);
      playerScript.movementSpeed = 0f;
      anim.SetFloat("movementValue", 0f);
      yield return new WaitForSeconds(0.5f);
      anim.SetBool("SingleAttack1", false);
      playerScript.movementSpeed = 5f;
      anim.SetFloat("movementValue", 0f);

    }

    IEnumerator SingleAttack2()
    {
      anim.SetBool("SingleAttack2", true);
      playerScript.movementSpeed = 0f;
      anim.SetFloat("movementValue", 0f);
      yield return new WaitForSeconds(0.5f);
      anim.SetBool("SingleAttack2", false);
      playerScript.movementSpeed = 5f;
      anim.SetFloat("movementValue", 0f);

    }

    IEnumerator SingleAttack3()
    {
      anim.SetBool("SingleAttack3", true);
      playerScript.movementSpeed = 0f;
      anim.SetFloat("movementValue", 0f);
      yield return new WaitForSeconds(0.5f);
      anim.SetBool("SingleAttack3", false);
      playerScript.movementSpeed = 5f;
      anim.SetFloat("movementValue", 0f);

    }

    IEnumerator SingleAttack4()
    {
      anim.SetBool("SingleAttack4", true);
      playerScript.movementSpeed = 0f;
      anim.SetFloat("movementValue", 0f);
      yield return new WaitForSeconds(0.5f);
      anim.SetBool("SingleAttack4", false);
      playerScript.movementSpeed = 5f;
      anim.SetFloat("movementValue", 0f);

    }

    IEnumerator SingleAttack5()
    {
      anim.SetBool("SingleAttack5", true);
      playerScript.movementSpeed = 0f;
      anim.SetFloat("movementValue", 0f);
      yield return new WaitForSeconds(0.5f);
      anim.SetBool("SingleAttack5", false);
      playerScript.movementSpeed = 5f;
      anim.SetFloat("movementValue", 0f);

    }

    
}
