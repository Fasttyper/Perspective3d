using UnityEngine;

public class GridDrawer : MonoBehaviour
{
    public int gridSize = 10;
    public float cellSize = 1f;
    public Material lineMaterial;
    public Color lineColor = Color.white; // default to white color
    public float lineWidth = 0.02f; // default width

    void Start()
    {
        DrawGrid();
    }

    void DrawGrid()
    {
        float halfTotalSize = gridSize * cellSize * 0.5f; // half of the grid's total size

        // Draw all vertical lines
        for (int i = 0; i <= gridSize; i++)
        {
            float posX = i * cellSize - halfTotalSize;
            CreateLineSegment(new Vector3(posX, 0, -halfTotalSize), new Vector3(posX, 0, halfTotalSize));
        }

        // Draw all horizontal lines
        for (int i = 0; i <= gridSize; i++)
        {
            float posZ = i * cellSize - halfTotalSize;
            CreateLineSegment(new Vector3(-halfTotalSize, 0, posZ), new Vector3(halfTotalSize, 0, posZ));
        }
    }

    void CreateLineSegment(Vector3 start, Vector3 end)
    {
        GameObject lineSegment = new GameObject("LineSegment");
        lineSegment.transform.SetParent(this.transform);

        LineRenderer lr = lineSegment.AddComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.startColor = lineColor;
        lr.endColor = lineColor;
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
