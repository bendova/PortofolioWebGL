using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FlowController : MonoBehaviour
{
    public void GoToLandingPage()
    {
        SceneManager.LoadScene(SceneIds.LANDING_PAGE);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene(SceneIds.GAME);
    }
}
