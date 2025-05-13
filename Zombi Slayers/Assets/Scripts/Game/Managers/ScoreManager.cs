using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public Scriptable_Scoring scorer;
    public TextMeshProUGUI scoreText;
    public bool debug;
    public bool IncreaseEverySecond;

    private ZombiAtTheBack_Manager zombiATBM;

    private Tween scoreLoopTween;

    private int score = 0;

    [Header("Private Deðiþkenler")]
    [SerializeField] private int scoreFromZombiATB;
    [SerializeField] private int scoreFromTime;
    [SerializeField] private int scoreFromKillingZombi;
    [SerializeField] private int scoreUnknown;

    private void Awake()
    {
        // Singleton kontrolü
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this; 
        //DontDestroyOnLoad(gameObject); // Ýsteðe baðlý: sahneler arasý korumak için
    }

    private void Start()
    {
        StartAutoScore(scorer.scoreEarnedPerSecond, 1f);

        zombiATBM = FindAnyObjectByType<ZombiAtTheBack_Manager>();

        scoreText.fontSizeMax = 79;
    }

    private void RewriteScore(bool hype = false)
    {
        scoreText.text = "Score: " + score;
        scoreText.fontSize = 36 + (score * 0.005f);
        scoreText.transform.DOKill(); // Eski animasyonu iptal et
        scoreText.transform.localScale = Vector3.one; // Ölçeði sýfýrla
        scoreText.transform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 10, 1);
    }
    public void AllPlayersDied()
    {
        scoreText.transform.DOKill(); // Eski animasyonu iptal et
        scoreText.transform.localScale = Vector3.one; // Ölçeði sýfýrla
    }

    public void AddScore(int amount, string scoreType = "Kill")
    {
        score += (int)(amount * scorer.scoreMultiplier);
        if (debug) Debug.Log(score);

        if (scoreType == "Kill")
        {
            scoreFromKillingZombi += amount;
        }
        else if (scoreType == "ZombiATB")
        {
            scoreFromZombiATB += amount;
        }
        else if (scoreType == "Time")
        {
            scoreFromTime += amount;
        }
        else
        {
            scoreUnknown += amount;
        }
        if (amount < 49)
        {
            RewriteScore();
        }
        else
        {
            RewriteScore(true);
        }
    }

    public int GetScore()
    {
        return score;
    }


    public void StartAutoScore(int amount, float interval)
    {
        if (!IncreaseEverySecond) { return; }

        StopAutoScore(); // Önce varsa iptal et

        scoreLoopTween = DOVirtual.DelayedCall(interval, () =>
        {
            AddScore(amount, "Time");
            AddScore((zombiATBM.GetZombiCount() * scorer.scoreEarnedPerZombie), "ZombiATB");
            StartAutoScore(amount, interval); // Kendini tekrar çaðýr
        });
    }

    public void StopAutoScore()
    {
        if (scoreLoopTween != null && scoreLoopTween.IsActive())
        {
            scoreLoopTween.Kill();
            scoreLoopTween = null;
        }
    }
}
