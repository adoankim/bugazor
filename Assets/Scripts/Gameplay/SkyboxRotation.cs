using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    [SerializeField]
    private Material skyboxMaterial;

    [SerializeField]
    private float rotationSpeed = 1f;

    [SerializeField]
    private float exposureSpeed = .5f;

    [SerializeField]
    private float exposureIntensity = .15f;

    [SerializeField]
    private float exposureCenter = .6f;

    private float maxRotation = 360;
    private float currentRotation = 0;
    void Start()
    {
        
    }
    void Update()
    {
        if(currentRotation > maxRotation)
        {
            currentRotation = 0;
        }

        currentRotation += Time.deltaTime * rotationSpeed;
        skyboxMaterial.SetFloat("_Rotation", currentRotation);
     
        skyboxMaterial.SetFloat("_Exposure", Mathf.PingPong(Time.time * exposureSpeed, exposureIntensity) + exposureCenter);
    }
}
