using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeCanvas : MonoBehaviour
{
    [SerializeField] private int timeIntro = 3;
    [SerializeField] private Game game;

    [Header("Canvas")]
    [SerializeField] private TextMeshProUGUI txtCentral;
    [SerializeField] private Button btnNext;
    [SerializeField] private Button btnRetry;
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI txtBtn;
    [SerializeField] private Image imgBtn;
    [SerializeField] private Sprite spriteGreen;
    [SerializeField] private Sprite spriteRed;
    AudioSource audio;
    bool endChange;

    void Awake()
    {
        audio = GetComponentInChildren<AudioSource>();
    }

    IEnumerator Start()
    {
        GameManager.Instance.time = GameManager.Time.Intro;
        panel.SetActive(true);
        btnRetry.gameObject.SetActive(false);
        btnNext.gameObject.SetActive(false);
        txtCentral.fontSize = 300f;
        audio.Play();
        for (int i = timeIntro; i > 0; i--) 
        {
            txtCentral.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        btnRetry.gameObject.SetActive(true);
        panel.SetActive(false);
        GameManager.Instance.time = GameManager.Time.Jeu;
    }

    void Update()
    {
        if (GameManager.Instance.time != GameManager.Time.Fin) return;
        if (!endChange) ChangeFinalPanel();
    }

    void ChangeFinalPanel()
    {
        endChange = true;
        btnNext.gameObject.SetActive(true);
        txtCentral.fontSize = 200f;
        txtCentral.text = StringResult();
        ChangeUI();
        panel.SetActive(true);
    }

    string StringResult()
    {
        bool pvp = GameManager.Instance.mode == GameManager.Mode.PvP;
        switch (game.GetResult())
        {
            case 0: return !pvp ? "YOU WIN !" : "PLAYER 1 WIN";
            case 1: return "DRAW !";
            case 2: return !pvp ? "YOU LOSE !" : "PLAYER 2 WIN";
            default: return "";
        }
    }

    void ChangeUI()
    {
        bool pvp = GameManager.Instance.mode == GameManager.Mode.PvP;
        if (!pvp)
        {
            switch (game.GetResult()) //change le panel suivant le score du joueur
            {
                case 0:
                    txtBtn.text = "NEXT";
                    imgBtn.sprite = spriteGreen;
                    break;
                case 1:
                case 2:
                    txtBtn.text = "RETRY";
                    imgBtn.sprite = spriteRed;
                    break;
                case 3:  // cas de la victoire finale
                    imgBtn.sprite = spriteRed;
                    txtBtn.text = "QUIT";
                    txtCentral.fontSize = 150f;
                    txtCentral.text = "YOU CANT BEAT THE GOAT";
                    btnNext.onClick.AddListener(LoadMenuScene);
                    break;
            }
        }
        else
        {
            txtBtn.text = "NEXT";
            imgBtn.sprite = spriteGreen;
        }
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
