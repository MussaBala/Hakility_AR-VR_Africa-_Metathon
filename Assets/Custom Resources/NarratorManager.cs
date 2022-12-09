using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;

/// <summary>
/// The role of the manager is to tell dialogues and make the game advance.
/// He also provides possibilities to answer the question he asks
/// </summary>
public class NarratorManager : MonoBehaviour
{
    [Header("Scene 1")]
    public string Scene1Dialogue;
    public string Scene1GoodAnswer;
    public string Scene1BadAnswer;

    [Header("Scene 2")]
    public Transform MovePoint; //The narrator will move to this point after the user successfully answered the first question
    public string Scene2Dialogue;
    public string Scene2GoodAnswer;
    public string Scene2BadAnswer;

    [Header("Common")]
    public string GoodAnswerNarratorResponse;
    public string BadAnswerNarratorResponse;
    public float MoveSpeed;

    [Header("UI")]
    public GameObject NarrationCanvas;
    public TextMeshProUGUI NarrationText;
    [Space]
    public GameObject AnswersCanvas;
    public Button GoodAnswerButton;
    public Button BadAnswerButton;

    Animator anim;
    int step = 0;
    bool toMove = false;
    //NavMeshAgent agent;

    void Start()
    {
        anim = GetComponent<Animator>();
        NarrationCanvas.SetActive(false);
        AnswersCanvas.SetActive(false);
        //agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (toMove)
        {
            transform.LookAt(MovePoint);

            var step = MoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, MovePoint.position, step);

            //// Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, MovePoint.position) < 0.001f)
            {
                OnReachingPoint();
            }
        }
    }

    public void StartNarration()
    {
        step = 1;

        //play narration animation
        anim.SetTrigger("Explain");
        //display narration dialogue UI
        NarrationCanvas.SetActive(true);
        NarrationText.SetText(Scene1Dialogue);

        AnswersCanvas.SetActive(true);

        GoodAnswerButton.GetComponentInChildren<TextMeshProUGUI>().SetText(Scene1GoodAnswer);
        BadAnswerButton.GetComponentInChildren<TextMeshProUGUI>().SetText(Scene1BadAnswer);

        GoodAnswerButton.onClick.AddListener(() => {
            //trigger good answer animation
            AnswerUser(false);
        });
        
        BadAnswerButton.onClick.AddListener(() => {
            //trigger bad answer animation
            AnswerUser(true);
        });


        //go to idle animation
    }

    public void AnswerUser(bool isBadAnswer)
    {
        if (isBadAnswer)
        {
            //display bad answer response
            anim.SetTrigger("Bad Answer");
            print("Bad Answer");
        }
        else
        {
            print("Good Answer");
            //Display hint saying good answer
            anim.SetTrigger("Good Answer");
            NarrationCanvas.SetActive(false);
            if (step == 2)
            {
                step = 3;
                anim.SetBool("toWalk", false);
                GameManager.instance.DisplayGameOverMessage();
            }
            else if(step == 1)
            {
                step = 2;
                anim.SetBool("toWalk", true);
                //toMove = true;
            }
        }
    }

    public void GotoPoint()
    {
        if (step == 2)
        {
            //make the narrator move to a certain point using translate and making him facing the direction of movement
            toMove = true;
        }

        //trigger corresponding animation
        //anim.SetTrigger("Walk");
    }

    public void OnReachingPoint()
    {
        toMove = false;
        anim.SetBool("toWalk", false);

        transform.LookAt(Camera.main.transform.position);

        //triggger scene 2 dialogue
        NarrationCanvas.SetActive(true);
        NarrationText.SetText(Scene2Dialogue);

        AnswersCanvas.SetActive(true);

        GoodAnswerButton.GetComponentInChildren<TextMeshProUGUI>().SetText(Scene2GoodAnswer);
        BadAnswerButton.GetComponentInChildren<TextMeshProUGUI>().SetText(Scene2BadAnswer);

        GoodAnswerButton.onClick.AddListener(() => {
            //trigger good answer animation
            AnswerUser(false);
        });

        BadAnswerButton.onClick.AddListener(() => {
            //trigger bad answer animation
            AnswerUser(true);
        });
    }
}
