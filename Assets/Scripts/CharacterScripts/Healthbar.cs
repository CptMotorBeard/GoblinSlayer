using UnityEngine.UI;
using UnityEngine;

public class Healthbar : MonoBehaviour {

    public Slider Slider;
    public Image  Fill;
    public Color  HealthColor;
    public Color  DamagedColor;

	void Start () {
        Fill.color = HealthColor;
	}

    /// <summary>
    /// Setup the health bar to have a maximum and current value of maxValue
    /// </summary>
    /// <param name="maxValue"></param>
    public void SetupHealthbar(int maxValue)
    {
        Slider.maxValue = maxValue;
        Slider.value = maxValue;
    }

    /// <summary>
    /// Decreases the value of the healthbar by amount
    /// </summary>
    /// <param name="amount"></param>
    public void DecreaseHealthbar(int amount)
    {
        Slider.value -= amount;

        if (Slider.value < (0.3 * Slider.maxValue)) { Fill.color = DamagedColor; }
    }

    /// <summary>
    /// Increases the value of the healthbar by amount
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseHealthbar(int amount)
    {
        Slider.value += amount;

        if (Slider.value >= (0.3 * Slider.maxValue)) { Fill.color = HealthColor; }
    }

    /// <summary>
    /// Set the healthbar to a specified value
    /// </summary>
    /// <param name="value"></param>
    public void SetHealthbar(int value)
    {
        Slider.value = value;

        if      (Slider.value >= (0.3 * Slider.maxValue)) { Fill.color = HealthColor; }
        else if (Slider.value <  (0.3 * Slider.maxValue)) { Fill.color = DamagedColor; }
    }
}
