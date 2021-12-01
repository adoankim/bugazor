using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MainframuStory : StoryManager
{

    private GameObject container;
    private GameObject cylinder;
    private GameObject freeParticles;
    private GameObject theEndCanvas;

    private UnityEvent onStartStoryFinished = new UnityEvent();

    public void AddOnStartStoryFinishedListener(UnityAction listener)
    {
        onStartStoryFinished.AddListener(listener);
    }

    private void OnDestroy()
    {
        onStartStoryFinished.RemoveAllListeners();
    }

    protected override void Start()
    {
        base.Start();
        container = transform.Find("Container").gameObject.transform.Find("CylinderContainer").gameObject;
        theEndCanvas = GameObject.Find("Canvases").gameObject.transform.Find("TheEndCanvas").gameObject.gameObject;
        freeParticles = container.transform.Find("FreeParticles").gameObject;
        cylinder = container.transform.Find("Cylinder").gameObject;
        cylinder.GetComponent<EnemyDamage>().AddOnEnemyDieListener(OnCylinderDestroyed);
    }

    private void OnCylinderDestroyed()
    {

        playerController.StopMove();
        playerController.enabled = true;
        playerShoot.enabled = true;
        StartCoroutine(OldMasterFreedStory());
    }

    protected override IEnumerator StartStory()
    {
        MoveVerticalFromAToB check = transform.GetComponent<MoveVerticalFromAToB>();

        while (!check.HasReachedTargetPosition)
        {
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(1.5f);
        chatBox.SetActive(true);

        yield return DisplayTextProgresively("You've finally arrived!", 0.1f, 3f);

        yield return DisplayTextProgresively("And what a pain you've been!", 0.1f, 3f);

        yield return DisplayTextProgresively("The bugs you've freed are now causing problems everywhere!!", 0.1f, 4f);

        yield return DisplayTextProgresively("You shall not pass!", 0.05f, 3f);

        if (MusicManager._instance != null)
        {
            MusicManager._instance.PlayFinalBossTheme();
        }

        chatBox.SetActive(false);
        playerController.enabled = true;
        playerShoot.enabled = true;

        onStartStoryFinished.Invoke();
    }

    protected override void OnBossDie()
    {
        base.OnBossDie();
        StartCoroutine(StartEndingStory());
    }

    public void OnShieldBroken(UnityEngine.Events.UnityAction continueCallback)
    {
        StartCoroutine(StartShieldBrokenStory(continueCallback));
    }

    IEnumerator StartShieldBrokenStory(UnityAction continueCallback)
    {
        playerController.StopMove();
        playerController.enabled = false;
        playerShoot.enabled = false;

        yield return new WaitForSeconds(2);

        chatBox.SetActive(true);

        yield return DisplayTextProgresively("arg my collection waaaaall!!!", 0.025f, 4f);

        yield return DisplayTextProgresively("You're going to pay for this!!!", 0.025f, 4f);


        chatBox.SetActive(false);
        playerController.enabled = true;
        playerShoot.enabled = true;

        continueCallback();
    }

    IEnumerator StartEndingStory()
    {
        playerController.StopMove();
        playerController.enabled = false;
        playerShoot.enabled = false;

        player.GetComponent<PlayerDamage>().AddLives(9999);

        yield return new WaitForSeconds(0.25f);

        chatBox.SetActive(true);

        yield return DisplayTextProgresively("Nooooooooooo...", 0.15f, 2f);
        
        chatBox.SetActive(false);

        chatBoxText.text = "";

        MoveVerticalFromAToB check = container.GetComponent<MoveVerticalFromAToB>();

        check.enabled = true;

        while (!check.HasReachedTargetPosition)
        {
            yield return new WaitForSeconds(1.5f);
        }

        yield return new WaitForSeconds(3f);

        chatBox.SetActive(true);
        yield return new WaitForSeconds(.5f);

        yield return DisplayTextProgresively("Oh, child of mine, you've come to rescue me?", 0.15f, 2.5f);

        cylinder.GetComponent<BoxCollider2D>().enabled = true;
        chatBoxText.text = "";

        chatBox.SetActive(false);
        playerController.enabled = true;
        playerShoot.enabled = true;
    }

    IEnumerator OldMasterFreedStory()
    {
        if (MusicManager._instance != null)
        {
            MusicManager._instance.StartEndingDialogue();
        }

        freeParticles.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        cylinder.SetActive(false);
        yield return new WaitForSeconds(1.5f);

        chatBox.SetActive(true);
        yield return new WaitForSeconds(.5f);

        yield return DisplayTextProgresively("Oh, I'm free!, I've been trapped here since Sep 9th, 1947!");

        yield return DisplayTextProgresively("Thanks for saving me, and also the rest of our kind...");

        yield return DisplayTextProgresively("We can now spread our love in all the electronics around the world...", 0.15f, 4f);

        chatBox.SetActive(false);
        playerController.enabled = true;
        playerShoot.enabled = true;

        theEndCanvas.SetActive(true);
    }
}
