using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
  [SerializeField] private GameObject _PlayerObject;
  [SerializeField] private PlayerInputManager _InputManager;
  // Start is called before the first frame update
  [SerializeField] private int _PlayerNum = 0;

  [SerializeField] private AudioClip[] _audioClips;

  [SerializeField]
  private Vector3[] _spawnPoints = {
    new Vector3(-17f, -7f, 0f), new Vector3(17f, -7f, 0f)
  };
  void Start()
  {
    // var t = PlayerInput.Instantiate(_PlayerObject, -1, "Keyboard 1");
    // t.gameObject.transform.position = new Vector3(-17f, -7f, 0f);
    // var x = PlayerInput.Instantiate(_PlayerObject, -1, "Controller");
    // x.gameObject.transform.position = new Vector3(17f, -7f, 0f);

    // Debug.Log("SPAWNED PLAYERS!");
  }

  public void OnPlayerJoined(PlayerInput pi)
  {
    // _PlayerNum++;
    Debug.Log("PLAYER JOINED");
    var characterController = pi.gameObject.GetComponent<CharacterController>();
    characterController.enabled = false;
    pi.gameObject.transform.position = _spawnPoints[_PlayerNum];
    characterController.enabled = true;
    _PlayerNum++;
    // pi.gameObject.transform.position = new Vector3(0, 0, 0);
    // pi.gameObject.SetActive(false);

    // pi.transform.position = new Vector3(2, 2, 2);
    // Debug.Log("LOCATION" + pi.gameObject.transform.localScale = 200);
  }



  // Update is called once per frame
  // void z
}
