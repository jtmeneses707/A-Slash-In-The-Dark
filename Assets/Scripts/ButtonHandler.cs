using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour
{
  // Start is called before the first frame update
  public void HandleOnSelect()
  {
    Debug.Log(EventSystem.current.currentSelectedGameObject.name);
  }


}
