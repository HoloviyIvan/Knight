using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	[SerializeField] private Text nameText;
	[SerializeField] private Text dialogueText;

	[SerializeField] private Animator boxAnim;
	[SerializeField] private Animator startAnim;
	[SerializeField] private GameObject start;
	[SerializeField] private GameObject dialogue;
	[SerializeField] private Button nextDialogue;

	private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}

	public void StartDialogue (Dialogue dialogue)
	{
		boxAnim.SetBool("BoxOpen", true);
		startAnim.SetBool("StartOpen", false);
		nextDialogue.Select();
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

	public void EndDialogue()
	{
		boxAnim.SetBool("BoxOpen", false);
		start.SetActive(false);
		dialogue.SetActive(false);
	}

}
