using System.Collections;
using UnityEngine;
using TMPro;

public class StoryManager : MonoBehaviour
{
    protected PlayerController playerController;
    
    protected PlayerShoot playerShoot;

    protected GameObject chatBox;

    protected TextMeshProUGUI chatBoxText;

    public EnemyDamage enemyDamage;

    protected virtual void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        playerShoot = player.GetComponent<PlayerShoot>();
        playerController.StopMove();
        playerController.enabled = false;
        playerShoot.enabled = false;

        chatBox = GameObject.Find("Canvases").gameObject.transform.Find("GameCanvas").gameObject.transform.Find("ChatBox").gameObject;
        chatBoxText = chatBox.transform.Find("Text").GetComponent<TextMeshProUGUI>();


        enemyDamage.AddOnEnemyDieListener(OnBossDie);

        StartCoroutine(StartStory());
    }

    protected virtual void OnBossDie()
    {
    }

    protected virtual IEnumerator StartStory()
    {
        playerController.enabled = true;
        playerShoot.enabled = true;
        yield return null;
    }


    protected IEnumerator DisplayTextProgresively(string text, float delayBetweenCharacters=0.15f, float endWait=3f)
    {
        chatBoxText.text = "";
        int i = 0;
        while (i < text.Length)
        {
            chatBoxText.text += text[i];
            yield return new WaitForSeconds(delayBetweenCharacters);
            i++;
        }

        yield return new WaitForSeconds(endWait);
    }

}
