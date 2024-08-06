using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Missions : MonoBehaviour
{
    
    public bool Mission1; // Weapon
    public bool Mission2; // Locate
    public bool Mission3; // Fight knights
    public bool Mission4; // Fight boss

    public Text missiontext;
    public GameObject missionArea;
    public GameObject showButton;


public static Missions instance;

private void Awake()
{
    instance = this;
}
    void Update()
    {

        // if (Mission1 == false && Mission2 == false)
        // {
        //     missiontext.text = "Search Weapon";
        // }
        // if (Mission1 == true && Mission2 == false)
        // {
        //     missiontext.text = "Locate Knights";
        // }

        // if (Mission1 == true && Mission2 == false)
        // {
        //     missiontext.text = "Fight Knights";
        // }
        // if (Mission1 == true && Mission2 == false)
        // {
        //     missiontext.text = "Locate Boss";
        // }

        // if (Mission1 == true && Mission2 == true)
        // {
        //     missiontext.text = "Missions Completed";
        // }

        if(CrossPlatformInputManager.GetButtonDown("Show"))
        {

            StartCoroutine(showMissions());

        }

        if (!Mission1 && !Mission2 && !Mission3 && !Mission4)
        {
            missiontext.text = "Search for Weapons";
        }

        if (Mission1 && !Mission2 && !Mission3 && !Mission4)
        {
            missiontext.text = "Locate the Knights";
        }

        if (Mission1 && Mission2 && !Mission3 && !Mission4)
        {
            missiontext.text = "Fight Knights";
        }

        if (Mission1 && Mission2 && Mission3 && !Mission4)
        {
            missiontext.text = "Fight Boss";
        }
        if (Mission1 && Mission2 && Mission3 && Mission4)
        {
            missiontext.text = "Objectives Complete";
        }


    }

    IEnumerator showMissions()
{
    showButton.SetActive(false);
    missionArea.SetActive(true);
    yield return new WaitForSeconds(3f);
     missionArea.SetActive(false);
     showButton.SetActive(true);



}
}
