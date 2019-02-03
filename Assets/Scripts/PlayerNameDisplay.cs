using UnityEngine;

public class PlayerNameDisplay : MonoBehaviour
{
    public TextMesh playerNameText;

    public GameObject carGameObject;
    private NetworkCar m_Car;

    private static Camera spectator;
    private static bool spectatorIsSetup = false;

    public void SetSpectator(Camera newSpectator)
    {
        spectator = newSpectator;
        spectatorIsSetup = true;
    }

    public Camera GetSpectator()
    {
        return spectator;
    }

    private void Start()
    {
        m_Car = carGameObject.GetComponent<NetworkCar>();
    }

    private void Update()
    {
        if (spectatorIsSetup)
        {
            transform.position = m_Car.transform.position;

            playerNameText.text = m_Car.playerName;
            playerNameText.transform.LookAt(spectator.transform.position);

            // Need to face in the correct direction
            playerNameText.transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}