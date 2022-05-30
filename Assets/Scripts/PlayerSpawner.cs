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
    x.gameObject.transform.position = new Vector3(17f, -7f, 0f);

    Debug.Log("SPAWNED PLAYERS!");
  }

  // Update is called once per frame
  // void z
}
