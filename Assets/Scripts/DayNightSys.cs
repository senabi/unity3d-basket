using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class DayNightSys : MonoBehaviour
{
  public float rotationSpeed;
  public Volume volume;
  PhysicallyBasedSky sky;
  void Start()
  {
    if (volume != null)
    {
      volume.sharedProfile.TryGet(out sky);
    }
  }

  // Update is called once per frame
  void Update()
  {
    transform.Rotate(new Vector3(1, 0, 0) * Time.deltaTime * rotationSpeed);
    Vector3 current = sky.spaceRotation.value;
    current.z += Time.deltaTime * rotationSpeed * 0.2f;
    sky.spaceRotation.value = current;
  }
}