using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    public static float[] refCoorPlayer, refCoorDoor, coorEnemy;
    public static List<string> inCoorXY;
    public static bool flagMovePlayer;
    public Sprite Enemy1, Enemy2, Enemy3, Enemy4;

    private System.Random rnd = new System.Random();
    private int directionOption;

    void Start () {
        flagMovePlayer = true;

        for(int i = 0; i <= Map.numEnemy; i++)
        {
            coorEnemy = new float[] { this.transform.position.x, this.transform.position.y };
            StartCoroutine(Move(coorEnemy, this.name));
        }
    }

    void Update() {
    }

    IEnumerator Move(float[] coorEnemy, string name)
    {
        yield return new WaitForSeconds(1);
        while (flagMovePlayer)
        {
            directionOption = rnd.Next(1, 5);
            if (directionOption == 1)
            {
                GameObject.Find(name).GetComponent<SpriteRenderer>().sprite = Enemy1;
                for (int i = 0; i < 5; i++)
                {
                    if (inCoorXY.Contains(coorEnemy[0] + ", " + (coorEnemy[1] + 0.5f))) //&& (refCoorDoor[0] + ", " + refCoorDoor[1] != coorEnemy[0] + ", " + (coorEnemy[1] + 0.5f)) && flagMovePlayer)
                    {
                        coorEnemy[1] += 0.5f;
                        GameObject.Find(name).transform.position = new Vector3(coorEnemy[0], coorEnemy[1], 0);
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
                GameObject.Find(name).GetComponent<SpriteRenderer>().sprite = Enemy2;
                for (int i = 0; i < 5; i++)
                {
                    if (inCoorXY.Contains((coorEnemy[0] + 0.5f) + ", " + coorEnemy[1])) //&& (refCoorDoor[0] + ", " + refCoorDoor[1] != (coorEnemy[0] + 0.5f) + ", " + coorEnemy[1]) && flagMovePlayer)
                    {
                        coorEnemy[0] += 0.5f;
                        GameObject.Find(name).transform.position = new Vector3(coorEnemy[0], coorEnemy[1], 0);
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
                GameObject.Find(name).GetComponent<SpriteRenderer>().sprite = Enemy3;
                for (int i = 0; i < 5; i++)
                {
                    if (inCoorXY.Contains(coorEnemy[0] + ", " + (coorEnemy[1] - 0.5f))) //&& (refCoorDoor[0] + ", " + refCoorDoor[1] != coorEnemy[0] + ", " + (coorEnemy[1] - 0.5f)) && flagMovePlayer)
                    {
                        coorEnemy[1] -= 0.5f;
                        GameObject.Find(name).transform.position = new Vector3(coorEnemy[0], coorEnemy[1], 0);
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
                GameObject.Find(name).GetComponent<SpriteRenderer>().sprite = Enemy4;
                for (int i = 0; i < 5; i++)
                {
                    if (inCoorXY.Contains((coorEnemy[0] - 0.5f) + ", " + coorEnemy[1])) //&& (refCoorDoor[0] + ", " + refCoorDoor[1] != (coorEnemy[0] - 0.5f) + ", " + coorEnemy[1]) && flagMovePlayer)
                    {
                        coorEnemy[0] -= 0.5f;
                        GameObject.Find(name).transform.position = new Vector3(coorEnemy[0], coorEnemy[1], 0);
                        yield return new WaitForSeconds(0.5f);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}