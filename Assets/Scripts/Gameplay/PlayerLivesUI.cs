using UnityEngine;
using TMPro;

public class PlayerLivesUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI livesText;

    [SerializeField]
    private PlayerDamage playerDamage;

    private void Awake()
    {
        playerDamage.AddOnDamageDealtListener(UpdateLivesText);
        UpdateLivesText(playerDamage.PlayerHealth);
    }

    public void UpdateLivesText(int livesLeft)
    {
        livesText.text = "Lives: " + livesLeft;
    }

}
