using UnityEngine;

public class ControlBall : MonoBehaviour
{
    private GameState _gameStateScript;
    private bool _isOut;
    private Rigidbody _rigidBody;
    private SphereCollider _sphereCollider;

    private void Awake()
    {
        _gameStateScript = GameObject.Find("GameState").GetComponent<GameState>();
    }

    private void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody>();
        _rigidBody.useGravity = false;

        _sphereCollider = gameObject.GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.name.Contains("Wall")) return;

        // let ball fall out of gaming box (unless it's the white ball without which we can't play anymore)
        if (_isOut && collision.gameObject.name.Contains("Wall") && !gameObject.name.Contains("Ball_00"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            _rigidBody.useGravity = true;

            return;
        }


        var normal = collision.contacts[0].normal;
        _rigidBody.velocity = Vector3.Reflect(GetComponent<Rigidbody>().velocity, normal);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.name.Contains("Pocket")) return;

        _isOut = true;

        _gameStateScript.Score += 1;
        _gameStateScript.ScoreText.text = _gameStateScript.Score.ToString();
    }
}