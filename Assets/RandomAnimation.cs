using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimation : MonoBehaviour
{
    [SerializeField] float averageDelay;
    private AnimationClip[] clips;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        clips = animator.runtimeAnimatorController.animationClips;
    }

    void Start()
    {
        StartCoroutine(PlayRandomAnimations());
    }

    private IEnumerator PlayRandomAnimations()
    {
        while (true)
        {
            int index = Random.Range(0, clips.Length);
            AnimationClip clip = clips[index];
            animator.Play(clip.name);
            float delay = Random.Range(-0.5f, 0.5f) + averageDelay;
            yield return new WaitForSeconds(clip.length + delay);
        }
    }
}
