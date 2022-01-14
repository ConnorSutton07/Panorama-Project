using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimation : MonoBehaviour
{
    [SerializeField] float averageDelay;
    [SerializeField] float noise;
    private AnimationClip[] clips;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        clips = animator.runtimeAnimatorController.animationClips;
    }

    void Start()
    {
        animator.StopPlayback();
        StartCoroutine(PlayRandomAnimations());
    }

    private IEnumerator PlayRandomAnimations()
    {
        while (true)
        {
            int index = Random.Range(0, clips.Length);
            AnimationClip clip = clips[index];
            animator.Play(clip.name);
            float delay = Random.Range(-0.5f * noise, 0.5f * noise) + averageDelay;
            yield return new WaitForSeconds(clip.length + delay);
        }
    }
}
