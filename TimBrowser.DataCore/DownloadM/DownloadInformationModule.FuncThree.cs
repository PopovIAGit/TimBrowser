using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.DownloadM.Model;

namespace TimBrowser.DataCore.DownloadM
{
    public partial class DownloadInformationModule
    {
        private FuncThreeData FuncThreeDownload(FuncOneData funcOneData)
        {
            byte funcCodeTransmit = 3;
            ushort[] txData = new ushort[2];
            ushort[] rxData = new ushort[20];

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

                txData[0] = (ushort)funcCodeTransmit;// Передаем код функции
                txData[1] = (ushort)logType;// Передаем тип журнала, у которого будет считывать данные
                TransmitBytes(_communication.DeviceAddress, 65123, txData);
                                           
                System.Threading.Thread.Sleep(30);

                byte funcCodeReceive = 0;// ReceiveByte();                                   // Считываем ответный код функции
                byte logTypeReceive = 0;// ReceiveByte();                                   // Считываем ответный тип журнала
                byte funcDataSize = 0;// ReceiveByte();                                      // Считываем размер ответного пакета
                rxData = ReceiveBytes(_communication.DeviceAddress, 65123, 10);
                funcCodeReceive = (byte)rxData[0];
                logTypeReceive  = (byte)rxData[1];
                funcDataSize    = (byte)rxData[2];

                if (funcCodeTransmit != funcCodeReceive)                                // Проверяем ответный код функции
                {
                    System.Threading.Thread.Sleep(50);
                    logTypeIndex--;
                    continue;
                }

                if (logType != logTypeReceive)                                          // Проверяем ответный тип журнала
                {
                    System.Threading.Thread.Sleep(50);
                    logTypeIndex--;
                    continue;
                }

                if (funcDataSize <= 0)                                                  // Валидации размера ответного пакета
                {
                    System.Threading.Thread.Sleep(30);
                    logTypeIndex--;
                    continue;
                }

                byte logCellNumber = (byte)rxData[(int)FuncThreeIndicies.LogCellNum+3];       // Извлекаем количество ячеек у текущего типа журнала
                List<int> logCellFieldNumber = new List<int>();

                for (int i = ((int)FuncThreeIndicies.LogCellNum + 1); i < funcDataSize; i++)
                {
                    int currentCellFieldNum = (int)rxData[i+3];                         // Извлекаем число полей в каждой ячейке
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
