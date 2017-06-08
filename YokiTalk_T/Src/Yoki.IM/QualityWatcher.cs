using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yoki.IM
{
    public class Quality
    {
        public DateTime StartTime;
        public DateTime EndTime;
        
        private System.Collections.Concurrent.ConcurrentQueue<double> fpsVideoQueue;
        public System.Collections.Concurrent.ConcurrentQueue<double> FpsVideoQueue
        {
            get
            {
                if (this.fpsVideoQueue == null)
                {
                    this.fpsVideoQueue = new System.Collections.Concurrent.ConcurrentQueue<double>();
                }
                return this.fpsVideoQueue;
            }
        }

        private System.Collections.Concurrent.ConcurrentQueue<double> fpsAudioQueue;
        public System.Collections.Concurrent.ConcurrentQueue<double> FpsAudioQueue
        {
            get
            {
                if (this.fpsAudioQueue == null)
                {
                    this.fpsAudioQueue = new System.Collections.Concurrent.ConcurrentQueue<double>();
                }
                return this.fpsAudioQueue;
            }
        }

        public void Clean()
        {
            this.StartTime = DateTime.MinValue;
            this.EndTime = DateTime.MinValue;

            fpsVideoQueue = new System.Collections.Concurrent.ConcurrentQueue<double>();
            fpsAudioQueue = new System.Collections.Concurrent.ConcurrentQueue<double>();
        }

    }
    public class WatchDataEventArgs : EventArgs
    {
        public Quality Quality
        {
            get;
            set;
        }

    }


    public delegate void FrameTickTockHandle();

    public class QulityCounter
    {
        public int VideoCounter
        {
            get;
            set;
        }
        public int AudioCounter
        {
            get;
            set;
        }

        public void Clean()
        {
            this.VideoCounter = 0;
            this.AudioCounter = 0;
        }
    }

    public delegate void QualityWatcherRiseHandler(WatchDataEventArgs args);
    public class QualityWatcher : IDisposable
    {
        public event QualityWatcherRiseHandler OnWatchReport;
        public event QualityWatcherRiseHandler OnWatchEnded;
        Quality q;
        QulityCounter qc;

        public static long Durition = 10000; //ms

        System.Threading.Timer timer;

        private QualityWatcher()
        {

        }
        
        private static QualityWatcher _instance = null;
        public static QualityWatcher Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new QualityWatcher();
                }
                return _instance;
            }
        }

        public void StartWatch()
        {
            EndWatch();
            q = new Quality();
            qc = new QulityCounter();

            timer = new System.Threading.Timer(new System.Threading.TimerCallback((o) => {
                q.FpsVideoQueue.Enqueue((double)qc.VideoCounter / Durition * 1000);
                q.FpsAudioQueue.Enqueue((double)qc.AudioCounter / Durition * 1000);

                qc.Clean();

                if (this.OnWatchReport != null)
                {
                    this.OnWatchReport(new WatchDataEventArgs() { Quality = q });
                }

            }), null, Durition, Durition);
        }

        public void VideoTickTock()
        {
            if (this.qc != null)
            {
                qc.VideoCounter++;
            }
        }

        public void AudioTickTock()
        {
            if (this.qc != null)
            {
                qc.AudioCounter++;
            }
        }

        public void EndWatch()
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }

            if (this.OnWatchEnded != null)
            {
                this.OnWatchEnded(new WatchDataEventArgs() { Quality = q });
            }
        }

        public void Dispose()
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
        }
    }
}
