using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager _instance;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip mainTheme;

    [SerializeField]
    AudioClip freedCompanionSound;

    [SerializeField]
    AudioClip freedOldMasterSound;

    [SerializeField]
    AudioClip dialogueTheme;

    [SerializeField]
    AudioClip midBossTheme;

    [SerializeField]
    AudioClip finalBossTheme;

    void Start()
    {
        if(_instance != null)
        {
            Destroy(_instance);
        }

        _instance = this;

        PlayMainTheme();
    }

    public void PlayMainTheme()
    {
        audioSource.Stop();
        audioSource.clip = mainTheme;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayMidBossTheme()
    {
        audioSource.Stop();
        audioSource.clip = midBossTheme;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayFinalBossTheme()
    {
        audioSource.Stop();
        audioSource.clip = finalBossTheme;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayStartGame()
    {
        audioSource.Stop();
        audioSource.clip = freedCompanionSound;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void StartDialogue()
    {

        StopCoroutine("PlayDialogueMusic");
        audioSource.Stop();
        audioSource.clip = freedCompanionSound;
        audioSource.loop = false;
        audioSource.Play();

        StartCoroutine("PlayDialogueMusic");
    }

    IEnumerator PlayDialogueMusic()
    {
        while (!audioSource.loop && audioSource.isPlaying)
        {
            yield return new WaitForSeconds(0.5f);
        }
        audioSource.clip = dialogueTheme;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StartEndingDialogue()
    {

        StopCoroutine("PlayDialogueMusic");
        audioSource.Stop();
        audioSource.clip = freedOldMasterSound;
        audioSource.loop = false;
        audioSource.Play();

        StartCoroutine("PlayDialogueMusic");
    }
}
