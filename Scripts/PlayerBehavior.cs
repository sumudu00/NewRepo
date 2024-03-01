using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] CharacterController character;
    [SerializeField] float moveSpeed;
    private Vector3 movement;
    [SerializeField] Camera cam;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform playerModel;

    //shooting
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform gunMuzzle;
    float shootTicks;
    [SerializeField, Min(0)] float maxShootTicks;
    float maxShoottickInv;
    float smoothStep;

    [SerializeField] Light shootingLight;

    //animations
    [SerializeField] Animator playerAnim;

    enum ShootStates {
        Waiting,
        Shoot,
        Disabled
    }

    ShootStates currentShootState = ShootStates.Disabled;

    // Start is called before the first frame update
    void Start()
    {
        maxShoottickInv = 1/maxShootTicks;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        LookAtMouse();

        if(Input.GetMouseButtonDown(0))
        {
            StartShooting();
        }

        else if(Input.GetMouseButtonUp(0))
        {
            StopShooting();
        }


        switch (currentShootState)
        {
            case ShootStates.Disabled:
            //do nothing
            // Debug.Log("Gun Disabled");
            break;

            case ShootStates.Waiting:
            //count a timer
            shootTicks += Time.deltaTime;
            shootTicks = shootTicks > maxShootTicks ? maxShootTicks : shootTicks;

            smoothStep = shootTicks * maxShoottickInv;
            smoothStep = 1 - (3*smoothStep*smoothStep - 2*smoothStep*smoothStep*smoothStep);

            shootingLight.intensity = smoothStep * 1.5f;

            if(shootTicks == maxShootTicks)
            {
                shootingLight.intensity = 0f;
                currentShootState = ShootStates.Shoot;
            }
            // Debug.Log("waiting "+shootTicks);
            break;

            case ShootStates.Shoot:
            //shoot a bullet
            // Debug.Log("Shoot Bullet");
            GameObject bullet = GameObject.Instantiate(bulletPrefab, gunMuzzle.position, Quaternion.identity);
            bullet.transform.forward = gunMuzzle.forward;

            //play audio
            GameManager.Instance.audioManager.PlaySFX("laser_01");

            shootingLight.intensity = 1.5f;

            //go back to waiting
            currentShootState = ShootStates.Waiting;
            shootTicks = 0;
            // Debug.Log("Set to wait");
            break;
        }

        Animate();
    }

    void MovePlayer()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");

        character.Move(movement.normalized * moveSpeed * Time.deltaTime);
    }

    void LookAtMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction*5000, Color.red);

        if(Physics.Raycast(ray, out var hit, Mathf.Infinity, groundLayer))
        {
            Vector3 worldPos = hit.point;
            Vector3 direction = worldPos - transform.position;
            direction.y = 0;
            transform.forward = direction;
        }
    }

    void StartShooting()
    {
        currentShootState = ShootStates.Shoot;
        shootingLight.enabled = true;
        //spawn first bullet
    }

    void StopShooting()
    {
        currentShootState = ShootStates.Disabled;
        shootingLight.enabled = false;
    }

    void Animate()
    {
        float MoveX = Vector3.Dot(movement.normalized, transform.right);
        float MoveZ = Vector3.Dot(movement.normalized, transform.forward);

        playerAnim.SetFloat("MoveX", MoveX, 0.1f, Time.deltaTime);
        playerAnim.SetFloat("MoveZ", MoveZ, 0.1f, Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("enemy"))
        {
            GameManager.Instance.uiManager.ShowGameOverPanel();
            //set game over state
            GameManager.Instance.gameStateManager.SetState(GameStates.GameOver);
            GameManager.Instance.enemySpawner.ResetAllEnemies();
            gameObject.SetActive(false);
        }
    }
}
