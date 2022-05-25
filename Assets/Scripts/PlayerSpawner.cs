using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
  [SerializeField] private GameObject _PlayerObject;
  [SerializeField] private PlayerInputManager _InputManager;
  // Start is called before the first frame update
  void Start()
  {
    var t = PlayerInput.Instantiate(_PlayerObject, -1, "Keyboard 1");
    t.gameObject.transform.position = new Vector3(-17f, -7f, 0f);
    var x = PlayerInput.Instantiate(_PlayerObject, -1, "Controller");
    x.gameObject.transform.position = new Vector3(-2f, -7f, 0f);
    // PlayerInput.Instantiate(_PlayerObject);

    // _InputManager.playerPrefab = _PlayerObject;
    // Instantiate(_PlayerObject, new Vector3(-17.3700008f, -7f, 0f), Quaternion.identity);
    // _InputManager.JoinPlayer();
    // Instantiate(_PlayerObject, new Vector3(15.5100002f, -7f, 0f), Quaternion.identity);
    // _InputManager.JoinPlayer();
    Debug.Log("SPAWNED PLAYERS!");
  }

  // Update is called once per frame
  // void z
}
