using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAI2 : MonoBehaviour
{
    [Header("Character Info")]
    public float movingSpeed;
    public float runningSpeed;
    public float CurrentmovingSpeed;
    
    public float maxHealth = 150f;
    public float currenthealth;

   

 [Header("Knight AI")]
public GameObject playerBody;
public LayerMask playerLayer;
public float visionRadius;
public float attackRadius;
public bool playerInvisionRadius;
public bool playerInattackRadius;


[Header("Knight Attack Var")]
public int SingleMeleeVal;
public Transform attackArea;
public float giveDamage;
public float attackingRadius;
bool previouslyAttack;
public float timebtwAttack;
public Animator anim;


private void Start()
{
    CurrentmovingSpeed = movingSpeed;
    currenthealth = maxHealth;
    playerBody = GameObject.Find("Player");
}

    private void Update()
    {

      playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);
      playerInattackRadius = Physics.CheckSphere(transform.position, attackRadius, playerLayer);

      if(!playerInvisionRadius && !playerInattackRadius)
      {
        
        Idle();
       }
    if(playerInvisionRadius && !playerInattackRadius)
      {
         anim.SetBool("Idle", false);
        ChasePlayer();
      }

      if(playerInvisionRadius && playerInattackRadius)
      {
            anim.SetBool("Idle", true);
            SingleMeleeModes();


      }

    }

    public void Idle()
    {

        anim.SetBool("Run", false);
       
     }
//  void ChasePlayer()
// {
//   CurrentmovingSpeed = runningSpeed;
//   transform.position += transform.forward * CurrentmovingSpeed * Time.deltaTime;
//    transform.LookAt(playerBody.transform);

    
//         anim.SetBool("Attack", false);
//         anim.SetBool("Run", true);
// }
void ChasePlayer()
{
    Vector3 separationForce = Vector3.zero;
    float separationRadius = 2f;

  // Define the layer mask for the knights
    int knightLayer = LayerMask.NameToLayer("Knight");
    int layerMask = 1 << knightLayer;

  Collider[] nearbyKnights = Physics.OverlapSphere(transform.position, separationRadius, layerMask); // Check within 2 meters for other knights



    foreach (var knight in nearbyKnights)
    {
        if (knight.gameObject != gameObject) // Ensure not to count the knight itself
        {
            Vector3 difference = transform.position - knight.transform.position;
            if (difference.magnitude < 1f) // Only apply force if very close to each other
            {
                separationForce += difference.normalized;
            }
        }
    }

    CurrentmovingSpeed = runningSpeed;
    Vector3 moveDirection = (playerBody.transform.position - transform.position).normalized + separationForce.normalized * 0.1f; // Adding a small force for separation
    transform.position += moveDirection * CurrentmovingSpeed * Time.deltaTime;
    transform.LookAt(playerBody.transform);

    anim.SetBool("Attack", false);
    anim.SetBool("Run", true);
}

void SingleMeleeModes()
{
    if (!previouslyAttack)
    {
        SingleMeleeVal = Random.Range(1, 5);

        if (SingleMeleeVal == 1)
        {
           
            Attack();
            // Animation
            StartCoroutine(Attack1());
        }

        if (SingleMeleeVal == 2)
        {
            // Attack
           
            Attack();
            StartCoroutine(Attack2());
        }

        if (SingleMeleeVal == 3)
        {
            // Attack
            
            Attack();
            StartCoroutine(Attack3());
        }

        if (SingleMeleeVal == 4)
        {
            // Attack
            
            Attack();
            StartCoroutine(Attack4());
        }
    }
}
void Attack()
{
    Collider[] hitPlayer = Physics.OverlapSphere(attackArea.position, attackingRadius, playerLayer);

    foreach (Collider player in hitPlayer)
    {
        PlayerScript playerScript = player.GetComponent<PlayerScript>();

        if (playerScript != null)
        {
            playerScript.playerHitDamage(giveDamage);
        }
    }

    previouslyAttack = true;
    Invoke(nameof(ActiveAttack), timebtwAttack);
}

private void OnDrawGizmosSelected()
{
  if(attackArea == null)
  return;

  Gizmos.DrawWireSphere(attackArea.position, attackingRadius);
}
private void ActiveAttack()
{
    previouslyAttack = false;
}

IEnumerator Attack1()
{
    anim.SetBool("Attack1", true);
    movingSpeed = 0f;
    runningSpeed = 0f;
    yield return new WaitForSeconds(0.2f);
    anim.SetBool("Attack1", false);
    movingSpeed = 1f;
    runningSpeed = 3f;
}

IEnumerator Attack2()
{
    anim.SetBool("Attack2", true);
    movingSpeed = 0f;
    runningSpeed = 0f;
    yield return new WaitForSeconds(0.2f);
    anim.SetBool("Attack2", false);
    movingSpeed = 1f;
    runningSpeed = 3f;
}

IEnumerator Attack3()
{
    anim.SetBool("Attack3", true);
    movingSpeed = 0f;
    runningSpeed = 0f;
    yield return new WaitForSeconds(0.2f);
    anim.SetBool("Attack3", false);
    movingSpeed = 1f;
    runningSpeed = 3f;
}

IEnumerator Attack4()
{
    anim.SetBool("Attack4", true);
    movingSpeed = 0f;
    runningSpeed = 0f;
    yield return new WaitForSeconds(0.2f);
    anim.SetBool("Attack4", false);
    movingSpeed = 1f;
    runningSpeed = 3f;
}

public void TakeDamage(float amount)
{

    currenthealth -= amount;
    //anim.SetTrigger("GetHit");

    if(currenthealth <= 0f)
    {
        Missions.instance.Mission3 = true;
        Die();
    }
}
void Die()
  {
     anim.SetBool("isDead", true);
      this.enabled = false;
      GetComponent<Collider>().enabled = false;
      Destroy(gameObject, 2f);
  }

// void Die()
// {
//     Debug.Log("Die method called"); // Debug log for method call
//     anim.SetBool("isDead", true); // Trigger the isDead animation
//     this.enabled = false; // Disable this script
//     GetComponent<Collider>().enabled = false; // Disable the collider
//     StartCoroutine(WaitForDieAnimation());
// }

// private IEnumerator WaitForDieAnimation()
// {
//     // Wait until the die animation starts playing
//     Debug.Log("Waiting for die animation to start"); // Debug log for coroutine start
//     while (!anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
//     {
//         Debug.Log("Current animation state: " + anim.GetCurrentAnimatorStateInfo(0).IsName("Die")); // Debug log for current animation state
//         yield return null;
//     }

//     // Log animation state details
//     AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
//     Debug.Log("Entered Die state, length: " + stateInfo.length);

//     // Wait for the die animation to complete
//     float elapsedTime = 0f;
//     while (elapsedTime < stateInfo.length)
//     {
//         elapsedTime += Time.deltaTime;
//         Debug.Log("Die animation playing, time: " + elapsedTime);
//         yield return null;
//     }

//     Debug.Log("Die animation complete, destroying game object"); // Debug log for animation completion

//     // Destroy the game object after the animation has played
//     Destroy(gameObject);
// }




}
