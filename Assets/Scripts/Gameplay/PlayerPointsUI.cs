using UnityEngine;
using TMPro;

public class PlayerPointsUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pointsText;

    [SerializeField]
    private PlayerPoints playerPoints;

    private void Awake()
    {
        playerPoints.AddOnPointsEarnedListener(UpdatePointsText);
        UpdatePointsText(playerPoints.Count);
    }

    public void UpdatePointsText(int points)
    {
        pointsText.text = "Corrupted files: <b>" + points + "</b>";
    }

}
