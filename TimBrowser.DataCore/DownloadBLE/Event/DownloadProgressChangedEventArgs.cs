using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.DownloadBLE.Events
{
    public class DownloadProgressChangedEventArgs : EventArgs
    {
        public DownloadProgressChangedEventArgs(int currentIndex, int maxIndex, int progress)
        {
            _currentIndex = currentIndex;
            _maxIndex = maxIndex;
            _progress = progress;
        }

        #region Fields

        private int _currentIndex;
        private int _maxIndex;
        private int _progress;

        #endregion

        #region Properties

        public int CurrentIndex
        {
            get { return _currentIndex; }
        }

        public int MaxIndex
        {
            get { return _maxIndex; }
        }

        public int Progress
        {
            get { return _progress; }
        }

        #endregion



    }
}
