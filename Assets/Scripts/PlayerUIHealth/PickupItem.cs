using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PickupItem : MonoBehaviour
{
   [Header("Item Info")]
public int itemRadius;
public string ItemTag;
private GameObject ItemToPick;

[Header("Player Info")]
public Transform player;
public Inventory inventory;
public GameManager gameManager;

private void Start()
{
    ItemToPick = GameObject.FindWithTag(ItemTag);
}

private void Update()
{
    if(Vector3.Distance(transform.position, player.transform.position) < itemRadius)
    {
       // if(Input.GetKeyDown("f"))
       if(CrossPlatformInputManager.GetButtonDown("Pick"))
        {
            if(ItemTag == "Sword")
            {
                inventory.isweapon1Picked = true;
            }
            else if(ItemTag == "Rifle")
            {
               inventory.isweapon2Picked = true;    
            }
            else if(ItemTag == "Bazooka")
            {
                inventory.isweapon3Picked = true;
            }
            else if(ItemTag == "Grenade")
            {
                gameManager.numberofGrenades += 5;
                inventory.isweapon4Picked = true;
            }
            else if(ItemTag == "Health")
            {
                gameManager.numberofHealth += 1;
            }
            else if(ItemTag == "Energy")
            {
               gameManager.numberofEnergy += 1;
            }
            ItemToPick.SetActive(false);
        }
    }
}

}
