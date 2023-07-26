using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Download.Model;

namespace TimBrowser.DataCore.DownloadBLE
{
    public partial class DownloadInformationModule
    {

        private FuncSixData FuncSixDownload(FuncOneData funcOneData)
        {
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

                System.Threading.Thread.Sleep(30);
                TransmitByte(funcCodeTransmit);                             // Передаем код функции
                System.Threading.Thread.Sleep(30);                          // Приостанавливаем поток
                TransmitByte(currentParamMsb);                              // Передаем старший байт адреса первого параметра в группе
                System.Threading.Thread.Sleep(30);
                TransmitByte(currentParamLsb);                              // Передаем младший байт адреса первого параметра в группе
                System.Threading.Thread.Sleep(30);

                byte funcCodeReceive = ReceiveByte();                       // Считываем ответный код функции
                //System.Threading.Thread.Sleep(20);

                if (funcCodeReceive != funcCodeTransmit)                    // Проверяем ответный код функции
                {
                    //ThrowDownloadException(DownloadErrorCode.FuncSixError);
                    //SendAlgorythmMessage(DataManagerRes.AlgFuncSixTransmitFuncCode, true);
                    //return null;
                    for (int i = 0; i < 40; i++) ReceiveByte();
                    System.Threading.Thread.Sleep(400);
                    continue;
                }

                byte currentParamMsbReceive = ReceiveByte();                // Считываем ответный старший байт адреса первого параметра в группе
                //System.Threading.Thread.Sleep(20);
                byte currentParamLsbReceive = ReceiveByte();                // Считываем ответный младший байт адреса первого параметра в группе
                //System.Threading.Thread.Sleep(20);
                int currentParamReceive = (int)((currentParamMsbReceive << 8) | (currentParamLsbReceive & 255));

                if (currentParamReceive != currentParam)                    // Проверяем ответвый байт адреса первого параметра в группе
                {
                    //ThrowDownloadException(DownloadErrorCode.FuncSixError);
                    //SendAlgorythmMessage(DataManagerRes.AlgFuncSixCurrentParam, true);
                    //return null;
                    for (int i = 0; i < 40; i++) ReceiveByte();
                    System.Threading.Thread.Sleep(400);
                    continue;
                }

                byte[] funcData = ReceiveBytes(readParamsNum * 2);          // Умножением на 2 учитываем старший и младший байты

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
