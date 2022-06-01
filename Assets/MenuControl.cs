using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneCommands))]
public class MenuControl : MonoBehaviour
{
  private SceneCommands sc;
  // Start is called before the first frame update
  void Start()
  {
    sc = GetComponent<SceneCommands>();
  }

  // Update is called once per frame
  void Update()
  {

  }
}
