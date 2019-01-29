using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public GameObject goalTrigger;
    public GameObject checkpointTrigger;
    public GameObject car;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == car)
        {
            goalTrigger.SetActive(true);
            checkpointTrigger.SetActive(false);    
        }        
    }
}