using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour {   

    public Animator TitleAnimation;
    public Animator CameraAnimation;

    public GameObject PlayerFront;
    public GameObject PlayerBack;
    public GameObject TextboxObject;

    public TextBox CutsceneTextbox;
    public GameObject VillagerChat;
    public GameObject CrusaderChat;
    public Text ButtonText;

    int sceneNumber = -1;
    int sentenceNumber = 0;

    string[] sentences =
        {
            "WAIT! Please, sir. Sir Knight!",
            "Thank god you stopped, my village, it's ... it's been attacked by goblins! Please help!",
            "Did I hear you correctly? Your village is being attacked by heretics?!",
            "What? No they're just goblins...",
            "You're saying no, but I keep hearing you say heretics.",
            "Fine, whatever, if it'll get you to help than yes. My village is being attacked by heretics.",
            "DEUS VULT"
        };

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        TitleAnimation.SetTrigger("GameStart");
        CameraAnimation.SetTrigger("GameStart");
        StartCoroutine(WaitForSeconds(2.5f));

    }

    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ContinueScene();
    }

    public void ContinueScene()
    {
        sceneNumber++;

        if (sceneNumber == 0)
        {
            TextboxObject.SetActive(true);
        }
        else if (sceneNumber == 1)
        {
            StartCoroutine(WaitForSeconds(0.5f));
            PlayerBack.SetActive(false);
            PlayerFront.SetActive(true);            
            return;
        }
        else if (sceneNumber == 7)
        {

            CameraAnimation.SetTrigger("DeusVult");

            StartCoroutine(WaitForSeconds(1.2f));
            return;
        }
        else if (sceneNumber == 8)
        {
            VillagerChat.SetActive(false);
            CrusaderChat.SetActive(true);
            ButtonText.text = "Start";
            CutsceneTextbox.DisplaySentence(sentences[sentenceNumber], true);
            return;
        }
        else if (sceneNumber >= 9)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }

        else if (sceneNumber % 2 == 1)
        {
            VillagerChat.SetActive(false);
            CrusaderChat.SetActive(true);
        }
        else if (sceneNumber % 2 == 0)
        {
            VillagerChat.SetActive(true);
            CrusaderChat.SetActive(false);
        }

        CutsceneTextbox.DisplaySentence(sentences[sentenceNumber]);
        sentenceNumber++;
    }
}
