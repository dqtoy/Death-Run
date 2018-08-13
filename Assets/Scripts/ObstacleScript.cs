using UnityEngine;

public class ObstacleScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "FlyPowerPickUp")
        {
            Debug.Log("Point ++++++++++++++++ Obstacle");
            Destroy(collision.gameObject);
        }
    }
}
