using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private Text timeText;
    [SerializeField] private Text nowScoreText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private Animator anim;
    [SerializeField] private ParticleSystem bombEffect;
    private float timer = 0f;

    private string key = "bestScore";
    public bool isPlay = true;
    private void Awake()
    {
        Time.timeScale = 1f;

        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        InvokeRepeating(nameof(MakeSquare), 0f, 1f);
    }

    private void Update()
    {
        if (!isPlay)
        {
            return;
        }

        timer += Time.deltaTime;
        timeText.text = $"{timer:F2}";
    }
    void MakeSquare()
    {
        Instantiate(squarePrefab);
    }

    private void SetBestScore() // 최고점수 저장
    {
        PlayerPrefs.SetFloat(key, timer);
        bestScoreText.text = $"{timer:F2}";
    }
    public void GameOver()
    {
        isPlay = false;
        anim.SetBool("isDie", true);
        bombEffect.Play();
        Invoke(nameof(TimeStop), 1f);

        nowScoreText.text = $"{timer:F2}";

        if (PlayerPrefs.HasKey(key)) // 최고점수가 존재한다면
        {
            float bestScore = PlayerPrefs.GetFloat(key);
            if (bestScore < timer)
            {
                SetBestScore();
            }
            else
            {
                bestScoreText.text = $"{bestScore:F2}";
            }

        }
        else
        {
            SetBestScore();
        }

        endPanel.SetActive(true);

    }

    private void TimeStop()
    {
        Time.timeScale = 0f;
    }
}
