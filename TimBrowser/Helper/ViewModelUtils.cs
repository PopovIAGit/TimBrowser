using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Model;
using TimBrowser.ViewModels;

namespace TimBrowser.Helper
{
    public static class ViewModelUtils
    {
        public static List<ParameterSortItem> CreateParameterSortItem(List<TimParameterItem> timParameters,
            System.Action<ParameterSortItem> checkedChangedAction)
        {
            if (timParameters == null)
                return null;

            List<ParameterSortItem> sortParameters = new List<ParameterSortItem>();

            int counter = 0;

            bool defaultSet = false;

            foreach (var p in timParameters)
            {
                if (counter == 0)
                {
                    sortParameters.Add(new ParameterSortItem
                        (defaultSet, "Выделить все", Constants.CHOOSE_ALL_INDEX, checkedChangedAction));
                }
                else
                {
                    // исключаем добавление повторяющихся параметров в список
                    if (sortParameters.Where(sp => sp.Index == p.Index).FirstOrDefault() == null)
                    {
                        // по умолчанию все параметры выключены
                        sortParameters.Add(new ParameterSortItem(defaultSet, p.Name, p.Index, checkedChangedAction));
                    }
                }

                counter++;
            }

            return sortParameters;

        }

        public static List<ParameterSortItemAndCmd> CreateParameterSortItemAndCmd(List<TimParameterItem> timParameters,
            System.Action<ParameterSortItemAndCmd> checkedChangedAction)
        {
            if (timParameters == null)
                return null;

            List<ParameterSortItemAndCmd> sortParameters = new List<ParameterSortItemAndCmd>();

            int counter = 0;

            bool defaultSet = false;

            foreach (var p in timParameters)
            {
                if (counter == 0)
                {
                    sortParameters.Add(new ParameterSortItemAndCmd
                        (defaultSet, "Выделить все", Constants.CHOOSE_ALL_INDEX, checkedChangedAction));
                }
                else
                {
                    // исключаем добавление повторяющихся параметров в список
                    if (sortParameters.Where(sp => sp.Index == p.Index).FirstOrDefault() == null)
                    {
                        // по умолчанию все параметры выключены
                        sortParameters.Add(new ParameterSortItemAndCmd(defaultSet, p.Name, p.Index, checkedChangedAction));
                    }
                }

                counter++;
            }

            return sortParameters;

        }
    }
}
