using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;

     

    [Header("Movement")]
    //[SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;

    float lookRotationSpeed = 100f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(myRay, out hitInfo, Mathf.Infinity, clickableLayers))
            {
                GameObject clickEffect = ClickParticlesPool.Instance.GetPooledObject();

                if (clickEffect != null)
                {
                    clickEffect.transform.position = hitInfo.point += new Vector3(0, 0.1f, 0);
                    clickEffect.SetActive(true);
                    StartCoroutine(DisableClickEffect(clickEffect));
                }

                agent.SetDestination(hitInfo.point);
                FaceTarget();
            }
        }

    }

    void FaceTarget()
    {
        Vector3 direction = (agent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
    }

    IEnumerator DisableClickEffect(GameObject clickEffect)
    {
        yield return new WaitForSeconds(.5f);
        clickEffect.SetActive(false);
    }
}
