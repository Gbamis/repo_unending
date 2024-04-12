using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;



[System.Serializable]
public struct KeyCommand
{
    public string key;
    public bool IsPerformed;
    public Action IsStarted;
}

public class KeyInput : MonoBehaviour
{

    private static KeyInput m_nstance;
    public static KeyInput Instance { set => m_nstance = value; get => m_nstance; }


    public KeyCommand pausedCmd;
    public KeyCommand swapCmd;
    public KeyCommand shootCmd;
    public KeyCommand shieldCmd;

    private void Awake()
    {
        Instance = this;
        LoadKeyBindings();
    }

    private void Update()
    {
        PoolInput();
    }
    public void LoadKeyBindings()
    {

        string pause = PlayerPrefs.GetString("pause", "space");
        string swap = PlayerPrefs.GetString("swap", "t");
        string shoot = PlayerPrefs.GetString("shoot", "l");
        string shield = PlayerPrefs.GetString("shield", "e");

        pausedCmd = new KeyCommand { key = pause, };
        swapCmd = new KeyCommand() { key = swap };
        shootCmd = new KeyCommand() { key = shoot };
        shieldCmd = new KeyCommand() { key = shield };


    }

    public string GetKey(string action)
    {
        string value = "";
        if (action == "pauseAction") { value = pausedCmd.key; }
        else if (action == "swapWeaponAction") { value = swapCmd.key; }
        else if (action == "shootAction") { value = shootCmd.key; }
        else if (action == "shieldAction") { value = shieldCmd.key; }
        return value;
    }
    public void SetKey(string value, string action)
    {

        if (action == "pauseAction")
        {
            pausedCmd.key = value;
            PlayerPrefs.SetString("pause",value);
        }
        else if (action == "swapWeaponAction")
        {
            swapCmd.key = value;
            PlayerPrefs.SetString("swap",value);
        }
        else if (action == "shootAction")
        {
            shootCmd.key = value;
            PlayerPrefs.SetString("shoot",value);
        }
        else if (action == "shieldAction")
        {
            shieldCmd.key = value;
            PlayerPrefs.SetString("shield",value);
        }
    }

    //public void SetKeyCode(KeyCode code) => keyCode = code;

    public void SaveKeyBindings() { }

    private void PoolInput()
    {
        string paused = pausedCmd.key;
        string shoot = shootCmd.key;
        string swapWeapon = swapCmd.key;

        if (Input.GetKey(shoot)) { shootCmd.IsPerformed = true; } else { shootCmd.IsPerformed = false; }
        if (Input.GetKeyDown(swapWeapon)) { swapCmd.IsStarted(); }
        if (Input.GetKeyDown(paused)) { pausedCmd.IsStarted(); }
    }
}
