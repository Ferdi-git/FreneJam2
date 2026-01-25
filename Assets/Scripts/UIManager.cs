using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private int score = 0;
    private bool lost  = false;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject colorUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private TextMeshProUGUI UIScore;
    [SerializeField] private TextMeshProUGUI UILostScore;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        score = 0;
        UIScore.text = score.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !lost)
        {
            if(Time.timeScale == 0)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    #region Score


    public void AddToScore(int toAdd)
    {
        score += toAdd;
        UIScore.text = score.ToString();
    }


    #endregion


    #region UI
    public void Pause()
    {
        Time.timeScale = 0;
        Player.Instance.canShoot = false;
        pauseUI.SetActive(true);

    }


    public void OpenColorPicker()
    {

        pauseUI.SetActive(false);
        colorUI.SetActive(true);
    }

    public void Back()
    {
        colorUI.SetActive(false);
        pauseUI.SetActive(true);
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        colorUI.SetActive(false);
        Player.Instance.canShoot = true;

        Time.timeScale = 1.0f;
    }


    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Lost()
    {
        Player.Instance.canShoot = false;


        Time.timeScale = 0f;

        lost = true;
        pauseUI.SetActive(false);
        colorUI.SetActive(false);
        loseUI.SetActive(true);

        UILostScore.text ="Score: " +  score.ToString();
    }

    #endregion
}
