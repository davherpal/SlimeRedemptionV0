using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MenusScript : MonoBehaviour
{
    // Enum y el array de gameobject tienen el mismo, tener en cuenta los dos.
    public enum menuState { Mainmenu, Play, Sound, SoundIngame, Skins, Stats, Pause, GameOver }
    public RectTransform[] moveMenus;

    public Text statHeightTotal, statScoreTotal, statEnemiesTotal;
    public Text GameOverHeight, GameOverScore, GameOverEnemies;

    public SaveController saveController;
    public GameObject hazard;
    public GameObject player;

    // Para que no se vea el fondo del juego cuando te mueve por los menus
    public GameObject background;

    void Start()
    {

        // If player retry game
        if (RestartLevel.instance.retry)
        {
            // Mostramos se vea juego
            background.SetActive(false);

            ChangeMenu(moveMenus[(int)menuState.Play]);
            hazard.SetActive(true);
        }
        else
        {
            background.SetActive(true);
            hazard.SetActive(false);
            moveMenus[(int)menuState.Mainmenu].DOAnchorPos(Vector2.zero, .5f).From(Vector2.up * -750);

        }

    }

    // Mueve desde abajo al medio el canvas q nos interesa sino lo desplaza los otros dentro del array hacia arriba
    public void ChangeMenu(RectTransform menuToActivate)
    {
        foreach (RectTransform menu in moveMenus)
        {
            if (menu.name == menuToActivate.name)
            {
                menuToActivate.DOAnchorPos(Vector2.zero, .5f).From(Vector2.up * -750);
            }
            else
            {
                menu.DOAnchorPos(Vector2.up * 750, .5f).From(Vector2.zero);
            }
        }

    }

    // Load main menu without restarting scene
    public void loadMainMenu()
    {
        ChangeMenu(moveMenus[(int)menuState.Mainmenu]);
    }

    // Load main menu restarting the scene, only avaliable from pause menu
    public void loadMainMenuInGame()
    {
        RestartLevel.instance.retry = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Load in game menu
    public void loadPlayMenu()
    {
        // Activamos hazard y player ya que no podemos pausar sino animaciones no funcionarian
        hazard.SetActive(true);
        player.SetActive(true);
        // Escondemos se pueda ver juego
        background.SetActive(false);

        ChangeMenu(moveMenus[(int)menuState.Play]);

        // Escondemos los canvas cuando se esta dentro del juego para que no molesten y cuando se presione barra espaciadora el juego corra bien
        StartCoroutine(wait());

        IEnumerator wait()
        {
            yield return new WaitForSeconds(.5f);

            foreach (RectTransform menu in moveMenus)
            {
                if (menu.name != "InGameUI")
                {
                    menu.gameObject.SetActive(false);
                }
            }
        }
    }

    // Loud sound setting menu
    public void loadSoundMenu()
    {
        ChangeMenu(moveMenus[(int)menuState.Sound]);
    }

    // Load sound setting menu when player comes from pause menu
    public void loadSoundMenuInGame()
    {
        ChangeMenu(moveMenus[(int)menuState.SoundIngame]);
    }

    // Load Skins menu
    public void loadSkinsMenu()
    {
        ChangeMenu(moveMenus[(int)menuState.Skins]);

    }

    // Load player stats menu
    public void loadStatsMenu()
    {

        ChangeMenu(moveMenus[(int)menuState.Stats]);

        statEnemiesTotal.text = "Enemigos: " + saveController.LoadDataEnemies();
        statHeightTotal.text = "Altura: " + saveController.LoadDataHeight();
        statScoreTotal.text = "Puntuacion: " + saveController.LoadDataScore();

    }

    // Load Pause Menu
    public void loadPauseMenu()
    {
        // Escondemos hazard y player, no podemos pausar nivel
        hazard.SetActive(false);
        player.SetActive(false);
        // Mostramos no se vea juego de fondo
        background.SetActive(true);

        // Activamos todos los canvas escondidos anteriormente, es ineficaz porque recorre todos los canvs. Se podria crear un array canvas solo ingame y otros fuera.
        foreach (RectTransform menu in moveMenus)
        {
            menu.gameObject.SetActive(true);
        }

        // Movemos pantalla pause y que se vea que la ultima fue la ingame, si se usa la funcion ChangeMenu()  se mostrara la ultima pantalla recorrida en el array y se veria mal.
        moveMenus[(int)menuState.Pause].DOAnchorPos(Vector2.zero, .5f).From(Vector2.up * -750);
        moveMenus[(int)menuState.Play].DOAnchorPos(Vector2.up * 750, .5f).From(Vector2.zero);
    }

    // Load Pause Menu
    public void loadPauseMenuFromSoundMenu()
    {
        // Movemos pantalla pause y que se vea que la ultima fue la SoundInGame, si se usa la funcion ChangeMenu() se mostrara la ultima pantalla recorrida en el array y se veria mal.
        moveMenus[(int)menuState.Pause].DOAnchorPos(Vector2.zero, .5f).From(Vector2.up * -750);
        moveMenus[(int)menuState.SoundIngame].DOAnchorPos(Vector2.up * 750, .5f).From(Vector2.zero);
    }

    // Load Game Over Menu
    public void loadGameOverMenu()
    {
        // Activamos todos los canvas escondidos anteriormente, es ineficaz porque recorre todos los canvs. Se podria crear un array canvas solo ingame y otros fuera.

        foreach (RectTransform menu in moveMenus)
        {
            menu.gameObject.SetActive(true);
        }

        // Movemos game over al centro de la pantalla, si se usa la funcion ChangeMenu()  se mostrara la ultima pantalla recorrida en el array y se veria mal.
        moveMenus[(int)menuState.GameOver].DOAnchorPos(Vector2.zero, 2f).From(Vector2.up * -750);

        GameOverEnemies.text = "Enemigos: " + GameController.instance.enemiesKilled.ToString();
        GameOverHeight.text = "Altura: " + GameController.instance.alturaActual.ToString("#.#");
        GameOverScore.text = "Puntuacion: " + GameController.instance.score.ToString();

        // Escondemos player y hazard una vez la pantalla ya se vea, para evitar errores y que el hazard deje de subir cargando el nivel.
        StartCoroutine(wait());

        IEnumerator wait()
        {
            yield return new WaitForSeconds(2f);
            hazard.SetActive(false);
        }

    }
    // Restarts game
    public void retryGame()
    {
        FindObjectOfType<audioController>().Play("confirmSound");
        RestartLevel.instance.retry = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quitButton() { }


}
