using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    [SerializeField] GameObject popup;
    [SerializeField] Animator anim;
    GameObject player;
    public TextMeshProUGUI textField;
    public TextMeshProUGUI nameField;
    Queue<string> sentences;

    [SerializeField] Dialogue dialogue;
    [SerializeField] float timeBetweenSentences = 3;

    private void Awake()
    {
        sentences = new Queue<string>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            popup.SetActive(true);
            anim.Play("DialogueSpawn");
            StartDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            anim.Play("DialogueDespawn");
            StopCoroutine(SentenceTimer());
            StartCoroutine(DeactivateAfterX());
        }
    }

    IEnumerator DeactivateAfterX()
    {
        yield return new WaitForSeconds(0.25f);
        popup.SetActive(false);

    }

    public void StartDialogue()
    {
        nameField.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        StartCoroutine(SentenceTimer());
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        textField.text = sentence;
    }
    void EndDialogue()
    {
        
    }

    IEnumerator SentenceTimer()
    {
        DisplayNextSentence();
        yield return new WaitForSeconds(timeBetweenSentences);
        DisplayNextSentence();
        yield return new WaitForSeconds(timeBetweenSentences);
        DisplayNextSentence();
    }
}
