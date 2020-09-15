using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField] Text scoreText;
    int score = 0;

    private void Start()
    {
        Enemy.OnDeath.AddListener(AddScore);
    }

    void AddScore(Enemy enemy)
    {
        score += enemy.enemyScoreValue;
        scoreText.text = score.ToString();
    }
}
