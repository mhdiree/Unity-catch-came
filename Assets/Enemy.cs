using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager gamemanager;
    GameObject G_player;

    float f_enemyspeed;
    // Start is called before the first frame update
    void Start()
    {
        f_enemyspeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void SetGameManager(Component go)
    {
        gamemanager = go.GetComponent<GameManager>();
        G_player = gamemanager.G_player;
    }
    void Move()
    {
        transform.LookAt(G_player.transform);
        transform.Translate(0, 0, f_enemyspeed* Time.deltaTime);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Player player = collision.transform.GetComponent<Player>();
            player.DisLife();
        }
    }
    
    public void ObjectDestroy()
    {
        Destroy(this.gameObject);
        Destroy(this);
    }

}
