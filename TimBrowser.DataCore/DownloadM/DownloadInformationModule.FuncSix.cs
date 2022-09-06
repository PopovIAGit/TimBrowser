using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.DownloadM.Model;

namespace TimBrowser.DataCore.DownloadM
{
    public partial class DownloadInformationModule
    {

        private FuncSixData FuncSixDownload(FuncOneData funcOneData)
        {
            ushort[] txData = new ushort[4];
            ushort[] rxData = new ushort[200];

            byte funcCodeTransmit = 6;
            bool receiveComplete = false;                                       // Флаг завершения считывания параметров
            int readParamsNum = Helpers.Constants.ReadParamsPacketNum;          // Количество параметров, которые считываются за один запрос

            int currentParamIndex = 0;

            int paramsNum = funcOneData.ParametersNumber;

            if (paramsNum == 0)                                                 // Валидация количества параметров
            {
                ThrowDownloadException(DownloadErrorCode.FuncSixError);
                //SendAlgorythmMessage(DataManagerRes.AlgFuncSixParamsNum, true);
                return null;
            }

            List<int> parametersValuesList = new List<int>();

            //progressIndex++;

            while (!receiveComplete)
            {

                if (readParamsNum >= paramsNum)                             // Если количество параметров, которые считываются за раз
                {                                                           // больше, чем количество параметров, которые осталось считать
                    readParamsNum = paramsNum;
                    receiveComplete = true;
                }

                int currentParam = currentParamIndex * readParamsNum;       // Считаем адрес первого параметра в группе. Размер группы = readParamsNum
                byte currentParamMsb = (byte)(currentParam >> 8);
                byte currentParamLsb = (byte)(currentParam & 255);

                if (currentParam >= (paramsNum - readParamsNum))
                {
                    readParamsNum = paramsNum - currentParam;
                    receiveComplete = true;
                }

                /*System.Threading.Thread.Sleep(30);
                //TransmitByte(funcCodeTransmit);                            // Передаем код функции
                System.Threading.Thread.Sleep(30);                           // Приостанавливаем поток
                //TransmitByte(currentParamMsb);                             // Передаем старший байт адреса первого параметра в группе
                System.Threading.Thread.Sleep(30);
                //TransmitByte(currentParamLsb);                             // Передаем младший байт адреса первого параметра в группе
                System.Threading.Thread.Sleep(30);
                */

                txData[0] = (ushort)funcCodeTransmit;                        // Передаем код функции
                txData[1] = (ushort)currentParamMsb;                         // Передаем старший байт адреса первого параметра в группе
                txData[2] = (ushort)currentParamLsb;                         // Передаем младший байт адреса первого параметра в группе
                TransmitBytes(_communication.DeviceAddress, 65123, txData);
                System.Threading.Thread.Sleep(30);

                rxData = ReceiveBytes(_communication.DeviceAddress, 65123, readParamsNum * 2+4); //ReceiveBytes(readParamsNum * 2);          // Умножением на 2 учитываем старший и младший байты

                ushort[] funcData = new ushort[readParamsNum * 2];

                byte funcCodeReceive = (byte)rxData[0];                       // Считываем ответный код функции
                byte currentParamMsbReceive = (byte)rxData[1];                // Считываем ответный старший байт адреса первого параметра в группе
                byte currentParamLsbReceive = (byte)rxData[2];                // Считываем ответный младший байт адреса первого параметра в группе

                int currentParamReceive = (int)((currentParamMsbReceive << 8) | (currentParamLsbReceive & 255));

                if (funcCodeReceive != funcCodeTransmit)                    // Проверяем ответный код функции
                {
                    System.Threading.Thread.Sleep(50);
                    continue;
                }

                if (currentParamReceive != currentParam)                    // Проверяем ответвый байт адреса первого параметра в группе
                {
                    System.Threading.Thread.Sleep(50);
                    continue;
                }

                //ushort[] funcData = ReceiveBytes(_communication.DeviceAddress, 0, readParamsNum * 2); //ReceiveBytes(readParamsNum * 2);          // Умножением на 2 учитываем старший и младший байты
                for (int i = 0; i < (readParamsNum*2); i++)
                {
                    funcData[i] = rxData[i+4];
                }

                for (int i = 0; i < readParamsNum; i++)
                {
                    // Извлекаем значение параметра
                    int paramValue = ((funcData[(int)FuncSixIndicies.ParamValueMsb + (i * 2)] << 8) |
                        (funcData[(int)FuncSixIndicies.ParamValueLsb + (i * 2)] & 255));

                    parametersValuesList.Add(paramValue);                   // Добавляем значение параметра в список
                }

                int progressParam = 0;
                if (!receiveComplete)
                    progressParam = currentParam;
                else
                    progressParam = paramsNum;

                int currentProgress = Utils.CalculateProgress(progressParam, paramsNum, 100);

                // параметры всегда считываются последними, поэтому индекс указываем максимальный
                RaiseDownloadProgressChanged(_maxIndex, currentProgress);

                if (!receiveComplete)
                {
                    currentParamIndex++;
                }
            }

            return new FuncSixData(parametersValuesList);                   // Возвращаем объект шестой функции

        }

    }
}
