using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ParkourControllerScript : MonoBehaviour
{

 public EnvironmentChecker environmentChecker;
bool playerInAction;

public Animator animator;

public PlayerScript playerScript;

[SerializeField] NewParkourAction jumpDownParkourAction;
float autoJumnpHeightLimit = 2f;


[Header("Parkour Action Area")]
public List<NewParkourAction> newParkourActions;


   private void Update()
   {
      var hitData = environmentChecker.CheckObstacle();

    if(CrossPlatformInputManager.GetButtonDown("Jump") && !playerInAction)
    {
      if (hitData.hitFound)   
      {
         foreach (var action in newParkourActions)
         {
            if(action.CheckIfAvailable(hitData, transform)) 
            {
               StartCoroutine(PerformParkourAction(action));
               break;
            }
         }
       }
     }

     if(playerScript.playerOnLedge && !playerInAction && !hitData.hitFound)
     {

      bool canJump = true;

      if(playerScript.LedgeInfo.height > autoJumnpHeightLimit && !CrossPlatformInputManager.GetButtonDown("Jump"))
       canJump = false;
      if(canJump && playerScript.LedgeInfo.angle <= 90)
      {
      playerScript.playerOnLedge = false;
      StartCoroutine(PerformParkourAction(jumpDownParkourAction));
       }
     }
   }

   IEnumerator PerformParkourAction(NewParkourAction action)
   {
      playerInAction = true;
      playerScript.SetControl(false);

      animator.CrossFade(action.AnimationName, 0.2f);
      yield return null;

      var animationState = animator.GetNextAnimatorStateInfo(0);

      if(!animationState.IsName(action.AnimationName))
      Debug.Log("Animation name is incorrect");

      

      float timerCounter = 0f;

      while(timerCounter <= animationState.length)
      {
         timerCounter += Time.deltaTime;

         // Make player to look towards the obstacle

         if(action.LookAtObstacle)
         {
           transform.rotation = Quaternion.RotateTowards(transform.rotation, action.RequiredRotation, playerScript.rotSpeed * Time.deltaTime);
         }

         if(action.AllowTargetMatching)
         {
            CompareTarget(action);
         }

         if(animator.IsInTransition(0) && timerCounter > 0.5f)
         {
            break;
         }



         yield return null;
      }

      yield return new WaitForSeconds(action.ParkourActionDelay);


      playerScript.SetControl(true);

      playerInAction = false;
   }

   void CompareTarget(NewParkourAction action)
   {
      animator.MatchTarget(action.ComparePosition, transform.rotation, action.CompareBodyPart, new MatchTargetWeightMask(action.ComparePositionWeight, 0), action.CompareStartTime, action.CompareEndTime);
   }
}
