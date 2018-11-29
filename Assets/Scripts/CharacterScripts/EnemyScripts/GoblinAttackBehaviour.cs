using UnityEngine;

public class GoblinAttackBehaviour : StateMachineBehaviour {

    GoblinAttack goblin;

    float attackTime = 0;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        goblin = animator.GetComponentInChildren<GoblinAttack>();
	}

    // Set the attached goblin to attack after each animation cycle
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        attackTime += Time.deltaTime;

        if (attackTime >= stateInfo.length)
        {
            attackTime = 0;
            goblin.Attack();
        }
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        attackTime = 0;
        goblin.Attack();
	}
}
