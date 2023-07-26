using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimBrowser.DataCore.DownloadBLE.Model
{
    [Serializable]
    public class FuncDownloadDataBLE
    {
        public FuncDownloadDataBLE(FuncOneData funcOne, FuncTwoData funcTwo,
                    FuncThreeData funcThree, FuncFourData funcFour,
                    FuncFiveData funcFive, FuncSixData funcSix,
                    DateTime dateTimeDownload) 
        {
            _funcOneData = funcOne;
            _funcTwoData = funcTwo;
            _funcThreeData = funcThree;
            _funcFourData = funcFour;
            _funcFiveData = funcFive;
            _funcSixData = funcSix;

            _dateTimeDownload = dateTimeDownload;
        }

        #region Fields

        private FuncOneData _funcOneData;
        private FuncTwoData _funcTwoData;
        private FuncThreeData _funcThreeData;
        private FuncFourData _funcFourData;
        private FuncFiveData _funcFiveData;
        private FuncSixData _funcSixData;

        private DateTime _dateTimeDownload;

        private bool _isSaved;

        #endregion

        #region Properties

        public FuncOneData FuncOne
        {
            get { return _funcOneData; }
        }

        public FuncTwoData FuncTwo
        {
            get { return _funcTwoData; }
        }

        public FuncThreeData FuncThree
        {
            get { return _funcThreeData; }
        }

        public FuncFourData FuncFour
        {
            get { return _funcFourData; }
        }

        public FuncFiveData FuncFive
        {
            get { return _funcFiveData; }
        }

        public FuncSixData FuncSix
        {
            get { return _funcSixData; }
        }

        public DateTime DateTimeDownload
        {
            get { return _dateTimeDownload; }
        }

        public bool IsSaved
        {
            get { return _isSaved; }
            internal set { _isSaved = value; }
        }

        #endregion
    }
}
