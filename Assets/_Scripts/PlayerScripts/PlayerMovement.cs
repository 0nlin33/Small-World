using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;
    private GameManager gameManager;
     

    [Header("Movement")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask resourceLayer;


    float lookRotationSpeed = 70f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        gameManager = agent.GetComponent<GameManager>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(myRay, out hitInfo, Mathf.Infinity, groundLayer))
            {
                GameObject clickEffect = ClickParticlesPool.Instance.GetPooledObject();

                gameManager.SwitchPlayerState(PlayerState.Walking);

                if (clickEffect != null)
                {
                    clickEffect.transform.position = hitInfo.point += new Vector3(0, 0.1f, 0);
                    clickEffect.SetActive(true);
                    StartCoroutine(DisableClickEffect(clickEffect));
                }
                agent.SetDestination(hitInfo.point);

                FaceTarget();


                StartCoroutine(ReachedDestination(hitInfo.point));

            }
            else if (Physics.Raycast(myRay, out hitInfo, Mathf.Infinity, resourceLayer))
            {
                agent.SetDestination(hitInfo.point);
            }
        }

    }

    //If clicked on resource, the have the player stand 1 unit away from the position of the resource to match the trigger area and harvest

    IEnumerator ReachedDestination(Vector3 point)
    {
        bool walking = true;
        float tolerance = 0.1f;

        while (walking)
        {
            if (Mathf.Abs(agent.transform.position.x - point.x) < tolerance &&
                Mathf.Abs(agent.transform.position.z - point.z) < tolerance)
            {
                walking = false;
                gameManager.SwitchPlayerState(PlayerState.Idle);
                yield return null;
            }

            yield return new WaitForSeconds(1f);
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
