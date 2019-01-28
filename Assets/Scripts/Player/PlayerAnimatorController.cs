using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{

    Animator animator;
    AnimatorOverrideController overrideAnimator;

    [SerializeField] AnimationClip replaceableAttack;
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


}
