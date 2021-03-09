using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DelaunatorSharp;
using DelaunatorSharp.Unity;
using System.Linq;
using DelaunatorSharp.Unity.Extensions;

public class VoronoiGrid : MonoBehaviour
{
    [SerializeField] float generationMinDistance = .2f;
    [SerializeField] int pointsPerIteration = 10;
    [SerializeField] private GameObject sphere;

    private List<IPoint> points = new List<IPoint>();
    private Delaunator delaunator;
    private Camera cam;

    public void Start()
    {
        cam = Camera.main;
        Vector3 p = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));

        var sampler = UniformPoissonDiskSampler.SampleRectangle(p, new Vector2(10, 20), generationMinDistance, pointsPerIteration);
        points = sampler.Select(point => new Vector2(point.x, point.y)).ToPoints().ToList();
        
        delaunator = new Delaunator(points.ToArray());

        foreach (IPoint point in points)
        {
            SpawnChild(sphere, point.ToVector3(), Quaternion.identity);
        }
    }

    public void Update()
    {

    }

    public void SpawnChild(GameObject prefab, Vector3 relativePosition, Quaternion relativeRotation)
    {
        GameObject childObj = Instantiate(prefab);
        childObj.transform.parent = transform;
        childObj.transform.localPosition = relativePosition;
        childObj.transform.localRotation = relativeRotation;
        //childObj.transform.localScale = Vector3.one;
    }
}
