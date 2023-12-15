using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{

    [Header("Button Type")]
    [SerializeField] private ButtonsType ButtonType;

    private TextMeshProUGUI buttonText;

    private Image buttonImage;

    private Color imageOpacity;

    private void Awake()
    {
        buttonText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = ButtonType.buttonText;

        buttonImage = gameObject.GetComponent<Image>();
        imageOpacity = buttonImage.color;

        if (ButtonType.buttonIcon != null)
        {
            imageOpacity.a = 1;
            buttonImage.color = imageOpacity;
            buttonImage.sprite = ButtonType.buttonIcon;
        }
    }
}
