using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

    public GameObject player;

    void Start () {

        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
	void Update () {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}
