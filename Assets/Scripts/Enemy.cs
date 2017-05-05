using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    public static float[] refCoorPlayer, refCoorDoor, coorEnemy;
    public static List<string> refInCoorXY;
    public static bool flagMovePlayer;
    public Sprite Enemy1;
    public Sprite Enemy2;
    public Sprite Enemy3;
    public Sprite Enemy4;

    private System.Random rnd = new System.Random();
    private int directionOption;

    // Use this for initialization
    void Start () {
        flagMovePlayer = true;
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update() {
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(1);
        while (flagMovePlayer)
        {
            directionOption = rnd.Next(1, 5);
            if (directionOption == 1)
            {
                this.GetComponent<SpriteRenderer>().sprite = Enemy1;
                for (int i = 0; i < 11; i++)
                {
                    if (this.transform.position.y + 0.5f <= 2.5f && !refInCoorXY.Contains(coorEnemy[0] + ", " + (coorEnemy[1] + 0.5f)) && (refCoorDoor[0] + ", " + refCoorDoor[1] != coorEnemy[0] + ", " + (coorEnemy[1] + 0.5f)) && flagMovePlayer)
                    {
                        coorEnemy[1] += 0.5f;
                        this.transform.position = new Vector3(coorEnemy[0], coorEnemy[1], 0);
                        yield return new WaitForSeconds(0.5f);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else if (directionOption == 2)
            {
                this.GetComponent<SpriteRenderer>().sprite = Enemy2;
                for (int i = 0; i < 15; i++)
                {
                    if (this.transform.position.x + 0.5f <= 3.5f && !refInCoorXY.Contains((coorEnemy[0] + 0.5f) + ", " + coorEnemy[1]) && (refCoorDoor[0] + ", " + refCoorDoor[1] != (coorEnemy[0] + 0.5f) + ", " + coorEnemy[1]) && flagMovePlayer)
                    {
                        coorEnemy[0] += 0.5f;
                        this.transform.position = new Vector3(coorEnemy[0], coorEnemy[1], 0);
                        yield return new WaitForSeconds(0.5f);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else if (directionOption == 3)
            {
                this.GetComponent<SpriteRenderer>().sprite = Enemy3;
                for (int i = 0; i < 11; i++)
                {
                    if (this.transform.position.y - 0.5f >= -2.5f && !refInCoorXY.Contains(coorEnemy[0] + ", " + (coorEnemy[1] - 0.5f)) && (refCoorDoor[0] + ", " + refCoorDoor[1] != coorEnemy[0] + ", " + (coorEnemy[1] - 0.5f)) && flagMovePlayer)
                    {
                        coorEnemy[1] -= 0.5f;
                        this.transform.position = new Vector3(coorEnemy[0], coorEnemy[1], 0);
                        yield return new WaitForSeconds(0.5f);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else if (directionOption == 4)
            {
                this.GetComponent<SpriteRenderer>().sprite = Enemy4;
                for (int i = 0; i < 15; i++)
                {
                    if (this.transform.position.x - 0.5f >= -3.5f && !refInCoorXY.Contains((coorEnemy[0] - 0.5f) + ", " + coorEnemy[1]) && (refCoorDoor[0] + ", " + refCoorDoor[1] != (coorEnemy[0] - 0.5f) + ", " + coorEnemy[1]) && flagMovePlayer)
                    {
                        coorEnemy[0] -= 0.5f;
                        this.transform.position = new Vector3(coorEnemy[0], coorEnemy[1], 0);
                        yield return new WaitForSeconds(0.5f);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        this.GetComponent<SpriteRenderer>().sprite = Enemy3;
    }
}
