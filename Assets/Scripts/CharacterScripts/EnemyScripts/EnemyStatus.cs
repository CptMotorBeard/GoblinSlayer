using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour {

    CurrencyController currency;
    Enemy enemyType;
    
    float currentHealth;
    bool dead = false;
    bool markedForCleanup = false;
    bool currentlyRegenerating = false;
    bool waitingtoRegenerate = true;
    float regenerationDelay = 2.0f;
    float currentRegenerationDelay = 0;

    // Parameters    
    public Healthbar  Healthbar;
    public GameObject Coin;

    public float DropRadius = 0.3f;

    public void SetupEnemyStatus(Enemy enemyType)
    {
        this.enemyType = enemyType;
        currentHealth = enemyType.MaximumHealth;
        Healthbar.SetupHealthbar((int)enemyType.MaximumHealth);
    }

    private void Start()
    {
        currency = CurrencyController.instance;
    }

    /// <summary>
    /// A simple time slow effect, start coroutine when the enemy is hit by an attack
    /// </summary>
    IEnumerator HitEffect()
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(0.05f);
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (dead) { if (markedForCleanup) { Destroy(gameObject); } return; }

        if (waitingtoRegenerate)
        {
            WaitToRegen();
        }

        if (currentlyRegenerating)
        {
            currentHealth += 1;
            if (currentHealth >= enemyType.MaximumHealth)
            {
                currentHealth = enemyType.MaximumHealth;
                currentlyRegenerating = false;
            }
            Healthbar.SetHealthbar((int)currentHealth);
        }
    }

    /// <summary>
    /// Deals a specified amount of damage to this enemy. Defence calculations are dealt with within
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        // Deal with damage reduction
        // StartCoroutine(HitEffect());

        currentRegenerationDelay = 0;
        waitingtoRegenerate = true;
        currentlyRegenerating = false;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }

        Healthbar.SetHealthbar((int)currentHealth);
    }

    void WaitToRegen()
    {
        currentRegenerationDelay += Time.deltaTime;
        if (currentRegenerationDelay >= regenerationDelay)
        {
            currentRegenerationDelay = 0;
            currentlyRegenerating = true;
        }
    }

    /// <summary>
    /// Removes the enemy, spawns coins based on their value and increases players score
    /// </summary>
    private void Die()
    {
        dead = true;
        Vector2 ourPosition = transform.position;
        PlayerStatus.instance.EnemyKilled();

        int numberOfCoins = Random.Range(enemyType.MinCoinsDropped, enemyType.MaxCoinsDropped + 1);
        for (int i = 0; i < numberOfCoins; i++)
        {
            Vector2 randomPoint = ourPosition + Random.insideUnitCircle * DropRadius;
            GameObject coin = Instantiate(Coin, transform.position, Quaternion.identity, this.transform.parent);

            Coin c = coin.GetComponent<Coin>();
            c.value = Random.Range(enemyType.CoinValueMin, enemyType.CoinValueMax + 1);
            c.destination = randomPoint;
        }

        Time.timeScale = 1.0f;
        currency.increaseScore(enemyType.PointValue);

        transform.position = new Vector3(1000, 1000, 1000);
        markedForCleanup = true;
        // Move far far away, they get destroyed in the next frame
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, DropRadius);
    }
}
