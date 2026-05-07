using System;
using TMPro;
using Unity.FPS.Game;
using UnityEngine;

public class timer : MonoBehaviour
{
    [TextArea]
    [Tooltip("Doesn't do anything. Just comments shown in inspector")]
    public string Notice = "To reset highscore: \nEdit > Clear All PlayerPrefs";
    private TextMeshProUGUI m_TextMeshPro;
    private float m_Time;
    private float bestScore;
    private bool win = false;

    void Start()
    {
        m_TextMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        EventManager.AddListener<AllObjectivesCompletedEvent>(OnAllObjectivesCompleted);
        bestScore = PlayerPrefs.GetFloat("score");
        if(bestScore <= 0) bestScore = 86400;
    }

    void Update()
    {
        if (!win)
        {
            m_Time += Time.deltaTime;
            m_TextMeshPro.text = "Time: \n" + GenTimeSpanFromSeconds(m_Time) + "\n" + "Highscore: \n" + GenTimeSpanFromSeconds(bestScore);
        }
        else if(m_Time < bestScore)
        {
            m_TextMeshPro.text = "Highscore: \n" + GenTimeSpanFromSeconds(m_Time) + "\n" + "Previous best: \n" + GenTimeSpanFromSeconds(bestScore);
            Debug.Log("<color=green>"+m_TextMeshPro.text+"</color>");
            SaveScore();
        }
    }

    static String GenTimeSpanFromSeconds(float seconds)
    {
        string timeInterval = TimeSpan.FromSeconds(seconds).ToString();
        return timeInterval.Substring(0, timeInterval.Length - 4);
    }
    void OnAllObjectivesCompleted(AllObjectivesCompletedEvent evt)
    {
        win = true;
    }
    void SaveScore()
    {
        PlayerPrefs.SetFloat("score", m_Time);
        PlayerPrefs.Save();
        bestScore = m_Time;
    }
}
