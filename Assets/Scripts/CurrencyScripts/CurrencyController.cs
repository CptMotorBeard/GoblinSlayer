using TMPro;
using UnityEngine;

public class CurrencyController : MonoBehaviour {

    #region Singleton
    public static CurrencyController instance;
    private void Awake()
    {
        if (instance)
            return;
        instance = this;
    }
    #endregion

    int score = 0;
    int money = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI moneyText;

    public void increaseScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    public void increaseMoney(int value)
    {
        money += value;
        moneyText.text = money.ToString();
    }

    public int GetScore()
    {
        return score;
    }

    public int GetMoney()
    {
        return money;
    }
}
