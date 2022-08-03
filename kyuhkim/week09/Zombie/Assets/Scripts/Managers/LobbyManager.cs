using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private const string GameVersion = "1";
    private const int RoomSpace = 4;
    [SerializeField] private TextMeshProUGUI _connection;
    [SerializeField] private Button _button;

    private void Start()
    {
        PhotonNetwork.GameVersion = GameVersion;
        PhotonNetwork.ConnectUsingSettings();

        _button.interactable = false;
        _connection.text = "Connecting to server...";
    }
    
    public override void OnConnectedToMaster()
    {
        _button.interactable = true;
        _connection.text = "Online : connected to server";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _button.interactable = false;
        _connection.text = "Offline : not connected to server\nretrying";
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect()
    {
        _button.interactable = false;

        if (PhotonNetwork.IsConnected)
        {
            _connection.text = "Connecting to room...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            OnDisconnected(DisconnectCause.None);
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        _connection.text = "Creating new room...";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = RoomSpace });
    }

    public override void OnJoinedRoom()
    {
        _connection.text = "Join the Room";
        PhotonNetwork.LoadLevel("Main");
    }
}
