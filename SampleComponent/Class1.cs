using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace SampleComponent
{

    //public delegate void AssistResponseEventHandler<TEventArgs>(object sender, TEventArgs e);

    public sealed class Assist
    {
        /*
         * reference: 
         *  https://msdn.microsoft.com/en-us/library/hh972883.aspx
         *  https://docs.microsoft.com/en-us/windows/uwp/winrt-components/walkthrough-creating-a-simple-windows-runtime-component-and-calling-it-from-javascript#prerequisites
         */

        private EventRegistrationTokenTable<EventHandler<AssistResponse>> onAssistResponseTokenTable = null;

        #region Instance
        private static readonly Assist _instance = new Assist();
        public static Assist Instance { get { return _instance; } }
        #endregion

        #region Event
        public event EventHandler<AssistResponse> OnAssistResponsed
        {
            add
            {
                return EventRegistrationTokenTable<EventHandler<AssistResponse>>
                    .GetOrCreateEventRegistrationTokenTable(ref onAssistResponseTokenTable)
                    .AddEventHandler(value);
            }
            remove
            {
                EventRegistrationTokenTable<EventHandler<AssistResponse>>
                    .GetOrCreateEventRegistrationTokenTable(ref onAssistResponseTokenTable)
                    .RemoveEventHandler(value);
            }
        }
        #endregion

        void OnAssistResponse(AssistResponse arg)
        {
            EventRegistrationTokenTable<EventHandler<AssistResponse>>
                .GetOrCreateEventRegistrationTokenTable(ref onAssistResponseTokenTable)
                .InvocationList?.Invoke(this, arg);
        }

        public static string SendAssistResponse()
        {
            //OnAssistResponse(new AssistResponse()
            //{
            //    testresponse = DateTime.UtcNow.ToString()
            //});
            return "hello world";
        }
    }

    public sealed class AssistResponse
    {
        public string testresponse { get; set; }
    }
}
