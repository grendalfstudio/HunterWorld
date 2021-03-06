using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Game;
using UnityEngine;

public class Body : MonoBehaviour
{
    private AnimalsController controller;
    private void Awake()
    {
        controller = transform.root.gameObject.GetComponent<AnimalsController>();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Wall"))
        {
            controller.KillTheAnimal(transform.gameObject);
        }

        if (!gameObject.tag.Equals("Wolf") && col.tag.Equals("Wolf"))
        {
            controller.KillTheAnimal(transform.gameObject, true);
        }
    }
        
    private bool IsOutsideTheMap(Transform wall)
    {
        var checkByX = wall.position.y == 0 && Math.Abs(transform.position.x) > Math.Abs(wall.position.x - 1);
        var checkByY = wall.position.x == 0 && Math.Abs(transform.position.y) > Math.Abs(wall.position.y - 1);
            
        return checkByX || checkByY;
    }
}
