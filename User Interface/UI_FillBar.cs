using UnityEngine;
using UnityEngine.UI;

public class UI_FillBar : MonoBehaviour
{
    [SerializeField]
    protected Image image;
    [SerializeField]
    protected Gradient gradient;

    private void Awake()
    {
        if(!image) image = GetComponent<Image>();  
    }
    public void SetFillAmount(float percent)
    {
        if (!image) return;
        
        image.fillAmount = percent;
        image.color = gradient.Evaluate(percent);
    }
}
