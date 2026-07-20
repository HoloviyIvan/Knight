using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueAnimator : MonoBehaviour
{
    [SerializeField] private Animator startAnim;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] Button startDialog;


    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            startAnim.SetBool("StartOpen", true);
            startDialog.Select();
        }               
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            startAnim.SetBool("StartOpen", false);
            dialogueManager.EndDialogue();        
        }

        //startAnim.SetBool("StartOpen", false);
        //dialogueManager.EndDialogue();        
    }
}
