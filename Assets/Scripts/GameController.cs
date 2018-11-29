using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour {

    PlayerStatus player;
    int numberOfDeaths = 0;
    bool gameSetup = false;

    public GameObject Spawners;
    public GameObject Tutorial;
    public GameObject TutorialDeath;

    public GameObject GameScreen;
    public GameObject Shop;

    // First death cutscene
    int sceneNumber = -1;
    int sentenceNumber = 0;

    public Animator CameraAnimation;
    public Animator GodAnimation;
    public TextBox DeathTextBox;
    public Text    NextButton;

    // Player Class and stats
    public PlayerClass SelectedClass;
    public Ability[] Abilities;

    string[] sentences =
    {
        "Awaken my child.",
        "Ahahah, I bet you're surprised to see me, you died you see.",
        "But that's not good for any of us so here's what I'm going to do.",
        "I'll reset time to before the goblins attacked.",
        "Keep your donations from the goblins you already killed and visit the shop this time.",
        "Good luck, and remember...",
        "MEIS VULT"
    };

    #region Singleton
    public static GameController instance;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }
    #endregion

    // Use this for initialization
    private void Start()
    {
        SetupGame();
    }

    private void SetupGame()
    {
        if (gameSetup) { return; }
        gameSetup = true;

        player = PlayerStatus.instance;
        player.SetupPlayer(SelectedClass);
        player.GetComponent<PlayerController>().SetupController(Abilities);
        if (numberOfDeaths <= 0)
        {
            Tutorial.SetActive(true);
        }
        else
        {
            Spawners.SetActive(true);
        }
    }

    public void SetupGame(int numberOfDeaths,  PlayerClass playerclass, Ability[] abilities)
    {
        if (gameSetup) { return; }        

        this.numberOfDeaths = numberOfDeaths;

        SelectedClass = playerclass;
        Abilities = abilities;

        SetupGame();
    }

    public void PlayerDeath()
    {
        numberOfDeaths++;
        GameScreen.SetActive(false);
        player.gameObject.SetActive(false);
        if (numberOfDeaths == 1)
        {
            CameraAnimation.SetTrigger("FirstDeath");
            StartCoroutine(WaitForSeconds(1.0f));
        } else
        {
            BringUpShop();
        }
    }

    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ContinueScene();
    }

    public void ContinueScene()
    {
        sceneNumber++;
        if (sceneNumber == 0) { TutorialDeath.SetActive(true); }
        if (sceneNumber == 1) { GodAnimation.SetTrigger("FadeIn"); StartCoroutine(WaitForSeconds(1.0f)); return; }
        if (sceneNumber == 7) {
            NextButton.text = "DEUS VULT";
            DeathTextBox.DisplaySentence(sentences[sentenceNumber], true);
            return;
        }
        if (sceneNumber >= 8) {
            TutorialDeath.SetActive(false);
            GodAnimation.gameObject.SetActive(false);
            BringUpShop();
            return;
        }

        DeathTextBox.DisplaySentence(sentences[sentenceNumber]);
        sentenceNumber++;
    }


    public void BringUpShop()
    {
        Shop.SetActive(true);
    }

    public void ResetLevel()
    {
        PersistantData.instance.PrepareToLoad();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DeusVultMode()
    {
        PlayerClass t = new PlayerClass();
        // [ Defensive Options ]
        t.MaximumHealth = SelectedClass.MaximumHealth;
        t.DamageReduction = SelectedClass.DamageReduction;
        t.DamageResistance = SelectedClass.DamageResistance;
        t.LifePerKill = SelectedClass.LifePerKill;
        t.LifePerSecond = SelectedClass.LifePerSecond;
        t.CrowdControlReduction = SelectedClass.CrowdControlReduction;
        t.MaxCrowdControlReduction = SelectedClass.MaxCrowdControlReduction;

        // [ Offensive Options ]
        t.BaseDamage = SelectedClass.BaseDamage;
        t.CritDamage = SelectedClass.CritDamage;
        t.CritChance = SelectedClass.CritChance;

        // [ Magic Options ]
        t.MaximumEnergy = SelectedClass.MaximumEnergy;
        t.EnergyPerSecond = SelectedClass.EnergyPerSecond;
        t.CooldownReduction = SelectedClass.CooldownReduction;
        t.MaximumCooldownReduction = SelectedClass.MaximumCooldownReduction;
        t.SkillCostReduction = SelectedClass.SkillCostReduction;
        t.MaximumSkillCostReduction = SelectedClass.MaximumSkillCostReduction;

        // [ Misc  Options ]
        t.MoveSpeed = SelectedClass.MoveSpeed;
        t.DamageReflection = SelectedClass.DamageReflection;
        t.CurrentExperience = SelectedClass.CurrentExperience;
        t.Level = SelectedClass.Level;
        t.MaximumLevel = SelectedClass.MaximumLevel;

        //t.name = SelectedClass.name;

        t.MaximumHealth = 10000;
        t.MaximumEnergy = 10000;

        t.BaseDamage = 5000;
        t.CooldownReduction = 95;
        t.MaximumSkillCostReduction = 95;

        t.EnergyPerSecond = 50;
        t.LifePerSecond = 50;

        t.CritChance = 100;
        t.CritDamage = 500;

        SelectedClass = t;
    }
}