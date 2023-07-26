using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpeParameters.Model;
using TpeBuilder.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace TpeBuilder.Model
{
    public class TpeTable : ModelBase
    {

        public TpeTable(int id)
        {
            _id = id;
            _deviceId = 0;
            _deviceName = "Новое устройство";
            _name = _deviceName;
            _firmwareVersion = 0;
            _tpeGroups = null;
        }

        #region Fields

        private int _id;
        private int _deviceId;
        private string _name;
        private string _deviceName;
        private double _firmwareVersion;
        private ObservableCollection<TpeGroup> _tpeGroups;

        private bool _isChangesSaved;


        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
        }

        public int DeviceId
        {
            get { return _deviceId; }
            set
            {
                _deviceId = value;
                NotifyPropertyChanged("DeviceId");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _deviceName = value;
                NotifyPropertyChanged("Name");
            }
        }

        public string DeviceName
        {
            get { return _deviceName; }
            set
            {
                _deviceName = value;
                NotifyPropertyChanged("DeviceName");

                Name = _deviceName;
            }
        }

        public double FirmwareVersion
        {
            get { return _firmwareVersion; }
            set
            {
                _firmwareVersion = value;
                NotifyPropertyChanged("FirmwareVersion");
            }
        }


        public ObservableCollection<TpeGroup> TpeGroups
        {
            get { return _tpeGroups; }
            set
            {
                _tpeGroups = value;
                NotifyPropertyChanged("TpeGroups");
            }
        }

        public bool IsChangesSaved
        {
            get { return _isChangesSaved; }
            set { _isChangesSaved = value; }
        }

        #endregion

    }
}
