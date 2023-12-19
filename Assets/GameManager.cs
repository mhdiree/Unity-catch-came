using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum E_STATE
    {
        NONE = 0,
        INTRO,
        START,
        PLAY,
        GAMEOVER,
        GAMEWINNING,
        END
    }

    E_STATE gamestate;

    public GameObject G_player;
    public GameObject prf_enemy;
    public Player player;

    public GameObject g_ui_GameLogo;
    public GameObject g_ui_GameStart;
    public GameObject g_ui_GameOver;
    public GameObject g_ui_GameWinning;

    public GameObject g_ui_playerinfo;

    public Text txt_score;
    public Text txt_life;

    int i_enemynum;

    public List<GameObject> lst_enemy;
    public List<Enemy> lst_enemygroup;
    public List<Coin> lst_coingroup;

    // Start is called before the first frame update
    void Start()
    {
        i_enemynum = 5;
        //StartCoroutine(GameStart());
        gamestate = E_STATE.INTRO;
    }

    // Update is called once per frame
    void Update()
    {
        GameState();
    }
    void GameState()
    {
        switch (gamestate)
        {
            case E_STATE.NONE:
                break;

            case E_STATE.INTRO:
                gamestate = E_STATE.NONE;
                GameIntro();
                break;

            case E_STATE.START:
                gamestate = E_STATE.NONE;
                StartCoroutine(GameStart());
                break;

            case E_STATE.PLAY:
                Play();
                break;

            case E_STATE.GAMEOVER:
                gamestate = E_STATE.NONE;
                GameOver();
                break;

            case E_STATE.GAMEWINNING:
                gamestate = E_STATE.NONE;
                GameWinning();
                break;

            case E_STATE.END:
                gamestate = E_STATE.NONE;
                GameEnd();
                break;
        }
    }
    void GameIntro()
    {
        g_ui_GameLogo.SetActive(true);
        g_ui_GameStart.SetActive(false);
        g_ui_GameOver.SetActive(false);
        g_ui_GameWinning.SetActive(false);

    }

    IEnumerator GameStart()
    {
        player.init();

        g_ui_GameLogo.SetActive(false);
        g_ui_GameStart.SetActive(true);
        g_ui_GameOver.SetActive(false);
        g_ui_GameWinning.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        CreateEmemy(); // Àû »ý¼º

        g_ui_GameStart.SetActive(false);
        g_ui_playerinfo.SetActive(true);

        gamestate = E_STATE.PLAY;
    }

    void Play()
    {
        CheckGameOver();
        CheckGameEnd();

        txt_score.text = player.i_score.ToString();
        txt_life.text = player.i_life.ToString();
    }
    void CheckGameEnd()
    {
        if (player.i_score == lst_coingroup.Count)
            gamestate = E_STATE.GAMEWINNING;
    }
    void CheckGameOver()
    {
        if (player.i_life <= 0)
            gamestate = E_STATE.GAMEOVER;
    }

    void GameOver()
    {
        g_ui_GameLogo.SetActive(false);
        g_ui_GameStart.SetActive(false);
        g_ui_GameOver.SetActive(true);
        g_ui_GameWinning.SetActive(false);

        DesytoyAllEnemy();
    }

    void GameWinning()
    {
        g_ui_GameLogo.SetActive(false);
        g_ui_GameStart.SetActive(false);
        g_ui_GameOver.SetActive(false);
        g_ui_GameWinning.SetActive(true);

        DesytoyAllEnemy();
    }
    void GameEnd()
    {
      
    }

    public void OnClickStart()
    {
        gamestate = E_STATE.START;

    }
    public void OnClickGameEnd()
    {
        gamestate = E_STATE.END;
    }
    public void GameRestart()
    {

    }


    void CreateEmemy()
    {
        for (int i = 0; i < i_enemynum; i++)
        {
            int i_rand = Random.Range(0, lst_enemy.Count);

            Vector3 vec_enemy = lst_enemy[i_rand].transform.position;
            vec_enemy = new Vector3(vec_enemy.x, 1, vec_enemy.z);

            GameObject go = Instantiate(prf_enemy, vec_enemy, Quaternion.identity);

            Enemy enm = go.transform.GetComponent<Enemy>();
            enm.SetGameManager(this);
            lst_enemygroup.Add(enm);
        }
    }
    void DesytoyAllEnemy()
    {
        for (int i = 0; i < lst_enemygroup.Count; i++)
        {
            Destroy(lst_enemygroup[i].gameObject);
            Destroy(lst_enemygroup[i]);
        }
        lst_enemygroup.Clear();
    }
}
