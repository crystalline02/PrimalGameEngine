using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PrimalEditor.Utilities
{
    enum MessageType
    {
        Info = 0x01,
        Warning = 0x02,
        Error = 0x04
    }

    class LoggerMessage
    {
        public DateTime Time { get; }
        public MessageType Type { get; }
        public string Message { get; } = string.Empty;
        public string File { get; } = string.Empty;
        public string Caller { get; } = string.Empty;
        public int Line { get; } = 0;
        public string MetaData => $"[{File}]: {Caller}({Line})";
        public LoggerMessage(string message, MessageType type = MessageType.Info, 
            [CallerFilePath]string file =" ", [CallerMemberName]string caller = "", [CallerLineNumber]int line = 0)
        {
            Time = DateTime.Now;
            Message = message;
            Type = type;
            File = file;
            Caller = caller;
            Line = line;
        }
    }

    static class Logger
    {
        static private int _messageMask = (int)(MessageType.Info | MessageType.Warning | MessageType.Error);
        static public int MessageMask {
            private get => _messageMask;
            set {
                if (_messageMask != value)
                {
                    _messageMask = value;
                    MessageFiler.View.Refresh();  // Refresh view with the current massage mask.MessageFilter.Filter event is invoked immediately.
                }
            } 
        }
        static private ObservableCollection<LoggerMessage> _messages;
        static public ReadOnlyObservableCollection<LoggerMessage> Messages { get; }
        static public CollectionViewSource MessageFiler { get; }

        public static async Task Log(LoggerMessage message)
        {
            // BeginInvoke是一个异步函数，往主线程添加一个Action，这个action保证异步执行，也就是会在主线程空闲时执行
            await Application.Current.Dispatcher.BeginInvoke(() => { _messages.Add(message); });
        }

        public static async Task Clear()
        {
            // BeginInvoke是一个异步函数，往主线程添加一个Action，这个action保证异步执行，也就是会在主线程空闲时执行
            await Application.Current.Dispatcher.BeginInvoke(() => { _messages.Clear(); });
        }

        static Logger()
        {
            Messages = new ReadOnlyObservableCollection<LoggerMessage>(_messages);
            MessageFiler = new CollectionViewSource() { Source = Messages };

            MessageFiler.Filter += (s, e) =>
            {
                MessageType currentType = (e.Item as LoggerMessage).Type;
                e.Accepted = ((int)currentType & MessageMask) != 0;  // If there is any bit in currentType which equals to that of the MessageMask, its accepted.Otherwies filtered.
            };

        }
    }
}
