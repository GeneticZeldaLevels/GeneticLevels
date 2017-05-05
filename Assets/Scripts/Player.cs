using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public Sprite LinkFrontStop;
    public Sprite LinkTurnLeftStop;
    public Sprite LinkTurnRightStop;
    public Sprite LinkBackStop;
    public Sprite Door1;
    public Sprite Door2;
    public Sprite Door3;
    public Sprite Door4;
    public GameObject randomWall;
    public GameObject door;
    public GameObject enemy;

    private float[] coorX = new float[15] {-3.5f, -3.0f, -2.5f, -2.0f, -1.5f, -1.0f, -0.5f, 0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f };
    private float[] coorY = new float[11] {-2.5f, -2.0f, -1.5f, -1.0f, -0.5f, 0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f };
    private float[] coorPlayer, coorDoor;
    private System.Random rnd = new System.Random();
    private int randomX, randomY, numWalls;
    private List<string> inCoorXY;
    private Vector3 posPlayer, posDoor, posEnemy;

    void Start()
    {
        Time.timeScale = 1;
        this.GetComponent<SpriteRenderer>().sprite = LinkFrontStop;

        numWalls = rnd.Next(20, 70);
        inCoorXY = new List<string>();
        
        for (int i = 0; i < numWalls; i++)
        {
            randomX = rnd.Next(1, 15);
            randomY = rnd.Next(1, 11);
            if (inCoorXY.Contains(coorX[randomX] + ", " + coorY[randomY]))
            {
                while (inCoorXY.Contains(coorX[randomX] + ", " + coorY[randomY]))
                {
                    randomX = rnd.Next(1, 15);
                    randomY = rnd.Next(1, 11);
                }
                inCoorXY.Add(coorX[randomX] + ", " + coorY[randomY]);
                Vector3 pos = new Vector3(coorX[randomX], coorY[randomY], 0f);
                GameObject newWall = Instantiate(randomWall) as GameObject;
                newWall.transform.position = pos;
            }
            else
            {
                inCoorXY.Add(coorX[randomX] + ", " + coorY[randomY]);
                Vector3 pos = new Vector3(coorX[randomX], coorY[randomY], 0f);
                GameObject newWall = Instantiate(randomWall) as GameObject;
                newWall.transform.position = pos;
            }
        }

        /* position of Player */
        coorPlayer = new float[2];
        randomX = rnd.Next(1, 15);
        randomY = rnd.Next(1, 11);
        while (inCoorXY.Contains(coorX[randomX] + ", " + coorY[randomY]))
        {
            randomX = rnd.Next(1, 15);
            randomY = rnd.Next(1, 11);
        }
        posPlayer = new Vector3(coorX[randomX], coorY[randomY], 0f);
        this.transform.position = posPlayer;
        
        coorPlayer[0] = coorX[randomX];
        coorPlayer[1] = coorY[randomY];

        /* position of door */
        coorDoor = new float[2];
        randomX = rnd.Next(1, 15);
        randomY = rnd.Next(1, 11);
        coorDoor[0] = coorX[randomX];
        coorDoor[1] = coorY[randomY];
        while (inCoorXY.Contains(coorX[randomX] + ", " + coorY[randomY]) || coorDoor == coorPlayer)
        {
            randomX = rnd.Next(1, 15);
            randomY = rnd.Next(1, 11);
            coorDoor[0] = coorX[randomX];
            coorDoor[1] = coorY[randomY];
        }
        posDoor = new Vector3(coorX[randomX], coorY[randomY], 0f);
        door.transform.position = posDoor;

        /* position of Enemy */
        Enemy.coorEnemy = new float[2];
        randomX = rnd.Next(1, 15);
        randomY = rnd.Next(1, 11);
        Enemy.coorEnemy[0] = coorX[randomX];
        Enemy.coorEnemy[1] = coorY[randomY];
        while (inCoorXY.Contains(coorX[randomX] + ", " + coorY[randomY]) || Enemy.coorEnemy == coorPlayer || Enemy.coorEnemy == coorDoor)
        {
            randomX = rnd.Next(1, 15);
            randomY = rnd.Next(1, 11);
            Enemy.coorEnemy[0] = coorX[randomX];
            Enemy.coorEnemy[1] = coorY[randomY];
        }
        posEnemy = new Vector3(coorX[randomX], coorY[randomY], 0f);
        enemy.transform.position = posEnemy;
        
        /* Pass values to enemy script */
        Enemy.refCoorDoor = coorDoor;
        Enemy.refCoorPlayer = coorPlayer;
        Enemy.refInCoorXY = inCoorXY;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (this.transform.position.x - 0.5f >= -3.5f && !inCoorXY.Contains((coorPlayer[0] - 0.5f) + ", " + coorPlayer[1]))
            {
                coorPlayer[0] -= 0.5f;
                this.GetComponent<SpriteRenderer>().sprite = LinkTurnLeftStop;
                this.transform.position = new Vector3(coorPlayer[0], coorPlayer[1], 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (this.transform.position.x + 0.5f <= 3.5f && !inCoorXY.Contains((coorPlayer[0] + 0.5f) + ", " + coorPlayer[1]))
            {
                coorPlayer[0] += 0.5f;
                this.GetComponent<SpriteRenderer>().sprite = LinkTurnRightStop;
                this.transform.position = new Vector3(coorPlayer[0], coorPlayer[1], 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (this.transform.position.y + 0.5f <= 2.5f && !inCoorXY.Contains(coorPlayer[0] + ", " + (coorPlayer[1] + 0.5f)))
            {
                coorPlayer[1] += 0.5f;
                this.GetComponent<SpriteRenderer>().sprite = LinkBackStop;
                this.transform.position = new Vector3(coorPlayer[0], coorPlayer[1], 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (this.transform.position.y - 0.5f >= -2.5f && !inCoorXY.Contains(coorPlayer[0] + ", " + (coorPlayer[1] - 0.5f)))
            {
                coorPlayer[1] -= 0.5f;
                this.GetComponent<SpriteRenderer>().sprite = LinkFrontStop;
                this.transform.position = new Vector3(coorPlayer[0], coorPlayer[1], 0);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "door")
        {
            StartCoroutine(Animation());
        }
        if(obj.tag == "enemy")
        {
            this.enabled = false;
            Enemy.flagMovePlayer = false;
            this.GetComponent<SpriteRenderer>().sprite = LinkFrontStop;
            this.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        }
    }

    IEnumerator Animation()
    {
        this.enabled = false;
        door.GetComponent<SpriteRenderer>().sprite = Door1;
        yield return new WaitForSeconds(0.5f);
        door.GetComponent<SpriteRenderer>().sprite = Door2;
        this.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        yield return new WaitForSeconds(1);
        door.GetComponent<SpriteRenderer>().sprite = Door3;
        this.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        yield return new WaitForSeconds(1);
        door.GetComponent<SpriteRenderer>().sprite = Door4;
        this.transform.localScale = new Vector3(0, 0, 1);
        yield return new WaitForSeconds(1);
        door.GetComponent<SpriteRenderer>().sprite = Door1;
        Enemy.flagMovePlayer = false;
    }
}
