using UnityEngine;

public class SpellCastingController : MonoBehaviour
{
    public SpellCard currentSpell; // Assign this in the inspector with your desired SpellCard
    public Transform spellCastPoint; // Assign a transform from where the spell should be cast

    private float lastCastTime = 0f;

    void Update()
    {
        // Check for left mouse click and if the cooldown has passed
        if (Input.GetMouseButtonDown(0) && Time.time >= lastCastTime + currentSpell.cooldown)
        {
            CastCurrentSpell();
            lastCastTime = Time.time; // Reset the last cast time
        }
    }

    void CastCurrentSpell()
    {
        if (currentSpell != null)
        {
            // Assuming the spell is cast forward from the spellCastPoint
            Vector3 castPosition = spellCastPoint.position;
            Quaternion castRotation = spellCastPoint.rotation;

            // Call CastSpell on the currentSpell with the position and rotation
            currentSpell.CastSpell(castPosition, castRotation);
        }
        else
        {
            Debug.LogWarning("No current spell selected for casting.");
        }
    }
}
