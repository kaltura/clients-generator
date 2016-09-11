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
// Copyright (C) 2006-2011  Kaltura Inc.
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
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace Kaltura
{
    class KalturaClientTester : IKalturaLogger
    {
        private const int PARTNER_ID = @PARTNER_ID@; //enter your partner id
        private const string OPERATOR_USERNAME = "@OPERATOR_USERNAME@";
        private const string OPERATOR_PASSWORD = "@OPERATOR_PASSWORD@";
        private const string MASTER_USERNAME = "@MASTER_USERNAME@";
        private const string MASTER_PASSWORD = "@MASTER_PASSWORD@";
        private const string MASTER_DEVICE = "@MASTER_DEVICE@";
        private const string SERVICE_URL = "@SERVICE_URL@";


        private static string uniqueTag;
        private static KalturaClient client;

        public void Log(string msg)
        {
            Console.WriteLine(msg);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Starting C# Kaltura API Client Library");
            int code = 0;
            uniqueTag = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);

            KalturaConfiguration config = new KalturaConfiguration();
            config.ServiceUrl = SERVICE_URL;
            client = new KalturaClient(config);

            try
            {
                Login(OPERATOR_USERNAME, OPERATOR_PASSWORD);
            }
            catch (KalturaAPIException e)
            {
                Console.WriteLine("Failed Login as operator: " + e.Message);
                code = -1;
            }

            try
            {
                Login(MASTER_USERNAME, MASTER_PASSWORD, MASTER_DEVICE);
            }
            catch (KalturaAPIException e)
            {
                Console.WriteLine("Failed Login as master: " + e.Message);
                code = -1;
            }

            try
            {
                ListUserRoles();
            }
            catch (KalturaAPIException e)
            {
                Console.WriteLine("Failed ListUserRoles: " + e.Message);
                code = -1;
            }

            try
            {
                ListAssets();
            }
            catch (KalturaAPIException e)
            {
                Console.WriteLine("Failed ListAssets: " + e.Message);
                code = -1;
            }

            try
            {
                ListOttUsers();
            }
            catch (KalturaAPIException e)
            {
                Console.WriteLine("Failed ListOttUsers: " + e.Message);
                code = -1;
            }

            try
            {
                AdvancedMultiRequestExample();
            }
            catch (KalturaAPIException e)
            {
                Console.WriteLine("Failed AdvancedMultiRequestExample: " + e.Message);
                code = -1;
            }
			
            if (code == 0)
            {
                Console.WriteLine("Finished running client library tests");
            }

            Environment.Exit(code);
        }

        /// <summary>
        /// Login
        /// </summary>
        private static void Login(string username, string password, string udid = null)
        {
            KalturaLoginResponse loginResponse = client.OttUserService.Login(PARTNER_ID, username, password, null, udid);
            client.KS = loginResponse.LoginSession.Ks;
        }

        private static void ListUserRoles()
        {
            KalturaUserRoleListResponse userRolesList = client.UserRoleService.List();
        }

        private static void ListAssets()
        {
            KalturaAssetListResponse assetsList = client.AssetService.List();
            foreach(KalturaAsset asset in assetsList.Objects)
            {
                KalturaAsset getAsset;

                if(asset is KalturaMediaAsset)
                    getAsset = client.AssetService.Get(asset.Id.ToString(), KalturaAssetReferenceType.MEDIA);

                if (asset is KalturaProgramAsset)
                    getAsset = client.AssetService.Get(asset.Id.ToString(), KalturaAssetReferenceType.EPG_EXTERNAL);
            }
        }

        private static void ListOttUsers()
        {
            KalturaOTTUserFilter filter = new KalturaOTTUserFilter();
            KalturaOTTUserListResponse usersList = client.OttUserService.List(filter);
        }

        /// <summary>
        /// Shows how to perform few actions in a single request
        /// </summary>
        private static void AdvancedMultiRequestExample()
        {
            // TODO
        }
    }
}
