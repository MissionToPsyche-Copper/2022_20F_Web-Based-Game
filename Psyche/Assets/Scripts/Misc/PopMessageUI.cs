using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyBox;

public class PopMessageUI : MonoBehaviour
{
    [SerializeField] private static Transform MessgCanvas;
    [SerializeField] private static GameObject PopMessg;

    private static List<GameObject> messages;


    private void Awake()
    {
        MessgCanvas = this.transform.Find("Canvas");
        PopMessg = MessgCanvas.Find("PopMessage").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        messages = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Random.value > 0.999f)
        //{
        //    PopUpMessage("this is a test");
        //}
    }

    public static void PopUpMessage(string text)
    {
        GameObject popup = Instantiate(PopMessg);
        popup.transform.parent = MessgCanvas;
        popup.transform.position = PopMessg.transform.position;
        popup.GetComponent<TextMeshProUGUI>().text = text;
        if (messages.Count > 0)
            for (int i = 0; i < messages.Count; i++)
                messages[i].GetComponent<MessageAni>().ShiftY(50.0f);

        //Play a sound here?

        popup.SetActive(true);

        messages.Add(popup);
    }
    public static void PopUpMessage(string text, float time)
    {
        //check that this message isn't already up
        foreach (GameObject message in messages)
            if (text.Equals(message.GetComponent<TextMeshProUGUI>().text))
                return;

        GameObject popup = Instantiate(PopMessg);
        popup.transform.parent = MessgCanvas;
        popup.transform.position = PopMessg.transform.position;
        popup.GetComponent<MessageAni>().SetLifeTime(time);
        popup.GetComponent<TextMeshProUGUI>().text = text;
        if (messages.Count > 0)
            for (int i = 0; i < messages.Count; i++)
                messages[i].GetComponent<MessageAni>().ShiftY(50.0f);

        //Play a sound here?

        popup.SetActive(true);

        messages.Add(popup);
    }

    public static void ClearMessages()
    {
        for (int i = 0; i < messages.Count; i++)
            RemovePop(messages[i]);
    }

    public static void RemovePop(GameObject messg)
    {
        int index = messages.IndexOf(messg);
        if (messages.Count - 1 > index) 
            for(int i = index; i < messages.Count; i++)
                messages[i].GetComponent<MessageAni>().ShiftY(50.0f);

        messages.Remove(messg);
        Destroy(messg);
    }
}
