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
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (Cursor.visible == false)
            {
                Cursor.visible = true;
            }
            else
            {
                Cursor.visible = false;
            }


        }


        if (GameManager.Instance.CurrentLv == 1)
        {
            LevelDialogue.text = ("Welcome to the tutorial You can press E to interact with people and collect quests. Press J to close");
        }

        if (GameManager.Instance.CurrentLv == 2)
        {
            LevelDialogue.text = ("Welcome to the Level 1. Press J to close");
        }

        if (GameManager.Instance.CurrentLv == 3)
        {
            LevelDialogue.text = ("Welcome to the Level 2. Press J to close");
        }

        if (GameManager.Instance.CurrentLv == 4)
        {
            LevelDialogue.text = ("Welcome to the Level 3. Press J to close");
        }

        if (GameManager.Instance.CurrentLv == 5)
        {
            LevelDialogue.text = ("Welcome to the Level 4. Press J to close");
        }

        if (GameManager.Instance.CurrentLv == 6)
        {
            LevelDialogue.text = ("Congratulations you have completed the game!!");
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            GameManager.Instance.CurrentLv = 0;
            LevelDialogue.text = ("");
        }
    }
}
