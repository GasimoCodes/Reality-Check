using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    private static ScreenFader instance;

    [SerializeField] private Image fader;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static ScreenFader Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("ScreenFader instance is null.");
            }
            return instance;
        }
    }

    // Faders, first we will any ongoing fades, then we set the value (assuming it wa sunset) and then we fade!

    public void FadeIn(float duration = 1f)
    {
        fader.color = Color.black;
        fader.DOKill();
        fader.DOFade(1f, 0).SetUpdate(true);
        fader.DOFade(0f, duration).SetUpdate(true);
    }

    public void FadeOut(float duration = 1f)
    {
        fader.color = Color.black;
        fader.DOKill();
        fader.DOFade(0f, 0).SetUpdate(true);
        fader.DOFade(1f, duration).SetUpdate(true);
    }

    public void DoFlash(Color color, float duration = 1f)
    {
        fader.color = color;
        fader.DOKill();
        fader.DOFade(1f, 0).SetUpdate(true);
        fader.DOFade(0f, duration).SetUpdate(true);
    }



}
