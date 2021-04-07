using UnityEngine;
using UnityEngine.EventSystems;

//controls player movement
[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{

    public Interactable focus;
    //layer on which player can move
    public LayerMask movementMask;

    Camera cam;
    PlayerMovement playerMovement;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //don't move if something is in front of movement layer
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        //left click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                // Move our player to what we hit
                playerMovement.MoveToPoint(hit.point);

                // Stop focusing any objects
                RemoveFocus();
            }
        }
        //right click
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                // Check if we hit interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                // Set it as focus
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }
    //focus tartget
    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
            playerMovement.FollowTarger(newFocus);
        }
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        playerMovement.StopFollowingTarget();
    }
}
