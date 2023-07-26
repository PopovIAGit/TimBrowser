using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpeParameters.Model;
using TpeBuilder.Model;
using System.Windows;

namespace TpeBuilder.ViewModel
{
    public class ParameterEditViewModel : ViewModelBase
    {

        private TpeParameter _currentParameterItem;
        private Visibility _fieldsButtonVisibility;
        private Visibility _fieldsListVisibility;

        public void ActivateFieldsList()
        {
            FieldsListVisibility = (FieldsListVisibility == Visibility.Visible) ?
                Visibility.Collapsed : Visibility.Visible;
        }


        private void ValueTypeChanged()
        {
            TpeParameters.Helpers.ParamValueTypes vt = _currentParameterItem.ValueType;

            if (vt == TpeParameters.Helpers.ParamValueTypes.Enum ||
                vt == TpeParameters.Helpers.ParamValueTypes.List ||
                vt == TpeParameters.Helpers.ParamValueTypes.Union)
                FieldsButtonVisibility = Visibility.Visible;
            else
                FieldsButtonVisibility = Visibility.Collapsed;
        }


        public Visibility FieldsButtonVisibility
        {
            get { return _fieldsButtonVisibility; }
            set
            {
                _fieldsButtonVisibility = value;
                NotifyPropertyChanged("FieldsButtonVisibility");
            }
        }


        public TpeParameter CurrentParameterItem
        {
            get { return _currentParameterItem; }
            set
            {
                _currentParameterItem = value;
                NotifyPropertyChanged("CurrentParameterItem");

                _currentParameterItem.OnValueTypeChanged = ValueTypeChanged;
                ValueTypeChanged();
            }
        }

        public Visibility FieldsListVisibility
        {
            get { return _fieldsListVisibility; }
            set
            {
                _fieldsListVisibility = value;
                NotifyPropertyChanged("FieldsListVisibility");
            }
        }

    }
}
