using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using Raylib_cs;
using R = Raylib_cs.Raylib;

string gameName = "MugArm";

float playerSpeed = 5f;
int playerWidth = 50;
int playerHeight = 50;
int[] playerStartingPos = new int[] { 50, 50 };

float accel = 0;
int jumpInt = 100;

bool onFloor = false;
bool onPlatform = false;

Color playerColor = Color.WHITE;

R.InitWindow(800, 600, gameName);
R.SetTargetFPS(60);

Texture2D playerTexture = PlayerImage.LoadPlayerImage(playerWidth, playerHeight);

List<Rectangle> platforms = new List<Rectangle>();

Rectangle playerRect = new Rectangle(playerStartingPos[0], playerStartingPos[1], playerWidth, playerHeight);
Rectangle platformRect = new Rectangle(600, 550, 100, 20);

platforms.Add(platformRect);

while (!R.WindowShouldClose())
{
    R.BeginDrawing();
    R.ClearBackground(Color.WHITE);

    R.DrawRectangleRec(platformRect, Color.BLACK);
    R.DrawTexture(playerTexture, (int)playerRect.x, (int)playerRect.y, playerColor);

    R.EndDrawing();

    Console.WriteLine(platforms);

    Vector2 xMovement = PlayerMovemnt.HorizontalMovement(playerSpeed);
    playerRect.x += xMovement.X;

    foreach (var item in platforms)
    {
        if (R.CheckCollisionRecs(playerRect, item))
        {
            playerRect.x -= xMovement.X;
        }
    }

    (Vector2, int) upWardMovementTuple = PlayerMovemnt.UpwardMovement(onFloor, jumpInt, playerSpeed);

    Vector2 yMovement = upWardMovementTuple.Item1;
    jumpInt = upWardMovementTuple.Item2;

    playerRect.y += yMovement.Y;

    foreach (var item in platforms)
    {
        if (R.CheckCollisionRecs(playerRect, item))
        {
            playerRect.y -= yMovement.Y;
        }
    }

    (Rectangle, bool) ifOnFloorTuple = Check.IfOnFloor(playerRect, playerHeight, onFloor, onPlatform);

    playerRect = ifOnFloorTuple.Item1;
    onFloor = ifOnFloorTuple.Item2;

    if (onFloor)
    {
        accel = 0;
        jumpInt = 100;
    }
    else
    {
        (Vector2, float) gravityTuple = PlayerMovemnt.Gravity(accel);
        yMovement = gravityTuple.Item1;
        accel = gravityTuple.Item2;

        playerRect.y += yMovement.Y;
    }

    foreach (var item in platforms)
    {
        if (R.CheckCollisionRecs(playerRect, item))
        {
            while (R.CheckCollisionRecs(playerRect, item))
            {
                playerRect.y -= 1;
            }
            onPlatform = true;
        }
        else
        {
            onPlatform = false;
        }
    }
}

R.CloseWindow();