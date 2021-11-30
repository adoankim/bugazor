using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MainframuStory : StoryManager
{
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

        chatBoxText.text = "ah";
        int i = 10;
        while(i > 0)
        {
            yield return new WaitForSeconds(0.1f);
            chatBoxText.text += "h";
            i--;
        }
        yield return new WaitForSeconds(3);
    }
}
