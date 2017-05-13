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
    public GameObject door;
    public GameObject enemy;
    
    private float[] coorX = new float[32] { -7.5f, -7.0f, -6.5f, -6.0f, -5.5f, -5.0f, -4.5f, 4.0f, -3.5f, -3.0f, -2.5f, -2.0f, -1.5f, -1.0f, -0.5f, 0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f, 4.0f, 4.5f, 5.0f, 5.5f, 6.0f, 6.5f, 7.0f, 7.5f, 8.0f };
    private float[] coorY = new float[32] { -7.5f, -7.0f, -6.5f, -6.0f, -5.5f, -5.0f, -4.5f, 4.0f, -3.5f, -3.0f, -2.5f, -2.0f, -1.5f, -1.0f, -0.5f, 0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f, 4.0f, 4.5f, 5.0f, 5.5f, 6.0f, 6.5f, 7.0f, 7.5f, 8.0f };
    public static float[] coorPlayer;
    private float[] coorDoor;
    private System.Random rnd = new System.Random();
    private int randomX, randomY;
    private List<string> inCoorXY;
    private Vector3 posPlayer, posDoor, posEnemy;

    void Start()
    {
        Time.timeScale = 1;
        this.GetComponent<SpriteRenderer>().sprite = LinkFrontStop;
        
        inCoorXY = new List<string>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //if (this.transform.position.x - 0.5f >= -3.5f && !inCoorXY.Contains((coorPlayer[0] - 0.5f) + ", " + coorPlayer[1]))
            //{
                coorPlayer[0] -= 0.5f;
                this.GetComponent<SpriteRenderer>().sprite = LinkTurnLeftStop;
                this.transform.position = new Vector3(coorPlayer[0], coorPlayer[1], 0);
            //}
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //if (this.transform.position.x + 0.5f <= 3.5f && !inCoorXY.Contains((coorPlayer[0] + 0.5f) + ", " + coorPlayer[1]))
            //{
                coorPlayer[0] += 0.5f;
                this.GetComponent<SpriteRenderer>().sprite = LinkTurnRightStop;
                this.transform.position = new Vector3(coorPlayer[0], coorPlayer[1], 0);
            //}
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //if (this.transform.position.y + 0.5f <= 2.5f && !inCoorXY.Contains(coorPlayer[0] + ", " + (coorPlayer[1] + 0.5f)))
            //{
                coorPlayer[1] += 0.5f;
                this.GetComponent<SpriteRenderer>().sprite = LinkBackStop;
                this.transform.position = new Vector3(coorPlayer[0], coorPlayer[1], 0);
            //}
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //if (this.transform.position.y - 0.5f >= -2.5f && !inCoorXY.Contains(coorPlayer[0] + ", " + (coorPlayer[1] - 0.5f)))
            //{
                coorPlayer[1] -= 0.5f;
                this.GetComponent<SpriteRenderer>().sprite = LinkFrontStop;
                this.transform.position = new Vector3(coorPlayer[0], coorPlayer[1], 0);
            //}
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