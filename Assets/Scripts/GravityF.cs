using System;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GravityF : MonoBehaviour
{
    private Rigidbody rb;

    const float G = 0.00667f;

    public static List<GravityF> gravityObjectList;
    
    //orbit
    [SerializeField] private bool planets = false;
    [SerializeField] private int orbetSpeed = 1000;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (gravityObjectList == null)
        {
            gravityObjectList = new List<GravityF>();
            
        }
        gravityObjectList.Add(this);
        if (! planets)
        {
            rb.AddForce(Vector3.left * orbetSpeed);
        }
    }

    private void FixedUpdate()
    {
        foreach (var obj in gravityObjectList)
        {
            //call Attract
            if (obj!=this)
            {
                 Attract(obj);     
            }
        }
    }

    void Attract(GravityF other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 direction = rb.position - otherRb.position;
        float distance = direction.magnitude;
        
        float forceMagnitude = G * (rb.mass * otherRb.mass/ Mathf.Pow(distance, 2));
        Vector3 gavityForce = forceMagnitude * direction.normalized;
        
        otherRb.AddForce(gavityForce);
            
        
    }
}
