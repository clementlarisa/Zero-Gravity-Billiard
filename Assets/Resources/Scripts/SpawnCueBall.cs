using UnityEngine;

public class SpawnCueBall : MonoBehaviour
{
    public void SpawnWhiteCueBall()
    {
        var randomPointInBox = GetRandomPointInBox();

        var ball0 = Instantiate(Resources.Load("Prefabs/Ball_00"), randomPointInBox, Quaternion.identity) as GameObject;
        ball0.transform.parent = gameObject.transform;
    }

    // Start is called before the first frame update
    private void Start()
    {
        SpawnWhiteCueBall();
    }

    private void Awake()
    {
        // SpawnWhiteCueBall();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private Vector3 GetRandomPointInBox()
    {
        var gamingBox = GameObject.Find("GamingBox");
        var renderers = gamingBox.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return new Vector3(0, 0, 0);

        var b = renderers[0].bounds;
        foreach (var r in renderers) b.Encapsulate(r.bounds);

        // Obtain a random point on a 1 unit radius inside the gaming box object; use it to spawn the white ball
        var randomPointInBox = b.center + Random.insideUnitSphere * 1.1f;
        var bounds = gameObject.GetComponent<MeshRenderer>().bounds;
        // Check if this position will knock over the ball pyramid
        while (bounds.Contains(randomPointInBox) || !b.Contains(randomPointInBox))
            randomPointInBox = b.center + Random.insideUnitSphere * 0.5f;

        return randomPointInBox;
    }
}