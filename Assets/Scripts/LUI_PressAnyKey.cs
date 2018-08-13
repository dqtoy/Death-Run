using UnityEngine;
using UnityEngine.SceneManagement;

public class LUI_PressAnyKey : MonoBehaviour {

	[Header("OBJECTS")]
	public GameObject scriptObject;
	public Animator animatorComponent;

    [Header("ANIM NAMES")]
    public string outAnim;


    void Start ()
	{
        animatorComponent.GetComponent<Animator>();
        Invoke("GoToGameScene", 3);
	}

    void GoToGameScene()
    {
        animatorComponent.Play(outAnim);
        Destroy(scriptObject);

        Initiate.Fade("Game", Color.gray, 20f);
    }

	void Update ()
	{
			

	}
}