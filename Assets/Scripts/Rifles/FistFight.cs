using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class FistFight : MonoBehaviour
{
    public float Timer = 0f;
    public int FistFightVal;
    public Animator anim;
    public PlayerScript playerScript;
    public Inventory inventory;


    public Transform attackArea;
    public float giveDamage = 10f;
    public float attackRadius;
    public LayerMask knightLayer;
    public GameObject goreEffect;


    [SerializeField] Transform LeftHandPunch;
    [SerializeField] Transform RightHandPunch;
    [SerializeField] Transform LeftLegKick;
    public AudioClip shootingSound;

    public AudioSource audioSource;


    private void Update()
    {
        if(!CrossPlatformInputManager.GetButtonDown("Attack"))
        {
            Timer += Time.deltaTime;
        }
        else
        {
            playerScript.movementSpeed = 2f;
            anim.SetBool("FistFightActive", true);
            Timer = 0f;
        }

        if(Timer > 5f)
        {
            playerScript.movementSpeed = 5f;
            anim.SetBool("FistFightActive", false);
            inventory.fistFightMode = false;
            Timer = 0f;
            this.gameObject.GetComponent<FistFight>().enabled = false;

        }

        FistFightModes();
    }

    void FistFightModes()
    {
        if(CrossPlatformInputManager.GetButtonDown("Attack"))
        {
            FistFightVal = Random.Range(1, 7);

            if(FistFightVal == 1)
            {

              attackArea = LeftHandPunch;
              attackRadius = 0.5f;
              Attck();

              StartCoroutine(SingleFist());
            }

            if(FistFightVal == 2)
            {

              attackArea = RightHandPunch;
              attackRadius = 0.6f;
              Attck();

              StartCoroutine(DoubleFist());
            }

             if(FistFightVal == 3)
            {

              attackArea = LeftHandPunch;
              attackArea = LeftLegKick;
              attackRadius = 0.7f;
              Attck();

              StartCoroutine(FirstFistKick());
            }

            if(FistFightVal == 4)
            {

              attackArea = LeftLegKick;
              attackRadius = 0.9f;
              Attck();

              StartCoroutine(KickCombo());
            }

            if(FistFightVal == 5)
            {

              attackArea = LeftLegKick;
              attackRadius = 0.9f;
              Attck();

              StartCoroutine(LeftKick());
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

    // if (knightAI != null)
    // {
    //     knightAI.TakeDamage(giveDamage);
    // }

    // if (knightAI2 != null)
    // {
    //     knightAI2.TakeDamage(giveDamage);
    // }

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

    IEnumerator SingleFist()
    {
      anim.SetBool("SingleFist", true);
      playerScript.movementSpeed = 0f;
      anim.SetFloat("movementValue", 0f);
      yield return new WaitForSeconds(0.7f);
      anim.SetBool("SingleFist", false);
      playerScript.movementSpeed = 5f;
      anim.SetFloat("movementValue", 0f);

    }

    IEnumerator DoubleFist()
    {
      anim.SetBool("DoubleFist", true);
      playerScript.movementSpeed = 0f;
      anim.SetFloat("movementValue", 0f);
      yield return new WaitForSeconds(0.4f);
      anim.SetBool("DoubleFist", false);
      playerScript.movementSpeed = 5f;
      anim.SetFloat("movementValue", 0f);

    }

    IEnumerator FirstFistKick()
    {
      anim.SetBool("FirstFistKick", true);
      playerScript.movementSpeed = 0f;
      anim.SetFloat("movementValue", 0f);
      yield return new WaitForSeconds(0.4f);
      anim.SetBool("FirstFistKick", false);
      playerScript.movementSpeed = 5f;
      anim.SetFloat("movementValue", 0f);

    }

    IEnumerator KickCombo()
    {
      anim.SetBool("KickCombo", true);
      playerScript.movementSpeed = 0f;
      anim.SetFloat("movementValue", 0f);
      yield return new WaitForSeconds(0.4f);
      anim.SetBool("KickCombo", false);
      playerScript.movementSpeed = 5f;
      anim.SetFloat("movementValue", 0f);

    }

    IEnumerator LeftKick()
    {
      anim.SetBool("LeftKick", true);
      playerScript.movementSpeed = 0f;
      anim.SetFloat("movementValue", 0f);
      yield return new WaitForSeconds(0.4f);
      anim.SetBool("LeftKick", false);
      playerScript.movementSpeed = 5f;
      anim.SetFloat("movementValue", 0f);

    }

    
}
