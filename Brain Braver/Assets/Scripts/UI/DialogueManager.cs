using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    [SerializeField] Text m_nameText = null, m_dialogueText = null;
    [SerializeField] Animator m_animator = null;
    [SerializeField] OptionsSO m_options = null;
    [SerializeField] float transitionTime = 1f;

    Queue<string> sentences = new Queue<string>();
    bool isTyping = false;
    bool isChangingTime = false;
    float originalTime = 1f;

    private void OnEnable()
    {
        DialogueTrigger.OnDialogueTrigger += StartDialogue;
    }
    private void OnDisable()
    {
        DialogueTrigger.OnDialogueTrigger -= StartDialogue;
    }

    void StartDialogue(DialogueSO d, RoomSO room, float waitTime, int eventIndex)
    {
        StopAllCoroutines();
        StartCoroutine(DialogueCoroutine(d, room, waitTime, eventIndex));
    }
    IEnumerator DialogueCoroutine
        (DialogueSO dialogue, RoomSO room, float waitTime, int eventIndex)
    {
        yield return new WaitForSeconds(waitTime);
        sentences.Clear();
        originalTime = Time.timeScale;
        StartCoroutine(AlterTime(true));
        while (isChangingTime)
            yield return null;

        if (room && (eventIndex > -1 && eventIndex < room.storyEvents.Length))
            room.storyEvents[eventIndex].index = 1;
        m_nameText.text = dialogue.GetSpeaker();
        m_dialogueText.text = "";
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        m_animator.enabled = true;
        m_animator.gameObject.SetActive(true);
        m_animator.SetBool("isOpen", true);
        yield return new WaitForSecondsRealtime(1.5f);
        StartCoroutine(DisplaySentences());
    }

    IEnumerator DisplaySentences()
    {
        while (sentences.Count > 0)
        {
            string sentence = sentences.Dequeue();
            StopCoroutine(TypeSentence(sentence));
            StartCoroutine(TypeSentence(sentence));
            while (isTyping)
                yield return null;
            yield return new WaitForSeconds(3f);
            while (PauseMenu.m_isPaused)
                yield return null;
        }

        StartCoroutine(EndDialogue());
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        m_dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            while (PauseMenu.m_isPaused)
                yield return null;
            m_dialogueText.text += letter;
            yield return null;
        }
        isTyping = false;
    }

    IEnumerator EndDialogue()
    {
        m_animator.SetBool("isOpen", false);
        yield return new WaitForSecondsRealtime(1f);
        m_animator.enabled = false;
        m_animator.gameObject.SetActive(false);
        StartCoroutine(AlterTime(false));
    }

    IEnumerator AlterTime(bool slowDown)
    {
        isChangingTime = true;
        
        float elapsedTime = 0f;
        float target = slowDown ? m_options.dialogueTime : originalTime;
        while(Time.timeScale != target)
        {
            Time.timeScale = Mathf.MoveTowards(Time.timeScale, target, transitionTime / Time.unscaledDeltaTime);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
            if (elapsedTime > transitionTime + 0.5f) //In case there is some weird interaction, it eventually
            {//forcees the end result and breaks from the cycle
                Time.timeScale = target;
                break;
            }
        }
        isChangingTime = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            if (!m_animator.GetBool("isOpen"))
                return;
            StopAllCoroutines();
            StartCoroutine(DisplaySentences());
        }
    }
}
