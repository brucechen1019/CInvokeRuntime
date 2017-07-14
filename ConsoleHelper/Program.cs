using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelper
{
    class Program
    {
        public sealed class Assist
        {
            private static readonly Assist _instance = new Assist();
            private AssistResponseDelegate _del;
            public static Assist Instance { get { return _instance; } }

            public delegate void AssistResponseDelegate(AssistResponse r);

            public void Pub(AssistResponseDelegate del)
            {
                _del = (AssistResponseDelegate)Delegate.Combine(_del, del);
            }

            public void InvokeDemo()
            {
                var r = new AssistResponse() { testresponse = "test" };
                _del?.Invoke(r);
            }

            public class AssistResponse
            {
                public string testresponse { get; set; }
            }
        }
        static void Main(string[] args)
        {
            Assist.Instance.Pub((s) =>
            {
                Console.WriteLine("client1:" + s.testresponse);
            });

            Assist.Instance.Pub((s) =>
            {
                Console.WriteLine("client2:" + s.testresponse);
            });


            Assist.Instance.InvokeDemo();
        }
    }
}
