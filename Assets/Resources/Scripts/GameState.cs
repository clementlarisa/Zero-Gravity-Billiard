using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    private GameState _gameStateScript;
    public int Hits;
    public Text HitsText;

    public float Scale = 1f;
    public Text ScaleText;

    public int Score;
    public Text ScoreText;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void ScaleGame()
    {
    }

    public void ResetArena()
    {
        // delete billiard prefab
        var balls = GameObject.Find("BilliardBalls");
        if (balls == null) balls = GameObject.Find("BilliardBalls(Clone)");

        Destroy(balls);

        // we are trying to destroy a clone

        // reinstantiate it
        var center = GetCenterInBox();

        var arena = GameObject.Find("Arena");

        var billiardBalls =
            Instantiate(Resources.Load("Prefabs/BilliardBalls"), center, Quaternion.identity) as GameObject;
        billiardBalls.transform.parent = arena.transform;

        // white ball is spawned by the script on the prefab

        Score = 0;
        ScoreText.text = "0";
        Hits = 0;
        HitsText.text = "0";
        Scale = 1f;

        ChangeScale(1f);
    }

    public void ChangeScale(float scale)
    {
        ScaleText.text = $"Scale factor: {scale:0.00##}";
        Scale = scale;
        var arena = GameObject.Find("Arena");
        var cueStick = GameObject.Find("CueStick");

        arena.transform.localScale = new Vector3(scale, scale, scale);
        cueStick.transform.localScale = new Vector3(scale * 0.2f, scale * 0.5f, scale * 0.2f);
        // Debug.Log(scale);
    }

    private Vector3 GetCenterInBox()
    {
        var gamingBox = GameObject.Find("GamingBox");
        var renderers = gamingBox.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return new Vector3(0, 0, 0);

        var b = renderers[0].bounds;
        foreach (var r in renderers) b.Encapsulate(r.bounds);

        return b.center;
    }
}