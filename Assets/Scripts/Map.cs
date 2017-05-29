using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class Map : MonoBehaviour
{
    public GameObject floor, player, enemy, door;
    public static int numEnemy = 0;
    public static GameObject[] enemies;
    
    private int x, y, ancho, largo, direccion;
    private float floorUnitX, floorUnitY;
    private Vector3 pos;
    private float[] coorX = new float[32] { -7.5f, -7.0f, -6.5f, -6.0f, -5.5f, -5.0f, -4.5f, 4.0f, -3.5f, -3.0f, -2.5f, -2.0f, -1.5f, -1.0f, -0.5f, 0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f, 4.0f, 4.5f, 5.0f, 5.5f, 6.0f, 6.5f, 7.0f, 7.5f, 8.0f };
    private float[] coorY = new float[32] { 8.0f, 7.5f, 7.0f, 6.5f, 6.0f, 5.5f, 5.0f, 4.5f, 4.0f, 3.5f, 3.0f, 2.5f, 2.0f, 1.5f, 1.0f, 0.5f, 0.0f, -0.5f, -1.0f, -1.5f, -2.0f, -2.5f, -3.0f, -3.5f, -4.0f, -4.5f, -5.0f, -5.5f, -6.0f, -6.5f, -7.0f, -7.5f };
    private List<string> inCoorXY;
    private bool flagDoorpos;
    
    void Start()
    {
        flagDoorpos = false;
        inCoorXY = new List<string>();
        floorUnitX = floor.transform.localScale.x;
        floorUnitY = floor.transform.localScale.y;
        readJson();
        enemies = GameObject.FindGameObjectsWithTag("enemy");
    }

    void Update()
    {
        this.transform.position = player.transform.position;
    }

    void readJson()
    {        
        String json = File.ReadAllText("map.json");
        int flag = 0, i = 0;

        String[] charsToRemove = new String[] { "{", "\"", ":", "[", "]", "}" };
        foreach (String c in charsToRemove)
        {
            json = json.Replace(c, String.Empty);
        }
        
        String[] lines = json.Split(',');
        String[] posPlayerX, posPlayerY;
        
        posPlayerX = lines[0].Split(' ');
        posPlayerY = lines[1].Split(' ');
        Player.coorPlayer = new float[2];
        Player.coorPlayer[0] = coorX[Int32.Parse(posPlayerX[2])];
        Player.coorPlayer[1] = coorY[Int32.Parse(posPlayerY[1])];
        player.transform.position = new Vector3(coorX[Int32.Parse(posPlayerX[2])], coorY[Int32.Parse(posPlayerY[1])], 0);
        
        while (i < lines.Length)
        {
            String[] substrings = lines[i].Split(' ');
            if (substrings.Length == 3)
            {
                flag++;
            }
            if (flag == 1)
            {
                x = Int32.Parse(substrings[substrings.Length - 1]);
                substrings = lines[i + 1].Split(' ');
                y = Int32.Parse(substrings[1]);
                substrings = lines[i + 2].Split(' ');
                ancho = Int32.Parse(substrings[1]);
                substrings = lines[i + 3].Split(' ');
                largo = Int32.Parse(substrings[1]);
                i += 3;

                pos = new Vector3(coorX[x], coorY[y], 1f);
                GameObject newFloor = Instantiate(floor) as GameObject;
                newFloor.transform.position = pos;
                newFloor.transform.localScale = new Vector3(floorUnitX * ancho, floorUnitY * largo, 1);

                for(float a = coorX[x] - (0.5f*((ancho-1)/2)); a <= coorX[x] + (0.5f * ((ancho - 1) / 2)); a += 0.5f)
                {
                    for (float l = coorY[y] - (0.5f * ((largo - 1) / 2)); l <= coorY[y] + (0.5f * ((largo - 1) / 2)); l += 0.5f)
                    {
                        inCoorXY.Add(a + ", " + l);
                    }
                }
            }
            else if (flag == 2)
            {
                if(flagDoorpos == false)
                {
                    door.transform.position = pos;
                    Enemy.refCoorDoor = new float[] { pos.x, pos.y };
                    flagDoorpos = true;
                }

                x = Int32.Parse(substrings[substrings.Length - 1]);
                substrings = lines[i + 1].Split(' ');
                y = Int32.Parse(substrings[1]);
                substrings = lines[i + 2].Split(' ');
                largo = Int32.Parse(substrings[1]);
                substrings = lines[i + 3].Split(' ');
                direccion = Int32.Parse(substrings[1]);
                i += 3;

                pos = new Vector3(coorX[x], coorY[y], 1f);
                GameObject newFloor = Instantiate(floor) as GameObject;
                newFloor.transform.position = pos;
                newFloor.GetComponent<Renderer>().sortingOrder = 1;
                if (direccion == 0)
                {
                    newFloor.transform.localScale = new Vector3((floorUnitX * largo) + floorUnitX / 1.01f, floorUnitY, 1);
                    for (float l = coorX[x] - (0.5f * (largo / 2)); l <= coorX[x] + (0.5f * (largo / 2)); l += 0.5f)
                    {
                        inCoorXY.Add(l + ", " + coorY[y]);
                    }
                }
                else
                {
                    newFloor.transform.localScale = new Vector3(floorUnitX, (floorUnitY * largo) + floorUnitY / 1.01f, 1);
                    for (float l = coorY[y] - (0.5f * (largo / 2)); l <= coorY[y] + (0.5f * (largo / 2)); l += 0.5f)
                    {
                        inCoorXY.Add(coorX[x] + ", " + l);
                    }
                }              
            }
            else if (flag == 3)
            {
                x = Int32.Parse(substrings[substrings.Length - 1]);
                substrings = lines[i + 1].Split(' ');
                y = Int32.Parse(substrings[1]);
                i++;

                pos = new Vector3(coorX[x], coorY[y], 0f);
                GameObject newEnemy = Instantiate(enemy) as GameObject;
                newEnemy.transform.position = pos;
                newEnemy.name = "enemy " + numEnemy;
                numEnemy++;
            }
            i++;
        }
        Player.inCoorXY = inCoorXY;
        Enemy.inCoorXY = inCoorXY;
    }
}