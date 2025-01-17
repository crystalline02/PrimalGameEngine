using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrimalEditor.Utilities
{
    internal static class Serializier
    {
        public static void ToFile<T>(T instance, string path)
        {
            try
            {
                var serializier = new  DataContractSerializer(typeof(T));
                // using关键字表明实例fs是一个具有自动资源释放功能的变量，当前作用域结束，fs自动调用Dispose方法（FileStream实现了IDisposable接口）
                using var fs = new FileStream(path, FileMode.Create);
                serializier.WriteObject(fs, instance);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                // TO DO: Log message to primal interface
            }
        }

        public static T? FromFile<T>(string path)
        {
            try
            {
                using var fs = new FileStream(path, FileMode.Open);
                var serializier = new DataContractSerializer(typeof(T));
                T? instance = (T?)serializier.ReadObject(fs);
                return instance;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // TO DO: Log message to primal interface
                return default(T);
            }
        }
    }
}
