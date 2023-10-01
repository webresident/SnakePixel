using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySegmentGenerator : MonoBehaviour
{
    [SerializeField] private GameObject bodySegmentPrefab;
    private List<Transform> bodySegments = new List<Transform>();
    private Transform currentTransform;

    public List<Transform> BodySegments { get { return bodySegments; } }
    private void Awake()
    {
        currentTransform = transform;
    }
    public void GenerateBodySegments(int count)
    {
        for(int i = 1; i <= count; i++)
        {
            var temp = Instantiate(bodySegmentPrefab, currentTransform.position + new Vector3(-i, 0, 0), currentTransform.rotation);
            bodySegments.Add(temp.transform);
        }
    }
}
