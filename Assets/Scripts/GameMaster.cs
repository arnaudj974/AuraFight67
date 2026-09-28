using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    private Coroutine corout;
    public TextMeshProUGUI txtCentral;
    private TextMeshProUGUI txtBtn;
    private Image imgBtn;
    public Image imgZQSD;
    public Image imgArrows;
    public AudioSource audio;
    public Button btnNext;
    public Button btnRetry;
    public int timeIntro = 3;
    public float timeGame = 10f;
    public bool gameFinie = false;
    public bool gameStarted = false;
    public int res=5;
    public Sprite spriteGreen;
    public Sprite spriteRed;
    public bool pvp;
    void Start()
    {
        txtBtn = btnNext.GetComponentInChildren<TextMeshProUGUI>();
        imgBtn = btnNext.GetComponent<Image>();
    }

    void Update()
    {

    }

    public void StartGameMaster(float _timeGame)
    {
        corout = StartCoroutine(LaunchGameMaster(_timeGame));
    }

    public void StopGameMaster()
    {
        StopCoroutine(corout);
    }

    public IEnumerator LaunchGameMaster(float _timeGame)
    {
        transform.localScale = new Vector3(1, 1, 1);
        imgZQSD.enabled = true;
        if (pvp) { imgArrows.enabled = true; }
        btnRetry.gameObject.SetActive(false);
        btnNext.gameObject.SetActive(false);
        txtCentral.fontSize = 300f;
        if (audio.isPlaying) { audio.Stop(); }
        audio.Play();
        for (int i = timeIntro; i > 0; i--) //decompte de depart
        {
            txtCentral.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        btnRetry.gameObject.SetActive(true);
        transform.localScale = new Vector3(0, 0, 0);
        gameStarted = true;
        yield return new WaitForSeconds(_timeGame); //temps du jeu
        gameFinie = true;
        btnNext.gameObject.SetActive(true);
        txtCentral.fontSize = 200f;
        txtCentral.text = StringResult();
        ChangeUI();
        imgZQSD.enabled = false;
        if (pvp) { imgArrows.enabled = false; }
        transform.localScale = new Vector3(1, 1, 1);
    }

    public string StringResult()
    {
        switch (res)
        {
            case 0:return !pvp ? "YOU WIN !" : "PLAYER 1 WIN";
            case 1:return "DRAW !";
            case 2:return !pvp ? "YOU LOSE !" : "PLAYER 2 WIN";
            default: return "";
        }
    }

    public void ChangeUI()
    {
        if (!pvp)
        {
            switch (res) //change le panel suivant le score du joueur
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

    public void Clear()
    {
        gameFinie = false;
        gameStarted = false;
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

}
