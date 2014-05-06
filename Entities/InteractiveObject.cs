using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptonEngine.Entities
{
  public class InteractiveObject : GameObject
  {
    #region Properties

    protected List<Rectangle> mActionRectList = new List<Rectangle>();
    protected List<Rectangle> mCollisionRectList = new List<Rectangle>();
    
    protected Vector2 mActionPosition1;
    protected Vector2 mActionPosition2;

    protected int mActionId;
    protected int mDrawZ;
    //protected GameObject mDrawObject;
    protected Texture2D mTexture;
    protected String mTextureName;
    #endregion

    #region Getter & Setter

    public List<Rectangle> ActionRectList { get { return mActionRectList; } set { mActionRectList = value; } }
    public List<Rectangle> CollisionRectList { get { return mCollisionRectList; } set { mCollisionRectList = value; } }
    public Vector2 ActionPosition1 { get { return mActionPosition1; } set { mActionPosition1 = value; } }
    public Vector2 ActionPosition2 { get { return mActionPosition2; } set { mActionPosition2 = value; } }
    public int ActionId { get { return mActionId; } set { mActionId = value; } }
    public int DrawZ { get { return mDrawZ; } set { mDrawZ = value; } }
    //public GameObject DrawObject { get { return mDrawObject; } set { mDrawObject = value; } }
    public Texture2D Texture { get { return mTexture; } set { mTexture = value; } }
    public String TextureName { get { return mTextureName; } set { mTextureName = value; } }
    #endregion

    #region Constructor

    public InteractiveObject() { }
    #endregion

    #region Override Methods

    public override void Update()
    {
      
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      if(mTexture != null)
        spriteBatch.Draw(mTexture, Position, Color.White);
    }
    #endregion

    #region Methods

    public Vector2 GetNearestStartPosition(Vector2 PlayerPosition)
    {
      float Distance1 = Vector2.Distance(PlayerPosition, mActionPosition1);
      float Distance2 = Vector2.Distance(PlayerPosition, mActionPosition2);

      return (Math.Min(Distance1, Distance2) == Distance1) ? mActionPosition1 : mActionPosition2;
    }
    #endregion
  }
}
