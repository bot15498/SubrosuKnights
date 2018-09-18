using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{

    public Canvas selectMenu;
    public Button playButt;
    public Button quitButt;
    public InputField inputField;
    public static string songFilePath;

    void Start()
    {
        selectMenu = selectMenu.GetComponent<Canvas>();
        playButt = playButt.GetComponent<Button>();
        quitButt = quitButt.GetComponent<Button>();
        selectMenu.enabled = false;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SelectSongHandler()
    {
        selectMenu.enabled = true;
        playButt.enabled = false;
        quitButt.enabled = false;
    }

    public void DefaultSongSelect(string sceneName)
    {
        songFilePath = Application.dataPath + @"\resources\songMaps\[Hatsune Miku] Rolling Girl\song.mp3";
        SceneManager.LoadScene(sceneName);
    }

    public void SongSelect(string sceneName)
    {
        songFilePath = inputField.text;
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
