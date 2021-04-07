using System.Collections;
using UnityEngine;

//manages ragdoll of skeleton
public class RagdollManager : MonoBehaviour
{

    public GameObject show;
    public GameObject hide;

    public void Setup()
    {
        RecursiveBoneSearch(transform, true);
        Destroy(GetComponent<Animator>());
        show.SetActive(true);
        hide.SetActive(false);
        StartCoroutine(DisablePhysics(2));
    }

    //recursivly find bones and changes them
    void RecursiveBoneSearch(Transform t, bool physics)
    {
        foreach (Transform tC in t)
        {
            DoBone(tC, physics);
            RecursiveBoneSearch(tC, physics);
        }
    }

    //enables physics on bones
    void DoBone(Transform t, bool physics)
    {
        if (t.GetComponent<Rigidbody>())
        {
            if (physics)
            {
                t.GetComponent<Rigidbody>().isKinematic = false;
                t.GetComponent<Collider>().isTrigger = false;
            }
            else
            {
                t.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    //recursivly disables physics on bones
    IEnumerator DisablePhysics(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        RecursiveBoneSearch(transform, false);
    }
}
