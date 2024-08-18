using UnityEngine;
using UnityEngine.UI;

public class O2BarController : MonoBehaviour
{
    public Image O2BarImage; // Assign this in the Inspector to your O2Fill image
    private float currentOxygenLevel = 1f; // Starts full

    private void Update()
    {
        // Example: Decrease O2 level over time (you can change this logic)
        currentOxygenLevel -= Time.deltaTime * 0.05f;
        currentOxygenLevel = Mathf.Clamp01(currentOxygenLevel);

        // Update the fill amount of the O2 bar
        O2BarImage.fillAmount = currentOxygenLevel;
    }

    public void SetOxygenLevel(float level)
    {
        currentOxygenLevel = Mathf.Clamp01(level);
    }
}