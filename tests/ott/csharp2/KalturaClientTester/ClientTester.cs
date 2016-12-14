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
using Kaltura.Services;
using Kaltura.Request;
using Kaltura.Types;
using Kaltura.Enums;

namespace Kaltura
{
    delegate void OnLogin(ClientTester tester);

    class ClientTester : ILogger
    {
        private const int PARTNER_ID = @PARTNER_ID@; //enter your partner id
        private const string SERVICE_URL = "@SERVICE_URL@";
        private const string OPERATOR_USERNAME = "@OPERATOR_USERNAME@";
        private const string OPERATOR_PASSWORD = "@OPERATOR_PASSWORD@";
        private const string MASTER_USERNAME = "@MASTER_USERNAME@";
        private const string MASTER_PASSWORD = "@MASTER_PASSWORD@";
        private const string MASTER_DEVICE = "@MASTER_DEVICE@";
        private const int MASTER_DEVICE_BRAND = @MASTER_DEVICE_BRAND@;

        private static int code = 0;

        private string uniqueTag;
        private string userId;
        private Client client;
        private int openTasks = 0;
        private OnLogin onLogin;

        static void Main(string[] args)
        {
            ClientTester operatorTester = new ClientTester(new OnLogin(OnOperatorLogin), OPERATOR_USERNAME, OPERATOR_PASSWORD);

            ClientTester masterTester = new ClientTester(new OnLogin(OnMasterLogin), MASTER_USERNAME, MASTER_PASSWORD, MASTER_DEVICE);

            while (masterTester.openTasks > 0 || operatorTester.openTasks > 0)
            {
                Thread.Sleep(100);
            }

            Console.WriteLine("Done");
            Environment.Exit(code);
        }

        private static void OnOperatorLogin(ClientTester tester)
        {
            tester.ListUserRoles();
        }

        private static void OnMasterLogin(ClientTester tester)
        {
            tester.ListAssets();
            tester.ListOttUsers();
            tester.ListHouseholdUsers();
            tester.GetHousehold();
            tester.SearchCatalog();
            tester.GetHouseholdDevice();
            tester.AddHouseholdDevice();
        }

        private ClientTester(OnLogin onLogin, string username, string password, string udid = null)
        {
            this.onLogin = onLogin;

            uniqueTag = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);

            Configuration config = new Configuration();
            config.ServiceUrl = SERVICE_URL;
            client = new Client(config);

            Login(username, password, udid);
        }

        public void Log(string msg)
        {
            Console.WriteLine(msg);
        }

        /// <summary>
        /// Login
        /// </summary>
        private void Login(string username, string password, string udid = null)
        {
            openTasks++;
            OttUserService.Login(PARTNER_ID, username, password, null, udid)
                .SetCompletion(new OnCompletedHandler<LoginResponse>(OnLoginComplete))
                .Execute(client);
        }

        public void OnLoginComplete(LoginResponse loginResponse, Exception error)
        {
            if (error != null)
            {
                Console.WriteLine("Failed to login: " + error.Message);
                code = -1;
                openTasks = 0;
                return;
            }

            client.KS = loginResponse.LoginSession.Ks;
            userId = loginResponse.User.Id;
            onLogin(this);
            openTasks--;
        }

        private void ListUserRoles()
        {
            openTasks ++;
            UserRoleService.List()
                .SetCompletion(new OnCompletedHandler<ListResponse<UserRole>>(OnUserRoleListComplete))
                .Execute(client);
        }

        public void OnUserRoleListComplete(ListResponse<UserRole> userRolesList, Exception error)
        {
            if (error != null)
            {
                Console.WriteLine("Failed to list user-roles: " + error.Message);
                code = -1;
                openTasks = 0;
                return;
            }

            openTasks--;
        }

        private void ListAssets()
        {
            openTasks++;
            AssetService.List()
                .SetCompletion(new OnCompletedHandler<ListResponse<Asset>>(OnAssetListComplete))
                .Execute(client);
        }

        public void OnAssetListComplete(ListResponse<Asset> assetsList, Exception error)
        {
            if (error != null)
            {
                Console.WriteLine("Failed to list assets: " + error.Message);
                code = -1;
                openTasks = 0;
                return;
            }

            openTasks--;
            foreach (Asset asset in assetsList.Objects)
            {
                if (asset is MediaAsset)
                    GetAsset(asset.Id.ToString(), AssetReferenceType.MEDIA);

                if (asset is ProgramAsset)
                    GetAsset(asset.Id.ToString(), AssetReferenceType.EPG_EXTERNAL);
            }
        }

        private void GetAsset(string id, AssetReferenceType assetReferenceType)
        {
            openTasks ++;
            AssetService.Get(id, assetReferenceType)
                .SetCompletion(new OnCompletedHandler<Asset>(OnAssetGetComplete))
                .Execute(client);
        }

        public void OnAssetGetComplete(Asset asset, Exception error)
        {
            if (error != null)
            {
                Console.WriteLine("Failed to get asset: " + error.Message);
                code = -1;
                openTasks = 0;
                return;
            }

            openTasks--;
        }

        private void ListOttUsers()
        {
            openTasks++;
            OTTUserFilter filter = new OTTUserFilter();
            OttUserService.List(filter)
                .SetCompletion(new OnCompletedHandler<ListResponse<OTTUser>>(OnOTTUserListComplete))
                .Execute(client);
        }

        public void OnOTTUserListComplete(ListResponse<OTTUser> usersList, Exception error)
        {
            if (error != null)
            {
                Console.WriteLine("Failed to list users: " + error.Message);
                code = -1;
                openTasks = 0;
                return;
            }

            openTasks--;
            foreach (OTTUser user in usersList.Objects)
            {
                if (user.Id.Equals(userId) && !(user.IsHouseholdMaster.HasValue && user.IsHouseholdMaster.Value))
                {
                    Console.WriteLine("Current user is not listed as master");
                    code = -1;
                    openTasks = 0;
                    return;
                }
            }
        }

        private void ListHouseholdUsers()
        {
            openTasks++;
            HouseholdUserFilter filter = new HouseholdUserFilter();
            HouseholdUserService.List(filter)
                .SetCompletion(new OnCompletedHandler<ListResponse<HouseholdUser>>(OnHouseholdUserListComplete))
                .Execute(client);
        }

        public void OnHouseholdUserListComplete(ListResponse<HouseholdUser> usersList, Exception error)
        {
            if (error != null)
            {
                Console.WriteLine("Failed to list household users: " + error.Message);
                code = -1;
                openTasks = 0;
                return;
            }

            openTasks--;
        }

        private void GetHousehold()
        {
            openTasks++;
            HouseholdService.Get()
                .SetCompletion(new OnCompletedHandler<Household>(OnHouseholdGetComplete))
                .Execute(client);
        }

        public void OnHouseholdGetComplete(Household household, Exception error)
        {
            if (error != null)
            {
                Console.WriteLine("Failed to get household: " + error.Message);
                code = -1;
                openTasks = 0;
                return;
            }

            openTasks--;
        }

        private void SearchCatalog()
        {
            openTasks++;

            // pager not working
            FilterPager pager = new FilterPager();
            pager.PageSize = 50;
            pager.PageIndex = 1;

            SearchAssetFilter filter = new SearchAssetFilter();
            filter.OrderBy = AssetOrderBy.NAME_DESC;

            AssetService.List(filter, pager)
                .SetCompletion(new OnCompletedHandler<ListResponse<Asset>>(OnCatalogSearchComplete))
                .Execute(client);
        }

        public void OnCatalogSearchComplete(ListResponse<Asset> assetsList, Exception error)
        {
            if (error != null)
            {
                Console.WriteLine("Failed to search catalog: " + error.Message);
                code = -1;
                openTasks = 0;
                return;
            }

            openTasks--;
        }

        private void GetHouseholdDevice()
        {
            openTasks++;
            HouseholdDeviceService.Get()
                .SetCompletion(new OnCompletedHandler<HouseholdDevice>(OnHouseholdDeviceGetComplete))
                .Execute(client);
        }

        public void OnHouseholdDeviceGetComplete(HouseholdDevice householdDevice, Exception error)
        {
            if (error != null)
            {
                Console.WriteLine("Failed to get household device: " + error.Message);
                code = -1;
                openTasks = 0;
                return;
            }

            openTasks--;
        }

        private void AddHouseholdDevice()
        {
            openTasks++;
            HouseholdDeviceService.Delete(MASTER_DEVICE)
                .SetCompletion(new OnCompletedHandler<bool>(OnHouseholdDeviceDeleteComplete))
                .Execute(client);
        }

        public void OnHouseholdDeviceDeleteComplete(bool success, Exception error)
        {
            if (error != null)
            {
                if (error is APIException)
                {
                    APIException exception = error as APIException;
                    if (exception.Code != APIException.DeviceNotInDomain)
                    {
                        Console.WriteLine("Failed to delete household device: " + error.Message);
                        code = -1;
                        openTasks = 0;
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Failed to delete household device: " + error.Message);
                    code = -1;
                    openTasks = 0;
                    return;
                }

            }

            HouseholdDevice newDevice = new HouseholdDevice();
            newDevice.Name = MASTER_DEVICE;
            newDevice.Udid = MASTER_DEVICE;
            newDevice.BrandId = MASTER_DEVICE_BRAND;

            HouseholdDeviceService.Add(newDevice)
                .SetCompletion(new OnCompletedHandler<HouseholdDevice>(OnHouseholdDeviceAddComplete))
                .Execute(client);
        }

        public void OnHouseholdDeviceAddComplete(HouseholdDevice householdDevice, Exception error)
        {
            if (error != null)
            {
                Console.WriteLine("Failed to delete household device: " + error.Message);
                code = -1;
                openTasks = 0;
                return;
            }

            openTasks--;
        }
    }
}
