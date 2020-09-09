using UnityEngine;
using UnityEngine.UI;

public class GameOverFade : MonoBehaviour
{
    bool doneFading = true;
    Image image;
    float opacity = 0f;
    [SerializeField, Range(0f, 1f)]
    float fadeSpeed;

    float r;
    float g;
    float b;

    private void Awake()
    {
        image = GetComponent<Image>();
        Color color = image.color;
        r = color.r;
        g = color.g;
        b = color.b;
    }

    void Update()
    {
        if (!doneFading)
        {
            opacity += fadeSpeed * Time.deltaTime;
            if (opacity > 1)
            {
                opacity = 1;
                GameHandler.instance.gameOverDone = true;
                doneFading = true;
            }
            image.color = new Color(r, g, b, opacity);
        }
    }

    public void FadeScreen()
    {
        doneFading = false;
        opacity = 0f;
    }
}
