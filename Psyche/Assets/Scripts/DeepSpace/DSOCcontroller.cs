using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MyBox;

public class DSOCcontroller : MonoBehaviour
{
    public struct Questions
    {
        public int id;
        public string question;
        public string answer;
    };

    public List<Questions> Level1Questions;
    public List<Questions> Level2Questions;
    public List<Questions> Level3Questions;
    public List<Questions> Level4Questions;


    [SerializeField] private bool test;
    [ConditionalField(nameof(test))]
    [SerializeField] private SceneChanger.scenes testScene;


    public static DSOCcontroller dsocRoot;
    public DSOC_Questions dsoc_questions;
    public Button btn_Continue;


    public List<TextMeshProUGUI> questions;

    public List<SlotScript> slots;

    public List<DragAndDrop> answers;

    [HideInInspector]
    public bool caught = false;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(true);
        dsocRoot = this;
        //dsoc_questions = GetComponent<DSOC_Questions>();
        if (GameRoot._Root.nextScene == SceneChanger.scenes.credits)
            btn_Continue.GetComponentInChildren<TextMeshProUGUI>().text = "Credits";
        else
            btn_Continue.GetComponentInChildren<TextMeshProUGUI>().text = "Start Next Mission";

        btn_Continue.interactable = false;
        GenerateQuestions();
    }

    private void GenerateQuestions()
    {
        InitQuestions();
        Debug.Log("Entering GenerateQuestions1");

        Generate5Plus();
    }

    private void GenerateDebug()
    {
        Debug.Log("Entering GenerateQuestions2");

        Questions[] trivia = new Questions[5];

        Debug.Log("LastScene: " + GameRoot._Root.prevScene.ToString());

        switch (GameRoot._Root.prevScene)
        {
            case SceneChanger.scenes.level1:
                trivia[0] = new Questions() { id = 1,
                    question = "New Laser communication technology that encodes photons to communicate between a probe in deep space and earth.",
                    answer = "Deep Space Optical Communication" };
                trivia[1] = new Questions() { id = 2,
                    question = "Provides high-resolution images using filters to discriminate between Psyche's metallic and silicate constituents.",
                    answer = "MultiSpectral Imager" };
                trivia[2] = new Questions() { id = 3,
                    question = "Will detect, measure and map Psyche's elemental composition.",
                    answer = "Gamma Ray and Neutron Spectrometer" };
                trivia[3] = new Questions() { id = 4,
                    question = "Designed to detect and measure the remanent magnetic field of the asteroid.",
                    answer = "Magnetometer" };
                trivia[4] = new Questions() { id = 5,
                    question = "Use the X-band radio telecommunications system to measure Psyche's gravity field to high precision.",
                    answer = "Radio Science"};
                break;
            case SceneChanger.scenes.level2:
                Debug.Log("Picking Questions List 2");
                trivia[0] = new Questions()
                {
                    id = 1,
                    question = "When does the Psyche spaceraft stop orbiting the Psyche asteroid?",
                    answer = "2026"
                };
                trivia[1] = new Questions()
                {
                    id = 2,
                    question = "Provides high-resolution images using filters to discriminate between Psyche's metallic and silicate constituents.",
                    answer = "MultiSpectral Imager"
                };
                trivia[2] = new Questions()
                {
                    id = 3,
                    question = "What university is the mission led by?",
                    answer = "Arizona State University"
                };
                trivia[3] = new Questions()
                {
                    id = 4,
                    question = "When was the Psyche asteroid discovered?",
                    answer = "1852"
                };
                trivia[4] = new Questions()
                {
                    id = 5,
                    question = "What astronomer discovered the Psyche asteroid?",
                    answer = "Annibale de Gasparis"
                };
                break;
            case SceneChanger.scenes.level3:
                Debug.Log("Picking Questions List 3");
                trivia[0] = new Questions()
                {
                    id = 1,
                    question = "How far is the Psyche asteroid from the sun?",
                    answer = "93 million miles"
                };
                trivia[1] = new Questions()
                {
                    id = 2,
                    question = "How long is a year on Psyche?",
                    answer = "5 years"
                };
                trivia[2] = new Questions()
                {
                    id = 3,
                    question = "How long is a day on Psyche?",
                    answer = "4 hours and 12 minutes"
                };
                trivia[3] = new Questions()
                {
                    id = 4,
                    question = "Where is the Psyche asteroid?",
                    answer = "Main asteroid belt between Mars and Jupiter"
                };
                trivia[4] = new Questions()
                {
                    id = 5,
                    question = "Use the X-band radio telecommunications system to measure Psyche's gravity field to high precision.",
                    answer = "Radio Science"
                };
                break;

            case SceneChanger.scenes.level4:
                Debug.Log("Picking Questions List 4");
                trivia[0] = new Questions()
                {
                    id = 1,
                    question = "What is the length of the Psyche spacecraft?",
                    answer = "24.76 meters"
                };
                trivia[1] = new Questions()
                {
                    id = 2,
                    question = "What is the width of the Psyche spacecraft?",
                    answer = "7.34 meters"
                };
                trivia[2] = new Questions()
                {
                    id = 3,
                    question = "Will detect, measure and map Psyche's elemental composition.",
                    answer = "Gamma Ray and Neutron Spectrometer"
                };
                trivia[3] = new Questions()
                {
                    id = 4,
                    question = "What kind of propellant will the Psyche spacecraft use?",
                    answer = "Xenon"
                };
                trivia[4] = new Questions()
                {
                    id = 5,
                    question = "How far will the Psyche spacecraft travel?",
                    answer = "1.5 billion miles"
                };

                break;
        }

        Debug.Log("Trivia Questions Count: " + trivia.Length.ToString());

        foreach(Questions ques in trivia)
        {
            Debug.Log("ID: " + ques.id.ToString() +
                "\nQuestion: " + ques.question +
                "\nAnswer: " + ques.answer);
        }


        //Add the 5 questions to the window and shuffle where the answers are
        bool[] usedSlots = { false, false, false, false, false };
        int rand;
        for (int i = 0; i < 5; i++)
        {
            questions[i].text = trivia[i].question;
            slots[i].SetID(trivia[i].id);
            answers[i].SetCardData(trivia[i].id, trivia[i].answer);
        }
    }

    private void Generate5()
    {
        List<Questions> initList = new List<Questions>();

        //Create a scrabable list from the master lists
        switch (GameRoot._Root.prevScene)
        {
            case SceneChanger.scenes.level1:
                initList = Level1Questions;
                break;
            case SceneChanger.scenes.level2:
                initList = Level2Questions;
                break;
            case SceneChanger.scenes.level3:
                initList = Level3Questions;
                break;
            case SceneChanger.scenes.level4:
                initList = Level4Questions;
                break;
        }

        //Add the 5 questions to the window and shuffle where the answers are
        bool[] usedSlots = { false, false, false, false, false };
        int rand;
        for (int i = 0; i < 5; i++)
        {
            questions[i].text = initList[i].question;
            slots[i].id = initList[i].id;
            answers[i].id = initList[i].id;
            answers[i].UIText.text = initList[i].answer;
            //do
            //{
            //    rand = Random.Range(0, answers.Count);
            //    if (usedSlots[rand])
            //        rand++;
            //    if (rand >= answers.Count)
            //        rand = 0;
            //} while (usedSlots[rand]);

            //answers[rand].id = initList[i].id;
            //answers[rand].UIText.text = initList[i].answer;
            //usedSlots[rand] = true;
        }

    }

    private void Generate5Plus()
    {
        List<Questions> initList = new List<Questions>();
        List<Questions> finalList = new List<Questions>();

        if (test)
            GameRoot._Root.prevScene = testScene;

        //Create a scrabable list from the master lists
        switch (GameRoot._Root.prevScene)
        {
            case SceneChanger.scenes.level1:
                initList = Level1Questions;
                break;
            case SceneChanger.scenes.level2:
                initList = Level2Questions;
                break;
            case SceneChanger.scenes.level3:
                initList = Level3Questions;
                break;
            case SceneChanger.scenes.level4:
                initList = Level4Questions;
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
        GameRoot._Root.audioService.PlayUIAudio(GameRoot._Root.audioLibrary.UI[0]);

        GameRoot._Root.sceneChanger.ChangeScene(GameRoot._Root.nextScene);
        this.gameObject.SetActive(false);
    }

    public void InitQuestions()
    {
        Level1Questions = new List<Questions>{
            new Questions() {id = 1,
            question = "New Laser communication technology that encodes photons to communicate between a probe in deep space and earth.",
            answer = "Deep Space Optical Communication"} ,
            new Questions() {id = 2,
            question = "Provides high-resolution images using filters to discriminate between Psyche's metallic and silicate constituents.",
            answer = "MultiSpectral Imager"} ,
            new Questions() {id = 3,
            question = "Will detect, measure and map Psyche's elemental composition.",
            answer = "Gamma Ray and Neutron Spectrometer"} ,
            new Questions() {id = 4,
            question = "Designed to detect and measure the remanent magnetic field of the asteroid.",
            answer = "Magnetometer"} ,
            new Questions() {id = 5,
            question = "Use the X-band radio telecommunications system to measure Psyche's gravity field to high precision.",
            answer = "Radio Science"}
        };

        Level2Questions = new List<Questions>{
            new Questions() {id = 1,
            question = "When does the Psyche spaceraft stop orbiting the Psyche asteroid?",
            answer = "2026"} ,
            new Questions() {id = 2,
            question = "Provides high-resolution images using filters to discriminate between Psyche's metallic and silicate constituents.",
            answer = "MultiSpectral Imager"} ,
            new Questions() {id = 3,
            question = "What university is the mission led by?",
            answer = "Arizona State University"} ,
            new Questions() {id = 4,
            question = "When was the Psyche asteroid discovered?",
            answer = "1852"} ,
            new Questions() {id = 5,
            question = "What astronomer discovered the Psyche asteroid?",
            answer = "Annibale de Gasparis"}
        };

        Level3Questions = new List<Questions>{
            new Questions() {id = 1,
            question = "How far is the Psyche asteroid from the sun?",
            answer = "93 million miles"} ,
            new Questions() {id = 2,
            question = "How long is a year on Psyche?",
            answer = "5 years"} ,
            new Questions() {id = 3,
            question = "How long is a day on Psyche?",
            answer = "4 hours and 12 minutes"} ,
            new Questions() {id = 4,
            question = "Where is the Psyche asteroid?",
            answer = "Main asteroid belt between Mars and Jupiter"} ,
            new Questions() {id = 5,
            question = "Use the X-band radio telecommunications system to measure Psyche's gravity field to high precision.",
            answer = "Radio Science"}
        };

        Level4Questions = new List<Questions>{
            new Questions() {id = 1,
            question = "What is the length of the Psyche spacecraft?",
            answer = "24.76 meters"} ,
            new Questions() {id = 2,
            question = "What is the width of the Psyche spacecraft?",
            answer = "7.34 meters"} ,
            new Questions() {id = 3,
            question = "Will detect, measure and map Psyche's elemental composition.",
            answer = "Gamma Ray and Neutron Spectrometer"} ,
            new Questions() {id = 4,
            question = "What kind of propellant will the Psyche spacecraft use?",
            answer = "Xenon"} ,
            new Questions() {id = 5,
            question = "How far will the Psyche spacecraft travel?",
            answer = "1.5 billion miles"}
        };

    }

}
