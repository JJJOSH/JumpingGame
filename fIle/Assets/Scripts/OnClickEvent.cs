using TMPro;
using UnityEngine;

public class OnClickEvent : MonoBehaviour
{
    public TextMeshProUGUI soundsText;

    void Start()
    {
        if (GameManager.mute)
        {
            soundsText.text = "/";
        }
        else
        {
            soundsText.text = "";
        }
    }


    public void ToggleMute()
    {
        if (GameManager.mute)
        {
            GameManager.mute = false;
            soundsText.text = "";
        }
        else
        {
            GameManager.mute = true;
            soundsText.text = "/";
        }
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
