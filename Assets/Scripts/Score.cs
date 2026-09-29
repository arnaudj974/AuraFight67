using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score = 0;
    public int mult = 1;
    public TextMeshProUGUI txt;
    private List<int> listMult = new List<int>();
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void AddScore(int points)
    {
        score += points * mult;
    }

    public void AddtoMult(int nb)
    {
        listMult.Add(nb);
        if (listMult.Count == 11)
        {
            listMult.RemoveAt(0);
        }
        mult=ReturnMult();
    }

    public int ReturnMult() //calculer le mult suivant le nombre de bons inputs réussis à la suite
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

    public void ShowScore()
    {
        txt.text = score.ToString() + " X " + mult.ToString();
    }

    public void Clear()
    {
        score = 0;
        mult = 1;
        txt.text=" ";
        listMult.Clear();
    }

    public bool Erreur()
    {
        if (listMult.Count==0) { return false; }
        return listMult[listMult.Count - 1] == 0;
    }
}
