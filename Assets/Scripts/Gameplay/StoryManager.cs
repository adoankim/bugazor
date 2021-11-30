using System.Collections;
using UnityEngine;
using TMPro;
using System;

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

}
