using UnityEngine;

//manages loot drops
public class LootManager : MonoBehaviour
{
    //chance for loot drop
    public float chance;
    public int numOfItemsToDrop;
    public int dropOffset;
    //what to drop
    public GameObject[] equipments;
    Vector3 dropSpot;

    public void DropLoot()
    {
        if (Random.Range(1, (int)(100 / chance)) == 1)
        {
            for (int i = 0; i < numOfItemsToDrop; i++)
            {
                dropSpot = transform.position;
                dropSpot.x += dropOffset;

                GameObject equipment = equipments[Random.Range(0, equipments.Length)];
                Instantiate(equipment, dropSpot, transform.rotation);
            }
        }
    }
}
