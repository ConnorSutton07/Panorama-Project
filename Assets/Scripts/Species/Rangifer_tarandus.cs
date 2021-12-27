using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rangifer_tarandus : MonoBehaviour
{
    private Animator animator;
    private string currentState;
    float idle_length;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        idle_length = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

/*    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(idle_length * 2, idle_length * 5));
            animator.SetInteger("ActionIndex", Random.Range(0, 2));
            animator.SetBool("ActionTrigger", true);
        }
    }*/
}
