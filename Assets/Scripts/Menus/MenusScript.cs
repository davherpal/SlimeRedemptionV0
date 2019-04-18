using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenusScript : MonoBehaviour
{
   // Enum y el array de gameobject tienen el mismo, tener en cuenta los dos.
    public enum menuState {Mainmenu, Play, Sound, Stats, Skins, Pause, SoundIngame, GameOver}
    public GameObject[] menus;

    public Text statHeightTotal, statScoreTotal, statEnemiesTotal;
    public Text GameOverHeight, GameOverScore, GameOverEnemies;

    // Default Menu screen
    private void Awake()
    {
        ChangeMenu(menus[(int)menuState.Mainmenu]);
        Time.timeScale = 0;
       // ChangeMenu(menus[(int)menuState.Play]);
    }

    void Start()
    {
        // If player retry game
        if (RestartLevel.instance.retry)
        {
            ChangeMenu(menus[(int)menuState.Play]);
            Time.timeScale = 1;
        }

    }

    // Hides all canvas panels that are not the one we want
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

    // Load game menu
    public void loadPlayMenu() {
        ChangeMenu(menus[(int)menuState.Play]);
        Time.timeScale = 1;
    }

    // Loud sound setting menu
    public void loadSoundMenu() {
        ChangeMenu(menus[(int)menuState.Sound]);
    }

    // Load player stats menu
    public void loadStatsMenu() {
        ChangeMenu(menus[(int)menuState.Stats]);

        statEnemiesTotal.text = "Enemigos: " + RestartLevel.instance.enemiesKilled.ToString();
        statHeightTotal.text = "Altura: " + RestartLevel.instance.maxAltura.ToString("#.#");
        statScoreTotal.text = "Puntuacion: " + RestartLevel.instance.totalScore.ToString();
    }

    // Load Skins menu
    public void loadSkinsMenu() {
        ChangeMenu(menus[(int)menuState.Skins]);
    }

    // Load main menu without restarting scene
    public void loadMainMenu()
    {
        ChangeMenu(menus[(int)menuState.Mainmenu]);
    }

    // Load Pause Menu
    public void loadPauseMenu()
    {
        ChangeMenu(menus[(int)menuState.Pause]);
    }

    // Load sound setting menu when player comes from pause menu
    public void loadSoundMenuInGame()
    {
        ChangeMenu(menus[(int)menuState.SoundIngame]);
    }
    
    // Load main menu restarting the scene, only avaliable from pause menu
    public void loadMainMenuInGame()
    {
        RestartLevel.instance.retry = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Load Game Over Menu
    public void loadGameOverMenu()
    {
        Time.timeScale = 0;
        ChangeMenu(menus[(int)menuState.GameOver]);

        GameOverEnemies.text = "Enemigos: " + GameController.instance.enemiesKilled.ToString();
        GameOverHeight.text = "Altura: " + GameController.instance.alturaActual.ToString("#.#");
        GameOverScore.text = "Puntuacion: " + GameController.instance.score.ToString();


    }
    // Restarts game
    public void retryGame()
    {
        RestartLevel.instance.retry = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quitButton() { }

 
}
