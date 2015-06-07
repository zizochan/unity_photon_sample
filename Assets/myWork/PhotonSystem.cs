using UnityEngine;
using System.Collections;

public class PhotonSystem : Photon.MonoBehaviour {
	public GameObject GameraPrefab;
	
	void OnJoinedLobby() {
		PhotonNetwork.JoinRandomRoom();
	}
	
	void OnJoinedRoom() {
		Instantiate(GameraPrefab, new Vector3(0f, 0f, -10f), Quaternion.identity);
		PhotonNetwork.Instantiate("DemoUnityChan2D", new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
	}
	
	void OnPhotonRandomJoinFailed() {
		PhotonNetwork.CreateRoom(null);
	}
	
	void Awake() {
		PhotonNetwork.ConnectUsingSettings("v0.1");
	}
}
