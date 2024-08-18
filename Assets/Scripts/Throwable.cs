using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    private float mass;
    private Shape shape;
    public Rigidbody2D _rigidbody;
    
    public enum ShapeType
    {
        Rectangle,
        Circle,
        Triangle
    }
     public ShapeType shapeType;
    
    // Start is called before the first frame update
    void Start()
    {
        
        switch (shapeType)
        {
            case ShapeType.Rectangle:
                shape = new Rectangle(transform.localScale.x, transform.localScale.y); 
                break;
            case ShapeType.Circle:
                    shape = new Circle(transform.localScale.x);
                break;
            case ShapeType.Triangle:
                shape = new Triangle(transform.localScale.x, transform.localScale.y);
                break;
        }
        
        CalculateMass(shape);
        _rigidbody.mass = mass;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CalculateMass(Shape shape)
    {
        float area = shape.CalculateArea();
        mass = area;
        
    }
    public float GetMass()
    {
        return mass;
    }
    
    public void ApplyForce(Vector2 baseForce)
    {
        if (_rigidbody != null)
        {
            // Calculate force based on mass
            
            _rigidbody.AddForce(baseForce,ForceMode2D.Impulse);
        }
    }
    
   
    
}
