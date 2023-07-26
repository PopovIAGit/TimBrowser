using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpeParameters.Model;
using TpeBuilder.Model;
using System.Windows;
using System.Collections.ObjectModel;
using TpeBuilder.Events;
using TpeBuilder.Helpers;
using System.Windows.Controls;
using TpeBuilder.Views;
using TpeBuilder.Services;



namespace TpeBuilder.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel() : base()
        {
            _canCreateNewTable = true;

            Storage.Data.Instance.OnTreeViewItemSelected = TreeViewItemSelectedHandler;
        }


        #region Fields

        private ObservableCollection<TpeTable> _currentTpeTable;
        private TpeGroup _currentTpeGroup;
        private TpeParameter _currentTpeParameter;

        private TpeModelTypes _currentTpeType;

        private int _groupsIdCounter;
        private int _paramIdCounter;

        private UserControl _currentEditView;
        private bool _canCreateNewTable;

        #endregion

        #region Methods

        public void CreateNewTable()
        {
            /*
            TpeTable table = new TpeTable(1);

            table.DeviceName = "Новое устройство";
            table.FirmwareVersion = 1.01;

            table.TpeGroups = new ObservableCollection<TpeGroup>();
            table.TpeGroups.Add(
                new TpeGroup(1));

            table.TpeGroups[0].Description = "Описание группы";
            table.TpeGroups[0].Name = "Название группы";
            table.TpeGroups[0].GroupType = TpeParameters.Helpers.GroupTypes.Show;

            table.TpeGroups = new ObservableCollection<TpeGroup>();
            table.TpeGroups.Add(
                new TpeGroup(1));

            table.TpeGroups[0].TpeParameters = new ObservableCollection<TpeParameter>();

            table.TpeGroups[0].TpeParameters.Add(
                new TpeParameter(1));

            table.TpeGroups[0].TpeParameters[0].Address = 0;
            table.TpeGroups[0].TpeParameters[0].Appointment = TpeParameters.Helpers.ParamAppointments.Regular;
            table.TpeGroups[0].TpeParameters[0].Coefficient = 1;
            table.TpeGroups[0].TpeParameters[0].Comment = "Комментарий";
            table.TpeGroups[0].TpeParameters[0].Default = 0;
            table.TpeGroups[0].TpeParameters[0].Description = "Описание парамтра";
            table.TpeGroups[0].TpeParameters[0].Index = "А0";
            table.TpeGroups[0].TpeParameters[0].IsChosen = true;
            table.TpeGroups[0].TpeParameters[0].Maximum = 65530;
            table.TpeGroups[0].TpeParameters[0].Minimum = 0;
            table.TpeGroups[0].TpeParameters[0].Name = "Наименование параметра";
            table.TpeGroups[0].TpeParameters[0].ParamType = TpeParameters.Helpers.ParamTypes.Public;
            table.TpeGroups[0].TpeParameters[0].Unit = "Ед.изм";
            table.TpeGroups[0].TpeParameters[0].ValueType = TpeParameters.Helpers.ParamValueTypes.Uns;
            table.TpeGroups[0].TpeParameters[0].VariableName = "Наименование переменной";

            _currentTpeTable = new ObservableCollection<TpeTable>();
            _currentTpeTable.Add(table);


             * */



            _currentTpeTable = new ObservableCollection<TpeTable>()
            {
                new TpeTable(1)
            };
           
            CanCreateNewTable = false;

            Storage.Data.Instance.CurrentTpeTable = _currentTpeTable;
        }

        public void LoadTpe(string path)
        {
            FileService fileService = new FileService();

            if (_currentTpeTable == null)
            {
                _currentTpeTable = new ObservableCollection<TpeTable>();
                _currentTpeTable.Add(new TpeTable(1));
            }

            _currentTpeTable[0] = fileService.LoadTpe(path);

            Storage.Data.Instance.CurrentTpeTable = _currentTpeTable;
        }

        public void LoadXml(string path)
        {

        }

        public void SaveTPE()
        {
            FileService fileService = new FileService();

            fileService.SaveTableToTpe(_currentTpeTable[0]);
        }

        public void SaveXML()
        {
            FileService fileService = new FileService();

            fileService.SaveTableToXml(_currentTpeTable[0]);
        }

        public void AddGroup()
        {
            if (_currentTpeTable == null)
                return;

            if (_currentTpeTable[0].TpeGroups == null)
                _currentTpeTable[0].TpeGroups = new ObservableCollection<TpeGroup>();

            _groupsIdCounter++;

            _currentTpeTable[0].TpeGroups.Add(new TpeGroup(_groupsIdCounter));

        }

        public void AddParameter()
        {
            if (_currentTpeType == TpeModelTypes.TpeGroup)
            {
                if (_currentTpeGroup != null)
                {
                    if (CurrentTpeGroup.TpeParameters == null)
                    {
                        CurrentTpeGroup.TpeParameters = new ObservableCollection<TpeParameter>();
                    }

                    _paramIdCounter++;
                    CurrentTpeGroup.TpeParameters.Add(new TpeParameter(_paramIdCounter));
                }
            }
        }

        public void DeleteGroup()
        {
            if (_currentTpeGroup == null)
                return;

            var groupItem = _currentTpeTable[0].TpeGroups.Where(g => g.Id == _currentTpeGroup.Id).FirstOrDefault();

            if (groupItem == null)
                return;

            _currentTpeTable[0].TpeGroups.Remove(groupItem);
        }

        public void DeleteParameter()
        {
            if (_currentTpeParameter == null)
                return;

            foreach (var group in _currentTpeTable[0].TpeGroups)
            {
                var paramItem = group.TpeParameters.Where(p => p.Id == _currentTpeParameter.Id).FirstOrDefault();

                if (paramItem != null)
                {
                    group.TpeParameters.Remove(paramItem);

                    break;
                }
            }
        }


        #region Events

        private void OnTableUpdated()
        {
            _groupsIdCounter = 0;
            _paramIdCounter = 0;
        }

        private void OnGroupAdded()
        {
            
        }

        public void UpSort()
        {
            SortAction(true);
        }

        public void DownSort()
        {
            SortAction(false);
        }

        public void SortAction(bool isUp)
        {
            if (_currentTpeTable == null)
                return;

            switch (_currentTpeType)
            {
                case TpeModelTypes.TpeGroup:
                    SortGroup(isUp);
                    break;

                case TpeModelTypes.TpeParameter:
                    SortParameter(isUp);
                    break;
            }
        }

        private void SortGroup(bool isUp)
        {
            int count = _currentTpeTable[0].TpeGroups.Count;

            if (count < 2)
                return;

            int index = _currentTpeTable[0].TpeGroups.IndexOf(_currentTpeGroup);

            _currentTpeTable[0].TpeGroups.RemoveAt(index);

            index = GetSortIndex(index, isUp, count);

            _currentTpeTable[0].TpeGroups.Insert(index, _currentTpeGroup);
        }


        public void SortParameter(bool isUp)
        {

            int count = _currentTpeGroup.TpeParameters.Count;

            if (count < 2)
                return;

            int index = _currentTpeGroup.TpeParameters.IndexOf(_currentTpeParameter);

            _currentTpeGroup.TpeParameters.RemoveAt(index);

            index = GetSortIndex(index, isUp, count);

            _currentTpeGroup.TpeParameters.Insert(index, _currentTpeParameter);
        }

        private int GetSortIndex(int index, bool isUp, int count)
        {
            int lastIndex = count - 1;

            if (index > 0 && index < lastIndex)
            {
                if (isUp)
                    index -= 1;
                else
                    index += 1;
            }
            else if (index == 0)
            {
                if (isUp)
                    index = lastIndex;
                else
                    index += 1;
            }
            else if (index == lastIndex)
            {
                if (isUp)
                    index -= 1;
                else
                    index = 0;
            }


            return index;
        }



        private void OnGroupSelected(TpeGroup selectedGroup)
        {
            // активация редактирования группы
        }

        private void OnParameterSelected(TpeParameter selectedParamter)
        {
            // активация редактирования параметра
        }

        private void TreeViewItemSelectedHandler(object sender, TreeViewItemSelectedEventArgs e)
        {
            Type selectedObjectType = e.SelectedItem.GetType();

            if (selectedObjectType.FullName.Contains(TpeModelTypes.TpeTable.ToString()))
            {
                _currentTpeType = TpeModelTypes.TpeTable;
            }
            else if (selectedObjectType.FullName.Contains(TpeModelTypes.TpeGroup.ToString()))
            {
                _currentTpeType = TpeModelTypes.TpeGroup;
            }
            else if (selectedObjectType.FullName.Contains(TpeModelTypes.TpeParameter.ToString()))
            {
                _currentTpeType = TpeModelTypes.TpeParameter;
            }



            switch (_currentTpeType)
            {
                case TpeModelTypes.TpeTable:

                    TableEditView tableView = new TableEditView();
                    tableView.Init(_currentTpeTable[0]);
                    CurrentEditView = tableView;
                        break;

                case TpeModelTypes.TpeGroup:
                    {
                        CurrentTpeGroup = (TpeGroup)e.SelectedItem;

                        GroupEditView groupView = new GroupEditView();
                        groupView.Init(CurrentTpeGroup);
                        CurrentEditView = groupView;
                    }
                    break;

                case TpeModelTypes.TpeParameter:
                    CurrentTpeParameter = (TpeParameter)e.SelectedItem;

                    ParameterEditView parameterView = new ParameterEditView();
                    parameterView.Init(CurrentTpeParameter);

                    parameterView.OnBitFieldsActivated = OnParameterBitFieldsActivated;
                    CurrentEditView = parameterView;

                    break;

                default:
                    CurrentEditView = null;
                    break;
            }

        }

        public Action<Window> ActivateModalWindow;

        public void OnParameterBitFieldsActivated()
        {
            BitFieldsEditView view = new BitFieldsEditView();

            if (_currentTpeParameter.Fields == null)
                _currentTpeParameter.Fields = new ObservableCollection<TpeParameterFieldItem>();

            view.Init(_currentTpeParameter.Fields);

            if (ActivateModalWindow != null)
                ActivateModalWindow(view);
        }

        #endregion


        #endregion

        #region Properties



        public TpeGroup CurrentTpeGroup
        {
            get { return _currentTpeGroup; }
            set
            {
                _currentTpeGroup = value;
                NotifyPropertyChanged("CurrentTpeGroup");

                OnGroupSelected(_currentTpeGroup);
            }
        }

        public TpeParameter CurrentTpeParameter
        {
            get { return _currentTpeParameter; }
            set
            {
                _currentTpeParameter = value;
                NotifyPropertyChanged("CurrentTpeParameter");

                OnParameterSelected(_currentTpeParameter);
            }
        }

        public UserControl CurrentEditView
        {
            get { return _currentEditView; }
            set
            {
                _currentEditView = value;
                NotifyPropertyChanged("CurrentEditView");
            }
        }

        public bool CanCreateNewTable
        {
            get { return _canCreateNewTable; }
            set
            {
                _canCreateNewTable = value;
                NotifyPropertyChanged("CanCreateNewTable");
            }
        }

        #endregion

    }
}