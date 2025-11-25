using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_ImageSetter : MonoBehaviour
{
    [SerializeField]
    protected Image image;

    private void Awake()
    {
        if(!image) image = GetComponent<Image>();
    }

    public void SetImage(Sprite set)
    {
        image.sprite = set;
    }
    public void SetColor(Color color)
    {
        image.color = color;
    }
    public void AnimateColor(Color startColor, Color endColor, float duration = 1f)
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(TweenColor(startColor, endColor, duration));
    }
    Coroutine routine;
    IEnumerator TweenColor(Color startColor, Color endColor, float duration)
    {
        float lerp = 0;
        while(lerp < duration)
        {
            SetColor(Color.Lerp(startColor, endColor, lerp / duration));

            lerp += Time.deltaTime;
            yield return null;
        }

        routine = null;
    }
}
