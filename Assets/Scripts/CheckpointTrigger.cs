using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public GameObject goalTrigger;
    public GameObject checkpointTrigger;
    public string playerTag = "Player";

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(playerTag))
        {
            goalTrigger.SetActive(true);
            checkpointTrigger.SetActive(false);    
        }        
    }
}