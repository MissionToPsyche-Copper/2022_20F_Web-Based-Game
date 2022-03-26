using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MyBox;

public class DSOCcontroller : MonoBehaviour
{
    [SerializeField] private bool test;
    [ConditionalField(nameof(test))]
    [SerializeField] private SceneChanger.scenes testScene;


    public static DSOCcontroller dsocRoot;
    public Button btn_Continue;


    public List<TextMeshProUGUI> questions;

    public List<SlotScript> slots;

    public List<DragAndDrop> answers;

    public bool caught = false;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(true);
        dsocRoot = this;
        if (GameRoot._Root.nextScene == SceneChanger.scenes.credits)
            btn_Continue.GetComponentInChildren<TextMeshProUGUI>().text = "Credits";
        else
            btn_Continue.GetComponentInChildren<TextMeshProUGUI>().text = "Start Next Mission";

        btn_Continue.interactable = false;
        GenerateQuestions();
    }

    private void GenerateQuestions()
    {
        List<DSOC_Questions.Questions> initList = new List<DSOC_Questions.Questions>();
        List<DSOC_Questions.Questions> finalList = new List<DSOC_Questions.Questions>();

        if (test)
            GameRoot._Root.prevScene = testScene;

        //Create a scrabable list from the master lists
        switch (GameRoot._Root.prevScene)
        {
            case SceneChanger.scenes.level1:
                for (int i = 0; i < DSOC_Questions.Level1Questions.Length; i++)
                    initList.Add(DSOC_Questions.Level1Questions[i]);
                break;
            case SceneChanger.scenes.level2:
                for (int i = 0; i < DSOC_Questions.Level2Questions.Length; i++)
                    initList.Add(DSOC_Questions.Level2Questions[i]);
                break;
            case SceneChanger.scenes.level3:
                for (int i = 0; i < DSOC_Questions.Level3Questions.Length; i++)
                    initList.Add(DSOC_Questions.Level3Questions[i]);
                break;
            case SceneChanger.scenes.level4:
                for (int i = 0; i < DSOC_Questions.Level4Questions.Length; i++)
                    initList.Add(DSOC_Questions.Level4Questions[i]);
                break;
        }

        //Pick 5 random questions (remove questions already picked)
        int index;
        do
        {
            index = Random.Range(0, initList.Count);
            finalList.Add(initList[index]);
            initList.RemoveAt(index);
        } while (finalList.Count < 5);

        //Add the 5 questions to the window and shuffle where the answers are
        bool[] usedSlots = { false, false, false, false, false };
        int rand;
        for (int i = 0; i < 5; i++)
        {
            questions[i].text = finalList[i].question;
            slots[i].id = finalList[i].id;
            do { rand = Random.Range(0, 5); } while (usedSlots[rand]);
            usedSlots[rand] = true;
            answers[rand].id = finalList[i].id;
            answers[rand].UIText.text = finalList[i].answer;
        }

    }

    public void CheckAnswers()
    {
        bool correct = true;
        foreach(SlotScript slot in slots)
            if (!slot.correct)
            {
                correct = false;
                break;
            }
        if(correct)
        {
            foreach (DragAndDrop answer in answers)
                answer.interactable = false;
            btn_Continue.interactable = true;
        }    
    }

    public void Click_Continue()
    {
        GameRoot._Root.sceneChanger.ChangeScene(GameRoot._Root.nextScene);
        this.gameObject.SetActive(false);
    }
}
