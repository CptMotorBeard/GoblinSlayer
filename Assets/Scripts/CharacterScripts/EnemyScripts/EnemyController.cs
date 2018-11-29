using UnityEngine;

public class EnemyController : MonoBehaviour {

    Transform player;

    // Parameters
    public Enemy         EnemyType;
    public Transform     Sprite;    
    public EnemyStatus   EnemyStat;
    public EnemyAttack[] EnemyAttacks;
    public float AttackDistance = 1.0f;

    private void Start()
    {
        player = PlayerStatus.instance.transform;
        foreach (EnemyAttack e in EnemyAttacks) { e.SetupEnemyAttack(PlayerStatus.instance, EnemyType); }
        EnemyStat.SetupEnemyStatus(EnemyType);
    }

    private void Update()
    {
        if (player == null) { return; }

        Vector3 normal = Vector3.Normalize(transform.position - player.position);
        normal *= AttackDistance;

        transform.position = Vector2.MoveTowards(transform.position, (player.position + normal), Time.deltaTime * EnemyType.Speed);
        Sprite.eulerAngles = new Vector3(0, 0, 90 + Mathf.Rad2Deg * Mathf.Atan2(transform.position.y - player.position.y, transform.position.x - player.position.x));   
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackDistance);
    }
}
