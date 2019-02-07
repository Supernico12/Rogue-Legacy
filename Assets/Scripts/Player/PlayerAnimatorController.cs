using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{

    Animator animator;
    AnimatorOverrideController overrideAnimator;

    [SerializeField] AnimationClip replaceableAttack;
    [SerializeField] AnimationClip replacableIdle;
    [SerializeField] AnimationClip replacableMove;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        overrideAnimator = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideAnimator;
    }

    public void OverrideAttack(AnimationClip clip)
    {
        overrideAnimator[replaceableAttack.name] = clip;
    }

    public void OverrideIdle(AnimationClip clip)
    {
        overrideAnimator[replacableIdle.name] = clip;
    }

    public void OverrideMove(AnimationClip clip)
    {
        overrideAnimator[replacableMove.name] = clip;
    }


}
