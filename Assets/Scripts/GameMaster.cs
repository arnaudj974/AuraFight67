using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    private Coroutine corout;
    public TextMeshProUGUI txtCentral;
    public Button btnNext;
    public int timeIntro = 3;
    public float timeGame = 10f;
    public bool gameFinie = false;
    public bool gameStarted = false;
    public int res;
    void Start()
    {

    }

    void Update()
    {

    }

    public void StartGameMaster()
    {
        corout = StartCoroutine(LaunchGameMaster());
    }

    public void StopGameMaster()
    {
        StopCoroutine(corout);
    }

    public IEnumerator LaunchGameMaster()
    {
        btnNext.gameObject.SetActive(false);
        for (int i = timeIntro; i > 0; i--) //decompte de depart
        {
            txtCentral.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        transform.localScale = new Vector3(0, 0, 0);
        gameStarted = true;
        yield return new WaitForSeconds(timeGame); //temps du jeu
        gameFinie = true;
        btnNext.gameObject.SetActive(true);
        btnNext.GetComponentInChildren<TextMeshProUGUI>().text = res == 0 ? "ADVERSAIRE SUIVANT" : "REESAYER";
        txtCentral.text = StringResult();
        transform.localScale = new Vector3(1, 1, 1);
    }

    public string StringResult()
    {
        switch (res)
        {
            case 0:return "VICTOIRE !";
            case 1:return "EGALITE !";
            case 2:return "DEFAITE !";
            default: return "";
        }
    }

    public void Clear()
    {
        gameFinie = false;
        gameStarted = false;
    }
}
