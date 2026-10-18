using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bulle : MonoBehaviour
{
    [SerializeField] private Image arrow;
    [SerializeField] private Image image;
    private RectTransform rectThis;
    private RectTransform rectArrow;
    string inputAsked;
    Coroutine corout;
    public bool CoroutStarted { get; private set; }

    void Awake()
    {

        rectThis = GetComponent<RectTransform>();
        rectArrow = arrow.GetComponent<RectTransform>();
        image.enabled = false;
        arrow.enabled = false;
    }

    void Update()
    {

    }

    public string GetInputAsked()
    {
        return inputAsked;
    }

    private void Show()
    {
        switch (Random.Range(1, 5))
        {
            case 1:
                inputAsked = "up";
                arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 2:
                inputAsked = "left";
                arrow.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 3:
                inputAsked = "down";
                arrow.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case 4:
                inputAsked = "right";
                arrow.transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
            default:
                inputAsked = "";
                break;
        }
        image.enabled = true;
        arrow.enabled = true;
    }

    private void Hide()
    {
        arrow.enabled = false;
        inputAsked = "";
        image.enabled = false;
    }

    public void StartBulle(float waitTime, float screenTime)
    {
        corout = StartCoroutine(SpawnBulle(waitTime, screenTime));
    }

    public void StopBulle()
    {
        StopCoroutine(corout);
    }

    public IEnumerator SpawnBulle(float waitTime, float screenTime)
    {
        CoroutStarted = true;
        Hide();
        yield return new WaitForSeconds(waitTime);
        Show();
        PositionRandom();
        yield return new WaitForSeconds(screenTime);
        CoroutStarted = false;
    }

    public void Clear()
    {
        StopBulle();
        CoroutStarted = false;
        image.enabled = false;
        arrow.enabled = false;
        inputAsked = "";
    }

    public void PositionRandom()
    {
        Vector3 v = new Vector3(Random.Range(-130f, 130f), Random.Range(-200f, 200f), 0);
        rectThis.anchoredPosition = v;
        rectArrow.anchoredPosition = v;
        arrow.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
