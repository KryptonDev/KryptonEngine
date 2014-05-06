using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KryptonEngine.Particle;
using KryptonEngine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace KryptonEngine.Particle
{
    class Emitter : GameObject
    {
        #region Properties

        private List<Modifier> mEmitterModifer;
        private List<Particle> mEmitterParticel;
        
        #region Getter & Setter

        #endregion

        #endregion

        #region Constructor

        #endregion

        #region Methods

        public void addModifier(Modifier pModifier)
        {
            if(pModifier.Type == ModifierType.EmitterModifier) this.mEmitterModifer.Add(pModifier);
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch pSpriteBatch)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
