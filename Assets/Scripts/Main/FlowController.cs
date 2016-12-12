using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FlowController : MonoBehaviour
{
    public const int V_SYNC_DISABLE = 0;
    public const int V_SYNC_60_FPS = 1;
    public const int V_SYNC_30_FPS = 2;

    void Awake()
    {
        SetQualitySettings();
    }

    private void SetQualitySettings()
    {

#if UNITY_EDITOR
        QualitySettings.vSyncCount = V_SYNC_DISABLE;
#else
        QualitySettings.vSyncCount = V_SYNC_DISABLE;
#endif
        Application.targetFrameRate = -1;
        Application.runInBackground = true;
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
