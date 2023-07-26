using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Model;
using TimBrowser.Model;
using TpeParameters.Helpers;

namespace TimBrowser.Services.Print
{
    public class PrintTableService
    {

        public PrintTableService()
        {

        }


        #region Methods

        public List<PrintTableItem> PrintTablesRequest(List<PrintSelectionItem> printSelections, InformationModuleData informationModule,
                                                                string mainTitle, string subTitle)
        {
            PrintTableItemsCreator printTableCreator = new PrintTableItemsCreator();
            List<PrintTableItem> printTables = new List<PrintTableItem>();

            foreach (var ps in printSelections)
            {
                if (!ps.IsSet)
                    continue;

                PrintTableItem currentPrintTableItem = null;

                switch (ps.Id)
                {
                    case PrintSelectionIds.LogEvId: currentPrintTableItem = 
                            printTableCreator.CreateEvLogPrintTable(informationModule, mainTitle, subTitle); break;

                    case PrintSelectionIds.LogCmdId: currentPrintTableItem = 
                            printTableCreator.CreateCmdLogPrintTable(informationModule); break;

                    case PrintSelectionIds.LogParamId: currentPrintTableItem = 
                            printTableCreator.CreateParamLogPrintTable(informationModule); break;

                    case PrintSelectionIds.ParamGroupShow: currentPrintTableItem =
                            printTableCreator.CreateParamListPrintTable(informationModule, GroupTypes.Show,
                            "Параметры группы A"); break;

                    case PrintSelectionIds.ParamGroupUser: currentPrintTableItem = 
                            printTableCreator.CreateParamListPrintTable(informationModule, GroupTypes.User,
                            "Параметры группы B"); break;

                    case PrintSelectionIds.ParamGroupFactory: currentPrintTableItem =
                            printTableCreator.CreateParamListPrintTable(informationModule, GroupTypes.Factory,
                            "Параметры группы C"); break;

                    case PrintSelectionIds.ParamGroupHide: currentPrintTableItem =
                            printTableCreator.CreateParamListPrintTable(informationModule, GroupTypes.Hide,
                            "Параметры группы H"); break;
                }

                printTables.Add(currentPrintTableItem);

            }

            return printTables;
        }


        public List<PrintSelectionItem> GetPrintListSelections()
        {
            List<PrintSelectionItem> printSelections = new List<PrintSelectionItem>()
            {
                new PrintSelectionItem(PrintSelectionIds.LogEvId,           "Журнал событий", false),
                new PrintSelectionItem(PrintSelectionIds.LogCmdId,          "Журнал команд", false),
                new PrintSelectionItem(PrintSelectionIds.LogParamId,        "Журнал изменения параметров", false),
                new PrintSelectionItem(PrintSelectionIds.ParamGroupShow,    "Параметры группы A", false),
                new PrintSelectionItem(PrintSelectionIds.ParamGroupUser,    "Параметры группы B", false),
                new PrintSelectionItem(PrintSelectionIds.ParamGroupFactory, "Параметры группы C", false),
                new PrintSelectionItem(PrintSelectionIds.ParamGroupHide,    "Параметры группы H", false)
            };

            return printSelections;

        }

        #endregion



    }
}
