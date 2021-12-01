using System.Collections.Generic;
using UnityEngine;

public class PlayerKind : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private List<AnimationClip> animationClips;
    void Start()
    {
        int naturalSelection = Random.Range(0, animationClips.Count);
        string kindAnimationName = animationClips[naturalSelection].name;
        animator.Play(kindAnimationName);
    }
}
