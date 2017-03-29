using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Resources.Models;

public class StairsModel : BaseModel<StairsModel>
{
    public override int ID => 16;

    static StairsModel()
    {
        //Lower stair, front:
        //left vertex:
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, 0.5f), new Vector3(0, 0, 1), new Vector2(0.0f, 0.5f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, 0.5f), new Vector3(0, 0, 1), new Vector2(1.0f, 0.5f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0, 0, 1), new Vector2(0.0f, 1.0f)));
        //right vertex:
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, 0.5f), new Vector3(0, 0, 1), new Vector2(1.0f, 0.5f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(0, 0, 1), new Vector2(1.0f, 1.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0, 0, 1), new Vector2(0.0f, 1.0f)));

        //Lower stair, top:
        //Left vertex:
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, 0), new Vector3(0, 1, 0), new Vector2(0.0f, 0.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, 0), new Vector3(0, 1, 0), new Vector2(1.0f, 0.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, 0.5f), new Vector3(0, 1, 0), new Vector2(0.0f, 0.5f)));
        //Right vertex:
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, 0), new Vector3(0, 1, 0), new Vector2(1.0f, 0.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, 0.5f), new Vector3(0, 1, 0), new Vector2(1.0f, 0.5f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, 0.5f), new Vector3(0, 1, 0), new Vector2(0.0f, 0.5f)));

        //Upper stair, front:
        //left vertex:
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0), new Vector3(0, 0, 1), new Vector2(0.0f, 0.5f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0), new Vector3(0, 0, 1), new Vector2(1.0f, 0.5f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, 0), new Vector3(0, 0, 1), new Vector2(0.0f, 1.0f)));
        //right vertex:
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0), new Vector3(0, 0, 1), new Vector2(1.0f, 0.5f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, 0), new Vector3(0, 0, 1), new Vector2(1.0f, 1.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, 0), new Vector3(0, 0, 1), new Vector2(0.0f, 1.0f)));

        //Upper stair, top:
        //Left vertex:
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0, 1, 0), new Vector2(0.0f, 0.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(0, 1, 0), new Vector2(1.0f, 0.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0), new Vector3(0, 1, 0), new Vector2(0.0f, 0.5f)));
        //Right vertex:
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(0, 1, 0), new Vector2(1.0f, 0.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0), new Vector3(0, 1, 0), new Vector2(1.0f, 0.5f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0), new Vector3(0, 1, 0), new Vector2(0.0f, 0.5f)));

        //Back:
        //Left vertex:
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(0, 0, -1), new Vector2(0.0f, 0.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0, 0, -1), new Vector2(1.0f, 0.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0, 0, -1), new Vector2(0.0f, 1.0f)));
        //Right vertex:
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0, 0, -1), new Vector2(1.0f, 0.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0, 0, -1), new Vector2(1.0f, 1.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0, 0, -1), new Vector2(0.0f, 1.0f)));

        //Left side, lower:
        //Left vertex:
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, -0.5f), new Vector3(-1, 0, 0), new Vector2(0.0f, 0.5f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, 0.5f), new Vector3(-1, 0, 0), new Vector2(1.0f, 0.5f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-1, 0, 0), new Vector2(0.0f, 1.0f)));
        //Right vertex:
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, 0.5f), new Vector3(-1, 0, 0), new Vector2(1.0f, 0.5f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(-1, 0, 0), new Vector2(1.0f, 1.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-1, 0, 0), new Vector2(0.0f, 1.0f)));

        //Left side, upper:
        //Left vertex:
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(-1, 0, 0), new Vector2(0.0f, 0.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0), new Vector3(-1, 0, 0), new Vector2(0.5f, 0.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, -0.5f), new Vector3(-1, 0, 0), new Vector2(0.0f, 0.5f)));
        //Right vertex:
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0), new Vector3(-1, 0, 0), new Vector2(0.5f, 0.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, 0), new Vector3(-1, 0, 0), new Vector2(0.5f, 0.5f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0, -0.5f), new Vector3(-1, 0, 0), new Vector2(0.0f, 0.5f)));

        //Right side, lower:
        //Left vertex:
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, 0.5f), new Vector3(1, 0, 0), new Vector2(0.0f, 0.5f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, -0.5f), new Vector3(1, 0, 0), new Vector2(1.0f, 0.5f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(1, 0, 0), new Vector2(0.0f, 1.0f)));
        //Right vertex:
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, -0.5f), new Vector3(1, 0, 0), new Vector2(1.0f, 0.5f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(1, 0, 0), new Vector2(1.0f, 1.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(1, 0, 0), new Vector2(0.0f, 1.0f)));

        //Right side, upper:
        //Left vertex:
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0), new Vector3(1, 0, 0), new Vector2(0.5f, 0.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(1, 0, 0), new Vector2(1.0f, 0.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, 0), new Vector3(1, 0, 0), new Vector2(0.5f, 0.5f)));
        //Right vertex:
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(1, 0, 0), new Vector2(1.0f, 0.0f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, -0.5f), new Vector3(1, 0, 0), new Vector2(1.0f, 0.5f)));
        VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0, 0), new Vector3(1, 0, 0), new Vector2(0.5f, 0.5f)));

        SetupVb();
    }

}