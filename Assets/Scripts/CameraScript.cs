using UnityEngine;

public class CameraScript : MonoBehaviour {

    Transform player;
    public Vector3 offset;
    public Transform bg;

    public void FollowPlayer(Transform player)
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x + offset.x, offset.y, offset.z);
        }
    }

    public void MakeBGFollow()
    {
        if (player != null)
        {
            bg.transform.position = new Vector3(player.position.x + offset.x, bg.transform.position.y, bg.transform.position.z);
        }
    }

    void Start () {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
	}
	
	void Update () {
        if (GameManager.gameStarted)
        {
            FollowPlayer(player);
            MakeBGFollow();
        }
    }
}
