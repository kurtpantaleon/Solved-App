using UnityEngine;
using TMPro;

public class InstructionManager : MonoBehaviour
{
    public TextMeshProUGUI instructionText; // Assign in Inspector
    public float blinkSpeed = 2f; // Adjust for faster/slower blinking

    private bool isBlinking = true;

    void Update()
    {
        if (isBlinking)
        {
            // Fade alpha between 0.3 and 1.0
            float alpha = Mathf.Lerp(0.3f, 1f, Mathf.PingPong(Time.time * blinkSpeed, 1));
            Color color = instructionText.color;
            color.a = alpha;
            instructionText.color = color;
        }

        if (instructionText.gameObject.activeSelf && Input.GetMouseButtonDown(0))
        {
            instructionText.gameObject.SetActive(false);
            isBlinking = false;
        }
    }
}