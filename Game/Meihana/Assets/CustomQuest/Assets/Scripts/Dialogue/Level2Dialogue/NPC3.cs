using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC3 : MonoBehaviour
{
	public Dialogue dialogue;
	private bool triggerEntered = false;

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && triggerEntered == true && GameManager.Instance.Completed == 2)
		{

			FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
			GameManager.Instance.Completed = 3;

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

	}



}
