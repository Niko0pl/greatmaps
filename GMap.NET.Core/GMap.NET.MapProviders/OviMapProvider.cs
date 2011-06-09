﻿
namespace GMap.NET.MapProviders
{
   using System;
   using GMap.NET.Projections;

   public abstract class OviMapProviderBase : GMapProvider
   {
      #region GMapProvider Members
      public override Guid Id
      {
         get
         {
            throw new NotImplementedException();
         }
      }

      public override string Name
      {
         get
         {
            throw new NotImplementedException();
         }
      }

      readonly MercatorProjection projection = new MercatorProjection();
      public override PureProjection Projection
      {
         get
         {
            return projection;
         }
      }

      GMapProvider[] overlays;
      public override GMapProvider[] Overlays
      {
         get
         {
            if(overlays == null)
            {
               overlays = new GMapProvider[] { this };
            }
            return overlays;
         }
      }

      public override PureImage GetTileImage(GPoint pos, int zoom)
      {
         throw new NotImplementedException();
      }
      #endregion

      protected static readonly string UrlServerLetters = "bcde";
   }

   /// <summary>
   /// OviMap provider
   /// </summary>
   public class OviMapProvider : OviMapProviderBase
   {
      public static readonly OviMapProvider Instance;

      OviMapProvider()
      {
      }

      static OviMapProvider()
      {
         Instance = new OviMapProvider();
      }

      #region GMapProvider Members

      readonly Guid id = new Guid("30DC1083-AC4D-4471-A232-D8A67AC9373A");
      public override Guid Id
      {
         get
         {
            return id;
         }
      }

      readonly string name = "OviMap";
      public override string Name
      {
         get
         {
            return name;
         }
      }

      public override PureImage GetTileImage(GPoint pos, int zoom)
      {
         string url = MakeTileImageUrl(pos, zoom, Language);

         return GetTileImageUsingHttp(url);
      }

      #endregion

      string MakeTileImageUrl(GPoint pos, int zoom, string language)
      {
         // http://c.maptile.maps.svc.ovi.com/maptiler/v2/maptile/newest/normal.day/12/2321/1276/256/png8

         return string.Format(UrlFormat, UrlServerLetters[GetServerNum(pos, 4)], zoom, pos.X, pos.Y);
      }

      static readonly string UrlFormat = "http://{0}.maptile.maps.svc.ovi.com/maptiler/v2/maptile/newest/normal.day/{1}/{2}/{3}/256/png8";
   }
}