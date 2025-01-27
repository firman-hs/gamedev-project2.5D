using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    public static HUDController instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] TMP_Text interactionText;
    [SerializeField] Slider staminaBar;

    private void Start()
    {
        interactionText.gameObject.SetActive(false);
        staminaBar.gameObject.SetActive(false);
    }

    public void EnableInteractionText(string message)
    {
        interactionText.text = message;
        interactionText.gameObject.SetActive(true);
    }

    public void DisableInteractionText()
    {
        interactionText.gameObject.SetActive(false);
    }

    public void InitiateStaminaBar(float maxStamina)
    {
        staminaBar.maxValue = maxStamina;
    }

    public void UpdateStaminaBar(float value)
    {
        if (!staminaBar.gameObject.activeSelf)
        {
            staminaBar.gameObject.SetActive(true);
        }

        staminaBar.value = value;
    }

    public void DisableStaminaBar()
    {
        staminaBar.gameObject.SetActive(false);
    }
}
