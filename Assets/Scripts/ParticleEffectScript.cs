using UnityEngine;

public class ParticleEffectScript : MonoBehaviour {

    public GameObject pickupEffect;
    public GameObject bloodObstacleEffect;
    public GameObject bloodSpikeEffect;

	void Start () {
		
	}

    public void ShowPickupEffect(Transform effectTransform)
    {
        Instantiate(pickupEffect, effectTransform.position, effectTransform.rotation);
    }

    public void ShowBloodObstacleEffect(Transform effectTransform)
    {
        Instantiate(bloodObstacleEffect, effectTransform.position, effectTransform.rotation);
    }

    public void ShowBloodSpikeEffect(Transform effectTransform)
    {
        Instantiate(bloodSpikeEffect, effectTransform.position, effectTransform.rotation);
    }

}
