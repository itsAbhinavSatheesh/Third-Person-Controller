using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletprojectile;
    [SerializeField] private Transform spwanBulletPosition;

    public UnityEngine.Animations.Rigging.Rig aimRigValue;
    public float fireRate = 4f;
    public float damage = 10f;

    private float nextTimeToFire = 0f;

    private PlayerInput playerInput;
    private InputAction shootAction;
    

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Shoot"];
    }

    void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            NotTarget notTarget = raycastHit.transform.GetComponent<NotTarget>();
            if (notTarget == null){
                aimRigValue.weight = 1f;
            }else {
                aimRigValue.weight = 0f;
            }
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }


        if (shootAction.inProgress && aimRigValue.weight != 0 && Time.time >= nextTimeToFire)
        {
            var HitBox = raycastHit.collider.GetComponent<HitBox>();
            if (HitBox) {
                HitBox.onRaycastHit(this, ray.direction);
            }

            nextTimeToFire = Time.time + 1f / fireRate;
            Vector3 aimDir = (mouseWorldPosition - spwanBulletPosition.position).normalized;
            Instantiate(pfBulletprojectile, spwanBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));

        }
    }
}
