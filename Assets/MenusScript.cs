using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusScript : MonoBehaviour
{

    public enum menuState {Mainmenu, Play, Sound, Stats, Skins, Pause, SoundIngame}
    public GameObject[] menus;

    private void Awake()
    {
        ChangeMenu(menus[(int)menuState.Mainmenu]);
        Time.timeScale = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void ChangeMenu(GameObject menuToActivate)
    {
        foreach (GameObject menu in menus)
        {
            if (menu.name == menuToActivate.name)
            {
               menu.SetActive(true);
            }
            else
            {
                menu.SetActive(false);
            }
        }
    }

    public void loadPlayMenu() {
        ChangeMenu(menus[(int)menuState.Play]);
        Time.timeScale = 1;
    }
    public void loadSoundMenu() {
        ChangeMenu(menus[(int)menuState.Sound]);
    }
    public void loadStatsMenu() {
        ChangeMenu(menus[(int)menuState.Stats]);
    }
    public void loadSkinsMenu() {
        ChangeMenu(menus[(int)menuState.Skins]);
    }
    public void loadMainMenu()
    {
        ChangeMenu(menus[(int)menuState.Mainmenu]);
    }
    public void loadPauseMenu()
    {
        ChangeMenu(menus[(int)menuState.Pause]);
    }
    public void loadSoundMenuInGame()
    {
        ChangeMenu(menus[(int)menuState.SoundIngame]);
    }
    public void loadMainMenuInGame()
    {
       // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    

    public void quitButton() { }


}
