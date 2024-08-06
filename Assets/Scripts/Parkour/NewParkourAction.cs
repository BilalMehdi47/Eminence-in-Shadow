using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parkour menu/Create New Parkour Action")]
public class NewParkourAction : ScriptableObject
{


    [Header("Checking Obstacle height")]

   [SerializeField] string animationName;
   [SerializeField] string barrierTag;

   [SerializeField] float minimumHeight;
   [SerializeField] float maximumHeight;
   

   [Header("Rotating player towards Obstacles")]
   [SerializeField] bool lookAtObstacle;
   public Quaternion RequiredRotation {get; set;}

   [SerializeField] float parkourActionDelay;


     [Header("Target Matching")]
     [SerializeField] bool allowTargetMatching = true;
     [SerializeField] AvatarTarget compareBodyPart;
     [SerializeField] float compareStartTime;
     [SerializeField] float compareEndTime;
     public Vector3 ComparePosition {get; set;}
     [SerializeField] Vector3 comparePositionWeight = new Vector3(0, 1, 1);


     public bool CheckIfAvailable(ObstacleInfo hitData, Transform player)
     {

      if(!string.IsNullOrEmpty(barrierTag) && hitData.hitInfo.transform.tag != barrierTag)
      {
         return false;
      }


      // Check Height
      float checkHeight = hitData.heightInfo.point.y - player.position.y;

      if(checkHeight < minimumHeight || checkHeight > maximumHeight)
      return false;
      
      if(lookAtObstacle)
      {
         RequiredRotation = Quaternion.LookRotation(-hitData.hitInfo.normal);
      }

      if(allowTargetMatching)
      {
         ComparePosition = hitData.heightInfo.point;
      }


      return true;
     }

     public string AnimationName => animationName;

     public bool LookAtObstacle => lookAtObstacle;

     public float ParkourActionDelay => parkourActionDelay;


     public bool AllowTargetMatching => allowTargetMatching;
      public AvatarTarget CompareBodyPart => compareBodyPart;
      public float CompareStartTime => compareStartTime;
      public float CompareEndTime => compareEndTime;
      public Vector3 ComparePositionWeight => comparePositionWeight;

}
