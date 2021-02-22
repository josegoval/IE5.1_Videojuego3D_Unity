using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationsController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetIsWalking(bool state)
    {
        animator.SetBool(EnemyTags.IS_WALKING_ANIMATOR_PARAMETER, state);
    }
    public void SetIsRunning(bool state)
    {
        animator.SetBool(EnemyTags.IS_RUNNING_ANIMATOR_PARAMETER, state);
    }
    public void TriggerWantToAttack()
    {
        animator.SetTrigger(EnemyTags.WANT_TO_ATTACK_ANIMATOR_PARAMETER);
    } public void TriggerIsDead()
    {
        animator.SetTrigger(EnemyTags.IS_DEAD_ANIMATOR_PARAMETER);
    }
}
