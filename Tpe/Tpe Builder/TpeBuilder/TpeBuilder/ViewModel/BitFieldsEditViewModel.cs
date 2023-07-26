using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using TpeBuilder.Model;

namespace TpeBuilder.ViewModel
{
    public class BitFieldsEditViewModel : ViewModelBase
    {
        private ObservableCollection<TpeParameterFieldItem> _fieldItems;
        private TpeParameterFieldItem _selectedBitField;



        public void AddBitField()
        {
            FieldItems.Add(new TpeParameterFieldItem(0, "Описание", ""));
        }

        public void RemoveBitField()
        {
            if (_selectedBitField == null)
                return;

            FieldItems.Remove(_selectedBitField);
        }


        public ObservableCollection<TpeParameterFieldItem> FieldItems
        {
            get { return _fieldItems; }
            set
            {
                _fieldItems = value;
                NotifyPropertyChanged("FieldItems");
            }
        }

        public TpeParameterFieldItem SelectedBitField
        {
            get { return _selectedBitField; }
            set
            {
                _selectedBitField = value;
                NotifyPropertyChanged("SelectedBitField");
            }
        }
    }
}
