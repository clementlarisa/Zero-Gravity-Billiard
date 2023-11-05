using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControlCueStick : MonoBehaviour
{
    private GameState _gameStateScript;
    private Rigidbody _rigidBody;
    public XRBaseController LeftController, RighController;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        var interactable = GetComponent<XRBaseInteractable>();

        interactable.selectEntered.AddListener(TriggerGrabReleaseHaptic);
        interactable.selectExited.AddListener(TriggerGrabReleaseHaptic);
    }

    private void Awake()
    {
        _gameStateScript = GameObject.Find("GameState").GetComponent<GameState>();
    }


    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.name.Contains("Ball"))
            return;

        // Send haptic feedback
        LeftController.SendHapticImpulse(0.5f, 0.1f);
        RighController.SendHapticImpulse(0.5f, 0.1f);

        var rb = collision.gameObject.GetComponent<Rigidbody>();
        var force = _rigidBody.velocity * 1f;
        // Debug.Log(force);
        rb.AddForce(force, ForceMode.Impulse);

        _gameStateScript.Hits += 1;
        _gameStateScript.HitsText.text = _gameStateScript.Hits.ToString();

        // Debug.Log(_gameStateScript.Hits);
    }

    private void TriggerGrabReleaseHaptic(BaseInteractionEventArgs eventArgs)
    {
        // Debug.Log("select entered");
        if (eventArgs.interactorObject is XRBaseControllerInteractor controllerInteractor)
            controllerInteractor.xrController.SendHapticImpulse(0.9f, 0.2f);
    }
}