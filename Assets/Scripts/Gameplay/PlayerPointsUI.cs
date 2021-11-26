using UnityEngine;
using TMPro;

public class PlayerPointsUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pointsText;

    [SerializeField]
    private TextMeshProUGUI pointsGameOverText;

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

        pointsGameOverText.text = "C0nGr4t5!\n" + "You've corrupted <b>" + points + "</b> files!";
    }

}
