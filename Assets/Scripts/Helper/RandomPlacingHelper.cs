using UnityEngine;
using UnityEngine.AI;

//helper class to place any object randomly on mesh
public class RandomPlacingHelper : MonoBehaviour
{

    public float radius;
    public float noOfObjects;
    public bool randomYRotation;
    public bool randomScale;
    public float minScale, maxScale;
    public GameObject[] gameObjects;
    Quaternion rotation;

    [ContextMenu("Put object on the mesh")]
    public void PutOnMesh()
    {
        for (int i = 0; i < noOfObjects; i++)
        {
            int random = Random.Range(0, gameObjects.Length);
            RandomNavmeshLocation(gameObjects[random]);
        }
    }

    public void RandomNavmeshLocation(GameObject gameObject)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        rotation = transform.rotation;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
            //navmesh and ground offset
            finalPosition.y = finalPosition.y - 0.1f;
        }
        if (randomYRotation)
        {
            rotation.y = Random.Range(0, 360);
        }
        if (randomScale)
        {
            Vector3 scale = gameObject.transform.localScale;
            scale.x = Random.Range(minScale, maxScale);
            scale.y = Random.Range(minScale, maxScale);
            scale.z = Random.Range(minScale, maxScale);
            gameObject.transform.localScale = scale;
        }
        Instantiate(gameObject, finalPosition, rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
