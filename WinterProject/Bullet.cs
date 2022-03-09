using System;
using Raylib_cs;
using System.Numerics;

public class Bullet
{
    public Vector2 position;
    public float bulletRadius = 10;

    public int bulletSpeed = 25;

    public void Update()
    {
        position.X += bulletSpeed;
    }

    public void Draw()
    {
        Raylib.DrawCircleV(position, bulletRadius, Color.BEIGE);
    }
}