using KryptonEngine.Controls;
using KryptonEngine.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HanselAndGretel.Data
{
	public class Gretel : Player
	{
		#region Properties

		public int Chalk;

		#endregion

		#region Getter & Setter

		#endregion

		#region Constructor

		public Gretel()
		{
			Initialize();
		}

		#endregion

		#region OverrideMethods

		public override void Initialize()
		{
			base.Initialize();
			mInput = InputHelper.Player2;
			mCollisionBox.Width = 40;
			mCollisionBox.Height = 128;
			mModel = new SpineObject("sweetcheeks");
			mHandicaps.Add(Activity.JumpOverGap);
		}

		#endregion

		#region Methods

		public void TryToGrabItem()
		{
			
		}

		#endregion
	}
}
