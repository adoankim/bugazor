using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PointsEarned : UnityEvent<int>
{
}

public class PlayerPoints : MonoBehaviour
{
    [SerializeField]
    private int playerPoints = 0;

    private PointsEarned onPointsEarned = new PointsEarned();

    public int Count
    {
        get { return playerPoints; }
    }

    private void OnDestroy()
    {
        onPointsEarned.RemoveAllListeners();
    }

    public void Add(int points)
    {
        playerPoints += points;
        onPointsEarned.Invoke(playerPoints);
    }

    public void AddOnPointsEarnedListener(UnityEngine.Events.UnityAction<int> listener)
    {
        onPointsEarned.AddListener(listener);
    }

    public void RemoveOnPointsEarnedListener(UnityEngine.Events.UnityAction<int> listener)
    {
        onPointsEarned.RemoveListener(listener);
    }
}
