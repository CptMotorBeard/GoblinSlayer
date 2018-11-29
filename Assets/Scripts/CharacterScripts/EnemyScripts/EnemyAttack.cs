using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    PlayerStatus player;
    Enemy        enemyType;

    // Parameters
    public Animator animator;

    public void SetupEnemyAttack(PlayerStatus player, Enemy enemyType)
    {
        this.player    = player;
        this.enemyType = enemyType;
    }

    // When the player enters this creatures attack range start attacking
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("Attacking", true);
        }
    }

    // When the player leaves this creatures attack range stop attacking
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("Attacking", false);
        }
    }

    // When this creature attacks it always hits the player
    public virtual void Attack()
    {
        if (player == null) { return; }
        player.TakeDamage(enemyType.Damage);
    }
}
