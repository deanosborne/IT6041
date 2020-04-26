using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	public Animator animator;
	public Canvas myDialogBaloon;

	private Queue<string> sentences;

	public GameObject Player;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
		myDialogBaloon.GetComponent<Canvas>().enabled = false;
		Cursor.visible = false;
	}

	public void StartDialogue (Dialogue dialogue)
	{
		Player.GetComponent<CQExamplePlayer>().mIsControlEnabled = false;
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		myDialogBaloon.GetComponent<Canvas>().enabled = true;
		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		Player.GetComponent<CQExamplePlayer>().mIsControlEnabled = true;
		Cursor.visible = false;
		animator.SetBool("IsOpen", false);
		myDialogBaloon.GetComponent<Canvas>().enabled = false;
	}

}
