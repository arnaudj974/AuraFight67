using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    public int score { get; private set; }
    public int mult { get; private set; }
    public TextMeshProUGUI txt;
    private List<int> listMult = new List<int>();
    public UnityEvent OnMultChanged;

    void Start()
    {
        mult = 1;
        score = 0;
    }
    public void UpdateScore(int points,int mult)
    {
        AddtoMult(mult);
        AddScore(points);
        ShowScore();
    }

    void AddScore(int points)
    {
        score += points * mult;
    }

    void AddtoMult(int nb)
    {
        listMult.Add(nb);
        if (listMult.Count == 11)
        {
            listMult.RemoveAt(0);
        }
        mult=ReturnMult();
        OnMultChanged?.Invoke();
    }

    int ReturnMult() //calculer le mult suivant le nombre de bons inputs réussis à la suite
    {
        int i = listMult.Count-1;
        int m = 1;
        while (i >= 0 && listMult[i] != 0)
        {
            m++;
            i--;
        }
        return m > 1 ? m / 2 : 1;
    }

    void ShowScore()
    {
        txt.text = score.ToString() + " X " + mult.ToString();
    }
}
