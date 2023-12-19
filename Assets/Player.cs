using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    enum E_PlayerState
    {
        NONE = 0,
        ITEM1
    }

    E_PlayerState playerstate;

    public float f_walkspeed;
    public float f_rotspeed;

    public int i_score;
    public int i_life;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void init()
    {
        f_walkspeed = 2f;
        f_rotspeed = 500f;

        i_score = 0;
        i_life = 3;
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(0, 0, f_walkspeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(0, 0, -f_walkspeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.up * Time.deltaTime * -f_rotspeed);
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up * Time.deltaTime * f_rotspeed);
    }
    public void DisLife()
    {
        i_life--;
    }

}
