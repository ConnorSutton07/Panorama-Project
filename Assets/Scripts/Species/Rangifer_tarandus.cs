using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rangifer_tarandus : MonoBehaviour
{
    private AnimationClip[] clips;
    private Animator animator;
    private void Awake()
    {
        // Get the animator component
        animator = GetComponent<Animator>();

        // Get all available clips
        clips = animator.runtimeAnimatorController.animationClips;
    }

    private void Start()
    {
        PlayRandomly();
    }

    private IEnumerator PlayRandomly()
    {
        while (true)
        {
            int i = Random.Range(0, clips.Length);
            var randClip = clips[i];

            animator.Play(randClip.name);
            Debug.Log(i);

            // Wait until animation finished than pick the next one
            yield return new WaitForSeconds(randClip.length);
        }
    }
}
