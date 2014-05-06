using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptonEngine.Particle
{
    public enum ModifierType
    {
        PartikelModifier,
        EmitterModifier
    }

    public abstract class Modifier
    {
        #region Properties

        private ModifierType mType;

        #region Getter & Setter
        
        public ModifierType Type { get { return mType; } }
        
        #endregion

        #endregion

        #region Constructor

        #endregion

        #region Methods

        #endregion

    }
}
