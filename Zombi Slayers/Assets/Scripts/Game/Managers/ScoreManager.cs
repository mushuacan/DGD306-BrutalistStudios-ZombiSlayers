using DG.Tweening;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public Scriptable_Scoring scorer;

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
        DontDestroyOnLoad(gameObject); // Ýsteðe baðlý: sahneler arasý korumak için
    }

    private void Start()
    {
        StartAutoScore(scorer.scoreEarnedPerSecond, 1f);

        zombiATBM = FindAnyObjectByType<ZombiAtTheBack_Manager>(); 
    }

    public void AddScore(int amount, string scoreType = "Kill")
    {
        score += (int)(amount * scorer.scoreMultiplier);
        Debug.Log(score);

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
    }

    public int GetScore()
    {
        return score;
    }


    public void StartAutoScore(int amount, float interval)
    {
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
