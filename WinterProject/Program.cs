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

List<Rectangle> platform = new List<Rectangle>();

Rectangle playerRect = new Rectangle(playerStartingPos[0], playerStartingPos[1], playerWidth, playerHeight);
Rectangle platformRect = new Rectangle(600, 550, 100, 20);

platform.add(platformRect);
while (!R.WindowShouldClose())
{
    R.BeginDrawing();
    R.ClearBackground(Color.WHITE);

    R.DrawRectangleRec(platformRect, Color.BLACK);
    R.DrawTexture(playerTexture, (int)playerRect.x, (int)playerRect.y, playerColor);

    R.EndDrawing();

    Vector2 xMovement = PlayerMovemnt.HorizontalMovement(playerSpeed);
    playerRect.x += xMovement.X;
    if (R.CheckCollisionRecs(playerRect, platformRect))
    {
        playerRect.x -= xMovement.X;
    }

    Vector2 yMovement = PlayerMovemnt.UpwardMovement(onFloor, jumpInt, playerSpeed).Item1;
    playerRect.y += yMovement.Y;
    if (R.CheckCollisionRecs(playerRect, platformRect))
    {
        playerRect.y -= yMovement.Y;
    }

    jumpInt = PlayerMovemnt.UpwardMovement(onFloor, jumpInt, playerSpeed).Item2;

    if (playerRect.y > 600 - playerHeight)
    {
        playerRect.y = 600 - playerHeight;
        onFloor = true;
    }
    else if (onPlatform)
    {
        onFloor = true;
    }
    else
    {
        onFloor = false;
    }

    if (onFloor)
    {
        accel = 0;
        jumpInt = 100;
    }
    else
    {
        yMovement = PlayerMovemnt.Gravity(accel).Item1;
        accel = PlayerMovemnt.Gravity(accel).Item2;

        playerRect.y += yMovement.Y;
    }
    if (R.CheckCollisionRecs(playerRect, platformRect))
    {
        while (R.CheckCollisionRecs(playerRect, platformRect))
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

R.CloseWindow();