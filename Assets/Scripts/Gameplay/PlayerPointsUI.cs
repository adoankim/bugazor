using UnityEngine;
using TMPro;

public class PlayerPointsUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pointsText;

    [SerializeField]
    private TextMeshProUGUI pointsGameOverText;

    private void Start()
    {
        GameplayManager.playerPointsRef.AddOnPointsEarnedListener(UpdatePointsText);
        UpdatePointsText(GameplayManager.playerPointsRef.Count);
    }

    public void UpdatePointsText(int points)
    {
        pointsText.text = "Corrupted files: <b>" + points + "</b>";

        pointsGameOverText.text = "C0nGr4t5!\n" + "You've corrupted <b>" + points + "</b> files!";
    }

}
