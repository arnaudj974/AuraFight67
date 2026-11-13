using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeCanvas : MonoBehaviour
{
    [SerializeField] private int timeIntro = 3;
    [SerializeField] private Game game;

    [Header("Canvas")]
    [SerializeField] private TextMeshProUGUI txtCentral;
    [SerializeField] private TextMeshProUGUI txtLevel;
    [SerializeField] private Button btnNext;
    [SerializeField] private Button btnRetry;
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI txtBtn;
    [SerializeField] private Image imgBtn;
    [SerializeField] private Sprite spriteGreen;
    [SerializeField] private Sprite spriteRed;
    AudioSource audio;
    bool pvp;
    int result;
    public UnityEvent OnJeuStart;

    void Awake()
    {
        pvp = GameManager.Instance.mode == Mode.PvP;
        audio = GetComponentInChildren<AudioSource>();
    }

    IEnumerator Start()
    {
        if (!pvp) txtLevel.text = "LEVEL " + GameManager.Instance.lvl;
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
        OnJeuStart?.Invoke();
    }

    public void ChangeFinalPanel()
    {
        btnNext.gameObject.SetActive(true);
        txtCentral.fontSize = 200f;
        result = game.GetResult();
        txtCentral.text = StringResult();
        ChangeUI();
        panel.SetActive(true);
    }

    string StringResult()
    {
        switch (result)
        {
            case 0: return !pvp ? "YOU WIN !" : "PLAYER 1 WIN";
            case 1: return "DRAW !";
            case 2: return !pvp ? "YOU LOSE !" : "PLAYER 2 WIN";
            default: return "";
        }
    }

    void ChangeUI()
    {
        switch (GameManager.Instance.mode)
        {
            case Mode.PvP:
                txtBtn.text = "NEXT";
                imgBtn.sprite = spriteGreen;
                break;
            case Mode.Campagne:
                switch (result) //change le panel suivant le score du joueur
                {
                    case 0:
                        txtBtn.text = "NEXT";
                        imgBtn.sprite = spriteGreen;
                        GameManager.Instance.lvl++;
                        break;
                    case 1 or 2:
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
                break;
            case Mode.Infini:
                switch (result) //change le panel suivant le score du joueur
                {
                    case 0 or 1:
                        txtBtn.text = "NEXT";
                        imgBtn.sprite = spriteGreen;
                        GameManager.Instance.lvl++;
                        break;
                    case 2:
                        txtBtn.text = "RETRY";
                        imgBtn.sprite = spriteRed;
                        break;
                }
                break;
        }
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

    public void ReloadScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
