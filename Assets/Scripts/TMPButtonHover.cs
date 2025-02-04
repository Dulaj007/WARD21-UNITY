using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class TMPButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color normalTextColor = Color.white;   // Default text color
    public Color hoverTextColor = Color.red;     // Text color on hover

    private TextMeshProUGUI buttonText;          // Reference to TMP text
    private Button button;                       // Reference to the Button component

    private void Start()
    {
        // Get the TMP text and button components
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();

        // Set the initial text color
        if (buttonText != null)
            buttonText.color = normalTextColor;
    }

    // Triggered when the mouse enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null && button.interactable)
            buttonText.color = hoverTextColor;
    }

    // Triggered when the mouse exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null && button.interactable)
            buttonText.color = normalTextColor;
    }
}
