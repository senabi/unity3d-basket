using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
  public TextMeshProUGUI textMesh;

  public void OnTriggerExit(Collider colider)
  {
    var result = int.Parse(textMesh.text) + 1;
    textMesh.text = result.ToString();
  }
}
