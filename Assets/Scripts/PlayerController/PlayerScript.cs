using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

     [Header("Player Health And Energy")]
    private float playerHealth = 300f;
     public float presentHealth;
     public HealthBar healthbar;
     public EnergyBar energybar;
     public GameObject DamageIndicator;
     private float playerEnergy = 100f;
     public float presentEnergy;


    [Header("Player Movement")]
    public float movementSpeed = 5f;
    public MainCamera MCC;
    public float rotSpeed = 450f;
    public EnvironmentChecker environmentChecker;
    Quaternion requiredRotation;

    bool playerControl = true;


    [Header("Player Animator")]
    public Animator animator;


    [Header("Player Collision & Gravity")]
    public CharacterController CC;
    public float surfaceCheckRadius = 0.1f;
    public Vector3 surfaceCheckOffset;
    public LayerMask surfaceLayer;
    bool onSurface;
    public bool playerOnLedge{ get; set; }
    public LedgeInfo LedgeInfo{ get; set; }
    [SerializeField] public float fallingSpeed;
    [SerializeField] Vector3 moveDir;
    [SerializeField] Vector3 requiredMoveDir;

    Vector3 velocity;

        private void Awake()
        {
           presentHealth = playerHealth;
           presentEnergy = playerEnergy;
           healthbar.GiveFullHealth(presentHealth);
           energybar.GiveFullenergy(presentEnergy);
        }


    private void Update()
  {

     if (presentEnergy <= 0)
    {
        movementSpeed = 2f;

        if (!SimpleInput.GetButton("Horizontal") || !SimpleInput.GetButton("Vertical"))
        {
            animator.SetFloat("movementValue", 0f);
        }

        if (SimpleInput.GetButton("Horizontal") || SimpleInput.GetButton("Vertical"))
        {
            animator.SetFloat("movementValue", 0.5f);
            StartCoroutine(setEnergy());
        }
    }

    if(presentEnergy >= 1)
    {
      movementSpeed = 5f;
    }

    if (animator.GetFloat("movementValue") >= 0.999)
    {
        playerEnergyDecrease(0.02f);
    }

    if(!playerControl)
    return;

    velocity = Vector3.zero;

    if(onSurface) 
    {

   fallingSpeed = -0.5f;
   velocity = moveDir * movementSpeed;

   playerOnLedge = environmentChecker.CheckLedge(moveDir, out LedgeInfo ledgeInfo);
   
   if(playerOnLedge)
   {
    LedgeInfo = ledgeInfo;
    playerLedgeMovement();
      Debug.Log("Player on Ledge");
   }

   animator.SetFloat("movementValue", velocity.magnitude / movementSpeed, 0.2f, Time.deltaTime);

    }

    else 
    {

          fallingSpeed += Physics.gravity.y * Time.deltaTime;
          velocity = transform.forward * movementSpeed / 2;

    }

    
    velocity.y = fallingSpeed;

    PlayerMovement();
    SurfaceCheck();
    animator.SetBool("onSurface", onSurface);
    Debug.Log("Player on surface" + onSurface);

}

  void PlayerMovement()
  {
    float horizontal = SimpleInput.GetAxis("Horizontal");
    float vertical = SimpleInput.GetAxis("Vertical");

    float movementAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

    var movementInput = (new Vector3(horizontal, 0, vertical)).normalized;

    requiredMoveDir = MCC.flatRotation * movementInput;

    CC.Move(velocity * Time.deltaTime);

    if(movementAmount > 0 && moveDir.magnitude > 0.2f)
    {

    requiredRotation = Quaternion.LookRotation(moveDir);
    
    }

    moveDir = requiredMoveDir;

     transform.rotation = Quaternion.RotateTowards(transform.rotation, requiredRotation, rotSpeed * Time.deltaTime);

     
  }
  
  void SurfaceCheck()
  {
   onSurface = Physics.CheckSphere(transform.TransformPoint(surfaceCheckOffset), surfaceCheckRadius, surfaceLayer);
  }

  void playerLedgeMovement()
  {
    float angle = Vector3.Angle(LedgeInfo.surfaceHit.normal, requiredMoveDir);

    if(angle < 90)
    {
      velocity = Vector3.zero;
      moveDir = Vector3.zero;
    }
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawSphere(transform.TransformPoint(surfaceCheckOffset), surfaceCheckRadius);

  }

  public void SetControl(bool hasControl)
  {
    
    this.playerControl = hasControl;
    CC.enabled = hasControl;

    if(!hasControl)
    {
      animator.SetFloat("movementValue", 0f);
      requiredRotation = transform.rotation;
    }
  }

  public bool HasPlayerControl
  {
    get => playerControl;
    set => playerControl = value;
  }

  public void playerHitDamage(float takeDamage)
{
    presentHealth -= takeDamage;
    healthbar.SetHealth(presentHealth);

    StartCoroutine(showDamage());

    if (presentHealth <= 0)
    {
        PlayerDie();
    }
}

private void PlayerDie()
{
    Cursor.lockState = CursorLockMode.None;
    Object.Destroy(gameObject, 1.0f);
}

public void playerEnergyDecrease(float energyDecrease)
{
  presentEnergy -= energyDecrease;
  energybar.SetEnergy(presentEnergy);
}

IEnumerator setEnergy()
{
    presentEnergy = 0f;
    yield return new WaitForSeconds(5f);
    energybar.GiveFullenergy(presentEnergy);
    presentEnergy = 100f;
}

IEnumerator showDamage()
{
    DamageIndicator.SetActive(true);
    yield return new WaitForSeconds(0.9f);
    DamageIndicator.SetActive(false);
}

}
