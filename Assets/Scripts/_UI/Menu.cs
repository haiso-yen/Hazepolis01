using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool IsShow;

     void Start()
    {
        pauseMenu.SetActive(false);
        IsShow = false;
        Time.timeScale = (1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsShow)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void StartGame()
    {
        Debug.Log("���U'�}�l'���s");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void QuitGame()
    {
        Application.Quit();
        //EditorApplication.isPlaying = false; 

        //�o�O�]��Application.Quit�O�b�C��Build��~�|���@�ΡA�s�説�A�U�O�L�Ī�
        // �p�G�Q�n�b�s�説�A�U�������檺�ܭn�ϥΨ�EditorApplication.isPlaying�o�ӰѼ�
    }

    public void UIEable()
    {
        GameObject.Find("Canvas/MainMenu/UI").SetActive(true);
    }

    public void PauseGame()
    {
        pauseMenu.gameObject.SetActive(true);
        IsShow = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        IsShow = false;
        Time.timeScale = 1f;
    }

}
