using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCommands : MonoBehaviour
{

  private int ind;
  // Start is called before the first frame update
  void Start()
  {
    ind = SceneManager.GetActiveScene().buildIndex;
  }
  public void Quit()
  {
    Application.Quit();
  }

  public void Continue()
  {
    SceneManager.LoadScene(ind + 1);
  }

  public void Reset()
  {
    SceneManager.LoadScene(ind);
  }
}
