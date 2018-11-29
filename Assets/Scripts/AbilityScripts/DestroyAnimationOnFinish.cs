using UnityEngine;

public class DestroyAnimationOnFinish : StateMachineBehaviour {
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        Destroy(animator.gameObject);
    }
}
