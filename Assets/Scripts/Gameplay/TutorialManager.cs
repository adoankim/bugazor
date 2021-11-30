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

    [SerializeField]
    private LevelsManager levelsManager;

    [SerializeField]
    private GameObject gameCanvas;

    [SerializeField]
    private GameObject tutorialCanvas;

    [SerializeField]
    private GameObject storyDiskette;   

    int stage = 0;

    private bool leftPressed = false;
    private bool rightPressed = false;
    private bool spacePressed = false;

    void Start()
    {
        StartCoroutine("StartTutorial");
    }

    public void SkipTutorial()
    {
        StopCoroutine("StartTutorial");
        FinishTutorial();
    }

    private void FinishTutorial()
    {
        gameCanvas.SetActive(true);
        tutorialCanvas.SetActive(false);
        levelsManager.enabled = true;
        playerController.enabled = true;
        playerShoot.enabled = true;
        Destroy(gameObject);
    }

    IEnumerator StartTutorial()
    {
        yield return StartCoroutine(IntroStep());

        yield return StartCoroutine(MoveStep());

        yield return StartCoroutine(ShootStep());

        yield return StartCoroutine(StoryStep());

        FinishTutorial();
    }

    IEnumerator IntroStep()
    {
        yield return new WaitForSeconds(2f);

        tutorialText.text = "Hey!";
        yield return new WaitForSeconds(2f);

        tutorialText.text = "Yeah, you!";

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
        else if(stage == 2)
        {
            bool pressed = Input.GetKeyDown(KeyCode.Space);

            if(pressed && !spacePressed)
            {
                spacePressed = true;
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

        yield return new WaitForSeconds(3f);
    }

    IEnumerator ShootStep()
    {

        tutorialText.text = "Now, let's try something else...";

        yield return new WaitForSeconds(3f);

        tutorialText.text = "But be careful!";

        yield return new WaitForSeconds(2.5f);

        tutorialText.text = "Try to release your inner energy.";

        yield return new WaitForSeconds(2f);

        space.SetActive(true);
        playerShoot.enabled = true;
        stage = 2;

        while (!spacePressed)
        {
            yield return new WaitForSeconds(2.0f);
        }

        yield return new WaitForSeconds(2.0f);

        tutorialText.text = "Good job, you did well!";

        playerShoot.enabled = false;
        space.SetActive(false);

        yield return new WaitForSeconds(3f);
    }

    IEnumerator StoryStep()
    {
        storyDiskette.SetActive(true);

        MoveVerticalFromAToB check = storyDiskette.GetComponent<MoveVerticalFromAToB>();
        EnemyDamage enemyDamage = storyDiskette.GetComponent<EnemyDamage>();
        GameObject diskette = storyDiskette.transform.Find("diskette").gameObject;
        GameObject particleSystem = storyDiskette.transform.Find("FreeParticles").gameObject;
        GameObject bugInDiskette = storyDiskette.transform.Find("BugInDiskette").gameObject;
        GameObject byeParticles = storyDiskette.transform.Find("ByeParticles").gameObject;

        while (!check.HasReachedTargetPosition)
        {
            yield return new WaitForSeconds(2f);
        }

        tutorialText.text = "Mmmm... what is that?";

        yield return new WaitForSeconds(3f);

        tutorialText.text = "Should we shoot to it?";

        yield return new WaitForSeconds(3f);
        
        playerController.enabled = true;
        playerShoot.enabled = true;

        while (enemyDamage.IsAlive)
        {
            yield return new WaitForSeconds(2f);
        }

        playerController.enabled = false;
        playerShoot.enabled = false;

        tutorialText.text = "";
        particleSystem.SetActive(true);
        diskette.SetActive(false);

        yield return new WaitForSeconds(1.6f);
        bugInDiskette.SetActive(true);

        yield return new WaitForSeconds(3f);

        tutorialText.text = "Uh????";

        yield return new WaitForSeconds(2.5f);

        tutorialText.text = "Where am I??";

        yield return new WaitForSeconds(3f);

        tutorialText.text = "Oh!, I was trapped in that diskette?";

        yield return new WaitForSeconds(3.5f);

        tutorialText.text = "Thanks for saving me!";

        yield return new WaitForSeconds(2.5f);

        tutorialText.text = "Bye!";

        yield return new WaitForSeconds(1.5f);

        byeParticles.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        tutorialText.text = "";
        bugInDiskette.SetActive(false);


        yield return new WaitForSeconds(3f);
    }
}
