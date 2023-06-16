using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using PlayFab.ClientModels;
using PlayFab;

public class PhotonSetting : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField email;
    [SerializeField] InputField userID;
    [SerializeField] InputField password;

    public void LoginSuccess(LoginResult result)
    {
        PhotonNetwork.AutomaticallySyncScene = false;

        PhotonNetwork.GameVersion = "1.0f";

        PhotonNetwork.NickName = PlayerPrefs.GetString("Name");

        PhotonNetwork.LoadLevel("Photon Lobby");

    }

    public void LoginFailure(PlayFabError error)
    {
        NotificationManager.NotificationWindow
            (
            "Login failed",
            "There are currently no accounts registered on the server. " + 
            "\n\n Please enter your ID and password correctly"
            );
    }

    public void SignUpSuccess(RegisterPlayFabUserResult result)
    {
        NotificationManager.NotificationWindow
            (
            "Membership successful",
            "Congratulations on becoming a member" + 
            "\n\n Your email account has been registered on the game server."
            );
    }

    public void SignUpFailure(PlayFabError result)
    {
        NotificationManager.NotificationWindow
            (
            "Failed to sign up",
            "Membership registration failed due to a current server error." +
            "\n\n Please try to register as a member again."
            );
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email.text,
            Password = password.text,
        };

        PlayFabClientAPI.LoginWithEmailAddress
            (
            request,
            LoginSuccess,
            LoginFailure
            );
    }

    public void SignUp()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,
            Password = password.text,
            Username = userID.text,
        };

        PlayerPrefs.SetString("Name", userID.text);

        PlayFabClientAPI.RegisterPlayFabUser
            (
            request,
            SignUpSuccess,
            SignUpFailure
            );
    }
}
