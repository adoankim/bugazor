using System.Collections;
using UnityEngine;

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

    protected IEnumerator DisplayTextProgresively(string text, float endWait = 3f, float delayBetweenCharacters = 0.1f)
    {
        tutorialText.text = "";
        int i = 0;
        while (i < text.Length)
        {
            tutorialText.text += text[i];
            yield return new WaitForSeconds(delayBetweenCharacters);
            i++;
        }

        yield return new WaitForSeconds(endWait);
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

        yield return StartCoroutine(DisplayTextProgresively("Hey!", 2f));

        yield return StartCoroutine(DisplayTextProgresively("Yeah, you!", 2f));
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

        yield return StartCoroutine(DisplayTextProgresively("Are you able to move?", 2f));

        arrows.SetActive(true);
        playerController.enabled = true;
        stage = 1;

        while (!leftPressed || !rightPressed)
        {
            yield return new WaitForSeconds(2.0f);
        }

        yield return new WaitForSeconds(2.0f);

        playerController.StopMove();
        playerController.enabled = false;
        arrows.SetActive(false);

        yield return StartCoroutine(DisplayTextProgresively("Well done, I knew you could move!", 3f));
    }

    IEnumerator ShootStep()
    {

        yield return StartCoroutine(DisplayTextProgresively("Now, let's try something else...", 3f));

        yield return StartCoroutine(DisplayTextProgresively("But be careful!", 2.5f, 0.15f));

        yield return StartCoroutine(DisplayTextProgresively("Try to release your inner energy.", 2f));

        space.SetActive(true);
        playerShoot.enabled = true;
        stage = 2;

        while (!spacePressed)
        {
            yield return new WaitForSeconds(2.0f);
        }

        yield return new WaitForSeconds(2.0f);

        playerShoot.enabled = false;
        space.SetActive(false);

        yield return StartCoroutine(DisplayTextProgresively("Good job, you did well!", 3f));
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

        yield return StartCoroutine(DisplayTextProgresively("Mmmm... what's that?", 3f, 0.15f));

        yield return StartCoroutine(DisplayTextProgresively("Should we shoot it?", 3f, 0.05f));

        playerController.enabled = true;
        playerShoot.enabled = true;

        while (enemyDamage.IsAlive)
        {
            yield return new WaitForSeconds(2f);
        }

        playerController.StopMove();
        playerController.enabled = false;
        playerShoot.enabled = false;

        tutorialText.text = "";
        particleSystem.SetActive(true);
        diskette.SetActive(false);

        yield return new WaitForSeconds(1.6f);
        bugInDiskette.SetActive(true);

        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(DisplayTextProgresively("Uh????", 2.5f, 0.4f));


        yield return StartCoroutine(DisplayTextProgresively("Where am I??", 3f, 0.3f));

        yield return StartCoroutine(DisplayTextProgresively("Oh!, I was trapped in that diskette?", 3.5f, 0.1f));

        yield return StartCoroutine(DisplayTextProgresively("Thanks for saving me!", 3.5f, 0.08f));

        yield return StartCoroutine(DisplayTextProgresively("Bye!", 1.5f, 0.05f));

        byeParticles.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        tutorialText.text = "";
        bugInDiskette.SetActive(false);


        yield return new WaitForSeconds(3f);
    }
}
