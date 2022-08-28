using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Graph : MonoBehaviour
{
  private RectTransform _graphContainer;
  public Sprite circleSprite;
  private RectTransform _labelX;
  private RectTransform _labelY;
  private List<RectTransform> labels = new List<RectTransform>();
  private void Awake()
  {
    _graphContainer = transform.Find("Graph Container").GetComponent<RectTransform>();
    _labelX = _graphContainer.Find("LabelX").GetComponent<RectTransform>();
    _labelY = _graphContainer.Find("LabelY").GetComponent<RectTransform>();
    // max 146, 96
  }
  private void CreatePointGameObject(UnityEngine.Vector2 position)
  {
    GameObject point = new GameObject("node", typeof(Image));
    point.transform.SetParent(_graphContainer, false);
    point.GetComponent<Image>().sprite = circleSprite;
    RectTransform rect = point.GetComponent<RectTransform>();
    rect.anchoredPosition = position;
    rect.sizeDelta = new UnityEngine.Vector2(2, 2);
    rect.anchorMin = new UnityEngine.Vector2(0, 0);
    rect.anchorMax = new UnityEngine.Vector2(0, 0);
  }
  public void ShowGraph(List<float> values, float yMax)
  {
    ClearDataPoints();
    float graphHeight = _graphContainer.sizeDelta.y;
    float graphWidth = _graphContainer.sizeDelta.x;
    float xSize = graphWidth / values.Count;
    List<Vector2> points = new List<Vector2>();
    Vector2 prevPoint = new Vector2();
    float baseNum = 0f;
    for (int i = 0; i < values.Count; i++)
    {
      float xPosition = i * xSize;
      float yPosition = values[i] / yMax * graphHeight;
      Vector2 point = new Vector2(xPosition, yPosition);
      points.Add(point);
      if (i >= 1)
      {
        CreateEdge(prevPoint, point);
        CreatePointGameObject(prevPoint);
      }
      prevPoint = point;
      RectTransform labelX = Instantiate(_labelX);
      labelX.SetParent(_graphContainer);
      labelX.gameObject.SetActive(true);
      labelX.anchoredPosition3D = new Vector3(xPosition, -1.4f, 0f);
      labelX.localScale = new Vector3(1, 1, 1);
      labelX.localRotation = Quaternion.Euler(0, 0, 0);
      labelX.GetComponent<TextMeshProUGUI>().text = (baseNum).ToString("0.##");
      baseNum += 0.25f;
      labels.Add(labelX);
    }
    CreatePointGameObject(prevPoint);
    int separatorCount = 11;
    for (int i = 0; i < separatorCount; i++)
    {
      RectTransform labelY = Instantiate(_labelY);
      labelY.SetParent(_graphContainer);
      labelY.gameObject.SetActive(true);
      float normalizedVal = i * 1f / separatorCount;
      labelY.anchoredPosition3D = new Vector3(-3f, normalizedVal * graphHeight + 1f, 0f);
      labelY.localScale = new Vector3(1, 1, 1);
      labelY.localRotation = Quaternion.Euler(0, 0, 0);
      labelY.GetComponent<TextMeshProUGUI>().text = (normalizedVal * yMax).ToString("0.##");
      labels.Add(labelY);
    }
    // Max value
    {
      RectTransform labelY = Instantiate(_labelY);
      labelY.SetParent(_graphContainer);
      labelY.gameObject.SetActive(true);
      labelY.anchoredPosition3D = new Vector3(-3f, graphHeight + 1f, 0f);
      labelY.localScale = new Vector3(1, 1, 1);
      labelY.localRotation = Quaternion.Euler(0, 0, 0);
      labelY.GetComponent<TextMeshProUGUI>().text = (yMax).ToString("0.##");
      labels.Add(labelY);
    }
  }
  private void CreateEdge(UnityEngine.Vector2 a, UnityEngine.Vector2 b)
  {
    GameObject go = new GameObject("edge", typeof(Image));
    go.transform.SetParent(_graphContainer, false);
    go.GetComponent<Image>().color = Color.grey;
    RectTransform rect = go.GetComponent<RectTransform>();
    UnityEngine.Vector2 direction = (b - a).normalized;
    float distance = UnityEngine.Vector2.Distance(a, b);
    rect.anchorMax = new UnityEngine.Vector2(0, 0);
    rect.anchorMin = new UnityEngine.Vector2(0, 0);
    rect.sizeDelta = new UnityEngine.Vector2(distance, 0.5f);
    rect.anchoredPosition = a + direction * distance * 0.5f;
    rect.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
  }

  private void ClearDataPoints()
  {
    var circles = transform.GetComponentsInChildren<Transform>().Where(t => t.name == "node").ToArray();
    var connections = transform.GetComponentsInChildren<Transform>().Where(t => t.name == "edge").ToArray();
    foreach (var c in circles)
    {
      Destroy(c.gameObject);
    }
    foreach (var c in connections)
    {
      Destroy(c.gameObject);
    }
    foreach (var c in labels)
    {
      // Destroy(c.gameObject);
      c.gameObject.SetActive(false);
    }
  }
}
