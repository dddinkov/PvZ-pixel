using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccupied;
    [SerializeField]
    private SoundManager plantSoundManager;
    // Start is called before the first frame update
    void Start()
    {
        plantSoundManager = GameObject.Find("PlantSoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 PlacePlant(GameObject plant, float price)
    {
        if(isOccupied)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
            isOccupied = false;
            foreach (RaycastHit2D hit in hits)
            {
                if(hit.collider.gameObject.CompareTag("Plant"))
                {
                    isOccupied = true;
                    break;
                }
            }

            if (isOccupied)
            {
                return Vector3.zero;
            }
        }

        if (!SunManager.CanTakeSun(price))
        {
            return Vector3.zero;
        }
        SunManager.TakeSun(price);

        isOccupied = true;
        plantSoundManager.PlaySound();
        return transform.TransformPoint(transform.position);
    }

}
