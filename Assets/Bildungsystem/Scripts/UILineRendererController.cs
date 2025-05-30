using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UILineRendererController : MonoBehaviour {

    public bool Active;
    public UnityEngine.UI.Extensions.UILineRenderer LineRenderer; // Assign Line Renderer in editor
    public RectTransform  Point0;
    public RectTransform  Point1;
    public RectTransform  Point2;
    public RectTransform  Point3;

    // Use this for initialization
    public void Update () {
        if(Active)
        {
            var pointlist = new List<Vector2>(LineRenderer.Points);
            pointlist[0] = Point0.position;
            pointlist[1] = Point1.position;
            pointlist[2] = Point2.position;
            pointlist[3] = Point3.position;
            LineRenderer.Points = pointlist.ToArray();
        }        
    }
}