using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public bool canInterract = true;

    public void Fade()
    {
        canInterract = false;
        SceneManager.LoadScene("BeginingCutscene");
    }

    public void Play()
    {
        Debug.Log("played");
        SceneManager.LoadScene("Ze Birds");
    }

    public void Quit()
    {
        Debug.Log("left");
        Application.Quit();
    }
}
