using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace KryptonEngine.Controls
{
	public class InputHelper
	{
		private class Input //Input um Mapping zu Button & Keys  zu speichern
		{
			public Buttons Button;
			public Keys KeyPlayer1;
			public Keys KeyPlayer2;

			public Input(Buttons pButton ,Keys pKeyPlayer1, Keys pKeyPlayer2)
			{
				Button = pButton;
				KeyPlayer1 = pKeyPlayer1;
				KeyPlayer2 = pKeyPlayer2;
			}
		}

		#region Singleton

        private static InputHelper mPlayer1;
		private static InputHelper mPlayer2;
        public static InputHelper Player1
        {
            get
            {
                if (mPlayer1 == null) mPlayer1 = new InputHelper(PlayerIndex.One);
                return mPlayer1;
            }
        }
		public static InputHelper Player2
        {
            get
            {
                if (mPlayer2 == null) mPlayer2 = new InputHelper(PlayerIndex.Two);
                return mPlayer2;
            }
        }

        #endregion

		#region Properties

		private PlayerIndex mPlayer;

		private GamePadState mGamepadStateCurrent;
		private GamePadState mGamepadStateBefore;

		private static KeyboardState mKeyboardStateCurrent;
		private static KeyboardState mKeyboardStateBefore;

		private static Input mPause = new Input(Buttons.Start, Keys.Escape, Keys.Escape);
		private static Input mAction = new Input(Buttons.X, Keys.Space, Keys.RightControl);

		private static Input mMoveUp = new Input(Buttons.LeftThumbstickUp, Keys.W, Keys.Up);
		private static Input mMoveDown = new Input(Buttons.LeftThumbstickDown, Keys.S, Keys.Down);
		private static Input mMoveLeft = new Input(Buttons.LeftThumbstickLeft, Keys.A, Keys.Left);
		private static Input mMoveRight = new Input(Buttons.LeftThumbstickRight, Keys.D, Keys.Right);

		#endregion

		#region Getter & Setter

		public bool Connected { get { return mGamepadStateCurrent.IsConnected; } }
		
		//Movement
		public Vector2 Movement { get
		{
			Vector2 TmpMovement = Vector2.Zero;
			if (InputPressed(mMoveUp))
				TmpMovement.Y--;
			if (InputPressed(mMoveDown))
				TmpMovement.Y++;
			if (InputPressed(mMoveLeft))
				TmpMovement.X--;
			if (InputPressed(mMoveRight))
				TmpMovement.X++;
			return TmpMovement;
		} }

		//Pause
		public bool Pause { get { return InputJustPressed(mPause); } }
		public bool Action { get { return InputJustPressed(mAction); } }

		#endregion

		#region Constructor

		InputHelper(PlayerIndex pPlayer)
		{
			mPlayer = pPlayer;
		}

		#endregion

		#region InputState Methods

		#region InputStates

		private bool InputJustPressed(Input pInput)
		{
			if (Connected)
				return ButtonJustPressed(pInput.Button);
			return KeyJustPressed(PlayerMappedKey(pInput));
		}

		private bool InputJustReleased(Input pInput)
		{
			if (Connected)
				return ButtonJustReleased(pInput.Button);
			return KeyJustReleased(PlayerMappedKey(pInput));
		}

		private bool InputPressed(Input pInput)
		{
			if (Connected)
				return ButtonPressed(pInput.Button);
			return KeyPressed(PlayerMappedKey(pInput));
		}

		private bool InputReleased(Input pInput)
		{
			if (Connected)
				return ButtonReleased(pInput.Button);
			return KeyReleased(PlayerMappedKey(pInput));
		}

		#endregion

		#region ButtonStates

		private bool ButtonJustPressed(Buttons pButton)
		{
			return (mGamepadStateBefore.IsButtonUp(pButton) && mGamepadStateCurrent.IsButtonDown(pButton)) ? true : false;
		}

		private bool ButtonJustReleased(Buttons pButton)
		{
			return (mGamepadStateBefore.IsButtonDown(pButton) && mGamepadStateCurrent.IsButtonUp(pButton)) ? true : false;
		}

		private bool ButtonPressed(Buttons pButton)
		{
			return mGamepadStateCurrent.IsButtonDown(pButton);
		}

		private bool ButtonReleased(Buttons pButton)
		{
			return mGamepadStateCurrent.IsButtonUp(pButton);
		}

		#endregion

		#region KeyboardStates

		private bool KeyJustPressed(Keys pKey)
		{
			return (mKeyboardStateBefore.IsKeyUp(pKey) && mKeyboardStateCurrent.IsKeyDown(pKey)) ? true : false;
		}

		private bool KeyJustReleased(Keys pKey)
		{
			return (mKeyboardStateBefore.IsKeyDown(pKey) && mKeyboardStateCurrent.IsKeyUp(pKey)) ? true : false;
		}

		private bool KeyPressed(Keys pKey)
		{
			return mKeyboardStateCurrent.IsKeyDown(pKey);
		}

		private bool KeyReleased(Keys pKey)
		{
			return mKeyboardStateCurrent.IsKeyUp(pKey);
		}

		#endregion

		private Keys PlayerMappedKey(Input pInput) //Map Input.pKey to mPlayer
		{
			if (mPlayer == PlayerIndex.Two)
				return pInput.KeyPlayer2;
			return pInput.KeyPlayer1;
		}

		#endregion

		#region Methods

		private void UpdateInstance()
		{
			mGamepadStateBefore = mGamepadStateCurrent;
			mGamepadStateCurrent = GamePad.GetState(mPlayer, GamePadDeadZone.Circular);
		}

		public static void Update()
		{
			//Update Gamepad
			Player1.UpdateInstance();
			Player2.UpdateInstance();
			//Update Keyboard
			mKeyboardStateBefore = mKeyboardStateCurrent;
			mKeyboardStateCurrent = Keyboard.GetState();
		}
		
		#endregion
	}
}
