using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FlowController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GoToLandingPage()
    {
        SceneManager.LoadScene(SceneIds.LANDING_PAGE);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene(SceneIds.GAME);
    }
}
