using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI tutorialText;

    [SerializeField]
    private GameObject arrows;

    [SerializeField]
    private GameObject space;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private PlayerShoot playerShoot;

    int stage = 0;

    private bool leftPressed = false;
    private bool rightPressed = false;

    void Start()
    {
        StartCoroutine(StartTutorial());
    }

    IEnumerator StartTutorial()
    {
        yield return StartCoroutine(IntroStep());

        yield return StartCoroutine(MoveStep());
    }

    IEnumerator IntroStep()
    {
        yield return new WaitForSeconds(2f);

        tutorialText.text = "Hey!";
        yield return new WaitForSeconds(2f);

        tutorialText.text = "Yeah you!";

        yield return new WaitForSeconds(3f);
    }

    private void Update()
    {
        if(stage == 1)
        {
            float h = Input.GetAxisRaw("Horizontal");

            if (h < 0 && !leftPressed)
            {
                leftPressed = true;
            }

            if (h > 0 && !rightPressed)
            {
                rightPressed = true;
            }
        }   
    }

    IEnumerator MoveStep()
    {

        tutorialText.text = "Are you able to move?";

        yield return new WaitForSeconds(2f);

        arrows.SetActive(true);
        playerController.enabled = true;
        stage = 1;

        while (!leftPressed || !rightPressed)
        {
            yield return new WaitForSeconds(2.0f);
        }

        yield return new WaitForSeconds(2.0f);

        tutorialText.text = "Well done, I knew you could move!";

        playerController.StopMove();
        playerController.enabled = false;
        arrows.SetActive(false);
    }
}
