using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

public class KeyBind : MonoBehaviour, IPointerClickHandler
{
    public string actionName;
    public InputField text;
    public Text placeHolderTxt;

    private void OnEnable()
    {
       
        StartCoroutine(Activate());
        IEnumerator Activate()
        {
            yield return new WaitForSeconds(1);
            text.interactable = true;
             text.onValueChanged.AddListener(HandleInput);
        }
    }

    private void OnDisable() => text.interactable = false;

    private void Start()
    {
        string input = KeyInput.Instance.GetKey(actionName);
        text.text = input.ToUpper();
    }

    public void OnPointerClick(PointerEventData ped)
    {
        text.text = "";
        placeHolderTxt.text = "[Press key to set]";
    }

    public void HandleInput(string value)
    {
        Debug.Log("called");
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(kcode))
            {
                string input = kcode.ToString().ToLower().Trim();
                text.text = input.ToUpper();
                KeyInput.Instance.SetKey(input, actionName);
            }
        }


    }
}
