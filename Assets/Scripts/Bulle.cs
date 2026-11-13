using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Bulle : MonoBehaviour
{
    [SerializeField] private Image arrow;
    [SerializeField] private Image image;
    private RectTransform rectImage;
    private RectTransform rectArrow;
    Vector2 inputAsked = Vector2.one;
    public bool coroutStarted { get; private set; }
    public event Action<float> OnBulleStart;
    public event Action<int,bool> OnBulleFind;
    Controller[] controllers;

    void OnEnable()
    {
        controllers = FindObjectsByType<Controller>();
        foreach (var c in controllers) c.OnMoveInput += CheckInput;
    }

    void OnDisable()
    {
        if (controllers == null) return;
        foreach (var c in controllers)
            if (c != null) c.OnMoveInput -= CheckInput;
    }
    public void CheckInput(int index,Vector2 input)
    {
        if (coroutStarted)
        {
            OnBulleFind?.Invoke(index,input == inputAsked);
        }
    }
    void Awake()
    {
        rectImage = image.GetComponent<RectTransform>();
        rectArrow = arrow.GetComponent<RectTransform>();
        image.enabled = false;
        arrow.enabled = false;
    }

    private void Show()
    {
        switch (UnityEngine.Random.Range(1, 5))
        {
            case 1:
                inputAsked = Vector2.up;
                arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 2:
                inputAsked = Vector2.left;
                arrow.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 3:
                inputAsked = Vector2.down;
                arrow.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case 4:
                inputAsked = Vector2.right;
                arrow.transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
            default:
                inputAsked = Vector2.one;
                break;
        }
        image.enabled = true;
        arrow.enabled = true;
    }

    private void Hide()
    {
        arrow.enabled = false;
        inputAsked = Vector2.one;
        image.enabled = false;
    }

    public void StartBulle(float waitTime, float screenTime)
    {
        StartCoroutine(SpawnBulle(waitTime, screenTime));
    }

    public IEnumerator SpawnBulle(float waitTime, float screenTime)
    {
        coroutStarted = true;
        Hide();
        yield return new WaitForSeconds(waitTime);
        Show();
        PositionRandom();
        OnBulleStart?.Invoke(screenTime);
        yield return new WaitForSeconds(screenTime);
        Hide();
        coroutStarted = false;
    }

    public void PositionRandom()
    {
        Vector3 v = new Vector3(UnityEngine.Random.Range(-130f, 130f), UnityEngine.Random.Range(-200f, 200f), 0);
        rectImage.anchoredPosition = v;
        rectArrow.anchoredPosition = v;
        arrow.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }


}
