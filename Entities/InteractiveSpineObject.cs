using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptonEngine.Entities
{
	public class InteractiveSpineObject : InteractiveObject
	{
		#region Properties

		protected SpineObject mModel;

		#endregion

		#region Getter & Setter

		public SpineObject Model { get { return mModel; } set { mModel = value; } }
		public DrawPackage DrawPackage { get { return new DrawPackage(Position, DrawZ, CollisionBox, mDebugColor, mModel.Skeleton); } }

		#endregion

		#region Constructor

		public InteractiveSpineObject()
			:base()
		{
			Initialize();
		}

		#endregion

		#region Override Methods

		public override void Initialize()
		{
			
		}

		#endregion
	}
}
