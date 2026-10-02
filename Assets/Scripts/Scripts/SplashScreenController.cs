using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    [Header("Splash")]
    public CanvasGroup splashGroup;
    public CanvasGroup logoGroup;

    [Header("Timing")]
    public float fadeInTime = 1f;
    public float stayTime = 1.5f;
    public float fadeOutTime = 1f;
    public float sceneFadeTime = 1f;

    [Header("Next Scene")]
    public string nextSceneName;

    private void Start()
    {
        logoGroup.alpha = 0f;
        splashGroup.alpha = 1f;

        StartCoroutine(PlaySplashScreen());
    }

    private IEnumerator PlaySplashScreen()
    {
        // Fade logo in
        yield return FadeCanvasGroup(
            logoGroup,
            0f,
            1f,
            fadeInTime
        );

        // Keep logo visible
        yield return new WaitForSecondsRealtime(stayTime);

        // Fade logo out
        yield return FadeCanvasGroup(
            logoGroup,
            1f,
            0f,
            fadeOutTime
        );

        // Load next scene behind the splash screen
        if (!string.IsNullOrWhiteSpace(nextSceneName))
        {
            Scene splashScene = SceneManager.GetActiveScene();

            AsyncOperation loadOperation =
                SceneManager.LoadSceneAsync(
                    nextSceneName,
                    LoadSceneMode.Additive
                );

            while (!loadOperation.isDone)
            {
                yield return null;
            }

            Scene nextScene =
                SceneManager.GetSceneByName(nextSceneName);

            SceneManager.SetActiveScene(nextScene);

            // Fade the black splash screen away,
            // revealing the new scene underneath.
            yield return FadeCanvasGroup(
                splashGroup,
                1f,
                0f,
                sceneFadeTime
            );

            SceneManager.UnloadSceneAsync(splashScene);
        }
    }

    private IEnumerator FadeCanvasGroup(
        CanvasGroup group,
        float startAlpha,
        float endAlpha,
        float duration
    )
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;

            group.alpha = Mathf.Lerp(
                startAlpha,
                endAlpha,
                time / duration
            );

            yield return null;
        }

        group.alpha = endAlpha;
    }
}