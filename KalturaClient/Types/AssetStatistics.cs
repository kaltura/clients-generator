// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platfroms allow them to do with
// text.
//
// Copyright (C) 2006-2017  Kaltura Inc.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
// @ignore
// ===================================================================================================
using System;
using System.Xml;
using System.Collections.Generic;
using Kaltura.Enums;
using Kaltura.Request;

namespace Kaltura.Types
{
	public class AssetStatistics : ObjectBase
	{
		#region Constants
		public const string ASSET_ID = "assetId";
		public const string LIKES = "likes";
		public const string VIEWS = "views";
		public const string RATING_COUNT = "ratingCount";
		public const string RATING = "rating";
		public const string BUZZ_SCORE = "buzzScore";
		#endregion

		#region Private Fields
		private int _AssetId = Int32.MinValue;
		private int _Likes = Int32.MinValue;
		private int _Views = Int32.MinValue;
		private int _RatingCount = Int32.MinValue;
		private float _Rating = Single.MinValue;
		private BuzzScore _BuzzScore;
		#endregion

		#region Properties
		public int AssetId
		{
			get { return _AssetId; }
			set 
			{ 
				_AssetId = value;
				OnPropertyChanged("AssetId");
			}
		}
		public int Likes
		{
			get { return _Likes; }
			set 
			{ 
				_Likes = value;
				OnPropertyChanged("Likes");
			}
		}
		public int Views
		{
			get { return _Views; }
			set 
			{ 
				_Views = value;
				OnPropertyChanged("Views");
			}
		}
		public int RatingCount
		{
			get { return _RatingCount; }
			set 
			{ 
				_RatingCount = value;
				OnPropertyChanged("RatingCount");
			}
		}
		public float Rating
		{
			get { return _Rating; }
			set 
			{ 
				_Rating = value;
				OnPropertyChanged("Rating");
			}
		}
		public BuzzScore BuzzScore
		{
			get { return _BuzzScore; }
			set 
			{ 
				_BuzzScore = value;
				OnPropertyChanged("BuzzScore");
			}
		}
		#endregion

		#region CTor
		public AssetStatistics()
		{
		}

		public AssetStatistics(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "assetId":
						this._AssetId = ParseInt(propertyNode.InnerText);
						continue;
					case "likes":
						this._Likes = ParseInt(propertyNode.InnerText);
						continue;
					case "views":
						this._Views = ParseInt(propertyNode.InnerText);
						continue;
					case "ratingCount":
						this._RatingCount = ParseInt(propertyNode.InnerText);
						continue;
					case "rating":
						this._Rating = ParseFloat(propertyNode.InnerText);
						continue;
					case "buzzScore":
						this._BuzzScore = ObjectFactory.Create<BuzzScore>(propertyNode);
						continue;
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAssetStatistics");
			kparams.AddIfNotNull("assetId", this._AssetId);
			kparams.AddIfNotNull("likes", this._Likes);
			kparams.AddIfNotNull("views", this._Views);
			kparams.AddIfNotNull("ratingCount", this._RatingCount);
			kparams.AddIfNotNull("rating", this._Rating);
			kparams.AddIfNotNull("buzzScore", this._BuzzScore);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ASSET_ID:
					return "AssetId";
				case LIKES:
					return "Likes";
				case VIEWS:
					return "Views";
				case RATING_COUNT:
					return "RatingCount";
				case RATING:
					return "Rating";
				case BUZZ_SCORE:
					return "BuzzScore";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

