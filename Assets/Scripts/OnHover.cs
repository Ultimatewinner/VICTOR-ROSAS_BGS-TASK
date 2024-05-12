using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator anim;
    public TextMeshProUGUI nameText, descriptionText;
    public InventoryItem item;
    public GameObject sellButton, equipButton;
    public bool inStore = false;
    private static bool isAnimatingGlobal = false; // Static flag to track animation state across all instances
    private static OnHover currentHover; // Reference to the current hovered object
    private static Queue<OnHover> hoverQueue = new Queue<OnHover>(); // Queue to store hovered objects
    private bool isHovering = false; // Flag to track whether the mouse is currently hovering over this object

    private void Start()
    {
        nameText.text = item.itemName;
        descriptionText.text = item.itemDescription;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        // Check if the current hover object is not this one or re-trigger if it's the same
        if (currentHover != this || !anim.GetBool("Hovered"))
        {
            if (currentHover != null && currentHover != this)
                currentHover.anim.SetBool("Hovered", false);

            hoverQueue.Clear(); // Clear existing queue
            hoverQueue.Enqueue(this); // Enqueue the current object
            if (!isAnimatingGlobal)
            {
                StartCoroutine(ProcessHoverQueue());
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        currentHover.anim.SetBool("Hovered", false);
        sellButton.SetActive(false);
        equipButton.SetActive(false);
    }
    
    public void EnableButtons()
    {
        equipButton.SetActive(true);
        if(inStore)
        {
            sellButton.SetActive(true);
        }
    }

    private IEnumerator ProcessHoverQueue()
    {
        isAnimatingGlobal = true;

        while (hoverQueue.Count > 0)
        {
            currentHover = hoverQueue.Dequeue();
            if (currentHover != null)
            {
                currentHover.anim.SetBool("Hovered", true);
                yield return new WaitForSeconds(0.5f); // Time for the animation to play

                // Check if the mouse is still hovering over the current object
                if (!currentHover.isHovering)
                {
                    currentHover.anim.SetBool("Hovered", false);
                }
            }
        }

        isAnimatingGlobal = false;
    }
}
