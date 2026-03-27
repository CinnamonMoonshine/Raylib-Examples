using System.Numerics;
using Raylib_cs;

namespace Game;

public static class Program
{
    private static void Main(string[] args)
    {
        Raylib.InitWindow(800, 600, "Game Name"); // create a new window
        Raylib.SetTargetFPS(60);
        
        // placeholder texture:
        Image image = Raylib.GenImageChecked(16, 16, 2, 2, Color.Purple, Color.Black);
        Texture2D texture = Raylib.LoadTextureFromImage(image); // upload texture to VRAM
        Raylib.UnloadImage(image); // unload image from ram
        
        // you can load a custom texture from path:
        //Texture2D texture = Raylib.LoadTexture("C:/Images/Placeholder.png");

        // set texture filter to use in pixel art
        Raylib.SetTextureFilter(texture, TextureFilter.Point);

        Vector2 playerPosition = new Vector2(0f, 0f);
        Vector2 playerSize = new Vector2(16f, 16f);

        Camera2D camera = new Camera2D()
        {
            Zoom = 2f,
            Rotation = 0f,
            Offset = new Vector2(
                (Raylib.GetScreenWidth() * 0.5f),
                (Raylib.GetScreenHeight() * 0.5f)
            ),
            Target = playerPosition + playerSize * 0.5f
        };

        bool cameraFollow = true;
        
        while (!Raylib.WindowShouldClose()) // main loop
        {
            // move player
            if (Raylib.IsKeyDown(KeyboardKey.W)) playerPosition.Y -= 3;
            else if (Raylib.IsKeyDown(KeyboardKey.S)) playerPosition.Y += 3;
            if (Raylib.IsKeyDown(KeyboardKey.A)) playerPosition.X -= 3;
            else if (Raylib.IsKeyDown(KeyboardKey.D)) playerPosition.X += 3;
            if (Raylib.IsKeyDown(KeyboardKey.F1)) cameraFollow = !cameraFollow;
            if (Raylib.IsKeyDown(KeyboardKey.Escape)) Raylib.CloseWindow();

            if (cameraFollow)
            {
                camera.Target = playerPosition + playerSize * 0.5f ; // update camera
            }
            
            Raylib.BeginDrawing();
            Raylib.BeginMode2D(camera); // start rendering sprites using camera
            Raylib.ClearBackground(new Color(99, 149, 238)); // clean screen
            
            Raylib.DrawTexturePro(
                texture, 
                new Rectangle(0, 0, texture.Width, texture.Height), // texture region to be drawn
                new Rectangle(playerPosition.X, playerPosition.Y, playerSize.X, playerSize.Y), // the area of ​​the screen that will draw, in this case player position, with a size of 16x16.
                Vector2.Zero, // the pivot point in the image
                0f, 
                Color.White // the color that will be applied in this texture
            );
            
            Raylib.EndMode2D();
            
			// debug texts:
            Raylib.DrawTextPro(Raylib.GetFontDefault(), $"P1 Pos: {playerPosition}", new Vector2(2, 4), Vector2.Zero, 0f, 30, 1f, Color.Yellow);
            Raylib.DrawTextPro(Raylib.GetFontDefault(), $"Camera Pos: {camera.Target}", new Vector2(2, 31), Vector2.Zero, 0f, 30, 1f, Color.Yellow);
			
            Raylib.EndDrawing();
        }
    }
}