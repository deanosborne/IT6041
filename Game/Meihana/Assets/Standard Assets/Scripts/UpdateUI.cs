using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{


    [SerializeField]
    private Text LevelDialogue;

    void Update()
    {
        if (GameManager.Instance.CurrentLv == 1)
        {
            LevelDialogue.text = ("Welcome to the tutorial. Press J to close");
        }
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameManager.Instance.CurrentLv = 0;
            LevelDialogue.text = ("");
        }
    }
}
