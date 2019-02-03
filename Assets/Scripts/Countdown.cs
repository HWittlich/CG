using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class Countdown : MonoBehaviour
{
    public GameObject CountDown;
    public AudioSource shortBeep;
    public AudioSource longBeep;
    public GameObject LapTimer;


    void Start()
    {
        StartCoroutine(CountDownStart());
    }


    IEnumerator CountDownStart()
    {
        yield return new WaitForSeconds(0.5f);
        Behaviour CarController = (Behaviour) GameObject.Find("Car").GetComponent<CarUserControl>();
        Behaviour AIController = (Behaviour) GameObject.Find("RivalCar").GetComponent<CarAIControl>();
        CarController.enabled = false;
        AIController.enabled = false;
        CountDown.GetComponent<Text>().text = "5";
        shortBeep.Play();
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);
        CountDown.GetComponent<Text>().text = "4";
        shortBeep.Play();
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);
        CountDown.GetComponent<Text>().text = "3";
        shortBeep.Play();
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);
        CountDown.GetComponent<Text>().text = "2";
        shortBeep.Play();
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);
        CountDown.GetComponent<Text>().text = "1";
        shortBeep.Play();
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);

        CountDown.SetActive(false);
        longBeep.Play();
        CarController.enabled = true;
        AIController.enabled = true;
        LapTimer.SetActive(true);
        //CarControls.SetActive(true);
        CountDown.GetComponent<Text>().text = "GO!";
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);
    }
}