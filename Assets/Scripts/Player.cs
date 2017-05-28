using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public Sprite LinkFrontStop, LinkTurnLeftStop, LinkTurnRightStop, LinkBackStop;
    public Sprite Door1, Door2, Door3, Door4;
    public Sprite EnemyWin;
    public GameObject door;
    public GameObject enemy;
    
    private float[] coorX = new float[32] { -7.5f, -7.0f, -6.5f, -6.0f, -5.5f, -5.0f, -4.5f, 4.0f, -3.5f, -3.0f, -2.5f, -2.0f, -1.5f, -1.0f, -0.5f, 0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f, 4.0f, 4.5f, 5.0f, 5.5f, 6.0f, 6.5f, 7.0f, 7.5f, 8.0f };
    private float[] coorY = new float[32] { -7.5f, -7.0f, -6.5f, -6.0f, -5.5f, -5.0f, -4.5f, 4.0f, -3.5f, -3.0f, -2.5f, -2.0f, -1.5f, -1.0f, -0.5f, 0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f, 4.0f, 4.5f, 5.0f, 5.5f, 6.0f, 6.5f, 7.0f, 7.5f, 8.0f };
    public static float[] coorPlayer;
    private float[] coorDoor;
    private System.Random rnd = new System.Random();
    private int randomX, randomY;
    public static List<string> inCoorXY;
    private Vector3 posPlayer, posDoor, posEnemy;
    
    void Start()
    {
        Time.timeScale = 1;
        this.GetComponent<SpriteRenderer>().sprite = LinkFrontStop;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (inCoorXY.Contains((coorPlayer[0] - 0.5f) + ", " + coorPlayer[1]))
            {
                coorPlayer[0] -= 0.5f;
                this.GetComponent<SpriteRenderer>().sprite = LinkTurnLeftStop;
                this.transform.position = new Vector3(coorPlayer[0], coorPlayer[1], 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (inCoorXY.Contains((coorPlayer[0] + 0.5f) + ", " + coorPlayer[1]))
            {
                coorPlayer[0] += 0.5f;
                this.GetComponent<SpriteRenderer>().sprite = LinkTurnRightStop;
                this.transform.position = new Vector3(coorPlayer[0], coorPlayer[1], 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (inCoorXY.Contains(coorPlayer[0] + ", " + (coorPlayer[1] + 0.5f)))
            {
                coorPlayer[1] += 0.5f;
                this.GetComponent<SpriteRenderer>().sprite = LinkBackStop;
                this.transform.position = new Vector3(coorPlayer[0], coorPlayer[1], 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (inCoorXY.Contains(coorPlayer[0] + ", " + (coorPlayer[1] - 0.5f)))
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
            StartCoroutine(AnimationDoor());
        }
        if(obj.tag == "enemy")
        {
            StartCoroutine(AnimationEnemy());
        }
    }

    IEnumerator AnimationDoor()
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
    IEnumerator AnimationEnemy()
    {
        this.enabled = false;
        this.GetComponent<SpriteRenderer>().sprite = LinkFrontStop;
        yield return new WaitForSeconds(0.2f);
        this.transform.rotation = Quaternion.Euler(0, 0, 22.5f);
        yield return new WaitForSeconds(0.2f);
        this.transform.rotation = Quaternion.Euler(0, 0, 45);
        yield return new WaitForSeconds(0.2f);
        this.transform.rotation = Quaternion.Euler(0, 0, 67.5f);
        yield return new WaitForSeconds(0.2f);
        this.transform.rotation = Quaternion.Euler(0, 0, 90);
        Enemy.flagMovePlayer = false;
        yield return new WaitForSeconds(0.1f);
        foreach (GameObject e in Map.enemies)
            e.GetComponent<SpriteRenderer>().sprite = EnemyWin;
        Time.timeScale = 0;
    }
}