using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Download.Model;

namespace TimBrowser.DataCore.Download
{
    public partial class DownloadInformationModule
    {
        private FuncThreeData FuncThreeDownload(FuncOneData funcOneData)
        {
            byte funcCodeTransmit = 3;

            int logsNum = funcOneData.LogsNumber;

            if (logsNum == 0)                                                           // Проверяем количество журналов в устройстве
            {
                ThrowDownloadException(DownloadErrorCode.FuncFiveError);
                //SendAlgorythmMessage(DataManagerRes.AlgFuncThreeLogsNum, true);
                return null;
            }

            List<FuncThreeItem> funcThreeSubDataList = new List<FuncThreeItem>();

            for (int logTypeIndex = 0; logTypeIndex < logsNum; logTypeIndex++)          // Считывание для каждого типа журнала отдельно
            {
                byte logType = funcOneData.LogsTypes[logTypeIndex];

                System.Threading.Thread.Sleep(100);
                TransmitByte(funcCodeTransmit);                                         // Передаем код функции
                System.Threading.Thread.Sleep(100);
                TransmitByte(logType);                                                  // Передаем тип журнала, у которого будет считывать данные
                System.Threading.Thread.Sleep(100);

                byte funcCodeReceive = ReceiveByte();                                   // Считываем ответный код функции
                byte logTypeReceive = ReceiveByte();                                   // Считываем ответный тип журнала

                if (funcCodeTransmit != funcCodeReceive)                                // Проверяем ответный код функции
                {
                    //ThrowDownloadException(DownloadErrorCode.FuncFiveError);
                    //SendAlgorythmMessage(DataManagerRes.AlgFuncThreeTransmitFuncCode, true);
                    //return null;
                    for (int i = 0; i < 50; i++) ReceiveByte();
                    System.Threading.Thread.Sleep(500);
                    logTypeIndex--;
                    continue;
                }

                if (logType != logTypeReceive)                                          // Проверяем ответный тип журнала
                {
                    //ThrowDownloadException(DownloadErrorCode.FuncFiveError);
                    //SendAlgorythmMessage(DataManagerRes.AlgFuncThreeLogType, true);
                    //return null;
                    for (int i = 0; i < 50; i++) ReceiveByte();
                    System.Threading.Thread.Sleep(500);
                    logTypeIndex--;
                    continue;
                }

                byte funcDataSize = ReceiveByte();                                      // Считываем размер ответного пакета

                if (funcDataSize <= 0)                                                  // Валидации размера ответного пакета
                {
                    //ThrowDownloadException(DownloadErrorCode.FuncFiveError);
                    //SendAlgorythmMessage(DataManagerRes.AlgFuncThreeFuncDataSize, true);
                    //return null;
                    for (int i = 0; i < 50; i++) ReceiveByte();
                    System.Threading.Thread.Sleep(500);
                    logTypeIndex--;
                    continue;
                }

                byte[] funcData = ReceiveBytes(funcDataSize);                           // Считывает пакет данных

                byte logCellNumber = funcData[(int)FuncThreeIndicies.LogCellNum];       // Извлекаем количество ячеек у текущего типа журнала
                List<int> logCellFieldNumber = new List<int>();

                for (int i = ((int)FuncThreeIndicies.LogCellNum + 1); i < funcDataSize; i++)
                {
                    int currentCellFieldNum = (int)funcData[i];                         // Извлекаем число полей в каждой ячейке
                    logCellFieldNumber.Add(currentCellFieldNum);
                }

                // Добавляем данный в список по текущему типу журнала
                funcThreeSubDataList.Add(new FuncThreeItem(logCellNumber, logCellFieldNumber));
                System.Threading.Thread.Sleep(50);

            }

            return new FuncThreeData(funcThreeSubDataList);                             // Возвращаем данные третьей функции

        }
    }
}
