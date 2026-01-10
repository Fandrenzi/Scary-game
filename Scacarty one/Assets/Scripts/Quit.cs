using UnityEngine;

public class Quit : MonoBehaviour
{
    public void quitGame()
    {
        Application.Quit();

        Debug.Log("Left game");
    }
}
