using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverUIPanel;
    public TextMeshProUGUI bestScoreText;

    GameObject generator;
    bool isGameOver = false;
    float time = 60.0f;
    int point = 0;

    void Start()
    {
        generator = GameObject.Find("ItemGenerator");
    }

    void Update()
    {
        time -= Time.deltaTime;

        if (time < 0)
        {
            time = 0;
            EndGame();
        }
        else if (time >= 0 && time < 4)
        {
            generator.GetComponent<itemGenerator>().SetParamters(0.3f, -0.06f, 3);
        }
        else if (time >= 4 && time < 12)
        {
            generator.GetComponent<itemGenerator>().SetParamters(0.5f, -0.05f, 6);
        }
        else if (time >= 12 && time < 23)
        {
            generator.GetComponent<itemGenerator>().SetParamters(0.8f, -0.04f, 4);
        }
        else if (time >= 23 && time < 30)
        {
            generator.GetComponent<itemGenerator>().SetParamters(1.0f, -0.03f, 2);
        }

        timeText.text = time.ToString("F1");
        scoreText.text = point.ToString() + " point";
    }

    public bool GetIsGameOver()
    {
        return isGameOver;
    }

    public void GetApple()
    {
        point += 50;
    }

    public void GetBanana()
    {
        point += 250;
    }

    public void GetBomb()
    {
        point /= 2;
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif    
    }
    
    public void EndGame()
    {
        isGameOver = true;
        gameOverUIPanel.SetActive(true);

        float bestScore = PlayerPrefs.GetFloat("BestScore");
        if (point > bestScore)
        {
            bestScore = point;
            PlayerPrefs.SetFloat("BestScore", bestScore);
        }
        bestScoreText.text = "Best : " + (int)bestScore;
    }
}
