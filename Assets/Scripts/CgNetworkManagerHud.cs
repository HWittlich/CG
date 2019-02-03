using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

#pragma warning disable 618
public class CgNetworkManagerHud : MonoBehaviour
{
    public InputField networkAddressInputField;
    public InputField portInputField;

    private void SetupNetworkManager()
    {
        var networkAddressText = networkAddressInputField.text;
        var networkAddress = networkAddressText != "" ? networkAddressText : "127.0.0.1";

        var portText = portInputField.text;
        int port;
        if (portText == "" || !int.TryParse(portText, out port))
            port = 7777;

        NetworkManager.singleton.networkAddress = networkAddress;
        NetworkManager.singleton.networkPort = port;
    }

    public void OnHostClick()
    {
        SetupNetworkManager();
        NetworkManager.singleton.StartHost();
        gameObject.SetActive(false);
    }

    public void OnClientClick()
    {
        SetupNetworkManager();
        NetworkManager.singleton.StartClient();
        gameObject.SetActive(false);
    }
}