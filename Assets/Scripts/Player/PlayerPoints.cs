using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PointsEarned : UnityEvent<int>
{
}

public class PlayerPoints : MonoBehaviour
{
    [SerializeField]
    private int healthRecoveryPointFrequency = 10;

    [SerializeField]
    private int healthRecoveryQuantity= 2;

    private int playerPoints = 0;

    private PointsEarned onPointsEarned = new PointsEarned();

    private PlayerDamage playerDamage;

    public int Count
    {
        get { return playerPoints; }
    }

    private void Awake()
    {
        playerDamage = GetComponent<PlayerDamage>();
    }
    private void OnDestroy()
    {
        onPointsEarned.RemoveAllListeners();
    }

    public void ResetPoints()
    {
        playerPoints = 0;
    }

    public void Add(int points)
    {
        playerPoints += points;
        onPointsEarned.Invoke(playerPoints);

        if(playerPoints % healthRecoveryPointFrequency == 0)
        {

            if (AudioManager._instance != null)
            {
                AudioManager._instance.PlayHealthRecoverSound();
            }

            playerDamage.AddLives(healthRecoveryQuantity);
        }
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
