using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using System.Linq;

public class ThrowDetection : MonoBehaviour
{

  private XRGrabInteractable _grabInteractable;
  private bool _wasSelected = false;
  public Graph graph;

  private List<float> xValues = new List<float>();
  private List<float> yValues = new List<float>();
  private float _timePassed = 0f;
  private bool _first = true;
  void Start()
  {
    _grabInteractable = GetComponent<XRGrabInteractable>();
  }

  void Update()
  {
    ManageThrow();
  }

  void ManageThrow()
  {
    if (_grabInteractable.isSelected)
    {
      _wasSelected = true;
    }
    else if (_wasSelected && !_grabInteractable.isSelected)
    {
      ShowHeight();
    }
  }
  void ShowHeight()
  {
    _timePassed += Time.deltaTime;
    if (_timePassed > 0.25f || _first)
    {
      // Debug.Log(transform.position.y);
      yValues.Add(transform.position.y);
      graph.ShowGraph(yValues, yValues.Max());
      if (!_first)
      {
        _timePassed = 0f;
      }
      _first = false;
    }
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (_wasSelected)
    {
      _wasSelected = false;
      _first = true;
      yValues.Clear();
    }
  }
}
