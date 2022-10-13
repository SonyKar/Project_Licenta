using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float lineWidth;
    private LineRenderer _line;
       
    void Start()
    {
        _line = gameObject.GetComponent<LineRenderer>();
        DrawCircle();
    }
    
    private void DrawCircle()
    {
        var segments = 360;
        _line.useWorldSpace = false;
        _line.startWidth = lineWidth;
        _line.endWidth = lineWidth;
        _line.positionCount = segments + 1;

        var pointCount = segments + 1; // add extra point to make start point and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }

        _line.SetPositions(points);
    }
}
