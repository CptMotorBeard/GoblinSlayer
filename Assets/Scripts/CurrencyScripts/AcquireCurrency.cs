using UnityEngine;

public class AcquireCurrency : MonoBehaviour {

    CurrencyController currency;

    private void Start()
    {
        currency = CurrencyController.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.tag == "Money")
        {
            currency.increaseMoney(collision.GetComponent<Coin>().value);
            Destroy(collision.gameObject);
        }
    }
}