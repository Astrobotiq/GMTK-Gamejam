using UnityEngine;

public class Rectangle : Shape
{
    private float _width;
    private float _height;

    public Rectangle(float width, float height)
    {
        _width = width;
        _height = height;
    }

    public override float CalculateArea()
    {
        return _width * _height;
    }
}

public class Circle : Shape
{
    private float _radius;

    public Circle(float radius)
    {
        _radius = radius;
    }

    public override float CalculateArea()
    {
        return Mathf.PI * _radius * _radius;
    }
}

public class Triangle : Shape
{
    private float _baseLength;
    private float _height;

    public Triangle(float baseLength, float height)
    {
        _baseLength = baseLength;
        _height = height;
    }

    public override float CalculateArea()
    {
        return 0.5f * _baseLength * _height;
    }
}