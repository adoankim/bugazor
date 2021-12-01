using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MainframuStory : StoryManager
{

    private GameObject container;
    private GameObject cylinder;
    private GameObject freeParticles;
    private GameObject theEndCanvas;
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

        chatBoxText.text = "You've finally arrived!";
        yield return new WaitForSeconds(3f);

        chatBoxText.text = "And what a pain you've been!";
        yield return new WaitForSeconds(4f);

        chatBoxText.text = "The bugs you've freed are now causing problems everywhere!!";
        yield return new WaitForSeconds(5f);

        chatBoxText.text = "You shall not pass!";
        yield return new WaitForSeconds(3f);
        
        chatBox.SetActive(false);
        playerController.enabled = true;
        playerShoot.enabled = true;
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

        chatBoxText.text = "arg my collection waaaaall!!!";

        yield return new WaitForSeconds(4);

        chatBoxText.text = "You're going to pay for this!!!";

        yield return new WaitForSeconds(3);

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

        yield return new WaitForSeconds(0.25f);

        chatBox.SetActive(true);

        chatBoxText.text = "No";
        int i = 10;
        while(i > 0)
        {
            yield return new WaitForSeconds(0.1f);
            chatBoxText.text += "o";
            i--;
        }
        yield return new WaitForSeconds(2f);
        
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

        string text = "Oh, child of mine, you've come to rescue me?";

        chatBoxText.text = "";
        i = 0;
        while (i < text.Length)
        {
            chatBoxText.text += text[i];
            yield return new WaitForSeconds(0.15f);
            i++;
        }


        yield return new WaitForSeconds(2.5f);
        cylinder.GetComponent<BoxCollider2D>().enabled = true;
        chatBoxText.text = "";

        chatBox.SetActive(false);
        playerController.enabled = true;
        playerShoot.enabled = true;
    }

    IEnumerator OldMasterFreedStory()
    {
        freeParticles.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        cylinder.SetActive(false);
        yield return new WaitForSeconds(1.5f);

        chatBox.SetActive(true);
        yield return new WaitForSeconds(.5f);

        string text = "Oh, I'm free!, I've been trapped here since Sep 9th, 1947!";

        chatBoxText.text = "";
        int i = 0;
        while (i < text.Length)
        {
            chatBoxText.text += text[i];
            yield return new WaitForSeconds(0.15f);
            i++;
        }

        yield return new WaitForSeconds(3f);

        text = "Thanks for saving me, and also the rest of our kind...";

        chatBoxText.text = "";
        i = 0;
        while (i < text.Length)
        {
            chatBoxText.text += text[i];
            yield return new WaitForSeconds(0.15f);
            i++;
        }

        yield return new WaitForSeconds(3f);

        text = "We can know spread our love in all the electronics around the world...";

        chatBoxText.text = "";
        i = 0;
        while (i < text.Length)
        {
            chatBoxText.text += text[i];
            yield return new WaitForSeconds(0.15f);
            i++;
        }

        yield return new WaitForSeconds(4f);

        chatBox.SetActive(false);
        playerController.enabled = true;
        playerShoot.enabled = true;

        theEndCanvas.SetActive(true);
    }
}
