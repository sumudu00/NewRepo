using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Button ReplayButton;

    //score animation
    float scoreAnimTicks = 0, smoothStepAnim = 0, scoreAnimTickMaxInv = 0;
    [SerializeField] float scoreAnimTickmax = 0.5f;
    Vector2 animVector;
    Vector2 scoretextAnimInitScale;
    bool scoreIsAnimating;
    [SerializeField] float scoreAnimScale = 0.5f;

    void Start()
    {
        ReplayButton.onClick.AddListener(OnClickReplay);
        scoreAnimTickMaxInv = 1/scoreAnimTickmax;
        scoretextAnimInitScale = scoreText.rectTransform.localScale;
    }

    public void SetScoreText(int value)
    {
        scoreText.text = value.ToString();
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    void OnClickReplay()
    {
        GameManager.Instance.enemySpawner.ResetAllEnemies();
        GameManager.Instance.scoreManager.SetScore(0);
        SetScoreText(0);
        GameManager.Instance.gameStateManager.SetState(GameStates.Playing);
        GameManager.Instance.player.gameObject.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void StartScoreAnimation()
    {
        if(scoreIsAnimating == true)return;
        scoreAnimTicks = 0f;
        scoreIsAnimating = true;
    }

    void Update()
    {

        if(scoreIsAnimating == false) return;

        scoreAnimTicks += Time.deltaTime;
        scoreAnimTicks = scoreAnimTicks > scoreAnimTickmax ? scoreAnimTickmax : scoreAnimTicks;

        smoothStepAnim = scoreAnimTicks * scoreAnimTickMaxInv;
        smoothStepAnim = 4*smoothStepAnim - 4*smoothStepAnim*smoothStepAnim;

        animVector.x = smoothStepAnim;
        animVector.y = smoothStepAnim;

        scoreText.rectTransform.localScale = scoretextAnimInitScale + animVector*scoreAnimScale;

        if(scoreAnimTicks == scoreAnimTickmax){
            scoreAnimTicks = 0;
            scoreIsAnimating = false;
        }
    }
}
