using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public InputField input;
    public Transform ChatContent;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (input.text.Length == 0) return;

            string chat = PhotonNetwork.NickName + " : " + input.text;

            photonView.RPC("Chatting", RpcTarget.All, chat);
        }
    }

    [PunRPC]

    void Chatting(string msg)
    {
        GameObject chat = Instantiate(Resources.Load<GameObject>("String"));
        chat.GetComponent<Text>().text = msg;

        chat.transform.SetParent(ChatContent);

        input.ActivateInputField();

        input.text = "";
    }
}
