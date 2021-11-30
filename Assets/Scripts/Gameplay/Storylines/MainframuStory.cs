using System.Collections;
using UnityEngine;

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
        yield return new WaitForSeconds(3f);

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

    }
}
