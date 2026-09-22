using UnityEngine;
using System.Collections;
using TMPro;

public class Bubble : MonoBehaviour
{
    public float screenTime;
    public float waitTime;
    private char inputAsked;
    public Coroutine corout;

    public TextMeshProUGUI txt;

    private SpriteRenderer sp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        sp.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public char GetInputAsked()
    {
        return inputAsked;
    }

    public int ChooseInput()
    {
        return Random.Range(1, 5);
    }

    private void Show()
    {
        switch (ChooseInput())
        {
            case 1:
                inputAsked = 'w';
                txt.text = "Z";
                break;
            case 2:
                inputAsked = 'a';
                txt.text = "Q";
                break;
            case 3:
                inputAsked = 's';
                txt.text = "S";
                break;
            case 4:
                inputAsked = 'd';
                txt.text = "D";
                break;
            default:
                inputAsked = ' ';
                break;
        }
        sp.enabled = true;
    }

    private void Hide()
    {
        txt.text = "";
        inputAsked = ' ';
        sp.enabled = false;
    }

    public void StartBulle()
    {
        corout = StartCoroutine(SpawnBulle());
    }

    public void StopBulle()
    {
        StopCoroutine(corout);
    }

    public IEnumerator SpawnBulle()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Show();
            yield return new WaitForSeconds(screenTime);
            Hide();
        }
    }
}
