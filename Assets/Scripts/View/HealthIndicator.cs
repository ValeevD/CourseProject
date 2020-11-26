using UnityEngine;
using TMPro;

public class HealthIndicator : MonoBehaviour
{
    private TextMesh healthText;
    public int CurrentHealth{get;set;}

    private void Awake() {
        healthText = GetComponent<TextMesh>();
    }

    void Start()
    {
        healthText.text = "0";
        CurrentHealth = 0;
    }

    public void ChangeHealthText(int health)
    {
        CurrentHealth = health;
        healthText.text = health.ToString();
    }
}
