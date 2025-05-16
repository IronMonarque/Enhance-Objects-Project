using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void Play()
    {
        Debug.Log("played");
        SceneManager.LoadScene("Ze Birds");
    }

    /*public void Tutorial()
    {
        Debug.Log("tut");
        SceneManager.LoadScene("tutorial");
    }*/

    public void Quit()
    {
        Debug.Log("left");
        Application.Quit();
    }
}
