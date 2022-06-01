using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneCommands))]
public class MainMenuController : MonoBehaviour
{

  private SceneCommands sc;
  // Start is called before the first frame update

  // Update is called once per frame

  void Start()
  {
    sc = GetComponent<SceneCommands>();
  }
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      sc.Continue();
    }
    else if (Input.GetKeyDown(KeyCode.Q))
    {
      sc.Quit();
    }
  }
}
