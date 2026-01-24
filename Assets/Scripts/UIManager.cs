using UnityEngine;

public class UIManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject colorUI;

    private void Start()
    {
        score = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

    public void Pause()
    {
        Time.timeScale = 0;
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
        Time.timeScale = 1.0f;
    }
}
