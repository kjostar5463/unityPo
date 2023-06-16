using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Button RoomCreate;
    public InputField RoomName;
    public InputField RoomPerson;
    public Transform RoomContent;

    Dictionary<string, RoomInfo> RoomCatalog = new Dictionary<string, RoomInfo>();
    // Update is called once per frame
    void Update()
    {
        if(RoomName.text.Length > 0 && RoomPerson.text.Length > 0)
        {
            RoomCreate.interactable = true;
        }
        else
        {
            RoomCreate.interactable= false;
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }
    
    public void CreateRoomObject()
    {
        foreach(RoomInfo info in RoomCatalog.Values)
        {
            GameObject room = Instantiate(Resources.Load<GameObject>("Room"));

            room.transform.SetParent(RoomContent);

            room.GetComponent<Infomation>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }

    public void OnClickCreateRoom()
    {
        RoomOptions Room = new RoomOptions();

        Room.MaxPlayers = byte.Parse(RoomPerson.text);

        Room.IsOpen = true;

        Room.IsVisible = true;

        PhotonNetwork.CreateRoom(RoomName.text, Room);
    }

    public void AllDeleteRoom()
    {
        foreach (Transform trans in RoomContent)
        {
            Destroy(trans.gameObject);
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        AllDeleteRoom();
        UpdateRoom(roomList);
        CreateRoomObject();
    }

    void UpdateRoom(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            if (RoomCatalog.ContainsKey(roomList[i].Name))
            {
                if (roomList[i].RemovedFromList)
                {
                    RoomCatalog.Remove(roomList[i].Name);
                    continue;
                }
            }
            RoomCatalog[roomList[i].Name] = roomList[i];
        }
    }
}
