﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC3Dialogue1 : MonoBehaviour
{
	public Dialogue dialogue;
	private bool triggerEntered = false;

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && triggerEntered == true && GameManager.Instance.Completed > 2)
		{

			FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
			GameManager.Instance.Lv1 = 1;
		}
	}
	public void OnTriggerEnter(Collider coll)
	{
		// We set this variable to indicate that character is in trigger
		triggerEntered = true;
		Debug.Log("dialogue trigger entered");
	}
	public void OnTriggerExit()
	{
		// We reset this variable since character is no longer in the trigger
		triggerEntered = false;
		if (GameManager.Instance.Lv1 == 1)
        {
			GameManager.Instance.Completed = 0;
			GameManager.Instance.Lv1 = 0;
			GameManager.Instance.CurrentLv = 2;
			
			SceneManager.LoadScene(1);
		}
	}



}
