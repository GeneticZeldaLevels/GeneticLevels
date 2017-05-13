using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

public class Map : MonoBehaviour
{
    public GameObject floor, player, enemy;
    
    private int x, y, ancho, largo, direccion;
    private float floorUnitX = 0.195f, floorUnitY = 0.2f;
    private Vector3 pos;
    private float[] coorX = new float[32] { -7.5f, -7.0f, -6.5f, -6.0f, -5.5f, -5.0f, -4.5f, 4.0f, -3.5f, -3.0f, -2.5f, -2.0f, -1.5f, -1.0f, -0.5f, 0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f, 4.0f, 4.5f, 5.0f, 5.5f, 6.0f, 6.5f, 7.0f, 7.5f, 8.0f };
    private float[] coorY = new float[32] { 8.0f, 7.5f, 7.0f, 6.5f, 6.0f, 5.5f, 5.0f, 4.5f, 4.0f, 3.5f, 3.0f, 2.5f, 2.0f, 1.5f, 1.0f, 0.5f, 0.0f, -0.5f, -1.0f, -1.5f, -2.0f, -2.5f, -3.0f, -3.5f, -4.0f, -4.5f, -5.0f, -5.5f, -6.0f, -6.5f, -7.0f, -7.5f };
    
    // Use this for initialization
    void Start()
    {
        readJson();
    }

    // Update is called once per frame
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

                pos = new Vector3(coorX[x], coorY[y], 0f);
                GameObject newFloor = Instantiate(floor) as GameObject;
                newFloor.transform.position = pos;
                newFloor.transform.localScale = new Vector3(floorUnitX * ancho, floorUnitY * largo, 1);
            }
            else if (flag == 2)
            {
                x = Int32.Parse(substrings[substrings.Length - 1]);
                substrings = lines[i + 1].Split(' ');
                y = Int32.Parse(substrings[1]);
                substrings = lines[i + 2].Split(' ');
                largo = Int32.Parse(substrings[1]);
                substrings = lines[i + 3].Split(' ');
                direccion = Int32.Parse(substrings[1]);
                i += 3;

                Vector3 pos = new Vector3(coorX[x], coorY[y], 0f);
                GameObject newFloor = Instantiate(floor) as GameObject;
                newFloor.transform.position = pos;
                newFloor.GetComponent<Renderer>().sortingOrder = 1;
                if (direccion == 0)
                    newFloor.transform.localScale = new Vector3(floorUnitX * largo, floorUnitY, 1);
                else
                    newFloor.transform.localScale = new Vector3(floorUnitX, floorUnitY * largo, 1);
            }
            else if (flag == 3)
            {
                x = Int32.Parse(substrings[substrings.Length - 1]);
                substrings = lines[i + 1].Split(' ');
                y = Int32.Parse(substrings[1]);
                i++;

                Vector3 pos = new Vector3(coorX[x], coorY[y], 0f);
                GameObject newEnemy = Instantiate(enemy) as GameObject;
                newEnemy.transform.position = pos;
            }
            i++;
        }
        
        /* 
        string[] lines = File.ReadAllLines("data.txt");
        Char delimiter = ' ';
        int flag = 0, i = 0;
        String[] posPlayerX, posPlayerY;

        posPlayerX = lines[1].Split(delimiter);
        posPlayerY = lines[2].Split(delimiter);
        Player.coorPlayer = new float[2];
        Player.coorPlayer[0] = coorX[Int32.Parse(posPlayerX[1])];
        Player.coorPlayer[1] = coorY[Int32.Parse(posPlayerY[1])];
        player.transform.position = new Vector3(coorX[Int32.Parse(posPlayerX[1])], coorY[Int32.Parse(posPlayerY[1])], 0);
        

        // Display the file contents by using a foreach loop.
        while (i < lines.Length)
        {
            String[] substrings = lines[i].Split(delimiter);
            if (substrings.Length == 1)
            {
                flag ++;
            }
            else
            {
                if(flag == 1)
                {
                    x = Int32.Parse(substrings[1]);
                    substrings = lines[i+1].Split(delimiter);
                    y = Int32.Parse(substrings[1]);
                    substrings = lines[i + 2].Split(delimiter);
                    ancho = Int32.Parse(substrings[1]);
                    substrings = lines[i + 3].Split(delimiter);
                    largo = Int32.Parse(substrings[1]);
                    i += 3;

                    pos = new Vector3(coorX[x], coorY[y], 0f);
                    GameObject newFloor = Instantiate(floor) as GameObject;
                    newFloor.transform.position = pos;
                    newFloor.transform.localScale = new Vector3(floorUnitX * ancho, floorUnitY * largo, 1);
                }else if(flag == 2)
                {
                    x = Int32.Parse(substrings[1]);
                    substrings = lines[i + 1].Split(delimiter);
                    y = Int32.Parse(substrings[1]);
                    substrings = lines[i + 2].Split(delimiter);
                    largo = Int32.Parse(substrings[1]);
                    substrings = lines[i + 3].Split(delimiter);
                    direccion = Int32.Parse(substrings[1]);
                    i += 3;

                    Vector3 pos = new Vector3(coorX[x], coorY[y], 0f);
                    GameObject newFloor = Instantiate(floor) as GameObject;
                    newFloor.transform.position = pos;
                    newFloor.GetComponent<Renderer>().sortingOrder = 1;
                    if (direccion == 0)
                        newFloor.transform.localScale = new Vector3(floorUnitX * largo, floorUnitY, 1);
                    else
                        newFloor.transform.localScale = new Vector3(floorUnitX, floorUnitY * largo, 1);
                }
                else if(flag == 3)
                {
                    x = Int32.Parse(substrings[1]);
                    substrings = lines[i + 1].Split(delimiter);
                    y = Int32.Parse(substrings[1]);
                    i++;

                    Vector3 pos = new Vector3(coorX[x], coorY[y], 0f);
                    GameObject newEnemy = Instantiate(enemy) as GameObject;
                    newEnemy.transform.position = pos;
                }
            }
            i++;
        }*/
    }

}