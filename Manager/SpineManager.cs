///**************************************************************
// * (c) Jens Richter 2014
// *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Spine;
using KryptonEngine.Entities;


namespace KryptonEngine.Manager
{
  public class SpineManager : Manager<RawSpineData>
  {
    #region Singleton

    private static SpineManager mInstance;
    public static SpineManager Instance { get { if (mInstance == null) mInstance = new SpineManager(); return mInstance; } }

    #endregion

    #region Properties
    #endregion

    #region Constructor

    SpineManager()
    {
    }

    #endregion

    #region Methods

    public override void LoadContent()
    {
      Add("fluffy");
    }

    public override void Unload()
    {
      mRessourcen.Clear();
    }

    /// <summary>
    /// Fügt ein neues Element in mRessourcenManager ein.
    /// </summary>
    /// <param name="pName">Name des Skeletons.</param>
    /// <param name="pPath">Pfad zu den Skeleton-Daten.</param>
    public override RawSpineData Add(String pName, String pPath)
    {
      if (!mRessourcen.ContainsKey(pName))
      {
        RawSpineData data = new RawSpineData(pPath, pName);

        mRessourcen.Add(pName, data);

        return data;
      }
    
      return (RawSpineData)mRessourcen[pName];
    }

    /// <summary>
    /// Fügt ein neues Element in mRessourcenManager ein.
    /// </summary>
    /// <param name="pName">Name des Skeletons.</param>
    public void Add(String pName)
    {
      if (!mRessourcen.ContainsKey(pName))
      {
        RawSpineData data = new RawSpineData(pName);
        mRessourcen.Add(pName, data);
      }
    }

    /// <summary>
    /// Gibt RawSpineData zurück.
    /// </summary>
    public override RawSpineData GetElementByString(string pElementName)
    {
      if (mRessourcen.ContainsKey(pElementName))
        return mRessourcen[pElementName];

      throw new ArgumentException("Element not found!");
    }

    public Skeleton NewSkeleton(string pName, float pScale)
    {
      RawSpineData TmpSpineData = this.GetElementByString(pName);
      TmpSpineData.json.Scale = SpineSettings.GetScaling(pName); //Set Scaling
      SkeletonData TmpSkeletonData = TmpSpineData.json.ReadSkeletonData(SpineSettings.DefaultDataPath + pName + ".json"); //Apply Json with Scaling to get skelData
      return new Skeleton(TmpSkeletonData);
    }

    public AnimationState NewAnimationState(SkeletonData pSkeletonData)
    {
      AnimationStateData TmpAnimationStateData = new AnimationStateData(pSkeletonData);
      SpineSettings.SetFadingSettings(TmpAnimationStateData);//Set mixing between animations
      return new AnimationState(TmpAnimationStateData);
    }
    #endregion
  }
}
