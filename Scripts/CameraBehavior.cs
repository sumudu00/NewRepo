using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 offset;
    private float smoothTime;
    private Vector3 velocity;
    private Vector3 newPosition;

    bool iscCameraShaking = false;
    float camAnimTicks = 0f, camAnimTickMaxInv = 0f, animSmoothStep = 0f;
    [SerializeField] float camShakeTickMax = 1f;
    [SerializeField] AnimationCurve shakeAnimationCurveX;
    [SerializeField] AnimationCurve shakeAnimationCurveY;
    [SerializeField] AnimationCurve shakeAnimationCurveZ;
    [SerializeField] Transform cam;
    [SerializeField] float camShakeIntensity = 10f;

    Vector3 newCameraPosition;

    public static CameraBehavior Instance;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
        camAnimTickMaxInv = 1/camShakeTickMax;
    }

    void LateUpdate()
    {
        newPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    void Update()
    {
        ShakeCamera();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCameraShake();
        }
    }

    void ShakeCamera()
    {
        if(iscCameraShaking == false)return;

        camAnimTicks += Time.deltaTime;
        camAnimTicks = camAnimTicks > camShakeTickMax ? camShakeTickMax : camAnimTicks;

        animSmoothStep = camAnimTicks * camAnimTickMaxInv;

        newCameraPosition.x = shakeAnimationCurveX.Evaluate(animSmoothStep);
        newCameraPosition.y = shakeAnimationCurveY.Evaluate(animSmoothStep);
        newCameraPosition.z = shakeAnimationCurveZ.Evaluate(animSmoothStep);

        cam.localPosition = newPosition * camShakeIntensity;

        // Debug.Log(cam.position);

        if(camAnimTicks == camShakeTickMax)
        {
            cam.localPosition = Vector3.zero;
            camAnimTicks = 0f;
            iscCameraShaking = false;
        }
    }

    public void StartCameraShake()
    {
        if(iscCameraShaking == true)return;
        camAnimTicks = 0f;
        iscCameraShaking = true;
    }
}
