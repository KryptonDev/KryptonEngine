/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KryptonEngine.Interface;
using System.Xml.Serialization;

namespace KryptonEngine.Entities
{
	[Serializable, XmlInclude(typeof(Sprite)), XmlInclude(typeof(InteractiveObject))]
    public class GameObject : BaseObject
    {
        #region Properties

        protected Vector2 mPosition;
        protected Rectangle mCollisionBox = new Rectangle();
        protected Color mDebugColor = Color.Yellow;
        protected bool mVisible;
		protected int mDrawZ;
        #endregion

        #region Getter & Setter

        public Vector2 Position 
        { 
            set 
            { 
                mPosition = value;
                mCollisionBox.X = (int)value.X;
                mCollisionBox.Y = (int)value.Y;
            }
            get { return mPosition; } 
        }
        public int PositionX 
        { 
            set 
            { 
                mPosition.X = value;
                mCollisionBox.X = value;
            }
            get { return (int)mPosition.X; } 
        }
        public int PositionY 
        { 
            set 
            {
                mPosition.Y = value;
                mCollisionBox.Y = value;
            }
            get { return (int)mPosition.Y; } 
        }
		public Rectangle CollisionBox { get { return mCollisionBox; } set { mCollisionBox = value; } }
        public bool IsVisible { get { return mVisible; } set { mVisible = value; } }
		public int DrawZ { get { return mDrawZ; } set { mDrawZ = value; } }
        #endregion
        #region Constructor

        public GameObject()
            : base()
        {
            Position = Vector2.Zero;
            mVisible = true;
        }

        public GameObject(Vector2 pPosition)
            : base()
        {
            Position = pPosition;
            mVisible = true;
        }

        #endregion

        #region Methods

        public virtual void Draw(SpriteBatch spriteBatch) { }

        public override string GetInfo()
        {
            String tmpInfo;

            tmpInfo = "Objekt ID: " + mId;
            tmpInfo += "\nPosition: " + Position;
            tmpInfo += "\nRectangle Dim.: " + mCollisionBox.Width + "; " + mCollisionBox.Height;

            return tmpInfo;
        }

        #endregion
    }
}
