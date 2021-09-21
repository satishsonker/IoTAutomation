﻿using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.BusinessLayer
{
    public class AdminBL
    {
        private readonly IAdmin _adminBL;
        public AdminBL(IAdmin iadmin)
        {
            _adminBL = iadmin;
        }
        public int AddDeviceType(DeviceType deviceType,string userKey)
        {
            return _adminBL.AddDeviceType(deviceType,userKey);
        }

        public int UpdateDeviceType(DeviceType deviceType, string userKey)
        {
            return _adminBL.UpdateDeviceType(deviceType, userKey);
        }

        public int DeleteDeviceType(int deviceTypeId, string userKey)
        {
            return _adminBL.DeleteDeviceType(deviceTypeId, userKey);
        }

        public IEnumerable<DeviceType> SearchDeviceType(string searchTerm, string userKey)
        {
            return _adminBL.SearchDeviceType(searchTerm, userKey);
        }

        public DeviceType GetDeviceType(int deviceTypeId, string userKey)
        {
            return _adminBL.GetDeviceType(deviceTypeId, userKey);
        }
        public IEnumerable<DeviceAction> GetAllDeviceType(string userKey)
        {
            return _adminBL.GetAllDeviceAction(userKey);
        }

        public int AddDeviceAction(DeviceAction deviceAction, string userKey)
        {
            if(deviceAction!=null)
            {
                deviceAction.CreatedDate = DateTime.Now;
                deviceAction.ModifiedDate = DateTime.Now;
            }
            return _adminBL.AddDeviceAction(deviceAction, userKey);
        }

        public int UpdateDeviceAction(DeviceAction deviceAction, string userKey)
        {
            return _adminBL.UpdateDeviceAction(deviceAction,userKey);
        }

        public int DeleteDeviceAction(int deviceActionId, string userKey)
        {
            return _adminBL.DeleteDeviceAction(deviceActionId, userKey);
        }

        public IEnumerable<DeviceAction> SearchDeviceAction(string searchTerm,string userKey)
        {
            return _adminBL.SearchDeviceAction(searchTerm, userKey);
        }

        public DeviceAction GetDeviceAction(int deviceActionId, string userKey)
        {
            return _adminBL.GetDeviceAction(deviceActionId, userKey);
        }
        public bool UpdateAdminPermission(List<UserPermission> userPermissions, string userKey)
        {
            if(userPermissions!=null)
            {
                foreach (UserPermission userPermission in userPermissions)
                {
                    userPermission.ModifiedDate = DateTime.Now;
                }
            }
            return _adminBL.UpdateAdminPermission(userPermissions, userKey);
        }
    }
}