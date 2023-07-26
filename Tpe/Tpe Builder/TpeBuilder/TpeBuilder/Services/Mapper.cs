using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpeParameters.Model;
using TpeBuilder.Model;
using System.Collections.ObjectModel;

namespace TpeBuilder.Services
{
    public static class Mapper
    {
        #region ToTpe

        public static TableItem MapTableToTpe(TpeTable tpeTable)
        {
            List<GroupItem> groupItems = new List<GroupItem>();

            if (tpeTable.TpeGroups != null)
            {
                foreach (var item in tpeTable.TpeGroups)
                {
                    groupItems.Add(
                        MapGroupToTpe(item)
                        );
                }
            }

            TableItem tableItem = new TableItem(tpeTable.Id, tpeTable.DeviceId, tpeTable.DeviceName,
                tpeTable.FirmwareVersion, groupItems, null);

            return tableItem;
        }

        private static GroupItem MapGroupToTpe(TpeGroup tpeGroup)
        {
            if (tpeGroup == null)
                return null;

            List<ParameterItem> parameterItems = new List<ParameterItem>();

            if (tpeGroup.TpeParameters != null)
            {
                foreach (var item in tpeGroup.TpeParameters)
                {
                    parameterItems.Add(MapParameterToTpe(item));
                }
            }

            GroupItem groupItem = new GroupItem(
                tpeGroup.Id, tpeGroup.Name, tpeGroup.GroupType, tpeGroup.Description,
                parameterItems);

            return groupItem;
        }

        private static ParameterItem MapParameterToTpe(TpeParameter tpeParameter)
        {
            if (tpeParameter == null)
                return null;

            ParameterConfiguration parameterConfiguration = new ParameterConfiguration(
                tpeParameter.ParamType, tpeParameter.Appointment, tpeParameter.CanEdit, tpeParameter.IsChosen);

            ParameterValueDescription parameterValueDescription = new ParameterValueDescription(
                tpeParameter.Minimum, tpeParameter.Maximum, tpeParameter.Default,
                tpeParameter.Unit, tpeParameter.Coefficient, tpeParameter.ValueType,
                MapBitFieldsToTpe(tpeParameter.Fields));

            ParameterInfo parameterInfo = new ParameterInfo(tpeParameter.Description, tpeParameter.Comment);

            ParameterItem parameterItem = new ParameterItem(
                tpeParameter.Id, tpeParameter.Index, tpeParameter.Name, tpeParameter.Address,
                tpeParameter.VariableName, 0, "0",
                parameterConfiguration, parameterValueDescription, parameterInfo);

            return parameterItem;
        }

        private static List<ParameterFieldItem> MapBitFieldsToTpe(ObservableCollection<TpeParameterFieldItem> tpeFields)
        {
            if (tpeFields == null)
                return null;

            List<ParameterFieldItem> fields = new List<ParameterFieldItem>();

            foreach (var item in tpeFields)
            {
                fields.Add(new ParameterFieldItem(item.BitValue, item.Description, item.SpecialDescription));
            }

            return fields;
        }

        #endregion

        #region FromTpe

        public static TpeTable MapTableFromTpe(TableItem tpeTableItem)
        {
            if  (tpeTableItem == null)
                return null;

            ObservableCollection<TpeGroup> groupItems = new ObservableCollection<TpeGroup>();

            if (tpeTableItem.Groups != null)
            {
                foreach (var item in tpeTableItem.Groups)
                {
                    groupItems.Add(
                        MapGroupFromTpe(item)
                        );
                }
            }

            TpeTable tableItem = new TpeTable(tpeTableItem.Id);
            tableItem.DeviceId = tpeTableItem.DeviceId;
            tableItem.DeviceName = tpeTableItem.DeviceName;
            tableItem.FirmwareVersion = tpeTableItem.FirmwareVersion;
            tableItem.TpeGroups = groupItems;

            return tableItem;
        }

        private static TpeGroup MapGroupFromTpe(GroupItem tpeGroupItem)
        {
            if (tpeGroupItem == null)
                return null;


            ObservableCollection<TpeParameter> parameterItems = new ObservableCollection<TpeParameter>();

            if (tpeGroupItem.Parameters != null)
            {
                foreach (var item in tpeGroupItem.Parameters)
                {
                    parameterItems.Add(MapParameterFromTpe(item));
                }
            }

            TpeGroup groupItem = new TpeGroup(tpeGroupItem.Id);
            groupItem.Name = tpeGroupItem.Name;
            groupItem.GroupType = tpeGroupItem.GroupType;
            groupItem.Description = tpeGroupItem.Description;
            groupItem.TpeParameters = parameterItems;

            return groupItem;
        }

        private static TpeParameter MapParameterFromTpe(ParameterItem tpeParameterItem)
        {
            if (tpeParameterItem == null)
                return null;

            TpeParameter tpeParam = new TpeParameter(tpeParameterItem.Id);

            tpeParam.Index = tpeParameterItem.Index;
            tpeParam.Name = tpeParameterItem.Name;
            tpeParam.Address = tpeParameterItem.Address;
            tpeParam.VariableName = tpeParameterItem.VariableName;

            // Configuration
            tpeParam.ParamType = tpeParameterItem.Configuration.ParamType;
            tpeParam.CanEdit = tpeParameterItem.Configuration.CanEdit;
            tpeParam.IsChosen = tpeParameterItem.Configuration.IsChosen;
            tpeParam.Appointment = tpeParameterItem.Configuration.Appointment;

            // Value Description

            tpeParam.Minimum = tpeParameterItem.ValueDescription.Minimum;
            tpeParam.Maximum = tpeParameterItem.ValueDescription.Maximum;
            tpeParam.Default = tpeParameterItem.ValueDescription.Default;
            tpeParam.Unit = tpeParameterItem.ValueDescription.Unit;
            tpeParam.Coefficient = tpeParameterItem.ValueDescription.Coefficient;
            tpeParam.ValueType = tpeParameterItem.ValueDescription.ValueType;
            tpeParam.Minimum = tpeParameterItem.ValueDescription.Minimum;
            tpeParam.Fields = MapBitFieldsFromTpe(tpeParameterItem.ValueDescription.Fields);

            // Info
            tpeParam.Description = tpeParameterItem.Info.Description;
            tpeParam.Comment = tpeParameterItem.Info.Comment;

            return tpeParam;
        }

        private static ObservableCollection<TpeParameterFieldItem> MapBitFieldsFromTpe(List<ParameterFieldItem> tpeFields)
        {
            if (tpeFields == null)
                return null;

            ObservableCollection<TpeParameterFieldItem> fields = new ObservableCollection<TpeParameterFieldItem>();

            foreach (var item in tpeFields)
            {
                fields.Add(new TpeParameterFieldItem(item.BitValue, item.Description, item.SpecialDescription));
            }

            return fields;
        }

        #endregion

    }
}
