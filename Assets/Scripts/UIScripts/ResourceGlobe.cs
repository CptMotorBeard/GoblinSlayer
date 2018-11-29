using UnityEngine.UI;
using UnityEngine;

public class ResourceGlobe : MonoBehaviour {

    // Paremeters
    public Image GlobeBack;
    public Image GlobeTexture;
    public Image GlobeFront;

    public void SetGlobeValue(float percentage)
    {
        GlobeBack.fillAmount = percentage;
        GlobeTexture.fillAmount = percentage;
        GlobeFront.fillAmount = percentage;
    }
}
