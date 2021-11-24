using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private PlayerPoints playerPoints;

    void Start()
    {
        playerPoints.ResetPoints();
        
    }
}
