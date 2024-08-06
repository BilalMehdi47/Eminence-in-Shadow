using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class WeaponsMenu : MonoBehaviour
{
  
    public GameObject weaponsMenuUI;
    public bool weaponsMenuActive = false;
    public GameObject mainCamera;

    [Header("Weapons")]
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    public GameObject weapon4StockUI;

    [Header("Rations")]
  //  public GameObject healthBottle;
   // public GameObject energyBottle;
    public Inventory inventory;


    [Header("Menus")]
public GameObject playerUI;
public GameObject miniMapCanvas;
public GameObject currentMenuUI;


    private void Update()
    {

        if (weaponsMenuActive == true)
    {
        playerUI.SetActive(false);
        miniMapCanvas.SetActive(false);
        currentMenuUI.SetActive(false);
    }

    if (weaponsMenuActive == false)
    {
        playerUI.SetActive(true);
        miniMapCanvas.SetActive(true);
        currentMenuUI.SetActive(true);
    }
        if (CrossPlatformInputManager.GetButtonDown("Tab") && weaponsMenuActive == false)

        {
            //open weapon menu
            // playerUI.SetActive(false);
            // miniMapCanvas.SetActive(false);
            // currentMenuUI.SetActive(false);

            weaponsMenuUI.SetActive(true);
            weaponsMenuActive = true;
            Time.timeScale = 0;
            mainCamera.GetComponent<MainCamera>().enabled = false;
        }

        else if (CrossPlatformInputManager.GetButtonDown("CloseTab") && weaponsMenuActive == true)
        {
            //close weapon menu
            // playerUI.SetActive(true);
            // miniMapCanvas.SetActive(true);
            // currentMenuUI.SetActive(true);

            weaponsMenuUI.SetActive(false);
            weaponsMenuActive = false;
            Time.timeScale = 1;
            mainCamera.GetComponent<MainCamera>().enabled = true;
        }

         WeaponsCheck();

    }

    void WeaponsCheck()
{
    if (inventory.isweapon1Picked == true)
    {
        weapon1.SetActive(true);
    }

    if (inventory.isweapon2Picked == true)
    {
        weapon2.SetActive(true);
    }

    if (inventory.isweapon3Picked == true)
    {
        weapon3.SetActive(true);
    }

    if (inventory.isweapon4Picked == true)
    {
        weapon4.SetActive(true);
        weapon4StockUI.SetActive(true);
    }
}

}


