using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using Raylib_cs;
using R = Raylib_cs.Raylib;

string gameName = "MugArm";

float playerSpeed = 15f;
int playerWidth = 50;
int playerHeight = 50;
int[] playerStartingPos = new int[] { 960, 540 };

float accel = 0;
bool jumpBool = false;

bool onFloor = false;
bool onPlatform = false;

int health = 3;
int heartX = 1650;
int heartY = 75;

Color playerColor = Color.WHITE;

R.InitWindow(1920, 1080, gameName);
R.SetTargetFPS(60);

float screenWidth = R.GetScreenWidth();
float screenHeight = R.GetScreenHeight();

(Texture2D, Texture2D, Texture2D) playerTextureTuple = PlayerImage.LoadPlayerImage(playerWidth, playerHeight);

Texture2D playerTextureNormal = playerTextureTuple.Item1;
Texture2D playerTextureLeft = playerTextureTuple.Item2;
Texture2D playerTextureRight = playerTextureTuple.Item3;

Texture2D playerTexture = playerTextureNormal;

Image heartImage = R.LoadImage("Images/heartImage.png");
R.ImageResize(ref heartImage, 50, 50);
Texture2D heartTexture = R.LoadTextureFromImage(heartImage);

List<Rectangle> platforms = new List<Rectangle>();
List<Rectangle> walls = new List<Rectangle>();

Rectangle playerRect = new Rectangle(playerStartingPos[0], playerStartingPos[1], playerWidth, playerHeight);

Rectangle earthRect = new Rectangle(0, 880, screenWidth * 2, 300);
platforms.Add(earthRect);

Rectangle skyRect = new Rectangle(0, 0, screenWidth * 2, 880);

Rectangle leftWall = new Rectangle(-1000, 0, 1000, screenHeight);
walls.Add(leftWall);

Rectangle platformOne = new Rectangle(600, screenHeight - 450, 150, 25);
platforms.Add(platformOne);

List<Bullet> bullets = new List<Bullet>();

Camera2D camera = new Camera2D();
camera.target = new Vector2(screenWidth / 2, screenHeight / 2);
camera.zoom = 1f;
camera.offset = new Vector2(screenWidth / 2, screenHeight / 2);

while (!R.WindowShouldClose())
{
    R.BeginDrawing();
    R.BeginMode2D(camera);

    R.ClearBackground(Color.WHITE);

    R.DrawRectangleRec(earthRect, Color.GREEN);
    R.DrawRectangleRec(skyRect, Color.SKYBLUE);
    R.DrawRectangleRec(leftWall, Color.GREEN);
    R.DrawRectangleRec(platformOne, Color.BLACK);

    R.DrawTexture(playerTexture, (int)playerRect.x, (int)playerRect.y, playerColor);

    foreach (Bullet b in bullets)
    {
        b.Draw();
        b.Update();
    }

    R.EndMode2D();

    for (var i = 0; i < health; i++)
    {
        R.DrawTexture(heartTexture, heartX, heartY, Color.WHITE);
        heartX += 75;
    }

    R.EndDrawing();

    heartX = 1650;

    playerTexture = playerTextureNormal;

    (Vector2, Texture2D) playerMovementHorizontalTuple = PlayerMovemnt.HorizontalMovement(playerSpeed, playerTextureRight, playerTextureLeft, playerTexture);

    Vector2 xMovement = playerMovementHorizontalTuple.Item1;
    playerTexture = playerMovementHorizontalTuple.Item2;

    playerRect.x += xMovement.X;
    camera.target.X += xMovement.X;

    foreach (var item in platforms)
    {
        if (R.CheckCollisionRecs(playerRect, item))
        {
            playerRect.x -= xMovement.X;
            camera.target.X -= xMovement.X;
        }
    }

    foreach (var item in walls)
    {
        if (R.CheckCollisionRecs(playerRect, item))
        {
            playerRect.x -= xMovement.X;
            camera.target.X -= xMovement.X;
        }
    }

    (Vector2, bool) upWardMovementTuple = PlayerMovemnt.UpwardMovement(onFloor, jumpBool, playerSpeed, onPlatform);

    Vector2 yMovement = upWardMovementTuple.Item1;
    jumpBool = upWardMovementTuple.Item2;

    playerRect.y += yMovement.Y;

    foreach (var item in platforms)
    {
        if (R.CheckCollisionRecs(playerRect, item))
        {
            playerRect.y -= yMovement.Y;
            jumpBool = false;
        }
    }

    (Rectangle, bool) ifOnFloorTuple = Check.IfOnFloor(playerRect, playerHeight, onFloor, onPlatform);

    playerRect = ifOnFloorTuple.Item1;
    onFloor = ifOnFloorTuple.Item2;

    if (onFloor)
    {
        accel = 0;
        // jumpBool = false;
    }
    else
    {
        (Vector2, float) gravityTuple = PlayerMovemnt.Gravity(accel);
        yMovement = gravityTuple.Item1;
        accel = gravityTuple.Item2;

        playerRect.y += yMovement.Y;
    }

    if (R.CheckCollisionRecs(playerRect, earthRect))
    {
        playerRect.y = earthRect.y - playerHeight;

        onPlatform = true;
        jumpBool = false;
    }
    else
    {
        onPlatform = false;
    }

    foreach (var item in platforms)
    {
        if (R.CheckCollisionRecs(playerRect, item))
        {
            playerRect.y = item.y - playerHeight;
            accel = 0;
            jumpBool = false;
        }
    }

    if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
    {
        bullets.Add(new Bullet() { position = new Vector2(playerRect.x + playerWidth, playerRect.y + playerHeight/2) });
    }
}

R.CloseWindow();